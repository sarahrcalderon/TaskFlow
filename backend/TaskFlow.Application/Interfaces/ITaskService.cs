using TaskFlow.Application.DTOs.Tasks;

namespace TaskFlow.Application.Interfaces;

public interface ITaskService
{
  Task<TaskResponse> CreateAsync(
  Guid ownerId,
  Guid projectId,
  CreateTaskRequest request);

  Task<IEnumerable<TaskResponse>> GetAllAsync(
      Guid ownerId,
      Guid projectId);

  Task<TaskResponse> GetByIdAsync(
      Guid ownerId,
      Guid projectId,
      Guid taskId);

  Task<TaskResponse> UpdateAsync(
      Guid ownerId,
      Guid projectId,
      Guid taskId,
      UpdateTaskRequest request);

  Task DeleteAsync(
      Guid ownerId,
      Guid projectId,
      Guid taskId);


}
