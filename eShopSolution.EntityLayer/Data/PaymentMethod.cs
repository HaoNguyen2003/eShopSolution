using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eShopSolution.EntityLayer.Data
{
    [Table("PaymentMethod")]
    public class PaymentMethod
    {
        [Key]
        public int PaymentMethodID { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Order> Orders { get; set; }

    }
}
