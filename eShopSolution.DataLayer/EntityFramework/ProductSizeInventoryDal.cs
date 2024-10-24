using AutoMapper;
using eShopSolution.DataLayer.Abstract;
using eShopSolution.DataLayer.Context;
using eShopSolution.DtoLayer.Model;
using eShopSolution.DtoLayer.RepositoryModel;
using eShopSolution.EntityLayer.Data;
using Microsoft.EntityFrameworkCore;

namespace eShopSolution.DataLayer.EntityFramework
{
    public class ProductSizeInventoryDal : GenericDal<DetailQuantityProductModel, ProductSizeInventory>, IProductSizeInventoryDal
    {
        public ProductSizeInventoryDal(ApplicationContext context, IMapper mapper) : base(context, mapper)
        {
        }
        public async Task<BaseRep<string>> DeleteProductSizeInventory(int ProductColorID, int SizeID)
        {
            try
            {
                var checkID = await _context.productSizeInventories.FirstOrDefaultAsync(p => p.ProductColorID == ProductColorID && p.SizeID == SizeID);
                if (checkID == null) { return new BaseRep<string>() { code = 404, Value = "Not Found Link" }; }
                _context.productSizeInventories.Remove(checkID);
                await _context.SaveChangesAsync();
                return new BaseRep<string>() { code = 200, Value = "Delete success" };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                return new BaseRep<string>() { code = 500, Value = "Delete fail :" + ex.Message };
            }
        }

        public async Task<List<DetailQuantityProductModel>> GetAllDetailQuantityProducByProductColorID(int ID)
        {
            var list = await _context.productSizeInventories.Where(x => x.ProductColorID == ID).ToListAsync();
            return _mapper.Map<List<DetailQuantityProductModel>>(list);
        }

        public async Task<BaseRep<DetailQuantityProductModel>> GetProductSizeInventoryByProductColorIDAndSizeID(int ProductColorID, int SizeID)
        {
            var result = await _context.productSizeInventories.FirstOrDefaultAsync(p => p.ProductColorID == ProductColorID && p.SizeID == SizeID);
            if (result == null)
                return new BaseRep<DetailQuantityProductModel> { code = 404, Value = null };
            return new BaseRep<DetailQuantityProductModel> { code = 200, Value = _mapper.Map<DetailQuantityProductModel>(result)};
        }
    }
}
