﻿using Arke.ToDoList.API.Domain.Entities;
using Arke.ToDoList.API.Shared.Models;
using AutoMapper;
using System.Diagnostics.CodeAnalysis;

namespace Arke.ToDoList.API.Domain.Mappings;

[ExcludeFromCodeCoverage]
public class TaskProfile : Profile
{
    public TaskProfile()
    {
        CreateMap<TaskEntity, TaskModelWrite>();
        CreateMap<TaskModelWrite, TaskEntity>();

        CreateMap<TaskEntity, TaskModelRead>();
        CreateMap<TaskModelRead, TaskEntity>();
    }
}
