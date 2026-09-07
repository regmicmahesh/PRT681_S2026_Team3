# Week 2 study notes

Shijian Zhu | s394861 | Report Writer

These notes connect the listed learning topics to the park-visits exercise. They are prepared study material, not a record of completed videos or course certificates.

## Power BI Essential Training — Data Modeling

The useful unit of analysis is one park in one calendar year. The wide source layout is easy to read but awkward to chart across years. Unpivoting creates a Visits fact table with 110 rows. A Year dimension contains five years, and a Park dimension contains the park and its region. Keeping these dimensions separate gives slicers a clear filtering route to the counts.

There is no daily date column. Adding artificial dates would suggest detail the source does not provide. A small integer Year dimension is enough for this annual comparison.

## Power BI — DAX for Reporting

`Total Visits` sums the fact counts in the current filter context. `Selected Year Visits` uses the largest selected year. `Previous Year Visits` replaces the year filter with that year minus one while retaining the park and region filters. `Visit Change` gives the count difference; `YoY Change` divides the difference by the prior-year count.

For 2025 and all regions, this gives 170,100 additional estimated visits, or about 5.3%. `DIVIDE` returns blank if the denominator is unavailable. The 2021 result must therefore not pretend to know a 2020 growth rate.

Reference: [Microsoft — CALCULATE](https://learn.microsoft.com/en-us/dax/calculate-function-dax).

## Learning Tableau — comparison exercise

The proposed second-tool exercise uses the same long CSV: place Year on Columns and SUM(Visits) on Rows, then add Region as a filter. Add a separate park comparison sheet and combine them in a dashboard. Verify its totals against the Excel control values before comparing appearance.

| Question | Power BI approach here | Tableau comparison plan |
|---|---|---|
| Annual trend | Year dimension plus Total Visits measure | Year and SUM(Visits) in a worksheet |
| Region selection | Park[Region] slicer | Region filter shared by dashboard sheets |
| Previous year | Explicit DAX year-filter replacement | Build a prior-year calculation and check its filter behaviour |
| Main check | Same totals after selection | Same totals on the same selection |

No Tableau workbook has been created or tested in this package.

## Basics of Microsoft Fabric

The task's wording “AZURE FARBIC” is provisionally interpreted as Microsoft Fabric. This should be checked with the lecturer if they intended another product. Fabric includes capabilities for data ingestion, storage, analysis and Power BI reporting. A possible extension would store the annual source in OneLake and refresh a shared report when a new annual table is published. That is unnecessary for a five-year local exercise, and no Fabric workspace was provisioned.

Reference: [Microsoft — What is Microsoft Fabric?](https://learn.microsoft.com/en-us/fabric/fundamentals/microsoft-fabric-overview).

## Excel — Macros and VBA

The repeatable task is moving five year columns into one Year column without changing the numbers. `PrepareParkVisits` reads SourceWide, validates the inputs, fills an array and writes MacroOutput in one operation. A second run replaces the result instead of adding duplicates. The input remains available for checking. Actual execution in Excel remains a separate acceptance step.
