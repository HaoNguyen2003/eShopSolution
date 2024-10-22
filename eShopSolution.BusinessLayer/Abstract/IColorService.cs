using eShopSolution.DtoLayer.Model;
using eShopSolution.EntityLayer.Data;

namespace eShopSolution.BusinessLayer.Abstract
{
    public interface IColorService : IGenericService<ColorModel, Colors>
    {
        public Task<List<int>> GetIntColorByName(string name);
    }
}
