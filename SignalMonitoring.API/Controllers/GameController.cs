using Microsoft.AspNetCore.Mvc;
using SignalMonitoring.API.Managers;
using SignalMonitoring.API.Services;
using SolrEngine;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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
				Duration = 60,
				Id = Guid.NewGuid(),
				Term = m_termService.Term
			};

			return g;
		}

		// GET: api/Game/5
		[HttpGet("{id}", Name = "Get")]
		public IActionResult Get(Guid id)
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
			var solr = new Solr();

			foreach (var answer in game.Answers)
			{
				var isValid = await solr.ValidateAnswer(answer, game.Term);
				answer.PointsAchieved = !string.IsNullOrEmpty(isValid) ? 1 : 0;
			}
			
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
