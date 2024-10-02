using AutoMapper;
using eShopSolution.BusinessLayer.Abstract;
using eShopSolution.DtoLayer.AddModel;
using eShopSolution.DtoLayer.Model;
using eShopSolution.DtoLayer.UpdateModel;
using Microsoft.AspNetCore.Mvc;

namespace eShopSolution.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryAndBrandController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICategoryAndBrandService _categoryAndBrandService;
        public CategoryAndBrandController(IMapper mapper, ICategoryAndBrandService categoryAndBrandService)
        {
            _mapper = mapper;
            _categoryAndBrandService = categoryAndBrandService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategoryAndBrand()
        {
            var result = await _categoryAndBrandService.GetAll();
            return StatusCode(result.code, result.Value);
        }

        [HttpGet("GetCategoryAndBrandByID/{ID}")]
        public async Task<IActionResult> GetCategoryAndBrandByID(int ID)
        {
            var result = await _categoryAndBrandService.GetByID(ID);
            return StatusCode(result.code, result.Value);
        }

        [HttpGet("GetCategoryByBrandID/{BrandID}")]
        public async Task<IActionResult> GetCategoryByBrandID(int BrandID)
        {
            var result = await _categoryAndBrandService.GetAllCategoryByBrandID(BrandID);
            return StatusCode(result.code, result.Value);
        }

        [HttpGet("GetBrandByCategoryID/{CategoryID}")]
        public async Task<IActionResult> GetBrandByCategoryID(int CategoryID)
        {
            var result = await _categoryAndBrandService.GetAllBrandByCategoryID(CategoryID);
            return StatusCode(result.code, result.Value);
        }

        [HttpPost]
        public async Task<IActionResult> AddCategoryAndBrand([FromBody] AddCategoryAndBrand addCategoryAndBrand)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var model = _mapper.Map<CategoryAndBrandModel>(addCategoryAndBrand);
            var result = await _categoryAndBrandService.Create(model);
            return StatusCode(result.code, result.Value);
        }

        [HttpPut("{ID}")]
        public async Task<IActionResult> UpdateCategoryAndBrand(int ID, [FromBody] UpdateCategoryAndBrand updateCategoryAndBrand)
        {
            var model = _mapper.Map<CategoryAndBrandModel>(updateCategoryAndBrand);
            model.ID = ID;
            var result = await _categoryAndBrandService.Update(ID, model);
            return StatusCode(result.code, result.Value);
        }

        [HttpDelete("{ID}")]
        public async Task<IActionResult> DeleteGenderByID(int ID)
        {
            var result = await _categoryAndBrandService.Delete(ID);
            return StatusCode(result.code, result.Value);
        }
    }
}
