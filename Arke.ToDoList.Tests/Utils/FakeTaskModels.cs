using Arke.ToDoList.API.Shared.Enums;
using Arke.ToDoList.API.Shared.Models;
using Bogus;

namespace Arke.ToDoList.Tests.Utils;

internal class FakeTaskModels
{
    public static T GetFakeTaskModel<T>() where T : BaseTaskModel
    {
        return new Faker<T>()
            .RuleFor(u => u.Name, "Task of arke")
            .RuleFor(u => u.Description, "This is challenge requested by arke")
            .RuleFor(u => u.Status, TaskStatusEnum.ToDo)
            .Generate();
    }

    public static T GetFakeTaskModelWithId<T>(Guid id) where T : BaseTaskModel
    {
        return new Faker<T>()
            .RuleFor(u => u.Id, id)
            .RuleFor(u => u.Name, "Task of arke")
            .RuleFor(u => u.Description, "This is challenge requested by arke")
            .RuleFor(u => u.Status, TaskStatusEnum.ToDo)
            .Generate();
    }

    public static T GetFakeTaskModelDoneStatus<T>() where T : BaseTaskModel
    {
        return new Faker<T>()
            .RuleFor(u => u.Name, f => f.Random.ToString())
            .RuleFor(u => u.Description, f => f.Random.ToString())
            .RuleFor(u => u.Status, TaskStatusEnum.Done)
            .Generate();
    }

    public static T GetFakeTaskModelEmptyName<T>() where T : BaseTaskModel
    {
        return new Faker<T>()
            .RuleFor(u => u.Name, string.Empty)
            .RuleFor(u => u.Description, f => f.Random.ToString())
            .RuleFor(u => u.Status, TaskStatusEnum.ToDo)
            .Generate();
    }

    public static T GetFakeTaskModelDescriptionLessThan5Chars<T>() where T : BaseTaskModel
    {
        return new Faker<T>()
            .RuleFor(u => u.Name, f => f.Random.ToString())
            .RuleFor(u => u.Description, f => "1234")
            .RuleFor(u => u.Status, TaskStatusEnum.ToDo)
            .Generate();
    }
}
