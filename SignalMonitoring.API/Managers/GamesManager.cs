using System;
using System.Collections.Generic;
using System.Linq;
using SignalMonitoring.API.Persistence;

namespace SignalMonitoring.API.Managers
{
    public class GamesManager
    {
        private static GamesManager s_instance;
        private static readonly object s_padlock = new object();
        private Dictionary<Guid, Game> m_games = new Dictionary<Guid, Game>();

        private GamesManager()
        {
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

        public Game this[Guid id]
        {
            get
            {
                m_games.TryGetValue(id, out var game);
                return game;
            }
            set => m_games[id] = value;
        }

        public void AddToRoom(GroupModel groupModel, Player player)
        {
            var g = this[groupModel.Id];

            this[g.Id].IncreaseTeamNumber(groupModel.Team, player);
        }

        public IEnumerable<GroupModel> AllRooms()
        {
            return m_games.Select(x => x.Value.Room);
        }

        public bool Contains(Guid id)
        {
            return m_games.ContainsKey(id);
        }

        public void CreateNewGame(GroupModel group, Player player)
        {
            using var context = new MainDbContext();
            var term = context.Terms.Find(new Random().Next(1, context.Terms.Count()));

            m_games.Add(group.Id, new Game(group, player, term.Term));
        }
    }
}
