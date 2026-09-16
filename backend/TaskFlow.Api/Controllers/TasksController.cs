using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskFlow.Application.DTOs.Tasks;
using TaskFlow.Application.Interfaces;

namespace TaskFlow.Api.Controllers;

[ApiController]
[Route("api/Projects/{projectId:guid}/Tasks")]
[Authorize]
public class TasksController : ControllerBase
{
  private readonly ITaskService _taskService;

  public TasksController(ITaskService taskService)
  {
    _taskService = taskService;
  }

  [HttpPost]
  public async Task<ActionResult<TaskResponse>> Create(
      Guid projectId,
      CreateTaskRequest request)
  {
    var ownerId = GetUserId();

    try
    {
      var task = await _taskService.CreateAsync(
          ownerId,
          projectId,
          request);

      return CreatedAtAction(
          nameof(GetById),
          new
          {
            projectId,
            taskId = task.Id
          },
          task);
    }
    catch (KeyNotFoundException exception)
    {
      return NotFound(new
      {
        message = exception.Message
      });
    }
  }

  [HttpGet]
  public async Task<ActionResult<IEnumerable<TaskResponse>>> GetAll(
      Guid projectId)
  {
    var ownerId = GetUserId();

    try
    {
      var tasks = await _taskService.GetAllAsync(
          ownerId,
          projectId);

      return Ok(tasks);
    }
    catch (KeyNotFoundException exception)
    {
      return NotFound(new
      {
        message = exception.Message
      });
    }
  }

  [HttpGet("{taskId:guid}")]
  public async Task<ActionResult<TaskResponse>> GetById(
      Guid projectId,
      Guid taskId)
  {
    var ownerId = GetUserId();

    try
    {
      var task = await _taskService.GetByIdAsync(
          ownerId,
          projectId,
          taskId);

      return Ok(task);
    }
    catch (KeyNotFoundException exception)
    {
      return NotFound(new
      {
        message = exception.Message
      });
    }
  }

  [HttpPut("{taskId:guid}")]
  public async Task<ActionResult<TaskResponse>> Update(
      Guid projectId,
      Guid taskId,
      UpdateTaskRequest request)
  {
    var ownerId = GetUserId();

    try
    {
      var task = await _taskService.UpdateAsync(
          ownerId,
          projectId,
          taskId,
          request);

      return Ok(task);
    }
    catch (KeyNotFoundException exception)
    {
      return NotFound(new
      {
        message = exception.Message
      });
    }
  }

  [HttpDelete("{taskId:guid}")]
  public async Task<IActionResult> Delete(
      Guid projectId,
      Guid taskId)
  {
    var ownerId = GetUserId();

    try
    {
      await _taskService.DeleteAsync(
          ownerId,
          projectId,
          taskId);

      return NoContent();
    }
    catch (KeyNotFoundException exception)
    {
      return NotFound(new
      {
        message = exception.Message
      });
    }
  }

  private Guid GetUserId()
  {
    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)
        ?? User.FindFirstValue("sub");

    if (!Guid.TryParse(userId, out var parsedUserId))
    {
      throw new UnauthorizedAccessException(
          "Authenticated user ID is invalid.");
    }

    return parsedUserId;
  }

}
