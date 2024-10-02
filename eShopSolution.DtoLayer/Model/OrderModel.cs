using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.DtoLayer.Model
{
    public class OrderModel
    {
        public int OrderID { get; set; }
        public string UserID {  get; set; }
        public int AddressID { get; set; }
        public int PaymentMethodID { get; set; }
        public int? OrderStatusID { get; set; } = 1;
        public int ShippingProviderID { get; set; }
        public List<DetailCart> detailCarts { get; set; } = new List<DetailCart>();

    }
}
