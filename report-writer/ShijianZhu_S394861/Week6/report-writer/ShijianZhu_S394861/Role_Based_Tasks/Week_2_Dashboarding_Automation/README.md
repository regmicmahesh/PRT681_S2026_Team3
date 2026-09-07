# Week 2 — Dashboarding & Automation

**PRT681 SOFTWARE ENGINEERING: PRACTICE**  
**Shijian Zhu | s394861 | Report Writer**

This exercise uses real Northern Territory park visitation estimates to compare annual activity across parks and regions. The question is: how did visits change between 2021 and 2025, and did every region follow the same pattern?

The data is from [Parks & Wildlife NT](https://dth.nt.gov.au/parks-and-wildlife/parks-and-wildlife-statistics-and-research/park-visitor-data), accessed 5 September 2026. It covers the 22 listed parks/reserves, not every park in the NT. Counts are estimates of visits, not unique people. All comparisons in this project are student analysis.

## Open the work

1. Open [NT_Park_Visits.xlsx](excel/NT_Park_Visits.xlsx). Change the year and region on the Report sheet.
2. Open [dashboard.html](preview/dashboard.html) in a browser for the companion interactive preview.
3. On Windows, open [NTParkVisits.pbip](powerbi/NTParkVisits.pbip) in Power BI Desktop and refresh. Keep its adjacent report and model folders together. See [setup](docs/SETUP.md).
4. Import [UnpivotParkVisits.bas](automation/UnpivotParkVisits.bas) into a macro-enabled copy of the Excel workbook and run `PrepareParkVisits`.

## How it meets the practice task

| Task | Included work |
|---|---|
| Rebuild an Excel report in Power BI | Excel report plus Power BI report/model definitions, with year and region slicers, annual trend, region comparison and park table |
| At least three DAX measures | Six measures: Total Visits, Selected Year, Selected Year Visits, Previous Year Visits, YoY Change, Visit Change |
| Automate a repetitive Excel task | VBA converts the 22-row source table with five year columns into 110 park-year records |
| Study the listed tools | [Course notes](Course_Notes.md), including a Tableau comparison and an introduction to Microsoft Fabric |

The example YTD sales measure in the task is not suitable for annual park data. This project uses annual totals and year-on-year comparisons without inventing monthly observations.

## Baseline check

With 2025 and all regions selected, the report should show **3,359,700 visits**, **3,189,600 in the previous year**, and **+5.3%**. The five annual totals match the government table.

## Status

The data checks and Excel formula checks have passed. All five Excel sheets were rendered and inspected. The HTML calculation and filter logic have been checked with a JavaScript DOM stub.

Power BI Desktop and Excel VBA execution are still pending on Windows. The package contains PBIP source files and a BAS module; it does not contain a tested PBIX or an XLSM with an embedded macro. The HTML preview is a companion, not evidence that the Power BI report has run.

The intended repository path is `report-writer/ShijianZhu_S394861/Role_Based_Tasks/Week_2_Dashboarding_Automation/`. The GitHub integration rejected the write request with HTTP 403, so this package has not been uploaded by the assistant.

## Supporting files

1. [Practice report](Practice_Report.md)
2. [Data source and preparation](docs/DATA_SOURCE.md)
3. [Task interpretation](docs/TASK_REQUIREMENTS.md)
4. [Activity details](Activity_Details.md)
5. [Demonstration outline](Workshop_Walkthrough.md)
6. [Verification record](evidence/VERIFICATION.md)
7. [Excel report preview](evidence/excel_report.png)

To check the package locally, run `python scripts/verify.py` and `node scripts/verify_preview.mjs` from this folder. The checks do not require Office or Power BI.
