using eShopSolution.BusinessLayer.Abstract;
using eShopSolution.DataLayer.Abstract;
using eShopSolution.DataLayer.EntityFramework;
using eShopSolution.DtoLayer.Model;
using eShopSolution.DtoLayer.RepositoryModel;
using eShopSolution.DtoLayer.RequestModel;
using eShopSolution.EntityLayer.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.BusinessLayer.Service
{
    public class OrderService : IOrderService
    {
        private readonly IOrderDal _orderDal;
        private readonly IProductColorDal _productColorDal;
        private readonly IProductSizeInventoryDal _productSizeInventoryDal;
        private readonly IProductService _productService;
        private readonly IDetailOrderService _detailOrderService;
        private readonly ISizeService _sizeService;
        private readonly IColorService _colorService;
        private readonly IAddressDeliveryServive _addressDeliveryServive;

        public OrderService(IOrderDal orderDal,IProductColorDal productColorDal,
            IProductSizeInventoryDal productSizeInventoryDal,
            IProductService productService,IDetailOrderService detailOrderService,
            ISizeService sizeService,IColorService colorService,IAddressDeliveryServive addressDeliveryServive) {
            _orderDal=orderDal;
            _productColorDal=productColorDal;
            _productSizeInventoryDal=productSizeInventoryDal;
            _productService=productService;
            _detailOrderService=detailOrderService;
            _sizeService = sizeService;
            _colorService = colorService;
            _addressDeliveryServive=addressDeliveryServive;
        }
        public async Task<Response<VnPaymentResquestModel>> CreateOrder(OrderModel orderModel)
        {
           
            double SubTotal = 0;
            int weight = 0;
            var resultOrder = await _orderDal.CreateOrder(orderModel);
            orderModel.OrderID = resultOrder.Value;
            if(!resultOrder.IsSuccess)
                return new Response<VnPaymentResquestModel>() { IsSuccess=false,Error= resultOrder.Error };
            foreach (var detail in orderModel.detailCarts)
            {
                var ProductSizeInventory = await _productSizeInventoryDal.GetProductSizeInventoryByProductColorIDAndSizeID(detail.ProductColorID, detail.SizeID);
                var Product = await _productService.GetByID(detail.ProductID);
                if(Product.code != 200)
                    return new Response<VnPaymentResquestModel>() { IsSuccess = false, Error = $"No product available", Value = new VnPaymentResquestModel() {OrderId = resultOrder.Value} };
                if (ProductSizeInventory.code != 200)
                    return new Response<VnPaymentResquestModel>() { IsSuccess = false, Error = $"No product available", Value = new VnPaymentResquestModel() { OrderId = resultOrder.Value } };
                var price = (double)(Product.Value.PriceOut * (1 - Product.Value.Discount/100));
                ProductSizeInventory.Value.Quantity = ProductSizeInventory.Value.Quantity - detail.Quantity;
                
                if (ProductSizeInventory.Value.Quantity < 0) {
                    var Size = await _sizeService.GetByID(detail.SizeID);
                    return new Response<VnPaymentResquestModel>() { IsSuccess = false, Error = $"Insufficient quantity of product: {Product.Value.Name} | Size: {Size.Value.SizeName} | Color: {detail.ProductColorID}", Value = new VnPaymentResquestModel() { OrderId = resultOrder.Value } };
                }
                var result = await _productSizeInventoryDal.Update(ProductSizeInventory.Value.ID, ProductSizeInventory.Value);
                if (result.code != 200)
                    Console.WriteLine(result.ToString());
                SubTotal += detail.Quantity * price;
                weight += 500;
                var detailOrderModel = new DetailOrderModel() { OrderID = resultOrder.Value, ProductSizeInventoryID = ProductSizeInventory.Value.ID, Amount = detail.Quantity, Price = price, TotalPrice = detail.Quantity * price };
                var resultDetailOrder = await _detailOrderService.Create(detailOrderModel);
                if(resultDetailOrder.code!=200)
                    return new Response<VnPaymentResquestModel>() { IsSuccess = false, Error = resultDetailOrder.code + "", Value = new VnPaymentResquestModel() { OrderId = resultOrder.Value } };
            }
            double FeeShip = await _addressDeliveryServive.GetFeeShip(new CalculateFeeShipModel() {service_id= 53320, insurance_value = 0 , height = 15,length=15,width=15, weight=weight }, orderModel.AddressID,orderModel.UserID);
            var UpdateOrder = await _orderDal.UpdateOrder(orderModel, FeeShip, SubTotal, SubTotal+ FeeShip);
            if (!UpdateOrder.IsSuccess)
                return new Response<VnPaymentResquestModel>() { IsSuccess = false, Error = UpdateOrder.Error + "", Value = new VnPaymentResquestModel() { OrderId = resultOrder.Value}};
            return new Response<VnPaymentResquestModel>() { IsSuccess=true, Value = new VnPaymentResquestModel() { OrderId = resultOrder.Value,totalPrice = SubTotal + FeeShip , FullName ="" ,CreateDate = DateTime.Now, Description =""} };
        }
        public async Task UpdateQualityProductInStore(int OrderID)
        {
            var listDetailOrderModels = await _detailOrderService.GetDetailOrderModelsByOrderID(OrderID); 
            if (listDetailOrderModels.Count == 0) return;
            foreach(var item in listDetailOrderModels)
            {
                var ProductSizeInventoryResult = await _productSizeInventoryDal.GetByID(item.ProductSizeInventoryID);
                if(ProductSizeInventoryResult.code == 200)
                {
                    var productSizeInventory = ProductSizeInventoryResult.Value;
                    productSizeInventory.Quantity += item.Amount;
                    await _productSizeInventoryDal.Update(item.ProductSizeInventoryID, productSizeInventory);
                }
                else
                {
                    Console.WriteLine("Loi");
                }
            }
        }
        public async Task<Response<Order>> UpdateOrder(OrderModel orderModel, double FeeShip, double subTotal, double total)
        {
            return await _orderDal.UpdateOrder(orderModel, FeeShip, subTotal, total);
        }
        public async Task<Response<OrderModel>> ConfirmPayMent(int OrderID,string UserID, int StatusID)
        {
            return await _orderDal.ConfirmPayMent(OrderID,UserID,StatusID);
        }
        public async Task<Response<OrderModel>> ConfirmPayMent(int OrderID, int StatusID)
        {
            return await _orderDal.ConfirmPayMent(OrderID, StatusID);
        }
        public async Task<List<int>> getListIDOfOrderPending(DateTime timeout)
        {
            return await _orderDal.getListIDOfOrderPending(timeout);
        }

        public async Task DeleteOrder(int OrderID)
        {
            var listDetailOrderModels = await _detailOrderService.GetDetailOrderModelsByOrderID(OrderID);
            if (listDetailOrderModels.Count == 0) return;
            foreach (var item in listDetailOrderModels)
            {
                var ProductSizeInventoryResult = await _productSizeInventoryDal.GetByID(item.ProductSizeInventoryID);
                if (ProductSizeInventoryResult.code == 200)
                {
                    var productSizeInventory = ProductSizeInventoryResult.Value;
                    productSizeInventory.Quantity += item.Amount;
                    await _productSizeInventoryDal.Update(item.ProductSizeInventoryID, productSizeInventory);
                }
                else
                {
                    Console.WriteLine("Loi");
                }
                await _detailOrderService.Delete(item.ID);
            }
            await _orderDal.DeleteOrder(OrderID);

        }
        public async Task<OrderModel> GetByID(string UserID,int OrderID)
        {
            return await _orderDal.GetByID(UserID, OrderID);
        }
    }
}
