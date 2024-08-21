Imports System.IO
Imports System.Threading
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
        'Text = "*[列表已保存]列表连发编辑器"
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
            If dosavefile = False Then
                If (SaveFileDialog1.ShowDialog() = DialogResult.OK) Then
                    savefile = SaveFileDialog1.FileName
                    File.Create(savefile)
                    chdatetime = Date.Now
                    File.SetLastWriteTime(savefile, chdatetime)
                    Dim savestr As StreamWriter = New StreamWriter(savefile, False)
                    Using (savestr)
                        Dim i As Integer
                        For i = 0 To ListBox1.Items.Count()
                            savestr.WriteLine（ListBox1.GetItemText(i)）
                        Next
                        savestr.Close()
                    End Using
                    Text = "[列表已保存]列表连发编辑器"
                    dosavefile = True
                    savedpath = savefile
                Else
                    Text = "[列表未保存]列表连发编辑器"
                End If
            Else '文件已经“另存为”了
                chdatetime = Date.Now
                File.SetLastWriteTime(savedpath, chdatetime)
                Dim savestr As StreamWriter = New StreamWriter(savedpath, False)
                Using (savestr)
                    Dim i As Integer
                    For i = 0 To ListBox1.Items.Count()
                        savestr.WriteLine（ListBox1.GetItemText(i)）
                    Next
                    savestr.Close()
                End Using
                Text = "[列表已保存]列表连发编辑器"
            End If
        Catch er As Exception
            showexpdlg("列表连发编辑器保存错误，您的文件可能未被正确保存。", er.ToString)
            Text = "[列表未保存]列表连发编辑器"
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
    Private Sub open() 'todo:打开文件
        Try

        Catch er As Exception

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
End Class