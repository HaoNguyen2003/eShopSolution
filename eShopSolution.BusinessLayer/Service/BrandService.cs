using eShopSolution.BusinessLayer.Abstract;
using eShopSolution.DataLayer.Abstract;
using eShopSolution.DtoLayer.Model;
using eShopSolution.DtoLayer.RepositoryModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System.Diagnostics.Metrics;

namespace eShopSolution.BusinessLayer.Service
{
    public class BrandService : IBrandService
    {
        private readonly IBrandDal _brandDal;
        private readonly ICustomCache<string> _customCache;
        public BrandService(IBrandDal brandDal, ICustomCache<string> customCache)
        {
            _brandDal = brandDal;
            _customCache = customCache;
        }
        public async Task<BaseRep<List<BrandModel>>> GetAll()
        {
            var result = _customCache.Get<BaseRep<List<BrandModel>>>("brand");
            if (result == null)
            {
                result = await _brandDal.GetAll();
               _customCache.Set(key: "brand", value: result, priority: CacheItemPriority.High);
            }
            return result;
        }

        public async Task<BaseRep<BrandModel>> GetByID(int ID)
        {
            return await _brandDal.GetByID(ID);
        }
        public async Task<BaseRep<string>> Create(BrandModel model)
        {
            _customCache.Clear();
            return await _brandDal.Create(model);
        }
        public async Task<BaseRep<string>> Delete(int ID)
        {
            _customCache.Clear();
            return await _brandDal.Delete(ID);
        }
        public async Task<BaseRep<string>> Update(int ID, BrandModel model)
        {
            _customCache.Clear();
            return await _brandDal.Update(ID, model);
        }
    }
}
