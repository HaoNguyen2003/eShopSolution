using eShopSolution.DtoLayer.Model;
using eShopSolution.EntityLayer.Data;

namespace eShopSolution.DataLayer.Abstract
{
    public interface IColorDal : IGenericDal<ColorModel, Colors>
    {
        public Task<int> GetIntColorByName(string name);
    }
}
