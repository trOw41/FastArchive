Public Class FormFAQ
    Private faqContent As Dictionary(Of String, String)

    Private Sub FaqForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        InitializeFaqContent() ' FAQ-Inhalte initialisieren
        PopulateIndexBox()     ' IndexBox füllen
        IndexBox.SelectedIndex = 0 ' Keine Auswahl in der IndexBox
        ' Optional: Ersten FAQ-Eintrag beim Laden anzeigen
        If IndexView.Nodes.Count > 1 Then
            IndexView.SelectedNode = IndexView.Nodes(1) ' Wählt "1. Was ist FastArchiver?"
        End If
    End Sub

    Private Sub InitializeFaqContent()
        faqContent = New Dictionary(Of String, String) From {
            {"i1", "FastArchiver ist eine Anwendung, die Ihnen hilft, Dateien und Ordner zu komprimieren (als ZIP-Archive) und Inhalte aus bestehenden ZIP-Archiven zu entpacken. Sie bietet eine einfache Drag & Drop-Funktionalität und eine übersichtliche Dateiverwaltung."},
            {"i2", "Sie können Dateien und Ordner auf zwei Arten hinzufügen:" & Environment.NewLine & Environment.NewLine &
                              "• Drag & Drop: Ziehen Sie einfach eine oder mehrere Dateien oder ganze Ordner direkt auf das FastArchiver-Fenster. Die Inhalte werden automatisch in der Dateiliste angezeigt." & Environment.NewLine & Environment.NewLine &
                              "• ""Dateien auswählen""-Button: Klicken Sie auf den Button ""Dateien auswählen"" (oder ähnlich), um einen Dateiauswahldialog zu öffnen. Hier können Sie manuell Dateien und Ordner zum Komprimieren auswählen."},
            {"i2-1", "Ziehen Sie einfach eine oder mehrere Dateien oder ganze Ordner direkt auf das FastArchiver-Fenster. Die Inhalte werden automatisch in der Dateiliste angezeigt."},
            {"i2-2", "Klicken Sie auf den Button ""Dateien auswählen"" (oder ähnlich), um einen Dateiauswahldialog zu öffnen. Hier können Sie manuell Dateien und Ordner zum Komprimieren auswählen."},
            {"i3", "1. Fügen Sie die gewünschten Dateien und/oder Ordner zur Liste hinzu (siehe Frage 2)." & Environment.NewLine &
                              "2. Stellen Sie sicher, dass die Checkboxen neben den Dateien, die Sie komprimieren möchten, aktiviert sind." & Environment.NewLine &
                              "3. Klicken Sie auf den ""Start""-Button." & Environment.NewLine &
                              "4. Wählen Sie im erscheinenden Dialog einen Speicherort und einen Namen für Ihr neues ZIP-Archiv." & Environment.NewLine &
                              "5. Bestätigen Sie mit ""Speichern"", um den Komprimierungsvorgang zu starten."},
            {"i4", "Sie können ein ZIP-Archiv auf zwei Arten öffnen:" & Environment.NewLine & Environment.NewLine &
                              "• Drag & Drop: Ziehen Sie eine ZIP-Datei direkt auf das FastArchiver-Fenster. Der Inhalt des Archivs wird automatisch in der Dateiliste angezeigt." & Environment.NewLine & Environment.NewLine &
                              "• ""Archiv öffnen""-Button: Klicken Sie auf den Button ""Archiv öffnen"" (oder ähnlich), um einen Dateiauswahldialog zu öffnen. Wählen Sie hier die ZIP-Datei aus, deren Inhalt Sie sehen möchten."},
            {"i5", "1. Öffnen Sie das gewünschte ZIP-Archiv (siehe Frage 4). Die enthaltenen Dateien werden in der Liste angezeigt." & Environment.NewLine &
                              "2. Wählen Sie die Dateien in der Liste aus, die Sie entpacken möchten (standardmäßig sind alle ausgewählt)." & Environment.NewLine &
                              "3. Klicken Sie auf den ""Entpacken""-Button (UnZipButton)." & Environment.NewLine &
                              "4. Wählen Sie im erscheinenden Dialog den Zielordner aus, in den die Dateien entpackt werden sollen." & Environment.NewLine &
                              "5. Bestätigen Sie, um den Entpackvorgang zu starten. Nach Abschluss wird der Zielordner im Windows Explorer geöffnet."},
            {"i6", "• Einzelne Dateien entfernen: Wählen Sie die Datei in der Liste aus, klicken Sie mit der rechten Maustaste und wählen Sie ""Entfernen""." & Environment.NewLine &
                              "• Alle Dateien entfernen: Klicken Sie auf den ""Leeren""-Button (oder ähnlich), um alle Einträge aus der Liste zu entfernen."},
            {"i7", "Klicken Sie im Menü auf ""Optionen"" (oder ""Einstellungen""), um den Einstellungsdialog zu öffnen. Hier können Sie:" & Environment.NewLine &
                              "• Die Hintergrundfarbe der Anwendung ändern." & Environment.NewLine &
                              "• Eine andere Schriftart für die Benutzeroberfläche auswählen." & Environment.NewLine &
                              "• Optionen für die Standardschriftart festlegen." & Environment.NewLine &
                              "Vergessen Sie nicht, auf ""OK"" zu klicken, um Ihre Änderungen zu speichern."},
            {"i8", "FastArchiver benötigt möglicherweise Administratorrechte, um Dateien in bestimmte geschützte Systemordner zu entpacken oder dort neue Ordner zu erstellen. Wenn dies der Fall ist, werden Sie von Windows über die Benutzerkontensteuerung (UAC) zur Bestätigung aufgefordert. Dies dient dem Schutz Ihres Systems."},
            {"i9", "Diese Meldung bedeutet, dass FastArchiver keine Berechtigung hat, in den ausgewählten Zielordner zu schreiben oder die ZIP-Datei zu lesen." & Environment.NewLine & Environment.NewLine &
                              "• Stellen Sie sicher, dass Sie Schreibberechtigungen für den Zielordner haben. Versuchen Sie, einen anderen Ordner zu wählen, z. B. Ihren ""Downloads""- oder ""Dokumente""-Ordner." & Environment.NewLine &
                              "• Stellen Sie sicher, dass die ZIP-Datei nicht von einem anderen Programm geöffnet oder gesperrt ist." & Environment.NewLine &
                              "• Wenn das Problem weiterhin besteht, versuchen Sie, FastArchiver als Administrator auszuführen (Rechtsklick auf die .exe-Datei -> ""Als Administrator ausführen"")."},
            {"i10", "Klicken Sie auf den Button ""Leeren"" (oder ähnlich), um alle Dateien aus der Liste zu entfernen und die Anwendung für eine neue Aufgabe vorzubereiten."}
        }
    End Sub

    Private Sub PopulateIndexBox()
        IndexBox.Items.Clear()
        For Each node As TreeNode In IndexView.Nodes
            If node.Name <> "index" Then ' Den Wurzelknoten "Inhalt:" ausschließen
                IndexBox.Items.Add(node.Text)
            End If
        Next
    End Sub

    Private Sub IndexView_AfterSelect(sender As Object, e As TreeViewEventArgs) Handles IndexView.AfterSelect
        If e.Node IsNot Nothing Then

            Dim value As String = Nothing

            If faqContent.TryGetValue(e.Node.Name, value) Then
                FaqText.Text = value
            Else
                FaqText.Text = "Wählen Sie einen Eintrag aus dem Inhaltsverzeichnis."
            End If
        Else
            FaqText.Text = "Wählen Sie einen Eintrag aus dem Inhaltsverzeichnis."
        End If
    End Sub

    Private Sub IndexBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles IndexBox.SelectedIndexChanged
        If IndexBox.SelectedItem IsNot Nothing Then
            Dim selectedText As String = IndexBox.SelectedItem.ToString()
            For Each node As TreeNode In IndexView.Nodes
                If node.Text = selectedText Then
                    IndexView.SelectedNode = node
                    Exit For
                ElseIf node.Nodes.Count > 0 Then ' Auch in den Kindknoten suchen
                    For Each childNode As TreeNode In node.Nodes
                        If childNode.Text = selectedText Then
                            IndexView.SelectedNode = childNode
                            Exit For
                        End If
                    Next
                End If
            Next
        End If
    End Sub

    Private Sub SchließenToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SchließenToolStripMenuItem.Click
        Me.Close()
    End Sub
End Class