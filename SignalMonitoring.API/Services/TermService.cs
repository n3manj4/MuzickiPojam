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
            var term = string.Empty;
            try
            {
                var count = m_mainDbContext.Terms.Count();
                var item = m_mainDbContext.Terms.Find(m_random.Next(1, count));

                term = "jedna";//item.Term;
            }
            catch (Exception e)
            { 
                Console.WriteLine(e);
            }

            return term;
        }

        public string Term { get; set; }
    }
}
