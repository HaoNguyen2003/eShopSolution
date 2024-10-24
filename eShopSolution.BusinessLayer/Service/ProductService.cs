using eShopSolution.BusinessLayer.Abstract;
using eShopSolution.CrawlData.Model;
using eShopSolution.DataLayer.Abstract;
using eShopSolution.DtoLayer.AddModel;
using eShopSolution.DtoLayer.Model;
using eShopSolution.DtoLayer.RepositoryModel;
using eShopSolution.DtoLayer.RequestModel;
using eShopSolution.EntityLayer.Data;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using System.Drawing;

namespace eShopSolution.BusinessLayer.Service
{
    public class ProductService : IProductService
    {
        private readonly IProductDal _productDal;
        private readonly IProductColorDal _productColorDal;
        private readonly IProductImageDal _productImageDal;
        private readonly IProductSizeInventoryDal _productSizeInventoryDal;
        private readonly IColorDal _colorDal;
        private readonly ISizeDal _sizeDal;
        private readonly ICustomCache<string> _customCache;
        private readonly IColorCombinationService _colorCombinationService;
        private readonly IColorCombinationColorService _colorCombinationColorService;
        private readonly IServiceProvider _serviceProvider;

        public ProductService(IProductDal productDal, IProductColorDal productColorDal, IProductImageDal productImageDal,
            IProductSizeInventoryDal productSizeInventoryDal, IColorDal colorDal,
            ISizeDal sizeDal, ICustomCache<string> customCache,IColorCombinationService colorCombinationService,
            IColorCombinationColorService colorCombinationColorService, IServiceProvider serviceProvider)
        {
            _productDal = productDal;
            _productColorDal = productColorDal;
            _productImageDal = productImageDal;
            _productSizeInventoryDal = productSizeInventoryDal;
            _colorDal = colorDal;
            _sizeDal = sizeDal;
            _customCache = customCache;
            _colorCombinationService = colorCombinationService;
            _colorCombinationColorService = colorCombinationColorService;
            _serviceProvider = serviceProvider;
        }
        //----------------------------------------------------------------------------------//
        public async Task<BaseRep<string>> Create(ProductModel model)
        {
            return await _productDal.Create(model);
        }
        public async Task<BaseRep<string>> Delete(int ID)
        {
            return await _productDal.Delete(ID);
        }
        public async Task<BaseRep<ProductModel>> GetByID(int ID)
        {
            return await _productDal.GetByID(ID);
        }
        public async Task<BaseRep<string>> Update(int ID, ProductModel model)
        {
            var result= await _productDal.Update(ID, model);
            if(result.code!=200)
                return result;
            _customCache.Clear();
            var listColorID = await _productColorDal.GetColorIDByProductID(ID);
            return await _productDal.Update(ID, model);
        }
        public async Task<BaseRep<List<ProductModel>>> GetAll()
        {
            return await _productDal.GetAll();
        }
        //----------------------------------------------------------------------------------//
        public async Task<BaseRep<List<ProductCardModel>>> GetAllProduct()
        {
            var productCardModel = await _productDal.GetAllProductConvertToProductCardModel();
            foreach (var item in productCardModel)
            {
                var listProductColorIDs = await _productColorDal.GetProductColorIDByProductID(item.ID);
                foreach (int ProductColor in listProductColorIDs)
                {
                    ColorItemModel colorItemModel = new ColorItemModel();
                    colorItemModel.ProductColorID = ProductColor;
                    colorItemModel.ImageURL = await _productImageDal.GetImagefirstByID(ProductColor);
                    item.ColorItemModel.Add(colorItemModel);
                }
            }
            return new BaseRep<List<ProductCardModel>>() { code = 200, Value = productCardModel };
        }
        #region Create Cũ
        public async Task<BaseRep<string>> CreateProduct(ProductModel productModel, List<CollectionModel> collectionModels)
        {
            int ProductID = await _productDal.CreateProduct(productModel);
            if (ProductID == -1)
            {
                return new BaseRep<string>() { code = 500, Value = "Create Fail" };
            }
            try
            {
                foreach (var item in collectionModels)
                {
                    ProductColorModel productColor = new ProductColorModel();
                    productColor.ProductID = ProductID;

                    int ColorCombinationID = await _colorCombinationService.GetColorCombinationIdIfExists(item.ColorIDs);
                    if(ColorCombinationID == -1)
                    {
                        ColorCombinationID = await _colorCombinationService.CreateModel(new ColorCombinationModel() { ID = 0, Name = "" });
                        foreach(var colorID in item.ColorIDs)
                        {
                            await _colorCombinationColorService.Create(new ColorCombinationColorModel() 
                            { ID = 0, ColorCombinationID = ColorCombinationID, ColorID = colorID });
                        }
                        productColor.ColorCombinationID = ColorCombinationID;
                    }
                    else
                    {
                        productColor.ColorCombinationID = ColorCombinationID;
                    }
                    int ProductColorID = await _productColorDal.CreateProductColorReturnID(productColor);
                    foreach (var image in item.ListImageURL)
                    {
                        ProductImageModel productImageModel = new ProductImageModel();
                        productImageModel.ProductColorID = ProductColorID;
                        productImageModel.ImageURL = image.ImageURL;
                        productImageModel.PublicID = image.PublicID;
                        var check = await _productImageDal.Create(productImageModel);
                        if (check.code != 200)
                        {
                            await _productDal.Delete(ProductID);
                            return new BaseRep<string>() { code = 500, Value = check.Value };
                        }
                    }
                    foreach (var detailsizeandquantity in item.DetailQuantity)
                    {
                        DetailQuantityProductModel detailQuantityProductModel = new DetailQuantityProductModel()
                        {
                            ID = 0,
                            ProductColorID = ProductColorID,
                            SizeID = detailsizeandquantity.SizeID,
                            Quantity = detailsizeandquantity.Quantity
                        };
                        var check = await _productSizeInventoryDal.CreateProductSizeInventory(detailQuantityProductModel);
                        if (check.code != 200)
                        {
                            await _productDal.Delete(ProductID);
                            return new BaseRep<string>() { code = 500, Value = check.Value };
                        }
                    }
                }
                _customCache.Clear();
                return new BaseRep<string>() { code = 200, Value = "Create Success" };
            }
            catch (Exception ex)
            {
                await _productDal.Delete(ProductID);
                return new BaseRep<string>() { code = 500, Value = "Create Fail: " + ex.Message };
            }
        }
        public async Task<BaseRep<string>> CreateProduct(ProductModel productModel, List<ProductDataNew> collectionModels)
        {
            int ProductID = await _productDal.CreateProduct(productModel);
            if (ProductID == -1)
            {
                return new BaseRep<string>() { code = 500, Value = "Create Fail" };
            }
            try
            {
                foreach (var item in collectionModels)
                {
                    ProductColorModel productColor = new ProductColorModel();
                    productColor.ProductID = ProductID;
                    var ListColorID = await _colorDal.GetIntColorByName(item.Color);
                    int ColorCombinationID = await _colorCombinationService.GetColorCombinationIdIfExists(ListColorID);
                    if (ColorCombinationID == -1)
                    {
                        ColorCombinationID = await _colorCombinationService.CreateModel(new ColorCombinationModel() { ID = 0, Name = "" });
                        foreach (var colorID in ListColorID)
                        {
                            await _colorCombinationColorService.Create(new ColorCombinationColorModel()
                            { ID = 0, ColorCombinationID = ColorCombinationID, ColorID = colorID });
                        }
                        productColor.ColorCombinationID = ColorCombinationID;
                    }
                    else
                    {
                        productColor.ColorCombinationID = ColorCombinationID;
                    }
                    int ProductColorID = await _productColorDal.CreateProductColorReturnID(productColor);
                    foreach (var image in item.ListImageURL)
                    {
                        ProductImageModel productImageModel = new ProductImageModel();
                        productImageModel.ProductColorID = ProductColorID;
                        productImageModel.ImageURL = image.ImageURL;
                        productImageModel.PublicID = image.PublicID;
                        var check = await _productImageDal.Create(productImageModel);
                        if (check.code != 200)
                        {
                            await _productDal.Delete(ProductID);
                            return new BaseRep<string>() { code = 500, Value = check.Value };
                        }
                    }
                    foreach (var detailsizeandquantity in item.DetailQuantity)
                    {
                        DetailQuantityProductModel detailQuantityProductModel = new DetailQuantityProductModel()
                        {
                            ID = 0,
                            ProductColorID = ProductColorID,
                            SizeID = detailsizeandquantity.SizeID,
                            Quantity = detailsizeandquantity.Quantity
                        };
                        var check = await _productSizeInventoryDal.CreateProductSizeInventory(detailQuantityProductModel);
                        if (check.code != 200)
                        {
                            await _productDal.Delete(ProductID);
                            return new BaseRep<string>() { code = 500, Value = check.Value };
                        }
                    }
                }
                _customCache.Clear();
                return new BaseRep<string>() { code = 200, Value = "Create Success" };
            }
            catch (Exception ex)
            {
                await _productDal.Delete(ProductID);
                return new BaseRep<string>() { code = 500, Value = "Create Fail: " + ex.Message };
            }
        }
        #endregion
        
