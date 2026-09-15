using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskFlow.Domain.Entities;

namespace TaskFlow.Infrastructure.Data.Configurations;

public class ProjectConfiguration : IEntityTypeConfiguration<Project>
{
  public void Configure(EntityTypeBuilder<Project> builder)
  {
    builder.ToTable("projects");

    builder.HasKey(project => project.Id);

    builder.Property(project => project.Name)
        .IsRequired()
        .HasMaxLength(150);

    builder.Property(project => project.Description)
        .IsRequired()
        .HasMaxLength(1000);

    builder.Property(project => project.CreatedAt)
        .IsRequired();

    builder.HasOne(project => project.Owner)
        .WithMany(user => user.Projects)
        .HasForeignKey(project => project.OwnerId)
        .OnDelete(DeleteBehavior.Cascade);

    builder.HasMany(project => project.Tasks)
        .WithOne(task => task.Project)
        .HasForeignKey(task => task.ProjectId)
        .OnDelete(DeleteBehavior.Cascade);
  }
}

