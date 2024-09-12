using Arke.ToDoList.API.Enums;

namespace Arke.ToDoList.API.DataAccess.Entities;

public class TaskEntity : IEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public TaskStatusEnum Status { get; set; }

    public DateTime Created { get; set; }
    public DateTime TodoTime { get; set; }
    public DateTime InProgressTime { get; set; }
    public DateTime DoneTime { get; set; }
}
