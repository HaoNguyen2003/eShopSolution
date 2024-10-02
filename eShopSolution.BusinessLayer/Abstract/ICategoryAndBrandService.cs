using eShopSolution.DtoLayer.Model;
using eShopSolution.DtoLayer.RepositoryModel;
using eShopSolution.EntityLayer.Data;

namespace eShopSolution.BusinessLayer.Abstract
{
    public interface ICategoryAndBrandService : IGenericService<CategoryAndBrandModel, CategoryAndBrand>
    {
        public Task<BaseRep<List<CategoryModel>>> GetAllCategoryByBrandID(int brandID);
        public Task<BaseRep<List<BrandModel>>> GetAllBrandByCategoryID(int CategoryID);
    }
}
