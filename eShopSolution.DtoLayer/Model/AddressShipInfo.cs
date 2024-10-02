using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.DtoLayer.Model
{
    public class AddressShipInfo
    {
        public int ID { get; set; }
        public string Country { get; set; }
        public string Province { get; set; }
        public string district { get; set; }
        public string Ward { get; set; }
        public string AddressInfo { get; set; }
        public string ConsigneeName { get; set; }
        public string PhoneNumber { get; set; }
    }
}
