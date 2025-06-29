Imports System.IO
Imports System.Threading
Imports Microsoft.Win32
Public Class Form3
    Dim line As Integer
    Dim dosavefile As Boolean = False
    Dim savefile As String '保存路径名
    Dim chdatetime As Date = Date.Now '文件修改时间
    Dim savedpath As String
    Dim donewest As Boolean = True
    Dim dosendonce As Boolean = False
    Private Sub Form3_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        NumericUpDown.Value = Settings1.Default.listsendtime
        'If Form1.dodarkmode = True Then

        'Else

        'End If
    End Sub
    Private Sub 添加新行ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 添加新行AToolStripMenuItem.Click
        addnewline()
    End Sub
    Private Sub ToolStripButton1_Click(sender As Object, e As EventArgs) Handles ToolStripButton1.Click
        addnewline()
    End Sub

    Private Sub addnewline()
        If line = 0 Then
            ListBox1.Items.Add("新行")
        Else
            ListBox1.Items.Insert(line, "新行")
        End If
        linechanged()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If ListBox1.SelectedIndex = -1 Then
            ToolTip1.Show("没有选择行！", Label3)
        Else
            Dim editline As Integer
            editline = ListBox1.SelectedIndex
            ListBox1.Items.RemoveAt(editline)
            ListBox1.Items.Insert(editline, TextBox1.Text)
        End If
        linechanged()
    End Sub

    Private Sub ListBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox1.SelectedIndexChanged
        linechanged()
    End Sub
    Private Sub linechanged()
        line = ListBox1.SelectedIndex + 1
        If line = 0 Then
            Label3.Text = "未选择"
        Else
            Label3.Text = "第" & line & “行"
        End If
        donewest = False
        Text = "*[列表已保存]列表连发编辑器"
    End Sub

    Private Sub ToolStripButton2_Click(sender As Object, e As EventArgs) Handles ToolStripButton2.Click
        removeselectedline()
    End Sub
    Private Sub 移除选定行ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 移除选定行ToolStripMenuItem.Click
        removeselectedline()
    End Sub
    Private Sub removeselectedline()
        If ListBox1.SelectedIndex = -1 Then
            ToolTip1.Show("没有选择行！", Label3)
        Else
            ListBox1.Items.RemoveAt(ListBox1.SelectedIndex)
        End If
    End Sub

    Private Sub ToolStripButton3_Click(sender As Object, e As EventArgs) Handles ToolStripButton3.Click
        startsave()
    End Sub

    Private Sub 保存ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 保存ToolStripMenuItem.Click
        startsave()
    End Sub
    Private Sub startsave() '启动保存线程
        Dim savethr As Thread = New Thread(AddressOf save)
        savethr.SetApartmentState(ApartmentState.STA)
        savethr.Priority = ThreadPriority.BelowNormal
        savethr.Start()
    End Sub
    Private Sub save()
        Try
            Dim delimiter As String = "★"
            Dim allLines As New List(Of String)
            For i As Integer = 0 To ListBox1.Items.Count - 1
                allLines.Add(ListBox1.Items(i).ToString())
            Next
            Dim content As String = String.Join(delimiter, allLines)

            If dosavefile = False Then
                If (SaveFileDialog1.ShowDialog() = DialogResult.OK) Then
                    savefile = SaveFileDialog1.FileName
                    chdatetime = Date.Now
                    Using savestr As New StreamWriter(savefile, False)
                        savestr.Write(content)
                    End Using
                    File.SetLastWriteTime(savefile, chdatetime)
                    Me.Invoke(Sub()
                                  Text = "[列表已保存]列表连发编辑器"
                              End Sub)
                    dosavefile = True
                    savedpath = savefile
                Else
                    Me.Invoke(Sub()
                                  Text = "[列表未保存]列表连发编辑器"
                              End Sub)
                End If
            Else '文件已经“另存为”了
                chdatetime = Date.Now
                Using savestr As New StreamWriter(savedpath, False)
                    For i As Integer = 0 To ListBox1.Items.Count - 1
                        savestr.WriteLine(ListBox1.Items(i).ToString())
                    Next
                End Using
                File.SetLastWriteTime(savedpath, chdatetime)
                Me.Invoke(Sub()
                              Text = "[列表已保存]列表连发编辑器"
                          End Sub)
            End If
        Catch er As Exception
            Me.Invoke(Sub()
                          showexpdlg("列表连发编辑器保存错误，您的文件可能未被正确保存。", er.ToString)
                          Text = "[列表未保存]列表连发编辑器"
                      End Sub)
            dosavefile = False
            savedpath = ""
        End Try
    End Sub

    Private Sub ToolStripButton5_Click(sender As Object, e As EventArgs) Handles ToolStripButton5.Click
        startopen()
    End Sub

    Private Sub 打开ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 打开ToolStripMenuItem.Click
        startopen()
    End Sub
    Private Sub startopen()
        Dim openthr As Thread = New Thread(AddressOf open)
        openthr.SetApartmentState(ApartmentState.STA)
        openthr.Priority = ThreadPriority.BelowNormal
        openthr.Start()
    End Sub
    Private Sub open()
        Try
            If OpenFileDialog1.ShowDialog() = DialogResult.OK Then
                Dim delimiter As String = "★"
                Dim content As String = File.ReadAllText(OpenFileDialog1.FileName)
                Dim lines() As String = content.Split(New String() {delimiter}, StringSplitOptions.None)
                Me.Invoke(Sub()
                              ListBox1.Items.Clear()
                              For Each line As String In lines
                                  ListBox1.Items.Add(line)
                              Next
                              savedpath = OpenFileDialog1.FileName
                              dosavefile = True
                              Text = "[列表已打开]列表连发编辑器"
                          End Sub)
            End If
        Catch er As Exception
            Me.Invoke(Sub()
                          showexpdlg("列表连发编辑器打开错误，您的文件可能未被正确读取。", er.ToString)
                          Text = "[列表未打开]列表连发编辑器"
                      End Sub)
            dosavefile = False
            savedpath = ""
        End Try
    End Sub

    Public Sub OpenFileByPath(path As String) '处理文件双击的打开
        Show()
        Try
            Dim delimiter As String = "★"
            Dim content As String = IO.File.ReadAllText(path)
            Dim lines() As String = content.Split(New String() {delimiter}, StringSplitOptions.None)
            ListBox1.Items.Clear()
            For Each line As String In lines
                ListBox1.Items.Add(line)
            Next
            savedpath = path
            dosavefile = True
            Text = "[列表已打开]列表连发编辑器"
        Catch ex As Exception
            MessageBox.Show("打开文件失败：" & ex.Message)
        End Try
    End Sub

    '收尾工作
    Private Sub Form3_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        'todo
    End Sub
    'fix：处理空列表引发异常
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Try
            Settings1.Default.listsendtime = NumericUpDown.Value
            Settings1.Default.Save()
            Timer.Interval = NumericUpDown.Value
            Timer.Enabled = True
            Form2.Show()
            Enabled = False
            ListBox1.SelectedIndex = 0
        Catch ex As Exception
            ToolTip1.SetToolTip(Button2, "处理列表时遇到问题。请检查是否使用了空列表！")
            Return
        End Try
    End Sub
