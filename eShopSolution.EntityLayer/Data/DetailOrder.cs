using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eShopSolution.EntityLayer.Data
{
    [Table("DetailOrder")]
    public class DetailOrder
    {
        [Key]
        public int ID { get; set; }
        public int ProductSizeInventoryID { get; set; }
        public int OrderID { get; set; }
        public int Amount { get; set; }
        public double Price { get; set; }
        public double TotalPrice { get; set; }
        public virtual ProductSizeInventory ProductSizeInventory { get; set; }
        public virtual ICollection<ReturnDetail> ReturnDetails { get; set; }
        public virtual ICollection<ProductReview> ProductReviews { get; set; }
        public virtual Order Order { get; set; }
    }
}
