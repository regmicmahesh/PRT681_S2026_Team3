using System.ComponentModel.DataAnnotations;

namespace TasksApi.Models;

/// <summary>
/// Same Tasks table as Week 6. Entity Framework maps this class to SQLite.
/// </summary>
public class TaskItem
{
    public int Id { get; set; }

    [Required]
    [MaxLength(200)]
    public string Title { get; set; } = string.Empty;

    [MaxLength(1000)]
    public string? Description { get; set; }

    public bool IsCompleted { get; set; }

    public DateTime CreatedAt { get; set; }
}
