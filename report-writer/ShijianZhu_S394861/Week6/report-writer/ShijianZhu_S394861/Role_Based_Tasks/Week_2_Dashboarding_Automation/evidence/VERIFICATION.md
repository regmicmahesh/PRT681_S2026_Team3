# Verification record

Package: NT park visits, Week 2 Report Writer  
Checked: 5 September 2026

## Checks completed in the preparation environment

| Check | Result |
|---|---|
| Official table transcription | 22 parks/reserves and five years recorded; every annual total matches the official headline |
| Wide-to-long data | All 110 park-year values match the checked wide table; no duplicates or missing counts |
| Snapshot consistency | CSV embedded in Power Query matches the delivered long CSV byte for byte; HTML data matches the same 110 records |
| Model structure | Three tables, two many-to-one relationships and six measures; all report field bindings refer to defined fields |
| Report source layout | Eight visual definitions fall within the page boundaries; year-to-trend NoFilter interaction is present |
| Excel formulas | Cached 2025/All KPI values are 3,359,700, 3,189,600 and 0.0533295711; no error cells in the five exported sheets |
| Excel layout | All five sheets rendered and visually inspected; report PNG included; native line chart present in workbook |
| HTML logic | DOM-stub checks pass for default KPIs, Darwin selection, 2021 missing baseline, five-year trend and the labelled macro simulation |

Reproduce the automated checks with `python scripts/verify.py` and `node scripts/verify_preview.mjs`. Python and Node are needed; these scripts use only their standard libraries. The source totals were checked against the government page when preparing the transcription; the local scripts compare against the retained snapshot rather than fetching future revisions.

## Checks still requiring the actual applications

1. Open and refresh the PBIP in Power BI Desktop; check the report renders and the DAX measures return the expected results. Static checks do not prove Desktop compatibility or DAX execution.
2. Import and run the VBA in a macro-enabled Excel copy. Confirm 110 output rows, repeat-run behaviour and rejection of invalid counts. Reading the macro and checking the expected transformation do not prove VBA execution.
3. Open the HTML in a browser to confirm visual rendering. The DOM-stub check evaluates its logic, not browser layout.
4. Add actual Power BI and macro screenshots after those steps. The existing PNG shows the Excel report rendered in the preparation environment; it is not a screenshot of Power BI or Microsoft Excel.

## Submission status

The package is organised for the personal report-writer folder in Team 3. GitHub write access through the connected integration returned `403 Resource not accessible by integration`. No successful upload is claimed. Do not replace or remove earlier general weekly tasks when adding this separate role-based exercise.
