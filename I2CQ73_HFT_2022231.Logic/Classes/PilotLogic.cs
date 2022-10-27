using I2CQ73_HFT_2022231.Models;
using I2CQ73_HFT_2022231.Repository;
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;

namespace I2CQ73_HFT_2022231.Logic
{
	public class PilotLogic : IPilotLogic
	{
		IRepository<Pilot> pilotRepo;
		IRepository<Team> teamRepo;
		IRepository<Car> carRepo;

		public PilotLogic(IRepository<Pilot> pilotRepo, IRepository<Team> teamRepo, IRepository<Car> carRepo)
		{
			this.pilotRepo = pilotRepo;
			this.teamRepo = teamRepo;
			this.carRepo = carRepo;
		}

		public void Create(Pilot item)
		{
			this.pilotRepo.Create(item);
		}

		public void Delete(int id)
		{
			this.pilotRepo.Delete(id);
		}

		public Pilot Read(int id)
		{
			return this.pilotRepo.Read(id);
		}

		public IQueryable<Pilot> ReadAll()
		{
			return this.pilotRepo.ReadAll();
		}

		public void Update(Pilot item)
		{
			this.pilotRepo.Update(item);
		}

		public IEnumerable<object> LeclersCarStatistics()
		{
			return from p in this.pilotRepo.ReadAll()
				   from t in this.teamRepo.ReadAll()
				   where p.TeamId == t.TeamId && p.PilotName.Equals("Charles Leclerc")
				   select new
				   {
					   Name = p.PilotName,
					   Team = t.TeamName,
					   Speed = carRepo.Read(t.CarId).MaxSpeed,
				   };
		}

		public IEnumerable<object> YoungestPilotsEngineBrand()
		{
			return from p in this.pilotRepo.ReadAll()
				   from t in this.teamRepo.ReadAll()
				   let minAge = pilotRepo.ReadAll().Min(x => x.PilotAge)
				   where p.TeamId == t.TeamId && p.PilotAge == minAge
				   select new
				   {
					   Name = p.PilotName,
					   EngineBrand = carRepo.Read(p.TeamId),
				   };
		}

		public IEnumerable<object> Pilots1040HorsePower()
		{
			return from c in this.carRepo.ReadAll()
				   from t in this.teamRepo.ReadAll()
				   from p in this.pilotRepo.ReadAll()
				   where c.Horsepower == 1040 && c.CarId == t.CarId && p.TeamId == t.TeamId
				   select new
				   {
					   Name = p.PilotName,
				   };
		}
		public IEnumerable<object> MercedesBrandTeamPointsAbove200Pilots()
		{
			return from c in this.carRepo.ReadAll()
				   from t in this.teamRepo.ReadAll()
				   from p in this.pilotRepo.ReadAll()
				   where c.EngineBrand == "Mercedes" && c.CarId == t.TeamId && t.TeamPoints > 200 && p.TeamId == t.TeamId
				   select new
				   {
					   Name = p.PilotName,
				   };
		}
		public IEnumerable<object> YoungerThan30PilotsTeamBudgetAbove150MMaxSpeed()
		{
			return from p in this.pilotRepo.ReadAll()
				   from t in this.teamRepo.ReadAll()
				   from c in this.carRepo.ReadAll()
				   where p.PilotAge < 30 && p.TeamId == t.TeamId && t.Budget > 150000000 && t.CarId == c.CarId
				   select new
				   {
					   Name = p.PilotName,
					   MaxSpeed = c.MaxSpeed,
				   };
		}
	}
}
