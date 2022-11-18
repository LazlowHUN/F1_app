using I2CQ73_HFT_2022231.Models;
using I2CQ73_HFT_2022231.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace I2CQ73_HFT_2022231.Logic
{
	public class TeamLogic : ITeamLogic
	{
		IRepository<Team> teamRepo;

		public TeamLogic(IRepository<Team> teamRepo)
		{
			this.teamRepo = teamRepo;
		}

		public void Create(Team item)
		{
			if (item.TeamName.Length < 3)
			{
				throw new ArgumentException("The name is too short.");
			}
			else if (item.TeamName.Length > 240)
			{
				throw new ArgumentException("The name is too long.");
			}
			else
			{
				this.teamRepo.Create(item);
			}
		}

		public void Delete(int id)
		{
			var car = this.teamRepo.Read(id);
			if (car == null)
			{
				throw new ArgumentException("Team do not exists!");
			}
			this.teamRepo.Delete(id);
		}

		public Team Read(int id)
		{
			var car = this.teamRepo.Read(id);
			if (car == null)
			{
				throw new ArgumentException("Team do not exists!");
			}
			return car;
		}

		public IQueryable<Team> ReadAll()
		{
			return this.teamRepo.ReadAll();
		}

		public void Update(Team item)
		{
			if (item == null)
			{
				throw new NullReferenceException("Team do not exists!");
			}
			else
			{
				this.teamRepo.Update(item);
			}
		}
	}
}
