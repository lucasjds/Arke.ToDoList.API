using Arke.ToDoList.API.Domain.Entities;

namespace Arke.ToDoList.API.Domain.Contracts;

public interface ITaskRepository : IBaseRepository<TaskEntity>
{
    Task<IEnumerable<TaskEntity>> GetAllCompletedTasks();
}
