﻿using Arke.ToDoList.API.Domain.Contracts;
using Arke.ToDoList.API.Domain.Entities;
using Arke.ToDoList.API.Domain.Exceptions;
using Arke.ToDoList.API.Domain.Mappings;
using Arke.ToDoList.API.Services;
using Arke.ToDoList.API.Shared.Enums;
using Arke.ToDoList.API.Shared.Models;
using Arke.ToDoList.Tests.Utils;
using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.Extensions.Logging;
using Moq;

namespace Arke.ToDoList.Tests.Services;

public class TaskServiceTests
{
    private readonly Mock<ITaskRepository> _mockTaskRepository = new();
    private readonly Mock<IUnitOfWork> _mockUnityOfWork = new();
    private readonly Mock<ILogger<TaskService>> _taskServiceMock = new();
    private readonly TaskService _taskService;

    public TaskServiceTests()
    {
        _taskService = new TaskService(_mockUnityOfWork.Object,
                                        _mockTaskRepository.Object,
                                        Mapper, _taskServiceMock.Object);
    }

    private static IMapper Mapper { get => new MapperConfiguration(mc => mc.AddMaps(typeof(TaskProfile).Assembly)).CreateMapper(); }

    [Fact]
    public async Task Add_Successfull()
    {
        // Arrange
        var taskModel = FakeTaskModels.GetFakeTaskModel<TaskModelWrite>();
        var task = FakeTaskEntities.GetFakeTask();
        _mockTaskRepository.Setup(x => x.Save(It.IsAny<TaskEntity>())).ReturnsAsync(task);
        // Act
        var result = await _taskService.SaveAsync(taskModel);

        // Assert
        result.Should().BeEquivalentTo(taskModel,
                                       options => options.ExcludingMissingMembers());
        _mockTaskRepository.Verify(x => x.Save(It.IsAny<TaskEntity>()), Times.Once);
    }

    [Fact]
    public async Task Add_Unsuccessfull_WhenNameEmpty()
    {
        // Arrange
        var taskModel = FakeTaskModels.GetFakeTaskModelEmptyName<TaskModelWrite>();
        // Act
        var result = () => _taskService.SaveAsync(taskModel);

        // Assert
        await result.Should().ThrowAsync<GeneralErrorException>().WithMessage("Name: Can't be null/empty.");
    }

    [Fact]
    public async Task Add_Unsuccessfull_WhenDescriptionNotEmptyAndLengthLessThan5Chars()
    {
        // Arrange
        var taskModel = FakeTaskModels.GetFakeTaskModelDescriptionLessThan5Chars<TaskModelWrite>();
        // Act
        var result = () => _taskService.SaveAsync(taskModel);

        // Assert
        await result.Should().ThrowAsync<GeneralErrorException>().WithMessage("Description: minimum length should be at least 5 characters.");
    }

    [Fact]
    public async Task Add_Unsuccessfull_WhenStatusDone()
    {
        // Arrange
        var taskModel = FakeTaskModels.GetFakeTaskModelDoneStatus<TaskModelWrite>();
        // Act
        var result = () => _taskService.SaveAsync(taskModel);

        // Assert
        await result.Should().ThrowAsync<GeneralErrorException>().WithMessage("Status: Task can't be created as done.");
    }

    [Fact]
    public async Task Add_Unsuccessfull_WhenIdExists()
    {
        // Arrange
        var taskModel = FakeTaskModels.GetFakeTaskModel<TaskModelWrite>();
        var task = FakeTaskEntities.GetFakeTask();
        _mockTaskRepository.Setup(x => x.Save(It.IsAny<TaskEntity>())).ReturnsAsync(task);
        _mockTaskRepository.Setup(x => x.FindById(It.IsAny<Guid>())).ReturnsAsync(task);
        // Act
        var result = () => _taskService.SaveAsync(taskModel);

        // Assert
        await result.Should().ThrowAsync<GeneralErrorException>().WithMessage("Id: Id already exists in database.");
    }

