using eShopSolution.DtoLayer.Model;
using eShopSolution.DtoLayer.RepositoryModel;
using eShopSolution.DtoLayer.RequestModel;
using eShopSolution.EntityLayer.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.BusinessLayer.Abstract
{
    public interface IOrderService
    {
        public Task<Response<VnPaymentResquestModel>> CreateOrder(OrderModel orderModel);
        public Task<Response<Order>> UpdateOrder(OrderModel orderModel, double FeeShip, double subTotal, double total);
        public Task UpdateQualityProductInStore(int OrderID);
        public Task<Response<OrderModel>> ConfirmPayMent(int OrderID,string UserID,int StatusID);
        public Task<Response<OrderModel>> ConfirmPayMent(int OrderID, int StatusID);
        public Task<List<int>> getListIDOfOrderPending(DateTime timeout);
        public Task DeleteOrder(int OrderID);
        public Task<OrderModel> GetByID(string UserID, int OrderID);
        /*public Task<Response<string>> GetAllOrder(string UserID);
        public Task<Response<string>> GetlOrderByID(int ID,string UserID);*/
    }
}
