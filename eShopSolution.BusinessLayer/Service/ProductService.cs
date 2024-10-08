using eShopSolution.BusinessLayer.Abstract;
using eShopSolution.CrawlData.Model;
using eShopSolution.DataLayer.Abstract;
using eShopSolution.DtoLayer.AddModel;
using eShopSolution.DtoLayer.Model;
using eShopSolution.DtoLayer.RepositoryModel;
using eShopSolution.DtoLayer.RequestModel;
using Microsoft.Extensions.Caching.Memory;

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

        public ProductService(IProductDal productDal, IProductColorDal productColorDal, IProductImageDal productImageDal,
            IProductSizeInventoryDal productSizeInventoryDal, IColorDal colorDal,
            ISizeDal sizeDal, ICustomCache<string> customCache)
        {
            _productDal = productDal;
            _productColorDal = productColorDal;
            _productImageDal = productImageDal;
            _productSizeInventoryDal = productSizeInventoryDal;
            _colorDal = colorDal;
            _sizeDal = sizeDal;
            _customCache = customCache;
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
                var listColorID = await _productColorDal.GetColorIDByProductID(item.ID);
                foreach (int colorID in listColorID)
                {
                    int ProductColorID = await _productColorDal.GetProductColorByProductIDAndColorID(item.ID, colorID);
                    ColorItemModel colorItemModel = new ColorItemModel();
                    colorItemModel.ColorID = colorID;
                    colorItemModel.ImageURL = await _productImageDal.GetImagefirstByID(ProductColorID);
                    item.ColorItemModel.Add(colorItemModel);
                }
            }
            return new BaseRep<List<ProductCardModel>>() { code = 200, Value = productCardModel };
        }
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
                    productColor.ColorID = item.ColorID;
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
        public async Task<BaseRep<DetailProduct>> GetDetailProductByProductIDAndColorID(int ID, int ColorID)
        {

            var detailProduct = _customCache.Get<DetailProduct>(_customCache.GenerateCacheKeyProduct(new DetailProductReq() {ProductID=ID,ColorID= ColorID,Where = "DetailProduct" }));
            if(detailProduct == null)
            {
                detailProduct = await _productDal.GetDetailProductByID(ID);
                if (detailProduct == null)
                {
                    
                    return new BaseRep<DetailProduct>() { code = 404, Value = new DetailProduct() };
                }
                DetailColorAndProduct detailColorAndProduct = new DetailColorAndProduct();
                detailColorAndProduct.ColorID = ColorID;
                int ProductColorID = await _productColorDal.GetProductColorByProductIDAndColorID(ID, ColorID);
                detailColorAndProduct.ListImageURL = await _productImageDal.GetAllImageByProductColor(ProductColorID);
                var detailQuantityProductModels = await _productSizeInventoryDal.GetAllDetailQuantityProducByProductColorID(ProductColorID);
                foreach (var itemDetail in detailQuantityProductModels)
                {
                    var detailQuantityProductDisplay = new DetailQuantityProductDisplay
                    {
                        SizeID = itemDetail.SizeID,
                        Quantity = itemDetail.Quantity,
                        SizeName = (await _sizeDal.GetByID(itemDetail.SizeID)).Value.SizeName ?? "Unknown Size"
                    };
                    detailColorAndProduct.DetailQuantity.Add(detailQuantityProductDisplay);
                }
                var listColorID = await _productColorDal.GetColorIDByProductID(ID);
                foreach (var colorID in listColorID)
                {
                    ColorItemModel colorItemModel = new ColorItemModel();
                    colorItemModel.ColorID = colorID;
                    colorItemModel.ImageURL = await _productImageDal.GetImagefirstByID((await _productColorDal.GetProductColorByProductIDAndColorID(ID, colorID)));
                    detailProduct.ColorItemModel.Add(colorItemModel);
                }
                detailProduct.collectionModel = detailColorAndProduct;
                _customCache.Set(key: _customCache.GenerateCacheKeyProduct(new DetailProductReq() { ProductID = ID, ColorID = ColorID, Where = "DetailProduct" }), value: detailProduct, priority: CacheItemPriority.High);
            }
            return new BaseRep<DetailProduct>() { code = 200, Value = detailProduct };
        }
        public async Task<BaseRep<ProductDashBoard>> GetProductInDashBoardByProductIDAndColorID(int ProductID, int ColorID)
        {
            var ProductDashBoard = _customCache.Get<ProductDashBoard>(_customCache.GenerateCacheKeyProduct(new DetailProductReq() { ProductID = ProductID, ColorID = ColorID, Where = "DashBoardProduct" }));
            if (ProductDashBoard == null)
            {
                ProductDashBoard = await _productDal.GetProductDashBoardByID(ProductID);
                if (ProductDashBoard == null)
                {
                    return new BaseRep<ProductDashBoard>() { code = 404, Value = new ProductDashBoard() };
                }
                CollectionProductDashBoard collectionProductDashBoard = new CollectionProductDashBoard();
                collectionProductDashBoard.ColorID = ColorID;
                int ProductColorID = await _productColorDal.GetProductColorByProductIDAndColorID(ProductID, ColorID);
                collectionProductDashBoard.ProductColorID = ProductColorID;
                collectionProductDashBoard.productImageModels = await _productImageDal.GetAllProductImageByProductColor(ProductColorID);
                var detailQuantityProductModels = await _productSizeInventoryDal.GetAllDetailQuantityProducByProductColorID(ProductColorID);
                foreach (var itemDetail in detailQuantityProductModels)
                {
                    var detailQuantityProductDisplay = new DetailQuantityProductDisplay
                    {
                        SizeID = itemDetail.SizeID,
                        Quantity = itemDetail.Quantity,
                        SizeName = (await _sizeDal.GetByID(itemDetail.SizeID)).Value.SizeName ?? "Unknown Size"
                    };
                    collectionProductDashBoard.SizeAndQuantity.Add(detailQuantityProductDisplay);
                }
                var listColorID = await _productColorDal.GetColorIDByProductID(ProductID);
                foreach (var colorID in listColorID)
                {
                    var Color = await _colorDal.GetByID(colorID);
                    ProductDashBoard.Colors.Add(Color.Value);
                }
                ProductDashBoard.CollectionProductDashBoard = collectionProductDashBoard;
                _customCache.Set(key: _customCache.GenerateCacheKeyProduct(new DetailProductReq() { ProductID = ProductID, ColorID = ColorID, Where = "DashBoardProduct" }), value: ProductDashBoard, priority: CacheItemPriority.High);
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
            var ListColorID = await _productColorDal.GetColorIDByProductID(ID);
            foreach (var colorID in ListColorID)
            {
                
                int ProductColorID = await _productColorDal.GetProductColorByProductIDAndColorID(ID, colorID);
                var listImgOfProductColorID = await _productImageDal.GetAllPublicIDByProductColor(ProductColorID);
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
                    foreach (int colorID in listColorID)
                    {
                        int ProductColorID = await _productColorDal.GetProductColorByProductIDAndColorID(item.ID, colorID);
                        ColorItemModel colorItemModel = new ColorItemModel();
                        colorItemModel.ColorID = colorID;
                        colorItemModel.ImageURL = await _productImageDal.GetImagefirstByID(ProductColorID);
                        item.ColorItemModel.Add(colorItemModel);
                    }
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

        public Task<BaseRep<string>> CreateProduct(ProductModel productModel, List<ProductDataNew> collectionModels)
        {
            throw new NotImplementedException();
        }
    }
}
