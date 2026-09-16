namespace TaskFlow.Application.DTOs.Tasks;

public record TaskResponse(
Guid Id,
string Title,
string Description,
TaskFlow.Domain.Enums.TaskStatus Status,
TaskFlow.Domain.Enums.TaskPriority Priority,
DateTime CreatedAt,
DateTime? DueDate,
Guid ProjectId
);
