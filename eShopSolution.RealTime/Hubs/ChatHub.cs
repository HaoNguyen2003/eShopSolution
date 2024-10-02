using eShopSolution.RealTime.DataService;
using eShopSolution.RealTime.Models;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.RealTime.Hubs
{
    public class ChatHub:Hub
    {
        private readonly ShareDb _shareDb;
        public ChatHub(ShareDb shareDb)
        {
            _shareDb = shareDb;
        }
        public async Task JoinChat(UserConnection connection)
        {
            await Clients.All.SendAsync("ReceiveMessage","admin",$"{connection.Username} has joined");
        }
        public async Task JoinSpecificChatRoom(UserConnection connection)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, connection.ChatRoom);
            _shareDb.Connection[Context.ConnectionId] = connection;
            await Clients.Group(connection.ChatRoom).SendAsync("JoinSpecificChatRoom", "admin", $"{connection.Username} has joined ${connection.ChatRoom}");
        }
        public async Task SendMessage(string msg)
        {
            if(_shareDb.Connection.TryGetValue(Context.ConnectionId, out UserConnection conn))
            {
                await Clients.Group(conn.ChatRoom).SendAsync("ReceiveSpecificMessage",conn.Username,msg);
            }
        }
    }
}
