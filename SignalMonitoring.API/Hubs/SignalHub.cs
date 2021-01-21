using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using SignalMonitoring.API.Managers;
using SignalMonitoring.API.Persistence;

namespace SignalMonitoring.API.Hubs
{
    public class SignalHub : Hub
    {
        public async Task JoinGroup(GroupModel group, string userName)
        {
            if (!GamesManager.Games.Contains(group.Id))
            {
				await CreateNewGameAsync(group, userName);
            }
            else
            {
                var player = new Player(Context.ConnectionId, userName);
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

        private async Task CreateNewGameAsync(GroupModel group, string userName)
		{
            await Groups.AddToGroupAsync(Context.ConnectionId, group.Name);

            var player = new Player(Context.ConnectionId, userName);

            GamesManager.Games.CreateNewGame(group, player);
        }

        public async Task StartSingleGame(string userName)
        {
            using var context = new MainDbContext();
            var term = context.Terms.Find(new Random().Next(1, context.Terms.Count()));

            var g = new GroupModel
            {
                Duration = 60,
                Id = Guid.NewGuid(),
                Term = term.Term,
                Name = userName
            };

			await CreateNewGameAsync(g, userName);

            await Clients.Group(g.Name).SendCoreAsync("StartGame", new object[] { g });
        }
    }
}
