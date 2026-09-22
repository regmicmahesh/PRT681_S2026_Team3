# Week 8 (Report Writer Week 4) — Yuan Li, S390310

This week's focus was career preparation and foundational research, followed by hands-on implementation of the spatial data and Fabric ingestion practice tasks.

## Completed this week

- **Resume update** — added Power BI, Excel/VBA, and PowerShell/scripting skills reflecting the last 8 weeks of coursework.
- **Interview preparation** — prepared technical and behavioural Q&A for class (see `Interview_QA_YuanLi_S390310.docx`).
- **OneNote reorganisation** — moved course notes, GitHub content, and CDU OneNote content into a personal OneNote notebook, structured by Role (By Technology / By Topic / By Course → Paper) and Resume (Resume List, Q&A Round 1/2, Behavioural).
- **Spatial data research** — conceptual research into spatial/geospatial reporting (see `Spatial_Data_Research_Notes_YuanLi_S390310.md`), covering choropleth maps, GeoJSON, ArcGIS, Power BI Shape Maps, and Microsoft Fabric's Direct Lake mode.
- **Data anonymization** — replaced real employee names with anonymized codes (e.g. `Staff1`, `Staff12`) via deterministic ID-based mapping in Power Query (M), and added a fictional `Country` field (mapped from real-world country names) to support geographic analysis without exposing real location data.
- **Geographic dashboard** — built a "Geographic Overview" page in Power BI Desktop using the `Map` visual (Country as Legend, DISTINCTCOUNT-based Total Staff as Size). Fixed a data bug where default `Count of ID` was counting employee-days instead of unique employees.
- **Time-intelligence DAX measures** — added `Present This Week`, `Present Last Week`, and `WoW Change %` measures, cross-filterable by country via the map, to demonstrate dynamic week-over-week attendance trend analysis.
- **Microsoft Fabric Lakehouse** — created a Fabric Lakehouse (`Week4_Lakehouse`) in the PRT681 workspace (upgraded to Fabric trial capacity) and attempted to load the anonymized dataset as a new table. Encountered an `InvalidColumnName` error caused by spaces in the CSV column headers, which highlighted a practical constraint of Fabric's file-to-table ingestion.

## Files in this folder

- `Spatial_Data_Research_Notes_YuanLi_S390310.md` — spatial data concepts research
- `Interview_QA_YuanLi_S390310.docx` — technical + behavioural interview Q&A
- `Resume_YuanLi_S390310.docx` — updated resume
- `geographic_dashboard.png` — screenshot of the Geographic Overview dashboard page
- `anonymization_and_dax.md` — Power Query M anonymization code and time-intelligence DAX measures

## Next steps

- Continue exploring Direct Lake connectivity between the Fabric Lakehouse and the Power BI semantic model as a deeper demonstration of the Fabric ingestion pattern.
- Move on to Week 5 Report Analyst practice tasks — paginated reports (Power BI Report Builder) and dynamic Row-Level Security by region.
