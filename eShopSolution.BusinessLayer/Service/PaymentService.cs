using eShopSolution.BusinessLayer.Abstract;
using eShopSolution.DataLayer.Abstract;
using eShopSolution.DataLayer.EntityFramework;
using eShopSolution.DtoLayer.Model;
using eShopSolution.DtoLayer.RepositoryModel;
using Microsoft.Extensions.Caching.Memory;

namespace eShopSolution.BusinessLayer.Service
{
    public class PaymentService : IPaymentService
    {
        private readonly IPayMentDal _payMentDal;
        private readonly ICustomCache<string> _customCache;

        public PaymentService(IPayMentDal payMentDal, ICustomCache<string> customCache)
        {
            _payMentDal = payMentDal;
            _customCache = customCache;
        }
        public async Task<BaseRep<string>> Create(PaymentMethodModel model)
        {
            _customCache.Clear();
            return await _payMentDal.Create(model);
        }

        public async Task<BaseRep<string>> Delete(int ID)
        {
            _customCache.Clear();
            return await _payMentDal.Delete(ID);
        }

        public async Task<BaseRep<List<PaymentMethodModel>>> GetAll()
        {
            var result = _customCache.Get<BaseRep<List<PaymentMethodModel>>>("PaymentMethodModel");
            if (result == null)
            {
                result = await _payMentDal.GetAll();
                _customCache.Set(key: "PaymentMethodModel", value: result, priority: CacheItemPriority.High);
            }
            return result;
        }

        public async Task<BaseRep<PaymentMethodModel>> GetByID(int ID)
        {
            return await _payMentDal.GetByID(ID);
        }

        public async Task<BaseRep<string>> Update(int ID, PaymentMethodModel model)
        {
             _customCache.Clear();
            return await _payMentDal.Update(ID, model);
        }
    }
}
