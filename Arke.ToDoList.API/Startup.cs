using Arke.ToDoList.API.DataAccess.Entities;
using Arke.ToDoList.API.DataAccess.Repositories.Interfaces;
using Arke.ToDoList.API.DataAccess.Repositories;
using Arke.ToDoList.API.DataAccess.UnitOfWork;
using Arke.ToDoList.API.Services.Interfaces;
using Arke.ToDoList.API.Services;
using Arke.ToDoList.API.Context;
using Microsoft.EntityFrameworkCore;
using Arke.ToDoList.API.Mappings;

namespace Arke.ToDoList.API;

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
        services.AddAutoMapper(typeof(TaskProfile).Assembly);
        services.AddControllers();
        string DBUUID = Guid.NewGuid().ToString();

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
        services.AddScoped<ITaskService, TaskService>();

        //services.AddDbContext<DatabaseContext>(opt =>
        //    opt.UseInMemoryDatabase("ToDoDb" + DBUUID));
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseDeveloperExceptionPage();
        }

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}
