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
    public class AspNetRoleAccessDal : GenericDal<RoleAccessModel, AspNetRoleAccess>, IAspNetRoleAccessDal
    {
        public AspNetRoleAccessDal(ApplicationContext context, IMapper mapper) : base(context, mapper)
        {
        }
        public async Task<List<RoleAccessModel>> GetAllRoleAccessModel(string RoleID)
        {
            var resultRoleAccessModels = await _context.AspNetRoleAccesses.Where(x=>x.RoleID==RoleID).ToListAsync();
            return _mapper.Map<List<RoleAccessModel>>(resultRoleAccessModels);
        }
    }
}
