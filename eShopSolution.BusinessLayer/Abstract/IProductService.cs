using eShopSolution.CrawlData.Model;
using eShopSolution.DtoLayer.AddModel;
using eShopSolution.DtoLayer.Model;
using eShopSolution.DtoLayer.RepositoryModel;
using eShopSolution.EntityLayer.Data;

namespace eShopSolution.BusinessLayer.Abstract
{
    public interface IProductService : IGenericService<ProductModel, Product>
    {
        public Task<BaseRep<string>> CreateProduct(ProductModel productModel, List<CollectionModel> collectionModels);
        public Task<BaseRep<DetailProduct>> GetDetailProductByProductIDAndColorID(int ID, int ColorID);
        public Task<BaseRep<ProductDashBoard>> GetProductInDashBoardByProductIDAndColorID(int ProductID, int ColorID);
        public Task<BaseRep<List<ProductCardModel>>> GetAllProduct();
        public Task<BaseRep<List<String>>> DeleteProduct(int ID);
        public Task<BaseRep<PagedResult>> GetProductByFilterAndPage(FilterModel model, int Page, int Limit);
        public Task<BaseRep<string>> CreateProduct(ProductModel productModel, List<ProductDataNew> collectionModels);
    }
}
