Partial Class FaqForm
    Inherits System.Windows.Forms.Form
    ' Private components As System.ComponentModel.IContainer
    'Das Formular überschreibt den Löschvorgang, um die Komponentenliste zu bereinigen.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Wird vom Windows Form-Designer benötigt.


    'Hinweis: Die folgende Prozedur ist für den Windows Form-Designer erforderlich.
    'Das Bearbeiten ist mit dem Windows Form-Designer möglich.  
    'Das Bearbeiten mit dem Code-Editor ist nicht möglich.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FaqForm))

        ' TreeNodes für IndexView
        Dim TreeNode1 As TreeNode = New TreeNode("Inhalt:")
        Dim TreeNode2 As TreeNode = New TreeNode("1. Was ist FastArchiver?")
        Dim TreeNode3 As TreeNode = New TreeNode("Drag & Drop..")
        Dim TreeNode4 As TreeNode = New TreeNode("Dateien auswählen..")
        Dim TreeNode5 As TreeNode = New TreeNode("2. Wie füge ich Dateien oder Ordner zur Liste hinzu?", New TreeNode() {TreeNode3, TreeNode4})
        Dim TreeNode6 As TreeNode = New TreeNode("3. Wie erstelle ich ein ZIP-Archiv?")
        Dim TreeNode7 As TreeNode = New TreeNode("4. Wie öffne und sehe ich den Inhalt eines ZIP-Archivs?")
        Dim TreeNode8 As TreeNode = New TreeNode("5. Wie entpacke ich Dateien aus einem ZIP-Archiv?")
        Dim TreeNode9 As TreeNode = New TreeNode("6. Wie entferne ich Dateien aus der Liste?")
        Dim TreeNode10 As TreeNode = New TreeNode("7. Wie ändere ich die Einstellungen (Farbe, Schriftart)?")
        Dim TreeNode11 As TreeNode = New TreeNode("8. Warum benötigt die App Administratorrechte?")
        Dim TreeNode12 As TreeNode = New TreeNode("9. Ich erhalte eine ""Zugriff verweigert""-Fehlermeldung beim Entpacken. Was kann ich tun?")
        Dim TreeNode13 As TreeNode = New TreeNode("10. Wie leere ich die gesamte Dateiliste?")

        MenuStrip1 = New MenuStrip()
        DateiToolStripMenuItem = New ToolStripMenuItem()
        SchließenToolStripMenuItem = New ToolStripMenuItem()
        IndexBox = New ToolStripComboBox()
        IndexView = New TreeView()
        FaqText = New RichTextBox()

        ' Initialisiere das Dictionary für FAQ-Inhalte
        faqContent = New Dictionary(Of String, String)()

        MenuStrip1.SuspendLayout()
        SuspendLayout()

        '
        ' MenuStrip1
        '
        MenuStrip1.Items.AddRange(New ToolStripItem() {DateiToolStripMenuItem, IndexBox})
        MenuStrip1.Location = New Point(0, 0)
        MenuStrip1.Name = "MenuStrip1"
        MenuStrip1.Size = New Size(800, 27)
        MenuStrip1.TabIndex = 0
        MenuStrip1.Text = "MenuStrip1"
        '
        ' DateiToolStripMenuItem
        '
        DateiToolStripMenuItem.DropDownItems.AddRange(New ToolStripItem() {SchließenToolStripMenuItem})
        DateiToolStripMenuItem.Image = CType(resources.GetObject("DateiToolStripMenuItem.Image"), Image)
        DateiToolStripMenuItem.Name = "DateiToolStripMenuItem"
        DateiToolStripMenuItem.Size = New Size(62, 23)
        DateiToolStripMenuItem.Text = "Datei"
        '
        ' SchließenToolStripMenuItem
        '
        SchließenToolStripMenuItem.Image = CType(resources.GetObject("SchließenToolStripMenuItem.Image"), Image)
        SchließenToolStripMenuItem.Name = "SchließenToolStripMenuItem"
        SchließenToolStripMenuItem.Size = New Size(180, 22)
        SchließenToolStripMenuItem.Text = "Schließen"
        ' Ereignishandler für Schließen hinzufügen
        AddHandler SchließenToolStripMenuItem.Click, AddressOf Me.SchließenToolStripMenuItem_Click
        '
        ' IndexBox
        '
        IndexBox.Name = "IndexBox"
        IndexBox.Size = New Size(121, 23)
        IndexBox.Text = "Inhalt:"
        IndexBox.DropDownStyle = ComboBoxStyle.DropDownList ' Nur Auswahl, keine Eingabe
        ' Ereignishandler für IndexBox hinzufügen
        AddHandler IndexBox.SelectedIndexChanged, AddressOf Me.IndexBox_SelectedIndexChanged
        '
        ' IndexView
        '
        IndexView.Location = New Point(0, 30)
        IndexView.Name = "IndexView"
        ' Node Properties und Hinzufügen der Nodes
        TreeNode1.ForeColor = Color.SteelBlue
        TreeNode1.Name = "index"
        TreeNode1.Text = "Inhalt:"
        TreeNode2.Name = "i1"
        TreeNode2.Text = "1. Was ist FastArchiver?"
        TreeNode3.Name = "i2-1"
        TreeNode3.Text = "Drag & Drop.."
        TreeNode3.ToolTipText = "Drag & Drop"
        TreeNode4.Name = "i2-2"
        TreeNode4.Text = "Dateien auswählen.."
        TreeNode5.Name = "i2"
        TreeNode5.Text = "2. Wie füge ich Dateien oder Ordner zur Liste hinzu?"
        TreeNode6.Name = "i3"
        TreeNode6.Text = "3. Wie erstelle ich ein ZIP-Archiv?"
        TreeNode7.Name = "i4" ' Neuer Node
        TreeNode7.Text = "4. Wie öffne und sehe ich den Inhalt eines ZIP-Archivs?"
        TreeNode8.Name = "i5" ' Neuer Node
        TreeNode8.Text = "5. Wie entpacke ich Dateien aus einem ZIP-Archiv?"
        TreeNode9.Name = "i6" ' Neuer Node
        TreeNode9.Text = "6. Wie entferne ich Dateien aus der Liste?"
        TreeNode10.Name = "i7" ' Neuer Node
        TreeNode10.Text = "7. Wie ändere ich die Einstellungen (Farbe, Schriftart)?"
        TreeNode11.Name = "i8" ' Neuer Node
        TreeNode11.Text = "8. Warum benötigt die App Administratorrechte?"
        TreeNode12.Name = "i9" ' Neuer Node
        TreeNode12.Text = "9. Ich erhalte eine ""Zugriff verweigert""-Fehlermeldung beim Entpacken. Was kann ich tun?"
        TreeNode13.Name = "i10" ' Neuer Node
        TreeNode13.Text = "10. Wie leere ich die gesamte Dateiliste?"

        IndexView.Nodes.AddRange(New TreeNode() {TreeNode1, TreeNode2, TreeNode5, TreeNode6, TreeNode7, TreeNode8, TreeNode9, TreeNode10, TreeNode11, TreeNode12, TreeNode13})
        IndexView.Size = New Size(189, 512)
        IndexView.TabIndex = 1
        ' Ereignishandler für IndexView hinzufügen
        AddHandler IndexView.AfterSelect, AddressOf Me.IndexView_AfterSelect
        '
        ' FaqText
        '
        FaqText.Location = New Point(195, 30)
        FaqText.Name = "FaqText"
        FaqText.Size = New Size(593, 512)
        FaqText.TabIndex = 2
        FaqText.Text = ""
        FaqText.ReadOnly = True ' RichTextBox als ReadOnly setzen
        FaqText.ScrollBars = RichTextBoxScrollBars.Vertical ' Vertikale Scrollbalken aktivieren
        '
        ' FaqForm
        '
        AutoScaleDimensions = New SizeF(7.0F, 15.0F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(800, 624)
        Controls.Add(FaqText)
        Controls.Add(IndexView)
        Controls.Add(MenuStrip1)
        MainMenuStrip = MenuStrip1
        Name = "FaqForm"
        Text = "Häufig gestellte Fragen (FAQ) zu FastArchiver"
        MenuStrip1.ResumeLayout(False)
        MenuStrip1.PerformLayout()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    ' Dictionary zur Speicherung der FAQ-Inhalte
    Private faqContent As Dictionary(Of String, String)

    Private Sub FaqForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        InitializeFaqContent() ' FAQ-Inhalte initialisieren
        PopulateIndexBox()     ' IndexBox füllen

        ' Optional: Ersten FAQ-Eintrag beim Laden anzeigen
        If IndexView.Nodes.Count > 1 Then
            IndexView.SelectedNode = IndexView.Nodes(1) ' Wählt "1. Was ist FastArchiver?"
        End If
    End Sub

    Private Sub InitializeFaqContent()
        faqContent = New Dictionary(Of String, String)()

        faqContent.Add("i1", "FastArchiver ist eine Anwendung, die Ihnen hilft, Dateien und Ordner zu komprimieren (als ZIP-Archive) und Inhalte aus bestehenden ZIP-Archiven zu entpacken. Sie bietet eine einfache Drag & Drop-Funktionalität und eine übersichtliche Dateiverwaltung.")
        faqContent.Add("i2", "Sie können Dateien und Ordner auf zwei Arten hinzufügen:" & Environment.NewLine & Environment.NewLine &
                              "• Drag & Drop: Ziehen Sie einfach eine oder mehrere Dateien oder ganze Ordner direkt auf das FastArchiver-Fenster. Die Inhalte werden automatisch in der Dateiliste angezeigt." & Environment.NewLine & Environment.NewLine &
                              "• ""Dateien auswählen""-Button: Klicken Sie auf den Button ""Dateien auswählen"" (oder ähnlich), um einen Dateiauswahldialog zu öffnen. Hier können Sie manuell Dateien und Ordner zum Komprimieren auswählen.")
        faqContent.Add("i2-1", "Ziehen Sie einfach eine oder mehrere Dateien oder ganze Ordner direkt auf das FastArchiver-Fenster. Die Inhalte werden automatisch in der Dateiliste angezeigt.")
        faqContent.Add("i2-2", "Klicken Sie auf den Button ""Dateien auswählen"" (oder ähnlich), um einen Dateiauswahldialog zu öffnen. Hier können Sie manuell Dateien und Ordner zum Komprimieren auswählen.")
        faqContent.Add("i3", "1. Fügen Sie die gewünschten Dateien und/oder Ordner zur Liste hinzu (siehe Frage 2)." & Environment.NewLine &
                              "2. Stellen Sie sicher, dass die Checkboxen neben den Dateien, die Sie komprimieren möchten, aktiviert sind." & Environment.NewLine &
                              "3. Klicken Sie auf den ""Start""-Button." & Environment.NewLine &
                              "4. Wählen Sie im erscheinenden Dialog einen Speicherort und einen Namen für Ihr neues ZIP-Archiv." & Environment.NewLine &
                              "5. Bestätigen Sie mit ""Speichern"", um den Komprimierungsvorgang zu starten.")
        faqContent.Add("i4", "Sie können ein ZIP-Archiv auf zwei Arten öffnen:" & Environment.NewLine & Environment.NewLine &
                              "• Drag & Drop: Ziehen Sie eine ZIP-Datei direkt auf das FastArchiver-Fenster. Der Inhalt des Archivs wird automatisch in der Dateiliste angezeigt." & Environment.NewLine & Environment.NewLine &
                              "• ""Archiv öffnen""-Button: Klicken Sie auf den Button ""Archiv öffnen"" (oder ähnlich), um einen Dateiauswahldialog zu öffnen. Wählen Sie hier die ZIP-Datei aus, deren Inhalt Sie sehen möchten.")
        faqContent.Add("i5", "1. Öffnen Sie das gewünschte ZIP-Archiv (siehe Frage 4). Die enthaltenen Dateien werden in der Liste angezeigt." & Environment.NewLine &
                              "2. Wählen Sie die Dateien in der Liste aus, die Sie entpacken möchten (standardmäßig sind alle ausgewählt)." & Environment.NewLine &
                              "3. Klicken Sie auf den ""Entpacken""-Button (UnZipButton)." & Environment.NewLine &
                              "4. Wählen Sie im erscheinenden Dialog den Zielordner aus, in den die Dateien entpackt werden sollen." & Environment.NewLine &
                              "5. Bestätigen Sie, um den Entpackvorgang zu starten. Nach Abschluss wird der Zielordner im Windows Explorer geöffnet.")
        faqContent.Add("i6", "• Einzelne Dateien entfernen: Wählen Sie die Datei in der Liste aus, klicken Sie mit der rechten Maustaste und wählen Sie ""Entfernen""." & Environment.NewLine &
                              "• Alle Dateien entfernen: Klicken Sie auf den ""Leeren""-Button (oder ähnlich), um alle Einträge aus der Liste zu entfernen.")
        faqContent.Add("i7", "Klicken Sie im Menü auf ""Optionen"" (oder ""Einstellungen""), um den Einstellungsdialog zu öffnen. Hier können Sie:" & Environment.NewLine &
                              "• Die Hintergrundfarbe der Anwendung ändern." & Environment.NewLine &
                              "• Eine andere Schriftart für die Benutzeroberfläche auswählen." & Environment.NewLine &
                              "• Optionen für die Standardschriftart festlegen." & Environment.NewLine &
                              "Vergessen Sie nicht, auf ""OK"" zu klicken, um Ihre Änderungen zu speichern.")
        faqContent.Add("i8", "FastArchiver benötigt möglicherweise Administratorrechte, um Dateien in bestimmte geschützte Systemordner zu entpacken oder dort neue Ordner zu erstellen. Wenn dies der Fall ist, werden Sie von Windows über die Benutzerkontensteuerung (UAC) zur Bestätigung aufgefordert. Dies dient dem Schutz Ihres Systems.")
        faqContent.Add("i9", "Diese Meldung bedeutet, dass FastArchiver keine Berechtigung hat, in den ausgewählten Zielordner zu schreiben oder die ZIP-Datei zu lesen." & Environment.NewLine & Environment.NewLine &
                              "• Stellen Sie sicher, dass Sie Schreibberechtigungen für den Zielordner haben. Versuchen Sie, einen anderen Ordner zu wählen, z. B. Ihren ""Downloads""- oder ""Dokumente""-Ordner." & Environment.NewLine &
                              "• Stellen Sie sicher, dass die ZIP-Datei nicht von einem anderen Programm geöffnet oder gesperrt ist." & Environment.NewLine &
                              "• Wenn das Problem weiterhin besteht, versuchen Sie, FastArchiver als Administrator auszuführen (Rechtsklick auf die .exe-Datei -> ""Als Administrator ausführen"").")
        faqContent.Add("i10", "Klicken Sie auf den Button ""Leeren"" (oder ähnlich), um alle Dateien aus der Liste zu entfernen und die Anwendung für eine neue Aufgabe vorzubereiten.")
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
            If faqContent.ContainsKey(e.Node.Name) Then
                FaqText.Text = faqContent(e.Node.Name)
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