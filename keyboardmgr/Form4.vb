

Public Class Form4
    Public image As Image
    Private Sub Form4_DragDrop(sender As Object, e As DragEventArgs) Handles MyBase.DragDrop
        '检查是否是Windows文件的放置格式
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            e.Effect = DragDropEffects.All
            '这里Effect属性被设置成DragDropEffects.All，并不会影响到源数据
        End If
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            image = Image.FromFile(e.Data.GetData(DataFormats.FileDrop))
            PictureBox1.Image = image
            PictureBox1.Visible = True
            Label1.Visible = False
            Label2.Visible = False
            Button2.Visible = False
            PictureBox1.Image = image
        End If
    End Sub

    '发送
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Clipboard.SetImage(image)
        Settings1.Default.imgsendtime = NumericUpDown.Value
        Settings1.Default.Save()
        Timer.Interval = NumericUpDown.Value
        Timer.Enabled = True
        Form2.Show()
    End Sub
    '打开图片
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If (OpenFileDialog1.ShowDialog() = DialogResult.OK) Then
            image = Image.FromFile(OpenFileDialog1.FileName)
            PictureBox1.Image = image
            PictureBox1.Visible = True
            Label1.Visible = False
            Label2.Visible = False
            Button2.Visible = False
            PictureBox1.Image = image
        End If
    End Sub

    Private Sub Form4_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        NumericUpDown.Value = Settings1.Default.imgsendtime
    End Sub

    Private Sub rmfile()
        image.Dispose()
        PictureBox1.Visible = False
        Label1.Visible = True
        Label2.Visible = True
        Button2.Visible = True
    End Sub

    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        rmfile()
    End Sub

    Private Sub Timer_Tick(sender As Object, e As EventArgs) Handles Timer.Tick
        Dim sk As SendKeys
#Disable Warning BC42025
        sk.Send("^v")
#Enable Warning BC42025
#Disable Warning BC42025
        sk.Send("{Enter}")
#Enable Warning BC42025
    End Sub

    Private Sub OpenFileDialog1_FileOk(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles OpenFileDialog1.FileOk

    End Sub
End Class