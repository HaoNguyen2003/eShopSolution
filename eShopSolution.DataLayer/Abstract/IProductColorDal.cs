using eShopSolution.DtoLayer.Model;
using eShopSolution.DtoLayer.RepositoryModel;
using eShopSolution.EntityLayer.Data;

namespace eShopSolution.DataLayer.Abstract
{
    public interface IProductColorDal : IGenericDal<ProductColorModel, ProductColors>
    {
        public Task<BaseRep<string>> UpdateProductColor(int ID, ProductColorModel model);
        public Task<BaseRep<string>> CreateProductColor(ProductColorModel model);
        public Task<int> CreateProductColorReturnID(ProductColorModel model);
        public Task<List<int>> GetColorIDByProductID(int productID);
        public Task<int> GetProductColorByProductIDAndColorID(int productID, int colorID);

    }
}
