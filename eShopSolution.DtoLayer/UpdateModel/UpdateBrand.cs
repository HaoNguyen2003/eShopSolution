using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace eShopSolution.DtoLayer.UpdateModel
{
    public class UpdateBrand
    {
        [Required(ErrorMessage = "Please Input Brand Name")]
        public string BrandName { get; set; }
        public IFormFile? BrandImage { get; set; }
    }
}
