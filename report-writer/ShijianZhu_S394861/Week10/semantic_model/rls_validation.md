# Dynamic RLS Validation

## Production Role Logic

The `Regional Manager` role uses `LOWER(USERPRINCIPALNAME())` and matches it to `RLS Territory Access[UserEmail]`. A Crime row is returned only when the signed-in user's mapping includes the row's Reporting Region. A user without a mapping is denied by default.

## Local Validation Results

- Baseline model: 84,862 offences across eight regions.
- Darwin manager identity: 24,172 offences and one region.
- Unassigned identity: no Crime rows.
- Student owner mapping: 84,862 offences across eight regions.

The local tests were completed before the production expression was reduced to `USERPRINCIPALNAME()` only. This prevents a test-only custom identity from interfering with Power BI Service SSO.

## Service Validation Results

- The semantic model and final RDL were published to the same Power BI workspace.
- `S394861@students.cdu.edu.au` was added to the `Regional Manager` role.
- The final paginated report uses the native `PBIDATASET` provider with ClaimsToken SSO.
- The report rendered three pages and returned 2,339 offences for June 2026.
- PDF and Excel subscriptions were saved and test-sent from the RLS-connected report.

## Administration Note

Workspace Admin, Member, and Contributor roles are not restricted in the same way as Viewer consumers. Production validation should use Viewer access plus semantic-model Read permission for the manager accounts.
