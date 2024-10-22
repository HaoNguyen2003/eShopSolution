using eShopSolution.DtoLayer.Model;
using eShopSolution.EntityLayer.Data;

namespace eShopSolution.DataLayer.Abstract
{
    public interface IColorDal : IGenericDal<ColorModel, Colors>
    {
        public Task<List<int>> GetIntColorByName(string name);
    }
}
