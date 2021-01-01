using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using SignalMonitoring.API.Managers;

namespace SignalMonitoring.API.Hubs
{
    public class SignalHub : Hub
    {
        public async Task JoinGroup(GroupModel group, string userName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, group.Name);

            var player = new Player(Context.ConnectionId, userName);

            if (!GamesManager.Games.Contains(group.Id))
            {
                GamesManager.Games.CreateNewGame(group, player);
            }
            else
            {
                GamesManager.Games.AddToRoom(group, player);

                var g = GamesManager.Games[group.Id];

                if (g != null && group.MaxPlayers == g.Room.NoOfPlayers)
                {
                    await Clients.Group(group.Name).SendCoreAsync("StartGame", new object[]{ g.Room });
                }
            }

            await Clients.All.SendCoreAsync("GroupReceived", new object[]
            {
                GamesManager.Games[group.Id]
            });
        }

        public async Task StartSingleGame(GroupModel group, string userName)
        {
            var player = new Player(null, userName);
            GamesManager.Games.CreateNewGame(group, player);
        }
    }
}
