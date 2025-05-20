<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Dialog1
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Dialog1))
        TableLayoutPanel1 = New TableLayoutPanel()
        OK_Button = New Button()
        Label1 = New Label()
        Label2 = New Label()
        ColorDialog1 = New ColorDialog()
        Panel1 = New Panel()
        Panel2 = New Panel()
        PictureBox2 = New PictureBox()
        Label4 = New Label()
        LightTheme = New CheckBox()
        Label3 = New Label()
        DarkTheme = New CheckBox()
        Panel3 = New Panel()
        Label6 = New Label()
        Font2 = New CheckBox()
        Label5 = New Label()
        PictureBox1 = New PictureBox()
        Font1 = New CheckBox()
        FontDialog1 = New FontDialog()
        BackgroundWorker1 = New ComponentModel.BackgroundWorker()
        TableLayoutPanel1.SuspendLayout()
        Panel1.SuspendLayout()
        Panel2.SuspendLayout()
        CType(PictureBox2, ComponentModel.ISupportInitialize).BeginInit()
        Panel3.SuspendLayout()
        CType(PictureBox1, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' TableLayoutPanel1
        ' 
        TableLayoutPanel1.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        TableLayoutPanel1.ColumnCount = 2
        TableLayoutPanel1.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 50F))
        TableLayoutPanel1.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 50F))
        TableLayoutPanel1.Controls.Add(OK_Button, 1, 0)
        TableLayoutPanel1.Location = New Point(138, 252)
        TableLayoutPanel1.Margin = New Padding(5, 3, 5, 3)
        TableLayoutPanel1.Name = "TableLayoutPanel1"
        TableLayoutPanel1.RowCount = 1
        TableLayoutPanel1.RowStyles.Add(New RowStyle(SizeType.Percent, 50F))
        TableLayoutPanel1.Size = New Size(213, 41)
        TableLayoutPanel1.TabIndex = 0
        ' 
        ' OK_Button
        ' 
        OK_Button.Anchor = AnchorStyles.None
        OK_Button.AutoEllipsis = True
        OK_Button.BackColor = SystemColors.ControlLightLight
        OK_Button.Font = New Font("Arial", 9.75F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        OK_Button.Location = New Point(116, 5)
        OK_Button.Margin = New Padding(5, 3, 5, 3)
        OK_Button.Name = "OK_Button"
        OK_Button.Size = New Size(87, 31)
        OK_Button.TabIndex = 0
        OK_Button.Text = "S&peichern"
        OK_Button.UseVisualStyleBackColor = False
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Font = New Font("Arial", 11.25F, FontStyle.Underline, GraphicsUnit.Point, CByte(0))
        Label1.Location = New Point(-1, 0)
        Label1.Name = "Label1"
        Label1.Size = New Size(71, 17)
        Label1.TabIndex = 2
        Label1.Text = "Schriftart:"
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Font = New Font("Arial", 11.25F, FontStyle.Underline, GraphicsUnit.Point, CByte(0))
        Label2.Location = New Point(3, 6)
        Label2.Name = "Label2"
        Label2.Size = New Size(58, 17)
        Label2.TabIndex = 4
        Label2.Text = "Theme:"
        ' 
        ' Panel1
        ' 
        Panel1.BackColor = Color.Ivory
        Panel1.Controls.Add(Panel2)
        Panel1.Controls.Add(Panel3)
        Panel1.Location = New Point(7, 14)
        Panel1.Name = "Panel1"
        Panel1.Size = New Size(355, 236)
        Panel1.TabIndex = 5
        ' 
        ' Panel2
        ' 
        Panel2.BackColor = SystemColors.ControlLightLight
        Panel2.BorderStyle = BorderStyle.FixedSingle
        Panel2.Controls.Add(PictureBox2)
        Panel2.Controls.Add(Label2)
        Panel2.Controls.Add(Label4)
        Panel2.Controls.Add(LightTheme)
        Panel2.Controls.Add(Label3)
        Panel2.Controls.Add(DarkTheme)
        Panel2.Location = New Point(5, 127)
        Panel2.Name = "Panel2"
        Panel2.Size = New Size(342, 105)
        Panel2.TabIndex = 17
        ' 
        ' PictureBox2
        ' 
        PictureBox2.Image = CType(resources.GetObject("PictureBox2.Image"), Image)
        PictureBox2.Location = New Point(3, 26)
        PictureBox2.Name = "PictureBox2"
        PictureBox2.Size = New Size(60, 55)
        PictureBox2.SizeMode = PictureBoxSizeMode.Zoom
        PictureBox2.TabIndex = 8
        PictureBox2.TabStop = False
        ' 
        ' Label4
        ' 
        Label4.AutoSize = True
        Label4.BackColor = SystemColors.ControlLightLight
        Label4.Font = New Font("Arial", 9F, FontStyle.Italic, GraphicsUnit.Point, CByte(0))
        Label4.ForeColor = SystemColors.WindowText
        Label4.Location = New Point(134, 66)
        Label4.Name = "Label4"
        Label4.Size = New Size(198, 15)
        Label4.TabIndex = 14
        Label4.Text = "Dark Back Color / White Font Color"
        ' 
        ' LightTheme
        ' 
        LightTheme.AutoSize = True
        LightTheme.BackColor = SystemColors.ControlLightLight
        LightTheme.FlatStyle = FlatStyle.System
        LightTheme.Font = New Font("Arial", 10F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        LightTheme.Location = New Point(68, 26)
        LightTheme.Name = "LightTheme"
        LightTheme.Size = New Size(67, 21)
        LightTheme.TabIndex = 9
        LightTheme.Text = " Light"
        LightTheme.UseVisualStyleBackColor = False
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Font = New Font("Arial", 9F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label3.Location = New Point(134, 29)
        Label3.Name = "Label3"
        Label3.Size = New Size(94, 15)
        Label3.TabIndex = 13
        Label3.Text = "(default) Theme"
        ' 
        ' DarkTheme
        ' 
        DarkTheme.AutoSize = True
        DarkTheme.BackColor = SystemColors.WindowFrame
        DarkTheme.Font = New Font("Arial", 10F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        DarkTheme.ForeColor = Color.WhiteSmoke
        DarkTheme.Location = New Point(68, 61)
        DarkTheme.Name = "DarkTheme"
        DarkTheme.Size = New Size(60, 20)
        DarkTheme.TabIndex = 10
        DarkTheme.Text = " Dark"
        DarkTheme.UseVisualStyleBackColor = False
        ' 
        ' Panel3
        ' 
        Panel3.BackColor = SystemColors.ControlLight
        Panel3.BorderStyle = BorderStyle.FixedSingle
        Panel3.Controls.Add(Label6)
        Panel3.Controls.Add(Label1)
        Panel3.Controls.Add(Font2)
        Panel3.Controls.Add(Label5)
        Panel3.Controls.Add(PictureBox1)
        Panel3.Controls.Add(Font1)
        Panel3.Location = New Point(5, 12)
        Panel3.Name = "Panel3"
        Panel3.Size = New Size(342, 91)
        Panel3.TabIndex = 18
        ' 
        ' Label6
        ' 
        Label6.AutoSize = True
        Label6.Font = New Font("Bahnschrift SemiLight Condensed", 11F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label6.Location = New Point(147, 56)
        Label6.Name = "Label6"
        Label6.Size = New Size(98, 18)
        Label6.TabIndex = 16
        Label6.Text = "Modern Font Light"
        ' 
        ' Font2
        ' 
        Font2.AutoSize = True
        Font2.Font = New Font("Arial", 9.75F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Font2.Location = New Point(68, 57)
        Font2.Name = "Font2"
        Font2.Size = New Size(69, 20)
        Font2.TabIndex = 12
        Font2.Text = "Modern"
        Font2.UseVisualStyleBackColor = True
        ' 
        ' Label5
        ' 
        Label5.AutoSize = True
        Label5.Font = New Font("Arial", 9F, FontStyle.Italic, GraphicsUnit.Point, CByte(0))
        Label5.Location = New Point(147, 23)
        Label5.Name = "Label5"
        Label5.Size = New Size(114, 15)
        Label5.TabIndex = 15
        Label5.Text = "Default Font in Arial"
        ' 
        ' PictureBox1
        ' 
        PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), Image)
        PictureBox1.Location = New Point(4, 20)
        PictureBox1.Name = "PictureBox1"
        PictureBox1.Size = New Size(59, 57)
        PictureBox1.SizeMode = PictureBoxSizeMode.Zoom
        PictureBox1.TabIndex = 7
        PictureBox1.TabStop = False
        ' 
        ' Font1
        ' 
        Font1.AutoSize = True
        Font1.Font = New Font("Arial", 9.75F, FontStyle.Italic, GraphicsUnit.Point, CByte(0))
        Font1.Location = New Point(68, 20)
        Font1.Name = "Font1"
        Font1.Size = New Size(73, 20)
        Font1.TabIndex = 11
        Font1.Text = "Classic "
        Font1.UseVisualStyleBackColor = True
        ' 
        ' Dialog1
        ' 
        AcceptButton = OK_Button
        AutoScaleDimensions = New SizeF(8F, 17F)
        AutoScaleMode = AutoScaleMode.Font
        AutoSize = True
        AutoSizeMode = AutoSizeMode.GrowAndShrink
        AutoValidate = AutoValidate.EnableAllowFocusChange
        BackColor = SystemColors.ControlLightLight
        ClientSize = New Size(366, 295)
        ControlBox = False
        Controls.Add(TableLayoutPanel1)
        Controls.Add(Panel1)
        DoubleBuffered = True
        Font = New Font("Arial", 11.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        FormBorderStyle = FormBorderStyle.FixedDialog
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        Margin = New Padding(5, 3, 5, 3)
        MaximizeBox = False
        MinimizeBox = False
        Name = "Dialog1"
        ShowInTaskbar = False
        StartPosition = FormStartPosition.CenterParent
        Text = "Einstellungen"
        TableLayoutPanel1.ResumeLayout(False)
        Panel1.ResumeLayout(False)
        Panel2.ResumeLayout(False)
        Panel2.PerformLayout()
        CType(PictureBox2, ComponentModel.ISupportInitialize).EndInit()
        Panel3.ResumeLayout(False)
        Panel3.PerformLayout()
        CType(PictureBox1, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents OK_Button As System.Windows.Forms.Button
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents ColorDialog1 As ColorDialog
    Friend WithEvents Panel1 As Panel
    Friend WithEvents FontDialog1 As FontDialog
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents PictureBox2 As PictureBox
    Friend WithEvents Font2 As CheckBox
    Friend WithEvents Font1 As CheckBox
    Friend WithEvents DarkTheme As CheckBox
    Friend WithEvents LightTheme As CheckBox
    Friend WithEvents Label4 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Panel2 As Panel
    Friend WithEvents BackgroundWorker1 As System.ComponentModel.BackgroundWorker
    Friend WithEvents Panel3 As Panel

End Class
