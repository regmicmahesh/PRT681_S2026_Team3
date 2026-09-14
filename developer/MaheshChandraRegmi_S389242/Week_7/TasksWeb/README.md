# Week 7 — React frontend

Minimal React client for the Tasks API.

- Lists tasks from `GET /api/tasks`
- Adds a task with `POST /api/tasks`
- Deletes a task with `DELETE /api/tasks/{id}`
- Marks a task done with `PUT /api/tasks/{id}`

Vite proxies `/api` to `http://localhost:5191`, so start the API first.

```bash
cd Week_7
dotnet run --project TasksApi --launch-profile http

# new terminal
cd Week_7/TasksWeb
npm install
npm run dev
```

Open http://localhost:5173.
