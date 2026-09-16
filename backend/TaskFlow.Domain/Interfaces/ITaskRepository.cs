using TaskFlow.Domain.Entities;
namespace TaskFlow.Domain.Interfaces;

public interface ITaskRepository
{
  Task<TaskItem> AddAsync(TaskItem task);

  Task<IEnumerable<TaskItem>> GetAllByProjectAsync(Guid projectId);

  Task<TaskItem?> GetByIdAsync(Guid taskId, Guid projectId);

  Task UpdateAsync(TaskItem task);

  Task DeleteAsync(TaskItem task);

  Task SaveChangesAsync();

}
