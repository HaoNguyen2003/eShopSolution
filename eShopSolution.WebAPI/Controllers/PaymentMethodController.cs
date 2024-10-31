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
    public class PaymentMethodController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IPaymentService _paymentService;
        public PaymentMethodController(IMapper mapper, IPaymentService paymentService)
        {
            _mapper = mapper;
            _paymentService = paymentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPayMentMethod()
        {
            var result = await _paymentService.GetAll();
            return StatusCode(result.code, result.Value);
        }

        [HttpGet("{ID}")]
        [PermissionAuthorize(PermissionA.PaymentMethod + "." + AccessA.Get)]
        public async Task<IActionResult> GetPayMentMethodByID(int ID)
        {
            var result = await _paymentService.GetByID(ID);
            return StatusCode(result.code, result.Value);
        }

        [HttpPost]
        [PermissionAuthorize(PermissionA.PaymentMethod + "." + AccessA.Create)]
        public async Task<IActionResult> AddPaymentMethod([FromBody] AddPaymentMethod addPayment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var model = _mapper.Map<PaymentMethodModel>(addPayment);
            var result = await _paymentService.Create(model);
            return StatusCode(result.code, result.Value);
        }

        [HttpPut("{ID}")]
        [PermissionAuthorize(PermissionA.PaymentMethod + "." + AccessA.Update)]
        public async Task<IActionResult> UpdatePaymentMethod(int ID, [FromBody] UpdatePaymentMethod updatePayment)
        {
            var model = _mapper.Map<PaymentMethodModel>(updatePayment);
            model.ID = ID;
            var result = await _paymentService.Update(ID, model);
            return StatusCode(result.code, result.Value);
        }

        [HttpDelete("{ID}")]
        [PermissionAuthorize(PermissionA.PaymentMethod + "." + AccessA.Delete)]
        public async Task<IActionResult> DeletePaymentMethodByID(int ID)
        {
            var result = await _paymentService.Delete(ID);
            return StatusCode(result.code, result.Value);
        }
    }
}
