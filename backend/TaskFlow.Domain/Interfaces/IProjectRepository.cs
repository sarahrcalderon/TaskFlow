using TaskFlow.Domain.Entities;

namespace TaskFlow.Domain.Interfaces;

public interface IProjectRepository
{
  Task<Project> AddAsync(Project project);

  Task<IEnumerable<Project>> GetAllByOwnerAsync(Guid ownerId);

  Task<Project?> GetByIdAsync(Guid id, Guid ownerId);

  Task UpdateAsync(Project project);

  Task DeleteAsync(Project project);

  Task SaveChangesAsync();
}