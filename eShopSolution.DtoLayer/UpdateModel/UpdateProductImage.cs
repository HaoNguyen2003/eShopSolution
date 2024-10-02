using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace eShopSolution.DtoLayer.UpdateModel
{
    public class UpdateProductImage
    {
        [Required(ErrorMessage = "ProductColor is require")]
        public int ProductColorID { get; set; }
        public IFormFile? Image { get; set; }
    }
}
