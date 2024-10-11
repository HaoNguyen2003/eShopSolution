using eShopSolution.BusinessLayer.Abstract;
using eShopSolution.DataLayer.Abstract;
using eShopSolution.DtoLayer.Model;
using eShopSolution.DtoLayer.RepositoryModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.BusinessLayer.Service
{
    public class MenuService : IMenuService
    {
        private readonly IMenuDal _menuDal;

        public MenuService(IMenuDal menuDal) {
            _menuDal = menuDal;
        }
        public async Task<BaseRep<string>> Create(MenuModel model)
        {
            return await _menuDal.Create(model);
        }

        public async Task<BaseRep<string>> Delete(int ID)
        {
            return await (_menuDal.Delete(ID));
        }

        public async Task<BaseRep<List<MenuModel>>> GetAll()
        {
            return await _menuDal.GetAll();
        }

        public async Task<BaseRep<MenuModel>> GetByID(int ID)
        {
            return await _menuDal.GetByID(ID);
        }

        public async Task<BaseRep<string>> Update(int ID, MenuModel model)
        {
            return await _menuDal.Update(ID, model);
        }
    }
}
