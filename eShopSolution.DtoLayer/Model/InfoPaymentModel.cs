using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.DtoLayer.Model
{
    public class InfoPaymentModel
    {
        public int ID { get; set; }
        public int OrderID { get; set; }
        public string TxnRef { get; set; }
        public string TransactionNo { get; set; }
        public string UserCreateBy { get; set; }
    }
}
