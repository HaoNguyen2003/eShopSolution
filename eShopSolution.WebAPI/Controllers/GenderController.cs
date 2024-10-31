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
    public class GenderController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IGenderService _genderService;
        public GenderController(IMapper mapper, IGenderService genderService)
        {
            _mapper = mapper;
            _genderService = genderService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllGender()
        {
            var result = await _genderService.GetAll();
            return StatusCode(result.code, result.Value);
        }

        [HttpGet("{ID}")]
        [PermissionAuthorize(PermissionA.Gender + "." + AccessA.Get)]
        public async Task<IActionResult> GetGenderByID(int ID)
        {
            var result = await _genderService.GetByID(ID);
            return StatusCode(result.code, result.Value);
        }

        [HttpPost]
        [PermissionAuthorize(PermissionA.Gender+"."+AccessA.Create)]
        public async Task<IActionResult> AddGender([FromBody] AddGender addGender)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var genderModel = _mapper.Map<GenderModel>(addGender);
            var result = await _genderService.Create(genderModel);
            return StatusCode(result.code, result.Value);
        }

        [HttpPut("{ID}")]
        [PermissionAuthorize(PermissionA.Gender + "." + AccessA.Update)]
        public async Task<IActionResult> UpdateGender(int ID, [FromBody] UpdateGender updateGender)
        {
            var genderModel = _mapper.Map<GenderModel>(updateGender);
            genderModel.ID = ID;
            var result = await _genderService.Update(ID, genderModel);
            return StatusCode(result.code, result.Value);
        }

        [HttpDelete("{ID}")]
        [PermissionAuthorize(PermissionA.Gender + "." + AccessA.Delete)]
        public async Task<IActionResult> DeleteGenderByID(int ID)
        {
            var result = await _genderService.Delete(ID);
            return StatusCode(result.code, result.Value);
        }
    }
}
