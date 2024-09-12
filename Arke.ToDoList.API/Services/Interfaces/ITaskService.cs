namespace Arke.ToDoList.API.Services.Interfaces;

public interface ITaskService
{
    Task<IEnumerable<TaskModel>> FindAll();
    Task<TaskModel> FindById(long id);
    Task<TaskModel> Save(TaskModel taskModel);
    Task<TaskModel> Update(long id, TaskModel taskModel);
    Task Delete(long id);
}
