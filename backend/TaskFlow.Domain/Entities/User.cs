namespace TaskFlow.Domain.Entities;

public class User
{
  public Guid Id { get; private set; }

  public string Name { get; private set; }

  public string Email { get; private set; }

  public string PasswordHash { get; private set; }

  public DateTime CreatedAt { get; private set; }

  public ICollection<Project> Projects { get; private set; }

  public User(
      string name,
      string email,
      string passwordHash)
  {
    Id = Guid.NewGuid();
    Name = name;
    Email = email;
    PasswordHash = passwordHash;
    CreatedAt = DateTime.UtcNow;
    Projects = new List<Project>();
  }
}