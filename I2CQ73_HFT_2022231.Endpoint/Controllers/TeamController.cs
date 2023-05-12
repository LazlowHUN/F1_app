using I2CQ73_HFT_2022231.Endpoint.Services;
using I2CQ73_HFT_2022231.Logic;
using I2CQ73_HFT_2022231.Models;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace I2CQ73_HFT_2022231.Endpoint.Controllers
{
	[Route("[controller]")]
	[ApiController]
	public class TeamController : ControllerBase
	{
		ITeamLogic logic;
		IHubContext<SignalRHub> hub;

		public TeamController(ITeamLogic logic, IHubContext<SignalRHub> hub)
		{
			this.logic = logic;
			this.hub = hub;
		}

		// GET: api/<PilotController>
		[HttpGet]
		public IEnumerable<Team> Get()
		{
			return this.logic.ReadAll();
		}

		// GET api/<PilotController>/5
		[HttpGet("{id}")]
		public Team Get(int id)
		{
			return this.logic.Read(id);
		}

		// POST api/<F1Controller>
		[HttpPost]
		public void Post([FromBody] Team value)
		{
			this.logic.Create(value);
			this.hub.Clients.All.SendAsync("TeamCreated", value);
		}

		// PUT api/<F1Controller>/5
		[HttpPut]
		public void Put([FromBody] Team value)
		{
			this.logic.Update(value);
			this.hub.Clients.All.SendAsync("TeamUpdated", value);
		}

		// DELETE api/<F1Controller>/5
		[HttpDelete("{id}")]
		public void Delete(int id)
		{
			var teamToDelete = this.logic.Read(id);
			this.logic.Delete(id);
			this.hub.Clients.All.SendAsync("TeamDeleted", teamToDelete);
		}
	}
}
