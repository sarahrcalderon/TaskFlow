using Microsoft.EntityFrameworkCore;
using TaskFlow.Domain.Entities;
using TaskFlow.Domain.Interfaces;
using TaskFlow.Infrastructure.Data;

namespace TaskFlow.Infrastructure.Repositories;

public class ProjectRepository : IProjectRepository
{
  private readonly TaskFlowDbContext _context;

  public ProjectRepository(TaskFlowDbContext context)
  {
    _context = context;
  }

  public async Task<Project> AddAsync(Project project)
  {
    await _context.Projects.AddAsync(project);

    return project;
  }

  public async Task<IEnumerable<Project>> GetAllByOwnerAsync(Guid ownerId)
  {
    return await _context.Projects
        .Where(project => project.OwnerId == ownerId)
        .OrderByDescending(project => project.CreatedAt)
        .ToListAsync();
  }

  public async Task<Project?> GetByIdAsync(Guid id, Guid ownerId)
  {
    return await _context.Projects
        .FirstOrDefaultAsync(project =>
            project.Id == id &&
            project.OwnerId == ownerId);
  }

  public Task UpdateAsync(Project project)
  {
    _context.Projects.Update(project);

    return Task.CompletedTask;
  }

  public Task DeleteAsync(Project project)
  {
    _context.Projects.Remove(project);

    return Task.CompletedTask;
  }

  public async Task SaveChangesAsync()
  {
    await _context.SaveChangesAsync();
  }
}