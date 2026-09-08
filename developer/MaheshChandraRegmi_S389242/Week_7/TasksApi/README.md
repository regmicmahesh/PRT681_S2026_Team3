# Week 7 — Tasks API

ASP.NET Core Web API with Entity Framework Core and SQLite.

This is the Week 6 console app turned into HTTP:

- `TaskItem` is now an EF entity
- `TasksDbContext` replaces the hand-written SQL repository
- `TasksController` exposes GET, POST, PUT, DELETE at `/api/tasks`

## Run

```bash
cd Week_7
dotnet run --project TasksApi --launch-profile http
```

http://localhost:5191/api/tasks

CORS allows `http://localhost:5173`. The React app also proxies `/api` to this port.

`EnsureCreated()` builds the `Tasks` table on first start. Seed data is inserted only when the table is empty.
