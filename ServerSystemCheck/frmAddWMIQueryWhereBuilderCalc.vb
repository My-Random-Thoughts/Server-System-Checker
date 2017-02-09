Imports System.Globalization

Public Class frmAddWMIQueryWhereBuilderCalc

	Private Sub frmAddWMIQueryWhereBuilderCalc_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
		For Each c As Control In Controls
			c.Font = frmMain.sysFont
		Next
		lblOutput.Font = frmMain.sysFontBold

		With cmoUnits.Items
			.Clear()
			.Add(" KB")
			.Add(" MB")
			.Add(" GB")
			.Add(" TB")
			.Add(" PB")
		End With
		txtInput.Text = "1"
		cmoUnits.SelectedIndex = 0
	End Sub

	Private Sub changeOutput()
		lblOutput.Text = "0"
		If (IsNumeric(txtInput.Text) = False) Then Exit Sub
		Try
			lblOutput.Text = CDec(CInt(txtInput.Text) * (1024 ^ (cmoUnits.SelectedIndex + 1))).ToString
			cmdOK.Enabled = True
		Catch
			lblOutput.Text = "Number Too Big"
			cmdOK.Enabled = False
		End Try

		If (lblOutput.Text.Length > 20) Then
			lblOutput.Text = "Number Too Big"
			cmdOK.Enabled = False
		End If
	End Sub

	Private Sub cmoUnits_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cmoUnits.SelectedIndexChanged
		Call changeOutput()
	End Sub

	Private Sub txtInput_KeyUp(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles txtInput.KeyUp
		If (e.KeyCode = Keys.Enter) Then Call cmdOK_Click(sender, e)
	End Sub

	Private Sub txtInput_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtInput.TextChanged
		Call changeOutput()
	End Sub

	Private Sub cmdCancel_Click(sender As System.Object, e As System.EventArgs) Handles cmdCancel.Click
		Close()
	End Sub

	Private Sub cmdOK_Click(sender As System.Object, e As System.EventArgs) Handles cmdOK.Click
		frmAddWMIQueryWhereBuilder.txtWhereFreeText.Text = lblOutput.Text
		Close()
	End Sub
End Class