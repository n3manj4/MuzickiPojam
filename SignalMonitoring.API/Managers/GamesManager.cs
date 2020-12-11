using SignalMonitoring.API.Hubs;
using System.Collections.Generic;
using System.Linq;

namespace SignalMonitoring.API
{
    public class GamesManager
    {
        private static GamesManager s_instance;
        private static readonly object s_padlock = new object();


        private GamesManager()
        {
            SignalHub.ClientJoinedToGroup += SignalHubOnClientJoinedToGroup;
        }

        private void SignalHubOnClientJoinedToGroup(GroupModel groupModel)
        {
            var g = Groups.FirstOrDefault(x => x.Name == groupModel.Name);

            if (g is null)
            {
                var group = new GroupModel
                {
                    Duration = groupModel.Duration,
                    MaxPlayers = groupModel.MaxPlayers,
                    Name = groupModel.Name,
                    NoOfPlayers = 1,
                    Position = Groups.Count + 1
                };
                Groups.Add(group);
            }
            else
            {
                g.NoOfPlayers++;
            }
        }

        public static GamesManager Instance
        {
            get
            {
                lock (s_padlock)
                {
                    return s_instance ??= new GamesManager();
                }
            }
        }

        public List<Game> Games { get; set; } = new List<Game>();

        public List<GroupModel> Groups { get; set; } = new List<GroupModel>();
    }
}
