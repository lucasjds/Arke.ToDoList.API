using Arke.ToDoList.API.Data.Sql;
using Arke.ToDoList.API.DataAccess.Repositories;
using Arke.ToDoList.API.DataAccess.UnitOfWork;
using Arke.ToDoList.API.Domain.Contracts;
using Arke.ToDoList.API.Domain.Entities;
using Arke.ToDoList.API.Domain.Exceptions;
using Arke.ToDoList.API.Services;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Diagnostics.CodeAnalysis;

[ExcludeFromCodeCoverage]
public class Startup
{
	public void ConfigureServices(IServiceCollection services)
	{
		// CORS configuration
		services.AddCors(options =>
		{
			options.AddPolicy("AllowAllOrigins",
				builder => builder.AllowAnyOrigin()
								  .AllowAnyMethod()
								  .AllowAnyHeader());
		});

		// Controllers and JSON settings
		services.AddControllers(config =>
		{
			config.Filters.Add(typeof(GlobalExceptionFilter));
		})
		.AddNewtonsoftJson(options =>
		{
			options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
			options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
			options.SerializerSettings.Converters.Add(new StringEnumConverter());
		});

		// Swagger/OpenAPI and AutoMapper
		services.AddEndpointsApiExplorer();
		services.AddSwaggerGen();
		services.AddAutoMapper(typeof(TaskEntity).Assembly);

		// Dependency Injection
		services.AddScoped<IUnitOfWork, UnitOfWork>();
		services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
		services.AddScoped<ITaskService, TaskService>();
		services.AddScoped<ITaskRepository, TaskRepository>();

		// In-memory database configuration
		string DBUUID = Guid.NewGuid().ToString();
		services.AddDbContext<DatabaseContext>(opt =>
					opt.UseInMemoryDatabase("ToDoDb" + DBUUID));
	}

	public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
	{
		// Middleware configuration
		if (env.IsDevelopment())
		{
			app.UseDeveloperExceptionPage();
		}
		else
		{
			app.UseExceptionHandler("/Home/Error");
			app.UseHsts();
		}

		app.UseSwagger();
		app.UseSwaggerUI();
		app.UseHttpsRedirection();
		app.UseCors("AllowAllOrigins");
		app.UseAuthorization();

		app.UseRouting(); // Adicione isso para habilitar o roteamento

		app.UseEndpoints(endpoints =>
		{
			endpoints.MapControllers(); // Correto uso para ASP.NET Core 3.0 e posterior
		});
	}
}
