namespace TaskFlow.Domain.Entities;

public class Project
{
  public Guid Id { get; private set; }

  public string Name { get; private set; }

  public string Description { get; private set; }

  public DateTime CreatedAt { get; private set; }

  public Guid OwnerId { get; private set; }

  public User Owner { get; private set; }

  public ICollection<TaskItem> Tasks { get; private set; }

  public Project(
      string name,
      string description,
      Guid ownerId)
  {
    Id = Guid.NewGuid();
    Name = name;
    Description = description;
    OwnerId = ownerId;
    CreatedAt = DateTime.UtcNow;
    Tasks = new List<TaskItem>();
  }
}