using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using I2CQ73_HFT_2022231.Models;
using Microsoft.EntityFrameworkCore;

namespace I2CQ73_HFT_2022231.Repository
{
	public class F1DbContext : DbContext
	{
		public DbSet<Pilot> Pilots { get; set; }
		public DbSet<Team> Teams { get; set; }
		public DbSet<Car> Cars { get; set; }

		public F1DbContext()
		{
			this.Database.EnsureCreated();
		}

		protected override void OnConfiguring(DbContextOptionsBuilder builder)
		{
			if (!builder.IsConfigured)
			{
				builder
					.UseLazyLoadingProxies()
					.UseInMemoryDatabase("F1");
			}
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{

			modelBuilder.Entity<Pilot>(pilot => pilot
				.HasOne(pilot => pilot.Team)
				.WithMany(team => team.Pilots)
				.HasForeignKey(pilot => pilot.TeamId)
				.OnDelete(DeleteBehavior.Cascade));

			modelBuilder.Entity<Team>(team => team
				.HasOne(team => team.Car)
				.WithOne(car => car.Team)
				.OnDelete(DeleteBehavior.Cascade));

			modelBuilder.Entity<Car>().HasData(new Car[]
			{
				new Car("1#Ferrari#342,7#1050"),
				new Car("2#Mercedes#339,6#1040"),
				new Car("3#Honda#343,2#1045"),
				new Car("4#Mercedes#337,5#1040"),
				new Car("5#Reanult#336,7#1030"),
				new Car("6#Honda#336,6#1045"),
				new Car("7#Ferrari#337,4#1050"),
				new Car("8#Mercedes#336,6#1040"),
				new Car("9#Ferrari#336,4#1050"),
				new Car("10#Mercedes#336,2#1040"),
			});

			modelBuilder.Entity<Team>().HasData(new Team[]
			{
				new Team("1#Ferrari#1#463000000#454"),
				new Team("2#Mercedes#2#484000000#387"),
				new Team("3#Red Bull Racing#3#445000000#619"),
				new Team("4#McLaren#4#269000000#130"),
				new Team("5#Alpine#5#272000000#143"),
				new Team("6#AlphaTauri#6#138000000#34"),
				new Team("7#Alfa Romeo#7#132000000#52"),
				new Team("8#Aston Martin#8#188000000#45"),
				new Team("9#Haas F1 Team#9#173000000#34"),
				new Team("10#Williams#10#141000000#8"),
			});

			modelBuilder.Entity<Pilot>().HasData(new Pilot[]
			{
				new Pilot("1#Charles Leclerc#25#1"),
				new Pilot("2#Carlos Sainz#28#1"),
				new Pilot("3#Lewis Hamilton#37#2"),
				new Pilot("4#George Russel#24#2"),
				new Pilot("5#Max Verstappen#25#3"),
				new Pilot("6#Sergio Perez#32#3"),
				new Pilot("7#Lando Norris#22#4"),
				new Pilot("8#Daniel Ricciardo#33#4"),
				new Pilot("9#Esteban Ocon#26#5"),
				new Pilot("10#Fernando Alonso#41#5"),
				new Pilot("11#Pierre Gasly#26#6"),
				new Pilot("12#Yuki Tsunoda#22#6"),
				new Pilot("13#Valtteri Bottas#33#7"),
				new Pilot("14#Guanyu Zhou#23#7"),
				new Pilot("15#Sebastian Vettel#35#8"),
				new Pilot("16#Lance Stroll#23#8"),
				new Pilot("17#Kevin Magnussen#30#9"),
				new Pilot("18#Mick Schumacher#23#9"),
				new Pilot("19#Nicholas Latifi#27#10"),
				new Pilot("20#Alexander Albon#26#10"),
			});
		}
	}
}