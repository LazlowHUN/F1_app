using I2CQ73_HFT_2022231.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace I2CQ73_HFT_2022231.Repository
{
	public class PilotRepository : Repository<Pilot>, IRepository<Pilot>
	{
		public PilotRepository(F1DbContext ctx) : base(ctx)
		{
		}

		public override Pilot Read(int id)
		{
			return ctx.Pilots.FirstOrDefault(t => t.PilotId == id);
		}

		public override void Update(Pilot item)
		{
			var old = Read(item.PilotId);
			foreach (var prop in old.GetType().GetProperties())
			{
				prop.SetValue(old, prop.GetValue(item));
			}
			ctx.SaveChanges();
		}
	}
}
