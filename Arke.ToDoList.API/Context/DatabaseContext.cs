using Arke.ToDoList.API.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace Arke.ToDoList.API.Context;

[ExcludeFromCodeCoverage]
public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options)
    : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TaskEntity>();
    }
    public DbSet<TaskEntity> Tasks { get; set; }
}
