using Arke.ToDoList.API.DataAccess.Entities;
using Arke.ToDoList.API.DataAccess.Repositories.Interfaces;
using Arke.ToDoList.API.DataAccess.Repositories;
using Arke.ToDoList.API.DataAccess.UnitOfWork;
using Arke.ToDoList.API.Services.Interfaces;
using Arke.ToDoList.API.Services;
using Arke.ToDoList.API.Context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
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
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
