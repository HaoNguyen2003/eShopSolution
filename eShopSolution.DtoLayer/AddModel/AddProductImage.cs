using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace eShopSolution.DtoLayer.AddModel
{
    public class AddProductImage
    {
        [Required(ErrorMessage = "ProductColor is require")]
        public int ProductColorID { get; set; }
        [Required(ErrorMessage = "ImageURL is require")]
        public IFormFile Image { get; set; }
    }
}
