using eShopSolution.DtoLayer.Model;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.RealTime.Hubs
{
    public class ProductHub : Hub
    {
        public async Task NotifyAllClients(DetailProduct detailProduct)
        {
            try
            {
                await Clients.Group($"ProductGroup_{detailProduct.ID}").SendAsync("ReceiveProductUpdate", detailProduct);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception in NotifyAllClients: {ex.Message}");
            }
        }
        public async Task JoinProductGroup(int productId)
        {
            
            await Groups.AddToGroupAsync(Context.ConnectionId, $"ProductGroup_{productId}");
        }

        public async Task LeaveProductGroup(int productId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, $"ProductGroup_{productId}");
        }
    }
}
