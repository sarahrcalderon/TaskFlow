using System.ComponentModel.DataAnnotations;

namespace TaskFlow.Application.DTOs.Projects;

public record CreateProjectRequest(
    [Required]
    [StringLength(150, MinimumLength = 2)]
    string Name,

    [Required]
    [StringLength(1000, MinimumLength = 2)]
    string Description
);