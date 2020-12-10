using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace SignalMonitoring.API.Hubs
{
    public delegate void ClientGroupEventHandler(GroupModel group);

    public class SignalHub : Hub
    {
        public static event ClientGroupEventHandler ClientJoinedToGroup;
        public static event ClientGroupEventHandler ClientLeftGroup;

        public async Task JoinGroup(GroupModel group)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, group.Name);

            ClientJoinedToGroup?.Invoke(group);

            await Clients.All.SendCoreAsync("GroupReceived", new object[]
            {
                GamesManager.Instance.Groups
            });

        }
    }
}
