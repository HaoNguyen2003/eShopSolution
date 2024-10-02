using AutoMapper;
using eShopSolution.DataLayer.Abstract;
using eShopSolution.DataLayer.Context;
using eShopSolution.DtoLayer.AddModel;
using eShopSolution.DtoLayer.Model;
using eShopSolution.DtoLayer.RepositoryModel;
using eShopSolution.EntityLayer.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.DataLayer.EntityFramework
{
    public class AddressDeliveryDal:IAddressDeliveryDal
    {
        public readonly ApplicationContext _context;
        public readonly IMapper _mapper;
        public AddressDeliveryDal(ApplicationContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Response<AddressShipInfo>> Create(AddAddressShipInfo addAddressShipInfo, string UserID)
        {
            try
            {
                Address AddressShipInfo = _mapper.Map<Address>(addAddressShipInfo);
                AddressShipInfo.UserID=UserID; 
                await _context.AddAsync(AddressShipInfo);
                await _context.SaveChangesAsync();

                return new Response<AddressShipInfo>() { IsSuccess = true,Value = _mapper.Map<AddressShipInfo>(AddressShipInfo) };
            }
            catch (Exception ex)
            {
                return new Response<AddressShipInfo>() {IsSuccess=false,Error=ex.Message};
            }
        }

        public async Task<Response<string>> DeleteByIdOfUser(int ID, string UserID)
        {
            try
            {
                var Address = await _context.addresses.FindAsync(ID);
                if (Address.UserID == UserID)
                {
                    _context.addresses.Remove(Address);
                    await _context.SaveChangesAsync();
                    return new Response<string>() { IsSuccess = true, Value = "Delete Success" };
                }
                return new Response<string>() { IsSuccess = false, Value = "Delete Fail: You Not Permission Delete" };
            }
            catch (Exception ex)
            {
                return new Response<string>() { IsSuccess = false, Error = ex.Message };

            }
        }

        public async Task<Response<List<AddressShipInfo>>> GetAllAddress()
        {
            var listAddress = await _context.addresses.ToListAsync();
            if(listAddress == null)
                return new Response<List<AddressShipInfo>>() { IsSuccess=true,Value = new List<AddressShipInfo>() };
            return new Response<List<AddressShipInfo>>() { IsSuccess = true, Value = _mapper.Map<List<AddressShipInfo>>(listAddress) };
        }

        public async Task<Response<List<AddressShipInfo>>> GetAllAddressInfoByUserID(string UserID)
        {
            var listAddress = await _context.addresses
             .Where(a => a.UserID == UserID)
             .ToListAsync();
            if (listAddress == null || !listAddress.Any())
            {
                return new Response<List<AddressShipInfo>>()
                {
                    IsSuccess = true,
                    Value = new List<AddressShipInfo>() 
                };
            }
            return new Response<List<AddressShipInfo>>()
            {
                IsSuccess = true,
                Value = _mapper.Map<List<AddressShipInfo>>(listAddress) 
            };
        }

        public async Task<Response<AddAddressShipInfo>> GetByIdOfManager(int ID)
        {
            var Address = await _context.addresses.FindAsync(ID);
            if (Address == null)
            {
                var result = _mapper.Map<AddAddressShipInfo>(Address);
                return new Response<AddAddressShipInfo>() { IsSuccess = true, Value = result };
            }
            return new Response<AddAddressShipInfo>() { IsSuccess = false, Error = "Not Found" };
        }

        public async Task<Response<AddAddressShipInfo>> GetByIdOfUser(int ID, string UserID)
        {
            var Address = await _context.addresses.FindAsync(ID);
            if (Address.UserID == UserID)
            {
                var result = _mapper.Map<AddAddressShipInfo>(Address);
                return new Response<AddAddressShipInfo>() { IsSuccess = true, Value = result };
            }
            return new Response<AddAddressShipInfo>() { IsSuccess = false, Error = "You Not Permission Get" };
        }

        public async Task<Response<AddressShipInfo>> Update(AddAddressShipInfo addAddressShipInfo, string UserID)
        {
            try
            {
                var Address = await _context.addresses.FindAsync(addAddressShipInfo.ID);
                if (Address.UserID == UserID)
                {
                    _mapper.Map(addAddressShipInfo, Address);
                    await _context.SaveChangesAsync();
                    var result = _mapper.Map<AddressShipInfo>(Address);
                    return new Response<AddressShipInfo> { IsSuccess = true, Value = result };
                }
                return new Response<AddressShipInfo>() { IsSuccess = false, Error = "Update Fail" };
            }
            catch (Exception ex)
            {
                return new Response<AddressShipInfo>() { IsSuccess = false, Error = ex.Message };
            }
            
        }
    }

}
