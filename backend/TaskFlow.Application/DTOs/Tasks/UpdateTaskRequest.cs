namespace TaskFlow.Application.DTOs.Tasks;

public record UpdateTaskRequest(
string Title,
string Description,
TaskFlow.Domain.Enums.TaskPriority Priority,
TaskFlow.Domain.Enums.TaskStatus Status,
DateTime? DueDate
);
