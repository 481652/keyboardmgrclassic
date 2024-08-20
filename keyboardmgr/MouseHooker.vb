Imports System.Runtime.InteropServices
Module MouseHooker

    <DllImport("User32.dll", CharSet:=CharSet.Auto, CallingConvention:=CallingConvention.StdCall)>
        Public Function SetWindowsHookEx(idHook As Integer, HookProc As KeyHook, hInstance As IntPtr, wParam As Integer) As Integer
        End Function
        <DllImport("User32.dll", CharSet:=CharSet.Auto, CallingConvention:=CallingConvention.StdCall)>
        Public Function CallNextHookEx(idHook As Integer, nCode As Integer, wParam As Integer, lParam As IntPtr) As Integer
        End Function
        <DllImport("User32.dll", CharSet:=CharSet.Auto, CallingConvention:=CallingConvention.StdCall)>
        Public Function UnhookWindowsHookEx(idHook As Integer) As Boolean
        End Function
        <DllImport("kernel32.dll", CharSet:=CharSet.Auto, CallingConvention:=CallingConvention.StdCall)>
        Public Function GetModuleHandle(name As String) As IntPtr
        End Function

        <StructLayout(LayoutKind.Sequential)>
        Public Structure KBDLLHOOKSTRUCT
            Public vkCode As Keys
            Public scanCode As Keys
            Public flags As Integer
            Public time As Integer
            Public dwExtraInfo As Integer
        End Structure
        Public Const HC_ACTION As Integer = 0

        Public Const WH_KEYBOARD_LL As Integer = 13

        Public Delegate Function KeyHook(Code As Integer, wParam As Integer, lParam As IntPtr) As Integer

        '<MarshalAs(UnmanagedType.FunctionPtr)>
        Public callback As KeyHook

    End Module
