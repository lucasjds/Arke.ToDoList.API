﻿using Arke.ToDoList.API.Domain.Contracts;
using Arke.ToDoList.API.Domain.Entities;
using Arke.ToDoList.API.Domain.Exceptions;
using Arke.ToDoList.API.Shared.Enums;
using Arke.ToDoList.API.Shared.Models;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.Extensions.Logging;

namespace Arke.ToDoList.API.Services;

public class TaskService : ITaskService
{
	private readonly ITaskRepository _taskRepository;
	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;
	private readonly ILogger<TaskService> _logger;

	public TaskService(IUnitOfWork unitOfWork, ITaskRepository taskRepository, IMapper mapper, ILogger<TaskService> logger)
	{
		_taskRepository = taskRepository;
		_unitOfWork = unitOfWork;
		_logger = logger;
		_mapper = mapper;
	}

	public async Task DeleteCompletedTasksAsync()
	{
		try
		{
			var completedTasks = await _taskRepository.GetAllCompletedTasks();
			foreach (var completedTask in completedTasks)
			{
				_taskRepository.Delete(completedTask);
			}
			if (completedTasks != null && completedTasks.Any())
			{
				_logger.LogInformation($"Delete Completed Tasks was done successfully.");
				await _unitOfWork.CommitAsync();
			}
		}
		catch (Exception ex)
		{
			_logger.LogError($"Delete Completed Tasks was rollback. {ex.Message}");
			await _unitOfWork.RollbackAsync();
			throw;
		}
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
		if (taskModel.Status == TaskStatusEnum.Done)
		{
			throw new GeneralErrorException(nameof(taskModel.Status), "Task can't be created as done.");
		}
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
		_logger.LogInformation($"Created Task successfully.");

		return _mapper.Map<TaskModel>(taskEntity);
	}

	public async Task<TaskModel> UpdateAsync(Guid id, TaskModel taskModel)
	{
		Validations(taskModel);

		var existing = await GetTask(id);
		taskModel.Id = id;
		_mapper.Map(taskModel, existing);

		await _unitOfWork.CommitAsync();

		_logger.LogInformation($"Update Task successfully.");
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
		_logger.LogInformation($"Patch Task successfully.");
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

	private static void Validations(TaskModel taskModel)
	{
		if (string.IsNullOrEmpty(taskModel.Name))
		{
			throw new GeneralErrorException(nameof(taskModel.Name), "Can't be null/empty.");
		}
		if (!string.IsNullOrEmpty(taskModel.Description) && taskModel.Description.Length < 5)
		{
			throw new GeneralErrorException(nameof(taskModel.Description), "minimum length should be at least 5 characters.");
		}
	}
}
