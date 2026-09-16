using Moq;
using TaskFlow.Application.DTOs.Tasks;
using TaskFlow.Application.Services;
using TaskFlow.Domain.Entities;
using TaskFlow.Domain.Enums;
using TaskFlow.Domain.Interfaces;
using DomainTaskStatus = TaskFlow.Domain.Enums.TaskStatus;

namespace TaskFlow.Tests.Unit.Services;

public class TaskServiceTests
{
  private readonly Mock<ITaskRepository> _taskRepository;
  private readonly Mock<IProjectRepository> _projectRepository;
  private readonly TaskService _service;

  private readonly Guid _ownerId;
  private readonly Guid _projectId;

  public TaskServiceTests()
  {
    _taskRepository = new Mock<ITaskRepository>();
    _projectRepository = new Mock<IProjectRepository>();

    _service = new TaskService(
        _taskRepository.Object,
        _projectRepository.Object);

    _ownerId = Guid.NewGuid();
    _projectId = Guid.NewGuid();
  }

  [Fact]
  public async Task CreateAsync_ShouldCreateTask()
  {
    var project = new Project(
        "TaskFlow",
        "Project description",
        _ownerId);

    _projectRepository
        .Setup(repository =>
            repository.GetByIdAsync(_projectId, _ownerId))
        .ReturnsAsync(project);

    _taskRepository
        .Setup(repository =>
            repository.AddAsync(It.IsAny<TaskItem>()))
        .ReturnsAsync((TaskItem task) => task);

    var request = new CreateTaskRequest(
        "Implement authentication",
        "Create JWT authentication",
        TaskPriority.High,
        DateTime.UtcNow.AddDays(7));

    var result = await _service.CreateAsync(
        _ownerId,
        _projectId,
        request);

    Assert.NotEqual(Guid.Empty, result.Id);
    Assert.Equal(request.Title, result.Title);
    Assert.Equal(request.Description, result.Description);
    Assert.Equal(request.Priority, result.Priority);
    Assert.Equal(request.DueDate, result.DueDate);
    Assert.Equal(DomainTaskStatus.Pending, result.Status);
    Assert.Equal(_projectId, result.ProjectId);

    _taskRepository.Verify(
        repository => repository.AddAsync(
            It.Is<TaskItem>(task =>
                task.Title == request.Title &&
                task.ProjectId == _projectId)),
        Times.Once);

    _taskRepository.Verify(
        repository => repository.SaveChangesAsync(),
        Times.Once);
  }

  [Fact]
  public async Task CreateAsync_ShouldThrow_WhenProjectDoesNotExist()
  {
    _projectRepository
        .Setup(repository =>
            repository.GetByIdAsync(_projectId, _ownerId))
        .ReturnsAsync((Project?)null);

    var request = new CreateTaskRequest(
        "Task",
        "Description",
        TaskPriority.Medium,
        null);

    var exception = await Assert.ThrowsAsync<KeyNotFoundException>(() =>
        _service.CreateAsync(
            _ownerId,
            _projectId,
            request));

    Assert.Equal("Project not found.", exception.Message);

    _taskRepository.Verify(
        repository => repository.AddAsync(
            It.IsAny<TaskItem>()),
        Times.Never);
  }

  [Fact]
  public async Task GetAllAsync_ShouldReturnProjectTasks()
  {
    var project = new Project(
        "TaskFlow",
        "Project description",
        _ownerId);

    var task1 = new TaskItem(
        "Task 1",
        "Description 1",
        _projectId,
        TaskPriority.High);

    var task2 = new TaskItem(
        "Task 2",
        "Description 2",
        _projectId,
        TaskPriority.Low);

    _projectRepository
        .Setup(repository =>
            repository.GetByIdAsync(_projectId, _ownerId))
        .ReturnsAsync(project);

    _taskRepository
        .Setup(repository =>
            repository.GetAllByProjectAsync(_projectId))
        .ReturnsAsync(new List<TaskItem>
        {
            task1,
            task2
        });

    var result = await _service.GetAllAsync(
        _ownerId,
        _projectId);

    var tasks = result.ToList();

    Assert.Equal(2, tasks.Count);
    Assert.Equal("Task 1", tasks[0].Title);
    Assert.Equal("Task 2", tasks[1].Title);

    _taskRepository.Verify(
        repository => repository.GetAllByProjectAsync(_projectId),
        Times.Once);
  }

