using System.ComponentModel.DataAnnotations;

namespace eShopSolution.DtoLayer.UpdateModel
{
    public class UpdateSize
    {
        [Required(ErrorMessage = "Please Input Size Name")]
        public string SizeName { get; set; }
    }
}
