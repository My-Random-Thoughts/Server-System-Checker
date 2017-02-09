Option Explicit On

Public Class frmAddFreeSpace
	Public sResourceGUID As String ' Are we editing an existing resource
	Private iDriveThreshold_Fail As Integer = 10 ' Set Default
	Private iDriveThreshold_Warn As Integer = 20 ' Thresholds

	Private Sub frmDriveThresholds_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
		For Each c As Control In Controls
			c.Font = frmMain.sysFont
		Next
		lblTitle.Font = frmMain.sysFontTitle
		lnkHelp.Font = frmMain.sysFontHelp

		lnkHelp.LinkBehavior = LinkBehavior.HoverUnderline
		picIcon.Image = My.Resources._48___Drive.ToBitmap
		If (sResourceGUID IsNot Nothing) Then
			lblTitle.Text = "Edit Existing" & lblTitle.Text.Substring(7)
			Me.Text = " " & lblTitle.Text
		End If

		trackCritical.Value = iDriveThreshold_Fail
		trackWarning.Value = iDriveThreshold_Warn

		Dim cItem As ctrlComboBox_Icons.IconComboItem
		With cmoScanResult
			.ItemHeight = 19
			With .Items
				.Clear()
				cItem = New ctrlComboBox_Icons.IconComboItem("Include")
				cItem.ItemImage = CType(My.Resources.ResourceManager.GetObject("_16___Add"), Drawing.Icon)
				.Add(cItem)

				cItem = New ctrlComboBox_Icons.IconComboItem("Exclude")
				cItem.ItemImage = CType(My.Resources.ResourceManager.GetObject("_16___Remove"), Drawing.Icon)
				.Add(cItem)
			End With
			.SelectedIndex = 1
		End With
		cItem = Nothing

		If (sResourceGUID IsNot Nothing) Then
			Dim sDT As String = xml_getSpecificItem(sResourceGUID, "checking")
			If (sDT IsNot Nothing) Then
				Dim sData() As String = sDT.Split("|"c)
				Try
					trackCritical.Value = CInt(sData(0))
					If ((trackCritical.Value < 1) Or (trackCritical.Value > 98)) Then trackCritical.Value = iDriveThreshold_Fail

					trackWarning.Value = CInt(sData(1))
					If ((trackWarning.Value < 2) Or (trackWarning.Value > 99)) Then trackWarning.Value = iDriveThreshold_Warn
				Catch
				End Try

				If (sData.Count > 2) Then
					If (cmoScanResult.Items(0).DisplayText.Trim = sData(2)) Then cmoScanResult.SelectedIndex = 0 Else cmoScanResult.SelectedIndex = 1
				End If
			End If
		End If

		cmdOK.Enabled = Not bReadOnlyMode
		cmdReset.Enabled = Not bReadOnlyMode

		Call displayGraphic()
		Me.Visible = True
		Application.DoEvents()
	End Sub

	Private Sub cmdCancel_Click(sender As System.Object, e As System.EventArgs) Handles cmdCancel.Click
		frmMain.m_AddFreeSpace = Nothing
		Me.Close()
		Me.Dispose()
	End Sub

	Private Sub displayGraphic()

		picDTBackground.Image = My.Resources.fs_bg_long
		picDTBackground.BringToFront()

		With picSpaceG
			.Image = My.Resources.fs_c_g
			.Location = New Point((picDTBackground.Left + 1), (picDTBackground.Top + 1))
			.Size = New Size(picDTBackground.Width - 2, 12)
			.BringToFront()
		End With

		With picEndcapL
			.Image = My.Resources.fs_l_r
			.Location = New Point((picDTBackground.Left + 1), (picDTBackground.Top + 1))
			.Size = New Size(2, 12)
			.BringToFront()
		End With

		With picEndcapR
			.Image = My.Resources.fs_r_g
			.Location = New Point((picDTBackground.Left + picDTBackground.Width - 3), (picDTBackground.Top + 1))
			.Size = New Size(2, 12)
			.BringToFront()
		End With

		Dim iWidth As Integer = CInt((((picDTBackground.Width) - 2) / 100) * trackCritical.Value)
		With picSpaceR
			.Image = My.Resources.fs_c_r
			.Location = New Point((picDTBackground.Left + 1), (picDTBackground.Top + 1))
			.Size = New Size(iWidth, (picDTBackground.Height - 2))
			.BringToFront()
		End With

		iWidth = CInt(((picDTBackground.Width - 2) / 100) * trackWarning.Value) - picSpaceR.Width
		With picSpaceY
			.Image = My.Resources.fs_c_y
			.Location = New Point((picSpaceR.Left + picSpaceR.Width), (picDTBackground.Top + 1))
			.Size = New Size(iWidth, (picDTBackground.Height - 2))
			.BringToFront()
		End With

		lblValue_Critical.Text = trackCritical.Value & "%"
		lblValue_Warning.Text = trackWarning.Value & "%"
	End Sub

	Private Sub trackCritical_Scroll(sender As Object, e As System.EventArgs) Handles trackCritical.Scroll
		If (trackCritical.Value >= (trackWarning.Value - 1)) Then trackCritical.Value = (trackWarning.Value - 1)
		If (trackCritical.Value = 0) Then trackCritical.Value = 1
		Call displayGraphic()
	End Sub

	Private Sub trackWarning_Scroll(sender As System.Object, e As System.EventArgs) Handles trackWarning.Scroll
		If (trackWarning.Value <= (trackCritical.Value + 1)) Then trackWarning.Value = (trackCritical.Value + 1)
		If (trackWarning.Value = 100) Then trackWarning.Value = 99
		Call displayGraphic()
	End Sub

	Private Sub cmdReset_Click(sender As System.Object, e As System.EventArgs) Handles cmdReset.Click
		trackWarning.Value = 20
		trackCritical.Value = 10
		Call trackWarning_Scroll(sender, e)
		Call trackCritical_Scroll(sender, e)
		cmoScanResult.SelectedIndex = 1
	End Sub

	Private Sub cmdOK_Click(sender As System.Object, e As System.EventArgs) Handles cmdOK.Click
		' Settings are saved in calling procedure ('frmMain.onClick_addResource_Threshold' or 'frmMain.onClick_editProperties')
		frmMain.m_AddFreeSpace = trackCritical.Value & "|" & trackWarning.Value & "|" & cmoScanResult.SelectedItem.DisplayText.Trim
		lResourceConflicts = Nothing
		Me.Close()
		Me.Dispose()
	End Sub

	Private Sub lnkHelp_LinkClicked(sender As System.Object, e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lnkHelp.LinkClicked
		frmHelp.sSelectPageByID = "help0010"
		frmHelp.ShowDialog(Me)
	End Sub
End Class