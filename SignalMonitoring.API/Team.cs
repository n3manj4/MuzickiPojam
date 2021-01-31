using System.Collections.Generic;
using System.Linq;

namespace SignalMonitoring.API
{
	public class Team
    {
        public Team(int maxPlayers)
        {
            MaxPlayers = maxPlayers / 2;
        }

        public int MaxPlayers { get; }

        public List<Player> Players { get; } = new List<Player>();

        public void AddPlayer(Player player)
        {
            if (MaxPlayers > Players.Count)
            {
                Players.Add(player);
            }
        }

        public Player GetPlayer(string id)
		{
            return Players.Find(x => x.Id == id);
		}

		public bool IsTeamProcessed => !Players.Any(x => !x.Processed);
	}
}
