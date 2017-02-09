Public Class frmAddWMIQueryTestResults
	Private Sub frmAddWMIQueryTestResults_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
		For Each c As Control In Controls
			c.Font = frmMain.sysFont
		Next

		If (txtResult.Lines.Count > 1) Then
			lblNote.Text = "This result will not pass a boolean test, try adding filters"
		Else
			lblNote.Text = "Test Successful"
		End If
	End Sub

	Private Sub cmdClose_Click(sender As System.Object, e As System.EventArgs) Handles cmdClose.Click
		Me.Close()
		Me.Dispose()
	End Sub
End Class