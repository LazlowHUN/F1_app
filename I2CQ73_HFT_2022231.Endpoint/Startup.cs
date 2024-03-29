using I2CQ73_HFT_2022231.Endpoint.Services;
using I2CQ73_HFT_2022231.Logic;
using I2CQ73_HFT_2022231.Models;
using I2CQ73_HFT_2022231.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace I2CQ73_HFT_2022231.Endpoint
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddTransient<F1DbContext>();


			services.AddTransient<IRepository<Pilot>, PilotRepository>();
			services.AddTransient<IRepository<Team>, TeamRepository>();
			services.AddTransient<IRepository<Car>, CarRepository>();

			services.AddTransient<IPilotLogic, PilotLogic>();
			services.AddTransient<ITeamLogic, TeamLogic>();
			services.AddTransient<ICarLogic, CarLogic>();

			services.AddSignalR();

			services.AddControllers();
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "I2CQ73_HFT_2022231.Endpoint", Version = "v1" });
			});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseSwagger();
				app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "I2CQ73_HFT_2022231.Endpoint v1"));
			}

			app.UseExceptionHandler(c => c.Run(async context =>
			{
				var exception = context.Features
				.Get<IExceptionHandlerPathFeature>()
				.Error;
				var response = new { Msg = exception.Message };
				await context.Response.WriteAsJsonAsync(response);
			}));

			app.UseCors(x => x
				.AllowCredentials()
				.AllowAnyMethod()
				.AllowAnyHeader()
				.WithOrigins("http://localhost:50374"));

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
				endpoints.MapHub<SignalRHub>("/hub");
			});
		}
	}
}
