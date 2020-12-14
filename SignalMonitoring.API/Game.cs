using SolrEngine;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

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
        public GroupModel Room { get; set; }
        public Guid Id { get; set; }
        public string Term { get; set; }
        public Team RedTeam { get; set; }
        public Team BlueTeam { get; set; }

        public void IncreaseTeamNumber(TeamEnum team)
        {
            if (team is TeamEnum.Blue)
            {
                BlueTeam.Players.Add(new Player());
            }
            else
            {
                RedTeam.Players.Add(new Player());
            }

            Room.NoOfPlayers++;
        }
    }

    public class GroupModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public int Position { get; set; }
        public int Duration { get; set; }
        public int NoOfPlayers { get; set; }
        public int MaxPlayers { get; set; }
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

        public void AddPlayer(Player player)
        {
            if (MaxPlayers > Players.Count)
            {
                Players.Add(player);
            }
        }

        public List<Player> Players { get; } = new List<Player>();
        public int MaxPlayers { get; }
        public List<AnswerModel> Answers { get; set; } = new List<AnswerModel>();

    }

    public class Player
    {
        public string Id { get; set; }

        public string Name { get; set; }
    }
}
