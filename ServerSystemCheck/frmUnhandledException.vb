Imports Microsoft.VisualBasic.ApplicationServices
Public Class frmUnhandledException
	Public uErrorMessage As UnhandledExceptionEventArgs
	Private Sub frmUnhandledException_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
		For Each c As Control In Controls
			c.Font = frmMain.sysFont
		Next
		lblTitle.Font = frmMain.sysFontTitle

		picIcon.Image = My.Resources._48___MainIcon___Error.ToBitmap

		lblSubTitle.Text = uErrorMessage.Exception.Message
		txtStackTrace.Text = uErrorMessage.Exception.Source & vbCrLf
		txtStackTrace.Text = txtStackTrace.Text & uErrorMessage.Exception.StackTrace
		txtStackTrace.SelectionStart = 0
		picIcon.Focus()

		Dim dtNow As String = Now.ToString("yyyy-MM-dd--HH-mm")
		System.IO.File.WriteAllText(Application.StartupPath & "\SSC-Crash-Log-" & dtNow & ".log", lblSubTitle.Text & vbCrLf & vbCrLf & txtStackTrace.Text)
	End Sub
	Private Sub txtStackTrace_KeyUp(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles txtStackTrace.KeyUp
		If (e.Control And e.KeyCode = Keys.A) Then txtStackTrace.SelectAll()
	End Sub
	Private Sub cmdExit_Click(sender As System.Object, e As System.EventArgs) Handles cmdExit.Click
		uErrorMessage.ExitApplication = True
		Me.Close()
		Me.Dispose()
	End Sub

	Private Sub lnkCopyClipboard_LinkClicked(sender As System.Object, e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lnkCopyClipboard.LinkClicked
		Clipboard.SetText(txtStackTrace.Text)
		Application.DoEvents()
		If (Clipboard.GetText = txtStackTrace.Text) Then
			MessageBox.Show(Me, "Error copied successfully", APN, MessageBoxButtons.OK, MessageBoxIcon.Information)
		End If
	End Sub
End Class