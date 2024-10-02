using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.DtoLayer.Model
{
    public class DetailOrderModel
    {
        public int ID { get; set; }
        public int ProductSizeInventoryID { get; set; }
        public int OrderID { get; set; }
        public int Amount { get; set; }
        public double Price { get; set; }
        public double TotalPrice { get; set; }
    }
}
