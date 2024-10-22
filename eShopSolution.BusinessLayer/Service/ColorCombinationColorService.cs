using eShopSolution.BusinessLayer.Abstract;
using eShopSolution.DataLayer.Abstract;
using eShopSolution.DataLayer.EntityFramework;
using eShopSolution.DtoLayer.Model;
using eShopSolution.DtoLayer.RepositoryModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.BusinessLayer.Service
{
    public class ColorCombinationColorService : IColorCombinationColorService
    {
        private readonly IColorCombinationColorDal _colorCombinationColorDal;

        public ColorCombinationColorService(IColorCombinationColorDal colorCombinationColorDal) {
            _colorCombinationColorDal=colorCombinationColorDal;
        }
        public async Task<BaseRep<string>> Create(ColorCombinationColorModel model)
        {
            return await _colorCombinationColorDal.Create(model);
        }

        public async Task<BaseRep<string>> Delete(int ID)
        {
            return await _colorCombinationColorDal.Delete(ID);
        }

        public async Task<BaseRep<List<ColorCombinationColorModel>>> GetAll()
        {
            return await _colorCombinationColorDal.GetAll();
        }

        public async Task<BaseRep<ColorCombinationColorModel>> GetByID(int ID)
        {
            return await _colorCombinationColorDal.GetByID(ID);
        }

        public async Task<BaseRep<string>> Update(int ID, ColorCombinationColorModel model)
        {
            return await _colorCombinationColorDal.Update(ID, model);
        }
    }
}
