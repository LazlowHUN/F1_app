using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace I2CQ73_HFT_2022231.Repository
{
	public abstract class Repository<T> : IRepository<T> where T : class
	{
		protected F1DbContext ctx;

		public Repository(F1DbContext ctx)
		{
			this.ctx = ctx;
		}

		public void Create(T item)
		{
			ctx.Set<T>().Add(item);
			ctx.SaveChanges();
		}
		public IQueryable<T> ReadAll()
		{
			return ctx.Set<T>();
		}
		public void Delete(int id)
		{
			ctx.Set<T>().Remove(Read(id));
			ctx.SaveChanges();
		}
		public abstract T Read(int id);
		public abstract void Update(T item);
	}
}
