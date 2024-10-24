using eShopSolution.BusinessLayer.Abstract;
using eShopSolution.DataLayer.Abstract;
using eShopSolution.DtoLayer.Model;
using eShopSolution.DtoLayer.RepositoryModel;
using Microsoft.Extensions.Caching.Memory;

namespace eShopSolution.BusinessLayer.Service
{
    public class ProductSizeInventoryService : IProductSizeInventoryService
    {
        private readonly IProductSizeInventoryDal _productSizeInventoryDal;
        private readonly ICustomCache<string> _customCache;

        public ProductSizeInventoryService(IProductSizeInventoryDal productSizeInventoryDal, ICustomCache<string> customCache)
        {
            _productSizeInventoryDal = productSizeInventoryDal;
            _customCache = customCache;
        }
        public async Task<BaseRep<string>> Create(DetailQuantityProductModel model)
        {
            _customCache.Clear();
            return await _productSizeInventoryDal.Create(model);
        }

        public async Task<BaseRep<string>> Delete(int ID)
        {
            _customCache.Clear();
            return await _productSizeInventoryDal.Delete(ID);
        }
        public async Task<BaseRep<string>> DeleteProductSizeInventoryService(int ProductColorID, int SizeID)
        {
            _customCache.Clear();
            return await _productSizeInventoryDal.DeleteProductSizeInventory(ProductColorID, SizeID);
        }
        public async Task<BaseRep<List<DetailQuantityProductModel>>> GetAll()
        {
            var result = _customCache.Get<BaseRep<List<DetailQuantityProductModel>>>("DetailQuantityProductModel");
            if (result == null)
            {
                result = await _productSizeInventoryDal.GetAll();
                _customCache.Set(key: "DetailQuantityProductModel", value: result, priority: CacheItemPriority.High);
            }
            return result;
        }

        public async Task<BaseRep<DetailQuantityProductModel>> GetByID(int ID)
        {
            return await _productSizeInventoryDal.GetByID(ID);
        }

        public Task<BaseRep<DetailQuantityProductModel>> GetProductSizeInventoryByProductColorIDAndSizeID(int ProductColorID, int SizeID)
        {

           return _productSizeInventoryDal.GetProductSizeInventoryByProductColorIDAndSizeID(ProductColorID, SizeID);
        }

        public async Task<BaseRep<string>> Update(int ID, DetailQuantityProductModel model)
        {
            _customCache.Clear();
            return await _productSizeInventoryDal.Update(model.ID,model);
        }
    }
}
