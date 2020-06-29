using SignalMonitoring.API.Models;
using SignalMonitoring.API.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SignalMonitoring.API.Services
{
    public class TermService : ITermService
    {
        private readonly MainDbContext m_mainDbContext;

        public TermService(MainDbContext mainDbContext)
        {
            m_mainDbContext = mainDbContext;
        }

        public string  GetRandomTerm()
        {
            var item = m_mainDbContext.Terms.Find(1);
            return item.Terms;
        }

    }
}
