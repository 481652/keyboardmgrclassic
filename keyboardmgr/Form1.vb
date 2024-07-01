Imports Microsoft.Win32
Imports System.Runtime.InteropServices


Public Class Form1
    '定义
    Public ThemeColor As Boolean
    Public color As Color
    Public Shared dodarkmode As Boolean
    Dim hooks As Boolean
    Dim state As String
    Dim i As Integer
    Declare Sub mouse_event Lib "user32" (ByVal dwFlags As Long, ByVal dx As Long, ByVal dy As Long, ByVal cButtons As Long, ByVal dwExtraInfo As Long)
    Public Const MOUSEEVENTF_LEFTDOWN = &H2 '模拟鼠标左键按下
    Public Const MOUSEEVENTF_LEFTUP = &H4 '模拟鼠标左键释放
    Public Const MOUSEEVENTF_RIGHTDOWN = &H8 '模拟鼠标右键按下
    Public Const MOUSEEVENTF_RIGHTUP = &H10 '模拟鼠标右键释放
    Private Declare Function GetCursorPos Lib "user32" (ByRef lpPoint As POINTAPI) As Long '全屏坐标声明
    Private Declare Function ScreenToClient Lib "user32.dll" (ByVal hwnd As Integer, ByRef lpPoint As POINTAPI) As Integer '窗口坐标声明
    Dim P As POINTAPI
    '注册热键
    Public Declare Auto Function RegisterHotKey Lib "user32.dll" Alias "RegisterHotKey" (ByVal hwnd As IntPtr, ByVal id As Integer, ByVal fsModifiers As Integer, ByVal vk As Integer) As Boolean
    Public Declare Auto Function UnRegisterHotKey Lib "user32.dll" Alias "UnregisterHotKey" (ByVal hwnd As IntPtr, ByVal id As Integer) As Boolean

    Private Structure POINTAPI '声明坐标变量
        Public x As Integer '声明坐标变量为32位
        Public y As Integer '声明坐标变量为32位
    End Structure
    '自动更改主题色，深浅色
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
                color = GetSystemColor()
                LinkLabel1.LinkColor = color
                LinkLabel2.LinkColor = color
            End If
        End If
    End Sub
    Sub darkmode()
        Panel1.BackColor = Color.DarkCyan
        For Each TabPages In TabControl1.TabPages
            TabPages.backcolor = Color.LightSeaGreen
        Next
        BackColor = Color.LightSeaGreen
        TextBox1.BackColor = Color.MediumTurquoise
        TextBox2.BackColor = Color.MediumTurquoise
        TextBox3.BackColor = Color.MediumTurquoise
        Button1.BackColor = Color.MediumTurquoise
        Button2.BackColor = Color.MediumTurquoise
        Button3.BackColor = Color.MediumTurquoise
        Button4.BackColor = Color.MediumTurquoise
        ComboBox1.BackColor = Color.MediumTurquoise
        GroupBox1.BackColor = Color.LightSeaGreen
        GroupBox2.BackColor = Color.LightSeaGreen
        GroupBox3.BackColor = Color.LightSeaGreen
        CheckBox1.BackColor = Color.LightSeaGreen
        dodarkmode = True
    End Sub
    Sub lightmode()
        Panel1.BackColor = Color.DeepSkyBlue
        For Each TabPages In TabControl1.TabPages
            TabPages.backcolor = Color.LightCyan
        Next
        BackColor = Color.LightCyan
        TextBox1.BackColor = Color.PaleTurquoise
        TextBox2.BackColor = Color.PaleTurquoise
        TextBox3.BackColor = Color.PaleTurquoise
        Button1.BackColor = Color.PaleTurquoise
        Button2.BackColor = Color.PaleTurquoise
        Button3.BackColor = Color.PaleTurquoise
        Button4.BackColor = Color.PaleTurquoise
        ComboBox1.BackColor = Color.PaleTurquoise
        GroupBox1.BackColor = Color.LightCyan
        GroupBox2.BackColor = Color.LightCyan
        GroupBox3.BackColor = Color.LightCyan
        CheckBox1.BackColor = Color.LightCyan
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
        NumericUpDown1.Value = Settings1.Default.clicktime
        NumericUpDown2.Value = Settings1.Default.sendtime
        '检测是否设置了“跟随系统”以及设置radiobutton状态、深浅色模式
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
        Text = Settings1.Default.title
        TextBox3.Text = Text
        Label4.Text = Text
        RegisterHotKey(Handle, 0, 0, Keys.F4)
        '关闭没做完的功能

        GroupBox2.Visible = False
        GroupBox4.Visible = False

    End Sub

    '标题栏
    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click
        If Settings1.Default.doforceclose = True Then
            Application.Exit()
        Else
            Hide() '隐藏窗体
        End If
    End Sub

    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles Label2.Click
        WindowState = FormWindowState.Minimized
    End Sub
    Private Sub Label3_Click(sender As Object, e As EventArgs) Handles Label3.Click
        If TopMost = False Then
            TopMost = True
            Label3.Text = "|解除"
        Else
            TopMost = False
            Label3.Text = "|置顶"

        End If

    End Sub
    '窗体移动
    Declare Auto Function ReleaseCapture Lib "user32.dll" Alias "ReleaseCapture" () As Boolean
    'API ReleaseCapture函数是用来释放鼠标捕获的
    Declare Auto Function SendMessage Lib "user32.dll" Alias "SendMessage" (ByVal hWnd As IntPtr, ByVal wMsg As Integer, ByVal wParam As Integer, ByVal lParam As Integer) As IntPtr
    '向windows发送消息
    Public Const WM_SYSCOMMAND As Integer = &H112&
    Public Const SC_MOVE As Integer = &HF010&
    Public Const HTCAPTION As Integer = &H2&

    Private Sub Form1_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs) Handles MyBase.MouseMove
        If e.Button = MouseButtons.Left Then
            ReleaseCapture()
            SendMessage(Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0)
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
        Settings1.Default.Save()
    End Sub
    <DllImport("dwmapi.dll", PreserveSig:=True)>
    Public Shared Function DwmSetWindowAttribute(ByVal hwnd As IntPtr, ByVal attr As DwmWindowAttribute, ByRef attrValue As Integer, ByVal attrSize As Integer) As Integer

    End Function
    Public Enum DwmWindowAttribute
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
        Settings1.Default.Save()
        lightmode()
    End Sub

    Private Sub RadioButton4_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton4.CheckedChanged
        Settings1.Default.doAutochange = False
        Settings1.Default.dodarkmode = True
        Settings1.Default.Save()
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
        Settings1.Default.Save()
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

    Private Sub Timer3_Tick(sender As Object, e As EventArgs) Handles Timer3.Tick
        Dim sk As SendKeys
#Disable Warning BC42025
        sk.Send("^v")
#Enable Warning BC42025
#Disable Warning BC42025
        sk.Send("{Enter}")
#Enable Warning BC42025

    End Sub

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
        Settings1.Default.Save()
    End Sub

    Private Sub TextBox3_TextChanged(sender As Object, e As EventArgs) Handles TextBox3.TextChanged
        Text = TextBox3.Text
        Label4.Text = TextBox3.Text
        Settings1.Default.title = TextBox3.Text
        Settings1.Default.Save()
    End Sub

    Private Sub ToolStripMenuItem4_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem4.Click
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


End Class
