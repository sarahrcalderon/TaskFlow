using TaskFlow.Application.DTOs.Projects;
using TaskFlow.Application.Interfaces;
using TaskFlow.Domain.Entities;
using TaskFlow.Domain.Interfaces;

namespace TaskFlow.Application.Services;

public class ProjectService : IProjectService
{
  private readonly IProjectRepository _projectRepository;

  public ProjectService(IProjectRepository projectRepository)
  {
    _projectRepository = projectRepository;
  }

  public async Task<ProjectResponse> CreateAsync(
      Guid ownerId,
      CreateProjectRequest request)
  {
    var project = new Project(
        request.Name,
        request.Description,
        ownerId);

    await _projectRepository.AddAsync(project);
    await _projectRepository.SaveChangesAsync();

    return MapToResponse(project);
  }

  public async Task<IEnumerable<ProjectResponse>> GetAllAsync(
      Guid ownerId)
  {
    var projects = await _projectRepository
        .GetAllByOwnerAsync(ownerId);

    return projects.Select(MapToResponse);
  }

  public async Task<ProjectResponse> GetByIdAsync(
      Guid ownerId,
      Guid projectId)
  {
    var project = await _projectRepository
        .GetByIdAsync(projectId, ownerId);

    if (project is null)
    {
      throw new KeyNotFoundException("Project not found.");
    }

    return MapToResponse(project);
  }

  public async Task<ProjectResponse> UpdateAsync(
      Guid ownerId,
      Guid projectId,
      UpdateProjectRequest request)
  {
    var project = await _projectRepository
        .GetByIdAsync(projectId, ownerId);

    if (project is null)
    {
      throw new KeyNotFoundException("Project not found.");
    }

    var updatedProject = new Project(
        request.Name,
        request.Description,
        ownerId);

    var projectType = typeof(Project);

    projectType
        .GetProperty(nameof(Project.Name))!
        .SetValue(project, request.Name);

    projectType
        .GetProperty(nameof(Project.Description))!
        .SetValue(project, request.Description);

    await _projectRepository.UpdateAsync(project);
    await _projectRepository.SaveChangesAsync();

    return MapToResponse(project);
  }

  public async Task DeleteAsync(
      Guid ownerId,
      Guid projectId)
  {
    var project = await _projectRepository
        .GetByIdAsync(projectId, ownerId);

    if (project is null)
    {
      throw new KeyNotFoundException("Project not found.");
    }

    await _projectRepository.DeleteAsync(project);
    await _projectRepository.SaveChangesAsync();
  }

  private static ProjectResponse MapToResponse(Project project)
  {
    return new ProjectResponse(
        project.Id,
        project.Name,
        project.Description,
        project.CreatedAt,
        project.OwnerId);
  }
}