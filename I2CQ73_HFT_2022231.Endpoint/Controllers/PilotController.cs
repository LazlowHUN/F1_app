using I2CQ73_HFT_2022231.Endpoint.Services;
using I2CQ73_HFT_2022231.Logic;
using I2CQ73_HFT_2022231.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace I2CQ73_HFT_2022231.Endpoint.Controllers
{
	[Route("[controller]")]
	[ApiController]
	public class PilotController : ControllerBase
	{
		IPilotLogic logic;
		IHubContext<SignalRHub> hub;

		public PilotController(IPilotLogic logic, IHubContext<SignalRHub> hub)
		{
			this.logic = logic;
			this.hub = hub;
		}

		// GET: api/<PilotController>
		[HttpGet]
		public IEnumerable<Pilot> Get()
		{
			return this.logic.ReadAll();
		}

		// GET api/<PilotController>/5
		[HttpGet("{id}")]
		public Pilot Get(int id)
		{
			return this.logic.Read(id);
		}

		// POST api/<F1Controller>
		[HttpPost]
		public void Post([FromBody] Pilot value)
		{
			this.logic.Create(value);
			this.hub.Clients.All.SendAsync("PilotCreated", value);
		}

		// PUT api/<F1Controller>/5
		[HttpPut]
		public void Put([FromBody] Pilot value)
		{
			this.logic.Update(value);
			this.hub.Clients.All.SendAsync("PilotUpdated", value);
		}

		// DELETE api/<F1Controller>/5
		[HttpDelete("{id}")]
		public void Delete(int id)
		{
			var pilotToDelete = this.logic.Read(id);
			this.logic.Delete(id);
			this.hub.Clients.All.SendAsync("PilotDeleted", pilotToDelete);
		}
	}
}
