using System.ComponentModel.DataAnnotations;

namespace eShopSolution.DtoLayer.UpdateModel
{
    public class UpdateShip
    {
        [Required(ErrorMessage = "Please Input ShippingProviders Name")]
        public string Name { get; set; }
    }
}
