using System.ComponentModel.DataAnnotations;

namespace eShopSolution.DtoLayer.UpdateModel
{
    public class UpdateProduct
    {
        [Required(ErrorMessage = "BrandID is required.")]
        public int BrandID { get; set; }

        [Required(ErrorMessage = "CategoryID is required.")]
        public int CategoryID { get; set; }

        [Required(ErrorMessage = "GenderID is required.")]
        public int GenderID { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Title is required.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "PriceIn is required.")]
        public decimal PriceIn { get; set; }

        [Required(ErrorMessage = "PriceOut is required.")]
        public decimal PriceOut { get; set; }

        [Range(0, 100, ErrorMessage = "Discount must be between 0 and 100.")]
        public decimal Discount { get; set; }

        [StringLength(1000, ErrorMessage = "Description cannot exceed 1000 characters.")]
        public string Description { get; set; }
    }
}
