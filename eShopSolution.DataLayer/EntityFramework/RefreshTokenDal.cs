using AutoMapper;
using eShopSolution.DataLayer.Abstract;
using eShopSolution.DataLayer.Context;
using eShopSolution.DtoLayer.Model;
using eShopSolution.DtoLayer.RepositoryModel;
using eShopSolution.EntityLayer.Data;
using Microsoft.EntityFrameworkCore;

namespace eShopSolution.DataLayer.EntityFramework
{
    public class RefreshTokenDal : GenericDal<UserRefreshTokenModel, UserRefreshToken>, IRefreshTokenDal
    {
        public RefreshTokenDal(ApplicationContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<BaseRep<UserRefreshTokenModel>> GetUserRefreshToken(string RefreshToken)
        {
            var UserRefreshToken = await _context.UserRefreshTokens.FirstOrDefaultAsync(p => p.Code == RefreshToken);
            if (UserRefreshToken == null)
                return new BaseRep<UserRefreshTokenModel>() { code = 400, Value = new UserRefreshTokenModel() };
            return new BaseRep<UserRefreshTokenModel>() { code = 200, Value = _mapper.Map<UserRefreshTokenModel>(UserRefreshToken) };
        }

        public async Task<BaseRep<UserRefreshTokenModel>> GetUserRefreshTokenByUserID(string userID)
        {
            var UserRefreshToken = await _context.UserRefreshTokens.FirstOrDefaultAsync(p => p.UserId == userID);
            if (UserRefreshToken == null)
                return new BaseRep<UserRefreshTokenModel>() { code = 400, Value = new UserRefreshTokenModel() };
            return new BaseRep<UserRefreshTokenModel>() { code = 200, Value = _mapper.Map<UserRefreshTokenModel>(UserRefreshToken) };
        }

        public async Task<BaseRep<string>> RevokenRefreshToken(string refreshToken)
        {
            var UserRefreshToken = await _context.UserRefreshTokens.FirstOrDefaultAsync(p => p.Code == refreshToken);
            if (UserRefreshToken == null)
                return new BaseRep<string>() { code = 400, Value = "Not found" };
            _context.UserRefreshTokens.Remove(UserRefreshToken);
            _context.SaveChanges();
            return new BaseRep<string>() { code = 200, Value = "Delete success" };
        }
    }
}
