<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormFAQ
    Inherits System.Windows.Forms.Form

    'Das Formular überschreibt den Löschvorgang, um die Komponentenliste zu bereinigen.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    Private components As System.ComponentModel.IContainer
    Private MenuStrip1 As MenuStrip
    Private WithEvents DateiToolStripMenuItem As ToolStripMenuItem
    Private WithEvents SchließenToolStripMenuItem As ToolStripMenuItem
    Private WithEvents IndexBox As ToolStripComboBox
    Private WithEvents IndexView As TreeView

    'Hinweis: Die folgende Prozedur ist für den Windows Form-Designer erforderlich.
    'Das Bearbeiten ist mit dem Windows Form-Designer möglich.  
    'Das Bearbeiten mit dem Code-Editor ist nicht möglich.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormFAQ))
        Dim TreeNode14 As TreeNode = New TreeNode("Inhalt:")
        Dim TreeNode15 As TreeNode = New TreeNode("1. Was ist FastArchiver?")
        Dim TreeNode16 As TreeNode = New TreeNode("Drag & Drop..")
        Dim TreeNode17 As TreeNode = New TreeNode("Dateien auswählen..")
        Dim TreeNode18 As TreeNode = New TreeNode("2. Wie füge ich Dateien oder Ordner zur Liste hinzu?", New TreeNode() {TreeNode16, TreeNode17})
        Dim TreeNode19 As TreeNode = New TreeNode("3. Wie erstelle ich ein ZIP-Archiv?")
        Dim TreeNode20 As TreeNode = New TreeNode("4. Wie öffne und sehe ich den Inhalt eines ZIP-Archivs?")
        Dim TreeNode21 As TreeNode = New TreeNode("5. Wie entpacke ich Dateien aus einem ZIP-Archiv?")
        Dim TreeNode22 As TreeNode = New TreeNode("6. Wie entferne ich Dateien aus der Liste?")
        Dim TreeNode23 As TreeNode = New TreeNode("7. Wie ändere ich die Einstellungen (Farbe, Schriftart)?")
        Dim TreeNode24 As TreeNode = New TreeNode("8. Warum benötigt die App Administratorrechte?")
        Dim TreeNode25 As TreeNode = New TreeNode("9. Ich erhalte eine ""Zugriff verweigert""-Fehlermeldung beim Entpacken. Was kann ich tun?")
        Dim TreeNode26 As TreeNode = New TreeNode("10. Wie leere ich die gesamte Dateiliste?")
        MenuStrip1 = New MenuStrip()
        DateiToolStripMenuItem = New ToolStripMenuItem()
        SchließenToolStripMenuItem = New ToolStripMenuItem()
        ToolStripTextBox1 = New ToolStripTextBox()
        IndexBox = New ToolStripComboBox()
        IndexView = New TreeView()
        FaqText = New RichTextBox()
        MenuStrip1.SuspendLayout()
        SuspendLayout()
        ' 
        ' MenuStrip1
        ' 
        MenuStrip1.Font = New Font("Arial", 9F)
        MenuStrip1.Items.AddRange(New ToolStripItem() {DateiToolStripMenuItem, ToolStripTextBox1, IndexBox})
        MenuStrip1.Location = New Point(0, 0)
        MenuStrip1.Name = "MenuStrip1"
        MenuStrip1.Size = New Size(741, 27)
        MenuStrip1.TabIndex = 0
        MenuStrip1.Text = "MenuStrip1"
        ' 
        ' DateiToolStripMenuItem
        ' 
        DateiToolStripMenuItem.DropDownItems.AddRange(New ToolStripItem() {SchließenToolStripMenuItem})
        DateiToolStripMenuItem.Image = CType(resources.GetObject("DateiToolStripMenuItem.Image"), Image)
        DateiToolStripMenuItem.Name = "DateiToolStripMenuItem"
        DateiToolStripMenuItem.Size = New Size(64, 23)
        DateiToolStripMenuItem.Text = "Datei"
        ' 
        ' SchließenToolStripMenuItem
        ' 
        SchließenToolStripMenuItem.Image = CType(resources.GetObject("SchließenToolStripMenuItem.Image"), Image)
        SchließenToolStripMenuItem.Name = "SchließenToolStripMenuItem"
        SchließenToolStripMenuItem.Size = New Size(130, 22)
        SchließenToolStripMenuItem.Text = "Schließen"
        ' 
        ' ToolStripTextBox1
        ' 
        ToolStripTextBox1.BackColor = SystemColors.Control
        ToolStripTextBox1.Name = "ToolStripTextBox1"
        ToolStripTextBox1.ReadOnly = True
        ToolStripTextBox1.Size = New Size(50, 23)
        ToolStripTextBox1.Text = "Inhalt:"
        ' 
        ' IndexBox
        ' 
        IndexBox.DropDownStyle = ComboBoxStyle.DropDownList
        IndexBox.Name = "IndexBox"
        IndexBox.Size = New Size(121, 23)
        ' 
        ' IndexView
        ' 
        IndexView.Location = New Point(12, 39)
        IndexView.Name = "IndexView"
        TreeNode14.ForeColor = Color.SteelBlue
        TreeNode14.Name = "index"
        TreeNode14.Text = "Inhalt:"
        TreeNode15.Name = "i1"
        TreeNode15.Text = "1. Was ist FastArchiver?"
        TreeNode16.Name = "i2-1"
        TreeNode16.Text = "Drag & Drop.."
        TreeNode16.ToolTipText = "Drag & Drop"
        TreeNode17.Name = "i2-2"
        TreeNode17.Text = "Dateien auswählen.."
        TreeNode18.Name = "i2"
        TreeNode18.Text = "2. Wie füge ich Dateien oder Ordner zur Liste hinzu?"
        TreeNode19.Name = "i3"
        TreeNode19.Text = "3. Wie erstelle ich ein ZIP-Archiv?"
        TreeNode20.Name = "i4"
        TreeNode20.Text = "4. Wie öffne und sehe ich den Inhalt eines ZIP-Archivs?"
        TreeNode21.Name = "i5"
        TreeNode21.Text = "5. Wie entpacke ich Dateien aus einem ZIP-Archiv?"
        TreeNode22.Name = "i6"
        TreeNode22.Text = "6. Wie entferne ich Dateien aus der Liste?"
        TreeNode23.Name = "i7"
        TreeNode23.Text = "7. Wie ändere ich die Einstellungen (Farbe, Schriftart)?"
        TreeNode24.Name = "i8"
        TreeNode24.Text = "8. Warum benötigt die App Administratorrechte?"
        TreeNode25.Name = "i9"
        TreeNode25.Text = "9. Ich erhalte eine ""Zugriff verweigert""-Fehlermeldung beim Entpacken. Was kann ich tun?"
        TreeNode26.Name = "i10"
        TreeNode26.Text = "10. Wie leere ich die gesamte Dateiliste?"
        IndexView.Nodes.AddRange(New TreeNode() {TreeNode14, TreeNode15, TreeNode18, TreeNode19, TreeNode20, TreeNode21, TreeNode22, TreeNode23, TreeNode24, TreeNode25, TreeNode26})
        IndexView.Size = New Size(222, 493)
        IndexView.TabIndex = 1
        ' 
        ' FaqText
        ' 
        FaqText.Location = New Point(240, 39)
        FaqText.Name = "FaqText"
        FaqText.Size = New Size(489, 493)
        FaqText.TabIndex = 0
        FaqText.Text = ""
        ' 
        ' FormFAQ
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(741, 541)
        Controls.Add(FaqText)
        Controls.Add(IndexView)
        Controls.Add(MenuStrip1)
        HelpButton = True
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        MainMenuStrip = MenuStrip1
        Name = "FormFAQ"
        Text = "Häufig gestellte Fragen (FAQ) zu FastArchiver"
        MenuStrip1.ResumeLayout(False)
        MenuStrip1.PerformLayout()
        ResumeLayout(False)
        PerformLayout()


    End Sub

    Private WithEvents FaqText As RichTextBox
    Friend WithEvents ToolStripTextBox1 As ToolStripTextBox
End Class
