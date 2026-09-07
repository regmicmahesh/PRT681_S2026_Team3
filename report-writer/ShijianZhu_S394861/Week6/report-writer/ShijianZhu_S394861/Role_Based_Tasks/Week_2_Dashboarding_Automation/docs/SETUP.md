# Run the exercise

## Excel report

1. Open `excel/NT_Park_Visits.xlsx` in Excel.
2. On Report, select 2025 in B6 and All in B7. Expected values: 3,359,700; 3,189,600; +5.3%.
3. Select Darwin. Expected current/prior totals: 2,103,500 and 1,990,400.
4. Select 2021. The previous-year and YoY cards should show N/A because 2020 is not in the extract.
5. Review SourceWide, Visits, ParkComparison and Sources. The annual trend keeps all five years and follows the region choice.

## Power BI Desktop

Use the regular Windows Power BI Desktop application. Extract the project to a short local path such as `C:\NTWeek2` to avoid excessive nested path lengths. If needed, enable the Power BI Project save option under File > Options and settings > Options > Preview features and restart.

1. Open `powerbi/NTParkVisits.pbip`. Keep `NTParkVisits.Report` and `NTParkVisits.SemanticModel` beside it.
2. Select Refresh to load the embedded data. The project has no cached Power BI data and requires this first refresh.
3. Inspect Model view: Visits[Year] has a many-to-one relationship with Year[Year]; Visits[Park] has one with Park[Park]. Both filter from dimension to fact.
4. Inspect the six measures in Visits and compare them with `Measures.dax`.
5. Select one year and optionally one region. With the year slicer cleared, comparison cards use the latest year (2025). With multiple years selected they use the largest selected year. This is deliberate; they do not sum several years.
6. The year slicer does not filter the annual trend. The region slicer should filter it. Check this under Edit interactions if your Desktop version imports the interaction differently.
7. Check 2025/All, 2025/Darwin and 2021/All using the expected values above. Also test selecting a park in the table.
8. After checking the visuals and measures, save the project. Use File > Save as to produce a PBIX if needed, and capture actual Power BI screenshots in evidence.

These source files were generated outside Desktop and have not yet been opened there. If the application rejects the report metadata, use the supplied CSV/M and DAX files to recreate the same report: three cards, two slicers, an annual line chart, a regional bar chart and a park comparison table. This is a recovery route, not a claim that Desktop validation has passed.

Reference: [Microsoft — Power BI Desktop projects](https://learn.microsoft.com/en-us/power-bi/developer/projects/projects-overview).

## VBA automation

1. Save a copy of `NT_Park_Visits.xlsx` as `NT_Park_Visits_Automation.xlsm`.
2. Open the VBA editor with Alt+F11. Use File > Import File and choose `automation/UnpivotParkVisits.bas`.
3. Return to Excel, open Alt+F8, choose `PrepareParkVisits` and run it.
4. Check that MacroOutput has four headers and 110 data rows. SourceWide should be unchanged. The macro replaces only its MacroOutput sheet, so keep other work on another sheet.
5. Compare MacroOutput to Visits. Each park should have five records. Annual totals should match the table in DATA_SOURCE.md.
6. Run it again: there should still be 110 rows. To test validation, use a spare workbook copy, replace one count with text and run the macro. It should stop with a message before replacing the previous output.
7. Save the XLSM and capture evidence after a successful run. Do not label the macro as executed until this check has actually been done.

The VBA performs the repetitive wide-to-long conversion. The browser button only explains/simulates that conversion; it does not execute VBA.
