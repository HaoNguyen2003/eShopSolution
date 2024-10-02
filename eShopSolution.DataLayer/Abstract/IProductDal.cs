using eShopSolution.DtoLayer.AddModel;
using eShopSolution.DtoLayer.Model;
using eShopSolution.EntityLayer.Data;

namespace eShopSolution.DataLayer.Abstract
{
    public interface IProductDal : IGenericDal<ProductModel, Product>
    {
        public Task<int> CreateProduct(ProductModel model);
        public Task<DetailProduct> GetDetailProductByID(int ID);
        public Task<ProductDashBoard> GetProductDashBoardByID(int ID);
        public Task<List<ProductCardModel>> GetAllProductConvertToProductCardModel();
        public Task<Tuple<int, List<ProductCardModel>>> GetProductByFillterAndPage(FilterModel model, int Page, int Limit);
    }
}
