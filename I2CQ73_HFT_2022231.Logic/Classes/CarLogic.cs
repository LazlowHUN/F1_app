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
			this.carRepo.Create(item);
		}

		public void Delete(int id)
		{
			this.carRepo.Delete(id);
		}

		public Car Read(int id)
		{
			return this.carRepo.Read(id);
		}

		public IQueryable<Car> ReadAll()
		{
			return this.carRepo.ReadAll();
		}

		public void Update(Car item)
		{
			this.carRepo.Update(item);
		}


	}
}
