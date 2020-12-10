using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SolrEngine;

namespace SignalMonitoring.API
{
    public class Game
    {
        public Guid Id { get; set; }
        public string Term { get; set; }
        public List<AnswerModel> Answers { get; set; } = new List<AnswerModel>();
    }

    public class GroupModel
    {
        public string Name { get; set; }
        public int Position { get; set; }
        public int Duration { get; set; }
        public int NoOfPlayers { get; set; }
        public int MaxPlayers { get; set; }

    }

    class ClientItem 
    {
        public string Id { get; set; }

        public string Name { get; set; }
    }
}