        public async Task<BaseRep<DetailProduct>> GetDetailProductByProductIDAndProductColorID(int ID, int ProductColorID)
        {

            var detailProduct = _customCache.Get<DetailProduct>(_customCache.GenerateCacheKeyProduct(new DetailProductReq() {ProductID=ID,ProductColorID = ProductColorID,Where = "DetailProduct" }));
            if(detailProduct == null)
            {
                detailProduct = await _productDal.GetDetailProductByID(ID);
                if (detailProduct == null)
                {
                    
                    return new BaseRep<DetailProduct>() { code = 404, Value = new DetailProduct() };
                }
                DetailColorAndProduct detailColorAndProduct = new DetailColorAndProduct();
                detailColorAndProduct.ProductColorID = ProductColorID;
                detailColorAndProduct.ListImageURL = await _productImageDal.GetAllImageByProductColor(ProductColorID);
                detailColorAndProduct.DetailQuantity=await GetDetailQuantityByProductColorID(ProductColorID);
                var resultTuple = await GetColorIDByProductColorID(ProductColorID);
                detailColorAndProduct.MixColor = resultTuple.Item1;
                detailProduct.ColorItemModel = await GetColorItemModelsByProductID(ID);
                detailProduct.collectionModel = detailColorAndProduct;
                _customCache.Set(key: _customCache.GenerateCacheKeyProduct(new DetailProductReq() { ProductID = ID, ProductColorID = ProductColorID, Where = "DetailProduct" }), value: detailProduct, priority: CacheItemPriority.High);
            }
            return new BaseRep<DetailProduct>() { code = 200, Value = detailProduct };
        }
        public async Task<BaseRep<ProductDashBoard>> GetProductInDashBoardByProductIDAndProductColorID(int ProductID, int ProductColorID)
        {
            var ProductDashBoard = _customCache.Get<ProductDashBoard>(_customCache.GenerateCacheKeyProduct(new DetailProductReq() { ProductID = ProductID, ProductColorID = ProductColorID, Where = "DashBoardProduct" }));
            if (ProductDashBoard == null)
            {
                ProductDashBoard = await _productDal.GetProductDashBoardByID(ProductID);
                if (ProductDashBoard == null)
                {
                    return new BaseRep<ProductDashBoard>() { code = 404, Value = new ProductDashBoard() };
                }
                CollectionProductDashBoard collectionProductDashBoard = new CollectionProductDashBoard();
                collectionProductDashBoard.ProductColorID = ProductColorID;
                collectionProductDashBoard.productImageModels = await _productImageDal.GetAllProductImageByProductColor(ProductColorID);
                collectionProductDashBoard.SizeAndQuantity = await GetDetailQuantityByProductColorID(ProductColorID);
                ProductDashBoard.colorItemModels = await GetColorItemModelsByProductID(ProductID);
                var resultTuple = await GetColorIDByProductColorID(ProductColorID);
                collectionProductDashBoard.Colors = resultTuple.Item2;
                ProductDashBoard.CollectionProductDashBoard = collectionProductDashBoard;
                _customCache.Set(key: _customCache.GenerateCacheKeyProduct(new DetailProductReq() { ProductID = ProductID, ProductColorID = ProductColorID, Where = "DashBoardProduct" }), value: ProductDashBoard, priority: CacheItemPriority.High);
            }
            return new BaseRep<ProductDashBoard>() { code = 200, Value = ProductDashBoard };
        }
        public async Task<BaseRep<List<string>>> DeleteProduct(int ID)
        {

            var product = await _productDal.GetByID(ID);
            if (product.code == 404)
            {
                return new BaseRep<List<string>>() { code = 404, Value = new List<string> { } };
            }
            List<string> ListImg = new List<string>();
            var ListProductColorID = await _productColorDal.GetProductColorIDByProductID(ID);
            foreach (var ProductColor in ListProductColorID)
            {
                var listImgOfProductColorID = await _productImageDal.GetAllPublicIDByProductColor(ProductColor);
                ListImg.AddRange(listImgOfProductColorID);
            }
            var resultDeleteProduct = await _productDal.Delete(ID);
            if (resultDeleteProduct.code == 200)
            {
                _customCache.Clear();
                return new BaseRep<List<string>>() { code = 200, Value = ListImg };
            }
            else
            {
                return new BaseRep<List<string>>() { code = 500, Value = new List<string> { } };
            }
        }
        public async Task<BaseRep<PagedResult>> GetProductByFilterAndPage(FilterModel model, int Page, int Limit)
        {
            var pagedResult = _customCache.Get<PagedResult>(_customCache.GenerateCacheKey(model, Page, Limit));
            if (pagedResult == null)
            {
                var Tuple = await _productDal.GetProductByFillterAndPage(model, Page, Limit);
                var Products = Tuple.Item2;
                var TotalItem = Tuple.Item1;
                foreach (var item in Products)
                {
                    var listColorID = await _productColorDal.GetColorIDByProductID(item.ID);
                    item.ColorItemModel = await GetColorItemModelsByProductID(item.ID,model.ListColorID);
                }
                pagedResult = new PagedResult();
                pagedResult.Items = Products;
                pagedResult.TotalItem = Products.Count;
                pagedResult.Page = Page;
                pagedResult.TotalPage = (int)Math.Ceiling((double)TotalItem / Limit);
                pagedResult.Size = Limit;
                pagedResult.First = pagedResult.Page > 1 && pagedResult.Page <= pagedResult.TotalPage;
                pagedResult.Last = pagedResult.Page < pagedResult.TotalPage;
                _customCache.Set(key: _customCache.GenerateCacheKey(model, Page, Limit),value: pagedResult, priority: CacheItemPriority.Normal);
            }
            return new BaseRep<PagedResult>() { code = 200, Value = pagedResult };
        }
       
