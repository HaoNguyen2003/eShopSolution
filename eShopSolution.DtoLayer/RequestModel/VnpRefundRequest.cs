using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.DtoLayer.RequestModel
{
    public class VnpRefundRequest
    {
        public string vnp_TxnRef { get; set; } 
        public string refund_Amount { get; set; } 
        public string vnp_TransactionType { get; set; } = "03";
        public string vnp_TransactionDate { get; set; }
        public DateTime vnp_CreateBy { get; set; }=DateTime.Now;
    }
}
