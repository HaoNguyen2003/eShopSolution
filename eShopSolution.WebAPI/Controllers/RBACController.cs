using eShopSolution.BusinessLayer.Abstract;
using eShopSolution.BusinessLayer.Service;
using eShopSolution.DtoLayer.AddModel;
using eShopSolution.DtoLayer.Model;
using eShopSolution.EntityLayer.Data;
using eShopSolution.WebAPI.Permission;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace eShopSolution.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RBACController : ControllerBase
    {
        private readonly IRBACService _rBACService;

        public RBACController(IRBACService rBACService) {
            _rBACService=rBACService;
        }
        [HttpGet("GetALLPermissionOfUser")]
        [Authorize]
        public async Task<IActionResult> GetALLPermissionOfUser() 
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized(new { Error = "User not authorized" });
            }
            var result = await _rBACService.GetAllPermissionOfUser(userId);
            if(result.IsSuccess)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpGet("GetALLPermissionOfRole")]
        [PermissionAuthorize(PermissionA.RBAC + "." + AccessA.Get)]
        public async Task<IActionResult> GetALLPermissionOfRole(string RoleID)
        {
            var result = await _rBACService.GetAllPermissionOfRole(RoleID);
            if (result.IsSuccess)
                return Ok(result);
            return BadRequest(result);
        }
        [HttpPost("CreateRoleAndPermission")]
        [PermissionAuthorize(PermissionA.RBAC + "." + AccessA.Create)]
        public async Task<IActionResult> CreateRoleAndPermission(AddRoleAndPermissions addRoleAndPermissions)
        {
            var result = await _rBACService.CreateRoleAndPermission(addRoleAndPermissions.role, addRoleAndPermissions.permissionMenuModels);
            if (result.IsSuccess)
                return Ok(result);
            return BadRequest(result);
        }
        [HttpGet("AllRoleAccess")]
        [PermissionAuthorize(PermissionA.RBAC + "." + AccessA.Get)]
        public async Task<IActionResult> GetAllRoleAccess()
        {
            var result = await _rBACService.GetAllRoleAccess();
            if (result.IsSuccess)
                return Ok(result);
            return BadRequest(result);
        }
        [HttpGet("GetRoleAccessDetailByID")]
        [PermissionAuthorize(PermissionA.RBAC + "." + AccessA.Get)]
        public async Task<IActionResult> GetRoleAccessByID(int RoleAccessID)
        {
            var result = await _rBACService.GetRoleAccessByID(RoleAccessID);
            if (result.IsSuccess)
                return Ok(result);
            return BadRequest(result);
        }
    }
}
