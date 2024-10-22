using AutoMapper;
using eShopSolution.DataLayer.Abstract;
using eShopSolution.DataLayer.Context;
using eShopSolution.DtoLayer.Model;
using eShopSolution.EntityLayer.Data;
using Microsoft.EntityFrameworkCore;

namespace eShopSolution.DataLayer.EntityFramework
{
    public class ColorDal : GenericDal<ColorModel, Colors>, IColorDal
    {
        public ColorDal(ApplicationContext context, IMapper mapper) : base(context, mapper)
        {
        }
        public async Task<List<int>>GetIntColorByName(string name)
        {
            var ColorIDList = await _context.Colors
                             .Where(c => name.ToLower().Contains(c.Name.ToLower()))
                             .Select(c => c.ID)
                             .ToListAsync();
            if (!ColorIDList.Any())
            {
                return new List<int>(); 
            }
            return ColorIDList;

        }
    }
}
