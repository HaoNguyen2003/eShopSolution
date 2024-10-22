using System.ComponentModel.DataAnnotations;

namespace eShopSolution.DtoLayer.UpdateModel
{
    public class UpdateProductColor
    {
        [Required(ErrorMessage = "Product is Required")]
        public int ProductID { get; set; }
        [Required(ErrorMessage = "Color is Required")]
        public int ColorCombinationID { get; set; }
    }
}
