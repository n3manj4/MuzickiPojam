using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SignalMonitoring.API.Hubs;

namespace SignalMonitoring.API.Managers
{
    public class GroupManager
    {
        public GroupManager()
        {
            //SignalHub.ClientJoinedToGroup += SignalHub_ClientJoinedToGroup;
        }

        private void SignalHub_ClientJoinedToGroup(GroupModel group)
        {
            
        }
    }
}
