using AutoMapper;
using eShopSolution.BusinessLayer.Abstract;
using eShopSolution.cloudinaryManagerFile.Abstract;
using eShopSolution.DtoLayer.AddModel;
using eShopSolution.DtoLayer.Model;
using eShopSolution.DtoLayer.UpdateModel;
using eShopSolution.WebAPI.Helpers;
using eShopSolution.WebAPI.Permission;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eShopSolution.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IBrandService _brandService;
        private readonly ICloudinaryService _cloudinaryService;
        public BrandController(IMapper mapper, IBrandService brandService, ICloudinaryService cloudinaryService)
        {
            _mapper = mapper;
            _brandService = brandService;
            _cloudinaryService = cloudinaryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBrand()
        {
            var result = await _brandService.GetAll();
            return StatusCode(result.code, result.Value);
        }

        [HttpGet("{ID}")]
        public async Task<IActionResult> GetBrandByID(int ID)
        {
            var result = await _brandService.GetByID(ID);
            return StatusCode(result.code, result.Value);
        }
        [HttpPost]
        [Consumes("multipart/form-data")]
        [PermissionAuthorize(PermissionA.Brand+"."+AccessA.Create)]
        public async Task<IActionResult> AddBrand([FromForm] AddBrand addBrand)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (!WorkWithFile.IsImage(addBrand.BrandImage))
                return StatusCode(500, "Please select Image");
            var UploadImageResult = await _cloudinaryService.UploadFile(addBrand.BrandImage, "ImageEshop/Brand");
            if (!UploadImageResult.IsSuccess)
                return StatusCode(500, UploadImageResult);
            var brandModel = _mapper.Map<BrandModel>(addBrand);
            brandModel.ImageURl = UploadImageResult.Url;
            brandModel.PublicID = UploadImageResult.PublicID;
            var result = await _brandService.Create(brandModel);
            if (result.code != 200)
              await _cloudinaryService.RemoveFileAsync(brandModel.PublicID);
            return StatusCode(result.code, result.Value);
        }

        [HttpPut("{ID}")]
        [Consumes("multipart/form-data")]
        [PermissionAuthorize(PermissionA.Brand+"."+AccessA.Update)]
        public async Task<IActionResult> UpdateBrand(int ID, [FromForm] UpdateBrand updateBrand)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var tempPublicID = "";
            var check = await _brandService.GetByID(ID);
            if (check.code == 404)
                return StatusCode(404);
            var brandModel = _mapper.Map<BrandModel>(updateBrand);
            brandModel.ID = ID;
            if (updateBrand.BrandImage == null)
            {
                brandModel.ImageURl = check.Value.ImageURl;
                brandModel.PublicID = check.Value.PublicID;
            }
            else
            {
                if (!WorkWithFile.IsImage(updateBrand.BrandImage))
                    return StatusCode(500, "Please select Image");
                var UploadImageResult = await _cloudinaryService.UploadFile(updateBrand.BrandImage, "ImageEshop/Brand");
                if (!UploadImageResult.IsSuccess)
                    return StatusCode(500, UploadImageResult);
                brandModel.ImageURl = UploadImageResult.Url;
                brandModel.PublicID = UploadImageResult.PublicID;
                tempPublicID = UploadImageResult.PublicID;
            }
            var result = await _brandService.Update(ID, brandModel);
            if (result.code != 200)
            {
                if (!string.IsNullOrEmpty(tempPublicID))
                    await  _cloudinaryService.RemoveFileAsync(tempPublicID);
                return StatusCode(500, result);
            }
            if (updateBrand.BrandImage != null)
                await _cloudinaryService.RemoveFileAsync(check.Value.PublicID);
            return StatusCode(200, result);

        }


        [HttpDelete("{ID}")]
        [PermissionAuthorize(PermissionA.Brand + "." + AccessA.Delete)]
        public async Task<IActionResult> DeleteBrandByID(int ID)
        {
            var check = await _brandService.GetByID(ID);
            if (check.code == 404)
                return StatusCode(404);
            var result = await _brandService.Delete(ID);
            if (result.code == 200)
            {
                var resultRemove = await _cloudinaryService.RemoveFileAsync(check.Value.PublicID);
                if (!resultRemove.IsSuccess)
                    return StatusCode(500, resultRemove);
            }
            return StatusCode(result.code, result.Value);
        }
    }
}
