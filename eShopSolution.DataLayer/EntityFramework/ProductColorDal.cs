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
            try {
            
                    var productColors = _mapper.Map<ProductColors>(model);
                    await _context.ProductColors.AddAsync(productColors);
                    await _context.SaveChangesAsync();
                    return new BaseRep<string>() { code = 200, Value = "Create success" };
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
                    var ProductColor = _mapper.Map<ProductColors>(model);
                    await _context.ProductColors.AddAsync(ProductColor);
                    await _context.SaveChangesAsync();
                    return ProductColor.ID;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                return -1;
            }
        }

        public async Task<List<int>> GetColorIDByProductColorID(int productColorID)
        {
             var colorCombinationId = await _context.ProductColors
            .Where(pc => pc.ID == productColorID)
            .Select(pc => pc.ColorCombinationID)
            .FirstOrDefaultAsync();
             
            var colorIDs = await _context.ColorCombinationColors.Where(s=>s.ColorCombinationID == colorCombinationId)
                    .Select(s=>s.ColorID).ToListAsync();
            return colorIDs;
        }

        public async Task<List<int>> GetColorIDByProductID(int productID)
        {
            List<ProductColorModel> productColorModels =
            _mapper.Map<List<ProductColorModel>>((await _context.ProductColors.Where(p => p.ProductID == productID).ToListAsync()));
            List<int> colorIDs = new List<int>();
            foreach (var item in productColorModels)
            {
                colorIDs.Add(item.ColorCombinationID);
            }
            return colorIDs;
        }

        public async Task<int> GetProductColorByProductIDAndColorID(int productID, int colorID)
        {
            var productColor = await _context.ProductColors
            .FirstOrDefaultAsync(p => p.ProductID == productID && p.ColorCombinationID == colorID);
            if (productColor == null)
            {
                return -1;
            }

            return productColor.ID;
        }

        public async Task<List<int>> GetProductColorIDByProductID(int productID)
        {
            var ListProductColorIDs = await _context.ProductColors.Where(s=>s.ProductID == productID).Select(s=>s.ID).ToListAsync();
            return ListProductColorIDs;
        }

        public async Task<BaseRep<string>> UpdateProductColor(int ID, ProductColorModel model)
        {
            try
            {
                var checkID = await _context.ProductColors.FindAsync(ID);
                if (checkID == null) { return new BaseRep<string>() { code = 404, Value = "Not Found Link" }; }
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
