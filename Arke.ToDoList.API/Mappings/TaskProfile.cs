using Arke.ToDoList.API.DataAccess.Entities;
using Arke.ToDoList.API.Models;
using AutoMapper;

namespace Arke.ToDoList.API.Mappings;

public class TaskProfile : Profile
{
    public TaskProfile()
    {
        CreateMap<TaskEntity, TaskModel>();
        CreateMap<TaskModel, TaskEntity>();
    }
}
