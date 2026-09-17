using System.ComponentModel.DataAnnotations;
using TaskFlow.Domain.Enums;

namespace TaskFlow.Application.DTOs.Tasks;

public record CreateTaskRequest(
    [Required]
    [StringLength(200, MinimumLength = 2)]
    string Title,

    [Required]
    [StringLength(2000, MinimumLength = 2)]
    string Description,

    TaskPriority Priority,

    DateTime? DueDate
);