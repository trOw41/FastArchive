Imports System.Drawing
Imports System.Windows.Forms

Public Class Dialog1
    Private cold As ColorDialog
    Private defFont As New Font("Bahnschrift SemiCondensed", 10, FontStyle.Regular)
    Private defColor As Color = SystemColors.Control
    Private cont As Control

    Private Sub Dialog1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ' Load saved color or default
        Form1.BackColor = My.Settings.AppColor
        Me.BackColor = My.Settings.AppColor

        ' Initialize FontBox with available font families
        FontBox.Items.Clear()
        For Each f As FontFamily In FontFamily.Families
            FontBox.Items.Add(f.Name)
        Next

        ' Try to select the saved font family
        If Not String.IsNullOrEmpty(My.Settings.AppFont.FontFamily.Name) AndAlso FontBox.Items.Contains(My.Settings.AppFont) Then
            FontBox.SelectedItem = My.Settings.AppFont
        ElseIf FontBox.Items.Count > 0 Then
            FontBox.SelectedIndex = 0 ' Select the first font as default
        End If

        ' Load saved "Use Default Font" state
        CheckBox1.Checked = My.Settings.AppFont.FontFamily.Name = defFont.Name
        'ToggleFontControls(Not My.Settings.UseDefaultFont) ' Enable/disable FontBox based on checkbox state
    End Sub

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        ' Save background color
        My.Settings.AppColor = Form1.BackColor

        ' Save font settings if not using default
        If Not FontBox.SelectedItem IsNot Nothing Then
            ' Speichere den Namen der ausgewählten Schriftart als String
            My.Settings.AppFont = FontBox.SelectedItem
        Else

        End If

        My.Settings.Save()
        Me.DialogResult = DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ' Open the color selection dialog
        Dim colorDialog As New ColorDialog()
        colorDialog.Color = Form1.BackColor ' Set initial color to current Form1 background
        If colorDialog.ShowDialog() = DialogResult.OK Then
            Form1.BackColor = colorDialog.Color
            Me.BackColor = colorDialog.Color
        End If
    End Sub

    Private Sub FontBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles FontBox.SelectedIndexChanged
        If FontBox.SelectedItem IsNot Nothing Then
            Try
                Dim selectedFontName As String = FontBox.SelectedItem.ToString()
                Dim newFont As New Font(selectedFontName, defFont.Size, defFont.Style) ' Use default size/style

                ' Apply font to Form1 and Dialog1
                Form1.SetFormFont(newFont)
                Me.SetFormFont(newFont)
            Catch ex As Exception
                MessageBox.Show($"Fehler beim Anwenden der Schriftart: {ex.Message}", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged

        If CheckBox1.Checked Then
            ' Set default font and save
            Form1.SetFormFont(defFont)
            Me.SetFormFont(defFont)
            My.Settings.AppFont = defFont ' Clear any saved custom font
        End If
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
End Class
