using eShopSolution.BusinessLayer.Abstract;
using eShopSolution.BusinessLayer.Service;
using eShopSolution.DtoLayer.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eShopSolution.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private readonly IMenuService _menuService;

        public MenuController(IMenuService menuService) {
            _menuService = menuService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _menuService.GetAll();
            return Ok(result);
        }
        [HttpGet("ID")]
        public async Task<IActionResult> GetByID(int ID)
        {
            var result = await _menuService.GetByID(ID);
            if (result.code != 200)
                return NotFound();
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> Create(MenuModel menuModel)
        {
            var result = await _menuService.Create(menuModel);
            return StatusCode(result.code, result);
        }
        [HttpPut("ID")]
        public async Task<IActionResult> Update(MenuModel menuModel)
        {
            var result = await _menuService.Update(menuModel.ID, menuModel);
            return StatusCode(result.code, result);
        }
        [HttpDelete("ID")]
        public async Task<IActionResult> Delete(int ID)
        {
            var result = await _menuService.Delete(ID);
            return StatusCode(result.code, result);
        }
    }
}