  [Fact]
  public async Task GetAllAsync_ShouldThrow_WhenProjectDoesNotExist()
  {
    _projectRepository
        .Setup(repository =>
            repository.GetByIdAsync(_projectId, _ownerId))
        .ReturnsAsync((Project?)null);

    var exception = await Assert.ThrowsAsync<KeyNotFoundException>(() =>
        _service.GetAllAsync(
            _ownerId,
            _projectId));

    Assert.Equal("Project not found.", exception.Message);

    _taskRepository.Verify(
        repository => repository.GetAllByProjectAsync(_projectId),
        Times.Never);
  }

  [Fact]
  public async Task GetByIdAsync_ShouldReturnTask()
  {
    var project = new Project(
        "TaskFlow",
        "Project description",
        _ownerId);

    var taskId = Guid.NewGuid();

    var task = new TaskItem(
        "Implement JWT",
        "Create authentication flow",
        _projectId,
        TaskPriority.Critical);

    _projectRepository
        .Setup(repository =>
            repository.GetByIdAsync(_projectId, _ownerId))
        .ReturnsAsync(project);

    _taskRepository
        .Setup(repository =>
            repository.GetByIdAsync(taskId, _projectId))
        .ReturnsAsync(task);

    var result = await _service.GetByIdAsync(
        _ownerId,
        _projectId,
        taskId);

    Assert.Equal(task.Id, result.Id);
    Assert.Equal(task.Title, result.Title);
    Assert.Equal(task.ProjectId, result.ProjectId);

    _taskRepository.Verify(
        repository => repository.GetByIdAsync(
            taskId,
            _projectId),
        Times.Once);
  }

  [Fact]
  public async Task GetByIdAsync_ShouldThrow_WhenProjectDoesNotExist()
  {
    var taskId = Guid.NewGuid();

    _projectRepository
        .Setup(repository =>
            repository.GetByIdAsync(_projectId, _ownerId))
        .ReturnsAsync((Project?)null);

    var exception = await Assert.ThrowsAsync<KeyNotFoundException>(() =>
        _service.GetByIdAsync(
            _ownerId,
            _projectId,
            taskId));

    Assert.Equal("Project not found.", exception.Message);

    _taskRepository.Verify(
        repository => repository.GetByIdAsync(
            taskId,
            _projectId),
        Times.Never);
  }

  [Fact]
  public async Task GetByIdAsync_ShouldThrow_WhenTaskDoesNotExist()
  {
    var project = new Project(
        "TaskFlow",
        "Project description",
        _ownerId);

    var taskId = Guid.NewGuid();

    _projectRepository
        .Setup(repository =>
            repository.GetByIdAsync(_projectId, _ownerId))
        .ReturnsAsync(project);

    _taskRepository
        .Setup(repository =>
            repository.GetByIdAsync(taskId, _projectId))
        .ReturnsAsync((TaskItem?)null);

    var exception = await Assert.ThrowsAsync<KeyNotFoundException>(() =>
        _service.GetByIdAsync(
            _ownerId,
            _projectId,
            taskId));

    Assert.Equal("Task not found.", exception.Message);
  }

  [Fact]
  public async Task UpdateAsync_ShouldUpdateTask()
  {
    var project = new Project(
        "TaskFlow",
        "Project description",
        _ownerId);

    var taskId = Guid.NewGuid();

    var task = new TaskItem(
        "Old title",
        "Old description",
        _projectId,
        TaskPriority.Low);

    _projectRepository
        .Setup(repository =>
            repository.GetByIdAsync(_projectId, _ownerId))
        .ReturnsAsync(project);

    _taskRepository
        .Setup(repository =>
            repository.GetByIdAsync(taskId, _projectId))
        .ReturnsAsync(task);

    var request = new UpdateTaskRequest(
        "Updated title",
        "Updated description",
        TaskPriority.Critical,
        DomainTaskStatus.InProgress,
        DateTime.UtcNow.AddDays(10));

    var result = await _service.UpdateAsync(
        _ownerId,
        _projectId,
        taskId,
        request);

    Assert.Equal(request.Title, result.Title);
    Assert.Equal(request.Description, result.Description);
    Assert.Equal(request.Priority, result.Priority);
    Assert.Equal(request.Status, result.Status);
    Assert.Equal(request.DueDate, result.DueDate);

    _taskRepository.Verify(
        repository => repository.UpdateAsync(task),
        Times.Once);

    _taskRepository.Verify(
        repository => repository.SaveChangesAsync(),
        Times.Once);
  }

