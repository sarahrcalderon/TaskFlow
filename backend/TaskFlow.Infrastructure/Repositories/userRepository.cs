using Microsoft.EntityFrameworkCore;
using TaskFlow.Domain.Entities;
using TaskFlow.Domain.Interfaces;
using TaskFlow.Infrastructure.Data;

namespace TaskFlow.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
  private readonly TaskFlowDbContext _context;

  public UserRepository(TaskFlowDbContext context)
  {
    _context = context;
  }

  public async Task<User?> GetByEmailAsync(string email)
  {
    return await _context.Users
        .FirstOrDefaultAsync(user => user.Email == email);
  }

  public async Task<User?> GetByIdAsync(Guid id)
  {
    return await _context.Users
        .FirstOrDefaultAsync(user => user.Id == id);
  }

  public async Task AddAsync(User user)
  {
    await _context.Users.AddAsync(user);
  }

  public async Task SaveChangesAsync()
  {
    await _context.SaveChangesAsync();
  }
}