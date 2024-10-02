using System.ComponentModel.DataAnnotations;

namespace eShopSolution.DtoLayer.AddModel
{
    public class AddPaymentMethod
    {
        [Required(ErrorMessage = "Please Input Name Payment")]
        public string Name { get; set; }
    }
}
