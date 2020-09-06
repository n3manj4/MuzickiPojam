using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using SignalMonitoring.API.Persistence;
using SignalMonitoring.API.Services;
using SolrEngine;

namespace SignalMonitoring.API.Controllers
{
    [Route("api/[controller]"), ApiController]
    public class HomeController : ControllerBase
    {
        private ITermService m_termService;
        private string m_term;

        public HomeController(ITermService termService)
        {
            m_termService = termService;
        }

        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> GetTerms()
        {
            m_term = m_termService.Term;
            return Ok(m_term);
        }

        [HttpPost]
        public void Finish(Game game)
        {
            foreach (var answer in game.Answers)
            {
                Solr.ValidateAnswer(answer, game.Term);
            }
        }
    }
}