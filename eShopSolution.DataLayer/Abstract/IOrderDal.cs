using eShopSolution.DtoLayer.Model;
using eShopSolution.DtoLayer.RepositoryModel;
using eShopSolution.EntityLayer.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.DataLayer.Abstract
{
    public interface IOrderDal
    {
        public Task<Response<int>> CreateOrder(OrderModel orderModel);
        public Task<Response<Order>> UpdateOrder(OrderModel orderModel,double FeeShip,double subTotal,double total);
        public Task<Response<OrderModel>> ConfirmPayMent(int OrderID,string UserID,int StatusID);
        public Task<Response<OrderModel>> ConfirmPayMent(int OrderID, int StatusID);
        public Task<List<int>> getListIDOfOrderPending(DateTime timeout);
        public Task DeleteOrder(int ID);
        public Task<OrderModel> GetByID(string UserID, int OrderID);
    }
}
