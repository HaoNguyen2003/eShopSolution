using eShopSolution.BusinessLayer.Abstract;
using eShopSolution.BusinessLayer.Service;
using eShopSolution.DtoLayer.AddModel;
using eShopSolution.DtoLayer.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eShopSolution.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IUserService _userService;

        public RoleController(IUserService userService) {
            _userService=userService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _userService.GetAllRolesAsync();
            return Ok(result);
        }
        [HttpGet("ID")]
        public async Task<IActionResult> GetByID(string? ID, string? Name)
        {
            var result = await _userService.GetRolesByIDOrNameAsync(ID,Name);
            if (!result.IsSuccess)
                return NotFound();
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> Create(AddRole role)
        {
            var result = await _userService.CreateRoleAsync(role);
            if (!result.IsSuccess)
                return BadRequest(result);
            return Ok(result);
        }
        [HttpPut("ID")]
        public async Task<IActionResult> Update(RoleModel roleModel)
        {
            var result = await _userService.UpdateRoleAync(roleModel);
            if (!result.IsSuccess)
                return BadRequest(result);
            return Ok(result);
        }
        [HttpDelete("ID")]
        public async Task<IActionResult> Delete(string ID)
        {
            var result = await _userService.DeletRole(ID);
            if (!result.IsSuccess)
                return BadRequest(result);
            return Ok(result);
        }
    }
}
