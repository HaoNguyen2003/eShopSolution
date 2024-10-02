using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.DtoLayer.RequestModel
{
    public class VnPaymentResquestModel
    {
        public int OrderId { get; set; }
        public string FullName { get; set; }
        public string Description { get; set; }
        public double totalPrice { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
    }
}
