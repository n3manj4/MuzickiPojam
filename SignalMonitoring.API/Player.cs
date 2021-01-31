namespace SignalMonitoring.API
{
	public class Player
    {
        public Player(string id, string name, TeamEnum team)
        {
            Id = id;
            Name = name;
            Team = team;
        }

        public string Id { get; }

        public string Name { get; }

        public TeamEnum Team { get; }

        public bool Processed { get; set; } = false;
    }
}
