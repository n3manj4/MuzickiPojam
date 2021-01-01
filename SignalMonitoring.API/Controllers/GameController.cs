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
        public GroupModel Get()
        {
            var g = new GroupModel
            {
                Duration = 60, Id = Guid.NewGuid(), Term = "violina"
            };

            return g;
        }

        // GET: api/Game/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<IActionResult> Get(Guid id)
        {
            var game = GamesManager.Games[id];
            if (game is null)
            {
                return NotFound("Game not found");
            }

            return Ok(game);

        }

        // POST: api/Game
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] SingleGame game)
        {
            await Task.Run(() =>
            {
                foreach (var answer in game.Answers)
                {
                    answer.IsCorrectAnswer = Solr.ValidateAnswer(answer, game.Term);
                }
            });

            return Ok(game.Answers);
        }

        // PUT: api/Game/5
        [HttpPut("{id}")]
        public void Put(Guid id, [FromBody] string value)
        {
            GamesManager.Games[id].Room.Term = value;
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
