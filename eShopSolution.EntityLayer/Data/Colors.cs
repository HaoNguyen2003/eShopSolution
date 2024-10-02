using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eShopSolution.EntityLayer.Data
{
    [Table("Colors")]
    public class Colors
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public string HexValue { get; set; }
        public virtual ICollection<ProductColors> ProductColors { get; set; }
    }
}
