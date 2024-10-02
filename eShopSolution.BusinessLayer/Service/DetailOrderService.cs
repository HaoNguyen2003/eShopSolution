using eShopSolution.BusinessLayer.Abstract;
using eShopSolution.DataLayer.Abstract;
using eShopSolution.DataLayer.EntityFramework;
using eShopSolution.DtoLayer.Model;
using eShopSolution.DtoLayer.RepositoryModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.BusinessLayer.Service
{
    public class DetailOrderService : IDetailOrderService
    {
        private readonly IDetailOrderDal _detailOrderDal;

        public DetailOrderService(IDetailOrderDal detailOrderDal) {
            _detailOrderDal= detailOrderDal;
        }
        public async Task<BaseRep<string>> Create(DetailOrderModel model)
        {
            return await _detailOrderDal.Create(model);
        }

        public async Task<BaseRep<string>> Delete(int ID)
        {
            return await _detailOrderDal.Delete(ID);
        }

        public async Task<BaseRep<List<DetailOrderModel>>> GetAll()
        {
            return await _detailOrderDal.GetAll();
        }

        public async Task<BaseRep<DetailOrderModel>> GetByID(int ID)
        {
            return await _detailOrderDal.GetByID(ID);
            
        }

        public async Task<List<DetailOrderModel>> GetDetailOrderModelsByOrderID(int OrderID)
        {
            return await _detailOrderDal.GetDetailOrderModelsByOrderID(OrderID);
        }

        public async Task<BaseRep<string>> Update(int ID, DetailOrderModel model)
        {
            return await _detailOrderDal.Update(ID, model);
        }
    }
}
