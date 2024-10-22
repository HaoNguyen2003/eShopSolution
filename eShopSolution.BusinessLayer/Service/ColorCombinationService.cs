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
    public class ColorCombinationService : IColorCombinationService
    {
        private readonly IColorCombinationDal _colorCombinationDal;

        public ColorCombinationService(IColorCombinationDal colorCombinationDal) {
            _colorCombinationDal=colorCombinationDal;
        }
        public async Task<BaseRep<string>> Create(ColorCombinationModel model)
        {
            return await _colorCombinationDal.Create(model);
        }

        public async Task<BaseRep<string>> Delete(int ID)
        {
            return await (_colorCombinationDal.Delete(ID));
        }

        public async Task<BaseRep<List<ColorCombinationModel>>> GetAll()
        {
            return await _colorCombinationDal.GetAll();
        }

        public async Task<BaseRep<ColorCombinationModel>> GetByID(int ID)
        {
            return await _colorCombinationDal.GetByID(ID);
        }

        public async Task<BaseRep<string>> Update(int ID, ColorCombinationModel model)
        {
            return await _colorCombinationDal.Update(ID, model);
        }

        public async Task<int> CreateModel(ColorCombinationModel colorCombinationModel)
        {
            return await _colorCombinationDal.CreateModel(colorCombinationModel);
        }
        public async Task<int> GetColorCombinationIdIfExists(List<int> colorIds)
        {
            return await _colorCombinationDal.GetColorCombinationIdIfExists(colorIds);
        }
    }
}
