using System.ComponentModel.DataAnnotations;

namespace eShopSolution.DtoLayer.UpdateModel
{
    public class UpdatePaymentMethod
    {
        [Required(ErrorMessage = "Please Input Name Payment")]
        public string Name { get; set; }
    }
}
