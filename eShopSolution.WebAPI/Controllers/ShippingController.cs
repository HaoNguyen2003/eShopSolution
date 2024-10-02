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
    public class ShippingController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IShippingProvidersService _shippingProvidersService;
        public ShippingController(IMapper mapper, IShippingProvidersService shippingProvidersService)
        {
            _mapper = mapper;
            _shippingProvidersService = shippingProvidersService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllShippingProviders()
        {
            var result = await _shippingProvidersService.GetAll();
            return StatusCode(result.code, result.Value);
        }

        [HttpGet("{ID}")]
        public async Task<IActionResult> GetShippingProvidersByID(int ID)
        {
            var result = await _shippingProvidersService.GetByID(ID);
            return StatusCode(result.code, result.Value);
        }

        [HttpPost]
        public async Task<IActionResult> AddShippingProviders([FromBody] AddShip addShip)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var model = _mapper.Map<ShippingProvidersModel>(addShip);
            var result = await _shippingProvidersService.Create(model);
            return StatusCode(result.code, result.Value);
        }

        [HttpPut("{ID}")]
        public async Task<IActionResult> UpdateShippingProviders(int ID, [FromBody] UpdateShip updateShip)
        {
            var model = _mapper.Map<ShippingProvidersModel>(updateShip);
            model.ID = ID;
            var result = await _shippingProvidersService.Update(ID, model);
            return StatusCode(result.code, result.Value);
        }

        [HttpDelete("{ID}")]
        public async Task<IActionResult> DeleteOrderStatusByID(int ID)
        {
            var result = await _shippingProvidersService.Delete(ID);
            return StatusCode(result.code, result.Value);
        }
    }
}
