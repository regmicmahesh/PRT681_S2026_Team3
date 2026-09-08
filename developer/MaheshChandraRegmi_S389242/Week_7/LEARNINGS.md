# Week 7 — Web API + React on the Tasks table

Week 6 was a console menu talking to SQLite. This week I put the same `Tasks` rows behind HTTP and built a small React page that lists, adds, and deletes them.

## What I built

`TasksApi` — ASP.NET Core + EF Core + SQLite

- `GET /api/tasks`
- `GET /api/tasks/{id}`
- `POST /api/tasks`
- `PUT /api/tasks/{id}`
- `DELETE /api/tasks/{id}`

`TasksWeb` — Vite + React

- list tickets
- file a new one
- delete
- mark done (uses PUT)

## Things I sat with

**EF instead of SQL strings.** `TasksDbContext` + `DbSet<TaskItem>` replace `TaskRepository`. `EnsureCreated()` builds the table so I don't need a migration just to demo it.

**CORS + the Vite proxy.** The API allows `http://localhost:5173`. The React app also proxies `/api` to `http://localhost:5191`, so `fetch('/api/tasks')` just works in dev.

**DTOs on write.** Create/update take `CreateTaskRequest` / `UpdateTaskRequest` with `[Required]` / `[MaxLength]`. `[ApiController]` turns bad bodies into 400.

## How to run

Terminal 1:

```bash
cd Week_7
dotnet run --project TasksApi --launch-profile http
```

Terminal 2:

```bash
cd Week_7/TasksWeb
npm install
npm run dev
```

Open http://localhost:5173. API is at http://localhost:5191/api/tasks.
