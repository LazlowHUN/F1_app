using I2CQ73_HFT_2022231.Logic;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace I2CQ73_HFT_2022231.Endpoint.Controllers
{
	[Route("[controller]/[action]")]
	[ApiController]
	public class StatController : ControllerBase
	{
		IPilotLogic logic;

		public StatController(IPilotLogic logic)
		{
			this.logic = logic;
		}

		[HttpGet]
		public IEnumerable<NameTeamSpeed> LeclersCarStatistics()
		{
			return this.logic.LeclersCarStatistics();
		}

		[HttpGet]
		public IEnumerable<NameEngineBrand> YoungestPilotsEngineBrand()
		{
			return this.logic.YoungestPilotsEngineBrand();
		}

		[HttpGet]
		public IEnumerable<NameEngineBrand> Pilots1040HorsePower()
		{
			return this.logic.Pilots1040HorsePower();
		}

		[HttpGet]
		public IEnumerable<NameEngineBrand> MercedesBrandTeamPointsAbove200Pilots()
		{
			return this.logic.MercedesBrandTeamPointsAbove200Pilots();
		}

		[HttpGet]
		public IEnumerable<NameTeamSpeed> YoungerThan30PilotsTeamBudgetAbove150MMaxSpeed()
		{
			return this.logic.YoungerThan30PilotsTeamBudgetAbove150MMaxSpeed();
		}
	}
}
