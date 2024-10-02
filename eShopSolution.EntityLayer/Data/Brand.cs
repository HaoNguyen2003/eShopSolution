using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eShopSolution.EntityLayer.Data
{
    [Table("Brand")]
    public class Brand
    {
        [Key]
        public int BrandID { get; set; }
        public string BrandName { get; set; }
        public string ImageURL { get; set; }
        public string? PublicID { get; set; }
        public virtual ICollection<CategoryAndBrand> CategoryAndBrands { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
