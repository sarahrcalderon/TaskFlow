using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskFlow.Domain.Entities;

namespace TaskFlow.Infrastructure.Data.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
  public void Configure(EntityTypeBuilder<User> builder)
  {
    builder.ToTable("users");

    builder.HasKey(user => user.Id);

    builder.Property(user => user.Name)
        .IsRequired()
        .HasMaxLength(100);

    builder.Property(user => user.Email)
        .IsRequired()
        .HasMaxLength(255);

    builder.HasIndex(user => user.Email)
        .IsUnique();

    builder.Property(user => user.PasswordHash)
        .IsRequired()
        .HasMaxLength(500);

    builder.Property(user => user.CreatedAt)
        .IsRequired();

    builder.HasMany(user => user.Projects)
        .WithOne(project => project.Owner)
        .HasForeignKey(project => project.OwnerId)
        .OnDelete(DeleteBehavior.Cascade);
  }
}

