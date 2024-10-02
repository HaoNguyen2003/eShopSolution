using System.ComponentModel.DataAnnotations;

namespace eShopSolution.DtoLayer.AddModel
{
    public class AddCategoryAndBrand
    {
        [Required(ErrorMessage = "Please Select BrandID")]
        public int BrandID { get; set; }
        [Required(ErrorMessage = "Please Select Category")]
        public int CategoryID { get; set; }
    }
}