        private async Task<List<DetailQuantityProductDisplay>> GetDetailQuantityByProductColorID(int ProductColorID)
        {
            var detailQuantityProductModels = await _productSizeInventoryDal.GetAllDetailQuantityProducByProductColorID(ProductColorID);
            var detailQuantities = new List<DetailQuantityProductDisplay>();

            foreach (var itemDetail in detailQuantityProductModels)
            {
                var detailQuantityProductDisplay = new DetailQuantityProductDisplay
                {
                    SizeID = itemDetail.SizeID,
                    Quantity = itemDetail.Quantity,
                    SizeName = (await _sizeDal.GetByID(itemDetail.SizeID)).Value.SizeName ?? "Unknown Size"
                };
                detailQuantities.Add(detailQuantityProductDisplay);
            }

            return detailQuantities;
        }

        private async Task<List<ColorItemModel>> GetColorItemModelsByProductID(int ProductID, List<int>? ColorSearch = null)
        {
            var listProductColorIDs = await _productColorDal.GetProductColorIDByProductID(ProductID);
            var Listtuple = new List<Tuple<List<int>, ColorItemModel>>();
            foreach (var productColorID in listProductColorIDs)
            {
                var colorItemModel = new ColorItemModel
                {
                    ProductColorID = productColorID,
                    ImageURL = await _productImageDal.GetImagefirstByID(productColorID)
                };
                var colorIDs = await _productColorDal.GetColorIDByProductColorID(productColorID);
                Listtuple.Add(new Tuple<List<int>, ColorItemModel>(colorIDs, colorItemModel));
            }
            if (ColorSearch != null && ColorSearch.Count > 0)
            {
                Listtuple = Listtuple
                   .OrderByDescending(tuple =>
                   {
                       var firstColorId = tuple.Item1.FirstOrDefault(id => ColorSearch.Contains(id));
                       return firstColorId != null ? ColorSearch.IndexOf(firstColorId) : -1;
                   }).ToList();
            }
            var sortedColorItemModels = Listtuple.Select(tuple => tuple.Item2).ToList();
            return sortedColorItemModels;
        }        

