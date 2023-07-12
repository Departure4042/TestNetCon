<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        ButtonPing = New Button()
        Label1 = New Label()
        ButtonImportCSV = New Button()
        TextBoxDomains = New TextBox()
        ButtonHTTPandHTTPS = New Button()
        ButtonCheckCerts = New Button()
        Label2 = New Label()
        ButtonDNSLocal = New Button()
        ButtonDNS8888 = New Button()
        GroupBoxSysInfo = New GroupBox()
        LabelSysInfo = New Label()
        ButtonRunAll = New Button()
        ButtonExportHTML = New Button()
        ButtonSaveDomains = New Button()
        ButtonCheckPort = New Button()
        TextBoxPort = New TextBox()
        Label3 = New Label()
        ButtonOpenAll = New Button()
        ButtonTraceroute = New Button()
        ButtonNetEventLog = New Button()
        RichTextBoxResults = New RichTextBox()
        GroupBoxSysInfo.SuspendLayout()
        SuspendLayout()
        ' 
        ' ButtonPing
        ' 
        ButtonPing.Location = New Point(12, 257)
        ButtonPing.Name = "ButtonPing"
        ButtonPing.Size = New Size(75, 23)
        ButtonPing.TabIndex = 2
        ButtonPing.Text = "Ping"
        ButtonPing.UseVisualStyleBackColor = True
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Location = New Point(126, 12)
        Label1.Name = "Label1"
        Label1.Size = New Size(160, 15)
        Label1.TabIndex = 4
        Label1.Text = "or Enter domains line by line:"
        ' 
        ' ButtonImportCSV
        ' 
        ButtonImportCSV.Location = New Point(12, 8)
        ButtonImportCSV.Name = "ButtonImportCSV"
        ButtonImportCSV.Size = New Size(108, 23)
        ButtonImportCSV.TabIndex = 5
        ButtonImportCSV.Text = "Import from CSV"
        ButtonImportCSV.UseVisualStyleBackColor = True
        ' 
        ' TextBoxDomains
        ' 
        TextBoxDomains.Location = New Point(12, 37)
        TextBoxDomains.Multiline = True
        TextBoxDomains.Name = "TextBoxDomains"
        TextBoxDomains.ScrollBars = ScrollBars.Both
        TextBoxDomains.Size = New Size(399, 199)
        TextBoxDomains.TabIndex = 6
        ' 
        ' ButtonHTTPandHTTPS
        ' 
        ButtonHTTPandHTTPS.Location = New Point(93, 257)
        ButtonHTTPandHTTPS.Name = "ButtonHTTPandHTTPS"
        ButtonHTTPandHTTPS.Size = New Size(75, 23)
        ButtonHTTPandHTTPS.TabIndex = 7
        ButtonHTTPandHTTPS.Text = "HTTP/S"
        ButtonHTTPandHTTPS.UseVisualStyleBackColor = True
        ' 
        ' ButtonCheckCerts
        ' 
        ButtonCheckCerts.Location = New Point(174, 257)
        ButtonCheckCerts.Name = "ButtonCheckCerts"
        ButtonCheckCerts.Size = New Size(75, 23)
        ButtonCheckCerts.TabIndex = 8
        ButtonCheckCerts.Text = "Certificates"
        ButtonCheckCerts.UseVisualStyleBackColor = True
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Location = New Point(12, 239)
        Label2.Name = "Label2"
        Label2.Size = New Size(142, 15)
        Label2.TabIndex = 9
        Label2.Text = "Choose which test to run:"
        ' 
        ' ButtonDNSLocal
        ' 
        ButtonDNSLocal.Location = New Point(255, 257)
        ButtonDNSLocal.Name = "ButtonDNSLocal"
        ButtonDNSLocal.Size = New Size(75, 23)
        ButtonDNSLocal.TabIndex = 10
        ButtonDNSLocal.Text = "DNS Local"
        ButtonDNSLocal.UseVisualStyleBackColor = True
        ' 
        ' ButtonDNS8888
        ' 
        ButtonDNS8888.Location = New Point(336, 257)
        ButtonDNS8888.Name = "ButtonDNS8888"
        ButtonDNS8888.Size = New Size(75, 23)
        ButtonDNS8888.TabIndex = 11
        ButtonDNS8888.Text = "DNS 8.8.8.8"
        ButtonDNS8888.UseVisualStyleBackColor = True
        ' 
        ' GroupBoxSysInfo
        ' 
        GroupBoxSysInfo.Controls.Add(LabelSysInfo)
        GroupBoxSysInfo.Location = New Point(417, 12)
        GroupBoxSysInfo.Name = "GroupBoxSysInfo"
        GroupBoxSysInfo.Size = New Size(371, 314)
        GroupBoxSysInfo.TabIndex = 12
        GroupBoxSysInfo.TabStop = False
        GroupBoxSysInfo.Text = "System Info"
        ' 
        ' LabelSysInfo
        ' 
        LabelSysInfo.Location = New Point(3, 19)
        LabelSysInfo.Name = "LabelSysInfo"
        LabelSysInfo.Size = New Size(362, 292)
        LabelSysInfo.TabIndex = 0
        LabelSysInfo.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' ButtonRunAll
        ' 
        ButtonRunAll.Location = New Point(12, 286)
        ButtonRunAll.Name = "ButtonRunAll"
        ButtonRunAll.Size = New Size(156, 37)
        ButtonRunAll.TabIndex = 1
        ButtonRunAll.Text = "Run All"
        ButtonRunAll.UseVisualStyleBackColor = True
        ' 
        ' ButtonExportHTML
        ' 
        ButtonExportHTML.Enabled = False
        ButtonExportHTML.Location = New Point(255, 284)
        ButtonExportHTML.Name = "ButtonExportHTML"
        ButtonExportHTML.Size = New Size(156, 39)
        ButtonExportHTML.TabIndex = 13
        ButtonExportHTML.Text = "Export HTML Report"
        ButtonExportHTML.UseVisualStyleBackColor = True
        ' 
        ' ButtonSaveDomains
        ' 
        ButtonSaveDomains.Location = New Point(320, 8)
        ButtonSaveDomains.Name = "ButtonSaveDomains"
        ButtonSaveDomains.Size = New Size(91, 23)
        ButtonSaveDomains.TabIndex = 14
        ButtonSaveDomains.Text = "Save Domains"
        ButtonSaveDomains.UseVisualStyleBackColor = True
        ' 
        ' ButtonCheckPort
        ' 
        ButtonCheckPort.Location = New Point(63, 538)
        ButtonCheckPort.Name = "ButtonCheckPort"
        ButtonCheckPort.Size = New Size(75, 23)
        ButtonCheckPort.TabIndex = 15
        ButtonCheckPort.Text = "Check Port"
        ButtonCheckPort.UseVisualStyleBackColor = True
        ' 
        ' TextBoxPort
        ' 
        TextBoxPort.Location = New Point(12, 538)
        TextBoxPort.Name = "TextBoxPort"
        TextBoxPort.PlaceholderText = "22"
        TextBoxPort.Size = New Size(49, 23)
        TextBoxPort.TabIndex = 16
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Location = New Point(8, 520)
        Label3.Name = "Label3"
        Label3.Size = New Size(241, 15)
        Label3.TabIndex = 17
        Label3.Text = "Other Tests (not included in exported report)"
        ' 
        ' ButtonOpenAll
        ' 
        ButtonOpenAll.Location = New Point(144, 538)
        ButtonOpenAll.Name = "ButtonOpenAll"
        ButtonOpenAll.Size = New Size(125, 23)
        ButtonOpenAll.TabIndex = 18
        ButtonOpenAll.Text = "Open All in Browser"
        ButtonOpenAll.UseVisualStyleBackColor = True
        ' 
        ' ButtonTraceroute
        ' 
        ButtonTraceroute.Location = New Point(644, 538)
        ButtonTraceroute.Name = "ButtonTraceroute"
        ButtonTraceroute.Size = New Size(144, 23)
        ButtonTraceroute.TabIndex = 19
        ButtonTraceroute.Text = "Traceroute google.com"
        ButtonTraceroute.UseVisualStyleBackColor = True
        ' 
        ' ButtonNetEventLog
        ' 
        ButtonNetEventLog.Location = New Point(500, 538)
        ButtonNetEventLog.Name = "ButtonNetEventLog"
        ButtonNetEventLog.Size = New Size(138, 23)
        ButtonNetEventLog.TabIndex = 20
        ButtonNetEventLog.Text = "Network Error Events"
        ButtonNetEventLog.UseVisualStyleBackColor = True
        ' 
        ' RichTextBoxResults
        ' 
        RichTextBoxResults.Location = New Point(12, 329)
        RichTextBoxResults.Name = "RichTextBoxResults"
        RichTextBoxResults.Size = New Size(776, 184)
        RichTextBoxResults.TabIndex = 21
        RichTextBoxResults.Text = ""
        ' 
        ' Form1
        ' 
        AcceptButton = ButtonRunAll
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(794, 570)
        Controls.Add(RichTextBoxResults)
        Controls.Add(ButtonNetEventLog)
        Controls.Add(ButtonTraceroute)
        Controls.Add(ButtonOpenAll)
        Controls.Add(Label3)
        Controls.Add(TextBoxPort)
        Controls.Add(ButtonCheckPort)
        Controls.Add(ButtonSaveDomains)
        Controls.Add(ButtonExportHTML)
        Controls.Add(ButtonRunAll)
        Controls.Add(GroupBoxSysInfo)
        Controls.Add(ButtonDNS8888)
        Controls.Add(ButtonDNSLocal)
        Controls.Add(Label2)
        Controls.Add(ButtonCheckCerts)
        Controls.Add(ButtonHTTPandHTTPS)
        Controls.Add(TextBoxDomains)
        Controls.Add(ButtonImportCSV)
        Controls.Add(Label1)
        Controls.Add(ButtonPing)
        Name = "Form1"
        Text = "TestNetCon"
        GroupBoxSysInfo.ResumeLayout(False)
        ResumeLayout(False)
        PerformLayout()
    End Sub
    Friend WithEvents ButtonPing As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents ButtonImportCSV As Button
    Friend WithEvents TextBoxDomains As TextBox
    Friend WithEvents ButtonHTTPandHTTPS As Button
    Friend WithEvents ButtonCheckCerts As Button
    Friend WithEvents Label2 As Label
    Friend WithEvents ButtonDNSLocal As Button
    Friend WithEvents ButtonDNS8888 As Button
    Friend WithEvents GroupBoxSysInfo As GroupBox
    Friend WithEvents LabelSysInfo As Label
    Friend WithEvents ButtonRunAll As Button
    Friend WithEvents ButtonExportHTML As Button
    Friend WithEvents ButtonSaveDomains As Button
    Friend WithEvents ButtonCheckPort As Button
    Friend WithEvents TextBoxPort As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents ButtonOpenAll As Button
    Friend WithEvents ButtonTraceroute As Button
    Friend WithEvents ButtonNetEventLog As Button
    Friend WithEvents RichTextBoxResults As RichTextBox
End Class
