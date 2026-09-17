using System.ComponentModel.DataAnnotations;

namespace TaskFlow.Application.DTOs.Auth;

public record RegisterRequest(
    [Required]
    [StringLength(100, MinimumLength = 2)]
    string Name,

    [Required]
    [EmailAddress]
    [StringLength(255)]
    string Email,

    [Required]
    [StringLength(100, MinimumLength = 6)]
    string Password
);