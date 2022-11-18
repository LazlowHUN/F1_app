using I2CQ73_HFT_2022231.Logic;
using I2CQ73_HFT_2022231.Models;
using I2CQ73_HFT_2022231.Repository;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;

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

			var leclerc = new Pilot() { PilotName = "Charles Leclerc", PilotAge = 25, PilotId = 1, TeamId = 1 };
			var sainz = new Pilot() { PilotName = "Carlos Sainz", PilotAge = 28, PilotId = 2, TeamId = 1 };
			var hamilton = new Pilot() { PilotName = "Lewis Hamilton", PilotAge = 37, PilotId = 3, TeamId = 2 };
			var russel = new Pilot() { PilotName = "George Russel", PilotAge = 24, PilotId = 4, TeamId = 2 };
			var verstappen = new Pilot() { PilotName = "Max Verstappen", PilotAge = 25, PilotId = 5, TeamId = 3 };
			var perez = new Pilot() { PilotName = "Sergio Perez", PilotAge = 32, PilotId = 6, TeamId = 3 };
			var norris = new Pilot() { PilotName = "Lando Norris", PilotAge = 22, PilotId = 7, TeamId = 4 };
			var ricciardo = new Pilot() { PilotName = "Daniel Ricciardo", PilotAge = 33, PilotId = 8, TeamId = 4 };


			var ferrari = new Team() { TeamId = 1, TeamName = "Ferrari", TeamPoints = 454, CarId = 1, Budget = 463000000 , Pilots = new List<Pilot>() { leclerc, sainz} };
			var mercedes = new Team() { TeamId = 2, TeamName = "Mercedes", TeamPoints = 387, Budget = 484000000, CarId = 2, Pilots = new List<Pilot>() { hamilton, russel} };
			var redbull = new Team() { TeamId = 3, TeamName = "Red Bull Racing", TeamPoints = 619, Budget = 445000000, CarId = 3, Pilots = new List<Pilot>() { verstappen, perez } };
			var mclaren = new Team() { TeamId = 4, TeamName = "McLaren", TeamPoints = 130, Budget = 269000000, CarId = 4, Pilots = new List<Pilot>() { norris, ricciardo } };

			leclerc.Team = ferrari;
			sainz.Team = ferrari;
			hamilton.Team = mercedes;
			russel.Team = mercedes;
			verstappen.Team = redbull;
			perez.Team = redbull;
			norris.Team = mclaren;
			ricciardo.Team = mclaren;

			var fcar = new Car() { CarId = 1, EngineBrand = "Ferrari", Horsepower = 1050, MaxSpeed = double.Parse("342,7"), Team = leclerc.Team };
			var mcar = new Car() { CarId = 1, EngineBrand = "Mercedes", Horsepower = 1040, MaxSpeed = double.Parse("339,6"), Team = hamilton.Team };
			var rcar = new Car() { CarId = 1, EngineBrand = "Honda", Horsepower = 1045, MaxSpeed = double.Parse("343,2"), Team = verstappen.Team };
			var mlcar = new Car() { CarId = 1, EngineBrand = "Mercedes", Horsepower = 1040, MaxSpeed = double.Parse("337,5"), Team = norris.Team };

			ferrari.Car = fcar;
			mercedes.Car = mcar;
			redbull.Car = rcar;
			mclaren.Car = mlcar;
			

			mockPilotRepo.Setup(m => m.ReadAll()).Returns(new List<Pilot>()
			{
				leclerc,
				sainz,
				hamilton,
				russel,
				verstappen,
				perez,
				norris,
				ricciardo,
			}.AsQueryable());

			mockPilotRepo.Setup(m => m.Read(1)).Returns(new Pilot("1#Charles Leclerc#25#1"));

			mockTeamRepo.Setup(m => m.ReadAll()).Returns(new List<Team>()
			{
				ferrari,
				redbull,
				mercedes,
				mclaren,
			}.AsQueryable());

			mockTeamRepo.Setup(m => m.Read(1)).Returns(new Team("1#Ferrari#1#463000000#454"));

			mockCarRepo.Setup(m => m.ReadAll()).Returns(new List<Car>()
			{
				fcar,
				mcar,
				rcar,
				mlcar,
			}.AsQueryable());

			mockCarRepo.Setup(m => m.Read(1)).Returns(new Car("1#Ferrari#342,7#1050"));

			pilotLogic = new PilotLogic(mockPilotRepo.Object, mockTeamRepo.Object, mockCarRepo.Object);
			teamLogic = new TeamLogic(mockTeamRepo.Object);
			carLogic = new CarLogic(mockCarRepo.Object);
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
