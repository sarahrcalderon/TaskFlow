using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskFlow.Application.DTOs.Projects;
using TaskFlow.Application.Interfaces;

namespace TaskFlow.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ProjectsController : ControllerBase
{
  private readonly IProjectService _projectService;

  public ProjectsController(IProjectService projectService)
  {
    _projectService = projectService;
  }

  [HttpPost]
  public async Task<ActionResult<ProjectResponse>> Create(
      CreateProjectRequest request)
  {
    var ownerId = GetUserId();

    var project = await _projectService.CreateAsync(
        ownerId,
        request);

    return CreatedAtAction(
        nameof(GetById),
        new { id = project.Id },
        project);
  }

  [HttpGet]
  public async Task<ActionResult<IEnumerable<ProjectResponse>>> GetAll()
  {
    var ownerId = GetUserId();

    var projects = await _projectService.GetAllAsync(ownerId);

    return Ok(projects);
  }

  [HttpGet("{id:guid}")]
  public async Task<ActionResult<ProjectResponse>> GetById(Guid id)
  {
    var ownerId = GetUserId();

    try
    {
      var project = await _projectService.GetByIdAsync(
          ownerId,
          id);

      return Ok(project);
    }
    catch (KeyNotFoundException exception)
    {
      return NotFound(new
      {
        message = exception.Message
      });
    }
  }

  [HttpPut("{id:guid}")]
  public async Task<ActionResult<ProjectResponse>> Update(
      Guid id,
      UpdateProjectRequest request)
  {
    var ownerId = GetUserId();

    try
    {
      var project = await _projectService.UpdateAsync(
          ownerId,
          id,
          request);

      return Ok(project);
    }
    catch (KeyNotFoundException exception)
    {
      return NotFound(new
      {
        message = exception.Message
      });
    }
  }

  [HttpDelete("{id:guid}")]
  public async Task<IActionResult> Delete(Guid id)
  {
    var ownerId = GetUserId();

    try
    {
      await _projectService.DeleteAsync(
          ownerId,
          id);

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