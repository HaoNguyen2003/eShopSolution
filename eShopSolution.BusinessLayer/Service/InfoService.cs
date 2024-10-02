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
    public class InfoService : IInfoPaymentService
    {
        private readonly IInfoDal _infoDal;

        public InfoService(IInfoDal infoDal) {
            _infoDal=infoDal;
        }
        public async Task<BaseRep<string>> Create(InfoPaymentModel model)
        {
            return await _infoDal.Create(model);
          
        }

        public async Task<BaseRep<string>> Delete(int ID)
        {
            return await _infoDal.Delete(ID);
         
        }

        public Task<BaseRep<List<InfoPaymentModel>>> GetAll()
        {
            return _infoDal.GetAll();
          
        }

        public async Task<BaseRep<InfoPaymentModel>> GetByID(int ID)
        {
            return await _infoDal.GetByID(ID);
         
        }

        public async Task<InfoPaymentModel> GetByOrderId(int OrderID)
        {
           return await _infoDal.GetByOrderId(OrderID);
        }

        public async Task<BaseRep<string>> Update(int ID, InfoPaymentModel model)
        {
            return await _infoDal.Update(ID, model);
      
        }
    }
}
