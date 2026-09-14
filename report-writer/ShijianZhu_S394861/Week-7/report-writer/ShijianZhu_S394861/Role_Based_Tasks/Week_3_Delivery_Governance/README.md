# Week 3 — Delivery & Governance (draft)

PRT681 SOFTWARE ENGINEERING: PRACTICE  
Shijian Zhu | s394861 | Report Writer

This week continues the NT park visits report. It prepares the existing report for delivery through a Power BI workspace and documents its metrics for the next maintainer.

| Practice requirement | Deliverable | Status |
|---|---|---|
| Publish a report to a workspace | Complete PBIP project in powerbi/ and publishing steps | Prepared; actual publishing pending school-account sign-in |
| Set up scheduled refresh | External CSV Power Query source and refresh settings | Prepared; schedule not yet enabled in Power BI Service |
| One-paragraph report definition | Report_Definition.md | Written; all six model measures covered |

## What changed since Week 2

1. The Visits query now reads the existing public GitHub CSV over HTTPS instead of an embedded Base64 snapshot. Authentication is Anonymous for this public source; do not enter a GitHub password into the query.
2. The Year dimension is derived from the loaded data, so a newly added year can appear after refresh.
3. Column, row and duplicate checks stop refresh on common source errors instead of silently discarding records.
4. Report visuals, relationships and the six DAX definitions are preserved.

Open `powerbi/NTParkVisits.pbip` with its adjacent folders on Windows Power BI Desktop. Refresh before publishing. The local project has no preloaded Power BI cache. The report sources have not been executed in Desktop in this environment; use the validation steps in Publish_and_Refresh.md. If your own Desktop copy contains later changes, apply powerbi/Visits.m to its existing Visits query and update the Year query using the expression in model.bim, rather than discarding those changes.

## Files to review

1. Report_Definition.md — the requested one-paragraph document.
2. Publish_and_Refresh.md — desktop publishing, Service refresh and acceptance steps.
3. Refresh_Settings.json — a proposed UI configuration, not an automatic installer or evidence of a running schedule.
4. Course_Notes.md — brief notes on the five learning topics; no course completion claimed.
5. Evidence_Checklist.md — real evidence still needed after publishing.
6. powerbi/ — report, semantic model, M query and DAX source.

The proposed GitHub destination is `report-writer/ShijianZhu_S394861/Role_Based_Tasks/Week_3_Delivery_Governance/`, on a draft branch. Week 3 is the role-based task label; the earlier Week6 folder is retained only in the existing CSV source URL. No earlier coursework is removed or renamed.
