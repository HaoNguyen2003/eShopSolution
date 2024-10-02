using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eShopSolution.EntityLayer.Data
{
    [Table("AddressShipInfo")]
    public class Address
    {
        [Key]
        public int ID { get; set; }
        public string UserID { get; set; }
        public string Country { get; set; }
        public int ProvinceID { get; set; }
        public string ProvinceName {  get; set; }
        public int districtID { get; set; }
        public string DistrictName {  get; set; }
        public string WardCode { get; set; }
        public string WardName { get; set; }
        public string AddressInfo {  get; set; }
        public string ConsigneeName {  get; set; }
        public string PhoneNumber { get; set; }
        public virtual AppUser AppUser { get; set; }
        public virtual ICollection<Order> Orders { get; set; }

    }
}
