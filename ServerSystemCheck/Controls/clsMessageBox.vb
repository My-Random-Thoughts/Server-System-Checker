Imports System.Text
Public Class clsMessageBox
	Private Shared TheLabels() As String
	Private Shared TheLabelIndex As Integer

	Public Shared Sub CustomMsgBox(ByVal Labels() As String)
		TheLabels = Labels
		Application.OpenForms(0).BeginInvoke(New FindWindowDelegate(AddressOf FindMsgBox), GetCurrentThreadId())
	End Sub

	Private Shared Sub FindMsgBox(ByVal tid As Integer)
		EnumThreadWindows(tid, AddressOf EnumWindow, IntPtr.Zero)
	End Sub

	Private Shared Function EnumWindow(ByVal hWnd As IntPtr, ByVal lp As IntPtr) As Boolean
		Dim sb As New StringBuilder(256)
		GetClassName(hWnd, sb, sb.Capacity)
		If (sb.ToString() <> "#32770") Then Return True
		TheLabelIndex = 0
		EnumChildWindows(hWnd, AddressOf FindButtons, IntPtr.Zero)
		Return False
	End Function

	Private Shared Function FindButtons(ByVal hWnd As IntPtr, ByVal lp As IntPtr) As Boolean
		Dim sb As New StringBuilder(256)
		GetClassName(hWnd, sb, sb.Capacity)
		If ((sb.ToString() = "Button") And (TheLabelIndex <= UBound(TheLabels))) Then
			SetWindowText(hWnd, TheLabels(TheLabelIndex))
			TheLabelIndex += 1
		End If
		Return True
	End Function

	Private Delegate Sub FindWindowDelegate(ByVal tid As Integer)
	Private Delegate Function EnumWindowDelegate(ByVal hWnd As IntPtr, ByVal lp As IntPtr) As Boolean
	Private Declare Auto Function EnumThreadWindows Lib "user32.dll" (ByVal tid As Integer, ByVal callback As EnumWindowDelegate, ByVal lp As IntPtr) As Boolean
	Private Declare Auto Function EnumChildWindows Lib "user32.dll" (ByVal hWnd As IntPtr, ByVal callback As EnumWindowDelegate, ByVal lp As IntPtr) As Boolean
	Private Declare Auto Function GetClassName Lib "user32.dll" (ByVal hWnd As IntPtr, ByVal name As StringBuilder, ByVal maxlen As Integer) As Integer
	Private Declare Auto Function GetCurrentThreadId Lib "kernel32.dll" () As Integer
	Private Declare Auto Function SetWindowText Lib "user32.dll" (ByVal hWnd As IntPtr, ByVal text As String) As Boolean
End Class
