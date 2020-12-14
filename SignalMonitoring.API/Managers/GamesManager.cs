using System;
using SignalMonitoring.API.Hubs;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SignalMonitoring.API.Persistence;

namespace SignalMonitoring.API
{
    public class GamesManager
    {
        private static GamesManager s_instance;
        private static readonly object s_padlock = new object();

        public Game this[Guid id]
        {
            get => m_games[id];
            set => m_games[id] = value;
        }


        private GamesManager()
        {
        }
        private Dictionary<Guid, Game> m_games = new Dictionary<Guid, Game>();

        public bool Contains(Guid id)
        {
            return m_games.ContainsKey(id);
        }

        public void AddToRoom(GroupModel groupModel)
        {
            var g = this[groupModel.Id];

            this[g.Id].IncreaseTeamNumber(g.Room.Team);
        }

        public static GamesManager Games
        {
            get
            {
                lock (s_padlock)
                {
                    return s_instance ??= new GamesManager();
                }
            }
        }

        public IEnumerable<GroupModel> AllRooms()
        {
            return m_games.Select(x => x.Value.Room);
        }


        public void CreateNewGame(GroupModel group)
        {
            using var context = new MainDbContext();
            var term = context.Terms.Find(new Random().Next(1, context.Terms.Count()));

            m_games.Add(group.Id, new Game(group, term.Term));
        }
    }
}
