using eShopSolution.BusinessLayer.Abstract;
using eShopSolution.DataLayer.Abstract;
using eShopSolution.DataLayer.EntityFramework;
using eShopSolution.DtoLayer.Model;
using eShopSolution.DtoLayer.RepositoryModel;
using Microsoft.Extensions.Caching.Memory;

namespace eShopSolution.BusinessLayer.Service
{
    public class ColorService : IColorService
    {
        private readonly IColorDal _colorDal;
        private readonly ICustomCache<string> _customCache;

        public ColorService(IColorDal colorDal, ICustomCache<string> customCache)
        {
            _colorDal = colorDal;
            _customCache = customCache;
        }
        public async Task<BaseRep<string>> Create(ColorModel model)
        {
            _customCache.Clear();
            return await _colorDal.Create(model);
        }

        public async Task<BaseRep<string>> Delete(int ID)
        {
            _customCache.Clear();
            return await _colorDal.Delete(ID);
        }

        public async Task<BaseRep<List<ColorModel>>> GetAll()
        {
            var result = _customCache.Get<BaseRep<List<ColorModel>>>("color");
            if (result == null)
            {
                result = await _colorDal.GetAll();
                _customCache.Set(key: "color", value: result, priority: CacheItemPriority.High);
            }
            return result;
        }

        public async Task<BaseRep<ColorModel>> GetByID(int ID)
        {
            
            return await _colorDal.GetByID(ID);
        }

        public async Task<BaseRep<string>> Update(int ID, ColorModel model)
        {
            _customCache.Clear();
            return await _colorDal.Update(ID, model);
        }
    }
}
