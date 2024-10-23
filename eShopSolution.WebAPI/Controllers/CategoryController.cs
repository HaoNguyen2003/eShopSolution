using AutoMapper;
using eShopSolution.BusinessLayer.Abstract;
using eShopSolution.cloudinaryManagerFile.Abstract;
using eShopSolution.DtoLayer.AddModel;
using eShopSolution.DtoLayer.Model;
using eShopSolution.DtoLayer.UpdateModel;
using eShopSolution.WebAPI.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace eShopSolution.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;
        private readonly ICloudinaryService _cloudinaryService;

        public CategoryController(ICategoryService categoryService, IMapper mapper, ICloudinaryService cloudinaryService)
        {
            _categoryService = categoryService;
            _mapper = mapper;
            _cloudinaryService = cloudinaryService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllCategory()
        {
            var result = await _categoryService.GetAll();
            return StatusCode(result.code, result.Value);
        }

        [HttpGet("{ID}")]
        public async Task<IActionResult> GetCategoryByID(int ID)
        {
            var result = await _categoryService.GetByID(ID);
            return StatusCode(result.code, result.Value);
        }
        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> AddCategory([FromForm] AddCategory addCategory)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (!WorkWithFile.IsImage(addCategory.CategoryImage))
                return StatusCode(500, "Please select Image");
            var UploadImageResult = await _cloudinaryService.UploadFile(addCategory.CategoryImage, "ImageEshop/Category");
            if (!UploadImageResult.IsSuccess)
                return StatusCode(500, UploadImageResult);
            var categoryModel = _mapper.Map<CategoryModel>(addCategory);
            categoryModel.ImageURl = UploadImageResult.Url;
            categoryModel.PublicID = UploadImageResult.PublicID;
            var result = await _categoryService.Create(categoryModel);
            if (result.code != 200)
                await _cloudinaryService.RemoveFileAsync(categoryModel.PublicID);
            return StatusCode(result.code, result.Value);
        }

        [HttpPut("{ID}")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UpdateCategory(int ID, [FromForm] UpdateCategory updateCategory)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var tempPublicID = "";
            var check = await _categoryService.GetByID(ID);
            if (check.code == 404)
                return StatusCode(404);
            var categoryModel = _mapper.Map<CategoryModel>(updateCategory);
            categoryModel.ID = ID;
            if (updateCategory.CategoryImage == null)
            {
                categoryModel.ImageURl = check.Value.ImageURl;
                categoryModel.PublicID = check.Value.PublicID;
            }
            else
            {
                if (!WorkWithFile.IsImage(updateCategory.CategoryImage))
                    return StatusCode(500, "Please select Image");
                var UploadImageResult = await _cloudinaryService.UploadFile(updateCategory.CategoryImage, "ImageEshop/Category");
                if (!UploadImageResult.IsSuccess)
                    return StatusCode(500, UploadImageResult);
                categoryModel.ImageURl = UploadImageResult.Url;
                categoryModel.PublicID = UploadImageResult.PublicID;
                tempPublicID = UploadImageResult.PublicID;
            }
            var result = await _categoryService.Update(ID, categoryModel);
            if (result.code != 200)
            {
                if (!string.IsNullOrEmpty(tempPublicID))
                    await _cloudinaryService.RemoveFileAsync(tempPublicID);
                return StatusCode(500, result);
            }
            if (updateCategory.CategoryImage != null)
                await _cloudinaryService.RemoveFileAsync(check.Value.PublicID);
            return StatusCode(200, result);
        }

        [HttpDelete("{ID}")]
        public async Task<IActionResult> DeleteCategoryByID(int ID)
        {
            var check = await _categoryService.GetByID(ID);
            if (check.code == 404)
                return StatusCode(404);
            var result = await _categoryService.Delete(ID);
            if (result.code == 200)
            {
                var resultRemove = await _cloudinaryService.RemoveFileAsync(check.Value.PublicID);
                if (!resultRemove.IsSuccess)
                    return StatusCode(500, resultRemove);
            }
            return StatusCode(result.code, result.Value);
        }
    }
}
