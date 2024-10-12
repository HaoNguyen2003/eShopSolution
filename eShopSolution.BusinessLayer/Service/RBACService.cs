using eShopSolution.BusinessLayer.Abstract;
using eShopSolution.DtoLayer.AddModel;
using eShopSolution.DtoLayer.Model;
using eShopSolution.DtoLayer.RepositoryModel;
using eShopSolution.EntityLayer.Data;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.BusinessLayer.Service
{
    public class RBACService : IRBACService
    {
        private readonly IAspNetRoleAccessService _aspNetRoleAccessService;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly IUserService _userService;
        private readonly IPermissionMenuService _permissionMenuService;
        private readonly IPermissionService _permissionService;
        private readonly IMenuService _menuService;
        public RBACService(IAspNetRoleAccessService aspNetRoleAccessService,IMenuService menuService,IPermissionService permissionService,RoleManager<AppRole> roleManager,IUserService userService,IPermissionMenuService permissionMenuService) {
            _aspNetRoleAccessService = aspNetRoleAccessService;
            _roleManager = roleManager;
            _userService = userService;
            _permissionMenuService = permissionMenuService;
            _permissionService = permissionService;
            _menuService = menuService;
        }
        public async Task<Response<string>> CreateRoleAccess(RoleAccessModel roleAccessModel)
        {
            var result = await _aspNetRoleAccessService.Create(roleAccessModel);
            return new Response<string>() {IsSuccess =(result.code==200)?true:false,Error= (result.code == 200) ?"": result.Value,Value= (result.code == 200) ? result.Value:""};
        }

        public async Task<Response<List<PolicyModel>>> CreateRoleAndPermission(AddRole role, List<PermissionMenuModel> permissionMenuModels)
        {
            var resultRole = await _userService.CreateRoleAsync(role);
            if(!resultRole.IsSuccess)
                return new Response<List<PolicyModel>>() {IsSuccess = false ,Error= resultRole.Error };
            foreach (var permissionMenuModel in permissionMenuModels)
            {
                await _aspNetRoleAccessService.Create(new RoleAccessModel() { ID = 0, MenuPermissionID = permissionMenuModel.ID, RoleID = resultRole.Value.Id });
            }
            return await GetAllPermissionOfRole(resultRole.Value.Id);
        }

        public async Task<Response<string>> DeleteRoleAccess(int ID)
        {
            var result = await _aspNetRoleAccessService.Delete(ID);
            return new Response<string>() { IsSuccess = (result.code == 200) ? true : false, Error = (result.code == 200) ? "" : result.Value, Value = (result.code == 200) ? result.Value : "" };
        }

        public async Task<Response<List<PolicyModel>>> GetAllPermissionOfRole(string RoleID)
        {
            var ListPolicyModels = new List<PolicyModel>();
            var ListAccessModels = await _aspNetRoleAccessService.GetAllRoleAccessModel(RoleID);
            foreach(var permissionAccessModel in ListAccessModels)
            {
                var PermissionMenu = await _permissionMenuService.GetByID(permissionAccessModel.MenuPermissionID);
                var PermissionModel = await _permissionService.GetByID(PermissionMenu.Value.PermissionID);
                var MenuModel = await _menuService.GetByID(PermissionMenu.Value.MenuID);
                ListPolicyModels.Add(new PolicyModel() { PermissionMenuID = permissionAccessModel.MenuPermissionID, menu = MenuModel.Value, permission = PermissionModel.Value });
            }
            return new Response<List<PolicyModel>>() { IsSuccess=true,Value = ListPolicyModels};
        }

        public Task<Response<List<PolicyModel>>> GetAllPermissionOfUser(string UserID)
        {
            throw new NotImplementedException();
        }
    }
}
