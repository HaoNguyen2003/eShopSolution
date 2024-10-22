using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eShopSolution.EntityLayer.Data
{
    [Table("ProductColors")]
    public class ProductColors
    {
        [Key]
        public int ID { get; set; }
        public int ProductID { get; set; }
        public int ColorCombinationID {  get; set; }
        public virtual Product Product { get; set; }
        public virtual ColorCombination ColorCombination { get; set; }
        public virtual ICollection<ProductImages> ProductImages { get; set; }
        public virtual ICollection<ProductSizeInventory> ProductSizeInventories { get; set; }
    }
}
