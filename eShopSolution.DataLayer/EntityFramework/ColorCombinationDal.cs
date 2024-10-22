using AutoMapper;
using eShopSolution.DataLayer.Abstract;
using eShopSolution.DataLayer.Context;
using eShopSolution.DtoLayer.Model;
using eShopSolution.EntityLayer.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.DataLayer.EntityFramework
{
    public class ColorCombinationDal : GenericDal<ColorCombinationModel, ColorCombination>, IColorCombinationDal
    {
        public ColorCombinationDal(ApplicationContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<int> CreateModel(ColorCombinationModel colorCombinationModel)
        {
            try
            {
                var entity = _mapper.Map<ColorCombination>(colorCombinationModel);
                var result = await _context.ColorCombinations.AddAsync(entity);
                await _context.SaveChangesAsync();
                return entity.ID;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        public async Task<int> GetColorCombinationIdIfExists(List<int> colorIds)
        {
            var allColorCombinations = await _context.ColorCombinations.ToListAsync();

            foreach (var colorCombination in allColorCombinations)
            {
                var colorCombinationColorIds = _context.ColorCombinationColors
                    .Where(ccc => ccc.ColorCombinationID == colorCombination.ID)
                    .Select(ccc => ccc.ColorID)
                    .ToList();

                if (colorCombinationColorIds.Count == colorIds.Count &&
                    !colorIds.Except(colorCombinationColorIds).Any() &&
                    !colorCombinationColorIds.Except(colorIds).Any())
                {
                    return colorCombination.ID;
                }
            }
            return -1;
        }
    }
}
