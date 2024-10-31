using eShopSolution.BusinessLayer.Abstract;
using eShopSolution.BusinessLayer.Service;
using eShopSolution.DtoLayer.Model;
using eShopSolution.WebAPI.Permission;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eShopSolution.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleAccessController : ControllerBase
    {
        private readonly IAspNetRoleAccessService _aspNetRoleAccessService;

        public RoleAccessController(IAspNetRoleAccessService aspNetRoleAccessService) {
            _aspNetRoleAccessService=aspNetRoleAccessService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _aspNetRoleAccessService.GetAll();
            return Ok(result);
        }
        [HttpGet("ID")]
        [PermissionAuthorize(PermissionA.RoleAccess + "." + AccessA.Get)]
        public async Task<IActionResult> GetByID(int ID)
        {
            var result = await _aspNetRoleAccessService.GetByID(ID);
            if (result.code != 200)
                return NotFound();
            return Ok(result);
        }
        [HttpPost]
        [PermissionAuthorize(PermissionA.RoleAccess + "." + AccessA.Create)]
        public async Task<IActionResult> Create(RoleAccessModel roleccessModel)
        {
            var result = await _aspNetRoleAccessService.Create(roleccessModel);
            return StatusCode(result.code, result);
        }
        [HttpPut("ID")]
        [PermissionAuthorize(PermissionA.RoleAccess + "." + AccessA.Update)]
        public async Task<IActionResult> Update(RoleAccessModel roleccessModel)
        {
            var result = await _aspNetRoleAccessService.Update(roleccessModel.ID, roleccessModel);
            return StatusCode(result.code, result);
        }
        [HttpDelete("ID")]
        [PermissionAuthorize(PermissionA.RoleAccess + "." + AccessA.Delete)]
        public async Task<IActionResult> Delete(int ID)
        {
            var result = await _aspNetRoleAccessService.Delete(ID);
            return StatusCode(result.code, result);
        }
    }
}
