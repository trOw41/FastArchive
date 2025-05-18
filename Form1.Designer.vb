'Imports FastArchiver.FastArchiver
Imports Windows.System


<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
    Inherits System.Windows.Forms.Form

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
    Private components As System.ComponentModel.IContainer

    'Hinweis: Die folgende Prozedur ist für den Windows Form-Designer erforderlich.
    'Das Bearbeiten ist mit dem Windows Form-Designer möglich.  
    'Das Bearbeiten mit dem Code-Editor ist nicht möglich.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        components = New ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        DataSet1 = New DataSet()
        ItemNo = New Label()
        FileList = New ListView()
        FileListContextMenuStrip = New ContextMenuStrip(components)
        RemoveToolStripMenuItem = New ToolStripMenuItem()
        SizeText = New Label()
        StatusText = New Label()
        MenuStrip1 = New MenuStrip()
        FIleToolStripMenuItem = New ToolStripMenuItem()
        ToolStripMenuItem1 = New ToolStripMenuItem()
        ToolStripSeparator1 = New ToolStripSeparator()
        ExitToolStripMenuItem = New ToolStripMenuItem()
        HelpToolStripMenuItem = New ToolStripMenuItem()
        FAQToolStripMenuItem = New ToolStripMenuItem()
        OptionsToolStripMenuItem = New ToolStripMenuItem()
        InfoToolStripMenuItem = New ToolStripMenuItem()
        ZipFormatButton = New RadioButton()
        Button1 = New Button()
        ProgressBar1 = New ProgressBar()
        CheckBox1 = New CheckBox()
        OpenArchiv = New Button()
        StartButton = New Button()
        SelectButton = New Button()
        OpenZip = New OpenFileDialog()
        FolderBrowserDialog1 = New FolderBrowserDialog()
        NotifyIcon1 = New NotifyIcon(components)
        Process1 = New Process()
        RadioButton1 = New RadioButton()
        FileListIconList = New ImageList(components)
        ToolTip1 = New ToolTip(components)
        UnZipButton = New Button()
        CType(DataSet1, ComponentModel.ISupportInitialize).BeginInit()
        FileListContextMenuStrip.SuspendLayout()
        MenuStrip1.SuspendLayout()
        SuspendLayout()
        ' 
        ' DataSet1
        ' 
        DataSet1.DataSetName = "NewDataSet"
        ' 
        ' ItemNo
        ' 
        resources.ApplyResources(ItemNo, "ItemNo")
        ItemNo.FlatStyle = FlatStyle.Popup
        ItemNo.Name = "ItemNo"
        ToolTip1.SetToolTip(ItemNo, resources.GetString("ItemNo.ToolTip"))
        ' 
        ' FileList
        ' 
        resources.ApplyResources(FileList, "FileList")
        FileList.BackColor = SystemColors.GradientInactiveCaption
        FileList.CheckBoxes = True
        FileList.ContextMenuStrip = FileListContextMenuStrip
        FileList.FullRowSelect = True
        FileList.GridLines = True
        FileList.Name = "FileList"
        ToolTip1.SetToolTip(FileList, resources.GetString("FileList.ToolTip"))
        FileList.UseCompatibleStateImageBehavior = False
        ' 
        ' FileListContextMenuStrip
        ' 
        resources.ApplyResources(FileListContextMenuStrip, "FileListContextMenuStrip")
        FileListContextMenuStrip.Items.AddRange(New ToolStripItem() {RemoveToolStripMenuItem})
        FileListContextMenuStrip.Name = "ContextMenuStrip1"
        FileListContextMenuStrip.RenderMode = ToolStripRenderMode.Professional
        ToolTip1.SetToolTip(FileListContextMenuStrip, resources.GetString("FileListContextMenuStrip.ToolTip"))
        ' 
        ' RemoveToolStripMenuItem
        ' 
        resources.ApplyResources(RemoveToolStripMenuItem, "RemoveToolStripMenuItem")
        RemoveToolStripMenuItem.Name = "RemoveToolStripMenuItem"
        ' 
        ' SizeText
        ' 
        resources.ApplyResources(SizeText, "SizeText")
        SizeText.Name = "SizeText"
        ToolTip1.SetToolTip(SizeText, resources.GetString("SizeText.ToolTip"))
        ' 
        ' StatusText
        ' 
        resources.ApplyResources(StatusText, "StatusText")
        StatusText.Name = "StatusText"
        ToolTip1.SetToolTip(StatusText, resources.GetString("StatusText.ToolTip"))
        ' 
        ' MenuStrip1
        ' 
        resources.ApplyResources(MenuStrip1, "MenuStrip1")
        MenuStrip1.GripMargin = New Padding(0)
        MenuStrip1.GripStyle = ToolStripGripStyle.Visible
        MenuStrip1.Items.AddRange(New ToolStripItem() {FIleToolStripMenuItem, HelpToolStripMenuItem, InfoToolStripMenuItem})
        MenuStrip1.Name = "MenuStrip1"
        MenuStrip1.RenderMode = ToolStripRenderMode.Professional
        MenuStrip1.ShowItemToolTips = True
        ToolTip1.SetToolTip(MenuStrip1, resources.GetString("MenuStrip1.ToolTip"))
        ' 
        ' FIleToolStripMenuItem
        ' 
        resources.ApplyResources(FIleToolStripMenuItem, "FIleToolStripMenuItem")
        FIleToolStripMenuItem.DropDownItems.AddRange(New ToolStripItem() {ToolStripMenuItem1, ToolStripSeparator1, ExitToolStripMenuItem})
        FIleToolStripMenuItem.Name = "FIleToolStripMenuItem"
        FIleToolStripMenuItem.Overflow = ToolStripItemOverflow.AsNeeded
        ' 
        ' ToolStripMenuItem1
        ' 
        resources.ApplyResources(ToolStripMenuItem1, "ToolStripMenuItem1")
        ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        ' 
        ' ToolStripSeparator1
        ' 
        resources.ApplyResources(ToolStripSeparator1, "ToolStripSeparator1")
        ToolStripSeparator1.Name = "ToolStripSeparator1"
        ' 
        ' ExitToolStripMenuItem
        ' 
        resources.ApplyResources(ExitToolStripMenuItem, "ExitToolStripMenuItem")
        ExitToolStripMenuItem.Name = "ExitToolStripMenuItem"
        ' 
        ' HelpToolStripMenuItem
        ' 
        resources.ApplyResources(HelpToolStripMenuItem, "HelpToolStripMenuItem")
        HelpToolStripMenuItem.DropDownItems.AddRange(New ToolStripItem() {FAQToolStripMenuItem, OptionsToolStripMenuItem})
        HelpToolStripMenuItem.Name = "HelpToolStripMenuItem"
        ' 
        ' FAQToolStripMenuItem
        ' 
        resources.ApplyResources(FAQToolStripMenuItem, "FAQToolStripMenuItem")
        FAQToolStripMenuItem.Name = "FAQToolStripMenuItem"
        ' 
        ' OptionsToolStripMenuItem
        ' 
        resources.ApplyResources(OptionsToolStripMenuItem, "OptionsToolStripMenuItem")
        OptionsToolStripMenuItem.Name = "OptionsToolStripMenuItem"
        ' 
        ' InfoToolStripMenuItem
        ' 
        resources.ApplyResources(InfoToolStripMenuItem, "InfoToolStripMenuItem")
        InfoToolStripMenuItem.Name = "InfoToolStripMenuItem"
        ' 
        ' ZipFormatButton
        ' 
        resources.ApplyResources(ZipFormatButton, "ZipFormatButton")
        ZipFormatButton.Name = "ZipFormatButton"
        ZipFormatButton.TabStop = True
        ToolTip1.SetToolTip(ZipFormatButton, resources.GetString("ZipFormatButton.ToolTip"))
        ZipFormatButton.UseVisualStyleBackColor = True
        ' 
        ' Button1
        ' 
        resources.ApplyResources(Button1, "Button1")
        Button1.Name = "Button1"
        ToolTip1.SetToolTip(Button1, resources.GetString("Button1.ToolTip"))
        Button1.UseVisualStyleBackColor = True
        ' 
        ' ProgressBar1
        ' 
        resources.ApplyResources(ProgressBar1, "ProgressBar1")
        ProgressBar1.ForeColor = SystemColors.HotTrack
        ProgressBar1.Name = "ProgressBar1"
        ProgressBar1.Step = 1
        ToolTip1.SetToolTip(ProgressBar1, resources.GetString("ProgressBar1.ToolTip"))
        ProgressBar1.Value = 10
        ' 
        ' CheckBox1
        ' 
        resources.ApplyResources(CheckBox1, "CheckBox1")
        CheckBox1.Name = "CheckBox1"
        ToolTip1.SetToolTip(CheckBox1, resources.GetString("CheckBox1.ToolTip"))
        CheckBox1.UseVisualStyleBackColor = True
        ' 
        ' OpenArchiv
        ' 
        resources.ApplyResources(OpenArchiv, "OpenArchiv")
        OpenArchiv.FlatAppearance.BorderSize = 0
        OpenArchiv.FlatAppearance.MouseDownBackColor = Color.Gray
        OpenArchiv.FlatAppearance.MouseOverBackColor = Color.FromArgb(CByte(255), CByte(128), CByte(0))
        OpenArchiv.Name = "OpenArchiv"
        ToolTip1.SetToolTip(OpenArchiv, resources.GetString("OpenArchiv.ToolTip"))
        OpenArchiv.UseVisualStyleBackColor = True
        ' 
        ' StartButton
        ' 
        resources.ApplyResources(StartButton, "StartButton")
        StartButton.FlatAppearance.BorderSize = 0
        StartButton.FlatAppearance.MouseDownBackColor = Color.Gray
        StartButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(CByte(255), CByte(128), CByte(0))
        StartButton.Name = "StartButton"
        ToolTip1.SetToolTip(StartButton, resources.GetString("StartButton.ToolTip"))
        StartButton.UseVisualStyleBackColor = True
        ' 
        ' SelectButton
        ' 
        resources.ApplyResources(SelectButton, "SelectButton")
        SelectButton.FlatAppearance.BorderSize = 0
        SelectButton.FlatAppearance.MouseDownBackColor = Color.Gray
        SelectButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(CByte(255), CByte(128), CByte(0))
        SelectButton.Name = "SelectButton"
        ToolTip1.SetToolTip(SelectButton, resources.GetString("SelectButton.ToolTip"))
        SelectButton.UseVisualStyleBackColor = True
        ' 
        ' OpenZip
        ' 
        OpenZip.FileName = "OpenFileDialog1"
        resources.ApplyResources(OpenZip, "OpenZip")
        ' 
        ' FolderBrowserDialog1
        ' 
        resources.ApplyResources(FolderBrowserDialog1, "FolderBrowserDialog1")
        ' 
        ' NotifyIcon1
        ' 
        NotifyIcon1.BalloonTipIcon = ToolTipIcon.Info
        resources.ApplyResources(NotifyIcon1, "NotifyIcon1")
        NotifyIcon1.ContextMenuStrip = FileListContextMenuStrip
        ' 
        ' Process1
        ' 
        Process1.StartInfo.Domain = ""
        Process1.StartInfo.LoadUserProfile = False
        Process1.StartInfo.Password = Nothing
        Process1.StartInfo.StandardErrorEncoding = Nothing
        Process1.StartInfo.StandardInputEncoding = Nothing
        Process1.StartInfo.StandardOutputEncoding = Nothing
        Process1.StartInfo.UseCredentialsForNetworkingOnly = False
        Process1.StartInfo.UserName = ""
        Process1.SynchronizingObject = Me
        ' 
        ' RadioButton1
        ' 
        resources.ApplyResources(RadioButton1, "RadioButton1")
        RadioButton1.Name = "RadioButton1"
        RadioButton1.TabStop = True
        ToolTip1.SetToolTip(RadioButton1, resources.GetString("RadioButton1.ToolTip"))
        RadioButton1.UseVisualStyleBackColor = True
        ' 
        ' FileListIconList
        ' 
        FileListIconList.ColorDepth = ColorDepth.Depth32Bit
        resources.ApplyResources(FileListIconList, "FileListIconList")
        FileListIconList.TransparentColor = Color.Transparent
        ' 
        ' ToolTip1
        ' 
        ToolTip1.AutoPopDelay = 5000
        ToolTip1.InitialDelay = 500
        ToolTip1.IsBalloon = True
        ToolTip1.OwnerDraw = True
        ToolTip1.ReshowDelay = 500
        ToolTip1.ShowAlways = True
        ToolTip1.ToolTipIcon = ToolTipIcon.Info
        ToolTip1.ToolTipTitle = "Quick Info:"
        ' 
        ' UnZipButton
        ' 
        resources.ApplyResources(UnZipButton, "UnZipButton")
        UnZipButton.Name = "UnZipButton"
        ToolTip1.SetToolTip(UnZipButton, resources.GetString("UnZipButton.ToolTip"))
        UnZipButton.UseVisualStyleBackColor = True
        ' 
        ' Form1
        ' 
        resources.ApplyResources(Me, "$this")
        AutoScaleMode = AutoScaleMode.Dpi
        ContextMenuStrip = FileListContextMenuStrip
        Controls.Add(UnZipButton)
        Controls.Add(RadioButton1)
        Controls.Add(CheckBox1)
        Controls.Add(Button1)
        Controls.Add(ZipFormatButton)
        Controls.Add(StartButton)
        Controls.Add(SelectButton)
        Controls.Add(SizeText)
        Controls.Add(ItemNo)
        Controls.Add(MenuStrip1)
        Controls.Add(OpenArchiv)
        Controls.Add(FileList)
        Controls.Add(ProgressBar1)
        Controls.Add(StatusText)
        FormBorderStyle = FormBorderStyle.Fixed3D
        HelpButton = True
        KeyPreview = True
        MainMenuStrip = MenuStrip1
        MaximizeBox = False
        Name = "Form1"
        ToolTip1.SetToolTip(Me, resources.GetString("$this.ToolTip"))
        CType(DataSet1, ComponentModel.ISupportInitialize).EndInit()
        FileListContextMenuStrip.ResumeLayout(False)
        MenuStrip1.ResumeLayout(False)
        MenuStrip1.PerformLayout()
        ResumeLayout(False)
        PerformLayout()

    End Sub

    Friend WithEvents DataSet1 As DataSet
    Friend WithEvents ItemNo As Label
    Friend WithEvents FileList As ListView
    Friend WithEvents SizeText As Label
    Friend WithEvents StatusText As Label
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents FIleToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ExitToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents HelpToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents FAQToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents InfoToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents SelectButton As Button
    Friend WithEvents StartButton As Button
    Friend WithEvents ZipFormatButton As RadioButton
    Friend WithEvents Button1 As Button
    Friend WithEvents FileListContextMenuStrip As ContextMenuStrip
    Friend WithEvents RemoveToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ProgressBar1 As ProgressBar
    Friend WithEvents CheckBox1 As CheckBox
    Friend WithEvents OpenArchiv As Button
    Friend WithEvents OpenZip As OpenFileDialog
    Friend WithEvents FolderBrowserDialog1 As FolderBrowserDialog
    Friend WithEvents NotifyIcon1 As NotifyIcon
    Friend WithEvents Process1 As Process
    Friend WithEvents OptionsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents RadioButton1 As RadioButton
    Friend WithEvents FileListIconList As ImageList
    Friend WithEvents ToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As ToolStripSeparator
    Friend WithEvents UnZipButton As Button
    Public WithEvents ToolTip1 As ToolTip
End Class

