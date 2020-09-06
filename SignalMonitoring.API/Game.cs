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
}
