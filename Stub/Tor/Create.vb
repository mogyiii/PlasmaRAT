Imports System.IO
Imports System.Text

Module Create
    Public Sub torchfileCreate(ByVal path As String)
        If Not File.Exists(path & "\Tor\Data\Tor\torrc") Then
            Console.WriteLine("create torch...")
            Dim filestring As String = "
ControlPort 9151
DataDirectory " & path & "\Tor
DirPort 9030
ExitPolicy reject *:*
HashedControlPassword 16:4E1F1599005EB8F3603C046EF402B00B6F74C008765172A774D2853FD4
HiddenServiceDir " & path & "
HiddenServicePort 80 127.0.0.1:5557
Log notice stdout
Nickname VBMogyiii
SocksPort 9150"
            Dim fs As FileStream = File.Create(path & "\Tor\Data\Tor\torrc")
            Dim info As Byte() = New UTF8Encoding(True).GetBytes(filestring)
            fs.Write(info, 0, info.Length)
            fs.Close()
        Else
            Console.WriteLine("torch a live")
        End If
    End Sub
End Module
