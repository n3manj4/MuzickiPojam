using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using SignalMonitoring.API.Persistence;
using SignalMonitoring.API.Services;

namespace SignalMonitoring.API.Controllers
{
    [Route("api/[controller]"), ApiController]
    public class HomeController : ControllerBase
    {
        private ITermService m_termService;

        public HomeController(ITermService termService)
        {
            m_termService = termService;
        }

        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> GetTerms()
        {
            return Ok(m_termService.GetRandomTerm());
        }

        [HttpPost("finish")]
        public void Finish(List<AnswerModel> answers)
        {
            
        }
    }
}