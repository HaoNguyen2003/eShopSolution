using eShopSolution.BusinessLayer.Abstract;
using eShopSolution.DataLayer.Abstract;
using eShopSolution.DtoLayer.Model;
using eShopSolution.DtoLayer.RepositoryModel;

namespace eShopSolution.BusinessLayer.Service
{
    public class RefreshTokenService : IRefreshTokenService
    {
        public readonly IRefreshTokenDal _refreshTokenDal;
        public RefreshTokenService(IRefreshTokenDal refreshTokenDal)
        {
            _refreshTokenDal = refreshTokenDal;
        }
        public async Task<BaseRep<string>> Create(UserRefreshTokenModel model)
        {
            return await _refreshTokenDal.Create(model);
        }

        public async Task<BaseRep<string>> Delete(int ID)
        {
            return await _refreshTokenDal.Delete(ID);
        }

        public async Task<BaseRep<List<UserRefreshTokenModel>>> GetAll()
        {
            return await _refreshTokenDal.GetAll();
        }

        public async Task<BaseRep<UserRefreshTokenModel>> GetByID(int ID)
        {
            return await _refreshTokenDal.GetByID(ID);
        }

        public async Task<BaseRep<UserRefreshTokenModel>> GetUserRefreshToken(string RefreshToken)
        {
            return await _refreshTokenDal.GetUserRefreshToken(RefreshToken);
        }

        public async Task<BaseRep<UserRefreshTokenModel>> GetUserRefreshTokenByUserID(string userID)
        {
            return await _refreshTokenDal.GetUserRefreshTokenByUserID(userID);
        }

        public async Task<BaseRep<string>> RevokenRefreshToken(string refreshToken)
        {
            return await _refreshTokenDal.RevokenRefreshToken(refreshToken);
        }

        public async Task<BaseRep<string>> Update(int ID, UserRefreshTokenModel model)
        {
            return await _refreshTokenDal.Update(ID, model);
        }
    }
}
