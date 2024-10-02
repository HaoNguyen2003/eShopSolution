using System.ComponentModel.DataAnnotations;

namespace eShopSolution.DtoLayer.AddModel
{
    public class AddSize
    {
        [Required(ErrorMessage = "Please Input Size Name")]
        public string SizeName { get; set; }
    }
}
