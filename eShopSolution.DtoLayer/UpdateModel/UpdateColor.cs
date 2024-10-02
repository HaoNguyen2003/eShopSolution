using System.ComponentModel.DataAnnotations;

namespace eShopSolution.DtoLayer.UpdateModel
{
    public class UpdateColor
    {
        [Required(ErrorMessage = "Input Name Color")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Input Hexvalue")]
        public string HexValue { get; set; }
    }
}
