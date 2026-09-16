using Moq;
using TaskFlow.Application.DTOs.Projects;
using TaskFlow.Application.Services;
using TaskFlow.Domain.Entities;
using TaskFlow.Domain.Interfaces;

namespace TaskFlow.Tests.Unit.Services;

public class ProjectServiceTests
{
  private readonly Mock<IProjectRepository> _repositoryMock;
  private readonly ProjectService _service;

  public ProjectServiceTests()
  {
    _repositoryMock = new Mock<IProjectRepository>();
    _service = new ProjectService(_repositoryMock.Object);
  }

  [Fact]
  public async Task CreateAsync_ShouldCreateProject()
  {
    var ownerId = Guid.NewGuid();

    var request = new CreateProjectRequest(
        "TaskFlow",
        "Project management application");

    _repositoryMock
        .Setup(repository => repository.AddAsync(It.IsAny<Project>()))
        .ReturnsAsync((Project project) => project);

    var result = await _service.CreateAsync(
        ownerId,
        request);

    Assert.NotEqual(Guid.Empty, result.Id);
    Assert.Equal(request.Name, result.Name);
    Assert.Equal(request.Description, result.Description);
    Assert.Equal(ownerId, result.OwnerId);

    _repositoryMock.Verify(
        repository => repository.AddAsync(It.IsAny<Project>()),
        Times.Once);

    _repositoryMock.Verify(
        repository => repository.SaveChangesAsync(),
        Times.Once);
  }

  [Fact]
  public async Task GetAllAsync_ShouldReturnOnlyOwnerProjects()
  {
    var ownerId = Guid.NewGuid();

    var projects = new List<Project>
        {
            new Project(
                "Project 1",
                "Description 1",
                ownerId),

            new Project(
                "Project 2",
                "Description 2",
                ownerId)
        };

    _repositoryMock
        .Setup(repository => repository.GetAllByOwnerAsync(ownerId))
        .ReturnsAsync(projects);

    var result = await _service.GetAllAsync(ownerId);

    var resultList = result.ToList();

    Assert.Equal(2, resultList.Count);
    Assert.All(
        resultList,
        project => Assert.Equal(ownerId, project.OwnerId));
  }

  [Fact]
  public async Task GetByIdAsync_ShouldReturnProject()
  {
    var ownerId = Guid.NewGuid();

    var project = new Project(
        "TaskFlow",
        "Description",
        ownerId);

    _repositoryMock
        .Setup(repository => repository.GetByIdAsync(
            project.Id,
            ownerId))
        .ReturnsAsync(project);

    var result = await _service.GetByIdAsync(
        ownerId,
        project.Id);

    Assert.Equal(project.Id, result.Id);
    Assert.Equal(project.Name, result.Name);
    Assert.Equal(project.Description, result.Description);
    Assert.Equal(ownerId, result.OwnerId);
  }

  [Fact]
  public async Task GetByIdAsync_ShouldThrowWhenProjectDoesNotExist()
  {
    var ownerId = Guid.NewGuid();
    var projectId = Guid.NewGuid();

    _repositoryMock
        .Setup(repository => repository.GetByIdAsync(
            projectId,
            ownerId))
        .ReturnsAsync((Project?)null);

    await Assert.ThrowsAsync<KeyNotFoundException>(
        () => _service.GetByIdAsync(
            ownerId,
            projectId));
  }

  [Fact]
  public async Task UpdateAsync_ShouldUpdateProject()
  {
    var ownerId = Guid.NewGuid();

    var project = new Project(
        "Old Name",
        "Old Description",
        ownerId);

    _repositoryMock
        .Setup(repository => repository.GetByIdAsync(
            project.Id,
            ownerId))
        .ReturnsAsync(project);

    var request = new UpdateProjectRequest(
        "New Name",
        "New Description");

    var result = await _service.UpdateAsync(
        ownerId,
        project.Id,
        request);

    Assert.Equal("New Name", result.Name);
    Assert.Equal("New Description", result.Description);
    Assert.Equal(ownerId, result.OwnerId);

    _repositoryMock.Verify(
        repository => repository.UpdateAsync(project),
        Times.Once);

    _repositoryMock.Verify(
        repository => repository.SaveChangesAsync(),
        Times.Once);
  }

  [Fact]
  public async Task UpdateAsync_ShouldThrowWhenProjectDoesNotExist()
  {
    var ownerId = Guid.NewGuid();
    var projectId = Guid.NewGuid();

    _repositoryMock
        .Setup(repository => repository.GetByIdAsync(
            projectId,
            ownerId))
        .ReturnsAsync((Project?)null);

    var request = new UpdateProjectRequest(
        "New Name",
        "New Description");

    await Assert.ThrowsAsync<KeyNotFoundException>(
        () => _service.UpdateAsync(
            ownerId,
            projectId,
            request));
  }

  [Fact]
  public async Task DeleteAsync_ShouldDeleteProject()
  {
    var ownerId = Guid.NewGuid();

    var project = new Project(
        "TaskFlow",
        "Description",
        ownerId);

    _repositoryMock
        .Setup(repository => repository.GetByIdAsync(
            project.Id,
            ownerId))
        .ReturnsAsync(project);

    await _service.DeleteAsync(
        ownerId,
        project.Id);

    _repositoryMock.Verify(
        repository => repository.DeleteAsync(project),
        Times.Once);

    _repositoryMock.Verify(
        repository => repository.SaveChangesAsync(),
        Times.Once);
  }

  [Fact]
  public async Task DeleteAsync_ShouldThrowWhenProjectDoesNotExist()
  {
    var ownerId = Guid.NewGuid();
    var projectId = Guid.NewGuid();

    _repositoryMock
        .Setup(repository => repository.GetByIdAsync(
            projectId,
            ownerId))
        .ReturnsAsync((Project?)null);

    await Assert.ThrowsAsync<KeyNotFoundException>(
        () => _service.DeleteAsync(
            ownerId,
            projectId));
  }
}