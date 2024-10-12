using eShopSolution.BusinessLayer.Abstract;
using eShopSolution.BusinessLayer.Service;
using eShopSolution.DtoLayer.AddModel;
using eShopSolution.DtoLayer.Model;
using eShopSolution.EntityLayer.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> GetALLPermissionOfUser(string UserID) 
        { 
            var result = await _rBACService.GetAllPermissionOfUser(UserID);
            if(result.IsSuccess)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpGet("GetALLPermissionOfRole")]
        public async Task<IActionResult> GetALLPermissionOfRole(string RoleID)
        {
            var result = await _rBACService.GetAllPermissionOfRole(RoleID);
            if (result.IsSuccess)
                return Ok(result);
            return BadRequest(result);
        }
        [HttpPost("CreateRoleAndPermission")]
        public async Task<IActionResult> CreateRoleAndPermission(AddRoleAndPermissions addRoleAndPermissions)
        {
            var result = await _rBACService.CreateRoleAndPermission(addRoleAndPermissions.role, addRoleAndPermissions.permissionMenuModels);
            if (result.IsSuccess)
                return Ok(result);
            return BadRequest(result);
        }
        [HttpGet("AllRoleAccess")]
        public async Task<IActionResult> GetAllRoleAccess()
        {
            var result = await _rBACService.GetAllRoleAccess();
            if (result.IsSuccess)
                return Ok(result);
            return BadRequest(result);
        }
        [HttpGet("GetRoleAccessDetailByID")]
        public async Task<IActionResult> GetRoleAccessByID(int RoleAccessID)
        {
            var result = await _rBACService.GetRoleAccessByID(RoleAccessID);
            if (result.IsSuccess)
                return Ok(result);
            return BadRequest(result);
        }
    }
}
