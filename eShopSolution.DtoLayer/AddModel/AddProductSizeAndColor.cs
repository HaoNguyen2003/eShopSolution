using System.ComponentModel.DataAnnotations;

namespace eShopSolution.DtoLayer.AddModel
{
    public class AddProductSizeAndColor
    {
        [Required(ErrorMessage = "ProductColor is require")]
        public int ProductColorID { get; set; }
        [Required(ErrorMessage = "Size is require")]
        public int SizeID { get; set; }
        [Required(ErrorMessage = "Quantity is require")]
        public int Quantity { get; set; }
    }
}