#Disable Warning BC42025
    Private Sub Timer_Tick(sender As Object, e As EventArgs) Handles Timer.Tick
        Try
            Dim sendstring As String
            If ListBox1.SelectedIndex < ListBox1.Items.Count - 1 Then
                ListBox1.SelectedIndex += 1
            ElseIf dosendonce = False Then
                ListBox1.SelectedIndex = 0
            Else
                Enabled = True
                Timer.Enabled = False
                Form2.Close()
            End If
            sendstring = ListBox1.GetItemText(ListBox1.SelectedItem)
            Dim sk As SendKeys
            Clipboard.Clear()
            Clipboard.SetText(sendstring)
            sk.Send("^v")
            sk.Send("{Enter}")
        Catch ex As Exception
            ToolTip1.SetToolTip(Button2, "处理列表时遇到问题。请检查是否使用了空列表！")
            Enabled = True
            Timer.Enabled = False
            Form2.Close()
            Return
        End Try
    End Sub
#Enable Warning BC42025
    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked = True Then
            dosendonce = True
        Else
            dosendonce = False
        End If
    End Sub

    Private Sub 置顶ToolStripMenuItem_CheckedChanged(sender As Object, e As EventArgs) Handles 置顶ToolStripMenuItem.CheckedChanged
        If 置顶ToolStripMenuItem.Checked = False Then
            TopMost = False
        Else
            TopMost = True
        End If
    End Sub

    Private Sub 列表连发文件关联绑定BToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 列表连发文件关联绑定BToolStripMenuItem.Click
        Try
            '检查是否有管理员权限
            If Not IsAdministrator() Then
                If MessageBox.Show("此操作需要管理员权限，是否以管理员权限重启程序？", "权限不足", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = DialogResult.Yes Then
                    Dim psi As New ProcessStartInfo()
                    psi.FileName = Application.ExecutablePath
                    psi.Verb = "runas" ' 提升权限
                    Process.Start(psi)
                    Application.Exit() ' 退出当前实例
                End If
                Return
            End If
            Dim ext As String = ".lcslst"
            Dim progId As String = "ListSendFile"
            Dim appPath As String = Application.ExecutablePath

            '关联扩展名和ProgID
            Registry.SetValue("HKEY_CLASSES_ROOT\" & ext, "", progId)

            '设置ProgID的描述
            Registry.SetValue("HKEY_CLASSES_ROOT\" & progId, "", "LCS列表连发文件")
            Registry.SetValue("HKEY_CLASSES_ROOT\" & progId & "\DefaultIcon", "", appPath & ",0")
            Registry.SetValue("HKEY_CLASSES_ROOT\" & progId & "\shell\open\command", "", """" & appPath & """ ""%1""")
            Label3.Text = "文件关联已绑定"
        Catch ex As Exception
            ToolTip1.SetToolTip(Button2, "绑定文件关联失败" & ex.Message)
        End Try
    End Sub

    Private Function IsAdministrator() As Boolean
        Try
            Dim identity = System.Security.Principal.WindowsIdentity.GetCurrent()
            Dim principal = New System.Security.Principal.WindowsPrincipal(identity)
            Return principal.IsInRole(System.Security.Principal.WindowsBuiltInRole.Administrator)
        Catch ex As Exception
            Return False
        End Try
    End Function
End Class