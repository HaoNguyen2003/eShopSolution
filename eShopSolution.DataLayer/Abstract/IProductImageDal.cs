using eShopSolution.DtoLayer.Model;
using eShopSolution.EntityLayer.Data;

namespace eShopSolution.DataLayer.Abstract
{
    public interface IProductImageDal : IGenericDal<ProductImageModel, ProductImages>
    {
        public Task<List<string>> GetAllImageByProductColor(int ID);
        public Task<List<string>> GetAllPublicIDByProductColor(int ID);
        public Task<List<ProductImageModel>> GetAllProductImageByProductColor(int ID);
        public Task<string> GetImagefirstByID(int ID);
    }
}
