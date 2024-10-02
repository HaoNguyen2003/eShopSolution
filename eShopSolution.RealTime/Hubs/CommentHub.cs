using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.RealTime.Hubs
{
    public class CommentHub:Hub
    {
        public async Task JoinCommentProductGroup(int productId)
        {

            await Groups.AddToGroupAsync(Context.ConnectionId, $"ProductComment_{productId}");
        }

        public async Task LeaveCommentProductGroup(int productId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, $"ProductComment_{productId}");
        }
    }
}
