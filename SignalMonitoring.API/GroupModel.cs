using System;

namespace SignalMonitoring.API
{
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
}
