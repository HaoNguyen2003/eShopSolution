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
    public class PermissionService : IPermissionService
    {
        private readonly IPermissionDal _permissionDal;

        public PermissionService(IPermissionDal permissionDal) {
            _permissionDal=permissionDal;
        }  
        public async Task<BaseRep<string>> Create(PermissionModel model)
        {
            return await _permissionDal.Create(model);
        }

        public async Task<BaseRep<string>> Delete(int ID)
        {
            return await _permissionDal.Delete(ID);
        }

        public async Task<BaseRep<List<PermissionModel>>> GetAll()
        {
            return await _permissionDal.GetAll();
        }

        public async Task<BaseRep<PermissionModel>> GetByID(int ID)
        {
            return await _permissionDal.GetByID(ID);
        }

        public async Task<BaseRep<string>> Update(int ID, PermissionModel model)
        {
           return await _permissionDal.Update(ID, model);
        }
    }
}
