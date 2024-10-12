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
    public class PermissionMenuService : IPermissionMenuService
    {
        private readonly IPermissionMenuDal _permissionMenuDal;

        public PermissionMenuService(IPermissionMenuDal permissionMenuDal) {
            _permissionMenuDal = permissionMenuDal;
        }
        public async Task<BaseRep<string>> Create(PermissionMenuModel model)
        {
            return await _permissionMenuDal.Create(model);
        }

        public async Task<BaseRep<string>> Delete(int ID)
        {
            return await _permissionMenuDal.Delete(ID);
        }

        public async Task<BaseRep<List<PermissionMenuModel>>> GetAll()
        {
            return await _permissionMenuDal.GetAll();
        }

        public async Task<BaseRep<PermissionMenuModel>> GetByID(int ID)
        {
            return await _permissionMenuDal.GetByID(ID);
        }

        public Task<List<PermissionMenuModel>> GetPermissionMenuModelsByRoleAccessID(int ID)
        {
            throw new NotImplementedException();
        }

        public async Task<BaseRep<string>> Update(int ID, PermissionMenuModel model)
        {
            return await _permissionMenuDal.Update(ID, model);  
        }
    }
}
