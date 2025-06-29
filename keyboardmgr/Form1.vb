Imports System.Net.Http
Imports System.Runtime.InteropServices
Imports Microsoft.Win32


Public Class Form1
    '定义
    Public ThemeColor As Boolean
    Public usercolor As Color
    Public Shared dodarkmode As Boolean
    Dim hooks As Boolean
    Dim state As String
    Dim i As Integer
    Declare Sub mouse_event Lib "user32" (dwFlags As Long, dx As Long, dy As Long, cButtons As Long, dwExtraInfo As Long)
    Public Const MOUSEEVENTF_LEFTDOWN = &H2 '模拟鼠标左键按下
    Public Const MOUSEEVENTF_LEFTUP = &H4 '模拟鼠标左键释放
    Public Const MOUSEEVENTF_RIGHTDOWN = &H8 '模拟鼠标右键按下
    Public Const MOUSEEVENTF_RIGHTUP = &H10 '模拟鼠标右键释放
    Private Declare Function GetCursorPos Lib "user32" (ByRef lpPoint As POINTAPI) As Long '全屏坐标声明
    Private Declare Function ScreenToClient Lib "user32.dll" (hwnd As Integer, ByRef lpPoint As POINTAPI) As Integer '窗口坐标声明
    Dim P As POINTAPI
    Dim doclose As Integer = False
    Dim startup As Boolean
    Dim dotop As Boolean = False
    '注册热键
    Public Declare Auto Function RegisterHotKey Lib "user32.dll" Alias "RegisterHotKey" (hwnd As IntPtr, id As Integer, fsModifiers As Integer, vk As Integer) As Boolean
    Public Declare Auto Function UnRegisterHotKey Lib "user32.dll" Alias "UnregisterHotKey" (hwnd As IntPtr, id As Integer) As Boolean
    Private Structure POINTAPI '声明坐标变量
        Public x As Integer '声明坐标变量为32位
        Public y As Integer '声明坐标变量为32位
    End Structure
    '转换颜色
    Function ConvertSystemColor(HexColor As String) As Color
        Return Color.FromArgb(Convert.ToInt32(HexColor.Substring(0, 2), 16), Convert.ToInt32(HexColor.Substring(2, 2), 16), Convert.ToInt32(HexColor.Substring(4, 2), 16), Convert.ToInt32(HexColor.Substring(6, 2), 16))
    End Function
    Function GetSystemColor() As Color
        Dim key As RegistryKey = Registry.CurrentUser.OpenSubKey("SOFTWARE\Microsoft\Windows\DWM")
        If key IsNot Nothing Then
            Dim value As Integer = key.GetValue("ColorizationColor")
            Dim HexColor = Convert.ToString(value, 16)
            key.Close()
            Return ConvertSystemColor(HexColor)
        End If

    End Function
    Protected Overrides Sub WndProc(ByRef m As Message) '注册热键
        If m.Msg = 786 Then
            Show（）
            Activate() '显示主界面
        End If
        MyBase.WndProc(m)
    End Sub
    '窗体load事件
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Visible = False
        NumericUpDown1.Value = Settings1.Default.clicktime
        NumericUpDown2.Value = Settings1.Default.sendtime
        '初始化       
        Select Case Settings1.Default.startpage
            Case 1
                TabControl1.SelectedTab = TabPage0
                ComboBox1.Text = "欢迎"
            Case 2
                TabControl1.SelectedTab = TabPage1
                ComboBox1.Text = "连点"
            Case 3
                TabControl1.SelectedTab = TabPage2
                ComboBox1.Text = "连发"
            Case Else
                TabControl1.SelectedTab = TabPage0
                ComboBox1.Text = "欢迎"
        End Select
        If Settings1.Default.doforceclose = True Then
            CheckBox1.Checked = True
        Else
            CheckBox1.Checked = False
        End If


        RegisterHotKey(Handle, 0, 0, Keys.F4)
        Visible = True
        version.Text = "版本号：" & My.Application.Info.Version.Major & "." & My.Application.Info.Version.Minor & "." & My.Application.Info.Version.Build & "." & My.Application.Info.Version.Revision
        If Settings1.Default.dostartup = True Then
            startup = True
            CheckBox2.Checked = True
        Else
            startup = False
            CheckBox2.Checked = False
        End If

        Dim args() As String = Environment.GetCommandLineArgs()
        If args.Length > 1 Then
            Dim fileToOpen As String = args(1)
            If IO.File.Exists(fileToOpen) Then
                Form3.OpenFileByPath(fileToOpen)
            End If
        End If



    End Sub
    '窗体closing事件
    Private Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If Settings1.Default.doforceclose = False Then
            If doclose = False Then
                e.Cancel = True
                Hide()
            Else
                Application.Exit()
            End If
        Else
            Application.Exit()
        End If
    End Sub

    '网站
    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        Process.Start("http://lcs.info.gf/")
    End Sub
    'github页面
    Private Sub LinkLabel2_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel2.LinkClicked
        Process.Start("https://github.com/481652/keyboardmgr")
    End Sub
    '反馈问题
    Private Sub LinkLabel3_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel3.LinkClicked
        Process.Start("https://github.com/481652/keyboardmgr/issues")
    End Sub
    '更多信息
    Private Async Sub LinkLabel7_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel7.LinkClicked
        Dim url As String = "https://lcs.rth1.xyz/documents/kbdmgrclassic.txt"
        Dim httpClient As New HttpClient()
        httpClient.Timeout = TimeSpan.FromSeconds(30)
        httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36")
        httpClient.DefaultRequestHeaders.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8")
        Try
            '发送GET请求并获取响应内容
            Dim response As HttpResponseMessage = Await httpClient.GetAsync(url)
            response.EnsureSuccessStatusCode()
            Dim content As String = Await response.Content.ReadAsStringAsync()
            '处理响应内容
            MsgBox(content, MsgBoxStyle.OkOnly + MsgBoxStyle.Information, "来自LCS服务器的公告")
        Catch ex As HttpRequestException
            MsgBox("获取公告失败：发送请求时出错，可能是无网络连接、防火墙阻止或LCS服务出现问题！", MsgBoxStyle.OkOnly + MsgBoxStyle.Critical, "错误")
        Catch ex As TaskCanceledException
            MsgBox("获取公告失败：请求超时，可能是网络连接差或LCS服务出现问题！" & vbCrLf & "详细信息：", MsgBoxStyle.OkOnly + MsgBoxStyle.Critical, "错误")
        Catch ex As Exception
            MsgBox("获取公告失败：发生未知错误。" & vbCrLf & "详细信息：" & ex.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.Critical, "错误")
        End Try
    End Sub



    '鼠标连点

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        If NumericUpDown1.Value >= 1 Then
            If NumericUpDown1.Value <= 1000 Then
            End If
        Else
            ToolTip1.SetToolTip(NumericUpDown1, "值必须在1至1000之间。")

        End If
        If RadioButton1.Checked = True Then
            Timer1.Enabled = True
            Timer1.Interval = NumericUpDown1.Value
        Else
            Timer2.Enabled = True
            Timer2.Interval = NumericUpDown1.Value
        End If
        TabControl1.Enabled = False
        Settings1.Default.clicktime = NumericUpDown1.Value
        Settings1.Default.Save()
        Form2.Show()
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        '左键连点
        Dim P As POINTAPI
        GetCursorPos(P)
        mouse_event(MOUSEEVENTF_LEFTDOWN, P.x.ToString, P.y.ToString, 0, 0)
        mouse_event(MOUSEEVENTF_LEFTUP, P.x.ToString, P.y.ToString, 0, 0)
    End Sub

    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick
        '右键连点
        Dim P As POINTAPI
        GetCursorPos(P)
        mouse_event(MOUSEEVENTF_RIGHTDOWN, P.x.ToString, P.y.ToString, 0, 0)
        mouse_event(MOUSEEVENTF_RIGHTUP, P.x.ToString, P.y.ToString, 0, 0)
    End Sub

    Private Sub RadioButton5_CheckedChanged(sender As Object, e As EventArgs)
        Settings1.Default.doAutochange = True
    End Sub
    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        '设置项“startpage”含义：1=欢迎 2=连点 3=连发 其它=欢迎（默认）
        Select Case ComboBox1.SelectedItem
            Case "欢迎"
                Settings1.Default.startpage = 1
            Case "连点"
                Settings1.Default.startpage = 2
            Case "连发"
                Settings1.Default.startpage = 3
            Case Else
                Settings1.Default.startpage = 1
        End Select
    End Sub
    '连发
    Sub CreateClipBoard(CopyText As String)
        Clipboard.Clear()
        Clipboard.SetText(CopyText)
    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If TextBox2.Text IsNot "" Then  'fix:检测文本框内容是否为空
            CreateClipBoard(TextBox2.Text)
        Else
            ToolTip1.SetToolTip(Button2, "连发内容不能为空")
            Return
        End If
        Timer3.Enabled = True
        Timer3.Interval = NumericUpDown2.Value
        TabControl1.Enabled = False
        Settings1.Default.sendtime = NumericUpDown2.Value
        Settings1.Default.Save()
        Form2.Show()

    End Sub
