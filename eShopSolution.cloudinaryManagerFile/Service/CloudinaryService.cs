using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using eShopSolution.cloudinaryManagerFile.Abstract;
using eShopSolution.cloudinaryManagerFile.config;
using eShopSolution.cloudinaryManagerFile.ResponsitoryModel;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;

namespace eShopSolution.cloudinaryManagerFile.Service
{
    public class CloudinaryService : ICloudinaryService
    {
        private readonly Cloudinary _cloudinary;
        public CloudinaryService()
        {
            _cloudinary = CloudinaryConfig.GetCloudinary();
        }
        public static string GenerateRandomString()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            Random random = new Random();
            int length = random.Next(1, 100);
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        public async Task<BaseModel> UploadFile(string source, string folder, IFormFile formFile = null)
        {
            ImageUploadParams uploadParams;

            if (!string.IsNullOrEmpty(source))
            {
                uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(source),
                    Folder = folder
                };
            }
            else if (formFile != null && formFile.Length > 0) 
            {
                uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(Guid.NewGuid().ToString() + GenerateRandomString(), formFile.OpenReadStream()),
                    Folder = folder
                };
            }
            else
            {
                return new BaseModel
                {
                    IsSuccess = false,
                    Errors = "Source URL is empty or file is null/empty."
                };
            }

            var uploadResult = await _cloudinary.UploadAsync(uploadParams);

            if (uploadResult.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var jsonResponse = JObject.Parse(uploadResult.JsonObj.ToString());
                return new BaseModel
                {
                    IsSuccess = true,
                    Message = "File uploaded successfully.",
                    Url = jsonResponse["url"]?.ToString(),
                    PublicID = jsonResponse["public_id"]?.ToString()
                };
            }

            return new BaseModel
            {
                IsSuccess = false,
                Errors = $"Failed to upload file: {uploadResult.Error.Message}"
            };
        }

        public async Task<BaseModel> RemoveFileAsync(string PublicID)
        {
            var deletionParams = new DeletionParams(PublicID)
            {
                ResourceType = ResourceType.Image
            };

            //var deletionResult = _cloudinary.Destroy(deletionParams);
            var deletionResult = await Task.Run(() => _cloudinary.Destroy(deletionParams));

            if (deletionResult.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return new BaseModel() { IsSuccess = true, Message = "Image deleted successfully." };
            }
            else
            {
                return new BaseModel() { IsSuccess = false, Message = $"Failed to delete image: {deletionResult.Error?.Message}" };
            }
        }

        public async Task<BaseModel> UploadFile(IFormFile formFile, string Folder)
        {
            if (formFile.Length <= 0)
                return new BaseModel() { IsSuccess = false, Errors = "File is Empty" };

            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(Guid.NewGuid().ToString(), formFile.OpenReadStream()),
                Folder = Folder
            };
            var uploadResult = await _cloudinary.UploadAsync(uploadParams);

            if (uploadResult.StatusCode == System.Net.HttpStatusCode.OK)
            {
                JObject jObject = JObject.Parse(uploadResult.JsonObj.ToString());
                return new BaseModel() { IsSuccess = true, Message = "File uploaded successfully.", Url = jObject["url"]?.ToString(), PublicID = jObject["public_id"]?.ToString() };
            }
            else
            {
                return new BaseModel() { IsSuccess = false, Errors = $"Failed to upload file: {uploadResult.Error.Message}", };
            }
        }


    }
}
