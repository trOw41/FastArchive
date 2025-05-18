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
        FontColorButton = New Button()
        PictureBox2 = New PictureBox()
        PictureBox1 = New PictureBox()
        Button1 = New Button()
        FontBox = New ComboBox()
        FontDialog1 = New FontDialog()
        CheckBox1 = New CheckBox()
        TableLayoutPanel1.SuspendLayout()
        Panel1.SuspendLayout()
        CType(PictureBox2, ComponentModel.ISupportInitialize).BeginInit()
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
        TableLayoutPanel1.Location = New Point(138, 202)
        TableLayoutPanel1.Margin = New Padding(5, 3, 5, 3)
        TableLayoutPanel1.Name = "TableLayoutPanel1"
        TableLayoutPanel1.RowCount = 1
        TableLayoutPanel1.RowStyles.Add(New RowStyle(SizeType.Percent, 50F))
        TableLayoutPanel1.Size = New Size(213, 49)
        TableLayoutPanel1.TabIndex = 0
        ' 
        ' OK_Button
        ' 
        OK_Button.Anchor = AnchorStyles.None
        OK_Button.AutoEllipsis = True
        OK_Button.Font = New Font("Arial", 9.75F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        OK_Button.Location = New Point(116, 9)
        OK_Button.Margin = New Padding(5, 3, 5, 3)
        OK_Button.Name = "OK_Button"
        OK_Button.Size = New Size(87, 31)
        OK_Button.TabIndex = 0
        OK_Button.Text = "S&peichern"
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Font = New Font("Arial", 11.25F, FontStyle.Underline, GraphicsUnit.Point, CByte(0))
        Label1.Location = New Point(9, 19)
        Label1.Name = "Label1"
        Label1.Size = New Size(71, 17)
        Label1.TabIndex = 2
        Label1.Text = "Schriftart:"
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Font = New Font("Arial", 11.25F, FontStyle.Underline, GraphicsUnit.Point, CByte(0))
        Label2.Location = New Point(173, 19)
        Label2.Name = "Label2"
        Label2.Size = New Size(124, 17)
        Label2.TabIndex = 4
        Label2.Text = "Farbe auswählen:"
        ' 
        ' Panel1
        ' 
        Panel1.BackColor = SystemColors.ButtonFace
        Panel1.BorderStyle = BorderStyle.FixedSingle
        Panel1.Controls.Add(FontColorButton)
        Panel1.Controls.Add(PictureBox2)
        Panel1.Controls.Add(PictureBox1)
        Panel1.Controls.Add(Button1)
        Panel1.Controls.Add(FontBox)
        Panel1.Controls.Add(Label1)
        Panel1.Controls.Add(Label2)
        Panel1.Location = New Point(14, 14)
        Panel1.Name = "Panel1"
        Panel1.Size = New Size(335, 181)
        Panel1.TabIndex = 5
        ' 
        ' FontColorButton
        ' 
        FontColorButton.Font = New Font("Arial", 8.25F, FontStyle.Underline, GraphicsUnit.Point, CByte(0))
        FontColorButton.Location = New Point(9, 132)
        FontColorButton.Name = "FontColorButton"
        FontColorButton.Size = New Size(133, 26)
        FontColorButton.TabIndex = 9
        FontColorButton.Text = "&Schrifftfarbe"
        FontColorButton.UseVisualStyleBackColor = True
        ' 
        ' PictureBox2
        ' 
        PictureBox2.Image = CType(resources.GetObject("PictureBox2.Image"), Image)
        PictureBox2.Location = New Point(18, 69)
        PictureBox2.Name = "PictureBox2"
        PictureBox2.Size = New Size(114, 52)
        PictureBox2.SizeMode = PictureBoxSizeMode.Zoom
        PictureBox2.TabIndex = 8
        PictureBox2.TabStop = False
        ' 
        ' PictureBox1
        ' 
        PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), Image)
        PictureBox1.Location = New Point(182, 72)
        PictureBox1.Name = "PictureBox1"
        PictureBox1.Size = New Size(99, 42)
        PictureBox1.SizeMode = PictureBoxSizeMode.Zoom
        PictureBox1.TabIndex = 7
        PictureBox1.TabStop = False
        ' 
        ' Button1
        ' 
        Button1.Font = New Font("Arial", 9F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Button1.Location = New Point(173, 40)
        Button1.Name = "Button1"
        Button1.Size = New Size(124, 26)
        Button1.TabIndex = 6
        Button1.Text = "&Auswählen"
        Button1.UseVisualStyleBackColor = True
        ' 
        ' FontBox
        ' 
        FontBox.Font = New Font("Bahnschrift", 9F)
        FontBox.FormattingEnabled = True
        FontBox.Location = New Point(9, 41)
        FontBox.Name = "FontBox"
        FontBox.Size = New Size(133, 22)
        FontBox.TabIndex = 5
        ' 
        ' CheckBox1
        ' 
        CheckBox1.AutoSize = True
        CheckBox1.FlatStyle = FlatStyle.Flat
        CheckBox1.Font = New Font("Arial", 8.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        CheckBox1.Location = New Point(14, 202)
        CheckBox1.Name = "CheckBox1"
        CheckBox1.Size = New Size(51, 18)
        CheckBox1.TabIndex = 6
        CheckBox1.Text = "&Reset"
        CheckBox1.UseVisualStyleBackColor = True
        ' 
        ' Dialog1
        ' 
        AcceptButton = OK_Button
        AutoScaleDimensions = New SizeF(8F, 17F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(366, 253)
        ControlBox = False
        Controls.Add(CheckBox1)
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
        Panel1.PerformLayout()
        CType(PictureBox2, ComponentModel.ISupportInitialize).EndInit()
        CType(PictureBox1, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents OK_Button As System.Windows.Forms.Button
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents ColorDialog1 As ColorDialog
    Friend WithEvents Panel1 As Panel
    Friend WithEvents FontBox As ComboBox
    Friend WithEvents FontDialog1 As FontDialog
    Friend WithEvents Button1 As Button
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents PictureBox2 As PictureBox
    Friend WithEvents CheckBox1 As CheckBox
    Friend WithEvents FontColorButton As Button

End Class
