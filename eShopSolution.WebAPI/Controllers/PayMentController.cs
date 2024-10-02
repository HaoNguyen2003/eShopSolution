using eShopSolution.BusinessLayer.Abstract;
using eShopSolution.DtoLayer.AddModel;
using eShopSolution.DtoLayer.Model;
using eShopSolution.DtoLayer.RepositoryModel;
using eShopSolution.DtoLayer.RequestModel;
using eShopSolution.PayMentService.Helper;
using eShopSolution.PayMentService.Model;
using eShopSolution.PayMentService.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using System.Security.Claims;

namespace eShopSolution.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PayMentController : ControllerBase
    {
        private readonly IVnPayService _vnPayService;
        private readonly ZaloPayService _zaloPayService;
        private readonly IOrderService _orderService;
        private readonly IInfoPaymentService _infoPaymentService;

        public PayMentController(IVnPayService vnPayService, ZaloPayService zaloPayService,IOrderService orderService, IInfoPaymentService infoPaymentService) {
            _vnPayService=vnPayService;
            _zaloPayService=zaloPayService;
            _orderService=orderService;
            _infoPaymentService=infoPaymentService;
        }
        [HttpGet("GetDataOfVnPay")]
        public async Task<IActionResult> GetDataOfVnPay(string UserID) {
            var response = _vnPayService.PaymentExecute(Request.Query);
            if (response.Success)
            {
                await _orderService.ConfirmPayMent(response.OrderId,UserID,2);
                var result = await  _infoPaymentService.Create(new InfoPaymentModel() {OrderID=response.OrderId,TxnRef=response.vnp_TxnRef,TransactionNo=response.TransactionId,UserCreateBy=response.UserTransactionDate});
                return Ok(new
                {
                    Message = "Payment successful",
                    Data = response
                });
            }
            else
            {
                return BadRequest(new
                {
                    Message = "Payment failed: your order will be held on the system for 15 minutes before being canceled",
                    Data = response
                });
            }
        }
        [HttpPost("PostDataOfVnPay")]
        [Authorize(Roles = "Customer")]
        public IActionResult PostDataOfVnPay(VnPaymentResquestModel vnPaymentResquestModel)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized(new { Error = "User not authorized" });
            }
            return Ok(_vnPayService.CreatePayMentUrl(HttpContext, userId, vnPaymentResquestModel));
        }

        [HttpPost]
        public IActionResult Querydr(string vnp_TxnRef, string vnp_TransactionDate)
        {
            return Ok(_vnPayService.vnpay_querydr(vnp_TxnRef, vnp_TransactionDate, HttpContext));
        }
        [HttpPost("Refund")]
        public async Task<IActionResult> Refund(int OrderID, double price)
        {
            InfoPaymentModel model = await _infoPaymentService.GetByOrderId(OrderID);
            if (model == null)
            {
                return BadRequest();
            }
            VnpQueryResponse vnpQueryResponse = _vnPayService.vnpay_querydr(model.TxnRef, model.UserCreateBy, HttpContext);
            if (vnpQueryResponse == null)
            {
                return BadRequest();
            }
            string refund_Amount = price.ToString();
            VnpayRefundResponse vnpayRefundResponse = _vnPayService.Refund(new VnpRefundRequest() { vnp_TxnRef = vnpQueryResponse.vnp_TxnRef, refund_Amount = refund_Amount, vnp_TransactionDate = vnpQueryResponse.vnp_PayDate }, HttpContext);
            return Ok(vnpayRefundResponse);
        }
        #region chưa làm
        [HttpPost("RenderQRCodeZaloPay")]
        public async Task<IActionResult> RenderQRCodeZaloPay(VnPaymentResquestModel vnPaymentResquest)
        {
            var result = await _zaloPayService.renderQRCodeAsync();
            return Ok(result);
        }


        [HttpPost("GetDataOfZaloPay")]
        public IActionResult GetDataOfZaloPay([FromBody] dynamic cbdata)
        {
            var result = new Dictionary<string, object>();

            try
            {
                var dataStr = Convert.ToString(cbdata["data"]);
                var reqMac = Convert.ToString(cbdata["mac"]);

                var mac = HmacHelper.Compute(ZaloPayHMAC.HMACSHA256, "eG4r0GcoNtRGbO8", dataStr);

                Console.WriteLine("mac = {0}", mac);
                if (!reqMac.Equals(mac))
                {
                    result["return_code"] = -1;
                    result["return_message"] = "mac not equal";
                }
                else
                {
                    var dataJson = JsonConvert.DeserializeObject<Dictionary<string, object>>(dataStr);
                    Console.WriteLine("update order's status = success where app_trans_id = {0}", dataJson["app_trans_id"]);

                    result["return_code"] = 1;
                    result["return_message"] = "success";
                }
            }
            catch (Exception ex)
            {
                result["return_code"] = 0;
                result["return_message"] = ex.Message;
            }
            return Ok(result);
        }
        #endregion
    }
}
