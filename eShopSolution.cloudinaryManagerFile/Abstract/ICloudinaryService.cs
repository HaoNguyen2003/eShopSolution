using eShopSolution.cloudinaryManagerFile.ResponsitoryModel;
using Microsoft.AspNetCore.Http;

namespace eShopSolution.cloudinaryManagerFile.Abstract
{
    public interface ICloudinaryService
    {
        public Task<BaseModel> UploadFile(IFormFile formFile, string Folder);
        public BaseModel RemoveFile(string PublicID);
    }
}
