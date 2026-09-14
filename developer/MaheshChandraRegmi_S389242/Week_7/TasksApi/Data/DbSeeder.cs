using TasksApi.Models;

namespace TasksApi.Data;

public static class DbSeeder
{
    public static void Seed(TasksDbContext db)
    {
        if (db.Tasks.Any())
        {
            return;
        }

        db.Tasks.AddRange(
            new TaskItem
            {
                Title = "Watch ASP.NET Core: Building RESTful APIs",
                Description = "Map GET / POST / PUT / DELETE onto the Tasks controller.",
                CreatedAt = DateTime.UtcNow.AddDays(-2)
            },
            new TaskItem
            {
                Title = "Finish Entity Framework Core notes",
                Description = "DbContext, DbSet, and EnsureCreated versus migrations.",
                CreatedAt = DateTime.UtcNow.AddDays(-1)
            },
            new TaskItem
            {
                Title = "Build the React task board",
                Description = "List, add, and delete tasks by calling this API.",
                CreatedAt = DateTime.UtcNow
            });

        db.SaveChanges();
    }
}
