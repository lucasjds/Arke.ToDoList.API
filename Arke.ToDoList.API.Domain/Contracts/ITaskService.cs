using Arke.ToDoList.API.Shared.Models;
using Microsoft.AspNetCore.JsonPatch;

namespace Arke.ToDoList.API.Domain.Contracts;

public interface ITaskService
{
    Task<IEnumerable<TaskModelRead>> FindAllAsync();
    Task<TaskModelRead> FindByIdAsync(Guid id);
    Task<TaskModelRead> SaveAsync(TaskModelWrite taskModel);
    Task<TaskModelRead> PatchAsync(Guid id, JsonPatchDocument<TaskModelWrite> taskModel);
    Task<TaskModelRead> UpdateAsync(Guid id, TaskModelWrite taskModel);
    Task DeleteCompletedTasksAsync();
}
