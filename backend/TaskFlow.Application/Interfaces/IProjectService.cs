using TaskFlow.Application.DTOs.Projects;

namespace TaskFlow.Application.Interfaces;

public interface IProjectService
{
  Task<ProjectResponse> CreateAsync(
      Guid ownerId,
      CreateProjectRequest request);

  Task<IEnumerable<ProjectResponse>> GetAllAsync(
      Guid ownerId);

  Task<ProjectResponse> GetByIdAsync(
      Guid ownerId,
      Guid projectId);

  Task<ProjectResponse> UpdateAsync(
      Guid ownerId,
      Guid projectId,
      UpdateProjectRequest request);

  Task DeleteAsync(
      Guid ownerId,
      Guid projectId);
}