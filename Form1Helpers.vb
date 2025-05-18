Class Form1Helpers
    Public Shared Sub Form1Font(sender As Object, font As Font)
        Dim form As Form1 = DirectCast(sender, Form1)
        form.Font = font
    End Sub
End Class
