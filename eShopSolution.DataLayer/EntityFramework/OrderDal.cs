using AutoMapper;
using eShopSolution.DataLayer.Abstract;
using eShopSolution.DataLayer.Context;
using eShopSolution.DtoLayer.Model;
using eShopSolution.DtoLayer.RepositoryModel;
using eShopSolution.EntityLayer.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.DataLayer.EntityFramework
{
    public class OrderDal : IOrderDal
    {
        private readonly ApplicationContext _context;
        private readonly IMapper _mapper;

        public OrderDal(ApplicationContext context, IMapper mapper) {
            _context = context;
            _mapper = mapper;
        }
        public async Task<Response<int>> CreateOrder(OrderModel orderModel)
        {
            try
            {
                orderModel.OrderStatusID = 1;
                orderModel.PaymentMethodID = 2;
                var order = _mapper.Map<Order>(orderModel);
                await _context.orders.AddAsync(order);
                await _context.SaveChangesAsync();
                return new Response<int>() { IsSuccess = true, Value = order.OrderID };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new Response<int>() { IsSuccess = true, Error = ex.Message };
            }
        }

        public async Task<Response<Order>> UpdateOrder(OrderModel orderModel, double FeeShip, double subTotal, double total)
        {
            try
            {
                var order = await _context.orders.FindAsync(orderModel.OrderID);
                order.Subtotal = subTotal;
                order.Total = total;
                order.FeeShip = FeeShip;
                await _context.SaveChangesAsync();
                return new Response<Order>() { IsSuccess = true, Value = order };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new Response<Order>() { IsSuccess = false, Error = ex.Message };
            }

        }
        public async Task<Response<OrderModel>> ConfirmPayMent(int OrderID,string UserID, int StatusID)
        {
            try
            {
                var order = await _context.orders.FindAsync(OrderID);
                if(order.UserID!=UserID)
                    return new Response<OrderModel>() { IsSuccess = false, Error = "You Not Permission Confirm Payment"};
                order.OrderStatusID = StatusID;
                await _context.SaveChangesAsync();
                return new Response<OrderModel>() { IsSuccess = true, Value = _mapper.Map<OrderModel>(order) };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new Response<OrderModel>() { IsSuccess = false, Error = ex.Message };
            }
        }
        public async Task<Response<OrderModel>> ConfirmPayMent(int OrderID, int StatusID)
        {
            try
            {
                var order = await _context.orders.FindAsync(OrderID);
                order.OrderStatusID = StatusID;
                await _context.SaveChangesAsync();
                return new Response<OrderModel>() { IsSuccess = true, Value = _mapper.Map<OrderModel>(order) };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new Response<OrderModel>() { IsSuccess = false, Error = ex.Message };
            }
        }
        public async Task<List<int>> getListIDOfOrderPending(DateTime timeout)
        {
            var pendingOrderIDs = await _context.orders
                       .Where(o => o.OrderStatusID == 1 && o.OrderDate < timeout)
                       .Select(o => o.OrderID)
                       .ToListAsync();
            return pendingOrderIDs;
        }
        public async Task DeleteOrder(int ID)
        {
            try
            {
                var order = await _context.orders.FindAsync(ID);
                _context.orders.Remove(order);
                await _context.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
            }


        }

        public async Task<OrderModel> GetByID(string UserID, int OrderID)
        {

            var order = await _context.orders
                .Where(x => x.OrderID == OrderID && x.UserID == UserID)
                .Select(x => new OrderModel
                {
                    OrderID = x.OrderID,
                    UserID = x.UserID,
                    AddressID = x.AddressID,
                    OrderStatusID = x.OrderStatusID,
                    PaymentMethodID = x.PaymentMethodID,
                    ShippingProviderID= x.ShippingProviderID,
                })
                .FirstOrDefaultAsync();

            if (order == null)
                return null;

            return order;
        }
    }
}
