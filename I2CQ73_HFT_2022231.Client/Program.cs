using ConsoleTools;
using I2CQ73_HFT_2022231.Models;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Numerics;

namespace I2CQ73_HFT_2022231.Client
{
	public class Program
	{
		static RestService rest;

		static void Create(string entity)
		{
			try
			{
				if (entity == "Pilot")
				{
					Console.Write("Enter Pilot Id: ");
					int id = int.Parse(Console.ReadLine());
					Console.Write("Enter Pilot Name: ");
					string name = Console.ReadLine();
					Console.Write("Enter Pilot age: ");
					int age = int.Parse(Console.ReadLine());
					Console.Write("Enter Pilot TeamId: ");
					int team_id = int.Parse(Console.ReadLine());
					rest.Post(new Pilot() { PilotId = id, PilotName = name, PilotAge = age, TeamId = team_id }, "pilot");
				}
				if (entity == "Team")
				{
					Console.Write("Enter Team Id: ");
					int id = int.Parse(Console.ReadLine());
					Console.Write("Enter Team Name: ");
					string name = Console.ReadLine();
					Console.Write("Enter CarId: ");
					int car_id = int.Parse(Console.ReadLine());
					Console.Write("Enter Team budget: ");
					int budget = int.Parse(Console.ReadLine());
					Console.Write("Enter Team points: ");
					int points = int.Parse(Console.ReadLine());
					rest.Post(new Team() { TeamId = id, TeamName = name, CarId = car_id, Budget = budget, TeamPoints = points }, "team");
				}
				if (entity == "Car")
				{
					Console.Write("Enter Car Id: ");
					int id = int.Parse(Console.ReadLine());
					Console.Write("Enter Car engine brand: ");
					string brand = Console.ReadLine();
					Console.Write("Enter Car max speed: ");
					int speed = int.Parse(Console.ReadLine());
					Console.Write("Enter Car Horsepower: ");
					int horsepower = int.Parse(Console.ReadLine());
					rest.Post(new Car() { CarId = id, EngineBrand = brand, MaxSpeed = speed, Horsepower = horsepower }, "car");
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}
		}
		static void List(string entity)
		{
			try
			{
				if (entity == "Pilot")
				{
					List<Pilot> pilots = rest.Get<Pilot>("pilot");
					foreach (var item in pilots)
					{
						Console.WriteLine(item.PilotId + ": " + item.PilotName);
					}
				}
				if (entity == "Team")
				{
					List<Team> teams = rest.Get<Team>("team");
					foreach (var item in teams)
					{
						Console.WriteLine(item.TeamId + ": " + item.TeamName);
					}
				}
				if (entity == "Car")
				{
					List<Car> cars = rest.Get<Car>("car");
					foreach (var item in cars)
					{
						Console.WriteLine(item.CarId + ": " + item.EngineBrand);
					}
				}
				if (entity == "Leclerc")
				{
					List<object> stats = rest.Get<object>("Stat/LeclersCarStatistics");
					foreach (var item in stats)
					{
						Console.WriteLine(item);
					}
				}
				if (entity == "Youngest")
				{
					List<object> stats = rest.Get<object>("Stat/YoungestPilotsEngineBrand");
					foreach (var item in stats)
					{
						Console.WriteLine(item);
					}
				}
				if (entity == "1040")
				{
					List<object> stats = rest.Get<object>("Stat/Pilots1040HorsePower");
					foreach (var item in stats)
					{
						Console.WriteLine(item);
					}
				}
				if (entity == "Mercedes")
				{
					List<object> stats = rest.Get<object>("Stat/MercedesBrandTeamPointsAbove200Pilots");
					foreach (var item in stats)
					{
						Console.WriteLine(item);
					}
				}
				if (entity == "Younger")
				{
					List<object> stats = rest.Get<object>("Stat/YoungerThan30PilotsTeamBudgetAbove150MMaxSpeed");
					foreach (var item in stats)
					{
						Console.WriteLine(item);
					}
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}
			Console.ReadLine();
		}
		static void Update(string entity)
		{
			try
			{
				if (entity == "Pilot")
				{
					Console.Write("Enter Pilot's id to update: ");
					int id = int.Parse(Console.ReadLine());
					Pilot one = rest.Get<Pilot>(id, "pilot");
					Console.Write($"New name [old: {one.PilotName}]: ");
					string name = Console.ReadLine();
					one.PilotName = name;
					rest.Put(one, "pilot");
				}
				if (entity == "Team")
				{
					Console.Write("Enter Team's id to update: ");
					int id = int.Parse(Console.ReadLine());
					Team one = rest.Get<Team>(id, "team");
					Console.Write($"New name [old: {one.TeamName}]: ");
					string name = Console.ReadLine();
					one.TeamName = name;
					rest.Put(one, "team");
				}
				if (entity == "Car")
				{
					Console.Write("Enter Car's id to update: ");
					int id = int.Parse(Console.ReadLine());
					Car one = rest.Get<Car>(id, "car");
					Console.Write($"New name [old: {one.EngineBrand}]: ");
					string name = Console.ReadLine();
					one.EngineBrand = name;
					rest.Put(one, "car");
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}
		}
		static void Delete(string entity)
		{
			try
			{
				if (entity == "Pilot")
				{
					Console.Write("Enter Pilot's id to delete: ");
					int id = int.Parse(Console.ReadLine());
					rest.Delete(id, "pilot");
				}
				if (entity == "Team")
				{
					Console.Write("Enter Team's id to delete: ");
					int id = int.Parse(Console.ReadLine());
					rest.Delete(id, "team");
				}
				if (entity == "Car")
				{
					Console.Write("Enter Car's id to delete: ");
					int id = int.Parse(Console.ReadLine());
					rest.Delete(id, "car");
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}
		}

		static void Main(string[] args)
		{
			rest = new RestService("http://localhost:19484/", "pilot");

			var pilotSubMenu = new ConsoleMenu(args, level: 1)
				.Add("List", () => List("Pilot"))
				.Add("Create", () => Create("Pilot"))
				.Add("Delete", () => Delete("Pilot"))
				.Add("Update", () => Update("Pilot"))
				.Add("Exit", ConsoleMenu.Close);

			var teamSubMenu = new ConsoleMenu(args, level: 1)
				.Add("List", () => List("Team"))
				.Add("Create", () => Create("Team"))
				.Add("Delete", () => Delete("Team"))
				.Add("Update", () => Update("Team"))
				.Add("Exit", ConsoleMenu.Close);

			var carSubMenu = new ConsoleMenu(args, level: 1)
				.Add("List", () => List("Car"))
				.Add("Create", () => Create("Car"))
				.Add("Delete", () => Delete("Car"))
				.Add("Update", () => Update("Car"))
				.Add("Exit", ConsoleMenu.Close);

			var noncrudMenu = new ConsoleMenu(args, level: 1)
				.Add("Leclerc Stat", () => List("Leclerc"))
				.Add("Youngest pilots", () => List("Youngest"))
				.Add("1040 Horsepower", () => List("1040"))
				.Add("Mercedes above 200", () => List("Mercedes"))
				.Add("Younger than 30", () => List("Younger"))
				.Add("Exit", ConsoleMenu.Close);

			var menu = new ConsoleMenu(args, level: 0)
				.Add("Pilots", () => pilotSubMenu.Show())
				.Add("Teams", () => teamSubMenu.Show())
				.Add("Cars", () => carSubMenu.Show())
				.Add("Non-cruds", () => noncrudMenu.Show())
				.Add("Exit", ConsoleMenu.Close);

			menu.Show();
		}
	}
}
