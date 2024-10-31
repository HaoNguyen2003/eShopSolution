using AutoMapper;
using eShopSolution.BusinessLayer.Abstract;
using eShopSolution.DtoLayer.AddModel;
using eShopSolution.DtoLayer.Model;
using eShopSolution.WebAPI.Permission;
using Microsoft.AspNetCore.Mvc;

namespace eShopSolution.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductSizeInventoryController : ControllerBase
    {
        public IMapper _mapper;
        public IProductSizeInventoryService _productSizeInventoryService;
        public ProductSizeInventoryController(IMapper mapper, IProductSizeInventoryService productSizeInventoryService)
        {
            _mapper = mapper;
            _productSizeInventoryService = productSizeInventoryService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _productSizeInventoryService.GetAll();
            return StatusCode(result.code, result.Value);
        }
        [HttpGet("{ID}")]
        [PermissionAuthorize(PermissionA.ProductSizeInventory + "." + AccessA.Get)]
        public async Task<IActionResult> GetByID(int ID)
        {
            var result = await _productSizeInventoryService.GetByID(ID);
            return StatusCode(result.code, result.Value);
        }
        [HttpPost]
        [PermissionAuthorize(PermissionA.ProductSizeInventory + "." + AccessA.Create)]
        public async Task<IActionResult> Create(AddProductSizeAndColor addProductSizeAndColor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var model = _mapper.Map<DetailQuantityProductModel>(addProductSizeAndColor);
            var result = await _productSizeInventoryService.Create(model);
            return StatusCode(result.code, result.Value);
        }
        [HttpPut]
        [PermissionAuthorize(PermissionA.ProductSizeInventory + "." + AccessA.Update)]
        public async Task<IActionResult> Update(AddProductSizeAndColor addProductSizeAndColor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var model = _mapper.Map<DetailQuantityProductModel>(addProductSizeAndColor);
            var result = await _productSizeInventoryService.Update(0, model);
            return StatusCode(result.code, result.Value);
        }
        [HttpDelete("DeleteProductSizeAndColor/{ProductColorID}/{SizeID}")]
        [PermissionAuthorize(PermissionA.ProductSizeInventory + "." + AccessA.Delete)]
        public async Task<IActionResult> Delete(int ProductColorID, int SizeID)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _productSizeInventoryService.DeleteProductSizeInventoryService(ProductColorID, SizeID);
            return StatusCode(result.code, result.Value);
        }
    }
}
