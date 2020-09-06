using System;
using System.Linq;
using Newtonsoft.Json;
using SignalMonitoring.API.Persistence;

namespace SignalMonitoring.API.Services
{
    public class TermService : ITermService
    {
        private readonly MainDbContext m_mainDbContext;
        private Random m_random = new Random();

        public TermService(MainDbContext mainDbContext)
        {
            m_mainDbContext = mainDbContext;
            Term = GetRandomTerm();
        }

        public string GetRandomTerm()
        {
            var count = m_mainDbContext.Terms.Count();
           
            var item = m_mainDbContext.Terms.Find(m_random.Next(1, count));
            return "violina"; //item.Term;
        }

        public string Term { get; set; }
    }
}
