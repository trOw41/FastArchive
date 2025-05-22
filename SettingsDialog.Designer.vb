<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SettingsDialog
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(SettingsDialog))
        OK_Button = New Button()
        Label1 = New Label()
        Label2 = New Label()
        ColorDialog1 = New ColorDialog()
        Panel1 = New Panel()
        RadioButton2 = New RadioButton()
        RadioButton3 = New RadioButton()
        RadioButton1 = New RadioButton()
        Label8 = New Label()
        PictureBox3 = New PictureBox()
        Label5 = New Label()
        Label6 = New Label()
        Font2 = New CheckBox()
        PictureBox1 = New PictureBox()
        Panel2 = New Panel()
        PictureBox2 = New PictureBox()
        Label4 = New Label()
        LightT = New CheckBox()
        Label3 = New Label()
        DarkT = New CheckBox()
        Font1 = New CheckBox()
        FontDialog1 = New FontDialog()
        BackgroundWorker1 = New ComponentModel.BackgroundWorker()
        Panel1.SuspendLayout()
        CType(PictureBox3, ComponentModel.ISupportInitialize).BeginInit()
        CType(PictureBox1, ComponentModel.ISupportInitialize).BeginInit()
        Panel2.SuspendLayout()
        CType(PictureBox2, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' OK_Button
        ' 
        OK_Button.Anchor = AnchorStyles.None
        OK_Button.AutoEllipsis = True
        OK_Button.BackColor = SystemColors.ControlLightLight
        OK_Button.Font = New Font("Arial", 9.75F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        OK_Button.Location = New Point(278, 301)
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
        Label1.Font = New Font("Arial", 10.0F, FontStyle.Underline)
        Label1.Location = New Point(5, 1)
        Label1.Name = "Label1"
        Label1.Size = New Size(68, 16)
        Label1.TabIndex = 2
        Label1.Text = "Schriftart:"
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Font = New Font("Arial", 10.0F, FontStyle.Underline)
        Label2.Location = New Point(-1, 0)
        Label2.Name = "Label2"
        Label2.Size = New Size(55, 16)
        Label2.TabIndex = 4
        Label2.Text = "Theme:"
        ' 
        ' Panel1
        ' 
        Panel1.BackColor = Color.Transparent
        Panel1.Controls.Add(RadioButton2)
        Panel1.Controls.Add(RadioButton3)
        Panel1.Controls.Add(RadioButton1)
        Panel1.Controls.Add(Label8)
        Panel1.Controls.Add(PictureBox3)
        Panel1.Controls.Add(Label5)
        Panel1.Controls.Add(Label1)
        Panel1.Controls.Add(Label6)
        Panel1.Controls.Add(Font2)
        Panel1.Controls.Add(PictureBox1)
        Panel1.Controls.Add(Panel2)
        Panel1.Controls.Add(Font1)
        Panel1.Location = New Point(7, 12)
        Panel1.Name = "Panel1"
        Panel1.Size = New Size(371, 258)
        Panel1.TabIndex = 5
        ' 
        ' RadioButton2
        ' 
        RadioButton2.AutoSize = True
        RadioButton2.Font = New Font("Microsoft Sans Serif", 9.75F)
        RadioButton2.Location = New Point(55, 188)
        RadioButton2.Name = "RadioButton2"
        RadioButton2.Size = New Size(135, 20)
        RadioButton2.TabIndex = 20
        RadioButton2.TabStop = True
        RadioButton2.Text = "Standard (optimal)"
        RadioButton2.UseVisualStyleBackColor = True
        ' 
        ' RadioButton3
        ' 
        RadioButton3.AutoSize = True
        RadioButton3.Font = New Font("Microsoft Sans Serif", 9.75F)
        RadioButton3.Location = New Point(55, 224)
        RadioButton3.Name = "RadioButton3"
        RadioButton3.Size = New Size(149, 20)
        RadioButton3.TabIndex = 20
        RadioButton3.TabStop = True
        RadioButton3.Text = "Ultra (mehr CPU last)"
        RadioButton3.UseVisualStyleBackColor = True
        ' 
        ' RadioButton1
        ' 
        RadioButton1.AutoSize = True
        RadioButton1.Font = New Font("Microsoft Sans Serif", 9.75F)
        RadioButton1.Location = New Point(220, 188)
        RadioButton1.Name = "RadioButton1"
        RadioButton1.Size = New Size(122, 20)
        RadioButton1.TabIndex = 20
        RadioButton1.TabStop = True
        RadioButton1.Text = "Fast (schnellste)"
        RadioButton1.UseVisualStyleBackColor = True
        ' 
        ' Label8
        ' 
        Label8.AutoSize = True
        Label8.Font = New Font("Arial", 11.25F, FontStyle.Underline, GraphicsUnit.Point, CByte(0))
        Label8.Location = New Point(3, 161)
        Label8.Name = "Label8"
        Label8.Size = New Size(143, 17)
        Label8.TabIndex = 19
        Label8.Text = "Archiv Kompression:"
        ' 
        ' PictureBox3
        ' 
        PictureBox3.Image = CType(resources.GetObject("PictureBox3.Image"), Image)
        PictureBox3.Location = New Point(3, 188)
        PictureBox3.Name = "PictureBox3"
        PictureBox3.Size = New Size(44, 45)
        PictureBox3.SizeMode = PictureBoxSizeMode.Zoom
        PictureBox3.TabIndex = 21
        PictureBox3.TabStop = False
        ' 
        ' Label5
        ' 
        Label5.AutoSize = True
        Label5.Font = New Font("Arial", 9.0F, FontStyle.Italic, GraphicsUnit.Point, CByte(0))
        Label5.Location = New Point(130, 48)
        Label5.Name = "Label5"
        Label5.Size = New Size(114, 15)
        Label5.TabIndex = 15
        Label5.Text = "Default Font in Arial"
        ' 
        ' Label6
        ' 
        Label6.AutoSize = True
        Label6.Font = New Font("Bahnschrift SemiLight Condensed", 10.0F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label6.Location = New Point(130, 21)
        Label6.Name = "Label6"
        Label6.Size = New Size(92, 17)
        Label6.TabIndex = 16
        Label6.Text = "Modern Font Light"
        ' 
        ' Font2
        ' 
        Font2.AutoSize = True
        Font2.Font = New Font("Bahnschrift SemiLight SemiConde", 9.75F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Font2.Location = New Point(55, 20)
        Font2.Name = "Font2"
        Font2.Size = New Size(64, 20)
        Font2.TabIndex = 12
        Font2.Text = "Modern"
        Font2.UseVisualStyleBackColor = True
        ' 
        ' PictureBox1
        ' 
        PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), Image)
        PictureBox1.Location = New Point(5, 21)
        PictureBox1.Name = "PictureBox1"
        PictureBox1.Size = New Size(44, 42)
        PictureBox1.SizeMode = PictureBoxSizeMode.Zoom
        PictureBox1.TabIndex = 7
        PictureBox1.TabStop = False
        ' 
        ' Panel2
        ' 
        Panel2.BackColor = SystemColors.ControlLightLight
        Panel2.BorderStyle = BorderStyle.FixedSingle
        Panel2.Controls.Add(PictureBox2)
        Panel2.Controls.Add(Label2)
        Panel2.Controls.Add(Label4)
        Panel2.Controls.Add(LightT)
        Panel2.Controls.Add(Label3)
        Panel2.Controls.Add(DarkT)
        Panel2.Location = New Point(3, 72)
        Panel2.Name = "Panel2"
        Panel2.Size = New Size(365, 86)
        Panel2.TabIndex = 17
        ' 
        ' PictureBox2
        ' 
        PictureBox2.Image = CType(resources.GetObject("PictureBox2.Image"), Image)
        PictureBox2.Location = New Point(1, 20)
        PictureBox2.Name = "PictureBox2"
        PictureBox2.Size = New Size(44, 47)
        PictureBox2.SizeMode = PictureBoxSizeMode.Zoom
        PictureBox2.TabIndex = 8
        PictureBox2.TabStop = False
        ' 
        ' Label4
        ' 
        Label4.AutoSize = True
        Label4.BackColor = SystemColors.ControlDarkDark
        Label4.Font = New Font("Arial", 9.75F)
        Label4.ForeColor = SystemColors.Control
        Label4.Location = New Point(126, 47)
        Label4.Name = "Label4"
        Label4.Size = New Size(212, 16)
        Label4.TabIndex = 14
        Label4.Text = "Dark Back Color / White Font Color"
        ' 
        ' LightT
        ' 
        LightT.AutoSize = True
        LightT.BackColor = SystemColors.ControlLightLight
        LightT.FlatStyle = FlatStyle.System
        LightT.Font = New Font("Arial", 10.0F)
        LightT.Location = New Point(51, 20)
        LightT.Name = "LightT"
        LightT.Size = New Size(67, 21)
        LightT.TabIndex = 9
        LightT.Text = " Light"
        LightT.UseVisualStyleBackColor = False
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Font = New Font("Arial", 10.0F)
        Label3.Location = New Point(124, 22)
        Label3.Name = "Label3"
        Label3.Size = New Size(108, 16)
        Label3.TabIndex = 13
        Label3.Text = "(default) Theme"
        ' 
        ' DarkT
        ' 
        DarkT.AutoSize = True
        DarkT.BackColor = SystemColors.WindowFrame
        DarkT.Font = New Font("Arial", 9.75F)
        DarkT.ForeColor = Color.WhiteSmoke
        DarkT.Location = New Point(51, 47)
        DarkT.Name = "DarkT"
        DarkT.Size = New Size(53, 20)
        DarkT.TabIndex = 10
        DarkT.Text = "Dark"
        DarkT.UseVisualStyleBackColor = False
        ' 
        ' Font1
        ' 
        Font1.AutoSize = True
        Font1.Font = New Font("Arial", 9.75F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Font1.Location = New Point(55, 46)
        Font1.Name = "Font1"
        Font1.Size = New Size(73, 20)
        Font1.TabIndex = 11
        Font1.Text = "Classic "
        Font1.UseVisualStyleBackColor = True
        ' 
        ' SettingsDialog
        ' 
        AcceptButton = OK_Button
        AutoScaleMode = AutoScaleMode.None
        AutoValidate = AutoValidate.EnableAllowFocusChange
        BackColor = SystemColors.ControlLightLight
        ClientSize = New Size(384, 361)
        ControlBox = False
        Controls.Add(OK_Button)
        Controls.Add(Panel1)
        Font = New Font("Arial", 11.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        FormBorderStyle = FormBorderStyle.FixedDialog
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        Margin = New Padding(5, 3, 5, 3)
        MaximizeBox = False
        MaximumSize = New Size(400, 400)
        MdiChildrenMinimizedAnchorBottom = False
        MinimizeBox = False
        MinimumSize = New Size(400, 400)
        Name = "SettingsDialog"
        ShowInTaskbar = False
        SizeGripStyle = SizeGripStyle.Show
        Text = "Einstellungen"
        Panel1.ResumeLayout(False)
        Panel1.PerformLayout()
        CType(PictureBox3, ComponentModel.ISupportInitialize).EndInit()
        CType(PictureBox1, ComponentModel.ISupportInitialize).EndInit()
        Panel2.ResumeLayout(False)
        Panel2.PerformLayout()
        CType(PictureBox2, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)

    End Sub
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
    Friend WithEvents DarkT As CheckBox
    Friend WithEvents LightT As CheckBox
    Friend WithEvents Label4 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Panel2 As Panel
    Friend WithEvents BackgroundWorker1 As System.ComponentModel.BackgroundWorker
    Friend WithEvents PictureBox3 As PictureBox
    Friend WithEvents Label8 As Label
    Friend WithEvents RadioButton2 As RadioButton
    Friend WithEvents RadioButton3 As RadioButton
    Friend WithEvents RadioButton1 As RadioButton

End Class
