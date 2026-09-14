# Week 6 — C# console + SQLite CRUD

Went back to a small Tasks table and did the CRUD myself, no EF this week. Just C#, a menu loop, and parameterized SQL against a local SQLite file.

## What I built

`TaskConsole`

- list all tasks
- view one
- add
- update title / notes
- mark done or open
- delete

The `Tasks` table is created on first run. If it is empty, three sample rows go in.

## Things I sat with

**Raw SQL on purpose.** Week 1/3 I already used EF. This week I wanted the SQL course stuff in front of me: `CREATE TABLE`, `SELECT`, `INSERT`, `UPDATE`, `DELETE`, all with `$id` / `$title` parameters so I don't concatenate strings.

**Same model as next week.** `TaskItem` is just Id / Title / Description / IsCompleted / CreatedAt. Week 7 keeps that shape and swaps the repository for a `DbContext`.

## How to run

```bash
cd Week_6
dotnet run --project TaskConsole
```
