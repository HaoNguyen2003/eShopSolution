using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace eShopSolution.DtoLayer.AddModel
{

    public class AddCollection
    {
        [Required(ErrorMessage = "ColorID is required.")]
        public int ColorID { get; set; }

        [Required(ErrorMessage = "DetailQuantity is required.")]
        public List<AddDetailQuantityProduct> DetailQuantity { get; set; }
        [Required(ErrorMessage = "ListImage is required.")]
        public List<IFormFile> ListImage { get; set; }
    }
}
