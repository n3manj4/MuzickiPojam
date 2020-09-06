using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalMonitoring.API
{
    public class GamesManager
    {
        private static GamesManager s_instance = null;
        private static readonly object s_padlock = new object();

        private GamesManager()
        {
        }

        public static GamesManager Instance
        {
            get
            {
                lock (s_padlock)
                {
                    if (s_instance == null)
                    {
                        s_instance = new GamesManager();
                    }
                    return s_instance;
                }
            }
        }

        public List<Game> Games
        {
            get;
            set;
        } = new List<Game>();
    }
}
