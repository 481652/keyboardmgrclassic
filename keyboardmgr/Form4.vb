
Public Class Form4
    Public image As Image
    Dim dotop As Boolean = False

    '发送
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Clipboard.SetImage(image)
        Settings1.Default.imgsendtime = NumericUpDown.Value
        Settings1.Default.Save()
        Label3.Enabled = False
        NumericUpDown.Enabled = False
        Label4.Enabled = False
        LinkLabel1.Enabled = False
        Button1.Enabled = False
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
            LinkLabel1.Visible = True
            Button1.Visible = True
        End If
    End Sub

    Private Sub Form4_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        NumericUpDown.Value = Settings1.Default.imgsendtime
        Button1.Visible = False
        LinkLabel1.Visible = False
    End Sub

    Private Sub rmfile()
        image.Dispose()
        PictureBox1.Visible = False
        Label1.Visible = True
        Label2.Visible = True
        Button2.Visible = True
        LinkLabel1.Visible = False
        Button1.Visible = False
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

    Private Sub Label5_Click(sender As Object, e As EventArgs) Handles Label5.Click
        If dotop = False Then
            TopMost = True
            dotop = True
            Label5.Text = "取消置顶"
        Else
            TopMost = False
            dotop = False
            Label5.Text = "点我置顶"
        End If
    End Sub
    '检查拖放
    Private Sub Form4_DragEnter(sender As Object, e As DragEventArgs) Handles MyBase.DragEnter
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            e.Effect = DragDropEffects.Copy
        End If
    End Sub
    '拖放添加图片
    Private Sub Form4_DragDrop(sender As Object, e As DragEventArgs) Handles MyBase.DragDrop
        ' 获取拖放的文件列表
        Try
            '确保只取一个图片
            Dim files() As String = e.Data.GetData(DataFormats.FileDrop)
            If files.Length > 0 Then
                image = Image.FromFile(files(0))
            End If
        Catch ex As Exception
            ToolTip1.Show("拖放的图片无效！", Label1)
            Return
        End Try
        PictureBox1.Image = image
        PictureBox1.Visible = True
        Label1.Visible = False
        Label2.Visible = False
        Button2.Visible = False
        PictureBox1.Image = image
        LinkLabel1.Visible = True
        Button1.Visible = True
    End Sub


End Class