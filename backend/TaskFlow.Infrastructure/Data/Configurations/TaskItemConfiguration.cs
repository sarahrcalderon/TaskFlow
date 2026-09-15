using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskFlow.Domain.Entities;

namespace TaskFlow.Infrastructure.Data.Configurations;

public class TaskItemConfiguration : IEntityTypeConfiguration<TaskItem>
{
  public void Configure(EntityTypeBuilder<TaskItem> builder)
  {
    builder.ToTable("tasks");

    builder.HasKey(task => task.Id);

    builder.Property(task => task.Title)
        .IsRequired()
        .HasMaxLength(200);

    builder.Property(task => task.Description)
        .IsRequired()
        .HasMaxLength(2000);

    builder.Property(task => task.Status)
        .IsRequired()
        .HasConversion<string>()
        .HasMaxLength(30);

    builder.Property(task => task.Priority)
        .IsRequired()
        .HasConversion<string>()
        .HasMaxLength(30);

    builder.Property(task => task.CreatedAt)
        .IsRequired();

    builder.Property(task => task.DueDate);

    builder.HasOne(task => task.Project)
        .WithMany(project => project.Tasks)
        .HasForeignKey(task => task.ProjectId)
        .OnDelete(DeleteBehavior.Cascade);
  }
}

