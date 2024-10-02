using eShopSolution.DtoLayer.Model;
using eShopSolution.DtoLayer.RepositoryModel;
using eShopSolution.DtoLayer.RequestModel;
using eShopSolution.PayMentService.Model;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.PayMentService.Service
{
    public interface IVnPayService
    {
        public string CreatePayMentUrl(HttpContext context, string UserID, VnPaymentResquestModel model);
        public VnPaymentResponseModel PaymentExecute(IQueryCollection collection);

        public VnpQueryResponse vnpay_querydr(string TxnRef, string TransactionDate,HttpContext context);
        public VnpayRefundResponse Refund(VnpRefundRequest vnpRefundRequest,HttpContext httpContext);
    }
}
