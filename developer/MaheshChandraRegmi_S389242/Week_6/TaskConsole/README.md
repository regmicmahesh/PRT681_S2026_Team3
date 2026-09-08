# Week 6 — Task Console

C# console app that stores a `Tasks` table in a local SQLite file and runs full CRUD from a text menu.

## What it does

| Menu | Operation | SQL |
| --- | --- | --- |
| 1 | List all tasks | `SELECT` |
| 2 | View one task | `SELECT ... WHERE Id` |
| 3 | Add a task | `INSERT` |
| 4 | Update title / notes | `UPDATE` |
| 5 | Mark done or open | `UPDATE` |
| 6 | Delete a task | `DELETE` |

The database file is created on first run next to the built executable (`bin/Debug/net10.0/tasks.db`). If the table is empty, three sample rows are inserted.

SQL is written by hand in `Data/TaskRepository.cs` with `Microsoft.Data.Sqlite` and parameterized commands (`$title`, `$id`, ...). Week 7 replaces the repository with Entity Framework Core.

## Run

```bash
cd Week_6
dotnet run --project TaskConsole
```

## Project layout

```
TaskConsole/
  Program.cs              menu loop
  Models/TaskItem.cs      one Tasks row
  Data/TaskRepository.cs  open connection, create table, CRUD
```
