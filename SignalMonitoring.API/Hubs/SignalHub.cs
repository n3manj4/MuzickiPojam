using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using SignalMonitoring.API.Managers;
using SignalMonitoring.API.Models;
using SignalMonitoring.API.Persistence;
using SolrEngine;

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
                await Groups.AddToGroupAsync(Context.ConnectionId, group.Name);

                var player = new Player(Context.ConnectionId, userName, group.Team);
                GamesManager.Games.AddToRoom(group, player);

                var g = GamesManager.Games[group.Id];

                if (g != null && group.MaxPlayers == g.Room.NoOfPlayers)
                {
                    await Clients.Group(group.Name).SendAsync("StartGame", g.Room);
                }
            }

            await Clients.All.SendAsync("GroupReceived", GamesManager.Games[group.Id]);
        }

        private async Task CreateNewGameAsync(GroupModel group, string userName)
		{
            await Groups.AddToGroupAsync(Context.ConnectionId, group.Name);

            var player = new Player(Context.ConnectionId, userName, group.Team);

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

            await Clients.Group(g.Name).SendAsync("StartGame", g);
        }

        public async Task ValidateAnswers(string gameId, List<AnswerModel> answers)
		{
            var guid = Guid.Parse(gameId);
            var game = GamesManager.Games[guid];
            var player = game.GetPlayer(Context.ConnectionId);

            foreach(var answer in answers)
			{
                answer.Id = await game.Solr.ValidateAnswer(answer, game.Term);

                game.Manager.AddAndAssignPoints(answer, player.Team);
			}

            player.Processed = true;

            if (game.IsAllProcessed)
			{
                var redResults = GetResultsFor(game.Manager.RedAnswers);
                var blueResults = GetResultsFor(game.Manager.BlueAnswers);

                await Clients.Group(game.Room.Name).SendCoreAsync("GetResults", new object[]
                {
                    redResults,
                    blueResults
                });
			}
		}

        private List<ResultModel> GetResultsFor(List<AnswerModel> answers)
		{
            var results = new List<ResultModel>();
            if (answers is null)
			{
                return results;
			}

            foreach (var result in answers)
            {
                if (!string.IsNullOrEmpty(result.Lyric))
                {
                    results.Add(new ResultModel { Answer = result.Lyric, Points = result.PointsAchieved });
                }
                else
                {
                    results.Add(new ResultModel { Answer = result.Singer + " - " + result.Title, Points = result.PointsAchieved });
                }
            }

            return results;
        }
    }
}
