using System.ComponentModel.DataAnnotations;

namespace eShopSolution.DtoLayer.AddModel
{
    public class AddColor
    {
        [Required(ErrorMessage = "Input Name Color")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Input Hexvalue")]
        public string HexValue { get; set; }
    }
}
