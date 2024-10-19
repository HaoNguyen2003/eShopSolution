using eShopSolution.BusinessLayer.Abstract;
using eShopSolution.BusinessLayer.Service;
using eShopSolution.cloudinaryManagerFile.Abstract;
using eShopSolution.DtoLayer.AddModel;
using eShopSolution.DtoLayer.Model;
using eShopSolution.DtoLayer.RepositoryModel;
using eShopSolution.DtoLayer.UpdateModel;
using eShopSolution.EmailService.Model;
using eShopSolution.EmailService.Service;
using eShopSolution.WebAPI.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eShopSolution.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        private readonly ISenderEmail _senderEmail;
        private readonly ICloudinaryService _cloudinaryService;
        public UserController(IUserService userService, ISenderEmail senderEmail, ICloudinaryService cloudinaryService)
        {
            _userService = userService;
            _senderEmail = senderEmail;
            _cloudinaryService = cloudinaryService;
        }
        [HttpPost("SignUp")]
        public async Task<IActionResult> CreateUser(SignUp signUp)
        {
            var result = await _userService.CreateUserAsync(signUp);
            if (result.IsSuccess == false)
                return BadRequest(result);
            var token = result.keyValuePairs["token"];
            var email = result.keyValuePairs["email"];
            var confirmationLink = GenerateConfirmationLink(token, email);
            var emailResult = SendConfirmationEmailAsync(email, confirmationLink);
            if (!emailResult.IsSuccess)
            {
                return StatusCode(500, new { IsSuccess = false, Errors = emailResult.Error });
            }
            return StatusCode(200, new { IsSuccess = true, Message = "Check Mail To Confirm Email" });
        }

        [HttpPost("SendVerifiedAgain")]
        public async Task<IActionResult> SendVerifiedAgain(string Email)
        {
            var result = await _userService.GenerateConfirmToken(Email);
            if (result.IsSuccess == false)
                return BadRequest(result);
            var token = result.Value["token"];
            var confirmationLink = GenerateConfirmationLink(token, Email);
            var emailResult = SendConfirmationEmailAsync(Email, confirmationLink);
            if (!emailResult.IsSuccess)
            {
                return StatusCode(500, new { IsSuccess = false, Errors = emailResult.Error });
            }
            return StatusCode(200, new { IsSuccess = false, Message = "Check Mail To Confirm Email" });
        }

        [HttpGet("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail(string token, string email)
        {
            var result = await _userService.ConfirmEmail(token, email);
            if (result.IsSuccess == false)
                return BadRequest(result);
            return StatusCode(200, result);
        }
        [HttpPost("ChangeProfilePicture")]
        [Consumes("multipart/form-data")]
        [Authorize]
        public async Task<IActionResult> ChangeProfilePicture([FromForm] UpdateAvatar updateAvatar)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (!WorkWithFile.IsImage(updateAvatar.formFile))
                return StatusCode(500, "Please select Image");
            var UploadImageResult = await _cloudinaryService.UploadFile(updateAvatar.formFile, "ImageEshop/Avatar");
            if (!UploadImageResult.IsSuccess)
                return StatusCode(500, UploadImageResult);
            var result = await _userService.UpdateAvatar(UploadImageResult.Url,UploadImageResult.PublicID, updateAvatar.ID);
            if (!result.IsSuccess)
            {
                _cloudinaryService.RemoveFile(UploadImageResult.PublicID);
                return StatusCode(500, result.Error);
            }
            if (!string.IsNullOrEmpty(result.Value))
                _cloudinaryService.RemoveFile(result.Value);
            return StatusCode(200, "Update Avatar Success");

        }

        private string GenerateConfirmationLink(string token, string email)
        {
            return Url.Action(nameof(ConfirmEmail), "User", new { token, email }, protocol: Request.Scheme);
        }

        private Response<string> SendConfirmationEmailAsync(string email, string confirmationLink)
        {
            var message = new Message(new[] { email }, "Confirm Email", confirmationLink);
            return _senderEmail.SendEmail(message);
        }

    }
}
