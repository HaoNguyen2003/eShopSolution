using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.DtoLayer.Configuration
{
    public class ShippingProvidersConfiguration
    {
        public string TokenGHN {  get; set; }
        public string Appkey { get; set; }
        public string Provide { get; set; }
        public string District { get; set; }
        public string WardCode { get; set; }
    }
}
