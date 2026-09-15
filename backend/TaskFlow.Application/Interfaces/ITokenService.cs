namespace TaskFlow.Application.Interfaces;

public interface ITokenService
{
  string GenerateToken(Guid userId, string name, string email);
}