using eShopSolution.DtoLayer.Model;
using eShopSolution.DtoLayer.RepositoryModel;

namespace eShopSolution.DataLayer.Abstract
{
    public interface ICategoryAndBrandDal : IGenericDal<CategoryAndBrandModel, CategoryModel>
    {
        public Task<BaseRep<List<CategoryModel>>> GetAllCategoryByBrandID(int brandID);
        public Task<BaseRep<List<BrandModel>>> GetAllBrandByCategoryID(int CategoryID);
    }
}
