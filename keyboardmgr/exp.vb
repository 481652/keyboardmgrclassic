Public Class exp

    Private Sub OK_Button_Click(sender As Object, e As System.EventArgs) Handles OK_Button.Click
        DialogResult = DialogResult.OK
        Form1.Show()
        Close()
    End Sub
    Private Sub Practise_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        Application.Exit()

    End Sub

    Private Sub exp_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LinkLabel1.LinkColor = Form1.usercolor
    End Sub

    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        Process.Start("https://github.com/481652/keyboardmgr/issues")
    End Sub
End Class
