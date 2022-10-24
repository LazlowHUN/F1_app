using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace I2CQ73_HFT_2022231.Models
{
	public class Pilot
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int PilotId { get; set; }

		[Required]
		[StringLength(240)]
		public string PilotName { get; set; }

		[Range(10,100)]
		public int PilotAge { get; set; }

		[Required]
		[ForeignKey("TeamId")]
		public int TeamId { get; set; }

		public virtual Team Team { get; set; }

		public Pilot()
		{
			
		}

		public Pilot(string line)
		{
			string[] split = line.Split('#');
			PilotId = int.Parse(split[0]);
			PilotName = split[1];
			PilotAge = int.Parse(split[2]);
			TeamId = int.Parse(split[3]);
		}
	}
}
