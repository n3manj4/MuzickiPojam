using Microsoft.AspNetCore.SignalR;

namespace SignalMonitoring.API.Hubs
{
    public class SignalHub : Hub
    {
        public void AddToGroup(string name)
        {
            Groups.AddToGroupAsync(Context.ConnectionId, name);
        }
    }
}
