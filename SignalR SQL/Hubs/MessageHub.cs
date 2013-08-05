using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using SignalR_SQL.Models;

public class MessageHub : Hub
{
    public static void Show()
    {
        IHubContext context = GlobalHost.ConnectionManager.GetHubContext<MessageHub>();
        context.Clients.All.displayMessages();
    }
}