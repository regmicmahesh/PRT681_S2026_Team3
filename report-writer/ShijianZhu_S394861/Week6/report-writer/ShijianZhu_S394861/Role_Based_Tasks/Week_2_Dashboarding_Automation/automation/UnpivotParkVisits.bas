Attribute VB_Name = "UnpivotParkVisits"
Option Explicit

' Automates reshaping the annual wide table into one record per park/year.
' Preserves SourceWide. Replaces only the macro-owned MacroOutput sheet.
Public Sub PrepareParkVisits()
    Dim src As Worksheet, dst As Worksheet
    Dim lastRow As Long, r As Long, c As Long, n As Long, year As Long
    Dim oldScreen As Boolean, oldEvents As Boolean, msg As String
    Dim values() As Variant, v As Variant
    oldScreen = Application.ScreenUpdating
    oldEvents = Application.EnableEvents
    On Error GoTo Failed
    Set src = ThisWorkbook.Worksheets("SourceWide")
    If src.Cells(1, 1).Value <> "Region" Or src.Cells(1, 2).Value <> "Park" Then
        Err.Raise vbObjectError + 1, , "Expected Region and Park in A1:B1."
    End If
    For c = 3 To 7
        If Not IsNumeric(src.Cells(1, c).Value) Then
            Err.Raise vbObjectError + 2, , "Expected numeric year headers 2021 to 2025."
        End If
        If CDbl(src.Cells(1, c).Value) <> 2018 + c Then
            Err.Raise vbObjectError + 3, , "Expected year headers 2021 to 2025."
        End If
    Next c
    lastRow = src.Cells(src.Rows.Count, 2).End(xlUp).Row
    If lastRow < 2 Then Err.Raise vbObjectError + 4, , "No park records found."
    ReDim values(1 To (lastRow - 1) * 5, 1 To 4)
    ' Build and validate every output record before replacing the previous result.
    For r = 2 To lastRow
        If Len(Trim$(CStr(src.Cells(r, 1).Value))) = 0 Or _
           Len(Trim$(CStr(src.Cells(r, 2).Value))) = 0 Then
            Err.Raise vbObjectError + 5, , "Missing region or park at row " & r
        End If
        For c = 3 To 7
            v = src.Cells(r, c).Value2
            If IsError(v) Or IsEmpty(v) Then
                Err.Raise vbObjectError + 6, , "Missing/error count at " & src.Cells(r, c).Address
            End If
            If Not IsNumeric(v) Then Err.Raise vbObjectError + 7, , "Non-numeric visit count."
            If CDbl(v) < 0 Or CDbl(v) <> Fix(CDbl(v)) Then
                Err.Raise vbObjectError + 8, , "Visit counts must be non-negative whole numbers."
            End If
            n = n + 1
            values(n, 1) = CLng(src.Cells(1, c).Value)
            values(n, 2) = Trim$(CStr(src.Cells(r, 1).Value))
            values(n, 3) = Trim$(CStr(src.Cells(r, 2).Value))
            values(n, 4) = CDbl(v)
        Next c
    Next r
    Application.ScreenUpdating = False
    Application.EnableEvents = False
    On Error Resume Next
    Set dst = ThisWorkbook.Worksheets("MacroOutput")
    On Error GoTo Failed
    If dst Is Nothing Then
        Set dst = ThisWorkbook.Worksheets.Add(After:=src)
        dst.Name = "MacroOutput"
    Else
        dst.Cells.Clear
    End If
    dst.Cells(1, 1).Value = "Year"
    dst.Cells(1, 2).Value = "Region"
    dst.Cells(1, 3).Value = "Park"
    dst.Cells(1, 4).Value = "Visits"
    dst.Range("A2").Resize(n, 4).Value = values
    dst.Columns("A").NumberFormat = "0"
    dst.Columns("D").NumberFormat = "#,##0"
    dst.Range("A1:D1").Font.Bold = True
    dst.Range("A1:D1").Interior.Color = RGB(230, 230, 230)
    dst.Columns("A:D").AutoFit
    Application.ScreenUpdating = oldScreen
    Application.EnableEvents = oldEvents
    MsgBox n & " park-year records created. SourceWide has been retained.", vbInformation
    Exit Sub
Failed:
    msg = Err.Description
    Application.ScreenUpdating = oldScreen
    Application.EnableEvents = oldEvents
    MsgBox "Conversion stopped: " & msg, vbExclamation
End Sub
