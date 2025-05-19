Public Class Dialog1
    Private cold As ColorDialog
    Private defFont As New Font("Arial", 11, Drawing.FontStyle.Regular)
    Private defColor As Drawing.Color = SystemColors.Control
    Private cont As Control
    Dim Color As SystemColors
    Private Sub Dialog1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ' Load saved color or default
        Form1.BackColor = My.Settings.AppColor
        Me.BackColor = My.Settings.AppColor

        ' Initialize FontBox with available font families
        FontBox.Items.Clear()
        For Each f As FontFamily In FontFamily.Families
            FontBox.Items.Add(f.Name)
        Next

        ' Set ColorDialog1 to saved color or default
        If My.Settings.AppColor.Name.Length > 1 Then
            ColorDialog1.Color = My.Settings.AppColor
        Else
            ColorDialog1.Color = DefaultBackColor
        End If

        ' Try to select the saved font family
        If My.Settings.AppFont IsNot Nothing AndAlso Not String.IsNullOrEmpty(My.Settings.AppFont.FontFamily.Name) Then
            If FontBox.Items.Contains(My.Settings.AppFont.FontFamily.Name) Then
                FontBox.SelectedItem = My.Settings.AppFont.FontFamily.Name
            ElseIf FontBox.Items.Count > 0 Then
                FontBox.SelectedIndex = 0 ' Select the first font as default
                My.Settings.AppFont = FontBox.SelectedItem
            End If
        End If

        ' Load saved "Use Default Font" state
        'heckBox1.Checked = My.Settings.AppFont.FontFamily.Name = defFont.Name
        'ToggleFontControls(Not My.Settings.UseDefaultFont) ' Enable/disable FontBox based on checkbox state
    End Sub

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        ' Save background color
        My.Settings.AppColor = Form1.BackColor
        'Form1.BackColor = My.Settings.AppColor
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
        Dim colorDialog As New ColorDialog With {
            .Color = Form1.BackColor ' Set initial color to current Form1 background
            }
        If colorDialog.ShowDialog() = DialogResult.OK Then
            Form1.BackColor = colorDialog.Color
            Me.BackColor = colorDialog.Color
            ' Save the selected color to settings
            My.Settings.AppColor = colorDialog.Color
        End If
    End Sub

    Private Sub FontBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles FontBox.SelectedIndexChanged
        If FontBox.SelectedItem IsNot Nothing Then
            Try
                Dim selectedFontName As String = FontBox.SelectedItem.ToString()
                Dim newFont As New Font(selectedFontName, defFont.Size, defFont.Style) ' Verwende Standardgröße/-stil

                ' Wende die Schriftart auf Form1 und Dialog1 an
                Form1.SetFormFont(newFont)
                Me.SetFormFont(newFont)

                ' Speichere die ausgewählte Schriftart in den Einstellungen
                My.Settings.AppFont = newFont
            Catch ex As Exception
                MessageBox.Show($"Fehler beim Anwenden der Schriftart: {ex.Message}", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub

    Public Function GetColor() As SystemColors
        Return Color
    End Function

    Public Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged

        If CheckBox1.Checked = True Then


            ' Set default font and save

            Form1.SetFormFont(defFont)
            Me.SetFormFont(defFont) ' Clear any saved custom font
            CheckBox1.Checked = False
            FontBox.SelectedItem = 0
            Form1.FileList.BackColor = SystemColors.Window
            Form1.FileList.ForeColor = SystemColors.WindowText
            Form1.UpdateChildControlBackColors(Form1, DefaultBackColor)
            Form1.UpdateChildControlForeColors(Form1, DefaultForeColor)
            UpdateChildControlBackColors(Me, DefaultBackColor)
            UpdateChildControlForeColors(Me, DefaultForeColor)
            My.Settings.AppFont = defFont
            My.Settings.Save()
            Me.Close()
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

    Private Sub FontColorButton_Click(sender As Object, e As EventArgs) Handles FontColorButton.Click
        Dim colorDialog As New ColorDialog With {
           .Color = Form1.ForeColor ' Set initial color to current Form1 background
           }
        If colorDialog.ShowDialog() = DialogResult.OK Then
            Form1.UpdateChildControlForeColors(Form1, colorDialog.Color)
            UpdateChildControlForeColors(Me, colorDialog.Color)

            ' Save the selected color to settings
            My.Settings.StringColor = colorDialog.Color
            'SetFontColor(sender, e)

        End If
    End Sub

    Private Sub SetFontColor(sender As Object, e As Drawing.FontStyle)
        ' Setzt die Schriftfarbe aller Controls im Dialog auf die in den Einstellungen gespeicherte Farbe
        Dim fontColor As Drawing.Color = My.Settings.StringColor
        Me.ForeColor = fontColor
        Form1.ForeColor = fontColor
        My.Settings.StringColor = fontColor
        UpdateChildControlForeColors(Me, fontColor)
    End Sub
    Private Sub SetControlColor(sender As Object, e As Drawing.FontStyle)
        ' Setzt die Hintergrundfarbe aller Controls im Dialog auf die in den Einstellungen gespeicherte Farbe
        Dim controlColor As Drawing.Color = My.Settings.AppColor
        Me.BackColor = controlColor
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
End Class
