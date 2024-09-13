using Arke.ToDoList.API.Models;
using Bogus;

namespace Arke.ToDoList.Tests.Utils;

internal class FakeTaskModels
{
    public static TaskModel GetFakeTaskModel()
    {
        return new Faker<TaskModel>()
            .RuleFor(u => u.Name, "Task of arke")
            .RuleFor(u => u.Description, "This is challenge requested by arke")
            .RuleFor(u => u.Status  , API.Enums.TaskStatusEnum.ToDo)
            .Generate();
    }

    public static TaskModel GetFakeTaskModelWithId(Guid id)
    {
        return new Faker<TaskModel>()
            .RuleFor(u => u.Id, id)
            .RuleFor(u => u.Name, "Task of arke")
            .RuleFor(u => u.Description, "This is challenge requested by arke")
            .RuleFor(u => u.Status, API.Enums.TaskStatusEnum.ToDo)
            .Generate();
    }

    public static TaskModel GetFakeTaskModelDoneStatus()
    {
        return new Faker<TaskModel>()
            .RuleFor(u => u.Name, f => f.Random.ToString())
            .RuleFor(u => u.Description, f => f.Random.ToString())
            .RuleFor(u => u.Status, API.Enums.TaskStatusEnum.Done)
            .Generate();
    }

    public static TaskModel GetFakeTaskModelEmptyName()
    {
        return new Faker<TaskModel>()
            .RuleFor(u => u.Name, string.Empty)
            .RuleFor(u => u.Description, f => f.Random.ToString())
            .RuleFor(u => u.Status, API.Enums.TaskStatusEnum.ToDo)
            .Generate();
    }

    public static TaskModel GetFakeTaskModelDescriptionLessThan5Chars()
    {
        return new Faker<TaskModel>()
            .RuleFor(u => u.Name, f => f.Random.ToString())
            .RuleFor(u => u.Description, f => "1234")
            .RuleFor(u => u.Status, API.Enums.TaskStatusEnum.ToDo)
            .Generate();
    }
}
