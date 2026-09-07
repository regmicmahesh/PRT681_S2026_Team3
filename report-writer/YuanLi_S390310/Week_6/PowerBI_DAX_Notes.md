# Power BI Data Modeling & DAX — Concept Notes
**Yuan Li - S390310**

## What "data modeling" means in Power BI
Instead of one flat table (like our Excel Data sheet), Power BI connects
multiple related tables (e.g. Orders, Customers, Regions) through
relationships — similar to how our SQL practice used a JOIN between
Orders and Regional_Managers. This lets one dashboard pull from several
sources without duplicating data.

## What DAX is
DAX (Data Analysis Expressions) is Power BI's formula language for
calculated measures. Unlike a normal Excel formula (tied to one cell),
a DAX measure recalculates automatically based on whatever filter/slicer
the user has applied on the dashboard.

Example measures relevant to our Superstore data:
- `Total Sales = SUM(Orders[Sales])`
- `YTD Sales = TOTALYTD([Total Sales], Orders[Order Date])`
- `Sales % Change vs Prior Month = DIVIDE([Total Sales] - [Prior Month Sales], [Prior Month Sales])`

## Status
Hands-on practice is blocked — Power BI Desktop is Windows-only and I use
a MacBook. Discussing a workaround (remote lab / Windows VM / Power BI
Service) with Mahesh before continuing.
