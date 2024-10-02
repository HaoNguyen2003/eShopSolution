using eShopSolution.DtoLayer.Model;
using eShopSolution.EntityLayer.Data;

namespace eShopSolution.BusinessLayer.Abstract
{
    public interface IPaymentService : IGenericService<PaymentMethodModel, PaymentMethod>
    {
    }
}
