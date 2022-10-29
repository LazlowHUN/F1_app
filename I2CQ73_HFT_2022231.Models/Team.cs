using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace I2CQ73_HFT_2022231.Models
{
	[Table("Team")]
	public class Team
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int TeamId { get; set; }

		[Required]
		[StringLength(240)]
		public string TeamName { get; set; }

		[Required]
		[ForeignKey(nameof(Models.Car))]
		public int CarId { get; set; }

		public int Budget { get; set; }

		public int TeamPoints { get; set; }

		[NotMapped]
		public virtual ICollection<Pilot> Pilots { get; set; }

		[NotMapped]
		public virtual Car Car { get; set; }

		public Team()
		{
			Pilots = new HashSet<Pilot>();
		}

		public Team(string line)
		{
			string[] split = line.Split('#');
			TeamId = int.Parse(split[0]);
			TeamName = split[1];
			CarId = int.Parse(split[2]);
			Budget = int.Parse(split[3]);
			TeamPoints = int.Parse(split[4]);
			Pilots = new HashSet<Pilot>();
		}
	}
}
