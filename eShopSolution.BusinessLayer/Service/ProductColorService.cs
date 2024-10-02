using eShopSolution.BusinessLayer.Abstract;
using eShopSolution.DataLayer.Abstract;
using eShopSolution.DtoLayer.Model;
using eShopSolution.DtoLayer.RepositoryModel;

namespace eShopSolution.BusinessLayer.Service
{
    public class ProductColorService : IProductColorService
    {
        private readonly IProductColorDal _productColorDal;
        private readonly ICustomCache<string> _customCache;
        public ProductColorService(IProductColorDal productColorDal, ICustomCache<string> customCache)
        {
            _productColorDal = productColorDal;
            _customCache = customCache;
        }
        public async Task<BaseRep<string>> Create(ProductColorModel model)
        {
            _customCache.Clear();
            return await _productColorDal.CreateProductColor(model);
        }

        public async Task<BaseRep<string>> Delete(int ID)
        {
            _customCache.Clear();
            return await _productColorDal.Delete(ID);
        }

        public async Task<BaseRep<List<ProductColorModel>>> GetAll()
        {
            return await _productColorDal.GetAll();
        }

        public async Task<BaseRep<ProductColorModel>> GetByID(int ID)
        {

            return await _productColorDal.GetByID(ID);
        }

        public async Task<BaseRep<string>> Update(int ID, ProductColorModel model)
        {
            _customCache.Clear();
            return await _productColorDal.UpdateProductColor(ID, model);
        }
    }
}
