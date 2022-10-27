using I2CQ73_HFT_2022231.Models;
using System.Collections.Generic;
using System.Linq;

namespace I2CQ73_HFT_2022231.Logic
{
	public interface IPilotLogic
	{
		void Create(Pilot item);
		void Delete(int id);
		IEnumerable<object> LeclersCarStatistics();
		Pilot Read(int id);
		IQueryable<Pilot> ReadAll();
		void Update(Pilot item);
	}
}