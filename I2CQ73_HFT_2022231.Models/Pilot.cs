using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace I2CQ73_HFT_2022231.Models
{
	[Table("Pilot")]
	public class Pilot
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int PilotId { get; set; }

		[Required]
		[StringLength(240)]
		public string PilotName { get; set; }

		[Range(15,50)]
		public int PilotAge { get; set; }

		[Required]
		[ForeignKey(nameof(Models.Team))]
		public int TeamId { get; set; }

		[NotMapped]
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

		public override bool Equals(object obj)
		{
			Pilot b = obj as Pilot;
			if (b == null)
			{
				return false;
			}
			else
			{
				return b.PilotName == this.PilotName && b.PilotId == this.PilotId;
			}
		}
		public override int GetHashCode()
		{
			return HashCode.Combine(PilotId, PilotName, PilotAge, TeamId);
		}
	}
}
