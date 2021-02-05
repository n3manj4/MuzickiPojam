using System;
using System.Collections.Generic;
using System.Linq;
using SignalMonitoring.API.Managers;
using SolrEngine;

namespace SignalMonitoring.API
{
	public class Game
    {
        public Game(GroupModel group, Player player, string term)
        {
            Id = group.Id;

            RedTeam = new Team(group.MaxPlayers);
            BlueTeam = new Team(group.MaxPlayers);
            Room = group;
            Room.Term = term;

            IncreaseTeamNumber(group.Team, player);
        }
        public GroupManager Manager { get; } = new GroupManager();
        public Solr Solr { get; } = new Solr();

        public Team BlueTeam { get; set; }
        public Team RedTeam { get; set; }
        public string Id { get; set; }
        public GroupModel Room { get; set; }

        public bool IsAllProcessed
		{
            get
			{
                return BlueTeam.IsTeamProcessed && RedTeam.IsTeamProcessed;
			}
		}

        public string Term => Room.Term;

        public void IncreaseTeamNumber(TeamEnum team, Player player)
        {
            if (team is TeamEnum.Blue)
            {
                BlueTeam.AddPlayer(player);
                Room.BluePlayersCount++;
            }
            else
            {
                RedTeam.AddPlayer(player);
                Room.RedPlayersCount++;
            }
        }

        public IEnumerable<string> GetRedTeamIds()
        {
            return RedTeam.Players.Select(x => x.Id);
        }

        public IReadOnlyList<string> GetBlueTeamIds()
        {
            return BlueTeam.Players.Select(x => x.Id).ToList();
        }

        public Player GetPlayer(string id)
		{
            var player = BlueTeam.GetPlayer(id);
            if (player != null)
			{
                return player;
			}

            player = RedTeam.GetPlayer(id);

            if (player != null)
            {
                return player;
            }

            return new Player();
		}
    }
}
