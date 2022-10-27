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
		IRepository<Pilot> pilotRepo;
		IRepository<Team> teamRepo;
		IRepository<Car> carRepo;

		public TeamLogic(IRepository<Pilot> pilotRepo, IRepository<Team> teamRepo, IRepository<Car> carRepo)
		{
			this.pilotRepo = pilotRepo;
			this.teamRepo = teamRepo;
			this.carRepo = carRepo;
		}

		public void Create(Team item)
		{
			this.teamRepo.Create(item);
		}

		public void Delete(int id)
		{
			this.teamRepo.Delete(id);
		}

		public Team Read(int id)
		{
			return this.teamRepo.Read(id);
		}

		public IQueryable<Team> ReadAll()
		{
			return this.teamRepo.ReadAll();
		}

		public void Update(Team item)
		{
			this.teamRepo.Update(item);
		}
	}
}
