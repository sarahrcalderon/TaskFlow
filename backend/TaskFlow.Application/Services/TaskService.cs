using TaskFlow.Application.DTOs.Tasks;
using TaskFlow.Application.Interfaces;
using TaskFlow.Domain.Entities;
using TaskFlow.Domain.Interfaces;

namespace TaskFlow.Application.Services;

public class TaskService : ITaskService
{
  private readonly ITaskRepository _taskRepository;
  private readonly IProjectRepository _projectRepository;

  public TaskService(
      ITaskRepository taskRepository,
      IProjectRepository projectRepository)
  {
    _taskRepository = taskRepository;
    _projectRepository = projectRepository;
  }

  public async Task<TaskResponse> CreateAsync(
      Guid ownerId,
      Guid projectId,
      CreateTaskRequest request)
  {
    var project = await _projectRepository
        .GetByIdAsync(projectId, ownerId);

    if (project is null)
    {
      throw new KeyNotFoundException("Project not found.");
    }

    var task = new TaskItem(
        request.Title,
        request.Description,
        projectId,
        request.Priority,
        request.DueDate);

    await _taskRepository.AddAsync(task);
    await _taskRepository.SaveChangesAsync();

    return MapToResponse(task);
  }

  public async Task<IEnumerable<TaskResponse>> GetAllAsync(
      Guid ownerId,
      Guid projectId)
  {
    var project = await _projectRepository
        .GetByIdAsync(projectId, ownerId);

    if (project is null)
    {
      throw new KeyNotFoundException("Project not found.");
    }

    var tasks = await _taskRepository
        .GetAllByProjectAsync(projectId);

    return tasks.Select(MapToResponse);
  }

  public async Task<TaskResponse> GetByIdAsync(
      Guid ownerId,
      Guid projectId,
      Guid taskId)
  {
    var project = await _projectRepository
        .GetByIdAsync(projectId, ownerId);

    if (project is null)
    {
      throw new KeyNotFoundException("Project not found.");
    }

    var task = await _taskRepository
        .GetByIdAsync(taskId, projectId);

    if (task is null)
    {
      throw new KeyNotFoundException("Task not found.");
    }

    return MapToResponse(task);
  }

  public async Task<TaskResponse> UpdateAsync(
      Guid ownerId,
      Guid projectId,
      Guid taskId,
      UpdateTaskRequest request)
  {
    var project = await _projectRepository
        .GetByIdAsync(projectId, ownerId);

    if (project is null)
    {
      throw new KeyNotFoundException("Project not found.");
    }

    var task = await _taskRepository
        .GetByIdAsync(taskId, projectId);

    if (task is null)
    {
      throw new KeyNotFoundException("Task not found.");
    }

    task.Update(
        request.Title,
        request.Description,
        request.Priority,
        request.Status,
        request.DueDate);

    await _taskRepository.UpdateAsync(task);
    await _taskRepository.SaveChangesAsync();

    return MapToResponse(task);
  }

  public async Task DeleteAsync(
      Guid ownerId,
      Guid projectId,
      Guid taskId)
  {
    var project = await _projectRepository
        .GetByIdAsync(projectId, ownerId);

    if (project is null)
    {
      throw new KeyNotFoundException("Project not found.");
    }

    var task = await _taskRepository
        .GetByIdAsync(taskId, projectId);

    if (task is null)
    {
      throw new KeyNotFoundException("Task not found.");
    }

    await _taskRepository.DeleteAsync(task);
    await _taskRepository.SaveChangesAsync();
  }

  private static TaskResponse MapToResponse(TaskItem task)
  {
    return new TaskResponse(
        task.Id,
        task.Title,
        task.Description,
        task.Status,
        task.Priority,
        task.CreatedAt,
        task.DueDate,
        task.ProjectId);
  }

}
