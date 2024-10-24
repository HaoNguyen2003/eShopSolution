using eShopSolution.BusinessLayer.Abstract;
using eShopSolution.DataLayer.Abstract;
using eShopSolution.DtoLayer.Model;
using eShopSolution.DtoLayer.RepositoryModel;

namespace eShopSolution.BusinessLayer.Service
{
    public class CategoryAndBrandService : ICategoryAndBrandService
    {
        private readonly ICategoryAndBrandDal _categoryAndBrandDal;

        public CategoryAndBrandService(ICategoryAndBrandDal categoryAndBrandDal)
        {
            _categoryAndBrandDal = categoryAndBrandDal;
        }

        public async Task<BaseRep<string>> Create(CategoryAndBrandModel model)
        {
            return await _categoryAndBrandDal.Create(model);
        }

        public async Task<BaseRep<string>> Delete(int ID)
        {
            return await _categoryAndBrandDal.Delete(ID);
        }

        public async Task<BaseRep<List<CategoryAndBrandModel>>> GetAll()
        {
            return await _categoryAndBrandDal.GetAll();
        }

        public async Task<BaseRep<List<BrandModel>>> GetAllBrandByCategoryID(int CategoryID)
        {
            return await _categoryAndBrandDal.GetAllBrandByCategoryID(CategoryID);
        }

        public async Task<BaseRep<List<CategoryModel>>> GetAllCategoryByBrandID(int brandID)
        {
            return await _categoryAndBrandDal.GetAllCategoryByBrandID(brandID);
        }

        public async Task<BaseRep<CategoryAndBrandModel>> GetByID(int ID)
        {
            return await _categoryAndBrandDal.GetByID(ID);
        }

        public async Task<BaseRep<string>> Update(int ID, CategoryAndBrandModel model)
        {
            return await _categoryAndBrandDal.Update(ID, model);
        }
    }
}
