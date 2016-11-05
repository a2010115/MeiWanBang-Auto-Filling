Imports System.Drawing.Imaging
Imports System.Runtime.InteropServices
Imports System.Net
Imports HtmlAgilityPack
Imports System.Text
Imports System.ComponentModel

Public Class Form1




#Region "绘制窗体阴影“
    Private dwmMargins As MetroUI_Form.Dwm.MARGINS
    Private _marginOk As Boolean
    Private _aeroEnabled As Boolean = False

#Region "Props"
    Public ReadOnly Property AeroEnabled() As Boolean
        Get
            Return _aeroEnabled
        End Get
    End Property
#End Region

#Region "Methods"

    Public Shared Function LoWord(ByVal dwValue As Integer) As Integer
        Return dwValue And &HFFFF
    End Function

    Public Shared Function HiWord(ByVal dwValue As Integer) As Integer
        Return (dwValue >> 16) And &HFFFF
    End Function
#End Region

    Public Sub Form1_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Activated
        MetroUI_Form.Dwm.DwmExtendFrameIntoClientArea(Me.Handle, dwmMargins) '开启窗体阴影
        Me.Size = New Point(1614, 759)



    End Sub

    Protected Overrides Sub WndProc(ByRef m As Message)
        Dim WM_NCCALCSIZE As Integer = &H83
        Dim WM_NCHITTEST As Integer = &H84
        Dim result As IntPtr = IntPtr.Zero

        Dim dwmHandled As Integer = MetroUI_Form.Dwm.DwmDefWindowProc(m.HWnd, m.Msg, m.WParam, m.LParam, result)

        If dwmHandled = 1 Then
            m.Result = result
            Return
        End If

        If m.Msg = WM_NCCALCSIZE AndAlso CType(m.WParam, Int32) = 1 Then
            Dim nccsp As MetroUI_Form.WinApi.NCCALCSIZE_PARAMS = DirectCast(Marshal.PtrToStructure(m.LParam, GetType(MetroUI_Form.WinApi.NCCALCSIZE_PARAMS)), MetroUI_Form.WinApi.NCCALCSIZE_PARAMS)
            nccsp.rect0.Top += 0
            nccsp.rect0.Bottom += 0
            nccsp.rect0.Left += 0
            nccsp.rect0.Right += 0

            If Not _marginOk Then
                dwmMargins.cyTopHeight = 0
                dwmMargins.cxLeftWidth = 0
                dwmMargins.cyBottomHeight = 3
                dwmMargins.cxRightWidth = 0
                _marginOk = True
            End If

            Marshal.StructureToPtr(nccsp, m.LParam, False)

            m.Result = IntPtr.Zero
        ElseIf m.Msg = WM_NCHITTEST AndAlso CType(m.Result, Int32) = 0 Then
            m.Result = HitTestNCA(m.HWnd, m.WParam, m.LParam)
        Else
            MyBase.WndProc(m)
        End If
    End Sub
    Private Function HitTestNCA(ByVal hwnd As IntPtr, ByVal wparam As IntPtr, ByVal lparam As IntPtr) As IntPtr
        Dim HTNOWHERE As Integer = 0
        Dim HTCLIENT As Integer = 1
        Dim HTCAPTION As Integer = 2
        Dim HTGROWBOX As Integer = 4
        Dim HTSIZE As Integer = HTGROWBOX
        Dim HTMINBUTTON As Integer = 8
        Dim HTMAXBUTTON As Integer = 9
        Dim HTLEFT As Integer = 10
        Dim HTRIGHT As Integer = 11
        Dim HTTOP As Integer = 12
        Dim HTTOPLEFT As Integer = 13
        Dim HTTOPRIGHT As Integer = 14
        Dim HTBOTTOM As Integer = 15
        Dim HTBOTTOMLEFT As Integer = 16
        Dim HTBOTTOMRIGHT As Integer = 17
        Dim HTREDUCE As Integer = HTMINBUTTON
        Dim HTZOOM As Integer = HTMAXBUTTON
        Dim HTSIZEFIRST As Integer = HTLEFT
        Dim HTSIZELAST As Integer = HTBOTTOMRIGHT

        Dim p As New Point(LoWord(CType(lparam, Int32)), HiWord(CType(lparam, Int32)))
        Dim topleft As Rectangle = RectangleToScreen(New Rectangle(0, 0, dwmMargins.cxLeftWidth, dwmMargins.cxLeftWidth))

        If topleft.Contains(p) Then
            Return New IntPtr(HTTOPLEFT)
        End If

        Dim topright As Rectangle = RectangleToScreen(New Rectangle(Width - dwmMargins.cxRightWidth, 0, dwmMargins.cxRightWidth, dwmMargins.cxRightWidth))

        If topright.Contains(p) Then
            Return New IntPtr(HTTOPRIGHT)
        End If

        Dim botleft As Rectangle = RectangleToScreen(New Rectangle(0, Height - dwmMargins.cyBottomHeight, dwmMargins.cxLeftWidth, dwmMargins.cyBottomHeight))

        If botleft.Contains(p) Then
            Return New IntPtr(HTBOTTOMLEFT)
        End If

        Dim botright As Rectangle = RectangleToScreen(New Rectangle(Width - dwmMargins.cxRightWidth, Height - dwmMargins.cyBottomHeight, dwmMargins.cxRightWidth, dwmMargins.cyBottomHeight))

        If botright.Contains(p) Then
            Return New IntPtr(HTBOTTOMRIGHT)
        End If

        Dim top As Rectangle = RectangleToScreen(New Rectangle(0, 0, Width, dwmMargins.cxLeftWidth))

        If top.Contains(p) Then
            Return New IntPtr(HTTOP)
        End If

        Dim cap As Rectangle = RectangleToScreen(New Rectangle(0, dwmMargins.cxLeftWidth, Width, dwmMargins.cyTopHeight - dwmMargins.cxLeftWidth))

        If cap.Contains(p) Then
            Return New IntPtr(HTCAPTION)
        End If

        Dim left As Rectangle = RectangleToScreen(New Rectangle(0, 0, dwmMargins.cxLeftWidth, Height))

        If left.Contains(p) Then
            Return New IntPtr(HTLEFT)
        End If

        Dim right As Rectangle = RectangleToScreen(New Rectangle(Width - dwmMargins.cxRightWidth, 0, dwmMargins.cxRightWidth, Height))

        If right.Contains(p) Then
            Return New IntPtr(HTRIGHT)
        End If

        Dim bottom As Rectangle = RectangleToScreen(New Rectangle(0, Height - dwmMargins.cyBottomHeight, Width, dwmMargins.cyBottomHeight))

        If bottom.Contains(p) Then
            Return New IntPtr(HTBOTTOM)
        End If

        Return New IntPtr(HTCLIENT)
    End Function
    Private Const BorderWidth As Integer = 16

    <DllImport("user32.dll")>
    Public Shared Function ReleaseCapture() As Boolean
    End Function

    <DllImport("user32.dll")>
    Public Shared Function SendMessage(ByVal hWnd As IntPtr, ByVal Msg As Integer, ByVal wParam As Integer, ByVal lParam As Integer) As Integer
    End Function

    Private Const WM_NCLBUTTONDOWN As Integer = &HA1
    Private Const HTBORDER As Integer = 18
    Private Const HTBOTTOM As Integer = 15
    Private Const HTBOTTOMLEFT As Integer = 16
    Private Const HTBOTTOMRIGHT As Integer = 17
    Private Const HTCAPTION As Integer = 2
    Private Const HTLEFT As Integer = 10
    Private Const HTRIGHT As Integer = 11
    Private Const HTTOP As Integer = 12
    Private Const HTTOPLEFT As Integer = 13
    Private Const HTTOPRIGHT As Integer = 14
