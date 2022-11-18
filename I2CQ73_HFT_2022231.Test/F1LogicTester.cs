using I2CQ73_HFT_2022231.Logic;
using I2CQ73_HFT_2022231.Models;
using I2CQ73_HFT_2022231.Repository;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace I2CQ73_HFT_2022231.Test
{
	[TestFixture]
	public class F1LogicTester
	{
		PilotLogic pilotLogic;
		TeamLogic teamLogic;
		CarLogic carLogic;
		Mock<IRepository<Pilot>> mockPilotRepo;
		Mock<IRepository<Team>> mockTeamRepo;
		Mock<IRepository<Car>> mockCarRepo;

		[SetUp]
		public void Init()
		{
			mockPilotRepo = new Mock<IRepository<Pilot>>();
			mockTeamRepo = new Mock<IRepository<Team>>();
			mockCarRepo = new Mock<IRepository<Car>>();

			mockPilotRepo.Setup(m => m.ReadAll()).Returns(new List<Pilot>()
			{
				new Pilot("1#Charles Leclerc#25#1"),
				new Pilot("2#Carlos Sainz#28#1"),
				new Pilot("3#Lewis Hamilton#37#2"),
				new Pilot("4#George Russel#24#2"),
				new Pilot("5#Max Verstappen#25#3"),
				new Pilot("6#Sergio Perez#32#3"),
				new Pilot("7#Lando Norris#22#4"),
				new Pilot("8#Daniel Ricciardo#33#4"),
			}.AsQueryable());

			mockPilotRepo.Setup(m => m.Read(1)).Returns(new Pilot("1#Charles Leclerc#25#1"));

			mockTeamRepo.Setup(m => m.ReadAll()).Returns(new List<Team>()
			{
				new Team("1#Ferrari#1#463000000#454"),
				new Team("2#Mercedes#2#484000000#387"),
				new Team("3#Red Bull Racing#3#445000000#619"),
				new Team("4#McLaren#4#269000000#130"),
			}.AsQueryable());

			mockTeamRepo.Setup(m => m.Read(1)).Returns(new Team("1#Ferrari#1#463000000#454"));

			mockCarRepo.Setup(m => m.ReadAll()).Returns(new List<Car>()
			{
				new Car("1#Ferrari#342,7#1050"),
				new Car("2#Mercedes#339,6#1040"),
				new Car("3#Honda#343,2#1045"),
				new Car("4#Mercedes#337,5#1040"),
			}.AsQueryable());

			mockCarRepo.Setup(m => m.Read(1)).Returns(new Car("1#Ferrari#342,7#1050"));

			pilotLogic = new PilotLogic(mockPilotRepo.Object, mockTeamRepo.Object, mockCarRepo.Object);
			teamLogic = new TeamLogic(mockPilotRepo.Object, mockTeamRepo.Object, mockCarRepo.Object);
			carLogic = new CarLogic(mockPilotRepo.Object, mockTeamRepo.Object, mockCarRepo.Object);
		}

		[Test]
		public void LeclersCarStatisticsTest()
		{
			var stat = pilotLogic.LeclersCarStatistics().ToList();
			var expected = new List<NameTeamSpeed>()
			{
				new NameTeamSpeed()
				{
					Name = "Charles Leclerc",
					Team = "Ferrari",
					Speed = double.Parse("342,7"),
				}
			};

			Assert.AreEqual(expected, stat);
		}

		[Test]
		public void YoungestPilotsEngineBrandTest()
		{
			var stat = pilotLogic.YoungestPilotsEngineBrand().ToList();
			var expected = new List<NameEngineBrand>()
			{
				new NameEngineBrand()
				{
					Name = "Lando Norris",
					EngineBrand = "Mercedes",
				}
			};

			Assert.AreEqual(expected, stat);
		}

		[Test]
		public void Pilots1040HorsePowerTest()
		{
			var stat = pilotLogic.Pilots1040HorsePower().ToList();
			var expected = new List<NameEngineBrand>()
			{
				new NameEngineBrand()
				{
					Name = "Lewis Hamilton",
					EngineBrand = "Mercedes",
				},
				new NameEngineBrand()
				{
					Name = "George Russel",
					EngineBrand = "Mercedes",
				},
				new NameEngineBrand()
				{
					Name = "Lando Norris",
					EngineBrand = "Mercedes",
				},
				new NameEngineBrand()
				{
					Name = "Daniel Ricciardo",
					EngineBrand = "Mercedes",
				},
			};

			Assert.AreEqual(expected, stat);
		}

		[Test]
		public void MercedesBrandTeamPointsAbove200PilotsTest()
		{
			var stat = pilotLogic.MercedesBrandTeamPointsAbove200Pilots().ToList();
			var expected = new List<NameEngineBrand>()
			{
				new NameEngineBrand()
				{
					Name = "Lewis Hamilton",
					EngineBrand = "Mercedes",
				},
				new NameEngineBrand()
				{
					Name = "George Russel",
					EngineBrand = "Mercedes",
				},
			};

			Assert.AreEqual(expected, stat);
		}

		[Test]
		public void YoungerThan30PilotsTeamBudgetAbove150MMaxSpeed()
		{
			var stat = pilotLogic.YoungerThan30PilotsTeamBudgetAbove150MMaxSpeed().ToList();
			var expected = new List<NameTeamSpeed>()
			{
				new NameTeamSpeed()
				{
					Name = "Charles Leclerc",
					Team = "Ferrari",
					Speed = double.Parse("342,7"),
				},
				new NameTeamSpeed()
				{
					Name = "Carlos Sainz",
					Team = "Ferrari",
					Speed = double.Parse("342,7"),
				},
				new NameTeamSpeed()
				{
					Name = "George Russel",
					Team = "Mercedes",
					Speed = double.Parse("339,6"),
				},
				new NameTeamSpeed()
				{
					Name = "Max Verstappen",
					Team = "Red Bull Racing",
					Speed = double.Parse("343,2"),
				},
				new NameTeamSpeed()
				{
					Name = "Lando Norris",
					Team = "McLaren",
					Speed = double.Parse("337,5"),
				},
			};

			Assert.AreEqual(expected, stat);
		}

		[Test]
		public void CreateTeamWithCorrectTest()
		{
			var team = new Team()
			{
				TeamId = 5,
				TeamName = "AUDI",
				CarId = 5,
				Budget = 303000000,
				TeamPoints = 0,
			};

			teamLogic.Create(team);

			mockTeamRepo.Verify(r => r.Create(team), Times.Once);
		}

		[Test]
		public void CreateTeamWithIncorrectTest()
		{
			var team = new Team()
			{
				TeamId = 5,
				TeamName = "CA",
				CarId = 5,
				Budget = 303000000,
				TeamPoints = 0,
			};

			try
			{
				teamLogic.Create(team);
			}
			catch
			{

			}

			mockTeamRepo.Verify(r => r.Create(team), Times.Never);
		}

		[Test]
		public void CreatePilotWithCorrectTest()
		{
			var pilot = new Pilot()
			{
				PilotId = 9,
				PilotName = "Charles Alonso",
				PilotAge = 30,
				TeamId = 9,
			};

			pilotLogic.Create(pilot);

			mockPilotRepo.Verify(r => r.Create(pilot), Times.Once);
		}

		[Test]
		public void ReadPilotWithCorrectTest()
		{
			var pilot = new Pilot()
			{
				PilotId = 1,
				PilotName = "Charles Leclerc",
				PilotAge = 25,
				TeamId = 1,
			};

			var pilot_id = pilotLogic.Read(1);

			Assert.AreEqual(pilot, pilot_id);
		}

		[Test]
		public void ReadTeamWithCorrectTest()
		{
			var team = new Team()
			{
				TeamId = 1,
				TeamName = "Ferrari",
				CarId = 1,
				Budget = 463000000,
				TeamPoints = 454,
			};

			var team_id = teamLogic.Read(1);

			Assert.AreEqual(team, team_id);
		}
	}
}
