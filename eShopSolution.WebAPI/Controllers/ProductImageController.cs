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
    public class ProductImageController : ControllerBase
    {
        private readonly IProductImageService _productImageService;
        private readonly IMapper _mapper;
        private readonly ICloudinaryService _cloudinaryService;
        public ProductImageController(IProductImageService productImageService, IMapper mapper, ICloudinaryService cloudinaryService)
        {
            _productImageService = productImageService;
            _mapper = mapper;
            _cloudinaryService = cloudinaryService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllProductImage()
        {
            var result = await _productImageService.GetAll();
            return StatusCode(result.code, result.Value);
        }

        [HttpGet("{ID}")]
        public async Task<IActionResult> GetProductImageByID(int ID)
        {
            var result = await _productImageService.GetByID(ID);
            return StatusCode(result.code, result.Value);
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> AddProductImage([FromForm] AddProductImage productImage)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (!WorkWithFile.IsImage(productImage.Image))
                return StatusCode(500, "Please select Image");
            var UploadImageResult = await _cloudinaryService.UploadFile(productImage.Image, "ImageEshop/Product");
            if (!UploadImageResult.IsSuccess)
                return StatusCode(500, UploadImageResult);
            var productImageModel = _mapper.Map<ProductImageModel>(productImage);
            productImageModel.ImageURL = UploadImageResult.Url;
            productImageModel.PublicID = UploadImageResult.PublicID;
            var result = await _productImageService.Create(productImageModel);
            if (result.code != 200)
                _cloudinaryService.RemoveFile(productImageModel.PublicID);
            return StatusCode(result.code, result.Value);
        }

        [HttpPut("{ID}")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UpdateProductImage(int ID, [FromForm] UpdateProductImage updateProductImage)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var tempPublicID = "";
            var check = await _productImageService.GetByID(ID);
            if (check.code == 404)
                return StatusCode(404);
            var productImageModel = _mapper.Map<ProductImageModel>(updateProductImage);
            productImageModel.ID = ID;
            if (updateProductImage.Image == null)
            {
                productImageModel.ImageURL = check.Value.ImageURL;
                productImageModel.PublicID = check.Value.PublicID;
            }
            else
            {
                if (!WorkWithFile.IsImage(updateProductImage.Image))
                    return StatusCode(500, "Please select Image");
                var UploadImageResult = await _cloudinaryService.UploadFile(updateProductImage.Image, "ImageEshop/Product");
                if (!UploadImageResult.IsSuccess)
                    return StatusCode(500, UploadImageResult);
                productImageModel.ImageURL = UploadImageResult.Url;
                productImageModel.PublicID = UploadImageResult.PublicID;
                tempPublicID = UploadImageResult.PublicID;
            }
            var result = await _productImageService.Update(ID, productImageModel);
            if (result.code != 200)
            {
                if (!string.IsNullOrEmpty(tempPublicID))
                    _cloudinaryService.RemoveFile(tempPublicID);
                return StatusCode(500, result);
            }
            if (updateProductImage.Image != null)
                _cloudinaryService.RemoveFile(check.Value.PublicID);
            return StatusCode(200, result);
        }

        [HttpDelete("{ID}")]
        public async Task<IActionResult> DeleteProductImageByID(int ID)
        {
            var check = await _productImageService.GetByID(ID);
            if (check.code == 404)
                return StatusCode(404);
            var result = await _productImageService.Delete(ID);
            if (result.code == 200)
            {
                var resultRemove = _cloudinaryService.RemoveFile(check.Value.PublicID);
                if (!resultRemove.IsSuccess)
                    return StatusCode(500, resultRemove);
            }
            return StatusCode(result.code, result.Value);
        }
    }
}
