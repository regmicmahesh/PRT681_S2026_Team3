# Week 5 Presentation Notes

## What I Built

I built and published a monthly operational audit report using Power BI paginated reporting. The report uses an A4 landscape layout, parameters, repeating table headers, KPI totals, page numbers, and reliable PDF and Excel export.

The final RDL connects to the published `NT_Crime_Analytics` semantic model through the native Power BI dataset provider and single sign-on. It rendered three pages in Power BI Service and returned 2,339 offences for June 2026.

I also implemented dynamic row-level security. A mapping table links each Microsoft Entra user principal name to one or more reporting regions. The Regional Manager role uses `USERPRINCIPALNAME()` to filter the Crime table. The role was tested with assigned, unassigned, and owner identities, and the signed-in student account was added to the role in Power BI Service.

## Governance and Administration

- The semantic model and paginated report are stored in the same workspace so RLS can be enforced through SSO.
- The governance checklist covers DEV, TEST, and PROD promotion gates, access review, sensitivity, refresh ownership, export controls, and rollback.
- Workspace roles and semantic-model permissions are separated from data access. Report consumers should use Viewer and Read permissions when validating RLS.
- BusinessObjects is documented as a comparison platform for scheduled, parameter-driven enterprise distribution; no SAP BusinessObjects environment was supplied for deployment.

## Distribution

I created two monthly subscriptions on the final RLS-connected report: one PDF and one Excel subscription. Both run on the last day of the month at 7:15 PM Adelaide time, and both test sends completed successfully.

## Key Learning

Paginated reports solve fixed-layout and print requirements, while interactive Power BI reports support exploration. Dynamic RLS is more maintainable than one role per manager. Native Power BI SSO is essential because it passes the viewer identity to the semantic model and allows the same security rules to control both interactive and paginated reporting.
