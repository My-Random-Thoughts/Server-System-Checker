Option Explicit On

Public Class frmAddFileCheck
	Public lEditValues As ListViewItem ' Are we EDITING an existing item.?
	Private Const NVD As String = "No Version Details"
	Private Const FNF As String = "File Not Found Locally"

	Private Sub frmAddFileExistScan_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
		For Each c As Control In Controls
			c.Font = frmMain.sysFont
		Next
		lblTitle.Font = frmMain.sysFontTitle
		lnkHelp.Font = frmMain.sysFontHelp
		cmdBrowse.Font = frmMain.sysFontHelp

		lnkHelp.LinkBehavior = LinkBehavior.HoverUnderline
		picIcon.Image = My.Resources._48___File.ToBitmap
		If (lEditValues IsNot Nothing) Then
			lblTitle.Text = "Edit Existing" & lblTitle.Text.Substring(7)
			Me.Text = " " & lblTitle.Text
		End If

		' Start image disabled...
		picCopyVersion.Image = GreyScaleImage(My.Resources._16___Copy.ToBitmap, True)
		picCopyDate.Image = GreyScaleImage(My.Resources._16___Copy.ToBitmap, True)

		Dim cItem As ctrlComboBox_Icons.IconComboItem
		With cmoDT_BA
			.ItemHeight = 19
			With .Items
				.Clear()
				cItem = New ctrlComboBox_Icons.IconComboItem("On Or Before")
				cItem.ItemImage = CType(My.Resources.ResourceManager.GetObject("_16___Calendar___OnOrBefore"), Drawing.Icon)
				.Add(cItem)
				cItem = Nothing

				cItem = New ctrlComboBox_Icons.IconComboItem("On Or After")
				cItem.ItemImage = CType(My.Resources.ResourceManager.GetObject("_16___Calendar___OnOrAfter"), Drawing.Icon)
				.Add(cItem)
				cItem = Nothing
			End With
			.SelectedIndex = 0
		End With

		With cmoDT_ML
			.ItemHeight = 19
			With .Items
				.Clear()
				cItem = New ctrlComboBox_Icons.IconComboItem("Exactly")
				cItem.ItemImage = CType(My.Resources.ResourceManager.GetObject("_16___Calendar___Exactly"), Drawing.Icon)
				.Add(cItem)
				cItem = Nothing

				cItem = New ctrlComboBox_Icons.IconComboItem("Equal or less than")
				cItem.ItemImage = CType(My.Resources.ResourceManager.GetObject("_16___Calendar___OnOrBefore"), Drawing.Icon)
				.Add(cItem)
				cItem = Nothing

				cItem = New ctrlComboBox_Icons.IconComboItem("Equal or greater than")
				cItem.ItemImage = CType(My.Resources.ResourceManager.GetObject("_16___Calendar___OnOrAfter"), Drawing.Icon)
				.Add(cItem)
				cItem = Nothing
			End With
			.SelectedIndex = 0
		End With

		cmoDT_ML.Size = cmoDT_BA.Size
		cmoDT_ML.Location = cmoDT_BA.Location

		dtDate.Value = DateTime.Now
		Call optDateTime_CheckedChanged(sender, e)
		Call optVersion_CheckedChanged(sender, e)

		lblSettingsLabel.Text = vbNullString
		lblCurrentVersion.Text = NVD
		lblCurrentDate.Text = "--/--/----"
		lblCurrentDate.ForeColor = getSubItemColour("Disabled")
		lblCurrentVersion.ForeColor = getSubItemColour("Disabled")

		' Set Defaults...
		txtSelectedFile.Text = vbNullString
		txtVersion.Text = vbNullString
		optExists.Checked = True
		optNotExists.Checked = False
		optDateTime.Checked = False
		optVersion.Checked = False
		cmdOK.Enabled = False

		' Default tooltips
		Dim tt As New ToolTip
		tt.SetToolTip(picCopyDate, picCopyDate.Tag.ToString)
		tt.SetToolTip(picCopyVersion, picCopyVersion.Tag.ToString)

		If (lEditValues IsNot Nothing) Then
			txtSelectedFile.Text = lEditValues.Text
			Call getVersion(lEditValues.Text)

			Try
				Dim sItems() As String = Split(lEditValues.SubItems(2).Text, "|")
				If (sItems(0) = "Not Exists") Then optNotExists.Checked = True

				If (sItems(1).StartsWith("Date:")) Then
					optDateTime.Checked = True
					sItems(1) = sItems(1).Substring(6)
					dtDate.Value = CDate(Split(sItems(1), ":")(1))
					cmoDT_BA.SelectedIndex = CInt(IIf(Split(sItems(1), ":")(0).ToLower = "before", 0, 1))

				ElseIf (sItems(1).StartsWith("Version:")) Then
					optVersion.Checked = True
					sItems(1) = sItems(1).Substring(9)
					txtVersion.Text = Split(sItems(1), ":")(1)
					Select Case Split(sItems(1), ":")(0)
						Case "LessThan" : cmoDT_ML.SelectedIndex = 1
						Case "GreaterThan" : cmoDT_ML.SelectedIndex = 2
						Case Else : cmoDT_ML.SelectedIndex = 0
					End Select
				End If
			Catch
			End Try
		End If

		Call enableControls(False)

		If (bReadOnlyMode = True) Then
			cmdOK.Enabled = False
			cmdBrowse.Enabled = False
		End If

		Me.Visible = True
		Application.DoEvents()
		txtSelectedFile.Focus()
	End Sub

	Private Sub cmdCancel_Click(sender As System.Object, e As System.EventArgs) Handles cmdCancel.Click
		frmMain.m_AddFileScan = Nothing
		Me.Close()
		Me.Dispose()
	End Sub

	Private Sub cmdOK_Click(sender As System.Object, e As System.EventArgs) Handles cmdOK.Click
		txtSelectedFile.Text = txtSelectedFile.Text.Trim
		If (txtSelectedFile.Text = vbNullString) Then Exit Sub
		If (txtSelectedFile.Text.Contains("*") = True) Then Exit Sub
		If (txtSelectedFile.Text.Contains("?") = True) Then Exit Sub
		If ((optVersion.Checked = True) AndAlso (txtVersion.Text = vbNullString)) Then Exit Sub

		Dim sResult As String = txtSelectedFile.Text & "|" & IIf(optNotExists.Checked, "Not ", "").ToString & "Exists|"
		If (optDateTime.Checked = True) Then sResult = sResult & "Date: " & IIf(cmoDT_BA.SelectedIndex = 0, "Before", "After").ToString & ":" & dtDate.Text
		If (optVersion.Checked = True) Then
			Dim sText As String
			Select Case cmoDT_ML.SelectedIndex
				Case 1 : sText = "LessThan"
				Case 2 : sText = "GreaterThan"
				Case Else : sText = "Exactly"
			End Select
			sResult = sResult & "Version: " & sText & ":" & txtVersion.Text
		End If

		' RESOURCE DUPLICATE CHECK...
		Dim dResult As DialogResult = resourceDuplicationAlert("File Check", lEditValues, ({sResult}.ToList))
		If (dResult <> Windows.Forms.DialogResult.None) Then Exit Sub

		' Settings are saved in calling procedure ('frmMain.onClick_addResource_FileCheck' or 'frmMain.onClick_editProperties')
		lResourceConflicts = Nothing
		frmMain.m_AddFileScan = sResult
		Me.Close()
		Me.Dispose()
	End Sub

	Private Sub optExists_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles optExists.CheckedChanged
		lblSettingsLabel.Text = vbNullString
		Call enableOKButton()
	End Sub

	Private Sub optNotExists_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles optNotExists.CheckedChanged
		lblSettingsLabel.Text = vbNullString
		Call enableOKButton()
	End Sub

	Private Sub optDateTime_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles optDateTime.CheckedChanged
		cmoDT_BA.Visible = optDateTime.Checked
		dtDate.Visible = optDateTime.Checked
		picCopyDate.Image = My.Resources._16___Copy.ToBitmap
		picCopyDate.Cursor = CType(IIf(optDateTime.Checked, Cursors.Hand, Cursors.Default), Cursor)
		picCopyDate.Visible = optDateTime.Checked
		lblSettingsLabel.Text = "Date Check :"
		Call enableOKButton()
	End Sub

	Private Sub optVersion_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles optVersion.CheckedChanged
		cmoDT_ML.Visible = optVersion.Checked
		txtVersion.Visible = optVersion.Checked
		picCopyVersion.Image = My.Resources._16___Copy.ToBitmap
		picCopyVersion.Cursor = CType(IIf(optVersion.Checked, Cursors.Hand, Cursors.Default), Cursor)
		picCopyVersion.Visible = optVersion.Checked
		lblSettingsLabel.Text = "Version Check :"
		Call enableOKButton()
	End Sub

	Private Sub cmdBrowse_Click(sender As System.Object, e As System.EventArgs) Handles cmdBrowse.Click
		Using dlgOpenFile As New OpenFileDialog
			With dlgOpenFile
				.CheckFileExists = False
				.CheckPathExists = False
				.Filter = "All Files (*.*)|*.*"
				.Multiselect = False
				.CheckFileExists = True
				.CheckPathExists = True
				.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyComputer)
				.SupportMultiDottedExtensions = True

				Dim iResult As DialogResult = .ShowDialog
				If (iResult = Windows.Forms.DialogResult.OK) Then
					txtSelectedFile.Text = .FileName.ToLower
					Dim sEXT As String = txtSelectedFile.Text.ToUpper.Substring(txtSelectedFile.Text.LastIndexOf(".")).ToUpper
					Call getVersion(txtSelectedFile.Text)
				End If
			End With
		End Using
		Call enableOKButton()
	End Sub

	Private Sub getVersion(ByVal sFileName As String)
		lblCurrentVersion.Text = NVD
		lblCurrentDate.Text = "--/--/----"
		lblCurrentDate.ForeColor = getSubItemColour("Disabled")
		lblCurrentVersion.ForeColor = getSubItemColour("Disabled")

		If (txtSelectedFile.Text.StartsWith("\\") = True) Then
			MessageBox.Show(Me, "UNC paths are not permitted", APN, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Exit Sub
		End If

		If (System.IO.File.Exists(sFileName) = False) Then
			lblCurrentDate.Text = FNF
			lblCurrentDate.ForeColor = getSubItemColour("Error")
			lblCurrentVersion.Text = vbNullString
		Else
			Try
				Dim sVer As String = Split(FileVersionInfo.GetVersionInfo(sFileName).FileVersion, " ")(0)
				If (sVer <> vbNullString) Then lblCurrentVersion.Text = "v" & sVer
				lblCurrentDate.Text = System.IO.File.GetLastWriteTimeUtc(sFileName).ToShortDateString
			Catch ex As Exception
			End Try
		End If
	End Sub

	Private Sub picCopyVersion_Click(sender As System.Object, e As System.EventArgs) Handles picCopyVersion.Click
		txtVersion.Text = vbNullString
		If ((lblCurrentVersion.Text = vbNullString) Or (lblCurrentVersion.Text = FNF)) Then Return
		txtVersion.Text = lblCurrentVersion.Text.Substring(1)
		Call enableOKButton()
	End Sub

	Private Sub picCopyDate_Click(sender As System.Object, e As System.EventArgs) Handles picCopyDate.Click
		If (lblCurrentDate.Text = vbNullString) Then Return
		dtDate.Value = CDate(lblCurrentDate.Text)
	End Sub

	Private Sub lnkHelp_LinkClicked(sender As System.Object, e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lnkHelp.LinkClicked
		frmHelp.sSelectPageByID = "help0026"
		frmHelp.ShowDialog(Me)
	End Sub

	Private Sub txtSelectedFile_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles txtSelectedFile.KeyDown
		If (e.KeyCode = Keys.Enter) Then
			e.Handled = True
			optExists.Focus()
		End If
	End Sub

	Private Sub txtSelectedFile_LostFocus(sender As Object, e As System.EventArgs) Handles txtSelectedFile.LostFocus
		txtSelectedFile.Text = txtSelectedFile.Text.Trim
		If txtSelectedFile.Text = vbNullString Then Exit Sub
		Call getVersion(txtSelectedFile.Text)
		Call enableOKButton()
	End Sub

	Private Sub txtSelectedFile_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtSelectedFile.TextChanged
		lblCurrentVersion.Text = vbNullString
		lblCurrentDate.Text = vbNullString
	End Sub

	Private Sub enableOKButton()
		cmdOK.Enabled = txtSelectedFile.TextLength > 0
		Call enableControls(cmdOK.Enabled)
		If (optVersion.Checked = True) Then cmdOK.Enabled = (cmdOK.Enabled And txtVersion.TextLength > 0)
		If (bReadOnlyMode = True) Then cmdOK.Enabled = False
	End Sub

	Private Sub enableControls(ByVal bEnable As Boolean)
		optExists.Enabled = bEnable
		optNotExists.Enabled = bEnable
		optDateTime.Enabled = bEnable
		optVersion.Enabled = bEnable

		If (lblCurrentVersion.Text = NVD) Then
			If (optVersion.Checked = True) Then optExists.Checked = True
			optVersion.Enabled = False
		End If
	End Sub

	Private Sub txtVersion_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtVersion.TextChanged
		Call enableOKButton()
	End Sub
End Class