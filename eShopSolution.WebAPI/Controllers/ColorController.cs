using AutoMapper;
using eShopSolution.BusinessLayer.Abstract;
using eShopSolution.DtoLayer.AddModel;
using eShopSolution.DtoLayer.Model;
using eShopSolution.DtoLayer.UpdateModel;
using eShopSolution.WebAPI.Permission;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNetCore.Mvc;

namespace eShopSolution.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ColorController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IColorService _colorService;
        public ColorController(IMapper mapper, IColorService colorService)
        {
            _mapper = mapper;
            _colorService = colorService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllColor()
        {
            var result = await _colorService.GetAll();
            return StatusCode(result.code, result.Value);
        }

        [HttpGet("{ID}")]
        public async Task<IActionResult> GetColorByID(int ID)
        {
            var result = await _colorService.GetByID(ID);
            return StatusCode(result.code, result.Value);
        }

        [HttpPost]
        //[PermissionAuthorize("Color.Create")]
        public async Task<IActionResult> AddColor([FromBody] AddColor addColor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var colorModel = _mapper.Map<ColorModel>(addColor);
            var result = await _colorService.Create(colorModel);
            return StatusCode(result.code, result.Value);
        }

        [HttpPut("{ID}")]
        public async Task<IActionResult> UpdateColor(int ID, [FromBody] UpdateColor updateColor)
        {
            var colorModel = _mapper.Map<ColorModel>(updateColor);
            colorModel.ID = ID;
            var result = await _colorService.Update(ID, colorModel);
            return StatusCode(result.code, result.Value);
        }

        [HttpDelete("{ID}")]
        public async Task<IActionResult> DeleteColorByID(int ID)
        {
            var result = await _colorService.Delete(ID);
            return StatusCode(result.code, result.Value);
        }
    }
}
