using Arke.ToDoList.API.DataAccess.Repositories;
using Arke.ToDoList.API.DataAccess.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Arke.ToDoList.API.Domain.Entities;
using Arke.ToDoList.API.Services;
using Arke.ToDoList.API.Domain.Contracts;
using Arke.ToDoList.API.Data.Sql;
using Arke.ToDoList.API.Domain.Exceptions;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders().AddConsole();

// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder => builder.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader());
});


builder.Services.AddControllers(config =>
{
    config.Filters.Add(typeof(GlobalExceptionFilter));
})
.AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
    options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
    options.SerializerSettings.Converters.Add(new StringEnumConverter());
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(TaskEntity).Assembly);

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
builder.Services.AddScoped<ITaskService, TaskService>();
builder.Services.AddScoped<ITaskRepository, TaskRepository>();

string DBUUID = Guid.NewGuid().ToString();
builder.Services.AddDbContext<DatabaseContext>(opt =>
            opt.UseInMemoryDatabase("ToDoDb" + DBUUID));

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseDeveloperExceptionPage();
app.UseExceptionHandler("/Home/Error");
app.UseHsts();

app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseCors("AllowAllOrigins");
app.UseAuthorization();

app.MapControllers();

app.Run();
