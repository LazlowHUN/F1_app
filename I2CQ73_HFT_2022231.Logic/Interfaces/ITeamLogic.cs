using I2CQ73_HFT_2022231.Models;
using System.Linq;

namespace I2CQ73_HFT_2022231.Logic
{
	public interface ITeamLogic
	{
		void Create(Team item);
		void Delete(int id);
		Team Read(int id);
		IQueryable<Team> ReadAll();
		void Update(Team item);
	}
}