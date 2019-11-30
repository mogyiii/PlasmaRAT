Imports System.Management

Module ComputerComponent
    Public Function GetCPU() As String
        Try
            Dim Proc As New Management.ManagementObject("Win32_Processor.deviceid=""CPU0""")
            Proc.Get()
            Return (Proc("Name").ToString)

        Catch ex As Exception
            Return "N/A"
        End Try

    End Function
    Public Function GetVideoCard() As String
        Try
            Dim VideoCard As String = String.Empty
            Dim objquery As New ObjectQuery("SELECT * FROM Win32_VideoController")
            Dim objSearcher As New ManagementObjectSearcher(objquery)

            For Each MemObj As ManagementObject In objSearcher.Get
                VideoCard = VideoCard & (MemObj("Name")) & ". "

            Next
            Return (VideoCard)
        Catch
            Return "N/A"
        End Try
    End Function
    Public Function GetAntiVirus() As String
        Try
            Dim str As String = Nothing
            Dim searcher As New ManagementObjectSearcher("\\" & Environment.MachineName & "\root\SecurityCenter2", "SELECT * FROM AntivirusProduct")
            Dim instances As ManagementObjectCollection = searcher.[Get]()
            For Each queryObj As ManagementObject In instances
                str = queryObj("displayName").ToString()
            Next
            If str = String.Empty Then str = "N/A"
            str = "AntiVirus: " & str.ToString
            Return str
            searcher.Dispose()
        Catch
            Return "AntiVirus: N/A"
        End Try
    End Function
    Public Function GetFirewall() As String
        Try
            Dim str As String = Nothing
            Dim searcher As New ManagementObjectSearcher("\\" & Environment.MachineName & "\root\SecurityCenter2", "SELECT * FROM FirewallProduct")
            Dim instances As ManagementObjectCollection = searcher.[Get]()
            For Each queryObj As ManagementObject In instances
                str = queryObj("displayName").ToString()
            Next
            If str = String.Empty Then str = "N/A"
            str = "Firewall: " & str.ToString
            Return str
            searcher.Dispose()
        Catch
            Return "Firewall: N/A"
        End Try

    End Function
End Module
