namespace TaskFlow.Application.DTOs.Tasks;

public record CreateTaskRequest(
string Title,
string Description,
TaskFlow.Domain.Enums.TaskPriority Priority,
DateTime? DueDate
);
