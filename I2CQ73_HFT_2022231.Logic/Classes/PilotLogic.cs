using Castle.DynamicProxy.Generators.Emitters.SimpleAST;
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

		public IEnumerable<NameTeamSpeed> LeclersCarStatistics()
		{
			return from p in this.pilotRepo.ReadAll()
				   from t in this.teamRepo.ReadAll()
				   from c in this.carRepo.ReadAll()
				   where p.TeamId == t.TeamId && p.PilotName.Equals("Charles Leclerc") && c.CarId == t.CarId
				   select new NameTeamSpeed()
				   {
					   Name = p.PilotName,
					   Team = t.TeamName,
					   Speed = c.MaxSpeed,
				   };
			//where p.PilotName == "Charles Leclerc"
			//select new NameTeamSpeed
			//{
			// Name = p.PilotName,
			// Team = p.Team.TeamName,
			// Speed = p.Team.Car.MaxSpeed,
			//};
		}

		public IEnumerable<NameEngineBrand> YoungestPilotsEngineBrand()
		{
			return from p in this.pilotRepo.ReadAll()
				   from t in this.teamRepo.ReadAll()
				   from c in this.carRepo.ReadAll()
				   let minAge = pilotRepo.ReadAll().Min(x => x.PilotAge)
				   where p.TeamId == t.TeamId && p.PilotAge == minAge && c.CarId == t.CarId
				   select new NameEngineBrand()
				   {
					   Name = p.PilotName,
					   EngineBrand = c.EngineBrand,
				   };
		}

		public IEnumerable<NameEngineBrand> Pilots1040HorsePower()
		{
			return from c in this.carRepo.ReadAll()
				   from t in this.teamRepo.ReadAll()
				   from p in this.pilotRepo.ReadAll()
				   where c.Horsepower == 1040 && c.CarId == t.CarId && p.TeamId == t.TeamId
				   select new NameEngineBrand()
				   {
					   Name = p.PilotName,
					   EngineBrand = c.EngineBrand,
				   };
		}
		public IEnumerable<NameEngineBrand> MercedesBrandTeamPointsAbove200Pilots()
		{
			return from c in this.carRepo.ReadAll()
				   from t in this.teamRepo.ReadAll()
				   from p in this.pilotRepo.ReadAll()
				   where c.EngineBrand == "Mercedes" && c.CarId == t.TeamId && t.TeamPoints > 200 && p.TeamId == t.TeamId
				   select new NameEngineBrand()
				   {
					   Name = p.PilotName,
					   EngineBrand = c.EngineBrand,
				   };
		}
		public IEnumerable<NameTeamSpeed> YoungerThan30PilotsTeamBudgetAbove150MMaxSpeed()
		{
			return from p in this.pilotRepo.ReadAll()
				   from t in this.teamRepo.ReadAll()
				   from c in this.carRepo.ReadAll()
				   where p.PilotAge < 30 && p.TeamId == t.TeamId && t.Budget > 150000000 && t.CarId == c.CarId
				   select new NameTeamSpeed()
				   {
					   Name = p.PilotName,
					   Team = t.TeamName,
					   Speed = c.MaxSpeed,
				   };
		}
	}

	public class NameTeamSpeed
	{
		public string Name { get; set; }
		public string Team { get; set; }
		public double Speed { get; set; }

		public override bool Equals(object obj)
		{
			NameTeamSpeed a = obj as NameTeamSpeed;
			if (a == null)
			{
				return false;
			}
			else
			{
				return this.Name == a.Name && this.Team == a.Team && this.Speed == a.Speed;
			}
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(this.Name, this.Team, this.Speed);
		}
	}
	public class NameEngineBrand
	{
		public string Name { get; set; }
		public string EngineBrand { get; set; }

		public override bool Equals(object obj)
		{
			NameEngineBrand a = obj as NameEngineBrand;
			if (a == null)
			{
				return false;
			}
			else
			{
				return this.Name == a.Name && this.EngineBrand == a.EngineBrand;
			}
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(this.Name, this.EngineBrand);
		}
	}
}
