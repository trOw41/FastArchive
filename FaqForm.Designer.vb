<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FaqForm
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

    'Hinweis: Die folgende Prozedur ist für den Windows Form-Designer erforderlich.
    'Das Bearbeiten ist mit dem Windows Form-Designer möglich.  
    'Das Bearbeiten mit dem Code-Editor ist nicht möglich.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FaqForm))
        Dim TreeNode1 As TreeNode = New TreeNode("Inhalt:")
        Dim TreeNode2 As TreeNode = New TreeNode("1. Was ist FastArchiver?")
        Dim TreeNode3 As TreeNode = New TreeNode("Drag & Drop..")
        Dim TreeNode4 As TreeNode = New TreeNode("Dateien auswählen..")
        Dim TreeNode5 As TreeNode = New TreeNode("2. Wie füge ich Dateien oder Ordner zur Liste hinzu?", New TreeNode() {TreeNode3, TreeNode4})
        Dim TreeNode6 As TreeNode = New TreeNode("3. Wie erstelle ich ein ZIP-Archiv?")
        MenuStrip1 = New MenuStrip()
        DateiToolStripMenuItem = New ToolStripMenuItem()
        SchließenToolStripMenuItem = New ToolStripMenuItem()
        IndexBox = New ToolStripComboBox()
        IndexView = New TreeView()
        FaqText = New RichTextBox()
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
        ' 
        ' IndexBox
        ' 
        IndexBox.Name = "IndexBox"
        IndexBox.Size = New Size(121, 23)
        IndexBox.Text = "Inhalt:"
        ' 
        ' IndexView
        ' 
        IndexView.Location = New Point(0, 30)
        IndexView.Name = "IndexView"
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
        IndexView.Nodes.AddRange(New TreeNode() {TreeNode1, TreeNode2, TreeNode5, TreeNode6})
        IndexView.Size = New Size(189, 512)
        IndexView.TabIndex = 1
        ' 
        ' FaqText
        ' 
        FaqText.Location = New Point(195, 30)
        FaqText.Name = "FaqText"
        FaqText.Size = New Size(593, 512)
        FaqText.TabIndex = 2
        FaqText.Text = ""
        ' 
        ' FaqForm
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
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

    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents DateiToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents SchließenToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents IndexBox As ToolStripComboBox
    Friend WithEvents IndexView As TreeView
    Friend WithEvents FaqText As RichTextBox
End Class
