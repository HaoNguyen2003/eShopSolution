using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eShopSolution.EntityLayer.Data
{
    [Table("CategoryAndBrand")]
    public class CategoryAndBrand
    {
        [Key]
        public int ID { get; set; }
        public int BrandID { get; set; }
        public int CategoryID { get; set; }
        public virtual Category Category { get; set; }
        public virtual Brand Brand { get; set; }
    }
}
