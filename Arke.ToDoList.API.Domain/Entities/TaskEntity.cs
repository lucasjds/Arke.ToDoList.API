using Arke.ToDoList.API.Domain.Contracts;
using Arke.ToDoList.API.Shared.Enums;

namespace Arke.ToDoList.API.Domain.Entities;

public class TaskEntity : IEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public TaskStatusEnum Status { get; set; }
    public DateTime Created { get; set; }
}
