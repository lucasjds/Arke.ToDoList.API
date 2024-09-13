﻿using Arke.ToDoList.API.Models;
using Microsoft.AspNetCore.JsonPatch;

namespace Arke.ToDoList.API.Services.Interfaces;

public interface ITaskService
{
    Task<IEnumerable<TaskModel>> FindAllAsync();
    Task<TaskModel> FindByIdAsync(Guid id);
    Task<TaskModel> SaveAsync(TaskModel taskModel);
    Task<TaskModel> PatchTaskAsync(Guid id, JsonPatchDocument<TaskModel> taskModel);
    Task DeleteCompletedTasksAsync();
}
