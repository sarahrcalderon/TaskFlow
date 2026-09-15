using BCrypt.Net;
using TaskFlow.Application.DTOs.Auth;
using TaskFlow.Application.Interfaces;
using TaskFlow.Domain.Entities;
using TaskFlow.Domain.Interfaces;

namespace TaskFlow.Application.Services;

public class AuthService : IAuthService
{
  private readonly IUserRepository _userRepository;
  private readonly ITokenService _tokenService;

  public AuthService(
      IUserRepository userRepository,
      ITokenService tokenService)
  {
    _userRepository = userRepository;
    _tokenService = tokenService;
  }

  public async Task<AuthResponse> RegisterAsync(RegisterRequest request)
  {
    var existingUser = await _userRepository.GetByEmailAsync(request.Email);

    if (existingUser is not null)
    {
      throw new InvalidOperationException("Email already registered.");
    }

    var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

    var user = new User(
        request.Name,
        request.Email,
        passwordHash);

    await _userRepository.AddAsync(user);
    await _userRepository.SaveChangesAsync();

    var token = _tokenService.GenerateToken(
        user.Id,
        user.Name,
        user.Email);

    return new AuthResponse(
        user.Id,
        user.Name,
        user.Email,
        token);
  }

  public async Task<AuthResponse> LoginAsync(LoginRequest request)
  {
    var user = await _userRepository.GetByEmailAsync(request.Email);

    if (user is null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
    {
      throw new UnauthorizedAccessException("Invalid email or password.");
    }

    var token = _tokenService.GenerateToken(
        user.Id,
        user.Name,
        user.Email);

    return new AuthResponse(
        user.Id,
        user.Name,
        user.Email,
        token);
  }
}