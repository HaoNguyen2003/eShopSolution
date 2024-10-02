using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eShopSolution.EntityLayer.Data
{
    [Table("ProductImages")]
    public class ProductImages
    {
        [Key]
        public int ID { get; set; }
        public int ProductColorID { get; set; }
        public string ImageURL { get; set; }
        public string PublicID { get; set; }
        public virtual ProductColors ProductColors { get; set; }
    }
}