        private async Task<Tuple<string, List<ColorModel>>> GetColorIDByProductColorID(int ProductColorID)
        {
           List<ColorModel> colorModels = new List<ColorModel>();
           string MixColor = "";
           var listColorID = await _productColorDal.GetColorIDByProductColorID(ProductColorID);
            foreach (var colorID in listColorID)
            {
                var Color = await _colorDal.GetByID(colorID);
                MixColor += Color.Value.Name + "/";
                colorModels.Add(Color.Value);
            }
            if (MixColor.Length > 0)
            {
                MixColor = MixColor.TrimEnd('/', ' ');
            }
            return Tuple.Create(MixColor, colorModels);
        }

        private async Task<int> CreateProductColor(int productID, List<int> colorIDs)
        {
            int colorCombinationID = await _colorCombinationService.GetColorCombinationIdIfExists(colorIDs);

            if (colorCombinationID == -1)
            {
                colorCombinationID = await _colorCombinationService.CreateModel(new ColorCombinationModel() { ID = 0, Name = "" });
                var tasks = colorIDs.Select(async colorID =>
                {
                    using (var scope = _serviceProvider.CreateScope())
                    {
                        var colorCombinationColorService = scope.ServiceProvider.GetRequiredService<IColorCombinationColorService>();

                        var result = await colorCombinationColorService.Create(new ColorCombinationColorModel
                        {
                            ID = 0,
                            ColorCombinationID = colorCombinationID,
                            ColorID = colorID
                        });

                        if (result.code != 200)
                        {
                            Console.WriteLine($"Error creating color combination color for ColorID {colorID}: {result.Value}");
                        }
                    }
                }).ToList();
                await Task.WhenAll(tasks); 
            }

            ProductColorModel productColor = new ProductColorModel
            {
                ProductID = productID,
                ColorCombinationID = colorCombinationID
            };

            return await _productColorDal.CreateProductColorReturnID(productColor);
        }

