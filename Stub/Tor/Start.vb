Imports System.Threading
Module Start
    Private exePath As String = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase)
    Private Sub proccessstart()
        Console.WriteLine("Starting tor...")
        Dim Process As New Process
        Process.StartInfo.FileName = get_LocalPath() & "\Tor\tor.exe"
        Process.StartInfo.Arguments = "-f " & get_LocalPath() & "\Tor\Data\Tor\torrc"
        Process.StartInfo.UseShellExecute = False
        Process.StartInfo.RedirectStandardOutput = True
        Process.StartInfo.CreateNoWindow = True
        Process.StartInfo.WorkingDirectory = get_LocalPath() & "\Tor"
        Process.Start()
        Process.PriorityClass = ProcessPriorityClass.BelowNormal
    End Sub
    Public Sub Main_start()
        Dim p() As Process
        p = Process.GetProcessesByName("tor")
        If Not p.Length > 0 Then
            proccessstart()
        End If
        Thread.Sleep(milltosec(90))
    End Sub
    Public Function milltosec(ByVal sec As Integer)
        Return sec * 1000
    End Function
    Public Function get_LocalPath()
        Dim local_path As String = exePath.Replace("file:\", "")
        Return local_path
    End Function
    Private Function filereader(ByVal file As String)
        Return My.Computer.FileSystem.ReadAllText(file, System.Text.Encoding.UTF8)
    End Function
End Module
