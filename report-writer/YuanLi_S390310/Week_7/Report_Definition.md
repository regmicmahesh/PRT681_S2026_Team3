# Report Definition Document
**Yuan Li - S390310**

Describes how each metric on the Staff Attendance & Workforce Analytics dashboard is calculated, for future maintainers.

**Total Staff** — Count of unique employees in the selected period.

**Workforce Availability %** — Present/Worked days ÷ expected working days, with public holidays excluded from the denominator.

**Average Attendance Days** — Average number of Present days per staff member across the period.

**Staff with Leave Record** — Count of unique Full Time staff with at least one approved leave entry. Unauthorised Absence is excluded from this count, and Casual staff are excluded since they don't use leave codes.

**Unauthorised Absence (UA)** — Count of records where a staff member did not attend and had no recorded leave. Kept separate from normal approved leave throughout the dashboard, since it's the one category requiring management follow-up.

## Data model logic
- Full Time: blank = Present, leave code = Approved Leave, UA = Unauthorised Absence
- Casual: hours > 0 = Worked, 0 = Not Worked, UA = Unauthorised Absence (Casual staff never use leave codes)
- Employment Type derived from the "Leave" column: value = "Casual" → Casual, else → Full Time
