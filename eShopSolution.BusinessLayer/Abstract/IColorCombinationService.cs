using eShopSolution.DtoLayer.Model;
using eShopSolution.EntityLayer.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.BusinessLayer.Abstract
{
    public interface IColorCombinationService : IGenericService<ColorCombinationModel,ColorCombination>
    {
        public Task<int> GetColorCombinationIdIfExists(List<int> colorIds);
        public Task<int> CreateModel(ColorCombinationModel colorCombinationModel);
    }
}
