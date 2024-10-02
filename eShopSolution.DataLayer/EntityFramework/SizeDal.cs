using AutoMapper;
using eShopSolution.DataLayer.Abstract;
using eShopSolution.DataLayer.Context;
using eShopSolution.DtoLayer.Model;
using eShopSolution.EntityLayer.Data;

namespace eShopSolution.DataLayer.EntityFramework
{
    public class SizeDal : GenericDal<SizeModel, Sizes>, ISizeDal
    {
        public SizeDal(ApplicationContext context, IMapper mapper) : base(context, mapper)
        {
        }
    }
}
