using Microsoft.AspNetCore.SignalR;

namespace PopeyeClub.Hubs
{
    public class ChatHub : Hub
    {
        public string GetConnectionId()
        {
            return Context.ConnectionId;
        } 
    }
}
