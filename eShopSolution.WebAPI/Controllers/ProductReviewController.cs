using AutoMapper;
using eShopSolution.BusinessLayer.Abstract;
using eShopSolution.BusinessLayer.Service;
using eShopSolution.DtoLayer.AddModel;
using eShopSolution.DtoLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace eShopSolution.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductReviewController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IProductReviewService _productReviewService;

        public ProductReviewController(IMapper mapper,IProductReviewService productReviewService)
        {
            _mapper = mapper;
            _productReviewService = productReviewService;
        }
        [HttpPost]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> CreateProductReview(AddProductReview addProductReview)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized(new { Error = "User not authorized" });
            }
            var ProductReviewModel = _mapper.Map<ProductReviewModel>(addProductReview);
            ProductReviewModel.UserID = userId;
            var result = await _productReviewService.CreateProductReview(ProductReviewModel);
            if (!result.IsSuccess)
                return BadRequest(result);
            return Ok(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetProductReviewPage(int ProductID,int Size, int Page = 1)
        {
            var result = await _productReviewService.GetProductReviewPage(ProductID, Page, Size);
            return Ok(result);
        }
        [HttpGet("ID")]
        public async Task<ActionResult>GetProductReview(int ID)
        {
            var result = await _productReviewService.GetByID(ID);
            return StatusCode(result.code, result);
        }
    }
}
