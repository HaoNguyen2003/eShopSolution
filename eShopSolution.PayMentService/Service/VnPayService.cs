using Azure;
using eShopSolution.DtoLayer.Model;
using eShopSolution.DtoLayer.RepositoryModel;
using eShopSolution.DtoLayer.RequestModel;
using eShopSolution.PayMentService.Config;
using eShopSolution.PayMentService.Helper;
using eShopSolution.PayMentService.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Protocols;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.PayMentService.Service
{
    public class VnPayService : IVnPayService
    {
        private readonly VnPayConfig _VnPayConfig;
        public VnPayService(IOptions<VnPayConfig> options)
        {
            _VnPayConfig = options.Value;
        }
        public string CreatePayMentUrl(HttpContext context,string UserID,VnPaymentResquestModel model)
        {
            var tick = DateTime.Now.Ticks.ToString();

            var vnpay = new VnPayLibrary();
            vnpay.AddRequestData("vnp_Version", _VnPayConfig.Version);
            vnpay.AddRequestData("vnp_Command", _VnPayConfig.Command);
            vnpay.AddRequestData("vnp_TmnCode", _VnPayConfig.vnp_TmnCode);
            vnpay.AddRequestData("vnp_Amount", (model.totalPrice * 100).ToString());

            vnpay.AddRequestData("vnp_CreateDate", model.CreateDate.ToString("yyyyMMddHHmmss"));
            vnpay.AddRequestData("vnp_CurrCode", _VnPayConfig.CurrCode);
            vnpay.AddRequestData("vnp_IpAddr", common.GetIpAddress(context));
            vnpay.AddRequestData("vnp_Locale", _VnPayConfig.Locale);
            vnpay.AddRequestData("vnp_OrderInfo", "" + model.OrderId);
            vnpay.AddRequestData("vnp_OrderType", "other");
            vnpay.AddRequestData("vnp_ReturnUrl", _VnPayConfig.vnp_Returnurl+$"?UserID={UserID}");
            vnpay.AddRequestData("vnp_TxnRef", tick);
            var paymentUrl = vnpay.CreateRequestUrl(_VnPayConfig.vnp_Url, _VnPayConfig.vnp_HashSecret);
            return paymentUrl;
        }

        public VnPaymentResponseModel PaymentExecute(IQueryCollection collection)
        {
            var vnpay = new VnPayLibrary();
            foreach (var (key, value) in collection)
            {
                if (!string.IsNullOrEmpty(key) && key.StartsWith("vnp_"))
                {
                    vnpay.AddResponseData(key, value.ToString());
                }
            }
            var vnp_TxnRef = Convert.ToInt64(vnpay.GetResponseData("vnp_TxnRef"));
            var vnp_Transactionid = Convert.ToInt64(vnpay.GetResponseData("vnp_TransactionNo"));
            var vnp_SecureHash = collection.FirstOrDefault(p => p.Key == "vnp_SecureHash").Value;
            var vnp_ResponseCode = vnpay.GetResponseData("vnp_ResponseCode");
            var vnp_OrderInfo = vnpay.GetResponseData("vnp_OrderInfo");
            var vnp_CreateDate = vnpay.GetResponseData("vnp_PayDate");
            bool checkSignature = vnpay.ValidateSignature(vnp_SecureHash, _VnPayConfig.vnp_HashSecret);
            if (!checkSignature)
            {
                return new VnPaymentResponseModel
                {
                    Success = false
                };

            }
            return new VnPaymentResponseModel
            {
                Success = true,
                PaymentMethod = "VnPay",
                OrderDescription = "Đơn hàng: " + vnp_OrderInfo,
                vnp_TxnRef = vnp_TxnRef.ToString(),
                TransactionId = vnp_Transactionid.ToString(),
                OrderId = Int32.Parse(vnp_OrderInfo),
                Token = vnp_SecureHash,
                UserTransactionDate = vnp_CreateDate,
                VnPayResponseCode = vnp_ResponseCode.ToString(),
            };
        }

        public VnpayRefundResponse Refund(VnpRefundRequest vnpRefundRequest, HttpContext httpContext)
        {
            var vnp_Api = _VnPayConfig.vnp_Api;
            var vnp_HashSecret = _VnPayConfig.vnp_HashSecret;
            var vnp_TmnCode = _VnPayConfig.vnp_TmnCode;

            var vnp_RequestId = DateTime.Now.Ticks.ToString();
            var vnp_Version = _VnPayConfig.Version;
            var vnp_Command = "refund";
            var vnp_TransactionType = vnpRefundRequest.vnp_TransactionType;
            var vnp_Amount = Convert.ToInt64(vnpRefundRequest.refund_Amount) * 100;
            var vnp_TxnRef = vnpRefundRequest.vnp_TxnRef;
            var vnp_OrderInfo = "Hoan tien giao dich:" + vnp_TxnRef;
            var vnp_TransactionNo = "";
            var vnp_TransactionDate = vnpRefundRequest.vnp_TransactionDate;
            var vnp_CreateDate = DateTime.Now.ToString("yyyyMMddHHmmss");
            var vnp_CreateBy = vnpRefundRequest.vnp_CreateBy.ToString("yyyyMMddHHmmss");
            var vnp_IpAddr = common.GetIpAddress(httpContext);

            var signData = vnp_RequestId + "|" + vnp_Version + "|" + vnp_Command + "|" + vnp_TmnCode + "|" + vnp_TransactionType + "|" + vnp_TxnRef + "|" + vnp_Amount + "|" + vnp_TransactionNo + "|" + vnp_TransactionDate + "|" + vnp_CreateBy + "|" + vnp_CreateDate + "|" + vnp_IpAddr + "|" + vnp_OrderInfo;
            var vnp_SecureHash = common.HmacSHA512(vnp_HashSecret, signData);

            var rfData = new
            {
                vnp_RequestId = vnp_RequestId,
                vnp_Version = vnp_Version,
                vnp_Command = vnp_Command,
                vnp_TmnCode = vnp_TmnCode,
                vnp_TransactionType = vnp_TransactionType,
                vnp_TxnRef = vnp_TxnRef,
                vnp_Amount = vnp_Amount,
                vnp_OrderInfo = vnp_OrderInfo,
                vnp_TransactionNo = vnp_TransactionNo,
                vnp_TransactionDate = vnp_TransactionDate,
                vnp_CreateBy = vnp_CreateBy,
                vnp_CreateDate = vnp_CreateDate,
                vnp_IpAddr = vnp_IpAddr,
                vnp_SecureHash = vnp_SecureHash

            };
            var jsonData = JsonConvert.SerializeObject(rfData);
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(vnp_Api);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                streamWriter.Write(jsonData);
            }
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            var strData = "";
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                strData = streamReader.ReadToEnd();
                VnpayRefundResponse vnpayRefundResponse = JsonConvert.DeserializeObject<VnpayRefundResponse>(strData);
                return vnpayRefundResponse;
            }
        }


        public VnpQueryResponse vnpay_querydr(string TxnRef, string TransactionDate, HttpContext context)
        {
            var vnp_RequestId = DateTime.Now.Ticks.ToString(); 
            var vnp_Version = _VnPayConfig.Version; 
            var vnp_Command = "querydr";
            var vnp_TxnRef = TxnRef; 
            var vnp_OrderInfo = "Truy van giao dich:" + TxnRef;
            var vnp_TransactionDate = TransactionDate;
            var vnp_CreateDate = DateTime.Now.ToString("yyyyMMddHHmmss");
            var vnp_IpAddr = common.GetIpAddress(context);

            var signData = vnp_RequestId + "|" + vnp_Version + "|" + vnp_Command + "|" + _VnPayConfig.vnp_TmnCode + "|" + vnp_TxnRef + "|" + vnp_TransactionDate + "|" + vnp_CreateDate + "|" + vnp_IpAddr + "|" + vnp_OrderInfo;
            var vnp_SecureHash = common.HmacSHA512(_VnPayConfig.vnp_HashSecret, signData);

            var qdrData = new
            {
                vnp_RequestId = vnp_RequestId,
                vnp_Version = vnp_Version,
                vnp_Command = vnp_Command,
                vnp_TmnCode = _VnPayConfig.vnp_TmnCode,
                vnp_TxnRef = vnp_TxnRef,
                vnp_OrderInfo = vnp_OrderInfo,
                vnp_TransactionDate = vnp_TransactionDate,
                vnp_CreateDate = vnp_CreateDate,
                vnp_IpAddr = vnp_IpAddr,
                vnp_SecureHash = vnp_SecureHash

            };
            var jsonData = JsonConvert.SerializeObject(qdrData);
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(_VnPayConfig.vnp_Api);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                streamWriter.Write(jsonData);
            }
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            var strData = "";
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                strData = streamReader.ReadToEnd();
                VnpQueryResponse response = JsonConvert.DeserializeObject<VnpQueryResponse>(strData);
                Console.WriteLine(response);
                return response;
            }
        }


    }
}
