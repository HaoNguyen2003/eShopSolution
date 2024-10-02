using eShopSolution.BusinessLayer.Abstract;
using eShopSolution.DataLayer.Abstract;
using eShopSolution.DataLayer.EntityFramework;
using eShopSolution.DtoLayer.Model;
using eShopSolution.DtoLayer.RepositoryModel;
using Microsoft.Extensions.Caching.Memory;

namespace eShopSolution.BusinessLayer.Service
{
    public class GenderService : IGenderService
    {
        private readonly IGenderDal _genderDal;
        private readonly ICustomCache<string> _customCache;

        public GenderService(IGenderDal genderDal, ICustomCache<string> customCache)
        {
            _genderDal = genderDal;
            _customCache = customCache;
        }
        public async Task<BaseRep<string>> Create(GenderModel model)
        {
            _customCache.Clear();
            return await _genderDal.Create(model);
        }

        public async Task<BaseRep<string>> Delete(int ID)
        {
            _customCache.Clear();
            return await _genderDal.Delete(ID);
        }

        public async Task<BaseRep<List<GenderModel>>> GetAll()
        {
            var result = _customCache.Get<BaseRep<List<GenderModel>>>("gender");
            if (result == null)
            {
                result = await _genderDal.GetAll();
                _customCache.Set(key: "gender", value: result, priority: CacheItemPriority.High);
            }
            return result;
        }

        public async Task<BaseRep<GenderModel>> GetByID(int ID)
        {
            return await _genderDal.GetByID(ID);
        }

        public async Task<BaseRep<string>> Update(int ID, GenderModel model)
        {
            _customCache.Clear();
            return await _genderDal.Update(ID, model);
        }
    }
}
