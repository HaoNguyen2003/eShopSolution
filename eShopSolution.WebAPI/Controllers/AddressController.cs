using eShopSolution.BusinessLayer.Abstract;
using eShopSolution.DtoLayer.AddModel;
using eShopSolution.DtoLayer.Configuration;
using eShopSolution.DtoLayer.RequestModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;

namespace eShopSolution.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ShippingProvidersConfiguration _shippingProvidersConfiguration;
        private readonly IAddressDeliveryServive _addressDeliveryServive;
        public AddressController(IHttpClientFactory httpClientFactory, IOptions<ShippingProvidersConfiguration> options,IAddressDeliveryServive addressDeliveryServive)
        {
            _httpClientFactory = httpClientFactory;
            _shippingProvidersConfiguration = options.Value;
            _addressDeliveryServive = addressDeliveryServive;
        }
        [HttpGet("GetAllProvince")]
        public async Task<IActionResult> GetAllProvince()
        {
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Add("Token", _shippingProvidersConfiguration.TokenGHN);
            var response = await client.GetAsync("https://online-gateway.ghn.vn/shiip/public-api/master-data/province");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                return Ok(result);
            }
            var errorContent = await response.Content.ReadAsStringAsync();
            return BadRequest(new { StatusCode = response.StatusCode, ErrorMessage = errorContent });
        }
        [HttpGet("GetAllDistrict")]
        public async Task<IActionResult> GetAllDistrict(int province_id)
        {
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Add("Token", _shippingProvidersConfiguration.TokenGHN);
            var response = await client.GetAsync($"https://online-gateway.ghn.vn/shiip/public-api/master-data/district?province_id={province_id}");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                return Ok(result);
            }
            var errorContent = await response.Content.ReadAsStringAsync();
            return BadRequest(new { StatusCode = response.StatusCode, ErrorMessage = errorContent });
        }
        [HttpGet("GetAllWard")]
        public async Task<IActionResult> GetAllWard(int district_id)
        {
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Add("Token", _shippingProvidersConfiguration.TokenGHN);
            var response = await client.GetAsync($"https://online-gateway.ghn.vn/shiip/public-api/master-data/ward?district_id={district_id}");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                return Ok(result);
            }
            var errorContent = await response.Content.ReadAsStringAsync();
            return BadRequest(new { StatusCode = response.StatusCode, ErrorMessage = errorContent });
        }

        [HttpPost("GetAvailableServices")]
        public async Task<IActionResult> GetAvailableServices(CalculateFeeShipModel calculateFeeShipModel)
        {
            
            return Ok(await _addressDeliveryServive.GetFeeShip(calculateFeeShipModel));
        }
        [Authorize]
        [HttpPost("CreateAddressShip")]
        public async Task<IActionResult> CreateAddressShip(AddAddressShipInfo addAddressShipInfo)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized(new { Error = "User not authorized" });
            }
            var result = await _addressDeliveryServive.Create(addAddressShipInfo, userId);
            if(result.IsSuccess) {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [Authorize]
        [HttpDelete]
        public async Task<IActionResult> DeleteByIDOfUser(int ID)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized(new { Error = "User not authorized" });
            }
            var result = await _addressDeliveryServive.DeleteByIdOfUser(ID, userId);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [Authorize(Roles="Admin")]
        [HttpGet("GetAllAddress")]
        public async Task<IActionResult> GetAllAddress()
        {
            var result = await _addressDeliveryServive.GetAllAddress();
            return Ok(result);
        }
        [Authorize]
        [HttpGet("GetAllAddressInfoByUserID")]
        public async Task<IActionResult> GetAllAddressInfoByUserID()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized(new { Error = "User not authorized" });
            }
            var result = await _addressDeliveryServive.GetAllAddressInfoByUserID(userId);
            return Ok(result);
        }
        [Authorize]
        [HttpGet("GetByIdOfUser")]
        public async Task<IActionResult> GetAddressByIdOfUser(int id)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized(new { Error = "User not authorized" });
            }
            var result = await _addressDeliveryServive.GetByIdOfUser(id,userId);
            if(result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("GetByIdOfManager")]
        public async Task<IActionResult> GetByIdOfManager(int id)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized(new { Error = "User not authorized" });
            }
            var result = await _addressDeliveryServive.GetByIdOfUser(id, userId);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [Authorize]
        [HttpPut]
        public async Task<IActionResult> Update(AddAddressShipInfo addAddressShipInfo)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized(new { Error = "User not authorized" });
            }
            var result = await _addressDeliveryServive.Update(addAddressShipInfo, userId);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

    }
}
