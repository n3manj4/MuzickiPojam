using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace SignalMonitoring.API.Hubs
{
    public delegate void ClientGroupEventHandler(GroupModel group);

    public class SignalHub : Hub
    {
        public async Task JoinGroup(GroupModel group)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, group.Name);

            if (!GamesManager.Games.Contains(group.Id))
            {
                GamesManager.Games.CreateNewGame(group);
                await Clients.All.SendCoreAsync("GroupReceived", new object[]
                {
                    group
                });
            }
            else
            {
                GamesManager.Games.AddToRoom(group);

                var g = GamesManager.Games[group.Id];

                if (g != null && group.MaxPlayers == g.Room.NoOfPlayers)
                {
                    await Clients.Group(group.Name).SendCoreAsync("StartGame", new object[] { g.Room.Duration });
                }
            }


        }
    }
}
