using Arke.ToDoList.API.Context;
using Arke.ToDoList.API.DataAccess.Entities;
using Arke.ToDoList.API.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Arke.ToDoList.API.DataAccess.Repositories;

public class TaskRepository : BaseRepository<TaskEntity>, ITaskRepository
{
    public TaskRepository(DatabaseContext dbContext) : base(dbContext)
    {

    }

    public async Task<IEnumerable<TaskEntity>> GetAllCompletedTasks()
    {
        return await DbSet.Value.Where(x => x.Status == Enums.TaskStatusEnum.Done).ToListAsync();
    }
}
