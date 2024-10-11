using eShopSolution.BusinessLayer.Abstract;
using eShopSolution.BusinessLayer.Service;
using eShopSolution.DtoLayer.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eShopSolution.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuPermissionController : ControllerBase
    {
        private readonly IPermissionMenuService _permissionMenuService;

        public MenuPermissionController(IPermissionMenuService permissionMenuService) {
            _permissionMenuService= permissionMenuService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _permissionMenuService.GetAll();
            return Ok(result);
        }
        [HttpGet("ID")]
        public async Task<IActionResult> GetByID(int ID)
        {
            var result = await _permissionMenuService.GetByID(ID);
            if (result.code != 200)
                return NotFound();
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> Create(PermissionMenuModel permissionMenuModel)
        {
            var result = await _permissionMenuService.Create(permissionMenuModel);
            return StatusCode(result.code, result);
        }
        [HttpPut("ID")]
        public async Task<IActionResult> Update(PermissionMenuModel permissionMenuModel)
        {
            var result = await _permissionMenuService.Update(permissionMenuModel.ID, permissionMenuModel);
            return StatusCode(result.code, result);
        }
        [HttpDelete("ID")]
        public async Task<IActionResult> Delete(int ID)
        {
            var result = await _permissionMenuService.Delete(ID);
            return StatusCode(result.code, result);
        }
    }
}
