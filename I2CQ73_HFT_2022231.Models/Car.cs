using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace I2CQ73_HFT_2022231.Models
{
	public class Car
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int CarId { get; set; }

		[StringLength(240)]
		public string EngineBrand { get; set; }

		[Range(0, 500)]
		public double MaxSpeed { get; set; }

		public int Horsepower { get; set; }

		public virtual Team Team { get; set; }

		public Car()
		{

		}

		public Car(string line)
		{
			string[] split = line.Split('#');
			CarId = int.Parse(split[0]);
			EngineBrand = split[1];
			MaxSpeed = double.Parse(split[2]);
			Horsepower = int.Parse(split[3]);
		}
	}
}
