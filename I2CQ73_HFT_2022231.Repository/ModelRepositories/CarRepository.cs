using I2CQ73_HFT_2022231.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace I2CQ73_HFT_2022231.Repository
{
	public class CarRepository : Repository<Car>, IRepository<Car>
	{
		public CarRepository(F1DbContext ctx) : base(ctx)
		{
		}

		public override Car Read(int id)
		{
			return ctx.Cars.FirstOrDefault(t => t.CarId == id);
		}

		public override void Update(Car item)
		{
			var old = Read(item.CarId);
			foreach (var prop in old.GetType().GetProperties())
			{
				prop.SetValue(old, prop.GetValue(item));
			}
			ctx.SaveChanges();
		}
	}
}
