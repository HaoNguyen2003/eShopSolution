using eShopSolution.cloudinaryManagerFile.ResponsitoryModel;
using Microsoft.AspNetCore.Http;

namespace eShopSolution.cloudinaryManagerFile.Abstract
{
    public interface ICloudinaryService
    {
        public Task<BaseModel> UploadFile(IFormFile formFile, string Folder);
        public Task<BaseModel> RemoveFileAsync(string PublicID);
        public Task<BaseModel> UploadFile(string source, string folder, IFormFile formFile = null);
    }
}
