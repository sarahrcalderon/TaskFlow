namespace TaskFlow.Application.DTOs.Auth;

public record AuthResponse(
    Guid UserId,
    string Name,
    string Email,
    string Token
);