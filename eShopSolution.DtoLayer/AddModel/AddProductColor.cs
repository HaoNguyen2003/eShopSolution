using System.ComponentModel.DataAnnotations;

namespace eShopSolution.DtoLayer.AddModel
{
    public class AddProductColor
    {
        [Required(ErrorMessage = "Product is Required")]
        public int ProductID { get; set; }
        [Required(ErrorMessage = "Color is Required")]
        public int ColorID { get; set; }
    }
}
