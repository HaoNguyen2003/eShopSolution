using eShopSolution.BusinessLayer.Abstract;
using eShopSolution.DtoLayer.AddModel;
using eShopSolution.DtoLayer.Model;
using eShopSolution.DtoLayer.RepositoryModel;
using eShopSolution.EmailService.Model;
using eShopSolution.EmailService.Service;
using eShopSolution.EntityLayer.Data;
using Microsoft.AspNetCore.Identity;

namespace eShopSolution.BusinessLayer.Service
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly ITokenService _tokenService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IRefreshTokenService _refreshTokenService;
        private readonly ISenderEmail _senderEmail;

        public AuthenticationService(ITokenService tokenService, UserManager<AppUser> userManager, IRefreshTokenService refreshTokenService, ISenderEmail senderEmail)
        {
            _tokenService = tokenService;
            _userManager = userManager;
            _refreshTokenService = refreshTokenService;
            _senderEmail = senderEmail;
        }

        public async Task<Response<string>> CreateTokenAsync(SignIn loginDto)
        {
            if (loginDto == null) throw new ArgumentNullException(nameof(loginDto));
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user == null) return new Response<string>() { IsSuccess = false, Error = "Account does not exist" };
            if (!await _userManager.IsEmailConfirmedAsync(user))
                return new Response<string>() { IsSuccess = false, Error = "Email is not confirmed" };
            if (!await _userManager.CheckPasswordAsync(user, loginDto.Password))
            {
                await _userManager.AccessFailedAsync(user);
                int fail = await _userManager.GetAccessFailedCountAsync(user);
                if (user.LockoutEnd > DateTime.Now)
                {
                    return new Response<string>() { IsSuccess = false, Error = "Your account has been temporarily locked. Please try again later." };
                }
                if (fail == 4)
                {
                    await _userManager.SetLockoutEndDateAsync(user, new DateTimeOffset(DateTime.Now.AddMinutes(2)));
                    return new Response<string>() { IsSuccess = false, Error = "Your account has been locked for 20 minutes due to 3 failed login attempts. Please Try again later" };

                }
                return new Response<string>() { IsSuccess = false, Error = $"{fail} failed login attempts" };
            }
            if (await _userManager.IsLockedOutAsync(user))
            {
                return new Response<string>() { IsSuccess = false, Error = "Your account has been temporarily locked. Please try again later." };
            }
            if (await _userManager.GetTwoFactorEnabledAsync(user))
            {
                return await GennerateOTP(user);
            }
            return new Response<string>() { IsSuccess = false, Value = "123" };
        }

        private async Task<Response<string>> GennerateOTP(AppUser user)
        {
            var provider = await _userManager.GetValidTwoFactorProvidersAsync(user);
            if (!provider.Contains("Email"))
                return new Response<string>() { IsSuccess = false, Error = "InValid 2-Factor Provider" };
            var token = await _userManager.GenerateTwoFactorTokenAsync(user, "Email");

            var message = new Message(new[] { user.Email }, "Confirm OTP", token);
            var resultSenderEmail = _senderEmail.SendEmail(message);
            if (!resultSenderEmail.IsSuccess)
                return resultSenderEmail;
            return new Response<string>() { IsSuccess = true, Value = "Check Email To Confirm OTP" };
        }

        public async Task<AuthResponseDto> CreateTokenByRefreshToken(string refreshToken)
        {
            var existRefreshToken = await _refreshTokenService.GetUserRefreshToken(refreshToken);
            if (existRefreshToken.code != 200)
            {
                return new AuthResponseDto() { IsAuthSuccessful = false, ErrorMessage = "Not Found" };
            }

            var user = await _userManager.FindByIdAsync(existRefreshToken.Value.UserId);

            if (user == null)
            {
                return new AuthResponseDto() { IsAuthSuccessful = false, ErrorMessage = "Not Found" };
            }

            var TokenModel = _tokenService.CreateToken(user);
            UserRefreshTokenModel userRefreshTokenModel = new UserRefreshTokenModel { Id = existRefreshToken.Value.Id, UserId = user.Id, Code = TokenModel.RefreshToken, Expiration = (DateTime)TokenModel.RefreshTokenExpiration };
            await _refreshTokenService.Update(existRefreshToken.Value.Id, userRefreshTokenModel);
            return new AuthResponseDto() { IsAuthSuccessful = true, Token = TokenModel };

        }

        public async Task<BaseRep<string>> RevokenRefreshToken(string refreshToken)
        {
            return await _refreshTokenService.RevokenRefreshToken(refreshToken);
        }

        public async Task<Response<TokenModel>> CreateTokenOTP(string Email, string OTP)
        {
            var user = await _userManager.FindByEmailAsync(Email);
            if (user == null)
                return new Response<TokenModel>() { IsSuccess = false, Error = "Account does not exist" };
            var validVerification = await _userManager.VerifyTwoFactorTokenAsync(user, "Email", OTP);
            if (validVerification == false)
                return new Response<TokenModel>() { IsSuccess = false, Error = "OTP Code Incorrect" };
            var TokenModel = _tokenService.CreateToken(user);
            var userRefreshToken = await _refreshTokenService.GetUserRefreshTokenByUserID(user.Id);
            if (userRefreshToken.code != 200)
            {
                UserRefreshTokenModel userRefreshTokenModel = new UserRefreshTokenModel { UserId = user.Id, Code = TokenModel.RefreshToken, Expiration = (DateTime)TokenModel.RefreshTokenExpiration };
                await _refreshTokenService.Create(userRefreshTokenModel);
            }
            else
            {
                UserRefreshTokenModel userRefreshTokenModel = new UserRefreshTokenModel { Id = userRefreshToken.Value.Id, UserId = user.Id, Code = TokenModel.RefreshToken, Expiration = (DateTime)TokenModel.RefreshTokenExpiration };
                await _refreshTokenService.Update(userRefreshToken.Value.Id, userRefreshTokenModel);
            }
            await _userManager.ResetAccessFailedCountAsync(user);
            user.LastLogin = DateTime.Now;
            await _userManager.UpdateAsync(user);
            return new Response<TokenModel>() { IsSuccess = true, Value = TokenModel };
        }

        public async Task<Response<string>> ForgotPassword(string Email)
        {
            var user = await _userManager.FindByEmailAsync(Email);
            if (user == null)
                return new Response<string>() { IsSuccess = false, Error = "Account does not exist" };
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            return new Response<string> { IsSuccess = true, Value = token };
        }

        public async Task<Response<string>> ResetPassword(ResetPassword resetPassword)
        {
            var user = await _userManager.FindByEmailAsync(resetPassword.Email);
            if (user == null)
                return new Response<string>() { IsSuccess = false, Error = "not found" };
            if (resetPassword.Password != resetPassword.Password)
                return new Response<string> { IsSuccess = false, Error = "Password and ConfirmPassword not same" };
            var result = await _userManager.ResetPasswordAsync(user, resetPassword.Token, resetPassword.Password);
            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                return new Response<string> { IsSuccess = false, Error = errors };
            }
            return new Response<string> { IsSuccess = true, Value = "reset password success" };
        }
    }
}
