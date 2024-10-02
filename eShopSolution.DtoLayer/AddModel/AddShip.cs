using System.ComponentModel.DataAnnotations;

namespace eShopSolution.DtoLayer.AddModel
{
    public class AddShip
    {
        [Required(ErrorMessage = "Please Input ShippingProviders Name")]
        public string Name { get; set; }
    }
}
