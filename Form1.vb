Imports System.ComponentModel
Imports System.Drawing
Imports System.IO
Imports System.Threading
Imports System.Windows.Forms

Public Class Form1


    Private _isCancellationPending As Boolean = False
    Private _backgroundWorker As BackgroundWorker
    Private AppName As String = "FastArchive"
    Private _extractPath As String

    ' Public method to set the form's font
    Public Sub SetFormFont(newFont As Font)
        Me.Font = newFont
        ' You might need to update the font of child controls as well
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
        Using settingsDialog As New Dialog1()
            If settingsDialog.ShowDialog() = DialogResult.OK Then

            End If
        End Using
    End Sub

    Private Sub SelectButton_Click(sender As System.Object, e As System.EventArgs) Handles SelectButton.Click
        Using openFileDialog As New OpenFileDialog()
            openFileDialog.Multiselect = True
            openFileDialog.Title = "Dateien zum Archivieren auswählen"
            If openFileDialog.ShowDialog() = DialogResult.OK Then
                For Each filename As String In openFileDialog.FileNames
                    Dim item As New ListViewItem With {
                        .Text = Path.GetFileName(filename), ' Nur den Dateinamen anzeigen
                        .Tag = filename,  ' **Vollständigen Pfad im Tag speichern**
                        .Checked = False
                    }
                    Dim fileInfo As New FileInfo(filename)
                    Dim fileSize As String = FormatFileSize(fileInfo.Length)
                    ' Get the file's icon
                    Dim fileIcon As System.Drawing.Icon = System.Drawing.Icon.ExtractAssociatedIcon(filename)
                    Dim iconIndex As Integer = FileListIconList.Images.Count
                    FileListIconList.Images.Add(fileIcon)
                    item.ImageIndex = iconIndex
                    item.SubItems.Add(fileSize) ' Größe hinzufügen
                    FileList.Items.Add(item)
                    CheckBox1.Checked = True
                    OpenArchiv.Enabled = False
                    ItemNo.Text = "Files: " & FileList.Items.Count.ToString()
                Next
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

                ' **Nur ausgewählte Dateien abrufen**
                Dim selectedFilePaths As New List(Of String)()
                For Each item As ListViewItem In FileList.Items
                    If item.Checked Then
                        selectedFilePaths.Add(item.Tag) ' Pfad ist im Tag
                    End If
                Next


                ' Neues Form2-Instanz erstellen und anzeigen
                ' Private _compressionForm As Form2 = Nothing
                '_compressionForm = New Form2()
                StatusText.Text = $"Komprimiere nach: {Path.GetFileName(archiveFilePath)}"
                ' ProgressBar1 initialisieren
                FileList.Visible = False
                ProgressBar1.Visible = True
                ProgressBar1.Value = 0
                StatusText.Visible = True

                ' UI-Elemente in Form1 deaktivieren
                StartButton.Enabled = False
                SelectButton.Enabled = False
                ZipFormatButton.Enabled = False

                ' Parameter für den BackgroundWorker übergeben
                Dim parameters As New CompressionParameters With {
                    .ArchiveFilePath = archiveFilePath,
                    .FilePaths = selectedFilePaths.ToArray(), ' Übergabe des String-Arrays der *ausgewählten* Dateien
                    .IsZip = ZipFormatButton.Checked
                }

                _isCancellationPending = False

                _backgroundWorker.RunWorkerAsync(parameters)
            End If
        End Using
    End Sub

    Private Sub BackgroundWorker_DoWork(sender As Object, e As DoWorkEventArgs)
        Dim worker As BackgroundWorker = DirectCast(sender, BackgroundWorker)
        Dim parameters As CompressionParameters = DirectCast(e.Argument, CompressionParameters)

        Try
            If parameters.IsZip Then
                CreateZipArchiveWithProgress(parameters.ArchiveFilePath, parameters.FilePaths, worker, e) ' Nur ZIP
            Else

                Throw New NotImplementedException("Nur ZIP-Archivierung wird unterstützt.")
            End If

            If _isCancellationPending Then
                e.Cancel = True
            End If

        Catch ex As Exception
            e.Result = ex
        End Try
    End Sub

    Private Sub BackgroundWorker_ProgressChanged(sender As Object, e As ProgressChangedEventArgs)

        ProgressBar1.Value = e.ProgressPercentage
        If TypeOf e.UserState Is CompressionParameters Then
            StatusText.Text = $"Komprimiere nach: {Path.GetFileName(DirectCast(e.UserState, CompressionParameters).ArchiveFilePath)} ({e.ProgressPercentage}%)"
        Else
            StatusText.Text = $"Komprimiere... ({e.ProgressPercentage}%)"
        End If
    End Sub

    Private Sub BackgroundWorker_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs)
        Dim resultParams As CompressionParameters = DirectCast(e.Result, CompressionParameters)
        ProgressBar1.Visible = False
        StatusText.Visible = False
        StartButton.Enabled = True
        SelectButton.Enabled = True
        ZipFormatButton.Enabled = True
        OpenArchiv.Enabled = True
        FileList.Visible = True
        FileList.Enabled = True
        For Each item As ListViewItem In FileList.Items
            item.Checked = False
        Next
        FileList.Items.Clear()

        FileList.Items.Add(resultParams.ArchiveFilePath)
        FileList.Items.AddRange(resultParams.ArchiveFilePath.Select(Function(filePath) New ListViewItem With {
            .Text = Path.GetFileName(filePath),
            .Tag = filePath,
            .Checked = False
        }).ToArray())
        SizeText.Text = "Archiv Größe:: 0 Bytes"
        ItemNo.Text = "Einträge: 0"
        ProgressBar1.Visible = False
        StatusText.Text = "Fertig!"
        If e.Cancelled Then
            MessageBox.Show("Archivierung abgebrochen.", "Abgebrochen", MessageBoxButtons.OK, MessageBoxIcon.Information)
        ElseIf e.Error IsNot Nothing Then
            MessageBox.Show($"Fehler bei der Archivierung: {e.Error.Message}", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Else
            If TypeOf e.Result Is CompressionParameters Then

                'MessageBox.Show($"Archiv erfolgreich erstellt: {resultParams.ArchiveFilePath}", "Erfolg", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                MessageBox.Show("Archivierung erfolgreich, aber Ergebnis ist unerwartet.", "Erfolg", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

        End If

        _isCancellationPending = False
    End Sub
    Private Sub UnzipArchives(ByRef zipFilePath As String, ByRef extractPath As String, ByRef worker As BackgroundWorker, ByRef e As DoWorkEventArgs)
        Try
            Using archive As ZipArchive = ZipFile.OpenRead(zipFilePath)
                Dim totalEntries As Integer = archive.Entries.Count
                For i As Integer = 0 To totalEntries - 1
                    If worker.CancellationPending OrElse _isCancellationPending Then
                        e.Cancel = True
                        Exit For
                    End If
                    Dim entry As ZipArchiveEntry = archive.Entries(i)
                    Dim entryName As String = entry.FullName
                    Dim destinationPath As String = Path.Combine(extractPath, entryName)
                    ' Create directory if it doesn't exist
                    Directory.CreateDirectory(Path.GetDirectoryName(destinationPath))
                    ' Extract the file
                    entry.ExtractToFile(destinationPath, True)
                    ' Report progress
                    Dim progress As Integer = Math.Min(100, CInt((i + 1) / CDbl(totalEntries) * 100))
                    worker.ReportProgress(progress, New UnzipProgressInfo With {.EntryName = entryName, .ExtractPath = extractPath})
                Next
                e.Result = extractPath
            End Using
        Catch ex As Exception
            e.Result = Nothing
            Throw
        End Try
    End Sub
    Private Sub CreateZipArchiveWithProgress(zipFilePath As String, filePaths As String(), worker As BackgroundWorker, e As DoWorkEventArgs)
        Try
            'Debug.WriteLine("CreateZipArchiveWithProgress aufgerufen")
            For Each filePath As String In filePaths
                ' MessageBox.Show($"Archiviere: {filePath}")
            Next
            Using archiveStream As New FileStream(zipFilePath, FileMode.Create)
                Using archive As New ZipArchive(archiveStream, ZipArchiveMode.Create)
                    Dim totalFiles As Integer = filePaths.Length
                    For i As Integer = 0 To totalFiles - 1
                        If worker.CancellationPending OrElse _isCancellationPending Then
                            e.Cancel = True
                            Exit For
                        End If

                        ' Calculate progress
                        Dim progress As Integer = Math.Min(100, CInt((i + 1) / CDbl(totalFiles) * 100))
                        worker.ReportProgress(progress, New CompressionParameters With {.ArchiveFilePath = zipFilePath})

                        ' Add each file to the archive
                        Dim fileToAdd As String = filePaths(i) ' Vollständigen Pfad verwenden
                        Dim entryName As String = Path.GetFileName(fileToAdd) ' Nur den Dateinamen für den Eintrag verwenden!


                        'archive.CreateEntryFromFile(fileToAdd, fileToAdd, entryName)
                        archive.CreateEntryFromFile(fileToAdd, entryName, CompressionLevel.SmallestSize)


                        'archive.CreateEntryFromFile(filePaths(i), fileToAdd, entryName) ' Korrekte Überladung verwenden

                    Next

                    e.Result = New CompressionParameters With {.ArchiveFilePath = zipFilePath, .FilePaths = filePaths, .IsZip = True}

                End Using
            End Using

        Catch ex As Exception
            e.Result = Nothing
            Throw
        End Try

    End Sub
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
                    Dim fileInfo As New FileInfo(item.Tag)  ' Hier Tag verwenden!
                    totalSize += fileInfo.Length
                Catch ex As Exception
                    ' Fehlerbehandlung
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

    'Hilfsfunktion zur Formatierung der Dateigröße (z.B. in KB, MB, GB)
    Private Function FormatFileSize(ByVal bytes As Long) As String
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
    Private Sub FileList_SelectedIndexChanged(sender As Object, e As EventArgs) Handles FileList.SelectedIndexChanged
        UpdateTotalSizeLabel()
    End Sub

    Private Sub ItemNo_Click(sender As Object, e As EventArgs) Handles ItemNo.Click

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        FileList.Items.Clear()
        ItemNo.Text = "Einträge: 0"
        SizeText.Text = "Archiv Größe:: 0 Bytes"
        CheckBox1.Checked = False
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
            ' Der Benutzer möchte schließen - nichts weiter tun
        Else
            ' Der Benutzer möchte NICHT schließen - Schließen abbrechen
            e.Cancel = True
        End If
    End Sub
    Private Sub ExitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem.Click
        Application.Exit()
    End Sub

    Private Sub InfoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles InfoToolStripMenuItem.Click
        About.Show()
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        For Each item As ListViewItem In FileList.Items
            If Not CheckBox1.Checked = False Then

                item.Checked = True

            Else
                item.Checked = False
            End If
        Next
    End Sub

    Private _unzipWorker As BackgroundWorker

    Private Sub Form1_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

        ' Load the saved background color
        Me.BackColor = My.Settings.AppColor

        ' Load the saved font family
        If Not String.IsNullOrEmpty(My.Settings.AppFont.FontFamily.Name) Then
            Try
                Dim fontConverter As New FontConverter()
                Dim savedFont As Font = CType(fontConverter.ConvertFromString(My.Settings.AppFont.FontFamily.Name), Font)
                Me.Font = savedFont
                UpdateChildControlFonts(Me, savedFont)
            Catch ex As Exception
                MessageBox.Show($"Fehler beim Laden der Schriftart: {ex.Message}", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error)
                ' Optional: Standard-Schriftart setzen, falls das Laden fehlschlägt
            End Try
        End If

        Me.Text = AppName
        ' Konfiguration der ListView mit Checkboxen
        FileList.View = View.Details
        FileList.Columns.Add("Datei:", 250)
        FileList.Columns.Add("Größe:", 100)
        FileList.MultiSelect = True
        CheckBox1.Checked = False
        ' Standardmäßig ZIP-Format auswählen
        ZipFormatButton.Checked = True
        FileList.SmallImageList = FileListIconList
        ' Konfiguration des BackgroundWorker
        _backgroundWorker = New BackgroundWorker With {
            .WorkerReportsProgress = True,
            .WorkerSupportsCancellation = True
        }
        AddHandler _backgroundWorker.DoWork, AddressOf BackgroundWorker_DoWork
        AddHandler _backgroundWorker.ProgressChanged, AddressOf BackgroundWorker_ProgressChanged
        AddHandler _backgroundWorker.RunWorkerCompleted, AddressOf BackgroundWorker_RunWorkerCompleted
        _unzipWorker = New BackgroundWorker With {
            .WorkerReportsProgress = True,
            .WorkerSupportsCancellation = True
        }
        AddHandler _unzipWorker.DoWork, AddressOf UnzipWorker_DoWork
        AddHandler _unzipWorker.ProgressChanged, AddressOf UnzipWorker_ProgressChanged
        AddHandler _unzipWorker.RunWorkerCompleted, AddressOf UnzipWorker_RunWorkerCompleted
    End Sub

    ' 3. Entpacken asynchron starten:
    Private Sub OpenArchiv_Click(sender As Object, e As EventArgs) Handles OpenArchiv.Click
        Using openFileDialog As New OpenFileDialog()
            openFileDialog.Filter = "ZIP-Dateien (*.zip)|*.zip|Alle Dateien (*.*)|*.*"
            openFileDialog.Title = "ZIP-Datei zum Öffnen auswählen"
            If openFileDialog.ShowDialog() = DialogResult.OK Then
                Dim zipFilePath As String = openFileDialog.FileName
                Try
                    If FolderBrowserDialog1.ShowDialog() = DialogResult.OK Then
                        _extractPath = FolderBrowserDialog1.SelectedPath
                        ProgressBar1.Value = 0
                        ProgressBar1.Visible = True
                        StatusText.Text = "Starte Entpacken..."
                        StartButton.Enabled = False
                        SelectButton.Enabled = False
                        ZipFormatButton.Enabled = False
                        OpenArchiv.Enabled = False
                        Dim unzipParams As New UnzipParameters With {
                            .ZipFilePath = zipFilePath,
                            .ExtractPath = _extractPath
                        }
                        _unzipWorker.RunWorkerAsync(unzipParams)
                    End If
                Catch ex As Exception
                    MessageBox.Show($"Fehler: {ex.Message}", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            End If
        End Using
    End Sub

    ' 4. Parameterklasse für Entpacken:
    Private Class UnzipParameters
        Public Property ZipFilePath As String
        Public Property ExtractPath As String
    End Class

    ' 5. DoWork-Handler für Entpacken:
    Private Sub UnzipWorker_DoWork(sender As Object, e As DoWorkEventArgs)
        Dim worker As BackgroundWorker = DirectCast(sender, BackgroundWorker)
        Dim parameters As UnzipParameters = DirectCast(e.Argument, UnzipParameters)
        Try
            Using archive As ZipArchive = ZipFile.OpenRead(parameters.ZipFilePath)
                Dim totalEntries As Integer = archive.Entries.Count
                For i As Integer = 0 To totalEntries - 1
                    If worker.CancellationPending OrElse _isCancellationPending Then
                        e.Cancel = True
                        Exit For
                    End If
                    Dim entry As ZipArchiveEntry = archive.Entries(i)
                    Dim entryName As String = entry.FullName
                    Dim destinationPath As String = Path.Combine(parameters.ExtractPath, entryName)
                    Directory.CreateDirectory(Path.GetDirectoryName(destinationPath))
                    entry.ExtractToFile(destinationPath, True)
                    Dim progress As Integer = Math.Min(100, CInt((i + 1) / CDbl(totalEntries) * 100))
                    worker.ReportProgress(progress, entryName)
                Next
                e.Result = parameters.ExtractPath
            End Using
        Catch ex As Exception
            e.Result = Nothing
            Throw
        End Try
    End Sub

    ' 6. ProgressChanged-Handler für Entpacken:
    Private Sub UnzipWorker_ProgressChanged(sender As Object, e As ProgressChangedEventArgs)
        ProgressBar1.Value = e.ProgressPercentage
        StatusText.Text = $"Entpacke: {e.UserState} ({e.ProgressPercentage}%)"
    End Sub

    ' 7. RunWorkerCompleted-Handler für Entpacken:
    Private Sub UnzipWorker_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs)
        StartButton.Enabled = True
        SelectButton.Enabled = True
        ZipFormatButton.Enabled = True
        OpenArchiv.Enabled = True
        ProgressBar1.Visible = False
        If e.Cancelled Then
            StatusText.Text = "Entpacken abgebrochen."
            MessageBox.Show("Entpacken abgebrochen.", "Abgebrochen", MessageBoxButtons.OK, MessageBoxIcon.Information)
        ElseIf e.Error IsNot Nothing Then
            StatusText.Text = "Fehler beim Entpacken."
            MessageBox.Show($"Fehler beim Entpacken: {e.Error.Message}", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Else
            StatusText.Text = "Entpacken abgeschlossen!"
            MessageBox.Show($"Archiv erfolgreich entpackt nach: {e.Result}", "Erfolg", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
        _isCancellationPending = False
    End Sub
    Private Sub ExtractZipFile()

    End Sub

    Private Class UnzipProgressInfo
        Public Property EntryName As String
        Public Property ExtractPath As String
    End Class

    Private Sub OptionsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OptionsToolStripMenuItem.Click
        Dialog1.ShowDialog()
    End Sub

    Public Shared Sub Form1BackColor(sender As Object, it As Drawing.Color)
        Dim form As Form1 = DirectCast(sender, Form1)
        form.BackColor = it
    End Sub
    Public Shared Sub Form1Font(dialog1 As Dialog1, font As Font)
        Form1.Font = font
        ' Schriftart auch auf alle Controls anwenden
        For Each ctrl As Control In Form1.Controls
            ctrl.Font = font
        Next
    End Sub

    Private Sub ToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem1.Click
        SelectButton_Click(sender, e)
    End Sub

    Private Sub ToolTip1_Popup(sender As Object, e As PopupEventArgs) Handles ToolTip1.Popup
        If True Then
            ToolTip1.SetToolTip(FileList, "Hier werden die Dateien zum archivieren angezeigt..")
            ToolTip1.SetToolTip(SelectButton, "Dateien zum Archivieren auswählen")
            ToolTip1.SetToolTip(StartButton, "Archiv erstellen")
            ToolTip1.SetToolTip(OpenArchiv, "Archiv entpacken")
            ToolTip1.SetToolTip(ZipFormatButton, "Archiv im Zip-Format erstellen")
            ToolTip1.SetToolTip(CheckBox1, "Alle Dateien auswählen")
        End If
    End Sub

    Private Sub SelectButton_MouseEnter(sender As Object, e As EventArgs) Handles SelectButton.MouseEnter
        ToolTip1.SetToolTip(SelectButton, "Dateien zum Archivieren auswählen")
    End Sub
End Class

Namespace FastArchiver
    Friend Structure Resources
    End Structure
End Namespace
