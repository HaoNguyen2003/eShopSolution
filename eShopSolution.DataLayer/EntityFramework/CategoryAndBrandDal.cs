using AutoMapper;
using eShopSolution.DataLayer.Abstract;
using eShopSolution.DataLayer.Context;
using eShopSolution.DtoLayer.Model;
using eShopSolution.DtoLayer.RepositoryModel;
using eShopSolution.EntityLayer.Data;
using Microsoft.EntityFrameworkCore;

namespace eShopSolution.DataLayer.EntityFramework
{
    public class CategoryAndBrandDal : GenericDal<CategoryAndBrandModel, CategoryAndBrand>, ICategoryAndBrandDal
    {
        public CategoryAndBrandDal(ApplicationContext context, IMapper mapper) : base(context, mapper)
        {
        }
        public async Task<BaseRep<List<BrandModel>>> GetAllBrandByCategoryID(int CategoryID)
        {
            var brandIDs = await _context.categoryAndBrands
                                      .Where(cb => cb.CategoryID == CategoryID)
                                      .Select(cb => cb.BrandID)
                                      .ToListAsync();

            if (brandIDs.Count == 0)
            {
                return new BaseRep<List<BrandModel>>() { code = 200, Value = new List<BrandModel>() };
            }
            var brands = await _context.brands
                                   .Where(b => brandIDs.Contains(b.BrandID))
                                   .Select(b => new BrandModel
                                   {
                                       ID = b.BrandID,
                                       BrandName = b.BrandName,
                                       ImageURl = b.ImageURL
                                   })
                                   .ToListAsync();
            return new BaseRep<List<BrandModel>>() { code = 200, Value = brands };

        }

        public async Task<BaseRep<List<CategoryModel>>> GetAllCategoryByBrandID(int brandID)
        {
            var categoryIDs = await _context.categoryAndBrands
                                      .Where(cb => cb.BrandID == brandID)
                                      .Select(cb => cb.CategoryID)
                                      .ToListAsync();

            if (categoryIDs.Count == 0)
            {
                return new BaseRep<List<CategoryModel>>() { code = 200, Value = new List<CategoryModel>() };
            }
            var categorys = await _context.categories
                                   .Where(b => categoryIDs.Contains(b.CategoryID))
                                   .Select(b => new CategoryModel
                                   {
                                       ID = b.CategoryID,
                                       CategoryName = b.CategoryName,
                                       ImageURl = b.ImageURL
                                   })
                                   .ToListAsync();
            return new BaseRep<List<CategoryModel>>() { code = 200, Value = categorys };
        }
    }
}
