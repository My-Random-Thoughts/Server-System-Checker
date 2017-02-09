Option Explicit On

Public Class frmAddEventlog
	Public lEditValues As ListViewItem' Are we EDITING an existing item.?

	Private Sub frmEventLog_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
		For Each c As Control In Controls
			c.Font = frmMain.sysFont
		Next
		lblTitle.Font = frmMain.sysFontTitle
		lnkHelp.Font = frmMain.sysFontHelp
		lnkNote.Font = frmMain.sysFontHelp
		lnkNote.Top = lnkHelp.Bottom + 1

		lnkHelp.LinkBehavior = LinkBehavior.HoverUnderline
		lnkNote.LinkBehavior = LinkBehavior.HoverUnderline
		picIcon.Image = My.Resources._48___Eventlog.ToBitmap

		If (lEditValues IsNot Nothing) Then
			lblTitle.Text = "Edit Existing" & lblTitle.Text.Substring(7)
			Me.Text = " " & lblTitle.Text
		End If

		picImage_Critical.Image = My.Resources._16___Eventlog___Critical.ToBitmap
		picImage_Error.Image = My.Resources._16___Eventlog___Error.ToBitmap
		picImage_Warning.Image = My.Resources._16___Eventlog___Warning.ToBitmap
		picImage_Information.Image = My.Resources._16___Eventlog___Information.ToBitmap
		picImage_Success.Image = My.Resources._16___Eventlog___Success.ToBitmap
		picImage_Failure.Image = My.Resources._16___Eventlog___Failure.ToBitmap

		' Set Default values...
		radApplication.Checked = True
		chkCritical.Checked = True
		chkError.Checked = True
		chkWarning.Checked = True
		chkInformation.Checked = False
		chkSuccess.Checked = False
		chkFailure.Checked = False

		txtExclude.Text = vbNullString
		Call radSecurity_CheckedChanged(Nothing, Nothing)

		' Override with existing values...
		If (lEditValues IsNot Nothing) Then
			Select Case lEditValues.Text
				Case "Application" : radApplication.Checked = True
				Case "System" : radSystem.Checked = True
				Case "Security" : radSecurity.Checked = True
				Case Else : radSystem.Checked = True
			End Select

			Dim sLevels As String = lEditValues.SubItems(2).Text
			Select Case True
				Case sLevels.Contains("Excluding:")
					txtExclude.Text = vbNullString
					tabSearchType.SelectedTab = tabSearchType.TabPages(0)
					If (InStr(sLevels, "Critical, ") > 0) Then chkCritical.Checked = True Else chkCritical.Checked = False
					If (InStr(sLevels, "Error, ") > 0) Then chkError.Checked = True Else chkError.Checked = False
					If (InStr(sLevels, "Warning, ") > 0) Then chkWarning.Checked = True Else chkWarning.Checked = False
					If (InStr(sLevels, "Information, ") > 0) Then chkInformation.Checked = True Else chkInformation.Checked = False
					If (InStr(sLevels, "Success, ") > 0) Then chkSuccess.Checked = True Else chkSuccess.Checked = False
					If (InStr(sLevels, "Failure, ") > 0) Then chkFailure.Checked = True Else chkFailure.Checked = False
					Try
						txtExclude.Text = sLevels.Substring(InStr(sLevels, "Excluding: ", CompareMethod.Text) + 10)
						txtExclude.Text = cleanEventlogIDList(txtExclude.Text)
					Catch
					End Try

				Case sLevels.Contains("Specifically:")
					txtSpecificIDs.Text = vbNullString
					tabSearchType.SelectedTab = tabSearchType.TabPages(1)
					Try
						txtSpecificIDs.Text = sLevels.Substring(InStr(sLevels, "Specifically: ", CompareMethod.Text) + 13)
						txtSpecificIDs.Text = cleanEventlogIDList(txtSpecificIDs.Text)
					Catch
					End Try

				Case Else
					txtExclude.Text = vbNullString
					tabSearchType.SelectedTab = tabSearchType.TabPages(0)
			End Select
		End If

		If (bReadOnlyMode = True) Then cmdOK.Enabled = False
		Me.Visible = True
		Application.DoEvents()
	End Sub

	Private Sub cmdCancel_Click(sender As System.Object, e As System.EventArgs) Handles cmdCancel.Click
		frmMain.m_AddEventLog = Nothing
		Me.Close()
		Me.Dispose()
	End Sub

	Private Sub cmdOK_Click(sender As System.Object, e As System.EventArgs) Handles cmdOK.Click

		Dim sResult As String = vbNullString
		If (radApplication.Checked = True) Then sResult = "Application|"
		If (radSystem.Checked = True) Then sResult = "System|"
		If (radSecurity.Checked = True) Then sResult = "Security|"
		If (sResult = vbNullString) Then Exit Sub

		If (tabSearchType.SelectedTab.Name = "TabPage1") Then
			If (chkCritical.Checked = True) Then sResult = sResult & "Critical, "
			If (chkError.Checked = True) Then sResult = sResult & "Error, "
			If (chkWarning.Checked = True) Then sResult = sResult & "Warning, "
			If (chkInformation.Checked = True) Then sResult = sResult & "Information, "
			If (chkSuccess.Checked = True) Then sResult = sResult & "Success, "
			If (chkFailure.Checked = True) Then sResult = sResult & "Failure, "
			sResult = sResult & "Excluding: " & cleanEventlogIDList(txtExclude.Text)
		Else

			If (txtSpecificIDs.Text = vbNullString) Then Exit Sub
			sResult = sResult & "Specifically: " & cleanEventlogIDList(txtSpecificIDs.Text)
		End If

		' RESOURCE DUPLICATE CHECK...
		Dim dResult As DialogResult = resourceDuplicationAlert("Eventlog Scan", lEditValues, ({sResult}.ToList))
		If (dResult <> Windows.Forms.DialogResult.None) Then Exit Sub

		' Settings are saved in calling procedure ('frmMain.onClick_addResource_Eventlog' or 'frmMain.onClick_editProperties')
		lResourceConflicts = Nothing
		frmMain.m_AddEventLog = sResult
		Me.Close()
		Me.Dispose()
	End Sub

	Private Sub lnkHelp_LinkClicked(sender As System.Object, e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lnkHelp.LinkClicked
		frmHelp.sSelectPageByID = "help0009"
		frmHelp.ShowDialog(Me)
	End Sub

	Private Sub lnkNote_LinkClicked(sender As System.Object, e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lnkNote.LinkClicked
		frmHelp.sSelectPageByID = "help0022"
		frmHelp.ShowDialog(Me)
	End Sub

	Private Sub txtExclude_Leave(sender As Object, e As System.EventArgs) Handles txtExclude.Leave
		txtExclude.Text = cleanEventlogIDList(txtExclude.Text)
	End Sub

	Private Sub txtIDs_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles txtSpecificIDs.KeyPress, txtExclude.KeyPress
		Dim bHandled As Boolean = True
		Dim allowedchars As String = "0123456789,- " & vbCrLf & Chr(Keys.Back)
		If (allowedchars.Contains(e.KeyChar)) Then bHandled = False
		e.Handled = bHandled
	End Sub

	Private Sub txtSpecificIDs_Leave(sender As Object, e As System.EventArgs) Handles txtSpecificIDs.Leave
		txtSpecificIDs.Text = cleanEventlogIDList(txtSpecificIDs.Text)
	End Sub

	Private Sub radSecurity_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles radSecurity.CheckedChanged
		chkSuccess.Enabled = radSecurity.Checked
		chkFailure.Enabled = radSecurity.Checked

		If (radSecurity.Checked = False) Then
			chkSuccess.Checked = False
			chkFailure.Checked = False
			picImage_Success.Image = GreyScaleImage(My.Resources._16___Eventlog___Success.ToBitmap, True)
			picImage_Failure.Image = GreyScaleImage(My.Resources._16___Eventlog___Failure.ToBitmap, True)
		Else
			picImage_Success.Image = My.Resources._16___Eventlog___Success.ToBitmap
			picImage_Failure.Image = My.Resources._16___Eventlog___Failure.ToBitmap
		End If
	End Sub

	Private Sub chkCritical_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkCritical.CheckedChanged
		Call enableOKButton()
	End Sub
	Private Sub chkError_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkError.CheckedChanged
		Call enableOKButton()
	End Sub
	Private Sub chkWarning_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkWarning.CheckedChanged
		Call enableOKButton()
	End Sub
	Private Sub chkInformation_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkInformation.CheckedChanged
		Call enableOKButton()
	End Sub
	Private Sub chkSuccess_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkSuccess.CheckedChanged
		Call enableOKButton()
	End Sub
	Private Sub chkFailure_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkFailure.CheckedChanged
		Call enableOKButton()
	End Sub
	Private Sub txtSpecificIDs_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtSpecificIDs.TextChanged
		Call enableOKButton()
	End Sub
	Private Sub tabSearchType_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles tabSearchType.SelectedIndexChanged
		Call enableOKButton()
	End Sub
	Private Sub enableOKButton()
		If (tabSearchType.SelectedTab.Name = "TabPage1") Then
			cmdOK.Enabled = (chkCritical.Checked Or chkError.Checked Or chkWarning.Checked Or chkInformation.Checked Or chkSuccess.Checked Or chkFailure.Checked)
		Else
			cmdOK.Enabled = (txtSpecificIDs.Text.Length > 0)
		End If
		If (bReadOnlyMode = True) Then cmdOK.Enabled = False
	End Sub
End Class