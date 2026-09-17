using System.ComponentModel.DataAnnotations;
using TaskFlow.Domain.Enums;
using DomainTaskStatus = TaskFlow.Domain.Enums.TaskStatus;

namespace TaskFlow.Application.DTOs.Tasks;

public record UpdateTaskRequest(
    [Required]
    [StringLength(200, MinimumLength = 2)]
    string Title,

    [Required]
    [StringLength(2000, MinimumLength = 2)]
    string Description,

    TaskPriority Priority,

    DomainTaskStatus Status,

    DateTime? DueDate
);