Imports System.Drawing.Imaging
Imports Windows.UI
Imports System.Drawing.Color
Imports Windows.UI.WindowManagement
Imports Windows.System
Imports System.Drawing.Font
Imports Windows.Media.Capture
Imports Syncfusion.XForms.Themes
Imports Windows.Devices.Sensors
Imports Windows.Management.Deployment.Preview
Imports System.IO.Compression

Public Class SettingsDialog
    Private cold As ColorDialog
    Private defFont As New Font("Arial", 9.75, Drawing.FontStyle.Regular)
    Private defBackColor As Drawing.Color = SystemColors.ControlLightLight
    Private defFontColor As Drawing.Color = SystemColors.ControlText
    Private defControlColor As Drawing.Color = SystemColors.ControlLightLight
    Private newFont As New Font("Bahnschrift SemiLight Condensed", 10.75, Drawing.FontStyle.Regular)
    Private cont As Control
    Private checked As Boolean
    Dim Color As Drawing.Color
    Dim Dark As String = "Dark"
    Dim Light As String = "Light"
    Dim Classic As String = "Classic"
    Dim Modern As String = "Modern"
    Dim Style1 As String = ""
    Dim Style2 As String = ""
    Private Sub Dialog1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.BackColor = My.Settings.AppColor
        Me.ForeColor = My.Settings.StringColor
        Style1 = My.Settings.StyleF
        Style2 = My.Settings.StyleC
        If Style1 Is Nothing Then
            Style1 = Classic
            Font1.Checked = True
        ElseIf Style1 = Modern Then
            Font2.Checked = True
        End If
        If Style2 = Light Then
            LightT.Checked = True
        ElseIf Style2 = Dark Then
            DarkT.Checked = True
        End If
        If My.Settings.CompressionMode = modeOptimal Then
            RadioButton2.Checked = True
        ElseIf My.Settings.CompressionMode = modeFast Then
            RadioButton1.Checked = True
        Else RadioButton3.Checked = True
        End If
    End Sub

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        My.Settings.Save()
        Me.DialogResult = DialogResult.OK
        Me.Close()
    End Sub

    Public Sub SetFormFont(newFont As Font)
        Me.Font = newFont
        UpdateChildControlFonts(Me, newFont)
    End Sub

    Private Sub UpdateChildControlFonts(control As Control, newFont As Font)
        For Each childControl As Control In control.Controls
            childControl.Font = newFont
            If childControl.HasChildren Then
                UpdateChildControlFonts(childControl, newFont)
            End If
        Next
    End Sub

    Private Sub SetFontColor(sender As Object, e As Drawing.FontStyle)
        ' Setzt die Schriftfarbe aller Controls im Dialog auf die in den Einstellungen gespeicherte Farbe
        Dim fontColor As Drawing.Color = My.Settings.StringColor
        Me.ForeColor = fontColor
        Form1.ForeColor = fontColor
        My.Settings.StringColor = fontColor
        UpdateChildControlForeColors(Me, fontColor)
    End Sub
    Private Sub SetControlColor(sender As Object, e As Drawing.Color)
        ' Setzt die Hintergrundfarbe aller Controls im Dialog auf die in den Einstellungen gespeicherte Farbe
        Dim controlColor As Drawing.Color = My.Settings.AppColor
        My.Settings.ControlColor = controlColor
        Form1.SetControlColor(sender, controlColor)
        UpdateChildControlBackColors(Me, controlColor)
    End Sub
    Private Sub UpdateChildControlBackColors(control As Control, color As Drawing.Color)
        For Each childControl As Control In control.Controls
            childControl.BackColor = color
            If childControl.HasChildren Then
                UpdateChildControlBackColors(childControl, color)
            End If
        Next
    End Sub
    Private Sub UpdateChildControlForeColors(control As Control, color As Drawing.Color)
        For Each childControl As Control In control.Controls
            childControl.ForeColor = color
            If childControl.HasChildren Then
                UpdateChildControlForeColors(childControl, color)
            End If
        Next
    End Sub

    Private Sub Font1_CheckedChanged(sender As Object, e As EventArgs) Handles Font1.CheckedChanged
        If Font1.Checked Then
            Font2.Checked = False
            Form1.SetFormFont(defFont)
            SetFormFont(defFont)
            UpdateChildControlFonts(Form1, defFont)
            My.Settings.StyleF = "Classic"
            My.Settings.AppFont = defFont
        Else
            Font2.Checked = True
        End If
        My.Settings.Save()
    End Sub

    Private Sub Font2_CheckedChanged(sender As Object, e As EventArgs) Handles Font2.CheckedChanged
        If Font2.Checked Then
            Font1.Checked = False
            Form1.SetFormFont(newFont)
            SetFormFont(newFont)
            UpdateChildControlFonts(Form1, newFont)
            My.Settings.StyleF = Modern
            My.Settings.AppFont = newFont

        Else
            Font1.Checked = True
        End If
        My.Settings.Save()
    End Sub

    Private Sub LightTheme_CheckedChanged(sender As Object, e As EventArgs) Handles LightT.CheckedChanged
        Dim bc As Drawing.Color = defBackColor
        Dim fc As Drawing.Color = defFontColor
        Dim cc As Drawing.Color = defControlColor
        If LightT.Checked Then
            DarkT.Checked = False
            My.Settings.AppColor = bc
            Form1.BackColor = bc
            Panel_11.BackColor = bc
            Label4.ForeColor = fc
            My.Settings.StringColor = fc
            Me.BackColor = bc
            SetFontColor(fc, FontStyle.Regular)
            SetControlColor(sender, cc)
            UpdateChildControlBackColors(Form1, cc)
            UpdateChildControlForeColors(Form1, fc)
            My.Settings.StyleC = "Light"

        Else
            DarkT.Checked = True
        End If
        My.Settings.Save()
    End Sub

    Private Sub DarkTheme_CheckedChanged(sender As Object, e As EventArgs) Handles DarkT.CheckedChanged
        Dim bc As Drawing.Color = SystemColors.WindowFrame
        Dim fc As Drawing.Color = GhostWhite
        Dim cc As Drawing.Color = SystemColors.WindowFrame
        If DarkT.Checked Then
            LightT.Checked = False
            My.Settings.AppColor = bc
            Form1.BackColor = bc
            Panel_11.BackColor = bc
            Label4.ForeColor = fc
            My.Settings.StringColor = fc
            Me.BackColor = bc
            SetFontColor(fc, FontStyle.Regular)
            SetControlColor(sender, cc)
            UpdateChildControlBackColors(Form1, cc)
            UpdateChildControlForeColors(Form1, fc)
            My.Settings.StyleC = "Dark"
            Form1.FileList.ForeColor = SystemColors.WindowText
            Form1.FileList.BackColor = SystemColors.ControlDarkDark
        Else
            LightT.Checked = True
        End If
        My.Settings.Save()
    End Sub

    Private Sub RadioButton2_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton2.CheckedChanged

        If RadioButton2.Checked = True Then
            RadioButton1.Checked = False
            RadioButton3.Checked = False
            My.Settings.CompressionMode = modeOptimal
        End If
    End Sub

    Private Sub RadioButton1_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton1.CheckedChanged
        If RadioButton1.Checked = True Then
            RadioButton2.Checked = False
            RadioButton3.Checked = False
            My.Settings.CompressionMode = modeFast
        End If
    End Sub

    Private Sub RadioButton3_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton3.CheckedChanged
        If RadioButton3.Checked = True Then
            RadioButton1.Checked = False
            RadioButton2.Checked = False
            My.Settings.CompressionMode = modeUltra
        End If
    End Sub

    Private modeFast As String = CompressionLevel.Fastest
    Private modeOptimal As String = CompressionLevel.Optimal
    Private modeUltra As String = CompressionLevel.SmallestSize
End Class
