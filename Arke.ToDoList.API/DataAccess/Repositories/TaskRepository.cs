using Arke.ToDoList.API.Context;
using Arke.ToDoList.API.DataAccess.Entities;
using Arke.ToDoList.API.DataAccess.Repositories.Interfaces;
using Arke.ToDoList.API.Shared.Enums;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace Arke.ToDoList.API.DataAccess.Repositories;

[ExcludeFromCodeCoverage]
public class TaskRepository : BaseRepository<TaskEntity>, ITaskRepository
{
    public TaskRepository(DatabaseContext dbContext) : base(dbContext)
    {

    }

    public async Task<IEnumerable<TaskEntity>> GetAllCompletedTasks()
    {
        return await DbSet.Value.Where(x => x.Status == TaskStatusEnum.Done).ToListAsync();
    }
}
