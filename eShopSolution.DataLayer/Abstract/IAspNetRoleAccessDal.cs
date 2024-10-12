using eShopSolution.DtoLayer.Model;
using eShopSolution.EntityLayer.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.DataLayer.Abstract
{
    public interface IAspNetRoleAccessDal:IGenericDal<RoleAccessModel,AspNetRoleAccess>
    {
        public Task<List<RoleAccessModel>> GetAllRoleAccessModel(string RoleID);
    }
}
