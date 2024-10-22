using AutoMapper;
using eShopSolution.DataLayer.Abstract;
using eShopSolution.DataLayer.Context;
using eShopSolution.DtoLayer.AddModel;
using eShopSolution.DtoLayer.Model;
using eShopSolution.EntityLayer.Data;
using Microsoft.EntityFrameworkCore;

namespace eShopSolution.DataLayer.EntityFramework
{
    public class ProductDal : GenericDal<ProductModel, Product>, IProductDal
    {
        public ProductDal(ApplicationContext context, IMapper mapper) : base(context, mapper)
        {
        }
        public async Task<int> CreateProduct(ProductModel model)
        {
            try
            {
                var product = _mapper.Map<Product>(model);
                await _context.products.AddAsync(product);
                await _context.SaveChangesAsync();
                return product.ID;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }
        public async Task<ProductDashBoard> GetProductDashBoardByID(int ID)
        {
            var product = await _context.products.FindAsync(ID);
            return _mapper.Map<ProductDashBoard>(product);
        }
        public async Task<List<ProductCardModel>> GetAllProductConvertToProductCardModel()
        {
            var listProduct = await _context.products
                                .OrderByDescending(p => p.ID)
                                .ToListAsync();
            return _mapper.Map<List<ProductCardModel>>(listProduct);
        }

        public async Task<DetailProduct> GetDetailProductByID(int ID)
        {
            var product = await _context.products.FindAsync(ID);
            return _mapper.Map<DetailProduct>(product);
        }

        public async Task<Tuple<int, List<ProductCardModel>>> GetProductByFillterAndPage(FilterModel model, int Page, int Limit)
        {
            var skip = (Page - 1) * Limit;
            var take = Limit;
            IQueryable<Product> query = _context.products.AsQueryable();
            if (!string.IsNullOrWhiteSpace(model.TextSearch))
            {
                query = query.Where(p => p.Name.ToLower().Contains(model.TextSearch.ToLower()) || p.Title.ToLower().Contains(model.TextSearch.ToLower()));
            }
            if (model.ListBrandID.Count != 0)
            {
                query = query.Where(p => model.ListBrandID.Contains(p.BrandID));
            }
            if (model.ListCategoryID.Count != 0)
            {
                query = query.Where(p => model.ListCategoryID.Contains(p.CategoryID));
            }
            if (model.ListGenderID.Count != 0)
            {
                query = query.Where(p => model.ListGenderID.Contains(p.GenderID));
            }

            if (model.ListColorID.Count != 0 || model.ListSizeID.Count != 0)
            {
                query = query
                    .Join(
                        _context.ProductColors,
                        p => p.ID,
                        pc => pc.ProductID,
                        (p, pc) => new { Product = p, ProductColor = pc}
                    )
                    .Join(
                    _context.ColorCombinations,       
                    pc => pc.ProductColor.ColorCombinationID, 
                    cc => cc.ID,                       
                    (pc, cc) => new { pc.Product, ProductColor = pc.ProductColor, ColorCombination = cc }
                    )
                    .Join(
                    _context.ColorCombinationColors,   
                    cc => cc.ColorCombination.ID,      
                    ccc => ccc.ColorCombinationID,    
                    (cc, ccc) => new { cc.Product, cc.ProductColor, cc.ColorCombination, ColorCombinationColor = ccc })
                    .Join(
                    _context.Colors,
                    ccc => ccc.ColorCombinationColor.ColorID, 
                    c => c.ID,                         
                    (ccc, c) => new { ccc.Product, ccc.ProductColor, ccc.ColorCombination, Color = c }
                    )
                    .Where(joined =>
                    model.ListColorID.Contains(joined.Color.ID) ||  
                    !model.ListColorID.Any())
                    .Join(
                        _context.productSizeInventories,
                        j => j.ProductColor.ID,
                        s => s.ProductColorID,
                        (j, s) => new { j.Product, j.ProductColor, ProductSizeInventory = s }
                    )
                    .Where(joined => model.ListSizeID.Contains(joined.ProductSizeInventory.SizeID) || model.ListSizeID.Count == 0)
                    .Select(joined => joined.Product);
            }
            query = query.Distinct();
            int TotalItem = query.Count();
            var pagedProducts = await query
               .Skip(skip)
               .Take(take)
               .ToListAsync();
            var productQuery = pagedProducts.AsQueryable();
            if (model.SortByPrice)
            {
                productQuery = productQuery.OrderBy(p => p.PriceOut * (100 - p.Discount) / 100);
            }
            return Tuple.Create<int, List<ProductCardModel>>(TotalItem, _mapper.Map<List<ProductCardModel>>(productQuery));
        }
    }
}
