using I2CQ73_HFT_2022231.Models;
using I2CQ73_HFT_2022231.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace I2CQ73_HFT_2022231.Logic
{
	public class CarLogic : ICarLogic
	{
		IRepository<Pilot> pilotRepo;
		IRepository<Team> teamRepo;
		IRepository<Car> carRepo;

		public CarLogic(IRepository<Pilot> pilotRepo, IRepository<Team> teamRepo, IRepository<Car> carRepo)
		{
			this.pilotRepo = pilotRepo;
			this.teamRepo = teamRepo;
			this.carRepo = carRepo;
		}

		public void Create(Car item)
		{
			if (item.EngineBrand.Length < 3)
			{
				throw new ArgumentException("The name is too short.");
			}
			else if (item.EngineBrand.Length > 240)
			{
				throw new ArgumentException("The name is too long.");
			}
			else
			{
				this.carRepo.Create(item);
			}
		}

		public void Delete(int id)
		{
			var car = this.carRepo.Read(id);
			if (car == null)
			{
				throw new ArgumentException("Car do not exists!");
			}
			this.carRepo.Delete(id);
		}

		public Car Read(int id)
		{
			var car = this.carRepo.Read(id);
			if (car == null)
			{
				throw new ArgumentException("Car do not exists!");
			}
			return car;
		}

		public IQueryable<Car> ReadAll()
		{
			return this.carRepo.ReadAll();
		}

		public void Update(Car item)
		{
			if (item == null)
			{
				throw new NullReferenceException("Car do not exists!");
			}
			else
			{
				this.carRepo.Update(item);
			}
		}


	}
}
