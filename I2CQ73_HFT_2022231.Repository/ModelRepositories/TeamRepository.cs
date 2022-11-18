using I2CQ73_HFT_2022231.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace I2CQ73_HFT_2022231.Repository
{
	public class TeamRepository : Repository<Team>, IRepository<Team>
	{
		public TeamRepository(F1DbContext ctx) : base(ctx)
		{
		}

		public override Team Read(int id)
		{
			return ctx.Teams.FirstOrDefault(t => t.TeamId == id);
		}

		public override void Update(Team item)
		{
			var old = Read(item.TeamId);
			foreach (var prop in old.GetType().GetProperties())
			{
				if (prop.GetAccessors().FirstOrDefault(t => t.IsVirtual) == null)
				{
					prop.SetValue(old, prop.GetValue(item));
				}
			}
			ctx.SaveChanges();
		}
	}
}
