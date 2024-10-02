using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.DtoLayer.AddModel
{
    public class AddAddressShipInfo
    {
        public int ID { get; set; }
        public string Country { get; set; } = "Việt Nam";
        [Required(ErrorMessage = "ProvinceID is required.")]
        public int ProvinceID { get; set; }
        [Required(ErrorMessage = "ProvinceName is required.")]
        public string ProvinceName { get; set; }
        [Required(ErrorMessage = "districtID is required.")]
        public int districtID { get; set; }
        [Required(ErrorMessage = "DistrictName is required.")]
        public string DistrictName { get; set; }
        [Required(ErrorMessage = "WardCode is required.")]
        public string WardCode { get; set; }
        [Required(ErrorMessage = "WardName is required.")]
        public string WardName { get; set; }
        [Required(ErrorMessage = "AddressInfo is required.")]
        public string AddressInfo { get; set; }
        [Required(ErrorMessage = "ConsigneeName is required.")]
        public string ConsigneeName { get; set; }
        [Required(ErrorMessage = "PhoneNumber is required.")]
        public string PhoneNumber { get; set; }
    }
}
