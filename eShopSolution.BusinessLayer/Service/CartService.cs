using eShopSolution.BusinessLayer.Abstract;
using eShopSolution.DataLayer.Abstract;
using eShopSolution.DtoLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.BusinessLayer.Service
{
    public class CartService : ICartService
    {
        private readonly IProductService _productService;
        private readonly IProductColorDal _productColorDal;
        private readonly IProductSizeInventoryService _productSizeInventoryService;
        public CartService(IProductService productService, IProductColorDal productColorDal, IProductSizeInventoryService productSizeInventoryService)
        {
            _productService = productService;
            _productColorDal = productColorDal;
            _productSizeInventoryService = productSizeInventoryService;
        }

        public async Task<DetailProduct> UpdateDetailProductByProductIDAndProductColorID(DetailCart cart)
        {
            //var productColorID = await _productColorDal.GetProductColorByProductIDAndColorID(cart.ProductID, cart.ColorID);
            var DetailQuantityProductModel = await _productSizeInventoryService.GetProductSizeInventoryByProductColorIDAndSizeID(cart.ProductColorID,cart.SizeID);
            await _productSizeInventoryService.Update(0,new DetailQuantityProductModel() { ID = 0, ProductColorID = cart.ProductColorID, Quantity =DetailQuantityProductModel.Value.Quantity-cart.Quantity, SizeID = cart.SizeID });
            var result = await _productService.GetDetailProductByProductIDAndProductColorID(cart.ProductID, cart.ProductColorID);
            return result.Value;
        }
    }
}
