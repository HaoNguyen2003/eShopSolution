using eShopSolution.BusinessLayer.Abstract;
using eShopSolution.DataLayer.Abstract;
using eShopSolution.DataLayer.EntityFramework;
using eShopSolution.DtoLayer.Model;
using eShopSolution.DtoLayer.RepositoryModel;
using eShopSolution.EntityLayer.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.BusinessLayer.Service
{
    public class AspNetRoleAccessService : IAspNetRoleAccessService
    {
        private readonly IAspNetRoleAccessDal _aspNetRoleAccessDal;

        public AspNetRoleAccessService(IAspNetRoleAccessDal aspNetRoleAccessDal)
        {
            _aspNetRoleAccessDal=aspNetRoleAccessDal;
        }
        public async Task<BaseRep<string>> Create(RoleAccessModel model)
        {
            return await _aspNetRoleAccessDal.Create(model);
        }

        public async Task<BaseRep<string>> Delete(int ID)
        {
            return await _aspNetRoleAccessDal.Delete(ID);
        }

        public async Task<BaseRep<List<RoleAccessModel>>> GetAll()
        {
            return await _aspNetRoleAccessDal.GetAll();
        }

        public async Task<List<RoleAccessModel>> GetAllRoleAccessModel(string RoleID)
        {
            return await _aspNetRoleAccessDal.GetAllRoleAccessModel(RoleID);
        }

        public async Task<BaseRep<RoleAccessModel>> GetByID(int ID)
        {
            return await _aspNetRoleAccessDal.GetByID(ID);
        }

        public async Task<BaseRep<string>> Update(int ID, RoleAccessModel model)
        {
            return await _aspNetRoleAccessDal.Update(ID, model);
        }
    }
}
