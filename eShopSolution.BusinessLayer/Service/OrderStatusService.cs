using eShopSolution.BusinessLayer.Abstract;
using eShopSolution.DataLayer.Abstract;
using eShopSolution.DtoLayer.Model;
using eShopSolution.DtoLayer.RepositoryModel;
using Microsoft.Extensions.Caching.Memory;

namespace eShopSolution.BusinessLayer.Service
{
    public class OrderStatusService : IOrderStatusService
    {
        private readonly IStatusOrderDal _statusOrderDal;
        private readonly ICustomCache<string> _customCache;
        public OrderStatusService(IStatusOrderDal statusOrderDal, ICustomCache<string> customCache)
        {
            _statusOrderDal = statusOrderDal;
            _customCache = customCache;
        }
        public async Task<BaseRep<string>> Create(OrderStatusModel model)
        {
            _customCache.Clear();
            return await _statusOrderDal.Create(model);
        }

        public async Task<BaseRep<string>> Delete(int ID)
        {
            _customCache.Clear();
            return await _statusOrderDal.Delete(ID);
        }

        public async Task<BaseRep<List<OrderStatusModel>>> GetAll()
        {

            var result = _customCache.Get<BaseRep<List<OrderStatusModel>>>("OrderStatusModel");
            if (result == null)
            {
                result = await _statusOrderDal.GetAll();
                _customCache.Set(key: "PaymentMethodModel", value: result, priority: CacheItemPriority.High);
            }
            return result;
        }

        public async Task<BaseRep<OrderStatusModel>> GetByID(int ID)
        {
            return await _statusOrderDal.GetByID(ID);
        }

        public async Task<BaseRep<string>> Update(int ID, OrderStatusModel model)
        {
            _customCache.Clear();
            return await _statusOrderDal.Update(ID, model);
        }
    }
}
