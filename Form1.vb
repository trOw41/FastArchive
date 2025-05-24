Imports System.ComponentModel
Imports System.IO.Path
' Imports ABI.Windows.Globalization.NumberFormatting ' Nicht direkt in diesem Kontext verwendet, kann entfernt werden, wenn keine anderen WinRT-Abhängigkeiten bestehen
' Imports System.Runtime.ExceptionServices ' Nicht direkt in diesem Kontext verwendet, kann entfernt werden
' Imports System.Runtime.InteropServices.Marshalling ' Nicht direkt in diesem Kontext verwendet, kann entfernt werden
' Imports WinRT ' Nicht direkt in diesem Kontext verwendet, kann entfernt werden
' Imports Syncfusion.XForms ' Nicht direkt in diesem Kontext verwendet, kann entfernt werden, wenn keine Syncfusion-Komponenten verwendet werden

Public Class Form1

    Private _isCancellationPending As Boolean = False
    Private WithEvents _backgroundWorker As BackgroundWorker
    Private AppName As String = "FastArchiver"
    Private _zipFilePath As String = ""
    Private _extractPath As String = ""
    Private WithEvents _unzipWorker As BackgroundWorker

    ' Private Shared Deklaration für Icons auf Klassenebene
    Private Shared _folderIcon As Icon
    Private Shared _fileIcon As Icon
    ' Entfernen Sie 'Private dirIcon As Icon', da _folderIcon dafür verwendet wird

    Public Sub New()
        InitializeComponent()
        _backgroundWorker = New BackgroundWorker With {
            .WorkerReportsProgress = True,
            .WorkerSupportsCancellation = True
        }
        _unzipWorker = New BackgroundWorker With {
            .WorkerReportsProgress = True,
            .WorkerSupportsCancellation = True
        }
    End Sub

    Private Function EnsureAdminRightsAndCreateDirectory(directoryPath As String) As Boolean
        If Not IsUserAdministrator() Then
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
        Me.Invoke(Sub()
                      OpenArchiv.Enabled = False
                  End Sub)
        Using openFileDialog As New OpenFileDialog()
            openFileDialog.Multiselect = True
            openFileDialog.Title = "Dateien zum Archivieren auswählen"

            If openFileDialog.ShowDialog() = DialogResult.OK Then
                Dim droppedPaths As String() = openFileDialog.FileNames
                If droppedPaths.Length = 1 AndAlso droppedPaths(0).ToLower().EndsWith(".zip") Then
                    Dim zipFilePath As String = droppedPaths(0)
                    _zipFilePath = zipFilePath ' Speichere den Pfad für den Unzip-Vorgang
                    UnZipButton.Visible = True ' Unzip-Button sichtbar machen
                    StartButton.Visible = False
                    Panel_0.Visible = False

                    Try
                        FileList.Items.Clear() ' Vorherige Einträge löschen
                        FileListIconList.Images.Clear() ' Icons leeren

                        Using zip As ZipArchive = ZipFile.Open(zipFilePath, ZipArchiveMode.Read)
                            ItemNo.Text = "Datei: " & zip.Entries.Count.ToString()

                            For Each zEntry As ZipArchiveEntry In zip.Entries
                                ' Verwenden Sie AddZipEntryToListView für die gemeinsame Logik
                                AddZipEntryToListView(zEntry)
                            Next
                            FileList.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize)
                            FileList.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent)
                        End Using

                        UpdateTotalSizeLabel()
                    Catch ex As Exception
                        MessageBox.Show($"Fehler beim Öffnen des Archivs: {ex.Message}{Environment.NewLine}{ex.GetType().Name}: {ex.StackTrace}", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error)

                    End Try
                Else
                    For Each droppedPath As String In droppedPaths
                        ' Vereinfachte Logik: AddFileOrDirectoryToListView handhabt beides
                        AddFileOrDirectoryToListView(droppedPath)
                    Next

                    ItemNo.Text = "Files: " & FileList.Items.Count.ToString()
                    UpdateTotalSizeLabel()
                    FileList.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize)
                    FileList.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent)
                End If
            End If
        End Using
    End Sub

    Private Sub StartButton_Click(sender As System.Object, e As System.EventArgs) Handles StartButton.Click
        If FileList.Items.Count = 0 Then
            MessageBox.Show("Bitte wählen Sie zuerst Dateien aus.", "Hinweis", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If
        OpenArchiv.Enabled = True
        Using saveFileDialog As New SaveFileDialog With {
            .Filter = If(ZipFormatButton.Checked, "ZIP-Datei (*.zip)|*.zip", "Alle Dateien (*.*)|*.*") &
                      If(RARFormatButton.Checked, "|RAR-Datei (*.rar)|*.rar", ""), ' Filter für RAR hinzufügen, falls ausgewählt
            .Title = "Archiv speichern unter",
            .DefaultExt = If(ZipFormatButton.Checked, "zip", "")
        }

            If saveFileDialog.ShowDialog() = DialogResult.OK Then
                Dim archiveFilePath As String = saveFileDialog.FileName
                Dim selectedFilePaths As New List(Of String)()
                For Each item As ListViewItem In FileList.Items
                    If item.Checked Then
                        selectedFilePaths.Add(item.Tag.ToString()) ' Tag ist jetzt ein String
                    End If
                Next

                If selectedFilePaths.Count = 0 Then
                    MessageBox.Show("Bitte wählen Sie Dateien zum Archivieren aus.", "Hinweis", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Return
                End If
                Me.Invoke(Sub()
                              FileList.Visible = False
                              ProgressBar1.Value = 0
                              ProgressBar1.Visible = True
                              StatusText.Visible = True
                              Label3.Visible = True
                              StatusText.Text = $"Komprimiere nach: {Path.GetFileName(archiveFilePath)}"
                              Button1.Visible = False
                              CheckBox1.Visible = False
                              Panel_0.Visible = False
                              Panel_2.Visible = False
                              MenuStrip1.Enabled = False
                              Me.Size = New Drawing.Size(Me.Size.Width, Me.Size.Height - 250)
                          End Sub)

                _isCancellationPending = False
                Dim parameters As New CompressionParameters With {
                    .ArchiveFilePath = archiveFilePath,
                    .FilePaths = selectedFilePaths.ToArray(),
                    .IsZip = ZipFormatButton.Checked,
                    .IsRar = RARFormatButton.Checked
                    }
                _backgroundWorker.RunWorkerAsync(parameters)

            End If
        End Using
    End Sub

    Private Sub BackgroundWorker_DoWork(sender As Object, e As DoWorkEventArgs) Handles _backgroundWorker.DoWork
        Dim worker As BackgroundWorker = DirectCast(sender, BackgroundWorker)
        Dim parameters As CompressionParameters = DirectCast(e.Argument, CompressionParameters)

        Try
            Using archiveStream As New FileStream(parameters.ArchiveFilePath, FileMode.Create)
                ' Der ZipArchiveMode sollte immer Create sein, wenn ein neues Archiv erstellt wird.
                ' Die If-Bedingung ist hier redundant, da sie immer ZipArchiveMode.Create ergibt.
                Using archive As New ZipArchive(archiveStream, ZipArchiveMode.Create)
                    Dim totalFiles As Integer = parameters.FilePaths.Length
                    For i As Integer = 0 To totalFiles - 1
                        If worker.CancellationPending OrElse _isCancellationPending Then
                            e.Cancel = True
                            Exit For
                        End If
                        Dim progress As Integer = Math.Min(100, CInt((i + 1) / CDbl(totalFiles) * 100))
                        Dim fileToAdd As String = parameters.FilePaths(i)
                        Dim entryName As String = Path.GetFileName(fileToAdd)

                        ' Hinzufügen der verstrichenen Zeit zum ProgressInfo
                        Dim stopwatch As New Stopwatch()
                        stopwatch.Start()
                        archive.CreateEntryFromFile(fileToAdd, entryName, My.Settings.CompressionMode)
                        stopwatch.Stop()
                        worker.ReportProgress(progress, New CompressionProgressInfo With {.ArchiveFilePath = parameters.ArchiveFilePath, .CurrentFile = parameters.FilePaths(i), .ElapsedTime = stopwatch.Elapsed})
                    Next
                    e.Result = parameters ' Das Ergebnis sollte die Parameter für die Anzeige sein
                End Using
            End Using

        Catch ex As Exception
            e.Result = Nothing
            e.Cancel = True
            Throw ' Exception weiterleiten, damit sie von RunWorkerCompleted als e.Error behandelt werden kann
        End Try
    End Sub

    Private Sub BackgroundWorker_ProgressChanged(sender As Object, e As ProgressChangedEventArgs) Handles _backgroundWorker.ProgressChanged
        Dim progressInfo As CompressionProgressInfo = TryCast(e.UserState, CompressionProgressInfo)

        Dim mode As Integer = My.Settings.CompressionMode
        Dim modename As String = ""

        If mode = 0 Then
            modename = "Standard (optimal)"
        ElseIf mode = 1 Then
            modename = "Fast (schnell)"
        ElseIf mode = 2 Then ' Hinzugefügt: Für den Fall, dass Modus 2 für "Ultra" steht
            modename = "Ultra (langsam)"
        Else
            modename = "Unbekannt" ' Fallback für unbekannten Modus
        End If

        If progressInfo IsNot Nothing Then ' Überprüfen, ob progressInfo nicht Nothing ist
            ProgressBar1.Value = e.ProgressPercentage
            StatusText.Text = $"Komprimiere: {Path.GetFileName(progressInfo.CurrentFile)} ({e.ProgressPercentage}%)"
            Label3.Text = "Mode: " & modename

            If e.ProgressPercentage > 0 Then
                Dim elapsedSeconds As Double = progressInfo.ElapsedTime.TotalSeconds
                Dim totalEstimatedSeconds As Double = (elapsedSeconds / e.ProgressPercentage) * 100
                Dim remainingSeconds As Double = totalEstimatedSeconds - elapsedSeconds
                Label4.Text = $"Verbleibend: {FormatTimeSpan(TimeSpan.FromSeconds(remainingSeconds))}"
            Else
                Label4.Text = "Berechne Zeit..."
            End If
        End If
    End Sub

    Private Sub BackgroundWorker_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs) Handles _backgroundWorker.RunWorkerCompleted
        Me.Invoke(Sub()
                      Panel_0.Visible = True
                      Panel_2.Visible = True
                      Button1.Visible = True
                      CheckBox1.Visible = True
                      StartButton.Enabled = True
                      SelectButton.Enabled = True
                      FileList.Visible = True
                      OpenArchiv.Enabled = True
                      ProgressBar1.Visible = False
                      StatusText.Visible = False
                      MenuStrip1.Enabled = True
                      Label3.Visible = False
                      Me.Size = New Drawing.Size(Me.Size.Width, Me.Size.Height + 250)
                  End Sub)

        FileList.Items.Clear() ' Leere die Liste nach Abschluss der Komprimierung

        If e.Error IsNot Nothing Then
            MessageBox.Show($"Fehler beim Komprimieren: {e.Error.Message}", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error)
        ElseIf e.Cancelled Then
            MessageBox.Show("Komprimierung abgebrochen.", "Abgebrochen", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Else
            ' Wenn das Ergebnis die Parameter enthält, können Sie den Archivpfad anzeigen
            Dim resultParameters As CompressionParameters = TryCast(e.Result, CompressionParameters)
            If resultParameters IsNot Nothing Then
                MessageBox.Show($"Komprimierung abgeschlossen: {resultParameters.ArchiveFilePath}", "Erfolg", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                MessageBox.Show("Komprimierung abgeschlossen.", "Erfolg", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        End If
        _isCancellationPending = False
    End Sub

    Public Class CompressionProgressInfo
        Public ArchiveFilePath As String
        Public CurrentFile As String
        Public ElapsedTime As TimeSpan
    End Class

    Private Class CompressionParameters
        Public ArchiveFilePath As String
        Public FilePaths As String()
        Public IsZip As Boolean
        Public IsRar As Boolean
    End Class

    Private Function CalculateTotalSize() As Long
        Dim totalSize As Long = 0
        For Each item As ListViewItem In FileList.Items
            ' Hier item.Checked prüfen, da nur ausgewählte Dateien zur Gesamtgröße beitragen sollen
            If item.Checked Then
                ' Der Tag sollte hier den vollständigen Pfad enthalten, sei es von Disk oder innerhalb des ZIPs
                If item.Tag IsNot Nothing Then
                    Dim pathOrEntryName As String = item.Tag.ToString()
                    If Not String.IsNullOrEmpty(pathOrEntryName) Then
                        ' Prüfen, ob es sich um einen Pfad auf der Festplatte handelt
                        If Path.IsPathRooted(pathOrEntryName) AndAlso File.Exists(pathOrEntryName) Then
                            Try
                                Dim fileInfo As New FileInfo(pathOrEntryName)
                                totalSize += fileInfo.Length
                            Catch ex As Exception
                                MessageBox.Show($"Fehler beim Zugriff auf Datei: {pathOrEntryName}. Sie wird bei der Größenberechnung ignoriert.", "Datei Fehler", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                            End Try
                        Else
                            ' Wenn es sich um einen ZIP-Eintrag handelt, ist die Größe bereits in den SubItems
                            ' Sie müssen den Wert aus dem SubItem parsen
                            If item.SubItems.Count > 1 Then
                                Dim sizeString As String = item.SubItems(1).Text
                                ' Konvertieren Sie die formatierte Größe zurück in Bytes (oder speichern Sie die Bytes direkt im SubItem)
                                ' Für diese Implementierung speichern wir die Roh-Byte-Größe im Tag, wenn es ein ZipEntry ist
                                ' Aktuell FormatFileSize -> String, d.h. Parse ist notwendig
                                ' Besser: Speichern Sie die Raw-Größe im Tag für Zip-Einträge
                                ' Oder: Addieren Sie nur die Disk-Dateien zur Größe, wenn Sie gerade Disk-Dateien listen
                                ' Dies ist ein komplexerer Punkt. Für die aktuelle Struktur addieren wir nur die Dateigrößen von Disk-Dateien.
                                ' Wenn Sie Zip-Einträge im Tag haben, müssen Sie die tatsächliche Größe des Zip-Eintrags speichern,
                                ' um eine korrekte Gesamtgröße des Archivinhalts zu berechnen.
                                ' Für jetzt ignorieren wir die Größe von Zip-Einträgen in dieser Funktion,
                                ' da die FormatFileSize-Umkehrung komplex ist.
                            End If
                        End If
                    End If
                End If
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
        ' Diese Logik ist etwas ungewöhnlich. Normalerweise wird SelectedIndexChanged
        ' verwendet, um auf die Auswahl zu reagieren, nicht um Checked zu setzen.
        ' Die Prüfung "If FileList.Items.Count < 0 Then" ist immer False, da Count nie negativ ist.
        ' If FileList.CheckedItems.Count > 0 Then ' Besser so prüfen, wenn Sie nur auf gecheckte Items reagieren wollen
        ' Dim selectedItem As ListViewItem = FileList.CheckedItems(0)
        ' Wenn Sie nur das Checked-Verhalten steuern wollen, ist ItemChecked besser.
    End Sub

    Private Sub ItemNo_Click(sender As Object, e As EventArgs) Handles ItemNo.Click
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        FileList.Items.Clear()
        FileListIconList.Images.Clear() ' Auch die ImageList leeren

        ItemNo.Text = "Datei:"
        SizeText.Text = "Größe:"
        CheckBox1.Checked = False
        OpenArchiv.Enabled = True
        If UnZipButton.Visible = True Then
            UnZipButton.Visible = False
            Panel_0.Visible = True
            StartButton.Visible = True
            ZipFormatButton.Enabled = True
            RARFormatButton.Enabled = True
            StartButton.Enabled = True
        End If
    End Sub

    Private Sub RemoveToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RemoveToolStripMenuItem.Click
        ' Erstellen Sie eine Liste von Elementen zum Entfernen, um Probleme beim Iterieren zu vermeiden
        Dim itemsToRemove As New List(Of ListViewItem)()
        For Each item As ListViewItem In FileList.Items
            If item.Checked Then
                itemsToRemove.Add(item)
            End If
        Next

        For Each item As ListViewItem In itemsToRemove
            FileList.Items.Remove(item)
        Next

        ItemNo.Text = "Files: " & FileList.Items.Count.ToString()
        UpdateTotalSizeLabel()
    End Sub

    Public Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        Dim res As DialogResult = MessageBox.Show(Me, "Möchten Sie die Anwendung wirklich schließen?", "Beenden", MessageBoxButtons.OKCancel, MessageBoxIcon.Question)
        If res = DialogResult.OK Then
            ' Aufräumen der Icons beim Beenden
            If _folderIcon IsNot Nothing Then
                _folderIcon.Dispose()
                _folderIcon = Nothing
            End If
            If _fileIcon IsNot Nothing Then
                _fileIcon.Dispose()
                _fileIcon = Nothing
            End If
        Else
            e.Cancel = True
        End If
    End Sub

    Private Sub ExitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem.Click
        Me.Form1_FormClosing(sender, e)
    End Sub

    Private Sub InfoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles InfoToolStripMenuItem.Click
        About.Show() ' Angenommen About ist eine Instanz Ihrer About-Form
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        For Each item As ListViewItem In FileList.Items
            item.Checked = CheckBox1.Checked
            item.Selected = CheckBox1.Checked
        Next
        UpdateTotalSizeLabel() ' Größe aktualisieren, wenn alle ausgewählt/abgewählt werden
    End Sub

    Private Sub FileList_ItemChecked(sender As Object, e As ItemCheckedEventArgs) Handles FileList.ItemChecked
        e.Item.Selected = e.Item.Checked
        UpdateTotalSizeLabel() ' Größe aktualisieren, wenn ein einzelnes Item gecheckt wird
    End Sub

    Private Sub Form1_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        ' Icon-Initialisierung ganz am Anfang der Load-Methode
        InitializeIcons() ' Neue Methode zum Laden der Icons

        Me.BackColor = My.Settings.AppColor
        Me.ForeColor = My.Settings.StringColor
        Me.Font = My.Settings.AppFont
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

    Private Sub OpenArchiv_Click(sender As Object, e As EventArgs) Handles OpenArchiv.Click
        Using openFileDialog As New OpenFileDialog()
            openFileDialog.Filter = "ZIP-Dateien (*.zip)|*.zip|Alle Dateien (*.*)|*.*"
            openFileDialog.Title = "ZIP-Datei zum Öffnen auswählen"

            If openFileDialog.ShowDialog() = DialogResult.OK Then
                _zipFilePath = openFileDialog.FileName
                SetControlsForArchiveView(True)

                UnZipButton.Visible = True
                StartButton.Enabled = False
                SelectButton.Enabled = False
                ZipFormatButton.Enabled = False
                OpenArchiv.Enabled = False

                FileList.Items.Clear()
                FileListIconList.Images.Clear()

                Try
                    Using zip As ZipArchive = ZipFile.Open(_zipFilePath, ZipArchiveMode.Read)
                        ItemNo.Text = "Datei: " & zip.Entries.Count.ToString()

                        For Each zEntry As ZipArchiveEntry In zip.Entries
                            AddZipEntryToListView(zEntry) ' Nutzen der Hilfsmethode
                        Next
                    End Using
                    UpdateTotalSizeLabel()
                    FileList.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent)
                    FileList.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize)
                Catch ex As Exception
                    MessageBox.Show($"Fehler beim Öffnen des Archivs: {ex.Message}", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    SetControlsForArchiveView(False)
                    UnZipButton.Visible = False
                    StartButton.Enabled = True
                    SelectButton.Enabled = True
                    ZipFormatButton.Enabled = True
                    OpenArchiv.Enabled = True
                End Try
            End If
        End Using
    End Sub

    Private Sub UnZipButton_Click(sender As System.Object, e As System.EventArgs) Handles UnZipButton.Click
        Me.Invoke(Sub()
                      Panel_0.Visible = False
                      Panel_2.Visible = False
                      FileList.Visible = False
                      Button1.Visible = False
                      ProgressBar1.Visible = True
                      StatusText.Visible = True
                      StatusText.Text = "Entpacke.."
                      MenuStrip1.Enabled = False
                      Me.Size = New Drawing.Size(Me.Size.Width, Me.Size.Height - 250)
                  End Sub)

        FolderBrowserDialog1.Description = "Wählen Sie den Zielordner für die Entpackung aus"
        FolderBrowserDialog1.ShowNewFolderButton = True

        If FolderBrowserDialog1.ShowDialog() = DialogResult.OK Then
            _extractPath = FolderBrowserDialog1.SelectedPath
            Try
                ' Keine Notwendigkeit, die FileList hier neu zu füllen, da sie bereits geladen ist
                ' Wir benötigen die *aktuell ausgewählten* Elemente, nicht alle Einträge des ZIPs
                Dim selectedItemsList As New List(Of String)()
                Me.Invoke(Sub()
                              For Each item As ListViewItem In FileList.SelectedItems
                                  If item.Tag IsNot Nothing Then
                                      selectedItemsList.Add(item.Tag.ToString())
                                  End If
                              Next
                          End Sub)

                _isCancellationPending = False
                _unzipWorker.RunWorkerAsync(New UnzipParameters With {
                           .ZipFilePath = _zipFilePath,
                           .ExtractPath = _extractPath,
                           .EntriesToExtract = selectedItemsList.ToArray() ' Nur die ausgewählten Pfade übergeben
                       })
                'CheckBox1.Checked = True ' Dies sollte nach dem Entpacken erfolgen, nicht hier
                'FileList.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize)
                'FileList.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent)
            Catch ex As Exception
                MessageBox.Show(text:=$"Fehler beim Lesen des ZIP Archives: {ex.Message}")
                ' UI-Elemente im Fehlerfall zurücksetzen
                Me.Invoke(Sub()
                              Panel_0.Visible = True
                              Panel_2.Visible = True
                              FileList.Visible = True
                              Button1.Visible = True
                              ProgressBar1.Visible = False
                              StatusText.Visible = False
                              MenuStrip1.Enabled = True
                              Me.Size = New Drawing.Size(Me.Size.Width, Me.Size.Height + 250)
                          End Sub)
            End Try
        Else
            MessageBox.Show("Bitte wählen Sie einen Zielordner zum Entpacken aus.", "Hinweis", MessageBoxButtons.OK, MessageBoxIcon.Information)
            ' UI-Elemente im Falle des Abbrechens der Ordnerauswahl zurücksetzen
            Me.Invoke(Sub()
                          Panel_0.Visible = True
                          Panel_2.Visible = True
                          FileList.Visible = True
                          Button1.Visible = True
                          ProgressBar1.Visible = False
                          StatusText.Visible = False
                          MenuStrip1.Enabled = True
                          Me.Size = New Drawing.Size(Me.Size.Width, Me.Size.Height + 250)
                      End Sub)
        End If
    End Sub

    Private Class UnzipParameters
        Public ZipFilePath As String
        Public ExtractPath As String
        Public EntriesToExtract As String() ' Array von vollen Pfaden der zu entpackenden Einträge
        ' Public SelectedItems As ListView.SelectedListViewItemCollection ' Nicht mehr direkt benötigt, da wir die Pfade übergeben
    End Class

    Private Sub UnzipWorker_DoWork(sender As Object, e As DoWorkEventArgs) Handles _unzipWorker.DoWork
        Dim worker As BackgroundWorker = DirectCast(sender, BackgroundWorker)
        Dim parameters As UnzipParameters = DirectCast(e.Argument, UnzipParameters)

        Try
            Using archive As ZipArchive = ZipFile.Open(parameters.ZipFilePath, ZipArchiveMode.Read)
                Dim totalEntriesToExtract As Integer = parameters.EntriesToExtract.Length
                Dim extractedEntriesCount As Integer = 0

                For Each entryName As String In parameters.EntriesToExtract
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
                        Dim progressPercentage As Integer = CInt((extractedEntriesCount / CDbl(totalEntriesToExtract)) * 100)

                        worker.ReportProgress(progressPercentage, entry.FullName) ' Den vollen Namen des Eintrags übergeben
                    Else
                        worker.ReportProgress(0, $"Datei nicht gefunden im Archiv: {entryName}")
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
        Label3.Text = "" ' Sie können hier weitere Informationen anzeigen, z.B. die Gesamtgröße
    End Sub

    Private Sub UnzipWorker_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs) Handles _unzipWorker.RunWorkerCompleted
        Me.Invoke(Sub()
                      Panel_0.Visible = True
                      Panel_2.Visible = True
                      Button1.Visible = True
                      CheckBox1.Visible = True
                      StartButton.Enabled = True
                      SelectButton.Enabled = True
                      OpenArchiv.Enabled = True
                      FileList.Visible = True
                      ProgressBar1.Visible = False
                      StatusText.Visible = False
                      UnZipButton.Visible = False
                      MenuStrip1.Enabled = True
                      Me.Size = New Drawing.Size(Me.Size.Width, Me.Size.Height + 250)
                  End Sub)

        ' Alle Items nach dem Entpacken deaktivieren
        For Each item As ListViewItem In FileList.Items
            item.Checked = False
        Next

        If e.Cancelled Then
            MessageBox.Show("Entpacken abgebrochen.", "Abgebrochen", MessageBoxButtons.OK, MessageBoxIcon.Information)
        ElseIf e.Error IsNot Nothing Then
            MessageBox.Show($"Fehler beim Entpacken: {e.Error.Message}", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Else
            MessageBox.Show($"Archiv erfolgreich entpackt nach: {e.Result}", "Erfolg")
        End If
        _isCancellationPending = False
    End Sub

    Private Sub OptionsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OptionsToolStripMenuItem.Click
        Using settingsDialog As New SettingsDialog()
            settingsDialog.ShowDialog()
        End Using
    End Sub

    Public Shared Sub Form1BackColor(sender As Object, it As Drawing.Color)
        Dim form As Form1 = DirectCast(sender, Form1)
        form.BackColor = it
    End Sub

    ' Diese Methode ist problematisch, da Form1.Font und Form1.Controls nicht statisch sind.
    ' Nutzen Sie die UpdateChildControlFonts-Methode, die bereits existiert.
    ' Public Shared Sub Form1Font(dialog1 As SettingsDialog, font As Font)
    '     Form1.Font = font
    '     For Each ctrl As Control In Form1.Controls
    '         ctrl.Font = font
    '     Next
    ' End Sub

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

    Private Sub FileList_DragEnter(sender As Object, e As DragEventArgs) Handles FileList.DragEnter
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            e.Effect = DragDropEffects.Copy
        Else
            e.Effect = DragDropEffects.None
        End If
    End Sub

    Private Sub Form1_DragDrop(sender As Object, e As DragEventArgs) Handles MyBase.DragDrop, FileList.DragDrop
        Dim droppedPaths As String() = CType(e.Data.GetData(DataFormats.FileDrop), String())

        If droppedPaths Is Nothing OrElse droppedPaths.Length = 0 Then
            Return
        End If

        FileList.Items.Clear()
        FileListIconList.Images.Clear()

        If droppedPaths.Length = 1 AndAlso droppedPaths(0).ToLower().EndsWith(".zip") Then
            Dim zipFilePath As String = droppedPaths(0)
            _zipFilePath = zipFilePath
            SetControlsForArchiveView(True)

            UnZipButton.Visible = True
            StartButton.Enabled = False
            SelectButton.Enabled = False
            ZipFormatButton.Enabled = False
            OpenArchiv.Enabled = False
            Try
                Using zip As ZipArchive = ZipFile.Open(_zipFilePath, ZipArchiveMode.Read)
                    ItemNo.Text = "Datei: " & zip.Entries.Count.ToString()

                    For Each zEntry As ZipArchiveEntry In zip.Entries
                        AddZipEntryToListView(zEntry) ' Nutzen der Hilfsmethode
                    Next
                End Using
                UpdateTotalSizeLabel()
                FileList.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize)
                FileList.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent)
            Catch ex As Exception
                MessageBox.Show($"Fehler beim Öffnen des Archivs: {ex.Message}{Environment.NewLine}{ex.GetType().Name}: {ex.StackTrace}", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error)
                SetControlsForArchiveView(False)
                UnZipButton.Visible = False
                StartButton.Enabled = True
                SelectButton.Enabled = True
                ZipFormatButton.Enabled = True
                OpenArchiv.Enabled = True
            End Try
        Else
            SetControlsForArchiveView(False)
            UnZipButton.Visible = False
            StartButton.Enabled = True
            SelectButton.Enabled = True
            ZipFormatButton.Enabled = True
            OpenArchiv.Enabled = True

            For Each droppedPath As String In droppedPaths
                AddFileOrDirectoryToListView(droppedPath) ' Nutzen der Hilfsmethode
            Next
            ItemNo.Text = "Files: " & FileList.Items.Count.ToString()
            UpdateTotalSizeLabel()
            FileList.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize)
            FileList.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent)
        End If
    End Sub

    ' Hilfsmethode zum Hinzufügen von Dateien und Ordnern aus einem Verzeichnis (rekursiv)
    Private Sub AddDirectoryContentToList(directoryPath As String)
        Try
            ' Fügen Sie den Ordner selbst als Eintrag hinzu, falls er nicht schon ein übergeordneter ist
            ' Das ist wichtig, wenn man einen Ordner per Drag & Drop hinzufügt und er selbst als Element erscheinen soll
            AddFileOrDirectoryToListView(directoryPath)

            ' Dateien im Verzeichnis
            For Each filePath As String In Directory.GetFiles(directoryPath)
                AddFileOrDirectoryToListView(filePath)
            Next
            ' Unterverzeichnisse (rekursiv)
            For Each subDirPath As String In Directory.GetDirectories(directoryPath)
                AddDirectoryContentToList(subDirPath) ' Rekursiver Aufruf
            Next
        Catch ex As Exception
            MessageBox.Show($"Fehler beim Lesen des Verzeichnisses '{directoryPath}': {ex.Message}{Environment.NewLine}{ex.GetType().Name}: {ex.StackTrace}", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' **NEU** Hilfsmethode zum Hinzufügen einer einzelnen Datei/Ordner zur ListView
    Private Sub AddFileOrDirectoryToListView(path As String)
        Dim isDirectory As Boolean = Directory.Exists(path)
        Dim itemText As String = System.IO.Path.GetFileName(path)
        If isDirectory Then itemText &= "\" ' Füge einen Backslash für Ordner hinzu

        Dim item As New ListViewItem With {
            .Text = itemText,
            .Tag = path, ' Speichere den vollständigen Pfad
            .Checked = True
        }

        Dim fileSize As String = "Ordner"
        If Not isDirectory Then
            Try
                Dim fileInfo As New FileInfo(path)
                fileSize = FormatFileSize(fileInfo.Length)
            Catch ex As Exception
                fileSize = "N/A"
            End Try
        End If
        item.SubItems.Add(fileSize)

        Dim iconToAdd As Icon
        If isDirectory Then
            iconToAdd = _folderIcon ' Verwenden Sie Ihr geladenes Ordner-Icon
        Else
            iconToAdd = GetFileIconFromPath(path) ' Verwenden Sie die Methode, um ein Icon von der Datei zu bekommen
        End If

        ' Sicherstellen, dass ein Icon verfügbar ist
        If iconToAdd Is Nothing Then
            iconToAdd = SystemIcons.WinLogo ' Fallback-Icon
        End If

        Dim iconKey As String = Guid.NewGuid().ToString() ' Eindeutiger Schlüssel für das Icon
        FileListIconList.Images.Add(iconKey, iconToAdd)
        item.ImageKey = iconKey ' ImageKey statt ImageIndex verwenden für bessere Verwaltung
        item.Checked = True ' Standardmäßig als ausgewählt markieren

        FileList.Items.Add(item)
    End Sub

    ' **NEU** Hilfsmethode zum Hinzufügen eines ZIP-Eintrags zur ListView
    Private Sub AddZipEntryToListView(zEntry As ZipArchiveEntry)
        Dim isDirectory As Boolean = zEntry.FullName.EndsWith("/"c) ' Ordner in ZIPs enden mit '/'
        Dim itemText As String = zEntry.FullName
        If isDirectory Then itemText = itemText.TrimEnd("/"c) & "\" ' Formatierung anpassen

        Dim item As New ListViewItem With {
            .Text = itemText,
            .Tag = zEntry.FullName, ' Der volle Pfad innerhalb des ZIPs
            .Checked = True
        }

        Dim fileSize As String = "Ordner"
        If Not isDirectory Then
            fileSize = FormatFileSize(zEntry.CompressedLength) ' Genutzte Größe im Archiv
            ' Sie könnten hier auch zEntry.Length für die unkomprimierte Größe verwenden
        End If
        item.SubItems.Add(fileSize)

        Dim iconToAdd As Icon
        If isDirectory Then
            iconToAdd = _folderIcon ' Verwenden Sie Ihr geladenes Ordner-Icon
        Else
            ' Für ZIP-Einträge müssen wir das Icon basierend auf der Erweiterung ermitteln
            iconToAdd = GetIconForZipEntry(zEntry.FullName)
        End If

        ' Sicherstellen, dass ein Icon verfügbar ist
        If iconToAdd Is Nothing Then
            iconToAdd = SystemIcons.WinLogo ' Fallback-Icon
        End If

        Dim iconKey As String = Guid.NewGuid().ToString() ' Eindeutiger Schlüssel für das Icon
        FileListIconList.Images.Add(iconKey, iconToAdd)
        item.ImageKey = iconKey ' ImageKey statt ImageIndex verwenden

        FileList.Items.Add(item)
    End Sub

    ' **NEU** Hilfsmethode zum Initialisieren der Icons beim Start der Form
    Private Sub InitializeIcons()
        ' Laden des Ordner-Icons
        If My.Resources.icons8_mappe_144 Is Nothing Then
            MessageBox.Show("Ressource 'icons8_mappe_144' ist nicht verfügbar. Bitte prüfen Sie den Ressourcennamen und die Einbettung.", "Ressourcenfehler", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            _folderIcon = SystemIcons.WinLogo ' Fallback
        Else

            Using msDir As New MemoryStream(My.Resources.icons8_mappe_144)
                Try
                    _folderIcon = New Icon(msDir)
                Catch ex As Exception
                    MessageBox.Show("Fehler beim Laden des Ordner-Icons (icons8_mappe_144): " & ex.Message, "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    _folderIcon = SystemIcons.WinLogo ' Fallback
                End Try
            End Using
        End If

        ' Laden des Datei-Icons
        If My.Resources.Culture IsNot Nothing Then ' Angenommen Sie haben ein solches Icon in Ihren Ressourcen
            Using msFile As New MemoryStream
                Try
                    _fileIcon = New Icon(msFile)
                Catch ex As Exception
                    MessageBox.Show("Fehler beim Laden des Datei-Icons (icons8_datei_144): " & ex.Message, "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    _fileIcon = SystemIcons.WinLogo ' Fallback
                End Try
            End Using
        Else
            MessageBox.Show("Ressource 'icons8_datei_144' ist nicht verfügbar. Verwende SystemIcons.WinLogo.", "Ressourcenfehler", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            _fileIcon = SystemIcons.WinLogo ' Fallback
        End If

        ' Stellen Sie sicher, dass FileListIconList initialisiert ist und Icons aufnehmen kann
        If FileList.SmallImageList Is Nothing Then
            FileList.SmallImageList = New ImageList With {
                .ImageSize = New Size(16, 16) ' Standardgröße für kleine Icons
                }
        End If
        If FileList.LargeImageList Is Nothing Then
            FileList.LargeImageList = New ImageList With {
                .ImageSize = New Size(32, 32) ' Standardgröße für große Icons
                }
        End If

        ' Fügen Sie Ihre Icons direkt zur ImageList des ListViews hinzu, wenn Sie sie statisch laden
        ' Dies ist die primäre ImageList, die von der ListView verwendet wird.
        ' Vermeiden Sie es, das gleiche Icon immer wieder hinzuzufügen.
        If Not FileListIconList.Images.ContainsKey("Folder") Then
            FileListIconList.Images.Add("Folder", _folderIcon)
        End If
        If Not FileListIconList.Images.ContainsKey("File") Then
            FileListIconList.Images.Add("File", _fileIcon)
        End If
    End Sub

    ' Hilfsmethode, um Icons für Dateien auf dem Dateisystem zu bekommen
    Private Function GetFileIconFromPath(filePath As String) As Icon
        Try
            If File.Exists(filePath) Then
                ' Icon.ExtractAssociatedIcon funktioniert gut für existierende Dateien
                Return Icon.ExtractAssociatedIcon(filePath)
            Else
                Return _fileIcon ' Fallback auf das generische Datei-Icon aus Ressourcen
            End If
        Catch ex As Exception
            Console.WriteLine($"Fehler beim Extrahieren des Icons für '{filePath}': {ex.Message}")
            Return _fileIcon ' Fallback auf das generische Datei-Icon aus Ressourcen
        End Try
    End Function

    ' Hilfsmethode, um Icons für ZIP-Einträge zu bekommen (da sie nicht auf dem Dateisystem existieren)
    Private Function GetIconForZipEntry(entryFullName As String) As Icon
        If Not entryFullName.EndsWith("/"c) Then
            ' Wenn es sich um eine Datei handelt, versuchen Sie, ein Icon basierend auf der Dateierweiterung zu bekommen
            Dim extension As String = Path.GetExtension(entryFullName)
            If Not String.IsNullOrEmpty(extension) Then
                Try
                    ' Erstelle eine temporäre Datei, um das Icon zu extrahieren
                    Dim tempFile As String = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() & extension)
                    File.WriteAllBytes(tempFile, Array.Empty(Of Byte)()) ' Leere temporäre Datei erstellen
                    Dim icon As Icon = Icon.ExtractAssociatedIcon(tempFile)
                    File.Delete(tempFile) ' Temporäre Datei löschen
                    Return icon
                Catch
                    Return _fileIcon ' Fallback auf das generische Datei-Icon aus Ressourcen
                End Try
            Else
                Return _fileIcon ' Fallback auf das generische Datei-Icon aus Ressourcen
            End If
        Else
            Return _folderIcon ' Ordner im ZIP erhalten Ordner-Icon
        End If
    End Function

    Private Sub SetControlsForArchiveView(isArchiveView As Boolean)
        StartButton.Enabled = Not isArchiveView
        ZipFormatButton.Enabled = Not isArchiveView
        RARFormatButton.Enabled = Not isArchiveView
        Button1.Visible = isArchiveView ' Dieser Button ist wohl der "Clear List"-Button?
        UnZipButton.Visible = isArchiveView ' Unzip-Button nur in Archivansicht sichtbar

        FileList.Columns.Clear()
        FileList.Columns.Add("Datei:", 250)
        FileList.Columns.Add("Größe:", 200)

        FileList.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize)
        FileList.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent)
    End Sub

    Private Sub FAQToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FAQToolStripMenuItem.Click
        FormFAQ.Show() ' Angenommen FormFAQ ist eine Instanz Ihrer FAQ-Form
    End Sub

    Private Function FormatTimeSpan(timeSpan As TimeSpan) As String
        If timeSpan.TotalSeconds < 60 Then
            Return $"{Math.Round(timeSpan.TotalSeconds)} Sek."
        ElseIf timeSpan.TotalMinutes < 60 Then
            Return $"{Math.Round(timeSpan.TotalMinutes)} Min. {timeSpan.Seconds} Sek."
        Else
            Return $"{Math.Round(timeSpan.TotalHours)} Std. {timeSpan.Minutes} Min."
        End If
    End Function

    Private Sub RARFormatButton_CheckedChanged(sender As Object, e As EventArgs) Handles RARFormatButton.CheckedChanged
        My.Settings.CompressFormat = RARFormatButton.Checked
    End Sub

    Private Sub FileList_DragLeave(sender As Object, e As EventArgs) Handles FileList.DragLeave
    End Sub
End Class