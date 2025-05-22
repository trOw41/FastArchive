Imports System.ComponentModel
Imports System.Diagnostics
Imports System.Dynamic
Imports System.IO.Compression
Imports System.Threading
Imports Microsoft.VisualBasic.Logging
Imports Windows.Management.Deployment.Preview

Public Class Form1

    Private _isCancellationPending As Boolean = False
    Private WithEvents _backgroundWorker As BackgroundWorker ' Korrekt deklariert mit WithEvents
    Private AppName As String = "FastArchive"
    Private _zipFilePath As String = ""
    Private _extractPath As String = ""
    Private WithEvents _unzipWorker As BackgroundWorker



    Private Function EnsureAdminRightsAndCreateDirectory(directoryPath As String) As Boolean
        If Not IsUserAdministrator() Then
            ' Starte die Anwendung mit Admin-Rechten neu
            Dim currentProcessInfo As New ProcessStartInfo With {
                .FileName = Application.ExecutablePath,
                .Arguments = "",
                .Verb = "runas"
            }
            Try
                Process.Start(currentProcessInfo)
                Return True
            Catch ex As System.ComponentModel.Win32Exception When ex.NativeErrorCode = 1223
                MessageBox.Show("Administratorrechte sind erforderlich, um den Ordner zu erstellen.", "Berechtigung erforderlich", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return False
            Catch ex As Exception
                MessageBox.Show($"Fehler beim Anfordern von Administratorrechten: {ex.Message}", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return False
            End Try
            Return False
        Else
            Try
                If Not Directory.Exists(directoryPath) Then
                    Directory.CreateDirectory(directoryPath)
                End If
                Return True
            Catch ex As Exception
                MessageBox.Show($"Fehler beim Erstellen des Ordners: {ex.Message}", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return False
            End Try
        End If
    End Function

    Private Function IsUserAdministrator() As Boolean
        Dim identity As System.Security.Principal.WindowsIdentity = System.Security.Principal.WindowsIdentity.GetCurrent()
        Dim principal As New System.Security.Principal.WindowsPrincipal(identity)
        Return principal.IsInRole(System.Security.Principal.WindowsBuiltInRole.Administrator)
    End Function

#Const ShowToolTips = True
    Public Sub SetControlColor(sender As Object, e As Drawing.Color)
        Dim controlColor As Color = My.Settings.AppColor
        'Me.BackColor = controlColor
        My.Settings.ControlColor = controlColor
        UpdateChildControlBackColors(Me, controlColor)
    End Sub

    Public Shared Sub UpdateChildControlBackColors(control As Control, color As Color)
        For Each childControl As Control In control.Controls
            childControl.BackColor = color
            If childControl.HasChildren Then
                UpdateChildControlBackColors(childControl, color)
            End If
        Next
    End Sub

    Public Shared Sub UpdateChildControlForeColors(control As Control, color As Color)
        For Each childControl As Control In control.Controls
            childControl.ForeColor = color
            If childControl.HasChildren Then
                UpdateChildControlForeColors(childControl, color)
            End If
        Next
    End Sub
    Shared Function GetDefaultListViewForeColors() As Color
        Return SystemColors.WindowText
    End Function
    Shared Function GetDefaultListViewBackColors() As Color
        Return SystemColors.ControlLightLight
    End Function
    Public Sub GetListViewDefault()
        If My.Settings.StyleC = "Classic" Then
            FileList.BackColor = GetDefaultListViewBackColors()
            FileList.ForeColor = GetDefaultListViewForeColors()
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

    Private Sub OpenSettingsDialog()
        Using settingsDialog As New SettingsDialog()
            If settingsDialog.ShowDialog() = DialogResult.OK Then
            End If
        End Using
    End Sub

    Private Sub SelectButton_Click(sender As System.Object, e As System.EventArgs) Handles SelectButton.Click
        Using openFileDialog As New OpenFileDialog()
            openFileDialog.Multiselect = True
            openFileDialog.Title = "Dateien zum Archivieren auswählen"
            If openFileDialog.ShowDialog() = DialogResult.OK Then
                FileList.Items.Clear() 'Vorherige Einträge löschen
                For Each filename As String In openFileDialog.FileNames
                    Dim item As New ListViewItem With {
                        .Text = Path.GetFileName(filename),
                        .Tag = filename,
                        .Checked = False
                    }
                    Dim fileInfo As New FileInfo(filename)
                    Dim fileSize As String = FormatFileSize(fileInfo.Length)
                    Dim fileIcon As System.Drawing.Icon = System.Drawing.Icon.ExtractAssociatedIcon(filename)
                    Dim iconIndex As Integer = FileListIconList.Images.Count
                    FileListIconList.Images.Add(fileIcon)
                    item.ImageIndex = iconIndex
                    item.SubItems.Add(fileSize)
                    FileList.Items.Add(item)
                Next
                OpenArchiv.Enabled = False
                ItemNo.Text = "Files: " & FileList.Items.Count.ToString()
                UpdateTotalSizeLabel()
            End If
        End Using
    End Sub

    Private Sub StartButton_Click(sender As System.Object, e As System.EventArgs) Handles StartButton.Click
        If FileList.Items.Count = 0 Then
            MessageBox.Show("Bitte wählen Sie zuerst Dateien aus.", "Hinweis", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        Using saveFileDialog As New SaveFileDialog With {
            .Filter = If(ZipFormatButton.Checked, "ZIP-Datei (*.zip)|*.zip", "Alle Dateien (*.*)|*.*"),
            .Title = "Archiv speichern unter",
            .DefaultExt = If(ZipFormatButton.Checked, "zip", "")
        }

            If saveFileDialog.ShowDialog() = DialogResult.OK Then
                Dim archiveFilePath As String = saveFileDialog.FileName
                Dim selectedFilePaths As New List(Of String)()
                For Each item As ListViewItem In FileList.Items
                    If item.Checked Then
                        selectedFilePaths.Add(item.Tag)
                    End If
                Next

                'Prüfen ob Dateien ausgewählt wurden.
                If selectedFilePaths.Count = 0 Then
                    MessageBox.Show("Bitte wählen Sie Dateien zum Archivieren aus.", "Hinweis", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Return
                End If
                FileList.Visible = False
                ProgressBar1.Value = 0
                ProgressBar1.Visible = True
                StatusText.Visible = True
                StatusText.Text = $"Komprimiere nach: {Path.GetFileName(archiveFilePath)}"
                StartButton.Enabled = False
                SelectButton.Enabled = False
                ZipFormatButton.Enabled = False
                OpenArchiv.Enabled = False 'Disable OpenArchiv

                Dim parameters As New CompressionParameters With {
                    .ArchiveFilePath = archiveFilePath,
                    .FilePaths = selectedFilePaths.ToArray(),
                    .IsZip = ZipFormatButton.Checked
                }

                _isCancellationPending = False
                _backgroundWorker.RunWorkerAsync(parameters)
            End If
        End Using
    End Sub

    Private Sub BackgroundWorker_DoWork(sender As Object, e As DoWorkEventArgs) Handles _backgroundWorker.DoWork
        '_backgroundWorker = DirectCast(sender, BackgroundWorker)
        Dim parameters As CompressionParameters = DirectCast(e.Argument, CompressionParameters)

        Try
            Using archiveStream As New FileStream(parameters.ArchiveFilePath, FileMode.Create)
                Using archive As New ZipArchive(archiveStream, If(parameters.IsZip, ZipArchiveMode.Create, ZipArchiveMode.Create))
                    Dim totalFiles As Integer = parameters.FilePaths.Length
                    For i As Integer = 0 To totalFiles - 1
                        If _backgroundWorker.CancellationPending OrElse _isCancellationPending Then
                            e.Cancel = True
                            Exit For
                        End If
                        Dim progress As Integer = Math.Min(100, CInt((i + 1) / CDbl(totalFiles) * 100))
                        _backgroundWorker.ReportProgress(progress, New CompressionProgressInfo With {.ArchiveFilePath = parameters.ArchiveFilePath, .CurrentFile = parameters.FilePaths(i)})
                        Dim fileToAdd As String = parameters.FilePaths(i)
                        Dim entryName As String = Path.GetFileName(fileToAdd)

                        archive.CreateEntryFromFile(fileToAdd, entryName, CompressionLevel.Optimal)

                    Next
                    e.Result = parameters
                End Using
            End Using
        Catch ex As Exception
            e.Result = Nothing
            e.Cancel = True
            Throw
        End Try
    End Sub

    Private Sub BackgroundWorker_ProgressChanged(sender As Object, e As ProgressChangedEventArgs) Handles _backgroundWorker.ProgressChanged
        Dim progressInfo As CompressionProgressInfo = DirectCast(e.UserState, CompressionProgressInfo)
        Me.Invoke(Sub()

                      If TypeOf e.UserState Is CompressionProgressInfo Then

                          ProgressBar1.Value = e.ProgressPercentage
                          StatusText.Text = $"Komprimiere: {Path.GetFileName(progressInfo.CurrentFile)} ({e.ProgressPercentage}%)"
                      End If
                  End Sub)
    End Sub
    Private Sub BackgroundWorker_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs) Handles _backgroundWorker.RunWorkerCompleted
        StartButton.Enabled = True
        SelectButton.Enabled = True
        ZipFormatButton.Enabled = True
        OpenArchiv.Enabled = True 'Re-enable OpenArchiv
        ProgressBar1.Visible = False
        StatusText.Visible = False
        FileList.Visible = True
        Dim items As ListView.SelectedListViewItemCollection = FileList.SelectedItems
        If items.Count > 0 Then
            For Each item As ListViewItem In items
                item.Checked = False
            Next
        End If
        If e.Error IsNot Nothing Then
            MessageBox.Show($"Fehler beim Komprimieren: {e.Error.Message}", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error)
        ElseIf e.Cancelled Then
            MessageBox.Show("Komprimierung abgebrochen.", "Abgebrochen", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Else
            MessageBox.Show("Komprimierung abgeschlossen.", "Erfolg", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
        _isCancellationPending = False
    End Sub

    Private Class CompressionProgressInfo
        Public ArchiveFilePath As String
        Public CurrentFile As String
    End Class

    Private Class CompressionParameters
        Public ArchiveFilePath As String
        Public FilePaths As String()
        Public IsZip As Boolean
    End Class

    Private Function CalculateTotalSize() As Long
        Dim totalSize As Long = 0
        For Each item As ListViewItem In FileList.Items
            If item.Checked Then
                Try
                    Dim fileInfo As New FileInfo(item.Tag.ToString())
                    totalSize += fileInfo.Length
                Catch ex As Exception
                    MessageBox.Show($"Fehler beim Zugriff auf Datei: {item.Tag}. Sie wird bei der Größenberechnung ignoriert.", "Datei Fehler", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                End Try
            End If
        Next
        Return totalSize
    End Function

    Private Sub UpdateTotalSizeLabel()
        Dim totalSizeInBytes As Long = CalculateTotalSize()
        Dim formattedSize As String = FormatFileSize(totalSizeInBytes)
        SizeText.Text = $"Archiv Größe: {formattedSize}"
    End Sub

#Const ShowToolTips = True
    Private Sub FileList_SelectedIndexChanged(sender As Object, e As EventArgs) Handles FileList.SelectedIndexChanged
        If FileList.SelectedItems.Count > 0 Then
            Dim selectedItem As ListViewItem = FileList.SelectedItems(0)
            If Not selectedItem.Checked Then
                selectedItem.Checked = True
                selectedItem.Checked = FileList.SelectedItems.Count > 0
            End If
        End If
    End Sub

    Private Sub ItemNo_Click(sender As Object, e As EventArgs) Handles ItemNo.Click
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        FileList.Items.Clear()
        ItemNo.Text = "Einträge:"
        SizeText.Text = "Archiv Größe:"
        CheckBox1.Checked = False
        If UnZipButton.Visible Then
            UnZipButton.Visible = False
            StartButton.Enabled = True
            SelectButton.Enabled = True
            ZipFormatButton.Enabled = True
            OpenArchiv.Enabled = True
        End If

    End Sub

    Private Sub RemoveToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RemoveToolStripMenuItem.Click
        For Each item As ListViewItem In FileList.Items
            If item.Checked Then
                FileList.Items.Remove(item)
            End If
        Next
        ItemNo.Text = "Files: " & FileList.Items.Count.ToString()
        UpdateTotalSizeLabel()
    End Sub

    Private Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        Dim res As DialogResult = MessageBox.Show(Me, "Möchten Sie die Anwendung wirklich schließen?", "Beenden", MessageBoxButtons.OKCancel, MessageBoxIcon.Question)
        If res = DialogResult.OK Then
        Else
            e.Cancel = True
        End If
    End Sub

    Private Sub ExitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem.Click
        Me.Close()
    End Sub

    Private Sub InfoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles InfoToolStripMenuItem.Click
        About.Show()
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        ' Alle Items entsprechend der Checkbox an-/abwählen
        For Each item As ListViewItem In FileList.Items
            item.Checked = CheckBox1.Checked
            item.Selected = CheckBox1.Checked
        Next
    End Sub

    Private Sub FileList_ItemChecked(sender As Object, e As ItemCheckedEventArgs) Handles FileList.ItemChecked
        ' Wenn ein Item gecheckt wird, auch selektieren; wenn ungecheckt, deselektieren
        e.Item.Selected = e.Item.Checked
    End Sub

    Private Sub Form1_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Me.BackColor = My.Settings.AppColor
        Me.ForeColor = My.Settings.StringColor
        SetControlColor(sender, My.Settings.ControlColor)
        SetFormFont(My.Settings.AppFont)
        UpdateChildControlFonts(Me, My.Settings.AppFont)
        FileList.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent)
        FileList.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize)
        FileList.ForeColor = My.Settings.StringColor
        If My.Settings.AppFont IsNot Nothing AndAlso My.Settings.AppFont.FontFamily IsNot Nothing AndAlso
           Not String.IsNullOrEmpty(My.Settings.AppFont.FontFamily.Name) Then
            Try
                Dim savedFont As Font = My.Settings.AppFont
                Me.Font = savedFont
                UpdateChildControlFonts(Me, savedFont)
            Catch ex As Exception
                MessageBox.Show($"Fehler beim Laden der Schriftart: {ex.Message}", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
            'My.Settings.AppColor = DefaultBackColor
        End If
        IsUserAdministrator()
        Me.Text = AppName
        FileList.View = View.Details
        FileList.Columns.Add("Datei:", 250)
        FileList.Columns.Add("Größe:", 200)
        FileList.MultiSelect = True
        CheckBox1.Checked = False
        ZipFormatButton.Checked = True
        FileList.SmallImageList = FileListIconList
        ToolTip1.ShowAlways = True
        _backgroundWorker.WorkerReportsProgress = True
        _backgroundWorker.WorkerSupportsCancellation = True

    End Sub
    Public Sub New()
        InitializeComponent() 'Stelle sicher, dass die Komponente initialisiert wird
        _backgroundWorker = New BackgroundWorker With {
            .WorkerReportsProgress = True,
            .WorkerSupportsCancellation = True
        }
        _unzipWorker = New BackgroundWorker With {
            .WorkerReportsProgress = True,
            .WorkerSupportsCancellation = True
        }
    End Sub
    Private Sub OpenArchiv_Click(sender As Object, e As EventArgs) Handles OpenArchiv.Click
        Using openFileDialog As New OpenFileDialog()
            openFileDialog.Filter = "ZIP-Dateien (*.zip)|*.zip|Alle Dateien (*.*)|*.*"
            openFileDialog.Title = "ZIP-Datei zum Öffnen auswählen"
            FileList.Items.Clear()
            FileList.Columns.Clear()
            FileList.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent)
            FileList.AutoArrange = True
            FileList.View = View.Details
            FileList.Columns.Add("Datei:", 250)
            FileList.Columns.Add("Größe(in Archiv):", 130)
            If openFileDialog.ShowDialog() = DialogResult.OK Then
                _zipFilePath = openFileDialog.FileName
                Dim zipName As String = Path.GetFileName(_zipFilePath)
                Dim zippath As String = Path.GetDirectoryName(_zipFilePath)
                UnZipButton.Visible = True
                StartButton.Enabled = False
                SelectButton.Enabled = False
                ZipFormatButton.Enabled = False
                openFileDialog.Dispose()
                OpenArchiv.Enabled = False
                Try
                    Using zip As ZipArchive = ZipFile.Open(_zipFilePath, ZipArchiveMode.Read)
                        ItemNo.Text = "Dateien: " & zip.Entries.Count.ToString()
                        For Each zEntry As ZipArchiveEntry In zip.Entries
                            Dim fileName As String = zEntry.FullName
                            Dim entryFullPath As String = Path.Combine(zippath, fileName)
                            Dim item As New ListViewItem With {
                                .Text = fileName,
                                .Tag = entryFullPath,
                                .Checked = False
                            }
                            item.SubItems.Add(FormatFileSize(zEntry.CompressedLength))
                            Dim fileIcon As Icon = Nothing
                            Try
                                If File.Exists(entryFullPath) Then
                                    fileIcon = System.Drawing.Icon.ExtractAssociatedIcon(entryFullPath)
                                Else
                                    fileIcon = SystemIcons.WinLogo
                                End If
                            Catch ex As Exception
                                fileIcon = SystemIcons.WinLogo
                            End Try
                            Dim iconIndex As Integer = FileListIconList.Images.Count
                            FileListIconList.Images.Add(fileIcon)
                            item.ImageIndex = iconIndex
                            FileList.Items.Add(item)
                            UpdateTotalSizeLabel()
                        Next
                    End Using
                Catch ex As Exception
                    MessageBox.Show($"Fehler beim Öffnen des Archivs: {ex.Message}", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    If UnZipButton.Visible Then UnZipButton.Visible = False
                    If Not StartButton.Enabled Then StartButton.Enabled = True
                    If Not SelectButton.Enabled Then SelectButton.Enabled = True
                    If Not ZipFormatButton.Enabled Then ZipFormatButton.Enabled = True
                    If Not OpenArchiv.Enabled Then OpenArchiv.Enabled = True
                End Try
            End If
        End Using
    End Sub

    Private Sub UnZipButton_Click(sender As Object, e As EventArgs) Handles UnZipButton.Click
        FolderBrowserDialog1.Description = "Wählen Sie den Zielordner für die Entpackung aus"
        FolderBrowserDialog1.ShowNewFolderButton = True
        If FolderBrowserDialog1.ShowDialog() = DialogResult.OK Then
            If EnsureAdminRightsAndCreateDirectory(FolderBrowserDialog1.SelectedPath) = True Then
                If FileList.SelectedItems.Count > 0 Then
                    Dim selectedItem As ListView.SelectedListViewItemCollection = FileList.SelectedItems
                    Dim extractPath As String = FolderBrowserDialog1.SelectedPath
                    Dim unzipParams As New UnzipParameters With {
                        .ZipFilePath = _zipFilePath,
                        .ExtractPath = extractPath,
                        .SelectedItems = selectedItem
                    }
                    _unzipWorker.RunWorkerAsync(unzipParams)
                Else
                    MessageBox.Show("Bitte wählen Sie mindestens eine Datei aus.", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                End If
            Else
                MessageBox.Show("Administratorrechte sind erforderlich, um den Zielordner zu erstellen.", "Berechtigung erforderlich", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
        End If
    End Sub

    Private Sub UnzipWorker_DoWork(sender As Object, e As DoWorkEventArgs) Handles _unzipWorker.DoWork
        Dim worker As BackgroundWorker = DirectCast(sender, BackgroundWorker)
        Dim parameters As UnzipParameters = DirectCast(e.Argument, UnzipParameters)

        ' UI-Änderungen müssen im UI-Thread erfolgen
        Me.Invoke(Sub()
                      StartButton.Enabled = False
                      SelectButton.Enabled = False
                      ZipFormatButton.Enabled = False
                      OpenArchiv.Enabled = True
                      ProgressBar1.Visible = True
                      StatusText.Visible = True
                      FileList.Visible = False
                      OpenArchiv.Visible = False
                      StatusText.Visible = True
                  End Sub)

        Try
            Using archive As ZipArchive = ZipFile.Open(parameters.ZipFilePath, ZipArchiveMode.Read)
                Dim totalEntries As Integer = archive.Entries.Count
                Dim extractedEntriesCount As Integer = 0
                Dim selectedItemsCount As Integer

                ' Kopiere die ListViewItems in eine eigene Liste, um Threading-Probleme zu vermeiden
                Dim selectedFileNames As New List(Of String)()
                Me.Invoke(Sub()
                              For Each item As ListViewItem In CType(parameters.SelectedItems, ListView.SelectedListViewItemCollection)
                                  selectedFileNames.Add(item.Text)
                              Next
                          End Sub)
                selectedItemsCount = selectedFileNames.Count

                For Each entryName As String In selectedFileNames
                    If worker.CancellationPending OrElse _isCancellationPending Then
                        e.Cancel = True
                        Exit For
                    End If

                    Dim entry As ZipArchiveEntry = archive.GetEntry(entryName)

                    If entry IsNot Nothing Then
                        Dim destinationPath As String = Path.Combine(parameters.ExtractPath, entry.FullName)
                        Dim directoryPath As String = Path.GetDirectoryName(destinationPath)

                        If Not Directory.Exists(directoryPath) Then
                            Directory.CreateDirectory(directoryPath)
                        End If

                        entry.ExtractToFile(destinationPath, True)
                        extractedEntriesCount += 1
                        Dim progressPercentage As Integer = CInt((extractedEntriesCount / CDbl(selectedItemsCount)) * 100)

                        worker.ReportProgress(progressPercentage, entryName)
                    Else
                        worker.ReportProgress(0, $"Datei nicht gefunden: {entryName}")
                    End If
                Next
                e.Result = parameters.ExtractPath
            End Using
        Catch ex As Exception
            e.Cancel = True
            Throw
        End Try
    End Sub

    Private Sub UnzipWorker_ProgressChanged(sender As Object, e As ProgressChangedEventArgs) Handles _unzipWorker.ProgressChanged
        ProgressBar1.Value = e.ProgressPercentage
        StatusText.Text = $"Entpacke: {e.UserState} ({e.ProgressPercentage}%)"
    End Sub

    Private Class UnzipParameters
        Public ZipFilePath As String
        Public ExtractPath As String
        Friend SelectedItems As Object
    End Class

    Private Sub UnzipWorker_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs) Handles _unzipWorker.RunWorkerCompleted
        StartButton.Enabled = True
        SelectButton.Enabled = True
        ZipFormatButton.Enabled = True
        ProgressBar1.Visible = False
        StatusText.Visible = False
        FileList.Visible = True
        OpenArchiv.Visible = True
        UnZipButton.Visible = False
        StatusText.Visible = False
        If e.Cancelled Then
            StatusText.Text = "Entpacken abgebrochen."
            MessageBox.Show("Entpacken abgebrochen.", "Abgebrochen", MessageBoxButtons.OK, MessageBoxIcon.Information)
        ElseIf e.Error IsNot Nothing Then
            'StatusText.Text = "Fehler beim Entpacken."
            MessageBox.Show($"Fehler beim Entpacken: {e.Error.Message}", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Else
            'StatusText.Text = "Entpacken abgeschlossen!"
            MessageBox.Show($"Archiv erfolgreich entpackt nach: {e.Result}", "Erfolg")
        End If
        _isCancellationPending = False
    End Sub

    Private Sub OptionsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OptionsToolStripMenuItem.Click
        SettingsDialog.ShowDialog()
    End Sub

    Public Shared Sub Form1BackColor(sender As Object, it As Drawing.Color)
        Dim form As Form1 = DirectCast(sender, Form1)
        form.BackColor = it
    End Sub

    Public Shared Sub Form1Font(dialog1 As SettingsDialog, font As Font)
        Form1.Font = font
        For Each ctrl As Control In Form1.Controls
            ctrl.Font = font
        Next
    End Sub

    Private Function FormatFileSize(bytes As Long) As String
        If bytes < 1024 Then
            Return $"{bytes} Bytes"
        ElseIf bytes < 1024 * 1024 Then
            Return $"{Math.Round(bytes / 1024.0, 2)} KB"
        ElseIf bytes < 1024 * 1024 * 1024 Then
            Return $"{Math.Round(bytes / (1024.0 * 1024), 2)} MB"
        Else
            Return $"{Math.Round(bytes / (1024.0 * 1024 * 1024), 2)} GB"
        End If
    End Function

    Private Sub ToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem1.Click
        SelectButton_Click(sender, e)
    End Sub
    ' Drag & Drop Event Handler

    Private Sub FileList_DragDrop(sender As Object, e As DragEventArgs) Handles MyBase.DragDrop, FileList.DragDrop
        Dim files = CType(e.Data.GetData(DataFormats.FileDrop), String())

        ' Überprüfen, ob eine einzelne ZIP-Datei gezogen wurde
        If files.Length = 1 AndAlso files(0).ToLower.EndsWith(".zip") Then
            Dim zipFilePath = files(0)
            ' Setze _zipFilePath für OpenArchiv_Click
            _zipFilePath = zipFilePath
            Dim zipName As String = Path.GetFileName(_zipFilePath)
            Dim zippath As String = Path.GetDirectoryName(_zipFilePath)
            UnZipButton.Visible = True
            StartButton.Enabled = False
            SelectButton.Enabled = False
            ZipFormatButton.Enabled = False
            OpenArchiv.Enabled = False
            Try
                Using zip As ZipArchive = ZipFile.Open(_zipFilePath, ZipArchiveMode.Read)
                    ItemNo.Text = "Dateien: " & zip.Entries.Count.ToString()
                    For Each zEntry As ZipArchiveEntry In zip.Entries
                        Dim fileName As String = zEntry.FullName
                        Dim entryFullPath As String = Path.Combine(zippath, fileName)
                        Dim item As New ListViewItem With {
                            .Text = fileName,
                            .Tag = entryFullPath,
                            .Checked = False
                        }
                        item.SubItems.Add(FormatFileSize(zEntry.CompressedLength))
                        Dim fileIcon As Icon = Nothing
                        Try
                            If File.Exists(entryFullPath) Then
                                fileIcon = System.Drawing.Icon.ExtractAssociatedIcon(entryFullPath)
                            Else
                                fileIcon = SystemIcons.WinLogo
                            End If
                        Catch ex As Exception
                            fileIcon = SystemIcons.WinLogo
                        End Try
                        Dim iconIndex As Integer = FileListIconList.Images.Count
                        FileListIconList.Images.Add(fileIcon)
                        item.ImageIndex = iconIndex
                        FileList.Items.Add(item)
                        UpdateTotalSizeLabel()
                    Next
                End Using
            Catch ex As Exception
                MessageBox.Show($"Fehler beim Öffnen des Archivs: {ex.Message}", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        Else
            ' Füge die gezogenen Dateien zur Liste hinzu
            For Each filePath In files
                Dim item As New ListViewItem With {
                    .Text = Path.GetFileName(filePath),
                    .Tag = filePath,
                    .Checked = False
                }
                Dim fileInfo As New FileInfo(filePath)
                Dim fileSize = FormatFileSize(fileInfo.Length)
                Dim fileIcon = Icon.ExtractAssociatedIcon(filePath)
                Dim iconIndex = FileListIconList.Images.Count
                FileListIconList.Images.Add(fileIcon)
                item.ImageIndex = iconIndex
                item.SubItems.Add(fileSize)
                FileList.Items.Add(item)
            Next
        End If

    End Sub

    Private Sub FileList_DragEnter(sender As Object, e As DragEventArgs) Handles FileList.DragEnter
        ' Prüfen, ob die gezogenen Daten Dateipfade sind
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            e.Effect = DragDropEffects.Copy ' Zeige an, dass Kopieren erlaubt ist
        Else
            e.Effect = DragDropEffects.None ' Keine anderen Daten akzeptieren
        End If
    End Sub
End Class
