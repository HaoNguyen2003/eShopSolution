using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eShopSolution.EntityLayer.Data
{
    [Table("ProductReview")]
    public class ProductReview
    {
        [Key]
        public int ReviewID { get; set; }
        public string UserID { get; set; }
        public int ProductID { get; set; }
        public int DetailOrderID { get; set; }
        public DateTime ReviewDate { get; set; }
        public int Rating { get; set; }
        public string Review { get; set; } = string.Empty;
        public virtual AppUser AppUser { get; set; }
        public virtual Product Product { get; set; }
        public virtual DetailOrder DetailOrder { get; set; }


    }
}
