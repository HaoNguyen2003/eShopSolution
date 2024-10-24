using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eShopSolution.EntityLayer.Data
{
    [Table("Product")]
    public class Product
    {
        [Key]
        public int ID { get; set; }
        public int BrandID { get; set; }
        public int CategoryID { get; set; }
        public int GenderID { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        [Range(0.01, double.MaxValue, ErrorMessage = "PriceIn must be greater than 0.")]
        public decimal PriceIn { get; set; }
        [Range(0.01, double.MaxValue, ErrorMessage = "PriceOut must be greater than 0.")]
        public decimal PriceOut { get; set; }
        [Range(0, 99.99, ErrorMessage = "Discount must be less than 100.")]
        public decimal Discount { get; set; }
        public string Description { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public virtual Brand Brand { get; set; }
        public virtual Category Category { get; set; }
        public virtual Gender Gender { get; set; }
        public virtual ICollection<Comments> Comments { get; set; }
        public virtual ICollection<ProductColors> ProductColors { get; set; }
        public virtual ICollection<ProductReview> ProductReviews { get; set; }

    }
}
