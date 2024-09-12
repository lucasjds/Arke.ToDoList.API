using Arke.ToDoList.API.Enums;

namespace Arke.ToDoList.API.Models;

public class TaskModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public TaskStatusEnum Status { get; set; }
}
