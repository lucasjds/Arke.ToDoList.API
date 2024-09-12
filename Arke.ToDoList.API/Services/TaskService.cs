﻿using Arke.ToDoList.API.DataAccess.Entities;
using Arke.ToDoList.API.DataAccess.Repositories.Interfaces;
using Arke.ToDoList.API.DataAccess.UnitOfWork;
using Arke.ToDoList.API.Models;
using Arke.ToDoList.API.Services.Interfaces;
using Arke.ToDoList.API.Utils.Exceptions;
using AutoMapper;

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

    public Task Delete(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<TaskModel>> FindAll()
    {
        return _mapper.Map<IEnumerable<TaskModel>>(await _taskRepository.FindAll());
    }

    public async Task<TaskModel> FindById(Guid id)
    {
        var existing = await GetTask(id);
        return _mapper.Map<TaskModel>(existing);
    }

    public Task<TaskModel> Save(TaskModel taskModel)
    {
        throw new NotImplementedException();
    }

    public Task<TaskModel> Update(Guid id, TaskModel taskModel)
    {
        throw new NotImplementedException();
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
}
