using AutoMapper;
using eShopSolution.DataLayer.Abstract;
using eShopSolution.DataLayer.Context;
using eShopSolution.DtoLayer.Model;
using eShopSolution.EntityLayer.Data;

namespace eShopSolution.DataLayer.EntityFramework
{
    public class OrderStatusDal : GenericDal<OrderStatusModel, OrderStatus>, IStatusOrderDal
    {
        public OrderStatusDal(ApplicationContext context, IMapper mapper) : base(context, mapper)
        {
        }
    }
}
