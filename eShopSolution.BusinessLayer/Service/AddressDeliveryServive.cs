using eShopSolution.BusinessLayer.Abstract;
using eShopSolution.DataLayer.Abstract;
using eShopSolution.DtoLayer.AddModel;
using eShopSolution.DtoLayer.Configuration;
using eShopSolution.DtoLayer.Model;
using eShopSolution.DtoLayer.RepositoryModel;
using eShopSolution.DtoLayer.RequestModel;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.BusinessLayer.Service
{
    public class AddressDeliveryServive : IAddressDeliveryServive
    {
        public readonly IAddressDeliveryDal _addressDeliveryDal;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ShippingProvidersConfiguration _shippingProvidersConfiguration;
        public AddressDeliveryServive(IAddressDeliveryDal addressDeliveryDal, IOptions<ShippingProvidersConfiguration> options, IHttpClientFactory httpClientFactory) {
            _addressDeliveryDal= addressDeliveryDal;
            _shippingProvidersConfiguration = options.Value;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<Response<AddressShipInfo>> Create(AddAddressShipInfo addAddressShipInfo, string UserID)
        {
            return await _addressDeliveryDal.Create(addAddressShipInfo, UserID);
        }

        public async Task<Response<string>> DeleteByIdOfUser(int ID, string UserID)
        {
           return await _addressDeliveryDal.DeleteByIdOfUser(ID, UserID);
        }

        public async Task<Response<List<AddressShipInfo>>> GetAllAddress()
        {
           return await _addressDeliveryDal.GetAllAddress();
        }

        public async Task<Response<List<AddressShipInfo>>> GetAllAddressInfoByUserID(string UserID)
        {
            return await _addressDeliveryDal.GetAllAddressInfoByUserID(UserID);
        }

        public async Task<Response<AddAddressShipInfo>> GetByIdOfManager(int ID)
        {
            return await _addressDeliveryDal.GetByIdOfManager(ID);
        }

        public async Task<Response<AddAddressShipInfo>> GetByIdOfUser(int ID, string UserID)
        {
            return await _addressDeliveryDal.GetByIdOfUser(ID, UserID);
        }

        public async Task<Response<AddressShipInfo>> Update(AddAddressShipInfo addAddressShipInfo, string UserID)
        {
            return await _addressDeliveryDal.Update(addAddressShipInfo, UserID);
        }
        public async Task<double> GetFeeShip(CalculateFeeShipModel calculateFeeShipModel,int AddressID,string UserID)
        {
            var Address = await _addressDeliveryDal.GetByIdOfUser(AddressID, UserID);
            if(!Address.IsSuccess)
                return 0;
            calculateFeeShipModel.to_ward_code = Address.Value.WardCode;
            calculateFeeShipModel.to_district_id = Address.Value.districtID;
            calculateFeeShipModel.from_district_id = Int32.Parse(_shippingProvidersConfiguration.District);
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Add("Token", _shippingProvidersConfiguration.TokenGHN);
            var jsonContent = new StringContent(JsonConvert.SerializeObject(calculateFeeShipModel), System.Text.Encoding.UTF8, "application/json");
            var response = await client.PostAsync("https://online-gateway.ghn.vn/shiip/public-api/v2/shipping-order/fee", jsonContent);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                dynamic jsonResponse = JsonConvert.DeserializeObject<dynamic>(result);
                double totalFee = jsonResponse.data.total;
                return totalFee; 
            }
            var errorContent = await response.Content.ReadAsStringAsync();
            return 0;
        }

        public Task<double> GetFeeShip(CalculateFeeShipModel calculateFeeShipModel)
        {
            throw new NotImplementedException();
        }
    }
}
