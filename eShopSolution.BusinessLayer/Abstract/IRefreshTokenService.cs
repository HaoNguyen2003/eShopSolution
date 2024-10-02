using eShopSolution.DtoLayer.Model;
using eShopSolution.DtoLayer.RepositoryModel;
using eShopSolution.EntityLayer.Data;

namespace eShopSolution.BusinessLayer.Abstract
{
    public interface IRefreshTokenService : IGenericService<UserRefreshTokenModel, UserRefreshToken>
    {
        public Task<BaseRep<UserRefreshTokenModel>> GetUserRefreshToken(string RefreshToken);
        public Task<BaseRep<UserRefreshTokenModel>> GetUserRefreshTokenByUserID(string userID);
        public Task<BaseRep<string>> RevokenRefreshToken(string refreshToken);
    }
}
