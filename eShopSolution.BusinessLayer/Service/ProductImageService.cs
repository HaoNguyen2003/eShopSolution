using eShopSolution.BusinessLayer.Abstract;
using eShopSolution.DataLayer.Abstract;
using eShopSolution.DataLayer.EntityFramework;
using eShopSolution.DtoLayer.Model;
using eShopSolution.DtoLayer.RepositoryModel;
using Microsoft.Extensions.Caching.Memory;

namespace eShopSolution.BusinessLayer.Service
{
    public class ProductImageService : IProductImageService
    {
        private readonly IProductImageDal _productImageDal;
        private readonly ICustomCache<string> _customCache;

        public ProductImageService(IProductImageDal productImageDal, ICustomCache<string> customCache)
        {
            _productImageDal = productImageDal;
            _customCache = customCache;
        }
        public async Task<BaseRep<string>> Create(ProductImageModel model)
        {
            _customCache.Clear();
            return await _productImageDal.Create(model);
        }

        public async Task<BaseRep<string>> Delete(int ID)
        {
            _customCache.Clear();
            return await _productImageDal.Delete(ID);
        }

        public async Task<BaseRep<List<ProductImageModel>>> GetAll()
        {
            var result = _customCache.Get<BaseRep<List<ProductImageModel>>>("ProductImageModel");
            if (result == null)
            {
                result = await _productImageDal.GetAll();
                _customCache.Set(key: "ProductImageModel", value: result, priority: CacheItemPriority.High);
            }
            return result;
            return await _productImageDal.GetAll();
        }

        public async Task<BaseRep<ProductImageModel>> GetByID(int ID)
        {

            return await _productImageDal.GetByID(ID);
        }

        public async Task<BaseRep<string>> Update(int ID, ProductImageModel model)
        {
            _customCache.Clear();
            return await _productImageDal.Update(ID, model);
        }
    }
}
