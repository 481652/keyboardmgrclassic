Public Class Form2
    Public Declare Auto Function RegisterHotKey Lib "user32.dll" Alias "RegisterHotKey" (ByVal hwnd As IntPtr, ByVal id As Integer, ByVal fsModifiers As Integer, ByVal vk As Integer) As Boolean
    Public Declare Auto Function UnRegisterHotKey Lib "user32.dll" Alias "UnregisterHotKey" (ByVal hwnd As IntPtr, ByVal id As Integer) As Boolean

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click
        Call stopClicking()
    End Sub

    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        RegisterHotKey(Handle, 0, 0, Keys.Escape)
        '第3个参数意义： 0=nothing 1 -alt 2-ctrl 3-ctrl+alt 4-shift 5-alt+shift 6-ctrl+shift 7-ctrl+shift+alt
    End Sub
    Private Sub Form1_Closed(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Closed
        UnRegisterHotKey(Handle, 0)
    End Sub
    Protected Overrides Sub WndProc(ByRef m As Message)
        If m.Msg = 786 Then
            Activate()
            Call stopClicking()
        End If
        MyBase.WndProc(m)
    End Sub
    Sub stopClicking()
        Form1.RadioButton1.Enabled = True
        Form1.RadioButton2.Enabled = True
        Form1.Timer1.Enabled = False
        Form1.Timer2.Enabled = False
        Form1.Button1.Enabled = True
        Form1.NumericUpDown1.Enabled = True
        Form1.Focus()
        Close()
    End Sub
End Class