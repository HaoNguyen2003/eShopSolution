using eShopSolution.BusinessLayer.Abstract;
using eShopSolution.DtoLayer.Model;
using eShopSolution.EntityLayer.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.BusinessLayer.Abstract
{
    public interface IAspNetRoleAccessService:IGenericService<RoleAccessModel,AspNetRoleAccess>
    {
    }
}
