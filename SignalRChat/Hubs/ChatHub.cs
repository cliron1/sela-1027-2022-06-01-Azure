using Microsoft.AspNetCore.SignalR;

namespace SignalRChat.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(string user, string message)
        {
            //await Clients.All.SendAsync("ReceiveMessage", user, message);
            await Clients.Caller.SendAsync("ReceiveMessage", true, user, message);

            await Clients.Others.SendAsync("ReceiveMessage", false, user, message);
        }
    }
}