using Arke.ToDoList.API.Shared.Enums;

namespace Arke.ToDoList.API.Shared.Models;

public class TaskModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public TaskStatusEnum Status { get; set; }
}