  [Fact]
  public async Task UpdateAsync_ShouldThrow_WhenProjectDoesNotExist()
  {
    var taskId = Guid.NewGuid();

    _projectRepository
        .Setup(repository =>
            repository.GetByIdAsync(_projectId, _ownerId))
        .ReturnsAsync((Project?)null);

    var request = new UpdateTaskRequest(
        "Updated title",
        "Updated description",
        TaskPriority.High,
        DomainTaskStatus.Completed,
        null);

    var exception = await Assert.ThrowsAsync<KeyNotFoundException>(() =>
        _service.UpdateAsync(
            _ownerId,
            _projectId,
            taskId,
            request));

    Assert.Equal("Project not found.", exception.Message);

    _taskRepository.Verify(
        repository => repository.GetByIdAsync(
            taskId,
            _projectId),
        Times.Never);
  }

  [Fact]
  public async Task UpdateAsync_ShouldThrow_WhenTaskDoesNotExist()
  {
    var project = new Project(
        "TaskFlow",
        "Project description",
        _ownerId);

    var taskId = Guid.NewGuid();

    _projectRepository
        .Setup(repository =>
            repository.GetByIdAsync(_projectId, _ownerId))
        .ReturnsAsync(project);

    _taskRepository
        .Setup(repository =>
            repository.GetByIdAsync(taskId, _projectId))
        .ReturnsAsync((TaskItem?)null);

    var request = new UpdateTaskRequest(
        "Updated title",
        "Updated description",
        TaskPriority.High,
        DomainTaskStatus.Completed,
        null);

    var exception = await Assert.ThrowsAsync<KeyNotFoundException>(() =>
        _service.UpdateAsync(
            _ownerId,
            _projectId,
            taskId,
            request));

    Assert.Equal("Task not found.", exception.Message);

    _taskRepository.Verify(
        repository => repository.UpdateAsync(
            It.IsAny<TaskItem>()),
        Times.Never);
  }

  [Fact]
  public async Task DeleteAsync_ShouldDeleteTask()
  {
    var project = new Project(
        "TaskFlow",
        "Project description",
        _ownerId);

    var taskId = Guid.NewGuid();

    var task = new TaskItem(
        "Task to delete",
        "Description",
        _projectId,
        TaskPriority.Medium);

    _projectRepository
        .Setup(repository =>
            repository.GetByIdAsync(_projectId, _ownerId))
        .ReturnsAsync(project);

    _taskRepository
        .Setup(repository =>
            repository.GetByIdAsync(taskId, _projectId))
        .ReturnsAsync(task);

    await _service.DeleteAsync(
        _ownerId,
        _projectId,
        taskId);

    _taskRepository.Verify(
        repository => repository.DeleteAsync(task),
        Times.Once);

    _taskRepository.Verify(
        repository => repository.SaveChangesAsync(),
        Times.Once);
  }

  [Fact]
  public async Task DeleteAsync_ShouldThrow_WhenProjectDoesNotExist()
  {
    var taskId = Guid.NewGuid();

    _projectRepository
        .Setup(repository =>
            repository.GetByIdAsync(_projectId, _ownerId))
        .ReturnsAsync((Project?)null);

    var exception = await Assert.ThrowsAsync<KeyNotFoundException>(() =>
        _service.DeleteAsync(
            _ownerId,
            _projectId,
            taskId));

    Assert.Equal("Project not found.", exception.Message);

    _taskRepository.Verify(
        repository => repository.GetByIdAsync(
            taskId,
            _projectId),
        Times.Never);
  }

  [Fact]
  public async Task DeleteAsync_ShouldThrow_WhenTaskDoesNotExist()
  {
    var project = new Project(
        "TaskFlow",
        "Project description",
        _ownerId);

    var taskId = Guid.NewGuid();

    _projectRepository
        .Setup(repository =>
            repository.GetByIdAsync(_projectId, _ownerId))
        .ReturnsAsync(project);

    _taskRepository
        .Setup(repository =>
            repository.GetByIdAsync(taskId, _projectId))
        .ReturnsAsync((TaskItem?)null);

    var exception = await Assert.ThrowsAsync<KeyNotFoundException>(() =>
        _service.DeleteAsync(
            _ownerId,
            _projectId,
            taskId));

    Assert.Equal("Task not found.", exception.Message);

    _taskRepository.Verify(
        repository => repository.DeleteAsync(
            It.IsAny<TaskItem>()),
        Times.Never);
  }

}
