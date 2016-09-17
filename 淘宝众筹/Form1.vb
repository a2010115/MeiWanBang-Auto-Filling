
Imports System.Net
Imports HtmlAgilityPack
Imports System.Text
Public Class Form1

    Dim requireURL As String = "https://izhongchou.taobao.com/dreamdetail.htm?spm=a215p.1472805.0.0.o2RZBb&id=20046906"

    Structure TaobaoZC
        Dim allHTML
        Dim Name
        Dim Slogan
        Dim Company
        Dim URL
        Dim CoverURL
        Dim Price
    End Structure


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        WebBrowser1.Navigate(TextBox1.Text)
    End Sub

    Private Sub WebBrowser1_DocumentCompleted(sender As Object, e As WebBrowserDocumentCompletedEventArgs) Handles WebBrowser1.DocumentCompleted
        If Not e.Url.ToString = WebBrowser1.Url.ToString Then
            Exit Sub
        ElseIf WebBrowser1.ReadyState = WebBrowserReadyState.Complete Then
            Timer1.Enabled = True
        End If
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Timer1.Enabled = False
        Dim hiTaobao As New TaobaoZC

        Dim EncodingOfEXP As Encoding = Encoding.GetEncoding(WebBrowser1.Document.Encoding)
        Dim htmlMessage As String = WebBrowser1.Document.Body.InnerHtml
        hiTaobao.allHTML = Replace(htmlMessage, "¥", "ALITAOBAOZHONGCHOU")
        hiTaobao.URL = WebBrowser1.Url.ToString
        hiTaobao.Name = TextProcessing.CutString(hiTaobao.allHTML, "<DIV class=project-title>
<H1>", "</H1></DIV>")
        hiTaobao.Slogan = TextProcessing.CutString(hiTaobao.allHTML, "项目介绍</H2>
<DIV class=box>
<DIV>", "</DIV>")
        hiTaobao.Company = TextProcessing.CutString(hiTaobao.allHTML, "class=sponsor-info><SPAN class=""ww-light ww-large"" data-nick=""", """ data-display")
        hiTaobao.CoverURL = TextProcessing.CutString(hiTaobao.allHTML, "class=cover><IMG class=cover-img src=""", """>")
        hiTaobao.Price = TextProcessing.CutString(hiTaobao.allHTML, "repay-money><SPAN class=yen>ALITAOBAOZHONGCHOU</SPAN>", ".")
        Try
            WebBrowser2.Document.GetElementById("product-share-url").Focus()
            WebBrowser2.Document.GetElementById("product-share-url").InnerText = hiTaobao.URL
            WebBrowser2.Document.GetElementById("product-share-title").Focus()
            WebBrowser2.Document.GetElementById("product-share-title").InnerText = hiTaobao.Name
            For Each it As HtmlElement In WebBrowser2.Document.All.GetElementsByName("description")
                it.Focus()
                it.InnerText = hiTaobao.Slogan
            Next
            WebBrowser2.Document.GetElementById("product-share-save").Focus()
            WebBrowser2.Document.GetElementById("product-share-save").InvokeMember("click")



        Catch ex As Exception
            Debug.WriteLine(ex.Message)
        End Try

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If MessageBox.Show("确认已经登录？", "确认已经登录？", MessageBoxButtons.YesNo) = DialogResult.Yes Then
            Button1.Enabled = True
            WebBrowser2.Navigate("http://wan.meizu.com/product/share")
        End If

    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        WebBrowser2.Navigate("http://wan.meizu.com/product/share")
        MessageBox.Show(My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Internet Explorer\MAIN\FeatureControl\FEATURE_BROWSER_EMULATION", IO.Path.GetFileName(Application.ExecutablePath), 120))
        If Not My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Internet Explorer\MAIN\FeatureControl\FEATURE_BROWSER_EMULATION", IO.Path.GetFileName(Application.ExecutablePath), 120) = 9999 Then
            My.Computer.Registry.SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Internet Explorer\MAIN\FeatureControl\FEATURE_BROWSER_EMULATION", IO.Path.GetFileName(Application.ExecutablePath), 9999)
            MessageBox.Show("位置信息已更新，请重新打开应用程序！")
            Me.Close()
            Me.Dispose()

            End
        End If
        Debug.WriteLine(IO.Path.GetFileName(Application.ExecutablePath))
    End Sub
End Class

Class TextProcessing
    Shared Function CutString(ResourceString As String, Head As String, Tail As String)
        Try
            Dim cutStart = InStr(ResourceString, Head) + Head.Length
            Dim cutLength = InStr(Start:=cutStart, String1:=ResourceString, String2:=Tail) - InStr(ResourceString, Head) - Head.Length
            Return (Mid(ResourceString, cutStart, cutLength))
        Catch ex As Exception

        End Try

    End Function
End Class

Class Net
    Shared Function GetHTMLDoc(URL As String)
        Dim rq As HttpWebRequest
        Try
            If InStr(URL, "http://") Or InStr(URL, "https://") Then
                rq = WebRequest.Create(URL)
            Else
                rq = WebRequest.Create("http://" & URL)
            End If
        Catch ex As Exception
            Return False
            Exit Function
        End Try
        Dim rp As HttpWebResponse = rq.GetResponse()
        Dim reader As IO.StreamReader = New IO.StreamReader(rp.GetResponseStream(), System.Text.Encoding.GetEncoding("gb2312"))
        Dim resourceString = reader.ReadToEnd
        Return resourceString
    End Function


    Shared Function DownloadWebImage(ByVal ImageURL As String)
        Dim objImage As IO.MemoryStream
        Dim objwebClient As WebClient
        Dim sURL As String = Trim(ImageURL)
        Dim bAns As Boolean
        Try
            If Not sURL.ToLower().StartsWith("http://") Or Not sURL.ToLower().StartsWith("https://") Then sURL = "http://" & sURL
            objwebClient = New WebClient()
            objImage = New IO.MemoryStream(objwebClient.DownloadData(sURL))
            Return Image.FromStream(objImage)
        Catch ex As Exception
            Return False
        End Try
        Return bAns
    End Function
End Class
