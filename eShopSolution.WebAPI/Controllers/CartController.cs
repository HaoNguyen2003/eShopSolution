using eShopSolution.DtoLayer.Model;
using Microsoft.AspNetCore.Mvc;

namespace eShopSolution.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        [HttpGet("GetCart")]
        public IActionResult GetCart(List<DetailCart> detailCarts)
        {
            return Ok();
        }
      
    }
}
