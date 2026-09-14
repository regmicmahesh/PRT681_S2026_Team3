namespace TaskConsole.Models;

/// <summary>
/// One row in the Tasks table. Same shape is reused in Week 7 with Entity Framework.
/// </summary>
public class TaskItem
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public bool IsCompleted { get; set; }
    public DateTime CreatedAt { get; set; }

    public override string ToString()
    {
        var status = IsCompleted ? "done" : "open";
        var notes = string.IsNullOrWhiteSpace(Description) ? "-" : Description;
        return $"#{Id,-4} [{status,-4}] {Title}  |  {notes}  |  {CreatedAt:yyyy-MM-dd HH:mm}";
    }
}
