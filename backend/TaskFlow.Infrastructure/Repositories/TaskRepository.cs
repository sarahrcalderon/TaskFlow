using Microsoft.EntityFrameworkCore;
using TaskFlow.Domain.Entities;
using TaskFlow.Domain.Interfaces;
using TaskFlow.Infrastructure.Data;

namespace TaskFlow.Infrastructure.Repositories;

public class TaskRepository : ITaskRepository
{
  private readonly TaskFlowDbContext _context;


  public TaskRepository(TaskFlowDbContext context)
  {
    _context = context;
  }

  public async Task<TaskItem> AddAsync(TaskItem task)
  {
    await _context.Tasks.AddAsync(task);

    return task;
  }

  public async Task<IEnumerable<TaskItem>> GetAllByProjectAsync(Guid projectId)
  {
    return await _context.Tasks
        .Where(task => task.ProjectId == projectId)
        .OrderByDescending(task => task.CreatedAt)
        .ToListAsync();
  }

  public async Task<TaskItem?> GetByIdAsync(Guid taskId, Guid projectId)
  {
    return await _context.Tasks
        .FirstOrDefaultAsync(task =>
            task.Id == taskId &&
            task.ProjectId == projectId);
  }

  public Task UpdateAsync(TaskItem task)
  {
    _context.Tasks.Update(task);

    return Task.CompletedTask;
  }

  public Task DeleteAsync(TaskItem task)
  {
    _context.Tasks.Remove(task);

    return Task.CompletedTask;
  }

  public async Task SaveChangesAsync()
  {
    await _context.SaveChangesAsync();
  }

}
