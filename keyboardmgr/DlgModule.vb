Module DlgModule
    Public Sub showexpdlg(ex As String, text As String)
        Dim frm As New exp()
        frm.TextBox1.AppendText("异常发生位置：" & ex)
        frm.TextBox1.AppendText("系统名称：" & My.Computer.Info.OSFullName & vbNewLine)
        frm.TextBox1.AppendText("系统版本：" & My.Computer.Info.OSVersion & vbNewLine)
        'NET2.0下判断x86还是64
        If Environment.GetEnvironmentVariable("ProgramFiles(x86)") = "" Then
            frm.TextBox1.AppendText("系统平台：x86" & vbNewLine)
        Else
            frm.TextBox1.AppendText("系统平台：x64" & vbNewLine)
        End If
        frm.TextBox1.AppendText("以下是异常内容：" & vbCrLf)
        frm.TextBox1.AppendText(text)
        frm.ShowDialog()
    End Sub
End Module
