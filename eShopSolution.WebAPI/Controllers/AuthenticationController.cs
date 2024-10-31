using eShopSolution.BusinessLayer.Abstract;
using eShopSolution.DtoLayer.AddModel;
using eShopSolution.DtoLayer.Model;
using eShopSolution.EmailService.Model;
using eShopSolution.EmailService.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace eShopSolution.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly ISenderEmail _senderEmail;
        public AuthenticationController(IAuthenticationService authenicationService, ISenderEmail senderEmail)
        {
            _authenticationService = authenicationService;
            _senderEmail = senderEmail;
        }
        [HttpPost]
        public async Task<IActionResult> CreateToken(SignIn login)
        {
            var result = await _authenticationService.CreateTokenAsync(login);
           if (!result.IsSuccess)
                return BadRequest(result);
            return StatusCode(200, result);
        }


        [HttpPost("refreshToken")]
        public async Task<IActionResult> CreateTokenByRefreshToken(string refreshToken)
        {
            var result = await _authenticationService.CreateTokenByRefreshToken(refreshToken);
            return Ok(result);
        }


        [HttpPost("RevokeRefreshToken")]
        [Authorize]
        public async Task<IActionResult> RevokeRefreshToken(string refreshToken)
        {
            var result = await _authenticationService.RevokenRefreshToken(refreshToken);
            return StatusCode(result.code, result.Value);
        }

        [HttpPost("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword([Required] string Email)
        {
            var result = await _authenticationService.ForgotPassword(Email);
            var origin = Request.Headers["Origin"].ToString();
            if (!result.IsSuccess)
                return BadRequest(result);
            var URL = Url.Action("ResetPassword", "Authentication", new { token = result.Value, email = Email }, protocol: Request.Scheme);
            var Message = new Message(new[] { Email }, "Forgot Password", URL);
            var resultSendEmail = _senderEmail.SendEmail(Message);
            if (!resultSendEmail.IsSuccess)
                return BadRequest(resultSendEmail);
            return StatusCode(200, resultSendEmail);
        }
        [HttpGet("Reset-password")]
        public async Task<IActionResult> ResetPassword(string token, string email)
        {
            var resetPassword = new ResetPassword() { Email = email, Token = token };
            return Ok(resetPassword);
        }
        [HttpPost("Reset-password")]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword(ResetPassword resetPassword)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _authenticationService.ResetPassword(resetPassword);
            if (!result.IsSuccess)
                return BadRequest(result.ToString());
            return StatusCode(200, result);
        }
        [HttpPost("Login-2FA")]
        public async Task<IActionResult> LoginWithOTP(string Email, string OTP)
        {
            var result = await _authenticationService.CreateTokenOTP(Email, OTP);
            if (!result.IsSuccess)
                return BadRequest(result);
            return Ok(result);
        }
    }
}
