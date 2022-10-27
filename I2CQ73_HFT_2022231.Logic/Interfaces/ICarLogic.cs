using I2CQ73_HFT_2022231.Models;
using System.Linq;

namespace I2CQ73_HFT_2022231.Logic
{
	public interface ICarLogic
	{
		void Create(Car item);
		void Delete(int id);
		Car Read(int id);
		IQueryable<Car> ReadAll();
		void Update(Car item);
	}
}