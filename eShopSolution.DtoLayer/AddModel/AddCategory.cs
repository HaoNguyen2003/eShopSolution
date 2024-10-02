using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace eShopSolution.DtoLayer.AddModel
{
    public class AddCategory
    {
        [Required(ErrorMessage = "Please Input Brand Name")]
        public string CategoryName { get; set; }
        [Required(ErrorMessage = "Please Select Brand Image")]
        public IFormFile CategoryImage { get; set; }
    }
}
