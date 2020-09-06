using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        // GET: api/Game
        [HttpGet]
        public Game Get()
        {
            var game = new Game()
            {
                Id = Guid.NewGuid(), Term = m_termService.Term
            };
            GamesManager.Instance.Games.Add(game);
            return game;
        }

        // GET: api/Game/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Game
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Game value)
        {
            await Task.Run(() =>
            {
                foreach (var answer in value.Answers)
                {
                    answer.IsCorrectAnswer = Solr.ValidateAnswer(answer, value.Term);
                }
            });

            return Ok(value);
        }

        // PUT: api/Game/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
