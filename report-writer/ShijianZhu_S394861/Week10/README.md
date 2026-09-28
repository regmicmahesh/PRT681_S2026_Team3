# Week 5 Enterprise Reporting Delivery (Week10)

## Completion Status

This folder contains the completed Week 5 implementation for paginated reporting, dynamic row-level security, governance, and multi-format distribution. The Power BI report, semantic model, and final RLS-connected paginated report are published in `My workspace`.

## Published Reports

- Interactive Power BI report: https://app.powerbi.com/groups/me/reports/ed4459dc-58e2-43e7-9b9d-ace3c8403958?pbi_source=desktop
- Final RLS-connected paginated report: https://app.powerbi.com/groups/me/rdlreports/076d5e6e-f661-4773-b09e-b9ab6f516548?experience=power-bi
- Self-contained paginated fallback: https://app.powerbi.com/groups/me/rdlreports/27d51ffb-ab1b-49a6-852f-018a1e42de75?experience=power-bi

## Final Deliverables

- `paginated_report/NT_Crime_Monthly_Operational_Audit_RLS_LIVE.rdl`: final A4 landscape paginated report connected to the published semantic model through native Power BI SSO.
- `paginated_report/NT_Crime_Monthly_Operational_Audit.rdl`: self-contained fallback report with embedded audit data.
- `Week5_Audit_and_RLS_Data.xlsx`: audit summary, RLS mapping, and 12 months of aggregated operational data.
- `exports/NT_Crime_Monthly_Operational_Audit_2026-06.pdf`: final printable audit package.
- `semantic_model/`: Regional Manager role, territory mapping table, and validation evidence.
- `governance/`: workspace lifecycle and administration controls.
- `distribution/`: implemented subscription status and regional burst controls.

## Verified Results

- June 2026 total offences: 2,339.
- Domestic violence offences: 507.
- Alcohol-related offences: 310.
- Offences without SA2: 1,812.
- Paginated output: three rendered pages in Power BI Service.
- Local RLS test: Darwin manager returned 24,172 offences and one region.
- Unassigned RLS test: no rows returned.
- Owner test account: 84,862 offences across eight regions.

## Distribution

Two subscriptions are active on the final RLS-connected report:

- `Week5 RLS Monthly Audit PDF`
- `Week5 RLS Monthly Audit Excel`

Both run monthly on the last day at 7:15 PM in the Adelaide time zone. Both test sends completed successfully.

## Production Note

The `example.com` manager addresses are controlled demonstration identities. Replace them with real Microsoft Entra UPNs before enabling regional manager delivery. The student test account is assigned to the Regional Manager role for end-to-end validation.
