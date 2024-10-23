using eShopSolution.cloudinaryManagerFile.Abstract;
using eShopSolution.WebAPI.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace eShopSolution.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly ICloudinaryService _cloudinaryService;
        public TestController(ICloudinaryService cloudinaryService) { _cloudinaryService = cloudinaryService; }

        [HttpPost]
        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            if (!WorkWithFile.IsImage(file))
                return StatusCode(500, "Please select Image");
            var result = await _cloudinaryService.UploadFile(file, "ImageEshop/Test");
            return Ok(result);
        }

        [HttpPost("Delete")]
        public IActionResult Detele(string publicID)
        {
            var result = _cloudinaryService.RemoveFileAsync(publicID);
            return Ok(result);
        }
    }
}
