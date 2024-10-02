using AutoMapper;
using eShopSolution.BusinessLayer.Abstract;
using eShopSolution.DtoLayer.AddModel;
using eShopSolution.DtoLayer.Model;
using eShopSolution.DtoLayer.UpdateModel;
using Microsoft.AspNetCore.Mvc;

namespace eShopSolution.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SizeController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ISizeService _sizeService;
        public SizeController(IMapper mapper, ISizeService sizeService)
        {
            _mapper = mapper;
            _sizeService = sizeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSize()
        {
            var result = await _sizeService.GetAll();
            return StatusCode(result.code, result.Value);
        }

        [HttpGet("{ID}")]
        public async Task<IActionResult> GetSizeByID(int ID)
        {
            var result = await _sizeService.GetByID(ID);
            return StatusCode(result.code, result.Value);
        }

        [HttpPost]
        public async Task<IActionResult> AddSize([FromBody] AddSize addSize)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var sizeModel = _mapper.Map<SizeModel>(addSize);
            var result = await _sizeService.Create(sizeModel);
            return StatusCode(result.code, result.Value);
        }

        [HttpPut("{ID}")]
        public async Task<IActionResult> UpdateSize(int ID, [FromBody] UpdateSize updateSize)
        {
            var sizeModel = _mapper.Map<SizeModel>(updateSize);
            sizeModel.ID = ID;
            var result = await _sizeService.Update(ID, sizeModel);
            return StatusCode(result.code, result.Value);
        }

        [HttpDelete("{ID}")]
        public async Task<IActionResult> DeleteSizeByID(int ID)
        {
            var result = await _sizeService.Delete(ID);
            return StatusCode(result.code, result.Value);
        }
    }
}
