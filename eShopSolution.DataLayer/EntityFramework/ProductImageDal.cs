using AutoMapper;
using eShopSolution.DataLayer.Abstract;
using eShopSolution.DataLayer.Context;
using eShopSolution.DtoLayer.Model;
using eShopSolution.EntityLayer.Data;
using Microsoft.EntityFrameworkCore;

namespace eShopSolution.DataLayer.EntityFramework
{
    public class ProductImageDal : GenericDal<ProductImageModel, ProductImages>, IProductImageDal
    {
        public ProductImageDal(ApplicationContext context, IMapper mapper) : base(context, mapper)
        {
        }
        public async Task<List<string>> GetAllImageByProductColor(int ID)
        {
            var listImage = await _context.ProductImages.Where(x => x.ProductColorID == ID)
                .Select(x => x.ImageURL)
                .ToListAsync();
            return listImage;
        }
        public async Task<List<ProductImageModel>> GetAllProductImageByProductColor(int ID)
        {
            var listImage = await _context.ProductImages.Where(x => x.ProductColorID == ID)
                .ToListAsync();
            return _mapper.Map<List<ProductImageModel>>(listImage);
        }

        public async Task<List<string>> GetAllPublicIDByProductColor(int ID)
        {
            var listImage = await _context.ProductImages.Where(x => x.ProductColorID == ID)
                .Select(x => x.PublicID)
                .ToListAsync();
            return listImage;
        }

        public async Task<string> GetImagefirstByID(int ID)
        {
            var productImage = await _context.ProductImages
                                     .FirstOrDefaultAsync(p => p.ProductColorID == ID);
            return productImage?.ImageURL ?? "default-image-url.jpg";
        }
    }
}
