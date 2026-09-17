using System.ComponentModel.DataAnnotations;

namespace TaskFlow.Application.DTOs.Auth;

public record LoginRequest(
    [Required]
    [EmailAddress]
    [StringLength(255)]
    string Email,

    [Required]
    [StringLength(100, MinimumLength = 6)]
    string Password
);