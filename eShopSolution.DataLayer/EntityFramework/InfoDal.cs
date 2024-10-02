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
    public class InfoDal : GenericDal<InfoPaymentModel, InfoPayment>, IInfoDal
    {
        public InfoDal(ApplicationContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<InfoPaymentModel> GetByOrderId(int OrderID)
        {
            var infoPayment = await _context.infoPayments.FirstOrDefaultAsync(i=>i.OrderID == OrderID);
            return _mapper.Map<InfoPaymentModel>(infoPayment);
        }
    }
}
