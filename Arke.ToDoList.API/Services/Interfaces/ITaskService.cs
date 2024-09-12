using Arke.ToDoList.API.Models;

namespace Arke.ToDoList.API.Services.Interfaces;

public interface ITaskService
{
    Task<IEnumerable<TaskModel>> FindAll();
    Task<TaskModel> FindById(Guid id);
    Task<TaskModel> Save(TaskModel taskModel);
    Task<TaskModel> Update(Guid id, TaskModel taskModel);
    Task DeleteCompletedTasks();
}
