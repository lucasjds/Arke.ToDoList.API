using Arke.ToDoList.API.DataAccess.Entities;
using Arke.ToDoList.API.DataAccess.Repositories.Interfaces;

namespace Arke.ToDoList.API.DataAccess.Repositories;

public class TaskRepository : BaseRepository<TaskEntity>, ITaskRepository
{
    public TaskRepository(DatabaseContext dbContext) : base(dbContext)
    {

    }
}