#End Region




    Dim siteOFZhongchou As Integer = 0
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
        'WebBrowser2.Navigate(TextBox1.Text)
    End Sub

    Private Sub WebBrowser1_DocumentCompleted(sender As Object, e As WebBrowserDocumentCompletedEventArgs) Handles WebBrowser1.DocumentCompleted
        If Not e.Url.ToString = WebBrowser1.Url.ToString Then
            Exit Sub
        ElseIf WebBrowser1.ReadyState = WebBrowserReadyState.Complete Then
            Timer1.Enabled = True
        End If
        '  WebBrowser1.Document.Body.Style = "zoom:0.58"
    End Sub
    Dim publicZC As TaobaoZC
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Timer1.Enabled = False
        Dim hiTaobao As New TaobaoZC

        'Dim EncodingOfEXP As Encoding = Encoding.GetEncoding(WebBrowser1.Document.Encoding)
        Dim htmlMessage As String = WebBrowser1.Document.Body.InnerHtml

        If siteOFZhongchou = 0 Then
            hiTaobao.allHTML = Replace(htmlMessage, "¥", "ALITAOBAOZHONGCHOU")
            'RichTextBox1.Text = hiTaobao.allHTML
            hiTaobao.URL = WebBrowser1.Url.ToString
            hiTaobao.Name = TextProcessing.CutString(hiTaobao.allHTML, "project-title""", "</div>")
            hiTaobao.Name = TextProcessing.CutString(hiTaobao.Name, "<h1>", "</h1>")
            hiTaobao.Slogan = TextProcessing.CutString(hiTaobao.allHTML, ">项目介绍</h2>", "J_Desc")
            hiTaobao.Slogan = TextProcessing.CutString(hiTaobao.Slogan, "<div>", "</div>")
            hiTaobao.Company = TextProcessing.CutString(hiTaobao.allHTML, "<span class=""sponsor-name"">", "</span>")
            hiTaobao.CoverURL = TextProcessing.CutString(hiTaobao.allHTML, "cover-img"" src=""//", """>")
            hiTaobao.Price = TextProcessing.CutString(hiTaobao.allHTML, "repay-money""><span class=""yen"">ALITAOBAOZHONGCHOU</span>", ".")
            Try
                WebBrowser2.Document.GetElementById("product-share-url").Focus()
                WebBrowser2.Document.GetElementById("product-share-url").InnerText = hiTaobao.URL
                WebBrowser2.Document.GetElementById("product-share-title").Focus()
                WebBrowser2.Document.GetElementById("product-share-title").InnerText = hiTaobao.Name
                For Each it As HtmlElement In WebBrowser2.Document.All.GetElementsByName("description")
                    it.Focus()
                    it.InnerText = hiTaobao.Slogan
                Next
                WebBrowser2.Document.GetElementById("product-share-category").SetAttribute("value", "1")
                WebBrowser2.Document.GetElementById("product-share-company-fetch").Focus()
                WebBrowser2.Document.GetElementById("product-share-company-fetch").InnerText = hiTaobao.Company
                WebBrowser2.Document.GetElementById("product-share-price").Focus()
                WebBrowser2.Document.GetElementById("product-share-price").InnerText = hiTaobao.Price
                Dim input_bmp As Bitmap = Net.DownloadWebImage(hiTaobao.CoverURL)
                Dim output_bmp As New Bitmap(input_bmp, input_bmp.Width / input_bmp.Height * 550, 550)
                output_bmp.Save(My.Computer.FileSystem.SpecialDirectories.Desktop & "\" & hiTaobao.Name & ".png")
                For Each input As HtmlElement In WebBrowser3.Document.GetElementsByTagName("input")
                    input.Focus()
                    input.InnerText = hiTaobao.Name
                Next
            Catch ex As Exception
                Debug.WriteLine(ex.Message)
            End Try
        ElseIf siteOFZhongchou = 1 Then
            hiTaobao.allHTML = Replace(htmlMessage, "￥", "jdzhongchou")
            'RichTextBox1.Text = hiTaobao.allHTML
            hiTaobao.URL = WebBrowser1.Url.ToString
            hiTaobao.Name = TextProcessing.CutString(hiTaobao.allHTML, "<p class=""p-title"">", "</p>")
            hiTaobao.Slogan = hiTaobao.Name
            hiTaobao.Company = TextProcessing.CutString(hiTaobao.allHTML, "promoters-name", "icon-v")
            hiTaobao.Company = TextProcessing.CutString(hiTaobao.Company, "title=""", """")

            hiTaobao.CoverURL = TextProcessing.CutString(hiTaobao.allHTML, "zc-green-ing", "</div>")
            hiTaobao.CoverURL = TextProcessing.CutString(hiTaobao.CoverURL, "//", """")
            hiTaobao.CoverURL = Replace(hiTaobao.CoverURL, "&#10;", "")
            hiTaobao.CoverURL = Replace(hiTaobao.CoverURL, " ", "")
            hiTaobao.Price = TextProcessing.CutString(hiTaobao.allHTML, "支持jdzhongchou", """")
            Try
                If hiTaobao.Price = 1 Then
                    hiTaobao.Price = TextProcessing.CutString2(hiTaobao.allHTML, "支持jdzhongchou1", "支持jdzhongchou", """ clstag")
                End If
            Catch ex As Exception

            End Try

            'RichTextBox1.Text = hiTaobao.allHTML
            Try
                WebBrowser2.Document.GetElementById("product-share-url").Focus()
                WebBrowser2.Document.GetElementById("product-share-url").InnerText = hiTaobao.URL
                WebBrowser2.Document.GetElementById("product-share-title").Focus()
                WebBrowser2.Document.GetElementById("product-share-title").InnerText = hiTaobao.Name
                For Each it As HtmlElement In WebBrowser2.Document.All.GetElementsByName("description")
                    it.Focus()
                    it.InnerText = hiTaobao.Slogan
                Next
                WebBrowser2.Document.GetElementById("product-share-category").SetAttribute("value", "1")
                WebBrowser2.Document.GetElementById("product-share-company-fetch").Focus()
                WebBrowser2.Document.GetElementById("product-share-company-fetch").InnerText = hiTaobao.Company
                WebBrowser2.Document.GetElementById("product-share-price").Focus()
                WebBrowser2.Document.GetElementById("product-share-price").InnerText = hiTaobao.Price
                Dim input_bmp As Bitmap = Net.DownloadWebImage(hiTaobao.CoverURL)
                Dim output_bmp As New Bitmap(input_bmp, input_bmp.Width / input_bmp.Height * 550, 550)
                output_bmp.Save(My.Computer.FileSystem.SpecialDirectories.Desktop & "\" & hiTaobao.Name & ".png")
                For Each input As HtmlElement In WebBrowser3.Document.GetElementsByTagName("input")
                    input.Focus()
                    input.InnerText = hiTaobao.Name
                Next
            Catch ex As Exception
                Debug.WriteLine(ex.Message)
            End Try
        End If

        publicZC = hiTaobao

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        If MessageBox.Show("确认已经登录？", "确认已经登录？", MessageBoxButtons.YesNo) = DialogResult.Yes Then
            Button1.Enabled = True
            WebBrowser2.Navigate("http://wan.meizu.com/product/share")
        End If

    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'WebBrowser2.Navigate("http://wan.meizu.com/product/share") 'WebBrowser2.Navigate("http://wan.meizu.com/product/share")
        My.Computer.Registry.SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Internet Explorer\MAIN\FeatureControl\FEATURE_BROWSER_EMULATION", "淘宝众筹.vshost.exe", 11001)
        'MessageBox.Show(My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Internet Explorer\MAIN\FeatureControl\FEATURE_BROWSER_EMULATION", IO.Path.GetFileName(Application.ExecutablePath), 120))
        If Not My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Internet Explorer\MAIN\FeatureControl\FEATURE_BROWSER_EMULATION", IO.Path.GetFileName(Application.ExecutablePath), 120) = 11001 Then
            My.Computer.Registry.SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Internet Explorer\MAIN\FeatureControl\FEATURE_BROWSER_EMULATION", IO.Path.GetFileName(Application.ExecutablePath), 11001)
            MessageBox.Show("位置信息已更新，请重新打开应用程序！")
            Me.Close()
            Me.Dispose()

            End
        End If
        Debug.WriteLine(IO.Path.GetFileName(Application.ExecutablePath))
    End Sub

    Private Sub WebBrowser1_NewWindow(sender As Object, e As CancelEventArgs) Handles WebBrowser1.NewWindow
        '  WebBrowser1.Navigate(WebBrowser1.StatusText)
        If Button1.Enabled Then
            WebBrowser1.Navigate(WebBrowser1.StatusText)
        End If
        e.Cancel = True
    End Sub

    Private Sub WebBrowser3_DocumentCompleted(sender As Object, e As WebBrowserDocumentCompletedEventArgs) Handles WebBrowser3.DocumentCompleted
        Timer2.Enabled = True
    End Sub

    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick
        WebBrowser3.Document.GetElementById("entry-search").Focus()
        WebBrowser3.Document.GetElementById("entry-search").InvokeMember("click")
        Timer2.Enabled = False
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs)

        '  WebBrowser3.Document.GetElementsByTagName("input").Item(0).
        ' WebBrowser3.Document.GetElementsByTagName("input").Item(0).InnerText = "22222222"


    End Sub

    Private Sub WebBrowser2_Navigated(sender As Object, e As WebBrowserNavigatedEventArgs) Handles WebBrowser2.Navigated
        If InStr(WebBrowser2.Url.ToString, "wan.meizu.com/product") > 0 And Not InStr(WebBrowser2.Url.ToString, "product/share") > 0 And Button1.Enabled = True Then
            If MessageBox.Show("分享完毕？", "确认分享完毕？", MessageBoxButtons.YesNo) = DialogResult.Yes Then
                WebBrowser2.Navigate("http://wan.meizu.com/product/share")
                WebBrowser1.GoBack()
                Try
                    My.Computer.FileSystem.DeleteFile(My.Computer.FileSystem.SpecialDirectories.Desktop & "\" & publicZC.Name & ".png")
                Catch ex As Exception

                End Try
            End If
        End If

    End Sub

    Private Sub RadioButton2_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton2.CheckedChanged
        siteOFZhongchou = 1
        WebBrowser1.Navigate("http://z.jd.com/bigger/search.html?categoryId=10")
    End Sub

    Private Sub RadioButton1_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton1.CheckedChanged
        siteOFZhongchou = 0
        WebBrowser1.Navigate("https://hi.taobao.com/market/hi/list.php?spm=a215p.1472830.1.8.qv8Zht#type=121288001&page=1&status=3")
    End Sub

    Private Sub Button3_Click_1(sender As Object, e As EventArgs)
        'WebBrowser1.Navigate("baidu.com")
    End Sub

    Private Sub Button3_Click_2(sender As Object, e As EventArgs) Handles Button3.Click
        Me.Dispose()
    End Sub
End Class

Class TextProcessing
    Shared Function CutString(ResourceString As String, Head As String, Tail As String)
        Try
            Dim cutStart = InStr(ResourceString, Head) + Head.Length
            Dim cutLength = InStr(Start:=cutStart, String1:=ResourceString, String2:=Tail) - InStr(ResourceString, Head) - Head.Length
            Return (Mid(ResourceString, cutStart, cutLength))
        Catch ex As Exception
            Debug.WriteLine(ex.Message)
        End Try

    End Function
    Shared Function CutString2(ResourceString As String, start As String, Head As String, Tail As String)
        Try
            Dim cutStart = InStr(InStr(ResourceString, start) + CStr(InStr(ResourceString, start)).Length, ResourceString, Head) + Head.Length
            Dim cutLength = 40 'InStr(Start:=cutStart, String1:=ResourceString, String2:=Tail) - InStr(ResourceString, Head) - Head.Length - CStr(InStr(ResourceString, start)).Length
            Dim tempStr = (Mid(ResourceString, cutStart, cutLength))
            tempStr = Mid(tempStr, 1, InStr(tempStr, """") - 1)
            Return tempStr
        Catch ex As Exception
            Debug.WriteLine(ex.Message)
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
