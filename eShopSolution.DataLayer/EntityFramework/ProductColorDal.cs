using AutoMapper;
using eShopSolution.DataLayer.Abstract;
using eShopSolution.DataLayer.Context;
using eShopSolution.DtoLayer.Model;
using eShopSolution.DtoLayer.RepositoryModel;
using eShopSolution.EntityLayer.Data;
using Microsoft.EntityFrameworkCore;

namespace eShopSolution.DataLayer.EntityFramework
{
    public class ProductColorDal : GenericDal<ProductColorModel, ProductColors>, IProductColorDal
    {
        public ProductColorDal(ApplicationContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<BaseRep<string>> CreateProductColor(ProductColorModel model)
        {
            try
            {
                var check = await _context.ProductColors
               .Where(cb => cb.ProductID == model.ProductID && cb.ColorID == model.ColorID)
               .ToListAsync();
                if (check.Count > 0)
                {
                    return new BaseRep<string>() { code = 500, Value = "The link between the brand and category already exists" };
                }
                else
                {
                    var productColors = _mapper.Map<ProductColors>(model);
                    await _context.ProductColors.AddAsync(productColors);
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

        public async Task<int> CreateProductColorReturnID(ProductColorModel model)
        {
            try
            {
                var check = await _context.ProductColors
               .Where(cb => cb.ProductID == model.ProductID && cb.ColorID == model.ColorID)
               .ToListAsync();
                if (check.Count > 0)
                {
                    return -1;
                }
                else
                {
                    var ProductColor = _mapper.Map<ProductColors>(model);
                    await _context.ProductColors.AddAsync(ProductColor);
                    await _context.SaveChangesAsync();
                    return ProductColor.ID;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                return -1;
            }
        }
        public async Task<List<int>> GetColorIDByProductID(int productID)
        {
            List<ProductColorModel> productColorModels =
                _mapper.Map<List<ProductColorModel>>((await _context.ProductColors.Where(p => p.ProductID == productID).ToListAsync()));
            List<int> colorIDs = new List<int>();
            foreach (var item in productColorModels)
            {
                colorIDs.Add(item.ColorID);
            }
            return colorIDs;
        }

        public async Task<int> GetProductColorByProductIDAndColorID(int productID, int colorID)
        {
            var productColor = await _context.ProductColors
            .FirstOrDefaultAsync(p => p.ProductID == productID && p.ColorID == colorID);
            if (productColor == null)
            {
                return -1;
            }

            return productColor.ID;
        }

        public async Task<BaseRep<string>> UpdateProductColor(int ID, ProductColorModel model)
        {
            try
            {
                var checkID = await _context.ProductColors.FindAsync(ID);
                if (checkID == null) { return new BaseRep<string>() { code = 404, Value = "Not Found Link" }; }
                var check = await _context.ProductColors
                .Where(cb => cb.ProductID == model.ProductID && cb.ColorID == model.ColorID && cb.ID != ID)
                .ToListAsync();
                if (check.Count > 0)
                {
                    return new BaseRep<string>() { code = 500, Value = "The link between the brand and category already exists" };
                }
                else
                {
                    _mapper.Map(model, checkID);
                    await _context.SaveChangesAsync();
                    return new BaseRep<string>() { code = 200, Value = "Update success" };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                return new BaseRep<string>() { code = 500, Value = "Update fail" };
            }
        }
    }
}
