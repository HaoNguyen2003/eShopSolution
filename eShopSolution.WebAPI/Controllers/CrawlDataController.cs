using AutoMapper;
using eShopSolution.BusinessLayer.Abstract;
using eShopSolution.BusinessLayer.Service;
using eShopSolution.cloudinaryManagerFile.Abstract;
using eShopSolution.CrawlData.Model;
using eShopSolution.CrawlData.Service;
using eShopSolution.DtoLayer.AddModel;
using eShopSolution.DtoLayer.Model;
using eShopSolution.WebAPI.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eShopSolution.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CrawlDataController : ControllerBase
    {
        private readonly ReadFileJson _readFileJson;
        private readonly IMapper _mapper;
        private readonly ICloudinaryService _cloudinaryService;
        private readonly IProductService _productService;

        public CrawlDataController(ReadFileJson readFileJson,IMapper mapper,ICloudinaryService cloudinaryService,IProductService productService) {
            _readFileJson = readFileJson;
            _mapper = mapper;
            _cloudinaryService = cloudinaryService;
            _productService = productService;
        }
        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> GetDataFromFile(IFormFile file)
        {
            var ListProduct  = _readFileJson.FucntionReadFileJson(file);
            foreach (var product in ListProduct)
            {
                var productModel = _mapper.Map<ProductModel>(product.ProductInfo);
                List<ProductDataNew> collectionModels = new List<ProductDataNew>();
                var ListCollection = product.ProductwayData;
                foreach (var collectionModel in ListCollection)
                {
                    var model = new ProductDataNew() { Color=collectionModel.Color,DetailQuantity=collectionModel.DetailQuantity};
                    foreach (var fileModel in collectionModel.Imgs)
                    {
                        var resultImage = await _cloudinaryService.UploadFile(fileModel, "ImageEshop/Test");
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
                
            }
            return Ok();
        }

    }
}
