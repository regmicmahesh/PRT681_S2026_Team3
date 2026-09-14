# Publish and refresh — execution guide

Status: prepared instructions. No Power BI workspace URL, schedule or successful refresh is claimed yet.

## 1. Prepare and publish

1. On Windows, extract powerbi/ to a short local path. Open NTParkVisits.pbip with its Report and SemanticModel folders beside it. If using an existing edited report, apply the new M query there instead.
2. Choose Transform data and verify Visits uses Web.Contents with the supplied GitHub source. Use Anonymous authentication and Public privacy for that source. The Year query should derive distinct years from Visits; Park already derives from Visits.
3. Close & Apply and Refresh. Confirm 110 baseline rows, 22 parks and five years. Test the four 2025 values in Report_Definition.md; check 2021 gives blank prior-year change. Confirm year and region slicers work and the trend retains five years.
4. Save. Publish to an existing course workspace where you have the necessary rights; if unavailable, use My workspace. Use your school account and a supported account/license. Do not create a paid subscription just for this draft.
5. Open the published report and verify that both the report and semantic model appear in the workspace. Record their actual URLs in Evidence_Checklist.md.

Desktop publishing can publish a PBIP project. If transferring via a Mac browser, first save a PBIX from Desktop on Windows, then upload that PBIX through the workspace upload control. A PBIP file alone is not a browser-uploadable complete report. Mac users can view and configure a published report in the browser, but these local Desktop steps need Windows access.

## 2. Configure the Service

1. In the destination workspace, open the **semantic model**, then Refresh > Schedule refresh (or its Settings > Refresh section).
2. Configure its cloud connection/data source credentials for the public raw.githubusercontent.com source using Anonymous authentication. A gateway is normally unnecessary for this cloud-only Web.Contents source; if Service requests one, check that no local Excel/File.Contents source remains and inspect the connection configuration before installing anything.
3. Run **Refresh now** first. Check Refresh history for a completed entry. A Desktop refresh is not proof of Service refresh.
4. Turn the refresh schedule On. Choose Weekly, Monday, 08:00, and the Darwin time zone (UTC+09:30). If the picker groups cities, choose the Darwin option, not Adelaide's daylight-saving zone. This is a proposed weekly demonstration schedule, not a statement that the official source updates weekly.
5. Enable failure notification to the semantic model owner and apply/save the settings. Take a screenshot of the saved configuration.
6. After the next scheduled run, inspect Refresh history. Record the actual timestamp, type and status. A successful manual run plus a saved schedule does not yet prove that a scheduled run succeeded. For an earlier demonstration, choose a suitable available time slot and restore the weekly setting afterwards.

## 3. Meaning of refresh

The CSV is a reviewed copy of official annual data in the team's existing main branch. Scheduled refresh contacts that online file again. If its content has not changed, the totals should stay the same; unchanged numbers do not imply failure. Record refresh history as evidence. Never fabricate new park counts to make the dashboard appear updated.

When a new official year is published, verify and update the source CSV first, retaining the four column names and one row per Park–Year. Then refresh and update the control totals in the definition document. Renaming or moving the current repository file will break the source URL. Keep source modifications separate from this draft unless explicitly needed.

## 4. Governance and handover

Shijian Zhu is the proposed report maintainer. Grant report viewing to the intended course audience only when needed; editing/build rights should be limited to maintainers. Publishing into a workspace and sharing with someone are separate actions, and sharing can depend on licenses and tenant rules. Do not use Publish to web as a shortcut for course sharing. Keep credentials and tenant tokens out of GitHub. Review changes on the draft branch before merging. For refresh failures, inspect the error, source accessibility and schema, correct the issue, retry once, and record the resolution. Before reverting a model change, retain a copy and identify the last known working revision.

## References

1. [Publish from Power BI Desktop](https://learn.microsoft.com/en-us/power-bi/create-reports/desktop-upload-desktop-files)
2. [Configure scheduled refresh](https://learn.microsoft.com/en-us/power-bi/connect-data/refresh-scheduled-refresh)
3. [Data refresh in Power BI](https://learn.microsoft.com/en-us/power-bi/connect-data/refresh-data)

Documentation checked 14 September 2026. UI labels can differ by tenant/version; actual account state takes precedence.
