using Microsoft.AspNetCore.SignalR;
using SignalRCore.Hubs;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRChat.Hubs
{
    public class ChatHub : Hub
    {
        public static ConnectionMapping<string> connections = new ConnectionMapping<string>();

        public void Dummy(string user)
        {
            string key = user;
            connections.Add(key, Context.ConnectionId);
        }
        public async Task SendMessage(string user, string message)
        {
            HashSet<string> connectedUsers = new HashSet<string>();
            string key = user;
            foreach (var connectionId in connections.GetConnections(key))
            {
                connectedUsers.Add(connectionId);
            }
            // Send Messages to all users
            await Clients.All.SendAsync("ReceiveMessage", user, message);
            // Send messages to specific users
            // await Clients.Clients(connectedUsers.ToList()).SendAsync("ReceiveMessage", user, message);
        }
    }
}