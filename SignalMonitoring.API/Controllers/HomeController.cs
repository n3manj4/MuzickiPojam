using Microsoft.AspNetCore.Mvc;
using SignalMonitoring.API.Services;
using SolrEngine;
using System.Threading.Tasks;

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
		public IActionResult GetTerms()
		{
			m_term = m_termService.Term;
			return Ok(m_term);
		}

		[HttpPost]
        public void Finish(Game game)
        {
            var solr = new Solr();
            foreach (var answer in game.Manager.RedAnswers)
            {
				_ = solr.ValidateAnswer(answer, game.Room.Term);
            }
        }
    }
}