using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eShopSolution.EntityLayer.Data
{
    [Table("ProductColors")]
    public class ProductColors
    {
        [Key]
        public int ID { get; set; }
        public int ColorID { get; set; }
        public int ProductID { get; set; }
        public virtual Product Product { get; set; }
        public virtual Colors Color { get; set; }
        public virtual ICollection<ProductImages> ProductImages { get; set; }
        public virtual ICollection<ProductSizeInventory> ProductSizeInventories { get; set; }
    }
}
