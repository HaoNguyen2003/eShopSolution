using System.ComponentModel.DataAnnotations;

namespace eShopSolution.DtoLayer.AddModel
{
    public class AddGender
    {
        [Required(ErrorMessage = "Please Input Gender Name")]
        public string Name { get; set; }
    }
}
