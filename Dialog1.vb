Imports System.Drawing.Imaging
Imports Windows.UI
Imports System.Drawing.Color
Imports Windows.UI.WindowManagement
Imports Windows.System
Imports System.Drawing.Font
Imports Windows.Media.Capture

Public Class Dialog1
    Private cold As ColorDialog
    Private defFont As New Font("Arial", 11, Drawing.FontStyle.Regular)
    Private defBackColor As Drawing.Color = SystemColors.ControlLightLight
    Private defFontColor As Drawing.Color = SystemColors.WindowText
    Private defControlColor As Drawing.Color = SystemColors.ControlLightLight
    Private newFont As New Font("Bahnschrift SemiLight Condensed", 11)
    Private cont As Control
    Dim Color As Drawing.Color
    Private Sub Dialog1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.BackColor = My.Settings.AppColor
        Me.ForeColor = My.Settings.StringColor
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
            My.Settings.AppFont = defFont
            My.Settings.Save()
        End If
    End Sub

    Private Sub Font2_CheckedChanged(sender As Object, e As EventArgs) Handles Font2.CheckedChanged
        If Font2.Checked Then
            Font1.Checked = False
            Form1.SetFormFont(newFont)
            SetFormFont(newFont)
            UpdateChildControlFonts(Form1, newFont)
            My.Settings.AppFont = newFont
            My.Settings.Save()
        End If
    End Sub

    Private Sub LightTheme_CheckedChanged(sender As Object, e As EventArgs) Handles LightTheme.CheckedChanged
        Dim bc As Drawing.Color = defBackColor
        Dim fc As Drawing.Color = defFontColor
        Dim cc As Drawing.Color = defControlColor
        If LightTheme.Checked Then
            DarkTheme.Checked = False
            My.Settings.AppColor = bc
            Form1.BackColor = bc
            Panel2.BackColor = bc
            Label4.ForeColor = fc
            My.Settings.StringColor = fc
            Me.BackColor = bc
            SetFontColor(fc, FontStyle.Regular)
            SetControlColor(sender, cc)
            UpdateChildControlBackColors(Form1, cc)
            UpdateChildControlForeColors(Form1, fc)

            My.Settings.Save()
        End If
    End Sub

    Private Sub DarkTheme_CheckedChanged(sender As Object, e As EventArgs) Handles DarkTheme.CheckedChanged
        Dim bc As Drawing.Color = SystemColors.ControlDarkDark
        Dim fc As Drawing.Color = WhiteSmoke
        Dim cc As Drawing.Color = SystemColors.WindowFrame
        If DarkTheme.Checked Then
            LightTheme.Checked = False
            My.Settings.AppColor = bc
            Form1.BackColor = bc
            Panel2.BackColor = bc
            Label4.ForeColor = fc
            My.Settings.StringColor = fc
            Me.BackColor = bc
            SetFontColor(fc, FontStyle.Regular)
            SetControlColor(sender, cc)
            UpdateChildControlBackColors(Form1, cc)
            UpdateChildControlForeColors(Form1, fc)

            My.Settings.Save()
        End If
    End Sub
End Class
