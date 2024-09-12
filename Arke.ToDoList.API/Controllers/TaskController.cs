﻿using Arke.ToDoList.API.Models;
using Arke.ToDoList.API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Arke.ToDoList.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _service;

        public TaskController(ITaskService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskModel>>> FindAllTasksAsync()
        {
            return Ok(await _service.FindAll());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<TaskModel>>> FindTaskByIdAsync(Guid id)
        {
            return Ok(await _service.FindById(id));
        }

        [HttpPost]
        public async Task<ActionResult<TaskModel>> SaveTaskAsync(TaskModel task)
        {
            return StatusCode((int)HttpStatusCode.Created, await _service.Save(task));
        }
    }
}
