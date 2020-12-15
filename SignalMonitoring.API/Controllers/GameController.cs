using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SignalMonitoring.API.Managers;
using SignalMonitoring.API.Services;
using SolrEngine;

namespace SignalMonitoring.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private ITermService m_termService;

        public GameController(ITermService termService)
        {
            m_termService = termService;
        }

        [HttpGet, Route("rooms")]
        public IEnumerable<GroupModel> GetRooms()
        {
            return GamesManager.Games.AllRooms();
        }
        // GET: api/Game
        [HttpGet]
        public Game Get()
        {
            return new Game();
        }

        // GET: api/Game/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Game
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Game value, TeamEnum team)
        {
            await Task.Run(() =>
            {
                if (team is TeamEnum.Blue)
                {
                    foreach (var answer in value.BlueTeam.Answers)
                    {
                        answer.IsCorrectAnswer = Solr.ValidateAnswer(answer, value.Term);
                    }
                }
                else
                {
                    foreach (var answer in value.RedTeam.Answers)
                    {
                        answer.IsCorrectAnswer = Solr.ValidateAnswer(answer, value.Term);
                    }
                }
            });

            return Ok(value);
        }

        // PUT: api/Game/5
        [HttpPut("{id}")]
        public void Put(Guid id, [FromBody] string value)
        {
            GamesManager.Games[id].Term = value;
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
