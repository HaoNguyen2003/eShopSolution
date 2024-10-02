using AutoMapper;
using eShopSolution.DataLayer.Abstract;
using eShopSolution.DataLayer.Context;
using eShopSolution.DtoLayer.Model;
using eShopSolution.EntityLayer.Data;

namespace eShopSolution.DataLayer.EntityFramework
{
    public class ColorDal : GenericDal<ColorModel, Colors>, IColorDal
    {
        public ColorDal(ApplicationContext context, IMapper mapper) : base(context, mapper)
        {
        }
    }
}
