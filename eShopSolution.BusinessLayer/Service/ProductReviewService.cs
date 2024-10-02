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
    public class ProductReviewService : IProductReviewService
    {
        private readonly IProductReviewDal _productReviewDal;
        private readonly IOrderService _orderService;
        private readonly IDetailOrderService _detailOrderService;
        private readonly IProductSizeInventoryDal _productSizeInventoryDal;
        private readonly IProductColorDal _productColorDal;

        public ProductReviewService(IProductReviewDal productReviewDal, IOrderService orderService, IDetailOrderService detailOrderService, IProductSizeInventoryDal productSizeInventoryDal,IProductColorDal productColorDal)
        {
            _productReviewDal = productReviewDal;
            _orderService = orderService;
            _detailOrderService = detailOrderService;
            _productSizeInventoryDal = productSizeInventoryDal;
            _productColorDal = productColorDal;
        }
        public async Task<BaseRep<string>> Create(ProductReviewModel model)
        {
            return await _productReviewDal.Create(model);
        }

        public async Task<Response<ProductReviewModel>> CreateProductReview(ProductReviewModel ProductReview)
        {
            var DetailOrder = await _detailOrderService.GetByID(ProductReview.DetailOrderID);
            if(DetailOrder.code!=200)
                return new Response<ProductReviewModel>() { IsSuccess = false, Error = "You can't review this product because your detail order was not found." };
            var Order = await _orderService.GetByID(ProductReview.UserID, DetailOrder.Value.OrderID);
            if (Order == null)
                return new Response<ProductReviewModel>() { IsSuccess = false, Error = "You can't review this product because your order was not found." };
            if(Order.OrderStatusID==7)//chinh lai khac 6
                return new Response<ProductReviewModel>() { IsSuccess = false, Error = "You can't review this product because your order Not Complete." };
            var ResultProductSizeInventory = await _productSizeInventoryDal.GetByID(DetailOrder.Value.ProductSizeInventoryID);
            if(ResultProductSizeInventory.code!=200)
                return new Response<ProductReviewModel>() { IsSuccess = false, Error = "You can't review this product because product no longer available." };
            var ResultProductColor = await _productColorDal.GetByID(ResultProductSizeInventory.Value.ProductColorID);
            if(ResultProductColor.code!=200)
                return new Response<ProductReviewModel>() { IsSuccess = false, Error = "You can't review this product because product no longer available." };
            if(ResultProductColor.Value.ProductID!=ProductReview.ProductID)
                return new Response<ProductReviewModel>() { IsSuccess = false, Error = "You can't review this product." };
            var result = await _productReviewDal.CreateProductReview(ProductReview);
            return result;
        }

        public async Task<BaseRep<string>> Delete(int ID)
        {
            return await _productReviewDal.Delete(ID);
        }

        public async Task<BaseRep<List<ProductReviewModel>>> GetAll()
        {
            return await _productReviewDal.GetAll();
        }

        public async Task<BaseRep<ProductReviewModel>> GetByID(int ID)
        {
            return await _productReviewDal.GetByID(ID);
        }

        public async Task<Response<PaginationProductReview>> GetProductReviewPage(int ProductID, int Page, int Size)
        {
            return await _productReviewDal.GetProductReviewPage(ProductID, Page, Size);
        }

        public async Task<BaseRep<string>> Update(int ID, ProductReviewModel model)
        {
            return await _productReviewDal.Update(ID, model);
        }
    }
}
