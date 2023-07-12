Imports System.IO
Imports System.Net
Imports System.Net.NetworkInformation
Imports System.Net.Http
Imports System.Threading.Tasks
Imports System.ComponentModel
Imports System.Security.Cryptography.X509Certificates
Imports System.Net.Security
Imports System.Diagnostics
Imports System.Text
Imports System.Text.RegularExpressions
Imports Microsoft.VisualBasic.Devices
Public Class Form1
    Private WithEvents bgWorker As New BackgroundWorker With {.WorkerReportsProgress = True}
    Private WithEvents bgWorkerCerts As New BackgroundWorker With {.WorkerReportsProgress = True}
    Private WithEvents bgWorkerDNS As New BackgroundWorker With {.WorkerReportsProgress = True}
    Private WithEvents bgWorkerDNS8888 As New BackgroundWorker With {.WorkerReportsProgress = True}

    Private Async Sub PopulateSystemInfo()
        Try
            Dim hostname As String = Dns.GetHostName()
            Dim ipEntry As IPHostEntry = Dns.GetHostEntry(hostname)
            Dim ips As String = String.Join(", ", ipEntry.AddressList.Where(Function(ip) ip.AddressFamily = Net.Sockets.AddressFamily.InterNetwork).Select(Function(ip) ip.ToString()))

            Dim networkInterfaces As NetworkInterface() = NetworkInterface.GetAllNetworkInterfaces()
            Dim wiredAdapters As New List(Of String)
            Dim wirelessAdapters As New List(Of String)
            For Each ni As NetworkInterface In networkInterfaces
                If ni.OperationalStatus = OperationalStatus.Up Then
                    If ni.NetworkInterfaceType = NetworkInterfaceType.Wireless80211 Then
                        wirelessAdapters.Add(ni.Name)
                    ElseIf ni.NetworkInterfaceType = NetworkInterfaceType.Ethernet Then
                        wiredAdapters.Add(ni.Name)
                    End If
                End If
            Next

            Dim drives As DriveInfo() = DriveInfo.GetDrives()
            Dim driveSpace As New List(Of String)
            For Each drive As DriveInfo In drives
                If drive.IsReady Then
                    driveSpace.Add($"{drive.Name}: {Math.Round(drive.TotalFreeSpace / 1024.0 / 1024 / 1024, 2)} GB free of {Math.Round(drive.TotalSize / 1024.0 / 1024 / 1024, 2)} GB")
                End If
            Next

            Dim dnsServers As New List(Of String)
            Try
                Using process As New Process()
                    process.StartInfo.FileName = "ipconfig"
                    process.StartInfo.Arguments = "/all"
                    process.StartInfo.RedirectStandardOutput = True
                    process.StartInfo.UseShellExecute = False
                    process.StartInfo.CreateNoWindow = True
                    process.Start()

                    Dim output As String = process.StandardOutput.ReadToEnd()
                    process.WaitForExit()

                    ' The lines containing DNS servers information start with "DNS Servers . . . . . . . . . . . :"
                    Dim lines As String() = output.Split(New String() {Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries)
                    For Each line In lines
                        If line.Trim().StartsWith("DNS Servers") Then
                            dnsServers.Add(line.Substring(line.IndexOf(":") + 1).Trim())
                        End If
                    Next
                End Using
            Catch ex As Exception
                MessageBox.Show($"Error obtaining DNS servers: {ex.Message}")
            End Try
            ' Get total and free memory
            Dim computerInfo As New ComputerInfo()
            Dim totalMemory As Double = computerInfo.TotalPhysicalMemory / (1024 * 1024 * 1024) ' Convert bytes to GB
            Dim freeMemory As Double = computerInfo.AvailablePhysicalMemory / (1024 * 1024 * 1024) ' Convert bytes to GB

            ' Get system uptime
            Dim uptime As TimeSpan = TimeSpan.FromMilliseconds(Environment.TickCount64)

            ' Get external IP addresses
            Using httpClient As New HttpClient()
                httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/114.0.0.0 Safari/537.36 Edg/114.0.1823.67")
                Dim externalIp1 As String = Await httpClient.GetStringAsync("http://icanhazip.com")
                Dim externalIp2 As String = Await httpClient.GetStringAsync("http://ifconfig.me/ip")
                Dim ipChickenHtml As String = Await httpClient.GetStringAsync("http://ipchicken.com")
                Dim ipChickenRegex As New Regex("(\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3})")
                Dim externalIp3 As String = ipChickenRegex.Match(ipChickenHtml).Groups(1).Value


                LabelSysInfo.Text = $"Hostname: {hostname}" + Environment.NewLine +
        $"IP Addresses: {ips}" + Environment.NewLine +
        $"DNS Servers: {String.Join(", ", dnsServers)}" + Environment.NewLine +
        $"External IP (ipchicken.com): {externalIp3}" + Environment.NewLine +
        $"External IP (ifconfig.me): {externalIp2}" + Environment.NewLine +
        $"External IP (icanhazip.com): {externalIp1}" + Environment.NewLine +
        $"Enabled Wired Network Adapters: {String.Join(", ", wiredAdapters)}" + Environment.NewLine +
        $"Enabled Wireless Network Adapters: {String.Join(", ", wirelessAdapters)}" + Environment.NewLine +
        $"Drive Space: {Environment.NewLine}{String.Join(Environment.NewLine, driveSpace)}" + Environment.NewLine +
        $"Total Memory: {totalMemory} GB" + Environment.NewLine +
        $"Free Memory: {freeMemory} GB" + Environment.NewLine +
        $"System Uptime: {uptime.Days} days, {uptime.Hours} hours, {uptime.Minutes} minutes"

            End Using
        Catch ex As Exception
            MessageBox.Show($"Error gathering system info: {ex.Message}")
        End Try
    End Sub



    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        PopulateSystemInfo()
        TextBoxDomains.Text = "google.com" + Environment.NewLine +
                              "www.yahoo.com" + Environment.NewLine +
                              "bing.com" + Environment.NewLine
    End Sub

    Private Sub ButtonSaveDomains_Click(sender As Object, e As EventArgs) Handles ButtonSaveDomains.Click
        Try
            ' Open the save file dialog
            Dim saveFileDialog As New SaveFileDialog()
            saveFileDialog.Filter = "CSV Files (*.csv)|*.csv|All Files (*.*)|*.*"
            saveFileDialog.DefaultExt = "csv"
            saveFileDialog.AddExtension = True

            If saveFileDialog.ShowDialog() = DialogResult.OK Then
                File.WriteAllText(saveFileDialog.FileName, TextBoxDomains.Text.Replace(",", Environment.NewLine))
                If MessageBox.Show("Domains saved successfully. Do you want to open the saved file location?", "File Saved", MessageBoxButtons.YesNo) = DialogResult.Yes Then
                    Process.Start("explorer.exe", "/select," & saveFileDialog.FileName)
                End If
            End If
        Catch ex As Exception
            MessageBox.Show($"Error saving domains: {ex.Message}")
        End Try
    End Sub




    Private Async Sub ButtonPing_Click(sender As Object, e As EventArgs) Handles ButtonPing.Click
        RichTextBoxResults.Clear()
        ButtonPing.Enabled = False
        Await PingDomainsAsync(TextBoxDomains.Text.Split(New String() {Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries))
        ButtonPing.Enabled = True
    End Sub

    Private Async Function PingDomainsAsync(domains As String()) As Task
        For Each domain In domains
            Try
                Dim ping As New Ping()
                Dim reply As PingReply = Await ping.SendPingAsync(domain)
                If reply.Status = IPStatus.Success Then
                    RichTextBoxResults.AppendText($"{domain} is reachable. Roundtrip time: {reply.RoundtripTime} ms" + Environment.NewLine)
                Else
                    RichTextBoxResults.AppendText($"{domain} is not reachable. Status: {reply.Status}" + Environment.NewLine)
                End If
            Catch ex As Exception
                RichTextBoxResults.AppendText($"Error pinging {domain}: {ex.Message}" + Environment.NewLine)
            End Try
        Next
    End Function

    Private Async Sub ButtonHTTPandHTTPS_Click(sender As Object, e As EventArgs) Handles ButtonHTTPandHTTPS.Click
        RichTextBoxResults.Clear()
        ButtonHTTPandHTTPS.Enabled = False
        Await CheckHttpAndHttpsAsync(TextBoxDomains.Text.Split(New String() {Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries))
        ButtonHTTPandHTTPS.Enabled = True
    End Sub

    Private Async Function CheckHttpAndHttpsAsync(domains As String()) As Task
        Using httpClient As New HttpClient()
            For Each domain In domains
                Try
                    Dim httpResponse As HttpResponseMessage = Await httpClient.GetAsync("http://" + domain)
                    If httpResponse.IsSuccessStatusCode AndAlso httpResponse.Content.Headers.ContentLength.GetValueOrDefault() > 0 Then
                        RichTextBoxResults.AppendText($"Connected to http://{domain} successfully, status code: {httpResponse.StatusCode}, content length: {httpResponse.Content.Headers.ContentLength}" + Environment.NewLine)
                    Else
                        RichTextBoxResults.AppendText($"Connection to http://{domain} was not fully successful, status code: {httpResponse.StatusCode}, content length: {httpResponse.Content.Headers.ContentLength}" + Environment.NewLine)
                    End If
                Catch ex As Exception
                    RichTextBoxResults.AppendText($"Error connecting to http://{domain}: {ex.Message}" + Environment.NewLine)
                End Try

                Try
                    Dim httpsResponse As HttpResponseMessage = Await httpClient.GetAsync("https://" + domain)
                    If httpsResponse.IsSuccessStatusCode AndAlso httpsResponse.Content.Headers.ContentLength.GetValueOrDefault() > 0 Then
                        RichTextBoxResults.AppendText($"Connected to https://{domain} successfully, status code: {httpsResponse.StatusCode}, content length: {httpsResponse.Content.Headers.ContentLength}" + Environment.NewLine)
                    Else
                        RichTextBoxResults.AppendText($"Connection to https://{domain} was not fully successful, status code: {httpsResponse.StatusCode}, content length: {httpsResponse.Content.Headers.ContentLength}" + Environment.NewLine)
                    End If
                Catch ex As Exception
                    RichTextBoxResults.AppendText($"Error connecting to https://{domain}: {ex.Message}" + Environment.NewLine)
                End Try
            Next
        End Using
    End Function


    Private Async Sub ButtonCheckCerts_Click(sender As Object, e As EventArgs) Handles ButtonCheckCerts.Click
        RichTextBoxResults.Clear()
        ButtonCheckCerts.Enabled = False
        Await CheckCertsAsync(TextBoxDomains.Text.Split(New String() {Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries))
        ButtonCheckCerts.Enabled = True
    End Sub


    Private Async Function CheckCertsAsync(domains As String()) As Task
        Using handler As New HttpClientHandler()
            handler.ServerCertificateCustomValidationCallback = AddressOf ValidateServerCertificate
            Using httpClient As New HttpClient(handler)
                For Each domain In domains
                    Try
                        Dim httpsResponse As HttpResponseMessage = Await httpClient.GetAsync("https://" + domain)
                        If httpsResponse.IsSuccessStatusCode Then
                            RichTextBoxResults.AppendText($"SSL certificate for https://{domain} is valid" + Environment.NewLine)
                        Else
                            RichTextBoxResults.AppendText($"Failed to validate SSL certificate for https://{domain}: HTTP status code {httpsResponse.StatusCode}" + Environment.NewLine)
                        End If
                    Catch ex As Exception
                        RichTextBoxResults.AppendText($"Error validating SSL certificate for https://{domain}: {ex.Message}" + Environment.NewLine)
                    End Try
                Next
            End Using
        End Using
    End Function


    Private Function ValidateServerCertificate(sender As Object, certificate As X509Certificate, chain As X509Chain, sslPolicyErrors As SslPolicyErrors) As Boolean
        If sslPolicyErrors = SslPolicyErrors.None Then
            Return True
        Else
            RichTextBoxResults.AppendText($"SSL certificate error: {sslPolicyErrors}")
            Return False
        End If
    End Function

    Private Async Sub ButtonDNSLocal_Click(sender As Object, e As EventArgs) Handles ButtonDNSLocal.Click
        RichTextBoxResults.Clear()
        ButtonDNSLocal.Enabled = False
        Await CheckDNSLocalAsync(TextBoxDomains.Text.Split(New String() {Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries))
        ButtonDNSLocal.Enabled = True
    End Sub

    Private Async Function CheckDNSLocalAsync(domains As String()) As Task
        For Each domain In domains
            Try
                Dim hostEntry As IPHostEntry = Await Dns.GetHostEntryAsync(domain)
                If hostEntry.AddressList.Length > 0 Then
                    RichTextBoxResults.AppendText($"Resolved domain {domain} to {hostEntry.AddressList(0)}")
                Else
                    RichTextBoxResults.AppendText($"Domain {domain} resolved, but no IP addresses were found")
                End If
            Catch ex As Exception
                RichTextBoxResults.AppendText($"Error resolving domain {domain}: {ex.Message}")
            End Try
        Next
    End Function

    Private Async Sub ButtonDNS8888_Click(sender As Object, e As EventArgs) Handles ButtonDNS8888.Click
        RichTextBoxResults.Clear()
        ButtonDNS8888.Enabled = False
        Await CheckDNS8888Async(TextBoxDomains.Text.Split(New String() {Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries))
        ButtonDNS8888.Enabled = True
    End Sub

    Private Async Function CheckDNS8888Async(domains As String()) As Task
        For Each domain In domains
            Try
                Dim startInfo As New ProcessStartInfo With {
                .FileName = "nslookup",
                .Arguments = $"-querytype=A {domain} 8.8.8.8",
                .RedirectStandardOutput = True,
                .UseShellExecute = False,
                .CreateNoWindow = True
            }
                Using process As Process = Process.Start(startInfo)
                    Dim output As String = Await process.StandardOutput.ReadToEndAsync()
                    process.WaitForExit()
                    RichTextBoxResults.AppendText(output)
                End Using
            Catch ex As Exception
                RichTextBoxResults.AppendText($"Error running nslookup for domain {domain}: {ex.Message}")
            End Try
        Next
    End Function

    Private Async Sub ButtonRunAll_Click(sender As Object, e As EventArgs) Handles ButtonRunAll.Click
        Dim domains As String() = TextBoxDomains.Text.Split(New String() {Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries)

        ' Disable the button to prevent multiple clicks
        ButtonRunAll.Enabled = False
        ButtonRunAll.Text = "Running, wait."

        ' Clear the list box
        RichTextBoxResults.Clear()

        ' Run the ping tests
        Await PingDomainsAsync(domains)

        ' Run the HTTP/HTTPS tests
        Await CheckHttpAndHttpsAsync(domains)

        ' Check the certificates
        Await CheckCertsAsync(domains)

        ' Check the DNS locally
        Await CheckDNSLocalAsync(domains)

        ' Check the DNS using 8.8.8.8
        Await CheckDNS8888Async(domains)

        ' Enable the button after all tasks are complete
        ButtonRunAll.Enabled = True
        ButtonExportHTML.Enabled = True
        Form1.ActiveForm.AcceptButton = ButtonExportHTML
        ButtonRunAll.Text = "Run All"
    End Sub

    Private Sub ButtonExportHTML_Click(sender As Object, e As EventArgs) Handles ButtonExportHTML.Click
        Try
            ' Save the results to an HTML file
            Dim saveFileDialog As New SaveFileDialog()
            saveFileDialog.Filter = "HTML Files (*.html)|*.html|All Files (*.*)|*.*"
            saveFileDialog.DefaultExt = "html"
            saveFileDialog.AddExtension = True

            If saveFileDialog.ShowDialog() = DialogResult.OK Then
                File.WriteAllText(saveFileDialog.FileName, ExportToHtml())
                Dim dialogResult As DialogResult = MessageBox.Show("Results exported successfully. Do you want to open the exported file location?", "Export Successful", MessageBoxButtons.YesNo)

                If dialogResult = DialogResult.Yes Then
                    Process.Start("explorer.exe", "/select, """ & saveFileDialog.FileName & """")
                End If
            End If
        Catch ex As Exception
            MessageBox.Show($"Error exporting results: {ex.Message}")
        End Try
        Form1.ActiveForm.AcceptButton = ButtonRunAll
    End Sub


    Private Function ExportToHtml() As String
        Dim html As New StringBuilder()

        ' Define the HTML document structure
        html.Append("<!DOCTYPE html><html><head>")
        html.Append("<style>")
        html.Append("body {font-family: Arial, sans-serif; background-color: #333; color: #f0f0f0; padding: 20px;}")
        html.Append(".container {display: flex; flex-direction: column; gap: 20px;}")
        html.Append(".box {border: 1px solid #999; padding: 10px; background-color: #444;}")
        html.Append(".header {color: #ffa500; font-size: 1.2em; border-bottom: 1px solid #999; margin-bottom: 10px;}")
        html.Append(".system-info {font-size: 0.9em; line-height: 1.5em;}")
        html.Append(".test {font-size: 1em; line-height: 1.4em;}")
        html.Append(".success {color: #00cc00;}")
        html.Append(".failure {color: #cc0000;}")
        html.Append("</style>")
        html.Append("</head><body>")
        html.Append("<div class='container'>")

        ' Add system info
        html.Append("<div class='box'><div class='header'>System Info</div>")
        html.Append("<div class='system-info'>").Append(LabelSysInfo.Text.Replace(Environment.NewLine, "<br/>")).Append("</div></div>")

        ' Add test results
        For Each item As String In RichTextBoxResults.Lines
            Dim cssClass As String = If(item.ToLower().Contains("error"), "failure", "success")
            html.Append("<div class='box'><div class='test ").Append(cssClass).Append("'>").Append(item.Replace(Environment.NewLine, "<br/>")).Append("</div></div>")
        Next

        ' Add list of domains tested
        html.Append("<div class='box'><div class='header'>Domains Tested</div>")
        html.Append("<div class='system-info'>").Append(TextBoxDomains.Text.Replace(Environment.NewLine, "<br/>")).Append("</div></div>")

        html.Append("</div></body></html>")

        Return html.ToString()
    End Function


    Private Sub ButtonImportCSV_Click(sender As Object, e As EventArgs) Handles ButtonImportCSV.Click
        Dim openFileDialog As New OpenFileDialog()
        openFileDialog.Filter = "CSV Files (*.csv)|*.csv|All Files (*.*)|*.*"
        openFileDialog.Multiselect = False

        If openFileDialog.ShowDialog() = DialogResult.OK Then
            Try
                Using reader As New StreamReader(openFileDialog.FileName)
                    While Not reader.EndOfStream
                        TextBoxDomains.AppendText(reader.ReadLine() + Environment.NewLine)
                    End While
                End Using
            Catch ex As Exception
                MessageBox.Show($"Error reading file: {ex.Message}")
            End Try
        End If
    End Sub

    Private Async Function CheckPortConnection(hostname As String, port As Integer) As Task(Of Boolean)
        Using client As New Sockets.TcpClient()
            Try
                Await client.ConnectAsync(hostname, port)
                Return True
            Catch
                Return False
            End Try
        End Using
    End Function


    Private Async Sub ButtonCheckPort_Click(sender As Object, e As EventArgs) Handles ButtonCheckPort.Click
        Dim port As Integer = Integer.Parse(TextBoxPort.Text)
        Dim domains As String() = TextBoxDomains.Text.Split(New String() {Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries)

        For Each domain In domains
            Dim canConnect As Boolean = Await CheckPortConnection(domain, port)

            If canConnect Then
                RichTextBoxResults.AppendText($"Successfully connected to {domain} on port {port}." + Environment.NewLine)
            Else
                RichTextBoxResults.AppendText($"Failed to connect to {domain} on port {port}." + Environment.NewLine)
            End If
        Next
    End Sub

    Private Sub ButtonOpenAll_Click(sender As Object, e As EventArgs) Handles ButtonOpenAll.Click
        ' Get the list of domains from TextBoxDomains
        Dim domains As String() = TextBoxDomains.Text.Split(New String() {Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries)

        ' Iterate over the domains
        For Each domain In domains
            Try
                ' Use the Process.Start method with ProcessStartInfo to open the domain in the default browser
                Dim psi As New ProcessStartInfo()
                psi.UseShellExecute = True
                psi.FileName = "http://" + domain
                Process.Start(psi)
            Catch ex As Exception
                MessageBox.Show($"Error opening domain '{domain}': {ex.Message}")
            End Try
        Next
    End Sub

    Private Async Sub ButtonTraceroute_Click(sender As Object, e As EventArgs) Handles ButtonTraceroute.Click
        Try

            RichTextBoxResults.Clear()

            ' Run traceroute to google.com
            Using process As New Process()
                process.StartInfo.FileName = "tracert"
                process.StartInfo.Arguments = "google.com"
                process.StartInfo.RedirectStandardOutput = True
                process.StartInfo.UseShellExecute = False
                process.StartInfo.CreateNoWindow = True
                process.Start()

                While Not process.StandardOutput.EndOfStream
                    Dim line = Await process.StandardOutput.ReadLineAsync()
                    RichTextBoxResults.AppendText(line)
                End While

                process.WaitForExit()
            End Using
        Catch ex As Exception
            MessageBox.Show($"Error running traceroute: {ex.Message}")
        End Try
    End Sub

    Private Sub ButtonNetEventLog_Click(sender As Object, e As EventArgs) Handles ButtonNetEventLog.Click
        Try
            ' Clear the results list box
            RichTextBoxResults.Clear()

            Dim logName As String = "System"
            Dim logMachineName As String = "." ' Local machine

            ' List of common network-related event sources (adjust this to your needs)
            Dim networkEventSources As New List(Of String) From {"e1rexpress", "b57nd60a", "Netwtw04", "Netwtw06"}

            ' Create an EventLog instance and assign its log name and machine name
            Dim myEventLog As New EventLog(logName, logMachineName)

            ' Iterate through the entries in the log
            For Each entry As EventLogEntry In myEventLog.Entries
                ' Check if the entry is a warning, error, or failure audit and that it's from one of the specified sources
                If ((entry.EntryType = EventLogEntryType.Warning) OrElse (entry.EntryType = EventLogEntryType.Error) OrElse (entry.EntryType = EventLogEntryType.FailureAudit)) AndAlso (networkEventSources.Contains(entry.Source)) Then
                    RichTextBoxResults.AppendText($"Time: {entry.TimeGenerated}, Source: {entry.Source}, Message: {entry.Message}" + Environment.NewLine)
                End If
            Next
        Catch ex As Exception
            MessageBox.Show($"Error accessing event log: {ex.Message}")
        End Try
    End Sub

End Class
