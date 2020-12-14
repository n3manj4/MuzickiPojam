using SignalMonitoring.API.Persistence;
using System;
using System.Linq;

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
            try
            {
                var count = m_mainDbContext.Terms.Count();
                var item = m_mainDbContext.Terms.Find(m_random.Next(1, count));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return "violina"; //item.Term;
        }

        public string Term { get; set; }
    }
}
