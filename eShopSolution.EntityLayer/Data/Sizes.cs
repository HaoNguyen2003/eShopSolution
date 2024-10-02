using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eShopSolution.EntityLayer.Data
{
    [Table("Sizes")]
    public class Sizes
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public virtual ICollection<ProductSizeInventory> ProductSizeInventories { get; set; }
    }
}
