using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eShopSolution.EntityLayer.Data
{
    [Table("ShippingProvider")]
    public class ShippingProvider
    {
        [Key]
        public int ShippingProviderID { get; set; }
        public string ProviderName { get; set; }
        public virtual ICollection<Order> Orders { get; set; }

    }
}
