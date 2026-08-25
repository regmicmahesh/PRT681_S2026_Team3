# Week 2 — Theatre Administration Web App

- Student: Shijian Zhu
- Student ID: S394861
- Approach: ASP.NET Core MVC with Entity Framework Core Code First
- Database: SQL Server Express LocalDB

## Completed requirements

- [x] Created a web application for theatre administrators.
- [x] Added create, list, details, edit and delete operations for Movies.
- [x] Added create, list, details, edit and delete operations for Categories.
- [x] Added Movie fields: Id, Name, Release date, Director, Contact email,
  Language and required Category.
- [x] Added the Language enum with English, Japanese and Chinese.
- [x] Added Category fields: Id, Name and Code.
- [x] Added a required one-to-many relationship from Category to Movie.
- [x] Added a Category dropdown to the Movie Create and Edit forms.
- [x] Used ASP.NET Core scaffolding to generate both CRUD workflows.
- [x] Added Data Annotations validation to both models.
- [x] Added an EF Core migration and persisted data in SQL Express LocalDB.
- [x] Used meaningful Git commits for the major implementation stages.
- [x] Completed the 20-hour Week 2 timesheet.

## Project contents

- `TheatreAdmin/`: MVC application, models, controllers, Razor views and migration.
- `TheatreAdmin.Tests/`: automated model validation and CRUD controller tests.
- `ShijianZhu.Week2.slnx`: solution containing the application and tests.
- `Verification.md`: build, test, database and route verification results.
- `timesheet_ShijianZhu_S394861.xlsx`: 20-hour activity record.

## Run the application

From `ShijianZhu_S394861/Week_2`:

```powershell
dotnet tool restore
dotnet restore ShijianZhu.Week2.slnx
dotnet ef database update --project TheatreAdmin
dotnet run --project TheatreAdmin
```

Open the local address displayed in the terminal, then use the **Movies** and
**Categories** navigation links.

The database connection uses:

```text
Server=(localdb)\MSSQLLocalDB;Database=TheatreAdmin_Week2
```

## Scaffolding evidence

The controllers and Razor views were generated with the ASP.NET Core scaffolder:

```powershell
dotnet aspnet-codegenerator controller --controllerName CategoriesController `
  --model TheatreAdmin.Models.Category `
  --dataContext TheatreAdmin.Data.TheatreAdminContext `
  --relativeFolderPath Controllers --useDefaultLayout `
  --referenceScriptLibraries --databaseProvider sqlserver

dotnet aspnet-codegenerator controller --controllerName MoviesController `
  --model TheatreAdmin.Models.Movie `
  --dataContext TheatreAdmin.Data.TheatreAdminContext `
  --relativeFolderPath Controllers --useDefaultLayout `
  --referenceScriptLibraries --databaseProvider sqlserver
```

The generated code was then adjusted so the Language enum and Category dropdowns
display correctly and categories that are assigned to movies cannot be deleted.

## Validation summary

| Model | Validation |
|---|---|
| Movie Name | Required; 2–100 characters |
| Release date | Required date value |
| Director | Required; 2–80 characters |
| Contact email | Required valid email address |
| Language | English, Japanese or Chinese enum |
| Category | Required Category identifier |
| Category Name | Required; 2–50 characters |
| Category Code | Required; 2–10 uppercase letters, numbers or hyphens; unique |

## Build and test

```powershell
dotnet test ShijianZhu.Week2.slnx --configuration Release
```

Expected result: 6 tests passed, 0 failed, with 0 build warnings and 0 errors.
