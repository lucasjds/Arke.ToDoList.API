using Arke.ToDoList.API.DataAccess.Entities;
using Arke.ToDoList.API.Shared.Enums;
using Bogus;

namespace Arke.ToDoList.Tests.Utils;

internal class FakeTaskEntities
{
    public static TaskEntity GetFakeTask()
    {
        return new Faker<TaskEntity>()
            .RuleFor(u => u.Id, f => f.Random.Guid())
            .RuleFor(u => u.Name, "Task of arke")
            .RuleFor(u => u.Description, "This is challenge requested by arke")
            .RuleFor(u => u.Status, TaskStatusEnum.ToDo)
            .Generate();
    }

    public static TaskEntity GetFakeTaskWithId(Guid id)
    {
        return new Faker<TaskEntity>()
            .RuleFor(u => u.Id, id)
            .RuleFor(u => u.Name, "Task of arke")
            .RuleFor(u => u.Description, "This is challenge requested by arke")
            .RuleFor(u => u.Status, TaskStatusEnum.ToDo)
            .Generate();
    }

    public static List<TaskEntity> GetFakeTasks()
    {
        return new Faker<TaskEntity>()
            .RuleFor(u => u.Id, f => f.Random.Guid())
            .RuleFor(u => u.Name, f => f.Random.String())
            .RuleFor(u => u.Description, f => f.Random.String())
            .RuleFor(u => u.Status, TaskStatusEnum.ToDo)
            .Generate(5);
    }

    public static List<TaskEntity> GetFakeDoneTasks()
    {
        return new Faker<TaskEntity>()
            .RuleFor(u => u.Id, f => f.Random.Guid())
            .RuleFor(u => u.Name, f => f.Random.String())
            .RuleFor(u => u.Description, f => f.Random.String())
            .RuleFor(u => u.Status, TaskStatusEnum.Done)
            .Generate(5);
    }
}
