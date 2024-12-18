﻿using eShopSolution.DtoLayer.AddModel;
using eShopSolution.DtoLayer.Model;
using eShopSolution.DtoLayer.RepositoryModel;
using eShopSolution.EntityLayer.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.BusinessLayer.Abstract
{
    public interface IRBACService
    {
        public Task<Response<List<PolicyModel>>> GetAllPermissionOfUser(string UserID);
        public Task<Response<List<PolicyModel>>> CreateRoleAndPermission(AddRole role, List<PermissionMenuModel> permissionMenuModels);
        public Task<Response<PolicyModel>> GetRoleAccessByID(int RoleAccessID);
        public Task<Response<List<PolicyModel>>> GetAllRoleAccess();
        public Task<Response<List<PolicyModel>>> GetAllPermissionOfRole(string RoleID);

    }
}
