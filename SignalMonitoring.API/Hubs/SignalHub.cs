using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using SignalMonitoring.API.Managers;

namespace SignalMonitoring.API.Hubs
{
    public class SignalHub : Hub
    {
        public async Task JoinGroup(GroupModel group)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, group.Name);

            if (!GamesManager.Games.Contains(group.Id))
            {
                GamesManager.Games.CreateNewGame(group);
            }
            else
            {
                GamesManager.Games.AddToRoom(group);

                var g = GamesManager.Games[group.Id];

                if (g != null && group.MaxPlayers == g.Room.NoOfPlayers)
                {
                    await Clients.Group(group.Name).SendCoreAsync("StartGame", new object[]
                    {
                        g.Room.Duration
                    });
                }
            }

            await Clients.All.SendCoreAsync("GroupReceived", new object[]
            {
                GamesManager.Games[group.Id]
            });
        }
    }
}
