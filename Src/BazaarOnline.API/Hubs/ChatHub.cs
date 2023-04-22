using BazaarOnline.Application.Interfaces.Hubs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace BazaarOnline.API.Hubs;

[Authorize]
public class ChatHub : Hub<IChatHub>
{
    public override async Task OnConnectedAsync()
    {
        await base.OnConnectedAsync();

        var groupName = Context.User.Identity.Name;
        await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
    }

    public async Task SendMessage(string jsonData)
    {
        await Clients.Caller.ReceiveMessage(jsonData);
    }
}