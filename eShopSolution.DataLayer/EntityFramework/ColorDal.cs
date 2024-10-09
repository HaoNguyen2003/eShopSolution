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
        public async Task<int>GetIntColorByName(string name)
        {
            var ColorID = await _context.Colors
                             .Where(c => c.Name.ToLower() == name.ToLower())
                             .Select(c => c.ID)
                             .FirstOrDefaultAsync();
            if (ColorID == 0)
            {
                return 0;
            }
            return ColorID;

        }
    }
}
