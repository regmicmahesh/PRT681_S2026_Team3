Spatial Data — Basic Research Notes

Yuan Li — S390310 — Report Writer Week 4 research (conceptual overview only, no hands-on build this week)

What is spatial data?

Spatial data (also called geospatial data) is data that has a location attached to it — for example, a postcode, a suburb/region name, or a latitude/longitude coordinate. Instead of just showing "Sales = $1,200", spatial data lets you show "Sales = $1,200 in this region", so it can be placed on a map.

Common examples: postcodes, addresses, state/territory boundaries, GPS coordinates (latitude/longitude), store or branch locations.

Key concepts
Latitude/longitude — the standard coordinate system for marking an exact point on Earth (like a pin on a map).
Boundary shapes — the outline of an area, like a state, suburb, or postcode zone. These shapes are what let software colour in a whole region, not just a single point.
Choropleth map — a map where each region is shaded a different colour depending on a value (e.g. darker colour = higher sales). This is the most common way to show spatial data on a dashboard.
GeoJSON — a file format used to store the shape/boundary of regions (like state or postcode outlines) so mapping tools can read and draw them.
ArcGIS — a professional mapping software (by Esri) used to create and manage detailed geographic maps and shape files; GeoJSON shape files can be created or edited using tools like ArcGIS.
How this connects to Power BI

Power BI has a "Shape Map" visual that can take a GeoJSON file (custom region shapes) and colour each region based on your data — this is how you'd build a choropleth map inside a dashboard, plotting a KPI (like sales or attendance) by territory/region instead of just as a flat table.

Microsoft Fabric — Direct Lake mode

Microsoft Fabric is a larger Microsoft data platform that stores data centrally (in something called a "Lakehouse") so reports can connect straight to shared, governed data instead of a local file. "Direct Lake mode" is a way for Power BI to read data directly from the Fabric Lakehouse without fully importing/copying it first — this makes big datasets faster to work with, because Power BI queries the data where it already lives instead of loading a full copy into the report.

Why this matters for reporting

If a report needs to show "which region needs attention" (e.g. which postcode area has the most support tickets, or which state has the lowest sales), a map-based visual communicates that faster than a table of numbers, because the audience can see the pattern by area at a glance.

Status

This week's task was scoped to research only (confirmed with team) — no dataset was imported into Fabric and no dashboard was built yet. This note covers the concepts so they're understood ahead of any future hands-on task.