    [Fact]
    public async Task Update_Successfull()
    {
        // Arrange
        var taskModel = FakeTaskModels.GetFakeTaskModel<TaskModelWrite>();
        var task = FakeTaskEntities.GetFakeTask();
        _mockTaskRepository.Setup(x => x.FindById(task.Id)).ReturnsAsync(task);
        // Act
        var result = await _taskService.UpdateAsync(task.Id, taskModel);

        // Assert
        result.Should().BeEquivalentTo(taskModel,
                                       options => options.ExcludingMissingMembers());
        _mockTaskRepository.Verify(x => x.FindById(It.IsAny<Guid>()), Times.Once);
    }

    [Fact]
    public async Task Update_Unsuccessfull_WhenEntityNotFound()
    {
        // Arrange
        var taskModel = FakeTaskModels.GetFakeTaskModel<TaskModelWrite>();
        var task = FakeTaskEntities.GetFakeTask();
        // Act
        var result = () => _taskService.UpdateAsync(task.Id, taskModel);

        // Assert
        await result.Should().ThrowAsync<NotFoundException>().WithMessage($"BaseTaskModel with Id: '{task.Id}' does not exist.");
    }

    [Fact]
    public async Task FindById_Successfull()
    {
        // Arrange
        var guid = Guid.NewGuid();
        var taskModel = FakeTaskModels.GetFakeTaskModelWithId<TaskModelRead>(guid);
        var task = FakeTaskEntities.GetFakeTaskWithId(guid);
        _mockTaskRepository.Setup(x => x.FindById(guid)).ReturnsAsync(task);
        // Act
        var result = await _taskService.FindByIdAsync(guid);

        // Assert
        result.Should().BeEquivalentTo(taskModel,
                                       options => options.ExcludingMissingMembers());
        _mockTaskRepository.Verify(x => x.FindById(It.IsAny<Guid>()), Times.Once);
    }

    [Fact]
    public async Task FindAll_Successfull()
    {
        // Arrange
        var guid = Guid.NewGuid();
        var taskModel = FakeTaskModels.GetFakeTaskModelWithId<TaskModelRead>(guid);
        var task = FakeTaskEntities.GetFakeTasks();
        _mockTaskRepository.Setup(x => x.FindAll()).ReturnsAsync(task);

        // Act
        var result = await _taskService.FindAllAsync();

        // Assert
        result.Should().HaveCount(5);
        _mockTaskRepository.Verify(x => x.FindAll(), Times.Once);
    }

    [Fact]
    public void DeleteCompletedTasks_Successfull()
    {
        // Arrange
        var tasks = FakeTaskEntities.GetFakeDoneTasks();
        _mockTaskRepository.Setup(x => x.GetAllCompletedTasks()).ReturnsAsync(tasks);

        // Act
        var result = _taskService.DeleteCompletedTasksAsync();

        // Assert
        _mockTaskRepository.Verify(x => x.Delete(It.IsAny<TaskEntity>()), Times.Exactly(5));
        _mockTaskRepository.Verify(x => x.GetAllCompletedTasks(), Times.Once);
    }

    [Fact]
    public async Task Patch_Sucessfull_WhenChangingStatusToInProgress()
    {
        // Arrange
        var taskModel = FakeTaskModels.GetFakeTaskModel<TaskModelWrite>();
        var task = FakeTaskEntities.GetFakeTaskWithId(taskModel.Id);
        _mockTaskRepository.Setup(x => x.FindById(taskModel.Id)).ReturnsAsync(task);
        var jsonobject = new JsonPatchDocument<TaskModelWrite>();
        var operation = new Microsoft.AspNetCore.JsonPatch.Operations.Operation<TaskModelWrite> { op = "replace", path = "/status", value = "2" };
        jsonobject.Operations.Add(operation);

        // Act
        var result = await _taskService.PatchAsync(taskModel.Id, jsonobject);

        // Assert
        result.Status.Should().Be(TaskStatusEnum.InProgress);
    }
}
