using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace CoreV2._2_WithAngular.Hubs
{
    public class ChatHub : Hub
    {
        public async Task NewMessage(string username, string message)
            => await Clients.Others.SendAsync("messageReceived", username, message);

        public override async Task OnConnectedAsync()
            => await Clients.All.SendAsync("broadcastMessage", "new connection", "anonim");
    }
}
