using Arke.ToDoList.API.DataAccess.Entities;
using Arke.ToDoList.API.Models;
using AutoMapper;
using System.Diagnostics.CodeAnalysis;

namespace Arke.ToDoList.API.Mappings;

[ExcludeFromCodeCoverage]
public class TaskProfile : Profile
{
    public TaskProfile()
    {
        CreateMap<TaskEntity, TaskModel>();
        CreateMap<TaskModel, TaskEntity>();
    }
}
