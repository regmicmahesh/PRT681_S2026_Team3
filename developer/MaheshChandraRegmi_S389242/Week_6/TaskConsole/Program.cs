using TaskConsole.Data;

// Week 6 practice: C# console app + SQLite CRUD on a Tasks table.
// Week 7 turns this same model into an ASP.NET Core Web API + React UI.

var dbPath = Path.Combine(AppContext.BaseDirectory, "tasks.db");
using var repository = new TaskRepository(dbPath);

Console.WriteLine("Task Console  |  Week 6  |  SQLite CRUD");
Console.WriteLine($"Database: {dbPath}");
Console.WriteLine();

var running = true;
while (running)
{
    PrintMenu();
    Console.Write("Choose an option: ");
    var choice = Console.ReadLine()?.Trim();
    Console.WriteLine();

    try
    {
        switch (choice)
        {
            case "1":
                ListTasks(repository);
                break;
            case "2":
                ViewTask(repository);
                break;
            case "3":
                AddTask(repository);
                break;
            case "4":
                UpdateTask(repository);
                break;
            case "5":
                ToggleTask(repository);
                break;
            case "6":
                DeleteTask(repository);
                break;
            case "0":
                running = false;
                Console.WriteLine("Goodbye.");
                break;
            default:
                Console.WriteLine("Unknown option. Pick a number from the menu.");
                break;
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Something went wrong: {ex.Message}");
    }

    if (running)
    {
        Console.WriteLine();
    }
}

static void PrintMenu()
{
    Console.WriteLine("----------------------------------------");
    Console.WriteLine("1  List all tasks");
    Console.WriteLine("2  View one task");
    Console.WriteLine("3  Add a task");
    Console.WriteLine("4  Update a task");
    Console.WriteLine("5  Mark a task done / open");
    Console.WriteLine("6  Delete a task");
    Console.WriteLine("0  Exit");
    Console.WriteLine("----------------------------------------");
}

static void ListTasks(TaskRepository repository)
{
    var tasks = repository.GetAll();
    if (tasks.Count == 0)
    {
        Console.WriteLine("No tasks yet. Use option 3 to add one.");
        return;
    }

    Console.WriteLine($"{"ID",-6}{"Status",-8}Title");
    foreach (var task in tasks)
    {
        Console.WriteLine(task);
    }

    var open = tasks.Count(t => !t.IsCompleted);
    Console.WriteLine();
    Console.WriteLine($"{tasks.Count} task(s), {open} still open.");
}

static void ViewTask(TaskRepository repository)
{
    var id = ReadInt("Task id: ");
    if (id is null)
    {
        return;
    }

    var task = repository.GetById(id.Value);
    if (task is null)
    {
        Console.WriteLine($"No task with id {id}.");
        return;
    }

    Console.WriteLine($"Id:          {task.Id}");
    Console.WriteLine($"Title:       {task.Title}");
    Console.WriteLine($"Description: {task.Description ?? "(none)"}");
    Console.WriteLine($"Status:      {(task.IsCompleted ? "done" : "open")}");
    Console.WriteLine($"Created:     {task.CreatedAt:u}");
}

static void AddTask(TaskRepository repository)
{
    var title = ReadRequired("Title: ");
    if (title is null)
    {
        return;
    }

    Console.Write("Description (optional): ");
    var description = EmptyToNull(Console.ReadLine());

    var created = repository.Add(title, description);
    Console.WriteLine($"Added task #{created.Id}.");
}

static void UpdateTask(TaskRepository repository)
{
    var id = ReadInt("Task id to update: ");
    if (id is null)
    {
        return;
    }

    var existing = repository.GetById(id.Value);
    if (existing is null)
    {
        Console.WriteLine($"No task with id {id}.");
        return;
    }

    Console.WriteLine($"Current title: {existing.Title}");
    Console.Write("New title (leave blank to keep): ");
    var titleInput = Console.ReadLine();
    var title = string.IsNullOrWhiteSpace(titleInput) ? existing.Title : titleInput.Trim();

    Console.WriteLine($"Current description: {existing.Description ?? "(none)"}");
    Console.Write("New description (leave blank to keep): ");
    var descriptionInput = Console.ReadLine();
    var description = string.IsNullOrWhiteSpace(descriptionInput)
        ? existing.Description
        : descriptionInput.Trim();

    if (repository.Update(existing.Id, title, description, existing.IsCompleted))
    {
        Console.WriteLine($"Updated task #{existing.Id}.");
    }
}

static void ToggleTask(TaskRepository repository)
{
    var id = ReadInt("Task id to toggle: ");
    if (id is null)
    {
        return;
    }

    var existing = repository.GetById(id.Value);
    if (existing is null)
    {
        Console.WriteLine($"No task with id {id}.");
        return;
    }

    var next = !existing.IsCompleted;
    repository.Update(existing.Id, existing.Title, existing.Description, next);
    Console.WriteLine($"Task #{existing.Id} is now {(next ? "done" : "open")}.");
}

static void DeleteTask(TaskRepository repository)
{
    var id = ReadInt("Task id to delete: ");
    if (id is null)
    {
        return;
    }

    if (repository.Delete(id.Value))
    {
        Console.WriteLine($"Deleted task #{id}.");
    }
    else
    {
        Console.WriteLine($"No task with id {id}.");
    }
}

static int? ReadInt(string prompt)
{
    Console.Write(prompt);
    if (int.TryParse(Console.ReadLine(), out var value) && value > 0)
    {
        return value;
    }

    Console.WriteLine("Enter a positive whole number.");
    return null;
}

static string? ReadRequired(string prompt)
{
    Console.Write(prompt);
    var value = Console.ReadLine()?.Trim();
    if (string.IsNullOrWhiteSpace(value))
    {
        Console.WriteLine("Title cannot be empty.");
        return null;
    }

    return value;
}

static string? EmptyToNull(string? value)
{
    return string.IsNullOrWhiteSpace(value) ? null : value.Trim();
}
