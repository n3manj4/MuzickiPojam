using System;
using System.Collections.Generic;
using System.Linq;
using SolrEngine;

namespace SignalMonitoring.API
{
    public class SingleGame
    {
        //Conflict comment
        //another line on repo
        public List<AnswerModel> Answers { get; set; } = new List<AnswerModel>();
        public string Term { get; set; }
    }
    public class Game
    {
        public Game()
        {
        }

        public Game(GroupModel group, Player player, string term)
        {
            Id = group.Id;

            RedTeam = new Team(group.MaxPlayers);
            BlueTeam = new Team(group.MaxPlayers);
            Room = group;
            Room.Term = term;

            IncreaseTeamNumber(group.Team, player);
        }

        public Team BlueTeam { get; set; }
        public Guid Id { get; set; }
        public Team RedTeam { get; set; }
        public GroupModel Room { get; set; }

        public void IncreaseTeamNumber(TeamEnum team, Player player)
        {
            if (team is TeamEnum.Blue)
            {
                BlueTeam.Players.Add(player);
                Room.BluePlayersCount++;
            }
            else
            {
                RedTeam.Players.Add(player);
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
    }

    public class GroupModel
    {
        public int BluePlayersCount { get; set; }
        public int Duration { get; set; }
        public Guid Id { get; set; } = Guid.NewGuid();
        public int MaxPlayers { get; set; }
        public string Name { get; set; }

        public int NoOfPlayers
        {
            get => RedPlayersCount + BluePlayersCount;
        }

        public int Position { get; set; }
        public int RedPlayersCount { get; set; }
        public TeamEnum Team { get; set; }
        public string Term { get; set; }
    }

    public enum TeamEnum
    {
        Red,
        Blue
    }

    public class Team
    {
        public Team(int maxPlayers)
        {
            MaxPlayers = maxPlayers / 2;
        }

        public List<AnswerModel> Answers { get; set; } = new List<AnswerModel>();
        public int MaxPlayers { get; }

        public List<Player> Players { get; } = new List<Player>();

        public void AddPlayer(Player player)
        {
            if (MaxPlayers > Players.Count)
            {
                Players.Add(player);
            }
        }
    }

    public class Player
    {
        public Player(string id, string name)
        {
            Id = id;
            Name = name;
        }

        public string Id { get; set; }

        public string Name { get; set; }
    }
}
