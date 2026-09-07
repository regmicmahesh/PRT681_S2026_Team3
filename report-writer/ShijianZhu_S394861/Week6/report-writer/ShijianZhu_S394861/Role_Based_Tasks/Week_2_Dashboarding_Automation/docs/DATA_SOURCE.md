# Data source and preparation

Publisher: Parks & Wildlife NT, Northern Territory Government.  
Source: [Park visitor data](https://dth.nt.gov.au/parks-and-wildlife/parks-and-wildlife-statistics-and-research/park-visitor-data).  
Accessed: 5 September 2026.

The numeric cells were manually transcribed from the official HTML table into `data/parks_source_wide.csv`; this CSV is a project transcription, not a government-issued download. Park-name punctuation and whole-of-park capitalisation were normalised. The original numeric values were retained. `data/source_metadata.json` records provenance.

The extract contains 22 parks/reserves, four reporting regions and five calendar years, 2021–2025. The three entries in the source's Other section are excluded: George Brown Botanic Gardens, Territory Wildlife Park and Alice Springs Desert Park. This matches the scope of the source's headline totals.

## Preparation

1. Keep Region and Park as text; keep the five annual counts as non-negative integers.
2. Turn the year columns into rows. The result has 110 unique Park–Year pairs.
3. Retain the published region assignment for each park.
4. Compare every transformed value with the wide table and reconcile each annual sum.

| Calendar year | Published total | Extract total |
|---|---:|---:|
| 2021 | 3,341,400 | 3,341,400 |
| 2022 | 3,584,600 | 3,584,600 |
| 2023 | 3,453,500 | 3,453,500 |
| 2024 | 3,189,600 | 3,189,600 |
| 2025 | 3,359,700 | 3,359,700 |

## Analysis limits

The source uses estimates from different counting methods. Counts are rounded to the nearest 100 and may later be revised. Treat them as recorded visits, not a count of unique tourists. A person may visit several parks or return repeatedly. These observations do not cover all NT parks, and an annual change alone does not explain its cause.

No missing counts, months, revenue or forecasts were invented. There is no 2020 observation, so the 2021 prior-year comparison is blank/N/A. The charts and comparisons are this project's analysis, not analysis published by Parks & Wildlife NT.

## Files and reproducibility

`parks_source_wide.csv` preserves the checked input; `park_visits.csv` is the long table; `check_totals.json` contains control totals. Running `python scripts/build_sources.py` rebuilds the CSV, M query, DAX definitions and Power BI source files. It does not regenerate the Excel workbook or rewrite the embedded HTML data, so update and verify those separately if the source changes.

The workbook and Power BI project use the same dated snapshot. Power BI embeds the CSV in its M query so it can refresh without a machine-specific path. Editing the workbook does not automatically change the embedded Power BI data.
