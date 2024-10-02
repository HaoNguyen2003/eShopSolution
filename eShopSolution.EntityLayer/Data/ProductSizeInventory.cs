using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eShopSolution.EntityLayer.Data
{
    [Table("ProductSizeInventory")]
    public class ProductSizeInventory
    {
        [Key]
        public int ProductSizeInventoryID { get; set; }
        public int ProductColorID { get; set; }
        public int SizeID { get; set; }
        public int Quantity { get; set; }
        public virtual ProductColors ProductColor { get; set; }
        public virtual Sizes Size { get; set; }
        public virtual ICollection<DetailOrder> DetailOrders { get; set; }

    }
}
