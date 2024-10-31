using AutoMapper;
using eShopSolution.BusinessLayer.Abstract;
using eShopSolution.DtoLayer.AddModel;
using eShopSolution.DtoLayer.Model;
using eShopSolution.DtoLayer.UpdateModel;
using eShopSolution.WebAPI.Permission;
using Microsoft.AspNetCore.Mvc;

namespace eShopSolution.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductColorController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IProductColorService _productColorService;
        public ProductColorController(IMapper mapper, IProductColorService productColorService)
        {
            _mapper = mapper;
            _productColorService = productColorService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllProductColor()
        {
            var result = await _productColorService.GetAll();
            return StatusCode(result.code, result.Value);
        }

        [HttpGet("{ID}")]
        [PermissionAuthorize(PermissionA.ProductColor + "." + AccessA.Get)]
        public async Task<IActionResult> GetProductColorByID(int ID)
        {
            var result = await _productColorService.GetByID(ID);
            return StatusCode(result.code, result.Value);
        }

        [HttpPost]
        [PermissionAuthorize(PermissionA.ProductColor + "." + AccessA.Create)]
        public async Task<IActionResult> AddProductColor([FromBody] AddProductColor addProductColor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var Model = _mapper.Map<ProductColorModel>(addProductColor);
            var result = await _productColorService.Create(Model);
            return StatusCode(result.code, result.Value);
        }

        [HttpPut("{ID}")]
        [PermissionAuthorize(PermissionA.ProductColor + "." + AccessA.Update)]
        public async Task<IActionResult> UpdateProductColor(int ID, [FromBody] UpdateProductColor updateProductColor)
        {
            var Model = _mapper.Map<ProductColorModel>(updateProductColor);
            Model.ID = ID;
            var result = await _productColorService.Update(ID, Model);
            return StatusCode(result.code, result.Value);
        }

        [HttpDelete("{ID}")]
        [PermissionAuthorize(PermissionA.ProductColor + "." + AccessA.Delete)]
        public async Task<IActionResult> DeleteProductColorByID(int ID)
        {
            var result = await _productColorService.Delete(ID);
            return StatusCode(result.code, result.Value);
        }
    }
}
