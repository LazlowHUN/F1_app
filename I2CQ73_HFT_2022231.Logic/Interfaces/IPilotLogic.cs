using I2CQ73_HFT_2022231.Models;
using System.Collections.Generic;
using System.Linq;

namespace I2CQ73_HFT_2022231.Logic
{
	public interface IPilotLogic
	{
		void Create(Pilot item);
		void Delete(int id);
		Pilot Read(int id);
		IQueryable<Pilot> ReadAll();
		void Update(Pilot item);
		IEnumerable<NameTeamSpeed> LeclersCarStatistics();
		IEnumerable<NameEngineBrand> YoungestPilotsEngineBrand();
		IEnumerable<NameEngineBrand> Pilots1040HorsePower();
		IEnumerable<NameEngineBrand> MercedesBrandTeamPointsAbove200Pilots();
		IEnumerable<NameTeamSpeed> YoungerThan30PilotsTeamBudgetAbove150MMaxSpeed();
	}
}