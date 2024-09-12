using Arke.ToDoList.API.DataAccess.Entities;
using Arke.ToDoList.API.DataAccess.Repositories.Interfaces;
using Arke.ToDoList.API.DataAccess.UnitOfWork;
using Arke.ToDoList.API.Models;
using Arke.ToDoList.API.Services.Interfaces;
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

    public Task<IEnumerable<TaskModel>> FindAll()
    {
        throw new NotImplementedException();
    }

    public Task<TaskModel> FindById(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<TaskModel> Save(TaskModel taskModel)
    {
        throw new NotImplementedException();
    }

    public Task<TaskModel> Update(Guid id, TaskModel taskModel)
    {
        throw new NotImplementedException();
    }
}
