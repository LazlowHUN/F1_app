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
			if (item.PilotAge < 15 || item.PilotAge > 50)
			{
				throw new ArgumentOutOfRangeException("The age of pilots must be between 15 and 50.");
			}
			else
			{
				this.pilotRepo.Create(item);
			}		
		}

		public void Delete(int id)
		{
			var pilot = this.pilotRepo.Read(id);
			if (pilot == null)
			{
				throw new ArgumentException("Pilot do not exists!");
			}
			this.pilotRepo.Delete(id);
		}

		public Pilot Read(int id)
		{
			var pilot = this.pilotRepo.Read(id);
			if (pilot == null)
			{
				throw new ArgumentException("Pilot do not exists!");
			}
			return pilot;
		}

		public IQueryable<Pilot> ReadAll()
		{
			return this.pilotRepo.ReadAll();
		}

		public void Update(Pilot item)
		{
			if (item == null)
			{
				throw new NullReferenceException("Pilot do not exists!");
			}
			else
			{
				this.pilotRepo.Update(item);
			}
		}

		public IEnumerable<NameTeamSpeed> LeclersCarStatistics()
		{
			return from p in this.pilotRepo.ReadAll()
				   where p.PilotName == "Charles Leclerc"
				   select new NameTeamSpeed
				   {
					   Name = p.PilotName,
					   Team = p.Team.TeamName,
					   Speed = p.Team.Car.MaxSpeed,
				   };
		}

		public IEnumerable<NameEngineBrand> YoungestPilotsEngineBrand()
		{
			return from p in this.pilotRepo.ReadAll()
				   let minAge = pilotRepo.ReadAll().Min(x => x.PilotAge)
				   where p.PilotAge == minAge
				   select new NameEngineBrand()
				   {
					   Name = p.PilotName,
					   EngineBrand = p.Team.Car.EngineBrand,
				   };
		}

		public IEnumerable<NameEngineBrand> Pilots1040HorsePower()
		{
			return from p in this.pilotRepo.ReadAll()
				   where p.Team.Car.Horsepower == 1040
				   select new NameEngineBrand()
				   {
					   Name = p.PilotName,
					   EngineBrand = p.Team.Car.EngineBrand,
				   };
		}
		public IEnumerable<NameEngineBrand> MercedesBrandTeamPointsAbove200Pilots()
		{
			return from p in this.pilotRepo.ReadAll()
				   where p.Team.Car.EngineBrand == "Mercedes" && p.Team.TeamPoints > 200
				   select new NameEngineBrand()
				   {
					   Name = p.PilotName,
					   EngineBrand = p.Team.Car.EngineBrand,
				   };
		}
		public IEnumerable<NameTeamSpeed> YoungerThan30PilotsTeamBudgetAbove150MMaxSpeed()
		{
			return from p in this.pilotRepo.ReadAll()
				   where p.PilotAge < 30 &&  p.Team.Budget > 150000000
				   select new NameTeamSpeed()
				   {
					   Name = p.PilotName,
					   Team = p.Team.TeamName,
					   Speed = p.Team.Car.MaxSpeed,
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
