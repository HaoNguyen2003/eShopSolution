using AutoMapper;
using eShopSolution.BusinessLayer.Abstract;
using eShopSolution.BusinessLayer.Service;
using eShopSolution.cloudinaryManagerFile.Abstract;
using eShopSolution.CrawlData.Model;
using eShopSolution.CrawlData.Service;
using eShopSolution.DtoLayer.AddModel;
using eShopSolution.DtoLayer.Model;
using eShopSolution.DtoLayer.RepositoryModel;
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
        private readonly IServiceProvider _serviceProvider;

        public CrawlDataController(ReadFileJson readFileJson,IMapper mapper,ICloudinaryService cloudinaryService,IProductService productService,IServiceProvider serviceProvider) {
            _readFileJson = readFileJson;
            _mapper = mapper;
            _cloudinaryService = cloudinaryService;
            _productService = productService;
            _serviceProvider= serviceProvider;
        }
        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> GetDataFromFile(IFormFile file)
        {
            
            var ListProduct = _readFileJson.FucntionReadFileJson(file);
            var tasks = new List<Task<(BaseRep<string> result, List<CloudinaryImageModel> uploadedImages)>>();

            var productTasks = ListProduct.Select(async product =>
            {
                var productModel = _mapper.Map<ProductModel>(product.ProductInfo);
                List<ProductDataNew> collectionModels = new List<ProductDataNew>();
                var ListCollection = product.ProductwayData;

                foreach (var collectionModel in ListCollection)
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
                            var uploadedImage = new CloudinaryImageModel()
                            {
                                ImageURL = resultImage.Url,
                                PublicID = resultImage.PublicID
                            };
                            model.ListImageURL.Add(uploadedImage);
                            return uploadedImage;
                        }
                        return null;
                    });

                    var uploadedImages = await Task.WhenAll(imageUploadTasks);
                    model.ListImageURL = uploadedImages.Where(img => img != null).ToList();
                    collectionModels.Add(model);
                }

                using (var scope = _serviceProvider.CreateScope())
                {
                    var scopedProductService = scope.ServiceProvider.GetRequiredService<IProductService>();
                    var result = await scopedProductService.CreateProduct(productModel, collectionModels, null);
                    return (result, collectionModels.SelectMany(c => c.ListImageURL).ToList());
                }
            });

            tasks.AddRange(productTasks);
            var results = await Task.WhenAll(tasks);
            foreach (var (result, uploadedImages) in results)
            {
                if (result.code != 200)
                {
                    var tasksDelte = uploadedImages.Select(async uploadedImage => await _cloudinaryService.RemoveFileAsync(uploadedImage.PublicID));
                    await Task.WhenAll(tasksDelte);
                }
            }
            return Ok(new { Message = "Products processed", Results = results });
        }

    }
}
