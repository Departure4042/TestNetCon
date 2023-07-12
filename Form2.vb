Public Class TracerouteForm
    Inherits Form

    Private textBox As TextBox

    Public Sub New(domain As String)
        Me.Text = $"Traceroute to {domain}"

        textBox = New TextBox()
        textBox.Multiline = True
        textBox.ScrollBars = ScrollBars.Vertical
        textBox.Dock = DockStyle.Fill
        Me.Controls.Add(textBox)
    End Sub

    Public Sub AppendText(text As String)
        If textBox.InvokeRequired Then
            textBox.Invoke(New Action(Of String)(AddressOf AppendText), text)
        Else
            textBox.AppendText(text)
        End If
    End Sub
End Class
