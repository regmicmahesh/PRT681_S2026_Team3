using Microsoft.Data.Sqlite;
using TaskConsole.Models;

namespace TaskConsole.Data;

/// <summary>
/// Raw SQL against a local SQLite file. Week 6 stays close to the SQL course:
/// open a connection, run parameterized statements, map rows by hand.
/// Week 7 replaces this class with Entity Framework Core.
/// </summary>
public sealed class TaskRepository : IDisposable
{
    private readonly SqliteConnection _connection;

    public TaskRepository(string databasePath)
    {
        var directory = Path.GetDirectoryName(databasePath);
        if (!string.IsNullOrEmpty(directory))
        {
            Directory.CreateDirectory(directory);
        }

        _connection = new SqliteConnection($"Data Source={databasePath}");
        _connection.Open();
        EnsureSchema();
        SeedIfEmpty();
    }

    public IReadOnlyList<TaskItem> GetAll()
    {
        using var command = _connection.CreateCommand();
        command.CommandText =
            """
            SELECT Id, Title, Description, IsCompleted, CreatedAt
            FROM Tasks
            ORDER BY IsCompleted ASC, CreatedAt DESC;
            """;

        using var reader = command.ExecuteReader();
        var tasks = new List<TaskItem>();
        while (reader.Read())
        {
            tasks.Add(ReadTask(reader));
        }

        return tasks;
    }

    public TaskItem? GetById(int id)
    {
        using var command = _connection.CreateCommand();
        command.CommandText =
            """
            SELECT Id, Title, Description, IsCompleted, CreatedAt
            FROM Tasks
            WHERE Id = $id;
            """;
        command.Parameters.AddWithValue("$id", id);

        using var reader = command.ExecuteReader();
        return reader.Read() ? ReadTask(reader) : null;
    }

    public TaskItem Add(string title, string? description)
    {
        using var command = _connection.CreateCommand();
        command.CommandText =
            """
            INSERT INTO Tasks (Title, Description, IsCompleted, CreatedAt)
            VALUES ($title, $description, 0, $createdAt);
            SELECT last_insert_rowid();
            """;
        command.Parameters.AddWithValue("$title", title);
        command.Parameters.AddWithValue("$description", (object?)description ?? DBNull.Value);
        command.Parameters.AddWithValue("$createdAt", DateTime.UtcNow.ToString("o"));

        var id = Convert.ToInt32(command.ExecuteScalar());
        return GetById(id)!;
    }

    public bool Update(int id, string title, string? description, bool isCompleted)
    {
        using var command = _connection.CreateCommand();
        command.CommandText =
            """
            UPDATE Tasks
            SET Title = $title,
                Description = $description,
                IsCompleted = $isCompleted
            WHERE Id = $id;
            """;
        command.Parameters.AddWithValue("$id", id);
        command.Parameters.AddWithValue("$title", title);
        command.Parameters.AddWithValue("$description", (object?)description ?? DBNull.Value);
        command.Parameters.AddWithValue("$isCompleted", isCompleted ? 1 : 0);

        return command.ExecuteNonQuery() > 0;
    }

    public bool Delete(int id)
    {
        using var command = _connection.CreateCommand();
        command.CommandText = "DELETE FROM Tasks WHERE Id = $id;";
        command.Parameters.AddWithValue("$id", id);
        return command.ExecuteNonQuery() > 0;
    }

    public void Dispose() => _connection.Dispose();

    private void EnsureSchema()
    {
        using var command = _connection.CreateCommand();
        command.CommandText =
            """
            CREATE TABLE IF NOT EXISTS Tasks (
                Id          INTEGER PRIMARY KEY AUTOINCREMENT,
                Title       TEXT    NOT NULL,
                Description TEXT,
                IsCompleted INTEGER NOT NULL DEFAULT 0,
                CreatedAt   TEXT    NOT NULL
            );
            """;
        command.ExecuteNonQuery();
    }

    private void SeedIfEmpty()
    {
        using var countCommand = _connection.CreateCommand();
        countCommand.CommandText = "SELECT COUNT(*) FROM Tasks;";
        var count = Convert.ToInt32(countCommand.ExecuteScalar());
        if (count > 0)
        {
            return;
        }

        Add("Finish C# Essential Training notes", "Summarize classes, LINQ, and exception handling.");
        Add("Practice SQL SELECT / INSERT / UPDATE / DELETE", "Use the Tasks table in this console app.");
        Add("Sketch the Week 7 Web API endpoints", "GET, POST, PUT, DELETE for /api/tasks.");
    }

    private static TaskItem ReadTask(SqliteDataReader reader)
    {
        return new TaskItem
        {
            Id = reader.GetInt32(0),
            Title = reader.GetString(1),
            Description = reader.IsDBNull(2) ? null : reader.GetString(2),
            IsCompleted = reader.GetInt32(3) == 1,
            CreatedAt = DateTime.Parse(reader.GetString(4), null, System.Globalization.DateTimeStyles.RoundtripKind)
        };
    }
}
