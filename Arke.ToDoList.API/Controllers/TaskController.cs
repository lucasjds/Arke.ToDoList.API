using Arke.ToDoList.API.Domain.Contracts;
using Arke.ToDoList.API.Shared.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;
using System.Net;

namespace Arke.ToDoList.API.Controllers;

[ApiController]
[Route("[controller]")]
[ExcludeFromCodeCoverage]
public class TaskController : ControllerBase
{
    private readonly ITaskService _service;

    public TaskController(ITaskService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TaskModelRead>>> FindAllTasksAsync()
    {
        return Ok(await _service.FindAllAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<IEnumerable<TaskModelRead>>> FindTaskByIdAsync([FromRoute] Guid id)
    {
        return Ok(await _service.FindByIdAsync(id));
    }

    [HttpPost]
    public async Task<ActionResult<TaskModelRead>> SaveTaskAsync([FromBody] TaskModelWrite task)
    {
        return StatusCode((int)HttpStatusCode.Created, await _service.SaveAsync(task));
    }

    [HttpPatch("{id}")]
    public async Task<ActionResult<TaskModelRead>> PatchTaskAsync([FromRoute] Guid id, [FromBody] JsonPatchDocument<TaskModelWrite> task)
    {
        var result = await _service.PatchAsync(id, task);
        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<TaskModelRead>> UpdateTaskAsync([FromRoute] Guid id, [FromBody] TaskModelWrite taskModel)
    {
        var result = await _service.UpdateAsync(id, taskModel);
        return Ok(result);
    }

    [HttpDelete("completed-tasks")]
    public async Task<IActionResult> DeleteAllCompletedTasksAsync()
    {
        await _service.DeleteCompletedTasksAsync();
        return NoContent();
    }
}
