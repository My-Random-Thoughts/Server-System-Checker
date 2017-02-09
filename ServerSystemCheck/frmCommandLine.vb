Public Class frmCommandLine
	Private Sub frmCommandLine_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
		For Each c As Control In Controls
			c.Font = frmMain.sysFont
		Next
		lblTitle.Font = frmMain.sysFontTitle

		picIcon.Image = My.Resources._48___MainIcon.ToBitmap
		Me.Location = New Point(25, 25)
		Me.Visible = True
		Me.BringToFront()

		Application.DoEvents()
		If (xmlLoadConfig() = True) Then
			Dim sGUID As String = xml_getGUIDFromGroup(sCommand_Group)
			frmScan.sGroupGUID = sGUID
			frmScan.ShowDialog(Me)
			Me.Close()
			Me.Dispose()
		End If
	End Sub
End Class