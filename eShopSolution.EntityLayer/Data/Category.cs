using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eShopSolution.EntityLayer.Data
{
    [Table("Category")]
    public class Category
    {
        [Key]
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public string ImageURL { get; set; }
        public string? PublicID { get; set; }
        public virtual ICollection<CategoryAndBrand> CategoryAndBrands { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
