using System.ComponentModel.DataAnnotations;

namespace eShopSolution.DtoLayer.UpdateModel
{
    public class UpdateCategoryAndBrand
    {
        [Required(ErrorMessage = "Please Select BrandID")]
        public int BrandID { get; set; }
        [Required(ErrorMessage = "Please Select Category")]
        public int CategoryID { get; set; }
    }
}
