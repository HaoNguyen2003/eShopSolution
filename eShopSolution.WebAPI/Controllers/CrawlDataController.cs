using eShopSolution.CrawlData.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eShopSolution.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CrawlDataController : ControllerBase
    {
        private readonly DataInfomation _dataInfomation;

        public CrawlDataController(DataInfomation dataInfomation) {
            _dataInfomation = dataInfomation;
        }
        [HttpGet]
        public async Task<IActionResult> GetDataFromWeb(string url)
        {
            await _dataInfomation.FetchAndDisplayProductData(url); 
            return Ok();
        }
    }
}
