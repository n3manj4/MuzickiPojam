using System;
using System.Collections.Generic;
using SolrEngine;

namespace SignalMonitoring.API
{
    public class Game
    {
        public Game()
        {
        }

        public Game(GroupModel group, string term)
        {
            Id = group.Id;

            RedTeam = new Team(group.MaxPlayers);
            BlueTeam = new Team(group.MaxPlayers);
            Room = group;
            Term = term;

            IncreaseTeamNumber(group.Team);
        }

        public Team BlueTeam { get; set; }
        public Team RedTeam { get; set; }
        public Guid Id { get; set; }
        public GroupModel Room { get; set; }
        public string Term { get; set; }

        public void IncreaseTeamNumber(TeamEnum team)
        {
            if (team is TeamEnum.Blue)
            {
                BlueTeam.Players.Add(new Player());
                Room.BluePlayersCount++;
            }
            else
            {
                RedTeam.Players.Add(new Player());
                Room.RedPlayersCount++;
            }
        }
    }

    public class GroupModel
    {
        public int Duration { get; set; }
        public Guid Id { get; set; } = Guid.NewGuid();
        public int MaxPlayers { get; set; }
        public string Name { get; set; }
        public int NoOfPlayers
        {
            get => RedPlayersCount + BluePlayersCount;
        }
        public int RedPlayersCount { get; set; }
        public int BluePlayersCount { get; set; }
        public int Position { get; set; }
        public TeamEnum Team { get; set; }
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
        public string Id { get; set; }

        public string Name { get; set; }
    }
}
