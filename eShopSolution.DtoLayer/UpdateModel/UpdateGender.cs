using System.ComponentModel.DataAnnotations;

namespace eShopSolution.DtoLayer.UpdateModel
{
    public class UpdateGender
    {
        [Required(ErrorMessage = "Please Input Gender Name")]
        public string Name { get; set; }
    }
}
