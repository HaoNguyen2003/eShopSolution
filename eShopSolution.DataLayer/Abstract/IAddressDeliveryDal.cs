using eShopSolution.DtoLayer.AddModel;
using eShopSolution.DtoLayer.Model;
using eShopSolution.DtoLayer.RepositoryModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.DataLayer.Abstract
{
    public interface IAddressDeliveryDal
    {
        public Task<Response<AddressShipInfo>> Create(AddAddressShipInfo addAddressShipInfo, string UserID);
        public Task<Response<AddressShipInfo>> Update(AddAddressShipInfo addAddressShipInfo, string UserID);
        public Task<Response<AddAddressShipInfo>> GetByIdOfUser(int ID, string UserID);
        public Task<Response<List<AddressShipInfo>>> GetAllAddressInfoByUserID(string UserID);
        public Task<Response<List<AddressShipInfo>>> GetAllAddress();
        public Task<Response<AddAddressShipInfo>> GetByIdOfManager(int ID);
        public Task<Response<string>> DeleteByIdOfUser(int ID, string UserID);
    }
}
