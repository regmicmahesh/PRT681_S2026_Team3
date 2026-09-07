# Excel VBA Macro Practice
**Yuan Li - S390310**

## What it does
Formats the Superstore report tables automatically instead of doing it
by hand each time: bolds header rows, applies currency formatting to
sales/profit columns, and auto-fits column widths.

## Macro code
\`\`\`vba
Sub FormatReportTable()
    Dim ws As Worksheet
    Dim headerRow As Range
    Set ws = ActiveSheet

    ' Bold the header row (row containing the active cell's table header)
    Set headerRow = ws.Rows(ActiveCell.Row)
    headerRow.Font.Bold = True
    headerRow.Interior.Color = RGB(30, 39, 97) ' navy
    headerRow.Font.Color = RGB(255, 255, 255)

    ' Auto-fit all used columns
    ws.UsedRange.Columns.AutoFit

    MsgBox "Formatting applied."
End Sub
\`\`\`

## How to run it (Excel for Mac)
1. Open the Excel file, go to Tools > Macro > Visual Basic Editor (or Alt+F11 on Windows)
2. Insert > Module, paste the code above
3. Click into a table's header row cell, then run the macro (F5 or Tools > Macro > Run)
