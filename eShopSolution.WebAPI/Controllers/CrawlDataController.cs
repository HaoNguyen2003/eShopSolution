using eShopSolution.CrawlData.Model;
using eShopSolution.CrawlData.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eShopSolution.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CrawlDataController : ControllerBase
    {
        private readonly ReadFileJson _readFileJson;

        public CrawlDataController(ReadFileJson readFileJson) {
            _readFileJson = readFileJson;
        }
        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> GetDataFromFile(IFormFile file)
        {
            _readFileJson.FucntionReadFileJson(file);
            return Ok();
        }

    }
}
