using Arke.ToDoList.API.DataAccess.Entities;
using Arke.ToDoList.API.DataAccess.Repositories.Interfaces;
using Arke.ToDoList.API.DataAccess.UnitOfWork;
using Arke.ToDoList.API.Models;
using Arke.ToDoList.API.Services.Interfaces;
using Arke.ToDoList.API.Utils.Exceptions;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;

namespace Arke.ToDoList.API.Services;

public class TaskService : ITaskService
{
    private readonly ITaskRepository _taskRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public TaskService(IUnitOfWork unitOfWork, ITaskRepository taskRepository, IMapper mapper)
    {
        _taskRepository = taskRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task DeleteCompletedTasksAsync()
    {
        var completedTasks = await _taskRepository.GetAllCompletedTasks();
        foreach (var completedTask in completedTasks)
        {
            _taskRepository.Delete(completedTask);
        }
        await _unitOfWork.CommitAsync();
    }

    public async Task<IEnumerable<TaskModel>> FindAllAsync()
    {
        return _mapper.Map<IEnumerable<TaskModel>>(await _taskRepository.FindAll());
    }

    public async Task<TaskModel> FindByIdAsync(Guid id)
    {
        var existing = await GetTask(id);
        return _mapper.Map<TaskModel>(existing);
    }

    public async Task<TaskModel> SaveAsync(TaskModel taskModel)
    {
        Validations(taskModel);

        var taskEntity = await _taskRepository.Save(new TaskEntity());

        taskModel.Id = taskEntity.Id;
        _mapper.Map(taskModel, taskEntity);

        var existing = await _taskRepository.FindById(taskEntity.Id);
        if (existing != null)
        {
            throw new GeneralErrorException(nameof(taskModel.Id), "Id already exists in database.");
        }

        await _unitOfWork.CommitAsync();

        return _mapper.Map<TaskModel>(taskEntity);
    }

    public async Task<TaskModel> UpdateAsync(Guid id, TaskModel taskModel)
    {
        Validations(taskModel);

        var existing = await GetTask(id);
        taskModel.Id = id;
        _mapper.Map(taskModel, existing);

        await _unitOfWork.CommitAsync();

        return _mapper.Map<TaskModel>(existing);
    }

    public async Task<TaskModel> PatchAsync(Guid id, JsonPatchDocument<TaskModel> task)
    {
        var existing = await GetTask(id);

        var taskModel = new TaskModel();
        _mapper.Map(existing, taskModel);

        task.ApplyTo(taskModel);

        _mapper.Map(taskModel, existing);

        await _unitOfWork.CommitAsync();

        return _mapper.Map<TaskModel>(existing);
    }

    private async Task<TaskEntity> GetTask(Guid id)
    {
        var existing = await _taskRepository.FindById(id);
        if (existing == null)
        {
            throw new NotFoundException(nameof(TaskModel), id);
        }

        return existing;
    }

    private void Validations(TaskModel taskModel)
    {
        if (taskModel.Status == Enums.TaskStatusEnum.Done)
        {
            throw new GeneralErrorException(nameof(taskModel.Status), "Task can't be created as done.");
        }
        if (string.IsNullOrEmpty(taskModel.Name))
        {
            throw new GeneralErrorException(nameof(taskModel.Name), "Can't be null/empty.");
        }
        if (string.IsNullOrEmpty(taskModel.Description))
        {
            throw new GeneralErrorException(nameof(taskModel.Description), "Can't be null/empty.");
        }
        if (taskModel.Description.Length < 5)
        {
            throw new GeneralErrorException(nameof(taskModel.Description), "minimum length should be at least 5 characters");
        }
    }
}
