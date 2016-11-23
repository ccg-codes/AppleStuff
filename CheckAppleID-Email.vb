 Imports System.Net
 
 Function CheckID(ByVal email As String) As Boolean

        Dim req As HttpWebRequest
        Dim res As HttpWebResponse

        req = CType(HttpWebRequest.Create("https://iforgot.apple.com/password/verify/appleid"), HttpWebRequest)

        req.KeepAlive = True
        req.Method = "POST"
        req.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:50.0) Gecko/20100101 Firefox/50.0"
        req.ContentType = "application/json"
        req.Headers.Add("X-Requested-With: XMLHttpRequest")
        req.Accept = "application/json, text/javascript, */*; q=0.01"
        req.Referer = "https://iforgot.apple.com/password/verify/appleid"
        req.CookieContainer = New CookieContainer


        Dim postdata As String = "{""id"":""" & email & """}"
        Dim barray() As Byte = System.Text.Encoding.Default.GetBytes(postdata)
        req.ContentLength = barray.Length
        req.GetRequestStream.Write(barray, 0, barray.Length)

        res = CType(req.GetResponse, HttpWebResponse)
        Dim reader As New IO.StreamReader(res.GetResponseStream)
        Dim out As String = reader.ReadToEnd

        If out.Contains("""supportsUnlock"" : true,") Then
            Return True
        ElseIf out.Contains("""supportsUnlock"" : false,") Then
            Return False
        End If

    End Function