using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
using Arke.ToDoList.API.Domain.Entities;
using Arke.ToDoList.API.Shared.Enums;
using Arke.ToDoList.API.Domain.Contracts;
using Arke.ToDoList.API.Data.Sql;

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
