using I2CQ73_HFT_2022231.Repository;
using System;
using System.Linq;

namespace I2CQ73_HFT_2022231.Client
{
	internal class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Hello World!");

			F1DbContext ctx = new F1DbContext();

			var items = ctx.Teams.ToArray();

			;
		}
	}
}
