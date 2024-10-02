using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace eShopSolution.DtoLayer.AddModel
{
    public class AddBrand
    {
        [Required(ErrorMessage = "Please Input Brand Name")]
        public string BrandName { get; set; }
        [Required(ErrorMessage = "Please Select Brand Image")]
        public IFormFile BrandImage { get; set; }
    }
}
