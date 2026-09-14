using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TasksApi.Data;
using TasksApi.Models;

namespace TasksApi.Controllers;

[ApiController]
[Route("api/tasks")]
public class TasksController : ControllerBase
{
    private readonly TasksDbContext _db;

    public TasksController(TasksDbContext db)
    {
        _db = db;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TaskItem>>> GetAll(CancellationToken cancellationToken)
    {
        var tasks = await _db.Tasks
            .AsNoTracking()
            .OrderBy(task => task.IsCompleted)
            .ThenByDescending(task => task.CreatedAt)
            .ToListAsync(cancellationToken);

        return Ok(tasks);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<TaskItem>> GetById(int id, CancellationToken cancellationToken)
    {
        var task = await _db.Tasks.AsNoTracking().FirstOrDefaultAsync(item => item.Id == id, cancellationToken);
        return task is null ? NotFound() : Ok(task);
    }

    [HttpPost]
    public async Task<ActionResult<TaskItem>> Create(CreateTaskRequest request, CancellationToken cancellationToken)
    {
        var task = new TaskItem
        {
            Title = request.Title.Trim(),
            Description = string.IsNullOrWhiteSpace(request.Description) ? null : request.Description.Trim(),
            IsCompleted = false,
            CreatedAt = DateTime.UtcNow
        };

        _db.Tasks.Add(task);
        await _db.SaveChangesAsync(cancellationToken);

        return CreatedAtAction(nameof(GetById), new { id = task.Id }, task);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<TaskItem>> Update(int id, UpdateTaskRequest request, CancellationToken cancellationToken)
    {
        var task = await _db.Tasks.FirstOrDefaultAsync(item => item.Id == id, cancellationToken);
        if (task is null)
        {
            return NotFound();
        }

        task.Title = request.Title.Trim();
        task.Description = string.IsNullOrWhiteSpace(request.Description) ? null : request.Description.Trim();
        task.IsCompleted = request.IsCompleted;

        await _db.SaveChangesAsync(cancellationToken);
        return Ok(task);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var task = await _db.Tasks.FirstOrDefaultAsync(item => item.Id == id, cancellationToken);
        if (task is null)
        {
            return NotFound();
        }

        _db.Tasks.Remove(task);
        await _db.SaveChangesAsync(cancellationToken);
        return NoContent();
    }
}
