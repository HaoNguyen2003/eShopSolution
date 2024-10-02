using AutoMapper;
using eShopSolution.BusinessLayer.Abstract;
using eShopSolution.cloudinaryManagerFile.Abstract;
using eShopSolution.DtoLayer.AddModel;
using eShopSolution.DtoLayer.Model;
using eShopSolution.DtoLayer.UpdateModel;
using eShopSolution.WebAPI.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace eShopSolution.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IProductService _productService;
        private readonly ILogger<ProductController> _logger;
        private readonly ICloudinaryService _cloudinaryService;
        public ProductController(IMapper mapper, IProductService productService, ILogger<ProductController> logger, ICloudinaryService cloudinaryService)
        {
            _mapper = mapper;
            _productService = productService;
            _logger = logger;
            _cloudinaryService = cloudinaryService;
        }

        [HttpPost("AddProduct")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> AddProduct([FromForm] AddProductSizeInventory addProduct)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var productModel = _mapper.Map<ProductModel>(addProduct);
            var ListCollection = addProduct.addCollections;
            List<CollectionModel> collectionModels = new List<CollectionModel>();
            foreach (var collectionModel in ListCollection)
            {
                foreach (var fileModel in collectionModel.ListImage)
                {
                    if (!WorkWithFile.IsImage(fileModel))
                    {
                        return StatusCode(500, "There exists a file that is not an image");
                    }
                }
            }
            foreach (var collectionModel in ListCollection)
            {
                var model = _mapper.Map<CollectionModel>(collectionModel);
                foreach (var fileModel in collectionModel.ListImage)
                {
                    var resultImage = await _cloudinaryService.UploadFile(fileModel, "ImageEshop/Product");
                    if (resultImage.IsSuccess)
                        model.ListImageURL.Add(new CloudinaryImageModel() { ImageURL = resultImage.Url, PublicID = resultImage.PublicID });
                }
                collectionModels.Add(model);
            }
            var result = await _productService.CreateProduct(productModel, collectionModels);
            if (result.code != 200)
            {
                foreach (var item in collectionModels)
                {
                    foreach (var listURL in item.ListImageURL)
                    {
                        _cloudinaryService.RemoveFile(listURL.PublicID);
                    }
                }
            }
            return StatusCode(result.code, result.Value);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllProduct()
        {
            var result = await _productService.GetAllProduct();
            return StatusCode(result.code, result.Value);
        }

        [HttpGet("GetDetailProductByProductIDAndColorID/{ID}/{ColorID}")]
        public async Task<IActionResult> GetDetailProductByProductIDAndColorID(int ID, int ColorID)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var DetailProduct = await _productService.GetDetailProductByProductIDAndColorID(ID, ColorID);
            return StatusCode(DetailProduct.code, DetailProduct.Value);
        }

        [HttpGet("GetProductInDashBoardByProductIDAndColorID/{ProductID}/{ColorID}")]
        [Authorize(Policy = Permission.Permissions.Product.ViewProductDashBoard)]
        public async Task<IActionResult> GetProductInDashBoardByProductIDAndColorID(int ProductID, int ColorID)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var ProductDashBoard = await _productService.GetProductInDashBoardByProductIDAndColorID(ProductID, ColorID);
            _logger.LogInformation($"ProductDashBoard: {JsonConvert.SerializeObject(ProductDashBoard.Value)}");
            return StatusCode(ProductDashBoard.code, ProductDashBoard.Value);
        }

        [HttpGet("GetProductFilterAndPage/{Page}/{Limit}")]
        public async Task<IActionResult> GetProductFillterAndPage([FromForm] FilterModel filterModel, int Page, int Limit)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var result = await _productService.GetProductByFilterAndPage(filterModel, Page, Limit);
            return StatusCode(result.code, result.Value);
        }

        [HttpPut("{ID}")]
        public async Task<IActionResult> UpdateProduct(int ID, UpdateProduct updateProduct)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var model = _mapper.Map<ProductModel>(updateProduct);
            model.ID = ID;
            var result = await _productService.Update(ID, model);
            return StatusCode(result.code, result.Value);
        }

        [HttpDelete("{ID}")]
        public async Task<IActionResult> DeleteProduct(int ID)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _productService.DeleteProduct(ID);
            if (result.code == 404 || result.code == 500)
            {
                return StatusCode(result.code, "Delete Fail");
            }
            foreach (var item in result.Value)
            {
                _cloudinaryService.RemoveFile(item);
            }

            return StatusCode(result.code, "Delete Success");
        }
    }
}
