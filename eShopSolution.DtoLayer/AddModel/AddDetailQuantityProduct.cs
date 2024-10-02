using System.ComponentModel.DataAnnotations;

namespace eShopSolution.DtoLayer.AddModel
{
    public class AddDetailQuantityProduct
    {
        [Required(ErrorMessage = "Size is required.")]
        public int SizeID { get; set; }
        [Required(ErrorMessage = "Quantity is required.")]
        public int Quantity { get; set; }
    }
}
