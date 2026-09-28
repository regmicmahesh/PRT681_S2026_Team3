# Enterprise Governance and Workspace Lifecycle

## Workspace Lifecycle

| Stage | Workspace | Purpose | Access |
| --- | --- | --- | --- |
| Development | NT Crime DEV | Build RDL, semantic model, RLS, and refresh logic | Contributors only |
| Test | NT Crime TEST | Validate data, security, export layout, and subscriptions | Testers and selected business owners |
| Production | NT Crime PROD | Approved reporting and scheduled distribution | Viewers; limited administrators |

## Deployment Gates

1. Reconcile the monthly RDL totals to the source CSV and Power BI semantic model.
2. Test the Regional Manager role with each real user principal name.
3. Confirm that unassigned users see no data.
4. Check PDF page breaks, repeating headers, margins, and Excel export columns.
5. Confirm sensitivity labels and export permissions.
6. Record the semantic model, RDL, owner, refresh schedule, and approval date.
7. Promote through Development, Test, and Production without editing Production directly.

## Administration Controls

- Use least-privilege workspace roles.
- Give report consumers Read permission and grant Build permission only when required.
- Assign RLS users to the Regional Manager role after publishing.
- Keep the report and semantic model lineage visible and document ownership.
- Review inactive users, sharing links, subscriptions, and refresh failures monthly.
- Use certified or promoted content only after business approval.

## BusinessObjects Comparison

- A Power BI paginated report is comparable to a scheduled Crystal Reports or Web Intelligence output when the requirement is fixed layout and print-ready export.
- Power BI workspaces and deployment pipelines provide lifecycle separation similar to managed promotion between development, test, and production environments.
- Power BI subscriptions and Power Automate flows replace recurring PDF or Excel distribution jobs while preserving semantic-model security.
