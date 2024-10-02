using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.DtoLayer.Model
{
    public class RequestRefundModel
    {
        public string TxnRef {  get; set; }
        public string TransactionDate { get; set; }
        public double PriceRefund { get; set; }
        public string TransactionType { get; set; } = "02";


    }
}
