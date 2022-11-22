using I2CQ73_HFT_2022231.Logic;
using I2CQ73_HFT_2022231.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace I2CQ73_HFT_2022231.Endpoint.Controllers
{
	[Route("[controller]")]
	[ApiController]
	public class CarController : ControllerBase
	{
		ICarLogic logic;

		public CarController(ICarLogic logic)
		{
			this.logic = logic;
		}

		// GET: api/<PilotController>
		[HttpGet]
		public IEnumerable<Car> Get()
		{
			return this.logic.ReadAll();
		}

		// GET api/<PilotController>/5
		[HttpGet("{id}")]
		public Car Get(int id)
		{
			return this.logic.Read(id);
		}

		// POST api/<F1Controller>
		[HttpPost]
		public void Post([FromBody] Car value)
		{
			this.logic.Create(value);
		}

		// PUT api/<F1Controller>/5
		[HttpPut]
		public void Put([FromBody] Car value)
		{
			this.logic.Update(value);
		}

		// DELETE api/<F1Controller>/5
		[HttpDelete("{id}")]
		public void Delete(int id)
		{
			this.logic.Delete(id);
		}
	}
}
