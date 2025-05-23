Imports System.ComponentModel

Partial Class FragenKatalog
    Inherits System.Windows.Forms.Form

    Private faqContent As Dictionary(Of String, String)
    Private _FaqText As Object

    Public Sub New(menuStrip1 As MenuStrip, dateiToolStripMenuItem As ToolStripMenuItem, schließenToolStripMenuItem As ToolStripMenuItem, toolStripComboBox1 As ToolStripComboBox, faqContent As Dictionary(Of String, String), faqText As Object)
        Me.MenuStrip1 = menuStrip1
        Me.DateiToolStripMenuItem = dateiToolStripMenuItem
        Me.SchließenToolStripMenuItem = schließenToolStripMenuItem
        Me.ToolStripComboBox1 = toolStripComboBox1
        Me.faqContent = faqContent
        Me.FaqText = faqText
    End Sub

    <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
    Public Property FaqText As Object
        Get
            Return _FaqText
        End Get
        Set
            _FaqText = Value
        End Set
    End Property

    Private Sub FaqForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        InitializeFaqContent() ' FAQ-Inhalte initialisieren
        PopulateIndexBox()     ' IndexBox füllen

        ' Optional: Ersten FAQ-Eintrag beim Laden anzeigen
        If IndexView.Nodes.Count > 1 Then
            IndexView.SelectedNode = IndexView.Nodes(1) ' Wählt "1. Was ist FastArchiver?"
        End If
    End Sub

    Private Sub InitializeFaqContent()

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