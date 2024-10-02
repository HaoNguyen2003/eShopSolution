using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eShopSolution.EntityLayer.Data
{
    [Table("Order")]
    public class Order
    {
        [Key]
        public int OrderID { get; set; }
        public string UserID { get; set; }
        public int AddressID { get; set; }
        public int PaymentMethodID { get; set; }
        public int OrderStatusID { get; set; }
        public int ShippingProviderID { get; set; }
        public double FeeShip { get; set; } = 0;
        public double Subtotal { get; set; } = 0;
        public double Total { get; set; } = 0;
        public DateTime OrderDate { get; set; }
        public virtual AppUser AppUser { get; set; }
        public virtual Address Address { get; set; }
        public virtual PaymentMethod PaymentMethod { get; set; }
        public virtual OrderStatus OrderStatus { get; set; }
        public virtual ShippingProvider ShippingProvider { get; set; }
        public virtual ICollection<DetailOrder> DetailOrders { get; set; }
        public virtual ICollection<Return> Returns { get; set; }

    }
}
