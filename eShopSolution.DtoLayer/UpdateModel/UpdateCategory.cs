using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace eShopSolution.DtoLayer.UpdateModel
{
    public class UpdateCategory
    {
        [Required(ErrorMessage = "Please Input Brand Name")]
        public string CategoryName { get; set; }
        public IFormFile? CategoryImage { get; set; }
    }
}
