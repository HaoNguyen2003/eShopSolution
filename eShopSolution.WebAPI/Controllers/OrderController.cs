using AutoMapper;
using eShopSolution.BusinessLayer.Abstract;
using eShopSolution.BusinessLayer.Service;
using eShopSolution.DtoLayer.AddModel;
using eShopSolution.DtoLayer.Model;
using eShopSolution.DtoLayer.RequestModel;
using eShopSolution.PayMentService.Model;
using eShopSolution.PayMentService.Service;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading;

namespace eShopSolution.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IOrderService _orderService;
        private readonly IVnPayService _vnPayService;

        public OrderController(IMapper mapper,IOrderService orderService,IVnPayService vnPayService) {
            _mapper=mapper;
            _orderService=orderService;
            _vnPayService=vnPayService;
        }

        [Authorize(Roles = "Customer")]
        [HttpPost]
        public async Task<IActionResult>CreateOrder(AddOrderModel addOrderModel)
        {
            var orderModel = _mapper.Map<OrderModel>(addOrderModel);
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized(new { Error = "User not authorized" });
            }
            orderModel.UserID = userId;
            orderModel.ShippingProviderID = 1;
            var result = await _orderService.CreateOrder(orderModel);
            if (result.IsSuccess)
            {
                return Ok(_vnPayService.CreatePayMentUrl(HttpContext,userId, result.Value));
            }
            await _orderService.DeleteOrder(result.Value.OrderId);
            return BadRequest(result);
        }
        
    }
}
