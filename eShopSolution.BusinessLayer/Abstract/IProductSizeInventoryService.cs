using eShopSolution.DtoLayer.Model;
using eShopSolution.DtoLayer.RepositoryModel;
using eShopSolution.EntityLayer.Data;

namespace eShopSolution.BusinessLayer.Abstract
{
    public interface IProductSizeInventoryService : IGenericService<DetailQuantityProductModel, ProductSizeInventory>
    {
        public Task<BaseRep<string>> DeleteProductSizeInventoryService(int ProductColorID, int SizeID);
        public Task<BaseRep<DetailQuantityProductModel>> GetProductSizeInventoryByProductColorIDAndSizeID(int ProductColorID, int SizeID);
    }
}
