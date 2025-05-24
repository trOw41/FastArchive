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
        components = New ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormFAQ))
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
        ToolStripTextBox1 = New ToolStripTextBox()
        IndexBox = New ToolStripComboBox()
        IndexView = New TreeView()
        ImageList1 = New ImageList(components)
        FaqText = New RichTextBox()
        MenuStrip1.SuspendLayout()
        SuspendLayout()
        ' 
        ' MenuStrip1
        ' 
        MenuStrip1.BackColor = SystemColors.ControlLight
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
        IndexView.BackColor = SystemColors.Control
        IndexView.Font = New Font("Arial", 10F, FontStyle.Italic Or FontStyle.Underline, GraphicsUnit.Point, CByte(1), True)
        IndexView.FullRowSelect = True
        IndexView.HotTracking = True
        IndexView.ImageIndex = 0
        IndexView.ImageList = ImageList1
        IndexView.Location = New Point(12, 30)
        IndexView.Name = "IndexView"
        TreeNode1.BackColor = SystemColors.Control
        TreeNode1.ForeColor = SystemColors.MenuText
        TreeNode1.Name = "index"
        TreeNode1.NodeFont = New Font("Arial", 8.25F, FontStyle.Bold Or FontStyle.Italic, GraphicsUnit.Point, CByte(0))
        TreeNode1.SelectedImageKey = "(Standard)"
        TreeNode1.StateImageKey = "(keine)"
        TreeNode1.Text = "Inhalt:"
        TreeNode2.Name = "i1"
        TreeNode2.Text = "1. Was ist FastArchiver?"
        TreeNode3.ImageIndex = -2
        TreeNode3.Name = "i2-1"
        TreeNode3.Text = "Drag & Drop.."
        TreeNode3.ToolTipText = "Drag & Drop"
        TreeNode4.Name = "i2-2"
        TreeNode4.Text = "Dateien auswählen.."
        TreeNode5.Name = "i2"
        TreeNode5.Text = "2. Wie füge ich Dateien oder Ordner zur Liste hinzu?"
        TreeNode6.Name = "i3"
        TreeNode6.Text = "3. Wie erstelle ich ein ZIP-Archiv?"
        TreeNode7.Name = "i4"
        TreeNode7.Text = "4. Wie öffne und sehe ich den Inhalt eines ZIP-Archivs?"
        TreeNode8.Name = "i5"
        TreeNode8.Text = "5. Wie entpacke ich Dateien aus einem ZIP-Archiv?"
        TreeNode9.Name = "i6"
        TreeNode9.Text = "6. Wie entferne ich Dateien aus der Liste?"
        TreeNode10.Name = "i7"
        TreeNode10.Text = "7. Wie ändere ich die Einstellungen (Farbe, Schriftart)?"
        TreeNode11.Name = "i8"
        TreeNode11.Text = "8. Warum benötigt die App Administratorrechte?"
        TreeNode12.Name = "i9"
        TreeNode12.Text = "9. Ich erhalte eine ""Zugriff verweigert""-Fehlermeldung beim Entpacken. Was kann ich tun?"
        TreeNode13.Name = "i10"
        TreeNode13.Text = "10. Wie leere ich die gesamte Dateiliste?"
        IndexView.Nodes.AddRange(New TreeNode() {TreeNode1, TreeNode2, TreeNode5, TreeNode6, TreeNode7, TreeNode8, TreeNode9, TreeNode10, TreeNode11, TreeNode12, TreeNode13})
        IndexView.SelectedImageIndex = 1
        IndexView.ShowNodeToolTips = True
        IndexView.ShowPlusMinus = False
        IndexView.Size = New Size(222, 493)
        IndexView.StateImageList = ImageList1
        IndexView.TabIndex = 1
        ' 
        ' ImageList1
        ' 
        ImageList1.ColorDepth = ColorDepth.Depth32Bit
        ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), ImageListStreamer)
        ImageList1.TransparentColor = Color.Transparent
        ImageList1.Images.SetKeyName(0, "MapClose.ico")
        ImageList1.Images.SetKeyName(1, "MapOpen.ico")
        ' 
        ' FaqText
        ' 
        FaqText.BackColor = Color.MintCream
        FaqText.Font = New Font("Arial", 11F)
        FaqText.Location = New Point(240, 30)
        FaqText.Name = "FaqText"
        FaqText.Size = New Size(489, 493)
        FaqText.TabIndex = 0
        FaqText.Text = ""
        ' 
        ' FormFAQ
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        AutoSize = True
        BackColor = SystemColors.ControlLight
        ClientSize = New Size(741, 541)
        Controls.Add(FaqText)
        Controls.Add(IndexView)
        Controls.Add(MenuStrip1)
        DoubleBuffered = True
        FormBorderStyle = FormBorderStyle.Fixed3D
        HelpButton = True
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        MainMenuStrip = MenuStrip1
        MaximizeBox = False
        MinimizeBox = False
        Name = "FormFAQ"
        Text = "Häufig gestellte Fragen (FAQ) zu FastArchiver"
        MenuStrip1.ResumeLayout(False)
        MenuStrip1.PerformLayout()
        ResumeLayout(False)
        PerformLayout()


    End Sub

    Private WithEvents FaqText As RichTextBox
    Friend WithEvents ToolStripTextBox1 As ToolStripTextBox
    Friend WithEvents ImageList1 As ImageList
End Class
