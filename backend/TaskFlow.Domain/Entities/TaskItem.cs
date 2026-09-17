namespace TaskFlow.Domain.Entities;

public class TaskItem
{
  public Guid Id { get; private set; }

  public string Title { get; private set; }

  public string Description { get; private set; }

  public TaskFlow.Domain.Enums.TaskStatus Status { get; private set; }

  public TaskFlow.Domain.Enums.TaskPriority Priority { get; private set; }

  public DateTime CreatedAt { get; private set; }

  public DateTime? DueDate { get; private set; }

  public Guid ProjectId { get; private set; }

  public Project? Project { get; private set; }

  public TaskItem(
      string title,
      string description,
      Guid projectId,
      TaskFlow.Domain.Enums.TaskPriority priority,
      DateTime? dueDate = null)
  {
    Id = Guid.NewGuid();
    Title = title;
    Description = description;
    ProjectId = projectId;
    Priority = priority;
    DueDate = dueDate;
    Status = TaskFlow.Domain.Enums.TaskStatus.Pending;
    CreatedAt = DateTime.UtcNow;
  }

  public void Update(
      string title,
      string description,
      TaskFlow.Domain.Enums.TaskPriority priority,
      TaskFlow.Domain.Enums.TaskStatus status,
      DateTime? dueDate)
  {
    Title = title;
    Description = description;
    Priority = priority;
    Status = status;
    DueDate = dueDate;
  }
}