# Anonymization & Time-Intelligence DAX — Week 4 Practice

## 1. Power Query M — Data Anonymization

Applied to the `Timesheet` table (practice copy only, not production data).

```powerquery
#"Added Anon Name" = Table.AddColumn(#"Removed Columns", "Anon Name", each if [ID] = null then null else "Staff" & Text.From([ID]), type text),
#"Removed Old Name" = Table.RemoveColumns(#"Added Anon Name", {"Name"}),
#"Renamed Anon Name" = Table.RenameColumns(#"Removed Old Name", {{"Anon Name", "Name"}}),
#"Added Country" = Table.AddColumn(#"Renamed Anon Name", "Country", each
    if [ID] = null then null else
    let
        FakeCountries = {
            "Australia", "Japan", "Germany", "Brazil", "Canada",
            "India", "South Africa", "France", "Mexico", "Indonesia",
            "Egypt", "Norway", "Vietnam", "Argentina", "Kenya",
            "Italy", "Thailand", "Spain", "Chile", "New Zealand"
        },
        Idx = Number.Mod([ID] * 7 + 3, List.Count(FakeCountries))
    in
        FakeCountries{Idx}, type text),
```

- `Name` is replaced with a deterministic code (`Staff` + ID) so the same employee always maps to the same code across refreshes.
- `Country` is assigned pseudo-randomly but deterministically from a fixed list of 20 real country names, using `Number.Mod` on the employee ID as a seed — same employee always gets the same country.

## 2. DAX — Time-Intelligence Measures

```dax
Present This Week =
VAR MaxDate = MAX(Timesheet[Date])
VAR WeekStart = MaxDate - 6
RETURN
CALCULATE(
    DISTINCTCOUNT(Timesheet[ID]),
    Timesheet[Attendance Category] = "Present",
    Timesheet[Date] >= WeekStart,
    Timesheet[Date] <= MaxDate
)

Present Last Week =
VAR MaxDate = MAX(Timesheet[Date])
VAR LastWeekEnd = MaxDate - 7
VAR LastWeekStart = MaxDate - 13
RETURN
CALCULATE(
    DISTINCTCOUNT(Timesheet[ID]),
    Timesheet[Attendance Category] = "Present",
    Timesheet[Date] >= LastWeekStart,
    Timesheet[Date] <= LastWeekEnd
)

WoW Change % =
VAR ThisWeek = [Present This Week]
VAR LastWeek = [Present Last Week]
RETURN
DIVIDE(ThisWeek - LastWeek, LastWeek)
```

These measures compute a trailing 7-day window relative to the latest date in the dataset, compared against the prior 7-day window. When placed as cards on the same report page as the `Map` visual, selecting a country cross-filters these measures to show that country's week-over-week attendance trend.
