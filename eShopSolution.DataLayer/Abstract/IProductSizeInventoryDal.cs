using eShopSolution.DtoLayer.Model;
using eShopSolution.DtoLayer.RepositoryModel;
using eShopSolution.EntityLayer.Data;

namespace eShopSolution.DataLayer.Abstract
{
    public interface IProductSizeInventoryDal : IGenericDal<DetailQuantityProductModel, ProductSizeInventory>
    {
        public Task<List<DetailQuantityProductModel>> GetAllDetailQuantityProducByProductColorID(int ID);
        public Task<BaseRep<string>> CreateProductSizeInventory(DetailQuantityProductModel model);
        public Task<BaseRep<string>> UpdateProductSizeInventory(DetailQuantityProductModel model);
        public Task<BaseRep<string>> DeleteProductSizeInventory(int ProductColorID, int SizeID);
        public Task<BaseRep<DetailQuantityProductModel>> GetProductSizeInventoryByProductColorIDAndSizeID(int ProductColorID, int SizeID);
    }
}
