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
		PilotLogic logic;
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
			//elso
			//mockPilotRepo.Setup(m => m.Create(It.IsAny<Pilot>()));
			//mockPilotRepo.Setup(m => m.Read(1)).Returns(new Pilot
			//{
			//	PilotId = 1,
			//	PilotName = "Charles Leclerc",
			//	PilotAge = 25,
			//	TeamId = 1,
			//});

			mockTeamRepo.Setup(m => m.ReadAll()).Returns(new List<Team>()
			{
				new Team("1#Ferrari#1#463000000#454"),
				new Team("2#Mercedes#2#484000000#387"),
				new Team("3#Red Bull Racing#3#445000000#619"),
				new Team("4#McLaren#4#269000000#130"),
			}.AsQueryable());
			//masodik
			//mockTeamRepo.Setup(m => m.Create(It.IsAny<Team>()));
			//mockTeamRepo.Setup(m => m.Read(1)).Returns(new Team
			//{
			//	TeamId = 1,
			//	TeamName = "ferrari",
			//	CarId = 1,
			//	Budget = 463000000,
			//	TeamPoints = 454,
			//});

			mockCarRepo.Setup(m => m.ReadAll()).Returns(new List<Car>()
			{
				new Car("1#Ferrari#342,7#1050"),
				new Car("2#Mercedes#339,6#1040"),
				new Car("3#Honda#343,2#1045"),
				new Car("4#Mercedes#337,5#1040"),
			}.AsQueryable());
			//harmadik
			//mockCarRepo.Setup(m => m.Create(It.IsAny<Car>()));
			//mockCarRepo.Setup(m => m.Read(1)).Returns(new Car
			//{
			//	CarId = 1,
			//	EngineBrand = "Ferrari",
			//	MaxSpeed = double.Parse("342,7"),
			//	Horsepower = 1050,
			//});

			logic = new PilotLogic(mockPilotRepo.Object, mockTeamRepo.Object, mockCarRepo.Object);
		}

		[Test]
		public void LeclersCarStatisticsTest()
		{
			var stat = logic.LeclersCarStatistics().ToList();
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
			var stat = logic.YoungestPilotsEngineBrand().ToList();
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
			var stat = logic.Pilots1040HorsePower().ToList();
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
			var stat = logic.MercedesBrandTeamPointsAbove200Pilots().ToList();
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
			var stat = logic.YoungerThan30PilotsTeamBudgetAbove150MMaxSpeed().ToList();
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
	}
}
