# Week 2 Verification

Verification date: 10 August 2026

## Build and automated tests

Command:

```powershell
dotnet test ShijianZhu.Week2.slnx --configuration Release
```

Result:

```text
Build succeeded: 0 warnings, 0 errors
Tests: 6 passed, 0 failed, 0 skipped
```

The tests cover:

- valid Movie model input;
- required fields, director length, email and Category validation;
- Category Code format validation;
- Category dropdown population;
- creation and persistence of a Movie; and
- protection against deleting a Category that is assigned to a Movie.

## SQL Express LocalDB

The `InitialCreate` migration was successfully applied to
`(localdb)\MSSQLLocalDB`. It created the `TheatreAdmin_Week2` database, the
`Movies` and `Categories` tables, the required foreign key, and sample data.

## Runtime route checks

| Route | Result |
|---|---|
| `/` | HTTP 200 |
| `/Movies` | HTTP 200 |
| `/Movies/Create` | HTTP 200 |
| `/Movies/Details/1` | HTTP 200 |
| `/Categories` | HTTP 200 |
| `/Categories/Create` | HTTP 200 |

The Movie Create page was also checked for:

- English, Japanese and Chinese Language options; and
- Action, Drama and Horror Category options.

Both checks passed.
