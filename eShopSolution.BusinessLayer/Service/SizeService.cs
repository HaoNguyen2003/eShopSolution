using eShopSolution.BusinessLayer.Abstract;
using eShopSolution.DataLayer.Abstract;
using eShopSolution.DataLayer.EntityFramework;
using eShopSolution.DtoLayer.Model;
using eShopSolution.DtoLayer.RepositoryModel;
using Microsoft.Extensions.Caching.Memory;

namespace eShopSolution.BusinessLayer.Service
{
    public class SizeService : ISizeService
    {
        private readonly ISizeDal _sizeDal;
        private readonly ICustomCache<string> _customCache;

        public SizeService(ISizeDal sizeDal, ICustomCache<string> customCache)
        {
            _sizeDal = sizeDal; _customCache = customCache;
        }
        public async Task<BaseRep<string>> Create(SizeModel model)
        {
            _customCache.Clear();
            return await _sizeDal.Create(model);
        }

        public async Task<BaseRep<string>> Delete(int ID)
        {
            _customCache.Clear();
            return await _sizeDal.Delete(ID);
        }

        public async Task<BaseRep<List<SizeModel>>> GetAll()
        {
            var result = _customCache.Get<BaseRep<List<SizeModel>>>("size");
            if (result == null)
            {
                result = await _sizeDal.GetAll();
                _customCache.Set(key: "size", value: result, priority: CacheItemPriority.High);
            }
            return result;
        }

        public async Task<BaseRep<SizeModel>> GetByID(int ID)
        {
            return await _sizeDal.GetByID(ID);
        }

        public async Task<BaseRep<string>> Update(int ID, SizeModel model)
        {
            _customCache.Clear();
            return await _sizeDal.Update(ID, model);
        }
    }
}
