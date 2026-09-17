using Moq;
using TaskFlow.Api.Controllers;
using TaskFlow.Application.DTOs.Auth;
using TaskFlow.Application.Interfaces;
using Xunit;

namespace TaskFlow.Tests.Unit.Controllers;

public class AuthControllerTests
{
  private readonly Mock<IAuthService> _authService;
  private readonly AuthController _controller;

  public AuthControllerTests()
  {
    _authService = new Mock<IAuthService>();
    _controller = new AuthController(_authService.Object);
  }

  [Fact]
  public async Task Register_ShouldReturnOk_WhenRegistrationSucceeds()
  {
    var request = new RegisterRequest(
        "Sarah",
        "sarah@taskflow.com",
        "123456");

    var response = new AuthResponse(
        Guid.NewGuid(),
        "Sarah",
        "sarah@taskflow.com",
        "token");

    _authService
        .Setup(service => service.RegisterAsync(request))
        .ReturnsAsync(response);

    var result = await _controller.Register(request);

    var okResult = Assert.IsType<Microsoft.AspNetCore.Mvc.OkObjectResult>(
        result.Result);

    var actualResponse = Assert.IsType<AuthResponse>(
        okResult.Value);

    Assert.Equal(response.UserId, actualResponse.UserId);
    Assert.Equal(response.Email, actualResponse.Email);
    Assert.Equal(response.Token, actualResponse.Token);
  }

  [Fact]
  public async Task Register_ShouldReturnConflict_WhenEmailAlreadyExists()
  {
    var request = new RegisterRequest(
        "Sarah",
        "sarah@taskflow.com",
        "123456");

    _authService
        .Setup(service => service.RegisterAsync(request))
        .ThrowsAsync(new InvalidOperationException(
            "Email already registered."));

    var result = await _controller.Register(request);

    var conflictResult = Assert.IsType<Microsoft.AspNetCore.Mvc.ConflictObjectResult>(
        result.Result);

    Assert.NotNull(conflictResult.Value);
  }

  [Fact]
  public async Task Login_ShouldReturnOk_WhenCredentialsAreValid()
  {
    var request = new LoginRequest(
        "sarah@taskflow.com",
        "123456");

    var response = new AuthResponse(
        Guid.NewGuid(),
        "Sarah",
        "sarah@taskflow.com",
        "token");

    _authService
        .Setup(service => service.LoginAsync(request))
        .ReturnsAsync(response);

    var result = await _controller.Login(request);

    var okResult = Assert.IsType<Microsoft.AspNetCore.Mvc.OkObjectResult>(
        result.Result);

    var actualResponse = Assert.IsType<AuthResponse>(
        okResult.Value);

    Assert.Equal(response.UserId, actualResponse.UserId);
    Assert.Equal(response.Email, actualResponse.Email);
    Assert.Equal(response.Token, actualResponse.Token);
  }

  [Fact]
  public async Task Login_ShouldReturnUnauthorized_WhenCredentialsAreInvalid()
  {
    var request = new LoginRequest(
        "sarah@taskflow.com",
        "wrong-password");

    _authService
        .Setup(service => service.LoginAsync(request))
        .ThrowsAsync(new UnauthorizedAccessException(
            "Invalid email or password."));

    var result = await _controller.Login(request);

    var unauthorizedResult =
        Assert.IsType<Microsoft.AspNetCore.Mvc.UnauthorizedObjectResult>(
            result.Result);

    Assert.NotNull(unauthorizedResult.Value);
  }
}