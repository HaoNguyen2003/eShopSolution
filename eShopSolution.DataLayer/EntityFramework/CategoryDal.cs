using AutoMapper;
using eShopSolution.DataLayer.Abstract;
using eShopSolution.DataLayer.Context;
using eShopSolution.DtoLayer.Model;
using eShopSolution.EntityLayer.Data;

namespace eShopSolution.DataLayer.EntityFramework
{
    public class CategoryDal : GenericDal<CategoryModel, Category>, ICategoryDal
    {
        public CategoryDal(ApplicationContext context, IMapper mapper) : base(context, mapper)
        {
        }
    }
}
