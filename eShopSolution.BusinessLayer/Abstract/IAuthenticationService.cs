using eShopSolution.DtoLayer.AddModel;
using eShopSolution.DtoLayer.Model;
using eShopSolution.DtoLayer.RepositoryModel;

namespace eShopSolution.BusinessLayer.Abstract
{
    public interface IAuthenticationService
    {
        Task<Response<string>> CreateTokenAsync(SignIn loginDto);
        Task<AuthResponseDto> CreateTokenByRefreshToken(string refreshToken);
        Task<BaseRep<string>> RevokenRefreshToken(string refreshToken);
        Task<Response<TokenModel>> CreateTokenOTP(string Email, string OTP);
        Task<Response<string>> ForgotPassword(string Email);
        Task<Response<string>> ResetPassword(ResetPassword resetPassword);
    }
}
