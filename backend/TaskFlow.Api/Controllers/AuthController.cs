using Microsoft.AspNetCore.Mvc;
using TaskFlow.Application.DTOs.Auth;
using TaskFlow.Application.Interfaces;

namespace TaskFlow.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
  private readonly IAuthService _authService;

  public AuthController(IAuthService authService)
  {
    _authService = authService;
  }

  [HttpPost("register")]
  public async Task<ActionResult<AuthResponse>> Register(
      RegisterRequest request)
  {
    try
    {
      var response = await _authService.RegisterAsync(request);

      return Ok(response);
    }
    catch (InvalidOperationException exception)
    {
      return Conflict(new
      {
        message = exception.Message
      });
    }
  }

  [HttpPost("login")]
  public async Task<ActionResult<AuthResponse>> Login(
      LoginRequest request)
  {
    try
    {
      var response = await _authService.LoginAsync(request);

      return Ok(response);
    }
    catch (UnauthorizedAccessException exception)
    {
      return Unauthorized(new
      {
        message = exception.Message
      });
    }
  }
}