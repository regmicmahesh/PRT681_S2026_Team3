using Microsoft.EntityFrameworkCore;
using TasksApi.Models;

namespace TasksApi.Data;

public class TasksDbContext : DbContext
{
    public TasksDbContext(DbContextOptions<TasksDbContext> options)
        : base(options)
    {
    }

    public DbSet<TaskItem> Tasks => Set<TaskItem>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TaskItem>(entity =>
        {
            entity.ToTable("Tasks");
            entity.HasKey(task => task.Id);
            entity.Property(task => task.Title).IsRequired().HasMaxLength(200);
            entity.Property(task => task.Description).HasMaxLength(1000);
            entity.Property(task => task.IsCompleted).HasDefaultValue(false);
            entity.Property(task => task.CreatedAt).IsRequired();
        });
    }
}
