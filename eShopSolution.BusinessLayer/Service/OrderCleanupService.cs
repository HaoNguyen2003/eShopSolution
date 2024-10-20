using eShopSolution.BusinessLayer.Abstract;
using eShopSolution.DataLayer.Context;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.BusinessLayer.Service
{
    public class OrderCleanupService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        public OrderCleanupService(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _scopeFactory.CreateScope())
                {
                    
                    var _orderService = scope.ServiceProvider.GetRequiredService<IOrderService>();
                    var timeout = DateTime.Now.AddMinutes(-10);

                    var pendingOrders = await _orderService.getListIDOfOrderPending(timeout);

                    foreach (var ID in pendingOrders)
                    {
                        await _orderService.ConfirmPayMent(ID,6);
                        await _orderService.UpdateQualityProductInStore(ID);
                    }
                }
                await Task.Delay(150000, stoppingToken);
            }
        }
    }
}
