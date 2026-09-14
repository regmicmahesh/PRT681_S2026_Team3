let
    Source = Csv.Document(
        Web.Contents("https://raw.githubusercontent.com", [RelativePath="regmicmahesh/PRT681_S2026_Team3/main/report-writer/ShijianZhu_S394861/Week6/report-writer/ShijianZhu_S394861/Role_Based_Tasks/Week_2_Dashboarding_Automation/data/park_visits.csv", Timeout=#duration(0, 0, 2, 0)]),
        [Delimiter=",", Encoding=65001, QuoteStyle=QuoteStyle.Csv]
    ),
    Headers = Table.PromoteHeaders(Source, [PromoteAllScalars=true]),
    Required = Table.SelectColumns(Headers, {"Year", "Region", "Park", "Visits"}, MissingField.Error),
    Typed = Table.TransformColumnTypes(Required, {{"Year", Int64.Type}, {"Region", type text}, {"Park", type text}, {"Visits", Int64.Type}}),
    Invalid = Table.SelectRows(Typed, each [Year] = null or [Visits] = null or [Visits] < 0 or [Park] = null or [Region] = null or Text.Trim([Park]) = "" or Text.Trim([Region]) = ""),
    Checked = if Table.RowCount(Typed) = 0 then error "The source has no records."
        else if Table.RowCount(Invalid) > 0 then error "Missing identifiers or invalid visit count."
        else if Table.RowCount(Table.Distinct(Typed, {"Park", "Year"})) <> Table.RowCount(Typed) then error "Duplicate Park-Year records."
        else Typed
in
    Checked
