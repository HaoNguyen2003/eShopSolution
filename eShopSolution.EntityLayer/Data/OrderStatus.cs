using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eShopSolution.EntityLayer.Data
{
    [Table("OrderStatus")]
    public class OrderStatus
    {
        [Key]
        public int OrderStatusID { get; set; }
        public string StatusName { get; set; }
        public virtual ICollection<Order> Orders { get; set; }

    }
}
