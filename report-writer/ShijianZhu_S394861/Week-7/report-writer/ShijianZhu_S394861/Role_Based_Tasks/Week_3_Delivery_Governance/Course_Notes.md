# Week 3 course notes

These are study notes linked to the practice, not a claim of video completion or software deployment.

## Power BI: Publishing and Sharing Reports

Publishing places the report and its semantic model in a workspace. Sharing controls who can view it. For this exercise, the useful evidence is the workspace report, working filters, saved refresh settings and refresh history. A GitHub upload stores project files but does not publish a Power BI report. [Microsoft publishing guide](https://learn.microsoft.com/en-us/power-bi/create-reports/desktop-upload-desktop-files).

## SSAS and Power BI

SQL Server Analysis Services provides analytical models that reporting tools can query. Power BI can connect to an SSAS model rather than owning all calculations locally. An on-premises SSAS connection through the Service needs appropriate gateway and identity configuration. Import and live connections have different refresh behaviour; a live report does not use the same imported-data refresh cycle as this CSV exercise. No SSAS server is required or deployed for this task. [Microsoft SSAS gateway guide](https://learn.microsoft.com/en-us/power-bi/connect-data/service-gateway-enterprise-manage-ssas).

## Fabric and data warehousing

A data warehouse organises integrated data for analysis. Fabric Warehouse offers a managed SQL analytics environment; a larger version of this project could store a FactParkVisits table with Year and Park dimensions there. For 110 sample records, the public CSV keeps the practice easier to inspect. No warehouse was provisioned. [Fabric Warehouse overview](https://learn.microsoft.com/en-us/fabric/data-warehouse/data-warehousing).

## Business objects

The course title is ambiguous. In this BI context these notes provisionally interpret it as SAP BusinessObjects, a suite for reporting, visualisation and sharing. It provides a comparison with Power BI's reporting and distribution approach. If the lecturer means general business objects instead, the relevant domain objects here are Park, Region, Year and VisitRecord. No SAP BusinessObjects system has been used. [SAP learning overview](https://learning.sap.com/products/business-technology-platform/data-analytics/businessobjects).

## Data storytelling

Start with a question: did all regions share the 2025 increase? The overall estimate rose by 5.3%, but Katherine fell from 532,800 to 475,300 visits. Show the overall card, then the region comparison, then a park detail. This gives the reader a reason to use the filters. Label estimates clearly and avoid claiming a cause from a trend alone. These comparisons are student analysis of the Parks & Wildlife NT snapshot, not government conclusions. [Original source](https://dth.nt.gov.au/parks-and-wildlife/parks-and-wildlife-statistics-and-research/park-visitor-data).
