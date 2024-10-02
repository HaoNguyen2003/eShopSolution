using AutoMapper;
using eShopSolution.DataLayer.Abstract;
using eShopSolution.DataLayer.Context;
using eShopSolution.DtoLayer.Model;
using eShopSolution.EntityLayer.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.DataLayer.EntityFramework
{
    public class DetailOrderDal : GenericDal<DetailOrderModel, DetailOrder>, IDetailOrderDal
    {
        public DetailOrderDal(ApplicationContext context, IMapper mapper) : base(context, mapper)
        {
        }
        public async Task<List<DetailOrderModel>> GetDetailOrderModelsByOrderID(int OrderID)
        {
            var DetailOrders = await _context.detailOrders.Where(x => x.OrderID == OrderID).ToListAsync();
            return _mapper.Map<List<DetailOrderModel>>(DetailOrders);
        }
    }
}
