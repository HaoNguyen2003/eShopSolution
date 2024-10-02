using eShopSolution.BusinessLayer.Abstract;
using eShopSolution.DataLayer.Abstract;
using eShopSolution.DtoLayer.Model;
using eShopSolution.DtoLayer.RepositoryModel;
using Microsoft.Extensions.Caching.Memory;

namespace eShopSolution.BusinessLayer.Service
{
    public class ShippingProvidersService : IShippingProvidersService
    {
        private readonly IShippingProvidersDal _shippingProvidersDal;
        private readonly ICustomCache<string> _customCache;
        public ShippingProvidersService(IShippingProvidersDal shippingProvidersDal, ICustomCache<string> customCache)
        {
            _shippingProvidersDal = shippingProvidersDal;
            _customCache = customCache;
        }
        public async Task<BaseRep<string>> Create(ShippingProvidersModel model)
        {
            _customCache.Clear();
            return await _shippingProvidersDal.Create(model);
        }

        public async Task<BaseRep<string>> Delete(int ID)
        {
            _customCache.Clear();
            return await _shippingProvidersDal.Delete(ID);
        }

        public async Task<BaseRep<List<ShippingProvidersModel>>> GetAll()
        {
            var result = _customCache.Get<BaseRep<List<ShippingProvidersModel>>>("ShippingProvidersModel");
            if (result == null)
            {
                result = await _shippingProvidersDal.GetAll();
                _customCache.Set(key: "ShippingProvidersModel", value: result, priority: CacheItemPriority.High);
            }
            return result;
        }

        public async Task<BaseRep<ShippingProvidersModel>> GetByID(int ID)
        {
            return await _shippingProvidersDal.GetByID(ID);
        }

        public async Task<BaseRep<string>> Update(int ID, ShippingProvidersModel model)
        {
            _customCache.Clear();
            return await _shippingProvidersDal.Update(ID, model);
        }
    }
}
