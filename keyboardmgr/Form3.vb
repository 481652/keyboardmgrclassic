Imports System.IO
Public Class Form3
    Dim line As String
    Dim dosavefile As Boolean = False
    Dim savefile As String = SaveFileDialog1.FileName '保存路径名
    Dim crdatetime As Date = Date.Now '文件新建时间
    Dim chdatetime As Date = Date.Now '文件修改时间
    Dim savedpath As String

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
        save()
    End Sub

    Private Sub 保存ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 保存ToolStripMenuItem.Click
        save()
    End Sub

    Private Sub save()
        If dosavefile = False Then
            If (SaveFileDialog1.ShowDialog() = DialogResult.OK) Then
                Try
                    crdatetime = Date.Now
                    File.Delete(savefile)
                    File.Create(savefile)
                    File.SetCreationTime(savefile, crdatetime)
                    Dim savestr As StreamWriter = File.CreateText(savefile)
                    Using (savestr)
                        Dim i As Integer
                        For i = 0 To ListBox1.Items.Count()
                            savestr.WriteLine（ListBox1.GetItemText(i)）
                        Next
                    End Using
                    Text = "[列表已保存]列表连发编辑器"
                    dosavefile = True
                    savedpath = savefile
                Catch er As Exception
                    MsgBox("保存失败，原因：" & er.ToString(), MsgBoxStyle.OkOnly, "")
                    Text = "[列表未保存]列表连发编辑器"
                End Try
            Else
                Text = "[列表未保存]列表连发编辑器"
            End If
        Else '文件已经“另存为”了


        End If
    End Sub


End Class