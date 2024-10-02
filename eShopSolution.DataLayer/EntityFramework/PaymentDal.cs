using AutoMapper;
using eShopSolution.DataLayer.Abstract;
using eShopSolution.DataLayer.Context;
using eShopSolution.DtoLayer.Model;
using eShopSolution.EntityLayer.Data;

namespace eShopSolution.DataLayer.EntityFramework
{
    public class PaymentDal : GenericDal<PaymentMethodModel, PaymentMethod>, IPayMentDal
    {
        public PaymentDal(ApplicationContext context, IMapper mapper) : base(context, mapper)
        {
        }
    }
}
