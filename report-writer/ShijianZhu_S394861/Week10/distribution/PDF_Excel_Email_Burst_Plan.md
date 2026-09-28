# PDF and Excel Distribution Implementation

## Implemented Subscriptions

The final RLS-connected paginated report is published in `My workspace` and has two active subscriptions:

1. `Week5 RLS Monthly Audit PDF` attaches the full report as PDF.
2. `Week5 RLS Monthly Audit Excel` attaches the full report as Microsoft Excel.

Both subscriptions:

- send to the signed-in student account;
- use the current June 2026 and All Territories parameters;
- run on the last day of every month;
- run at 7:15 PM in the Adelaide time zone; and
- passed a manual Send now test on 28 September 2026.

## Regional Burst Design

For production, use a controlled recipient table containing `UserEmail`, `ReportingRegion`, `AttachmentFormat`, and `Active`. Each active row should drive an export with the correct region parameter, a consistent attachment name, an approved recipient, and an auditable run result.

## Controls

- Replace demonstration `example.com` identities with real Microsoft Entra UPNs.
- Give each viewer Read permission on the semantic model so RLS is enforced.
- Use Viewer workspace access for consumers; Admin, Member, and Contributor roles can bypass normal RLS validation.
- Apply the correct sensitivity label to exported governed copies.
- Log failed exports, missing recipients, and oversized attachments.
- Test one regional recipient before activating the complete burst list.
