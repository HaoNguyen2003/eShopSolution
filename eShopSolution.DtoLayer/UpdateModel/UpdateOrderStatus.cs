using System.ComponentModel.DataAnnotations;

namespace eShopSolution.DtoLayer.UpdateModel
{
    public class UpdateOrderStatus
    {
        [Required(ErrorMessage = "Please Input OrderStatus")]
        public string Name { get; set; }
    }
}