#Disable Warning BC42025
    Private Sub Timer3_Tick(sender As Object, e As EventArgs) Handles Timer3.Tick
        Dim sk As SendKeys
        sk.Send("^v")
        sk.Send("{Enter}")
    End Sub
#Enable Warning BC42025
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Form3.Show()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Form4.Show()
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked = True Then
            Settings1.Default.doforceclose = True
        Else
            Settings1.Default.doforceclose = False
        End If

    End Sub


    Private Sub ToolStripMenuItem4_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem4.Click
        doclose = True
        Application.Exit()
    End Sub

    Private Sub ToolStripMenuItem2_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem2.Click
        Show()
        Activate()
        TabControl1.SelectedTab = TabPage1
    End Sub

    Private Sub ToolStripMenuItem3_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem3.Click
        Show()
        Activate()
        TabControl1.SelectedTab = TabPage2
    End Sub

    Private Sub TrayIcon_MouseClick(sender As Object, e As MouseEventArgs) Handles TrayIcon.MouseClick
        If e.Button = Windows.Forms.MouseButtons.Left Then
            Show()
            Activate()
        End If
    End Sub

    Private Sub ToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem1.Click
        Show()
        Activate()
    End Sub

    Private Sub 图片连发ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 图片连发ToolStripMenuItem.Click
        Form4.Show()

    End Sub
    '保存设置
    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click

        Settings1.Default.Save()
        '检查是否设置开机自启，并置开机自启
        Try
            Dim appPath As String = Application.ExecutablePath
            Dim keyPath As String = "SOFTWARE\Microsoft\Windows\CurrentVersion\Run"
            Dim rk As RegistryKey = Nothing
            rk = Registry.CurrentUser.OpenSubKey(keyPath, True)
            If startup = True Then
                If rk Is Nothing Then
                    rk = Registry.CurrentUser.CreateSubKey(keyPath)
                End If
                rk.SetValue("keyboardmgr", Chr(34) & appPath & Chr(34))
                rk.Close()
            Else
                If rk Is Nothing Then
                    rk = Registry.CurrentUser.CreateSubKey(keyPath)
                End If
                If rk.GetValue("keyboardmgr") IsNot Nothing Then
                    rk.DeleteValue("keyboardmgr")
                End If
            End If
        Catch ex As Exception
            showexpdlg("设置程序随系统启动错误，可能是因为权限问题或被杀软拦截。", ex.ToString)
        End Try
    End Sub

    Private Sub CheckBox2_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox2.CheckedChanged
        If CheckBox2.Checked = True Then
            Settings1.Default.dostartup = True
            startup = True
        Else
            Settings1.Default.dostartup = False
            startup = False
        End If

    End Sub
    '设置置顶
    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click
        If dotop = False Then
            TopMost = True
            dotop = True
            Label1.Text = "取消置顶"
        Else
            TopMost = False
            dotop = False
            Label1.Text = "点我置顶"
        End If
    End Sub

    Private Sub 列表连发ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 列表连发ToolStripMenuItem.Click
        Form3.Show()
    End Sub

End Class
