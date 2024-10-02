using eShopSolution.DtoLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.DtoLayer.AddModel
{
    public class AddOrderModel
    {
        public int AddressID { get; set; }
        public int PaymentMethodID { get; set; }
        public List<DetailCart>detailCarts { get; set; } = new List<DetailCart>();
    }
}
