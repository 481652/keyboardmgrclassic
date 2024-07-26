
Public Class Form4
    Public image As Image


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


End Class