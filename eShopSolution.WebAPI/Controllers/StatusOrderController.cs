using AutoMapper;
using eShopSolution.BusinessLayer.Abstract;
using eShopSolution.DtoLayer.AddModel;
using eShopSolution.DtoLayer.Model;
using eShopSolution.DtoLayer.UpdateModel;
using eShopSolution.WebAPI.Permission;
using Microsoft.AspNetCore.Mvc;

namespace eShopSolution.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusOrderController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IOrderStatusService _orderStatusService;
        public StatusOrderController(IMapper mapper, IOrderStatusService orderStatusService)
        {
            _mapper = mapper;
            _orderStatusService = orderStatusService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOrderStatus()
        {
            var result = await _orderStatusService.GetAll();
            return StatusCode(result.code, result.Value);
        }

        [HttpGet("{ID}")]
        [PermissionAuthorize(PermissionA.StatusOrder + "." + AccessA.Get)]
        public async Task<IActionResult> GetStatusOrderByID(int ID)
        {
            var result = await _orderStatusService.GetByID(ID);
            return StatusCode(result.code, result.Value);
        }

        [HttpPost]
        [PermissionAuthorize(PermissionA.StatusOrder + "." + AccessA.Create)]
        public async Task<IActionResult> AddOrderStatus([FromBody] AddOrderStatus addOrderStatus)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var model = _mapper.Map<OrderStatusModel>(addOrderStatus);
            var result = await _orderStatusService.Create(model);
            return StatusCode(result.code, result.Value);
        }

        [HttpPut("{ID}")]
        [PermissionAuthorize(PermissionA.StatusOrder + "." + AccessA.Update)]
        public async Task<IActionResult> UpdateOrderStatus(int ID, [FromBody] UpdateOrderStatus updateOrderStatus)
        {
            var model = _mapper.Map<OrderStatusModel>(updateOrderStatus);
            model.ID = ID;
            var result = await _orderStatusService.Update(ID, model);
            return StatusCode(result.code, result.Value);
        }

        [HttpDelete("{ID}")]
        [PermissionAuthorize(PermissionA.StatusOrder + "." + AccessA.Delete)]
        public async Task<IActionResult> DeleteOrderStatusByID(int ID)
        {
            var result = await _orderStatusService.Delete(ID);
            return StatusCode(result.code, result.Value);
        }
    }
}
