using eShopSolution.BusinessLayer.Abstract;
using eShopSolution.DataLayer.Abstract;
using eShopSolution.DataLayer.EntityFramework;
using eShopSolution.DtoLayer.Model;
using eShopSolution.DtoLayer.RepositoryModel;
using Microsoft.Extensions.Caching.Memory;

namespace eShopSolution.BusinessLayer.Service
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryDal _categoryDal;
        private readonly ICustomCache<string> _customCache;

        public CategoryService(ICategoryDal categoryDal, ICustomCache<string> customCache)
        {
            _categoryDal = categoryDal;
            _customCache = customCache;
        }
        public Task<BaseRep<string>> Create(CategoryModel model)
        {
            _customCache.Clear();
            return _categoryDal.Create(model);
        }

        public Task<BaseRep<string>> Delete(int ID)
        {
            _customCache.Clear();
            return _categoryDal.Delete(ID);
        }

        public async Task<BaseRep<List<CategoryModel>>> GetAll()
        {
            var result = _customCache.Get<BaseRep<List<CategoryModel>>>("category");
            if (result == null)
            {
                result = await _categoryDal.GetAll();
                _customCache.Set(key: "category", value: result, priority: CacheItemPriority.High);
            }
            return result;
        }

        public Task<BaseRep<CategoryModel>> GetByID(int ID)
        {
           
            return _categoryDal.GetByID(ID);
        }

        public Task<BaseRep<string>> Update(int ID, CategoryModel model)
        {
            _customCache.Clear();
            return _categoryDal.Update(ID, model);
        }
    }
}
