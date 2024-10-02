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
        public async Task<BaseRep<string>> CreateProductSizeInventory(DetailQuantityProductModel model)
        {
            try
            {
                var check = await _context.productSizeInventories
               .Where(cb => cb.SizeID == model.SizeID && cb.ProductColorID == model.ProductColorID)
               .ToListAsync();
                if (check.Count > 0)
                {
                    return new BaseRep<string>() { code = 500, Value = "The link between the brand and category already exists" };
                }
                else
                {
                    var productSizeInventory = _mapper.Map<ProductSizeInventory>(model);
                    await _context.productSizeInventories.AddAsync(productSizeInventory);
                    await _context.SaveChangesAsync();
                    return new BaseRep<string>() { code = 200, Value = "Create success" };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                return new BaseRep<string>() { code = 500, Value = ex.Message };
            }
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

        public async Task<BaseRep<string>> UpdateProductSizeInventory(DetailQuantityProductModel model)
        {
            try
            {
                var checkID = await _context.productSizeInventories.FirstOrDefaultAsync(p => p.ProductColorID == model.ProductColorID && p.SizeID == model.SizeID);
                if (checkID == null) { return new BaseRep<string>() { code = 404, Value = "Not Found Link" }; }
                model.ID = checkID.ProductSizeInventoryID;
                _mapper.Map(model, checkID);
                await _context.SaveChangesAsync();
                return new BaseRep<string>() { code = 200, Value = "Update success" };

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                return new BaseRep<string>() { code = 500, Value = "Update fail" };
            }
        }
    }
}