        private async Task<BaseRep<string>> AddProductImages(int productColorID, List<CloudinaryImageModel> listImageURL)
        {
            var tasks = listImageURL.Select(async image =>
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var productImageDal = scope.ServiceProvider.GetRequiredService<IProductImageDal>();

                    var productImage = new ProductImageModel
                    {
                        ProductColorID = productColorID,
                        ImageURL = image.ImageURL,
                        PublicID = image.PublicID
                    };
                    var r = await productImageDal.Create(productImage);
                    if (r.code != 200)
                    {
                        Console.WriteLine($"Error adding product image: {r.Value}");
                    }
                    return r;
                }
            });
            var results = await Task.WhenAll(tasks);
            return results.FirstOrDefault(r => r.code != 200) ?? new BaseRep<string>() { code = 200 };
        }
        private async Task<BaseRep<string>> AddProductSizeAndQuantity(int productColorID, List<AddDetailQuantityProduct> detailQuantities)
        {
            var tasks = detailQuantities.Select(async details =>
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var productSizeInventoryDal = scope.ServiceProvider.GetRequiredService<IProductSizeInventoryDal>();

                    var detailModel = new DetailQuantityProductModel
                    {
                        ID = 0,
                        ProductColorID = productColorID,
                        SizeID = details.SizeID,
                        Quantity = details.Quantity
                    };
                    var result = await productSizeInventoryDal.CreateProductSizeInventory(detailModel);
                    if (result.code != 200)
                    {
                        Console.WriteLine($"Error adding product size and quantity for SizeID {details.SizeID}: {result.Value}");
                    }
                    return result;
                }
            });
            var results = await Task.WhenAll(tasks);
            return results.FirstOrDefault(r => r.code != 200) ?? new BaseRep<string>() { code = 200 };
        }
        private async Task<BaseRep<string>> ProcessProductCollection(int ProductID, List<int>? ListColorID = null, List<CloudinaryImageModel>? ListImageURL = null, List<AddDetailQuantityProduct>? DetailQuantity = null)
        {
            int productColorID = await CreateProductColor(ProductID, ListColorID);
            if (productColorID == -1)
            {
                return new BaseRep<string>() { code = 500, Value = "Create Product Color Fail" };
            }

            var imageCheck = await AddProductImages(productColorID, ListImageURL);
            if (imageCheck.code != 200)
            {
                return new BaseRep<string>() { code = 500, Value = imageCheck.Value };
            }

            var sizeCheck = await AddProductSizeAndQuantity(productColorID, DetailQuantity);
            if (sizeCheck.code != 200)
            {
                return new BaseRep<string>() { code = 500, Value = sizeCheck.Value };
            }
            return new BaseRep<string>() { code = 200 };
        }
        
        public async Task<BaseRep<string>> CreateProduct(ProductModel productModel, List<ProductDataNew>? newCollectionModels = null, List<CollectionModel>? oldCollectionModels = null)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var scopedProductDal = scope.ServiceProvider.GetRequiredService<IProductDal>();
                var scopedColorDal = scope.ServiceProvider.GetRequiredService<IColorDal>();

                int ProductID = await scopedProductDal.CreateProduct(productModel);
                if (ProductID == -1)
                {
                    return new BaseRep<string>() { code = 500, Value = "Create Fail" };
                }

                try
                {
                    if (oldCollectionModels != null)
                    {
                        foreach (var item in oldCollectionModels)
                        {
                            using (var innerScope = _serviceProvider.CreateScope())
                            {
                                var innerScopedProductDal = innerScope.ServiceProvider.GetRequiredService<IProductDal>();
                                var result = await ProcessProductCollection(ProductID, item.ColorIDs, item.ListImageURL, item.DetailQuantity);
                                if (result.code != 200)
                                {
                                    await innerScopedProductDal.Delete(ProductID);
                                    return result;
                                }
                            }
                        }
                    }
                    if (newCollectionModels != null)
                    {
                        foreach (var item in newCollectionModels)
                        {
                            using (var innerScope = _serviceProvider.CreateScope())
                            {
                                var innerScopedColorDal = innerScope.ServiceProvider.GetRequiredService<IColorDal>();
                                var ListColorID = await innerScopedColorDal.GetIntColorByName(item.Color);
                                var result = await ProcessProductCollection(ProductID, ListColorID, item.ListImageURL, item.DetailQuantity);
                                if (result.code != 200)
                                {
                                    await scopedProductDal.Delete(ProductID);
                                    return result;
                                }
                            }
                        }
                    }
                    _customCache.Clear();
                    return new BaseRep<string>() { code = 200, Value = "Create Success" };
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Create Fail: " + ex.Message);
                    await scopedProductDal.Delete(ProductID);
                    return new BaseRep<string>() { code = 500, Value = "Create Fail: " + ex.Message };
                }
            }
        }
    }
}
