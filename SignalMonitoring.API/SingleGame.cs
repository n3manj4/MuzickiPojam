using System.Collections.Generic;
using SolrEngine;

namespace SignalMonitoring.API
{
	public class SingleGame
    {
        public List<AnswerModel> Answers { get; set; } = new List<AnswerModel>();
        public string Term { get; set; }
    }
}
