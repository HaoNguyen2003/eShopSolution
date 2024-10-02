using System.ComponentModel.DataAnnotations;

namespace eShopSolution.DtoLayer.AddModel
{
    public class AddOrderStatus
    {
        [Required(ErrorMessage = "Please Input OrderStatus")]
        public string Name { get; set; }
    }
}
