using Arke.ToDoList.API.Shared.Models;
using Microsoft.AspNetCore.JsonPatch;

namespace Arke.ToDoList.API.Domain.Contracts;

public interface ITaskService
{
    Task<IEnumerable<TaskModel>> FindAllAsync();
    Task<TaskModel> FindByIdAsync(Guid id);
    Task<TaskModel> SaveAsync(TaskModel taskModel);
    Task<TaskModel> PatchAsync(Guid id, JsonPatchDocument<TaskModel> taskModel);
    Task<TaskModel> UpdateAsync(Guid id, TaskModel taskModel);
    Task DeleteCompletedTasksAsync();
}
