using eShopSolution.BusinessLayer.Abstract;
using eShopSolution.DataLayer.Migrations;
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
        private readonly IUserService _userService;
        private readonly IPermissionMenuService _permissionMenuService;
        private readonly IPermissionService _permissionService;
        private readonly IMenuService _menuService;
        private readonly UserManager<AppUser> _userManager;
        public RBACService(IAspNetRoleAccessService aspNetRoleAccessService,
            IMenuService menuService,
            IPermissionService permissionService,
            UserManager<AppUser> userManager,
            IUserService userService,
            IPermissionMenuService permissionMenuService) {
            _aspNetRoleAccessService = aspNetRoleAccessService;
            _userService = userService;
            _permissionMenuService = permissionMenuService;
            _permissionService = permissionService;
            _menuService = menuService;
            _userManager = userManager;
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

        public async Task<Response<List<PolicyModel>>> GetAllPermissionOfRole(string RoleID)
        {
            var ListPolicyModels = new List<PolicyModel>();
            var ListAccessModels = await _aspNetRoleAccessService.GetAllRoleAccessModel(RoleID);
            foreach(var permissionAccessModel in ListAccessModels)
            {
                var PermissionMenu = await _permissionMenuService.GetByID(permissionAccessModel.MenuPermissionID);
                var PermissionModel = await _permissionService.GetByID(PermissionMenu.Value.PermissionID);
                var MenuModel = await _menuService.GetByID(PermissionMenu.Value.MenuID);
                ListPolicyModels.Add(new PolicyModel() { RoleAccessID = permissionAccessModel.ID, menu = MenuModel.Value, permission = PermissionModel.Value });
            }
            return new Response<List<PolicyModel>>() { IsSuccess=true,Value = ListPolicyModels};
        }
        public List<PolicyModel> MergeObjectArrays(List<PolicyModel> array1, List<PolicyModel> array2)
        {
            return array1.Concat(array2)
                         .GroupBy(obj => obj.RoleAccessID)
                         .Select(group => group.First())
                         .ToList();
        }

        public async Task<Response<List<PolicyModel>>> GetAllPermissionOfUser(string UserID)
        {
            var appUser = await _userManager.FindByIdAsync(UserID);
            if(appUser == null)
                return new Response<List<PolicyModel>>() { IsSuccess=false,Value = new List<PolicyModel>() };
            var userRoles = await _userManager.GetRolesAsync(appUser);
            var PolicyModels = new List<PolicyModel>();
            foreach (var claim in userRoles)
            {
                var ClaimRole = await _userService.GetRolesByIDOrNameAsync(null, claim);
                var ResultPermissionRoles = await GetAllPermissionOfRole(ClaimRole.Value.Id);
                if (ResultPermissionRoles.IsSuccess)
                {
                    PolicyModels = MergeObjectArrays(PolicyModels, ResultPermissionRoles.Value);
                }
            }
            return new Response<List<PolicyModel>>() {IsSuccess =true,Value = PolicyModels};
        }


        public async Task<Response<PolicyModel>> GetRoleAccessByID(int RoleAccessID)
        {
            var AccessModel = await _aspNetRoleAccessService.GetByID(RoleAccessID);
            if (AccessModel.code != 200)
                return new Response<PolicyModel>() { IsSuccess = false ,Error=AccessModel.code+""};
            var PermissionMenu = await _permissionMenuService.GetByID(AccessModel.Value.MenuPermissionID);
            var PermissionModel = await _permissionService.GetByID(PermissionMenu.Value.PermissionID);
            var MenuModel = await _menuService.GetByID(PermissionMenu.Value.MenuID);
            return new Response<PolicyModel>
            {
                IsSuccess = true,
                Value = new PolicyModel
                {
                    RoleAccessID = RoleAccessID,
                    menu = MenuModel.Value,
                    permission = PermissionModel.Value
                }
            };
        }

        public async Task<Response<List<PolicyModel>>> GetAllRoleAccess()
        {
            var Roles = await _userService.GetAllRolesAsync();
            var PolicyModels = new List<PolicyModel>();
            if (Roles.IsSuccess == false)
                return new Response<List<PolicyModel>>() { IsSuccess = true, Value = PolicyModels };
            foreach (var claim in Roles.Value)
            {
                var ResultPermissionRoles = await GetAllPermissionOfRole(claim.Id);
                if (ResultPermissionRoles.IsSuccess)
                {
                    PolicyModels = MergeObjectArrays(PolicyModels, ResultPermissionRoles.Value);
                }
            }
            return new Response<List<PolicyModel>>() { IsSuccess = true, Value = PolicyModels };
        }

    }
}
