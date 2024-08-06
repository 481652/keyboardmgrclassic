Imports Microsoft.Win32
Imports System.Runtime.InteropServices


Public Class Form1
    '定义
    Public ThemeColor As Boolean
    Public usercolor As Color
    Public Shared dodarkmode As Boolean
    Dim hooks As Boolean
    Dim state As String
    Dim i As Integer
    Public lightmodecolor As Color
    Public darkmodecolor As Color
    Declare Sub mouse_event Lib "user32" (ByVal dwFlags As Long, ByVal dx As Long, ByVal dy As Long, ByVal cButtons As Long, ByVal dwExtraInfo As Long)
    Public Const MOUSEEVENTF_LEFTDOWN = &H2 '模拟鼠标左键按下
    Public Const MOUSEEVENTF_LEFTUP = &H4 '模拟鼠标左键释放
    Public Const MOUSEEVENTF_RIGHTDOWN = &H8 '模拟鼠标右键按下
    Public Const MOUSEEVENTF_RIGHTUP = &H10 '模拟鼠标右键释放
    Private Declare Function GetCursorPos Lib "user32" (ByRef lpPoint As POINTAPI) As Long '全屏坐标声明
    Private Declare Function ScreenToClient Lib "user32.dll" (ByVal hwnd As Integer, ByRef lpPoint As POINTAPI) As Integer '窗口坐标声明
    Dim P As POINTAPI
    Dim doclose As Integer = False
    Dim startup As Boolean
    '注册热键
    Public Declare Auto Function RegisterHotKey Lib "user32.dll" Alias "RegisterHotKey" (ByVal hwnd As IntPtr, ByVal id As Integer, ByVal fsModifiers As Integer, ByVal vk As Integer) As Boolean
    Public Declare Auto Function UnRegisterHotKey Lib "user32.dll" Alias "UnregisterHotKey" (ByVal hwnd As IntPtr, ByVal id As Integer) As Boolean

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
    Private Sub GetThemeColor()
        Dim key As RegistryKey = Registry.CurrentUser.OpenSubKey("SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize")

        If key IsNot Nothing Then
            Dim appsUseLightTheme As Integer = key.GetValue("AppsUseLightTheme", -1)

            If appsUseLightTheme = 0 Then   '系统为深色模式
                darkmode()
            ElseIf appsUseLightTheme = 1 Then  '系统为浅色模式
                lightmode()
            Else

            End If

            key.Close()
        End If

    End Sub
    Sub ChangeTheme(sender As Object, e As UserPreferenceChangedEventArgs)
        If e.Category = UserPreferenceCategory.General Then
            If Settings1.Default.doAutochange = True Then   '还要检测一遍是否跟随系统
                GetThemeColor()
                DwmSetWindowAttribute(Handle, DwmWindowAttribute.UseImmersiveDarkMode, ThemeColor, Marshal.SizeOf(Of Integer))
                usercolor = GetSystemColor()
                LinkLabel1.LinkColor = usercolor
                LinkLabel2.LinkColor = usercolor
            End If
        End If
    End Sub
    Sub darkmode()
        For Each TabPages In TabControl1.TabPages 'tabpage设置前景和背景
            TabPages.backcolor = ColorTranslator.FromHtml("#101010")
            TabPages.forecolor = Color.White
        Next
        BackColor = ColorTranslator.FromHtml("#101010")
        For i = 0 To Controls.Count - 1
            If TypeOf Controls(i) Is TextBox Then '如果是文本框控件
                Controls(i).BackColor = ColorTranslator.FromHtml("#696969")
            End If
        Next
        ComboBox1.BackColor = ColorTranslator.FromHtml("#696969")
        For i = 0 To Controls.Count - 1
            If TypeOf Controls(i) Is GroupBox Then '如果是groupbox控件
                Controls(i).BackColor = ColorTranslator.FromHtml("#101010")
                Controls(i).ForeColor = Color.White
            End If
        Next
        For i = 0 To Controls.Count - 1
            If TypeOf Controls(i) Is Button Then '如果是button控件
                Controls(i).BackColor = ColorTranslator.FromHtml("#696969")
            End If
        Next
        BackColor = ColorTranslator.FromHtml("#101010")
        ForeColor = Color.WhiteSmoke
        dodarkmode = True
    End Sub
    Sub lightmode()
        For Each TabPages In TabControl1.TabPages 'tabpage设置前景和背景
            TabPages.backcolor = Color.WhiteSmoke
            TabPages.forecolor = Color.Black
        Next
        BackColor = Color.WhiteSmoke
        For i = 0 To Controls.Count - 1
            If TypeOf Controls(i) Is TextBox Then '如果是Windows文本框控件
                Controls(i).BackColor = ColorTranslator.FromHtml("#F0F0F0")
            End If
        Next
        ComboBox1.BackColor = ColorTranslator.FromHtml("#F0F0F0")
        For i = 0 To Controls.Count - 1
            If TypeOf Controls(i) Is GroupBox Then '如果是groupbox控件
                Controls(i).BackColor = Color.WhiteSmoke
                Controls(i).ForeColor = Color.Black
            End If
        Next
        For i = 0 To Controls.Count - 1
            If TypeOf Controls(i) Is Button Then '如果是button控件
                Controls(i).BackColor = ColorTranslator.FromHtml("#F0F0F0")
            End If
        Next
        BackColor = Color.WhiteSmoke
        ForeColor = Color.Black
        dodarkmode = False
    End Sub
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
        '检测是否设置了“跟随系统”以及设置radiobutton状态、深浅色模式等
        If Settings1.Default.doAutochange = True Then
            GetThemeColor()
            RadioButton5.Checked = True
            AddHandler SystemEvents.UserPreferenceChanged, AddressOf ChangeTheme
        ElseIf Settings1.Default.dodarkmode = True Then
            darkmode()
            RadioButton4.Checked = True
        Else
            lightmode()
            RadioButton3.Checked = True
        End If
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
        '设置标题
        Dim title As String = Settings1.Default.title

        Text = title
        TextBox3.Text = title

        RegisterHotKey(Handle, 0, 0, Keys.F4)
        '关闭没做完的功能
        If Settings1.Default.doopendevelopingfeatures = False Then
            RadioButton4.Visible = False
            RadioButton5.Visible = False
            GroupBox4.Visible = False
            Button3.Visible = False
        End If
        Visible = True
        version.Text = "版本号：" & My.Application.Info.Version.Major & "." & My.Application.Info.Version.Minor & "." & My.Application.Info.Version.Build & "." & My.Application.Info.Version.Revision
        If Settings1.Default.dostartup = True Then
            startup = True
        Else
            startup = False
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
        Process.Start("https://481652.github.io/")
    End Sub
    'github页面
    Private Sub LinkLabel2_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel2.LinkClicked
        Process.Start("https://github.com/481652/keyboardmgr")
    End Sub
    '反馈问题
    Private Sub LinkLabel3_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel3.LinkClicked
        Process.Start("https://github.com/481652/keyboardmgr/issues")
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

    Private Sub RadioButton5_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton5.CheckedChanged
        Settings1.Default.doAutochange = True
    End Sub
    <DllImport("dwmapi.dll", PreserveSig:=True)>
    Public Shared Function DwmSetWindowAttribute(ByVal hwnd As IntPtr, ByVal attr As DwmWindowAttribute, ByRef attrValue As Integer, ByVal attrSize As Integer) As Integer

    End Function
    Public Enum DwmWindowAttribute '枚举窗口属性
        NCRenderingEnabled = 1
        NCRenderingPolicy
        TransitionsForceDisabled
        AllowNCPaint
        CaptionButtonBounds
        NonClientRtlLayout
        ForceIconicRepresentation
        Flip3DPolicy
        ExtendedFrameBounds
        HasIconicBitmap
        DisallowPeek
        ExcludedFromPeek
        Cloak
        Cloaked
        FreezeRepresentation
        PassiveUpdateMode
        UseHostBackdropBrush
        UseImmersiveDarkMode = 20
        WindowCornerPreference = 33
        BorderColor
        CaptionColor
        TextColor
        VisibleFrameBorderThickness
        SystemBackdropType
        Last
    End Enum

    Private Sub RadioButton3_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton3.CheckedChanged
        Settings1.Default.doAutochange = False
        Settings1.Default.dodarkmode = False
        lightmode()
    End Sub

    Private Sub RadioButton4_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton4.CheckedChanged
        Settings1.Default.doAutochange = False
        Settings1.Default.dodarkmode = True
        darkmode()
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
    Sub CreateClipBoard(ByVal CopyText As String)
        Clipboard.Clear()
        Clipboard.SetText(CopyText)
    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        CreateClipBoard(TextBox2.Text)
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
        '标题
        Settings1.Default.title = TextBox3.Text
        Text = TextBox3.Text

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
End Class
