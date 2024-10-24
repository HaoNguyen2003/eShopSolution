using AutoMapper;
using eShopSolution.BusinessLayer.Abstract;
using eShopSolution.cloudinaryManagerFile.Abstract;
using eShopSolution.CrawlData.Model;
using eShopSolution.CrawlData.Service;
using eShopSolution.DtoLayer.AddModel;
using eShopSolution.DtoLayer.Model;
using eShopSolution.DtoLayer.RepositoryModel;
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
        private readonly IServiceProvider _serviceProvider;
        private readonly ReadFileJson _readFileJson;

        public ProductController(IMapper mapper, IProductService productService,
            ILogger<ProductController> logger, ICloudinaryService cloudinaryService,
            IServiceProvider serviceProvider, ReadFileJson readFileJson)
        {
            _mapper = mapper;
            _productService = productService;
            _logger = logger;
            _cloudinaryService = cloudinaryService;
            _serviceProvider = serviceProvider;
            _readFileJson = readFileJson;
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
                if (collectionModel.ListImage.Any(fileModel => !WorkWithFile.IsImage(fileModel)))
                {
                    return StatusCode(500, "There exists a file that is not an image");
                }
                var model = _mapper.Map<CollectionModel>(collectionModel);
                var imageUploadTasks = collectionModel.ListImage.Select(async fileModel =>
                {
                    var resultImage = await _cloudinaryService.UploadFile(fileModel, "ImageEshop/Product");
                    if (resultImage.IsSuccess)
                    {
                        model.ListImageURL.Add(new CloudinaryImageModel
                        {
                            ImageURL = resultImage.Url,
                            PublicID = resultImage.PublicID
                        });
                    }
                    return resultImage.IsSuccess;
                });
                var uploadResults = await Task.WhenAll(imageUploadTasks);
                if (uploadResults.Any(success => !success))
                {
                    return StatusCode(500, "Some images failed to upload.");
                }
                collectionModels.Add(model);
            }
            using (var scope = _serviceProvider.CreateScope())
            {
                var scopedProductService = scope.ServiceProvider.GetRequiredService<IProductService>();
                var result = await scopedProductService.CreateProduct(productModel, null, collectionModels);
                if (result.code != 200)
                {
                    var publicIdsToRemove = collectionModels.SelectMany(item => item.ListImageURL)
                                            .Select(img => img.PublicID)
                                            .ToList();
                    var deleteTasks = publicIdsToRemove.Select(async p =>
                    {
                        await _cloudinaryService.RemoveFileAsync(p);
                    });
                    await Task.WhenAll(deleteTasks);
                    return StatusCode(result.code, result.Value);
                }
                return StatusCode(result.code, result.Value);
            }
        }

        [HttpPost("GetDataFromFileJson")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> GetDataFromFile(IFormFile file)
        {
            var ListProduct = _readFileJson.FucntionReadFileJson(file);
            var productTasks = ListProduct.Select(async product =>
            {
                var productModel = _mapper.Map<ProductModel>(product.ProductInfo);
                var collectionModels = new List<ProductDataNew>();
                var ListCollection = product.ProductwayData;
                var collectionUploadTasks = ListCollection.Select(async collectionModel =>
                {
                    var model = new ProductDataNew()
                    {
                        Color = collectionModel.Color,
                        DetailQuantity = collectionModel.DetailQuantity,
                        ListImageURL = new List<CloudinaryImageModel>()
                    };
                    var imageUploadTasks = collectionModel.Imgs.Select(async fileModel =>
                    {
                        var resultImage = await _cloudinaryService.UploadFile(fileModel, "ImageEshop/Product");
                        if (resultImage.IsSuccess)
                        {
                            return new CloudinaryImageModel
                            {
                                ImageURL = resultImage.Url,
                                PublicID = resultImage.PublicID
                            };
                        }
                        return null;
                    });

                    var uploadedImages = await Task.WhenAll(imageUploadTasks);
                    model.ListImageURL = uploadedImages.Where(img => img != null).ToList();
                    return model;
                });
                var uploadedCollectionModels = await Task.WhenAll(collectionUploadTasks);
                collectionModels.AddRange(uploadedCollectionModels);
                using (var scope = _serviceProvider.CreateScope())
                {
                    var scopedProductService = scope.ServiceProvider.GetRequiredService<IProductService>();
                    var result = await scopedProductService.CreateProduct(productModel, collectionModels, null);
                    return (result, collectionModels.SelectMany(c => c.ListImageURL).ToList());
                }
            });

            var results = await Task.WhenAll(productTasks);
            var failedResults = results.Where(r => r.result.code != 200);
            var tasksDelete = failedResults.SelectMany(failedResult =>
                failedResult.Item2.Select(async uploadedImage =>
                    await _cloudinaryService.RemoveFileAsync(uploadedImage.PublicID))
            );
            await Task.WhenAll(tasksDelete);
            return Ok(new { Message = "Products processed", Results = results });
        }
        [HttpGet]
        public async Task<IActionResult> GetAllProduct()
        {
            var result = await _productService.GetAllProduct();
            return StatusCode(result.code, result.Value);
        }

        [HttpGet("GetDetailProductByProductIDAndProductColorID/{ID}/{ProductColorID}")]
        public async Task<IActionResult> GetDetailProductByProductIDAndColorID(int ID, int ProductColorID)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var DetailProduct = await _productService.GetDetailProductByProductIDAndProductColorID(ID, ProductColorID);
            return StatusCode(DetailProduct.code, DetailProduct.Value);
        }

        [HttpGet("GetProductInDashBoardByProductIDAndProductColorID/{ProductID}/{ProductColorID}")]
        //[Authorize]
        public async Task<IActionResult> GetProductInDashBoardByProductIDAndColorID(int ProductID, int ProductColorID)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var ProductDashBoard = await _productService.GetProductInDashBoardByProductIDAndProductColorID(ProductID, ProductColorID);
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
                _cloudinaryService.RemoveFileAsync(item);
            }

            return StatusCode(result.code, "Delete Success");
        }
    }
}
