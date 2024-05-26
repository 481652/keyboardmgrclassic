Imports System.IO
Imports System.Threading
Public Class Form3
    Dim line As String
    Dim dosavefile As Boolean = False
    Dim savefile As String = SaveFileDialog1.FileName '保存路径名
    Dim crdatetime As Date = Date.Now '文件新建时间
    Dim chdatetime As Date = Date.Now '文件修改时间
    Dim savedpath As String
    Private Sub Form3_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If Form1.dodarkmode = True Then

        Else

        End If
    End Sub
    Private Sub 添加新行ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 添加新行AToolStripMenuItem.Click
        If line = 0 Then
            ListBox1.Items.Add("新行")
        Else
            ListBox1.Items.Insert(line, "新行")
        End If
        linechanged()
    End Sub
    Private Sub ToolStripButton1_Click(sender As Object, e As EventArgs) Handles ToolStripButton1.Click
        If line = 0 Then
            ListBox1.Items.Add("新行")
        Else
            ListBox1.Items.Insert(line, "新行")
        End If
        linechanged()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If ListBox1.SelectedIndex = -1 Then
            ToolTip1.Show("没有选择行！", Button1)
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
        line = ListBox1.SelectedIndex.ToString + 1
        If line = 0 Then
            Label3.Text = "未选择"
        Else
            Label3.Text = "第" & line & “行"
        End If
    End Sub

    Private Sub ToolStripButton2_Click(sender As Object, e As EventArgs) Handles ToolStripButton2.Click
        ListBox1.Items.RemoveAt(ListBox1.SelectedIndex)
    End Sub

    Private Sub ToolStripButton3_Click(sender As Object, e As EventArgs) Handles ToolStripButton3.Click
        startsave()
    End Sub

    Private Sub 保存ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 保存ToolStripMenuItem.Click
        startsave()
    End Sub
    Private Sub startsave() '启动保存线程
        Dim savethr As Thread = New Thread(AddressOf save)
        savethr.Priority = ThreadPriority.BelowNormal
        savethr.Start()
    End Sub
    Private Sub save()
        Try
            If dosavefile = False Then
                If (SaveFileDialog1.ShowDialog() = DialogResult.OK) Then
                    File.Create(savefile)
                    chdatetime = Date.Now
                    File.SetLastWriteTime(savefile, chdatetime)
                    crdatetime = Date.Now
                    File.SetCreationTime(savefile, crdatetime)
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
                crdatetime = Date.Now
                File.SetCreationTime(savedpath, crdatetime)
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
            showexpdlg("列表连发编辑器保存错误", er.ToString)
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

    End Sub


End Class