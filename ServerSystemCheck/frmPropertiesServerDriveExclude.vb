Imports System.Management
Imports System.Xml
Imports Microsoft.Win32

Public Class frmPropertiesServerDriveExclude

	Public sServerName As String
	Public sServerGUID As String
	Private iDriveThreshold_Fail As Integer = 10
	Private iDriveThreshold_Warn As Integer = 20
	Private lvwCS_D As ListViewColumnSorter			' Free Space
	Public dFreeSpaceSize As List(Of ManagementObject)

	Private Sub frmPropertiesServerDriveExclude_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
		For Each c As Control In Controls
			c.Font = frmMain.sysFont
		Next
		lblTitle.Font = frmMain.sysFontTitle
		lnkHelp.Font = frmMain.sysFontHelp

		lnkHelp.LinkBehavior = LinkBehavior.HoverUnderline
		picIcon.Image = My.Resources._48___Drive.ToBitmap
		lvwCS_D = New ListViewColumnSorter()

		Dim img16 As New ImageList
		With img16
			.ImageSize = New Size(16, 16)
			.ColorDepth = ColorDepth.Depth32Bit
			With .Images
				.Clear()
				.Add("_16___Scan___Failed", My.Resources._16___Scan___Failed)
				.Add("_16___Scan___Scanning", My.Resources._16___Scan___Scanning)
				.Add("_16___Resource___Drive", My.Resources._16___Resource___Drive)
				.Add("_16___Resource___Drive___System", My.Resources._16___Resource___Drive___System)
				.Add("_16___Resource___Drive___Disabled", My.Resources._16___Resource___Drive___Disabled)
				.Add("_16___Blank", My.Resources._16___Blank)
			End With
		End With

		With lstDrives
			.Clear()
			.FullRowSelect = True
			.SmallImageList = img16
			.MultiSelect = False
			.View = View.Details
			.ListViewItemSorter = lvwCS_D
			.ShowGroups = True
			.HeaderStyle = ColumnHeaderStyle.None
			With .Groups
				.Clear()
				.Add(New ListViewGroup("Scanned", "Scanned Drives"))
				.Add(New ListViewGroup("Excluded", "Excluded Drives"))
				.Add(New ListViewGroup("WAIT", "Getting Drive Information"))
			End With
			With .Columns
				.Clear()
				.Add("label", "Drive Label", 175)
				.Add("free", "Free Space Percentage", lstDrives.Width - 175 - SystemInformation.VerticalScrollBarWidth - 4)
			End With
			lvwCS_D.SortColumn = .Columns("label").Index
			lvwCS_D.Order = SortOrder.Ascending
		End With

		If (bUseVisualStyles = True) Then
			Call SetWindowTheme(lstDrives.Handle, "Explorer", Nothing)
		End If

		cmdOK.Enabled = Not bReadOnlyMode
		lstDrives.Enabled = Not bReadOnlyMode

		Me.Visible = True
		Application.DoEvents()
		Me.Cursor = Cursors.WaitCursor

		lstDrives.Items.Add("WAIT", "Please wait...", "_16___Scan___Scanning")
		lstDrives.Items("WAIT").Group = lstDrives.Groups("WAIT")
		Application.DoEvents()

		dFreeSpaceSize = scanServer_DriveSpace()
		Call loadResults_DriveSpace()

		Dim sDrives As String = xml_GetExcludedDrives(sServerGUID)
		If (sDrives IsNot Nothing) Then
			For Each iDrive As String In sDrives.Split(","c)
				For Each lItem As ListViewItem In lstDrives.Items
					If (lItem.Text.Substring(1, 2) = iDrive) Then Call changeGroups(lItem)
				Next
			Next
		End If

		Me.Cursor = Cursors.Default
	End Sub

	Private Sub cmdCancel_Click(sender As System.Object, e As System.EventArgs) Handles cmdCancel.Click
		Me.Close()
		Me.Dispose()
	End Sub

	Private Function scanServer_DriveSpace() As List(Of ManagementObject)
		Dim sOKSoFar As New List(Of ManagementObject)
		Dim oResult As ManagementObjectCollection
		Dim sResult As String
		Dim rKey As RegistryKey

		Try
			' First, get the remote registry value for the system drive letter (might not be C:)
			rKey = RegistryKey.OpenRemoteBaseKey(RegistryHive.LocalMachine, sServerName.Trim).OpenSubKey("SOFTWARE\Microsoft\Windows NT\CurrentVersion", False)
			If (rKey Is Nothing) Then Return Nothing
			sResult = rKey.GetValue("SystemRoot", vbNullString, RegistryValueOptions.None).ToString
			If (sResult Is vbNullString) Then Return Nothing

			oResult = wmiConnect("\\" & sServerName.ToUpper.Trim & "\root\cimv2", "SELECT * FROM Win32_LogicalDisk WHERE DriveType='3'", 5)
		Catch ex As Exception
			Return Nothing
		End Try

		' Make sure system drive is first in list, then add the rest...
		For Each mObj As ManagementObject In oResult
			If (mObj("DeviceID").ToString = sResult.Substring(0, 2)) Then
				mObj("Name") = "THIS_ONE"
				sOKSoFar.Insert(0, mObj)
			Else
				sOKSoFar.Add(mObj)
			End If
		Next

		oResult.Dispose()
		Return sOKSoFar
	End Function

	Private Sub loadResults_DriveSpace()
		If (dFreeSpaceSize Is Nothing) Then
			lstDrives.Items("WAIT").Text = "Error contacting server"
			lstDrives.Items("WAIT").ImageKey = "_16___Scan___Failed"
			Exit Sub
		End If

		Dim lItem As ListViewItem
		Dim iPercent As Integer

		With lstDrives
			.BeginUpdate()
			For Each mObj As ManagementObject In dFreeSpaceSize
				Try
					' This is a test to see if "mObj('VolumeName')" is nothing (possible cluster resource)
					Console.WriteLine("IGNORE ME: VolumeName: " & mObj("VolumeName").ToString)

					Dim sVolumeName As String = mObj("VolumeName").ToString
					If (sVolumeName = vbNullString) Then sVolumeName = "Local Disk"
					lItem = New ListViewItem("(" & mObj("DeviceID").ToString & ") " & sVolumeName)

					iPercent = CInt((100 / CLng(mObj("Size"))) * CLng(mObj("FreeSpace")))
					lItem.SubItems.Add(getSizeAndUnits(CLng(mObj("FreeSpace"))) & " free of " & getSizeAndUnits(CLng(mObj("Size"))) & "  (" & iPercent & "% free)")
					lItem.Group = .Groups("Scanned")
					lItem.Name = mObj("Name").ToString
					lItem.ImageKey = "_16___Resource___Drive"
					If (mObj("Name").ToString = "THIS_ONE") Then lItem.ImageKey = "_16___Resource___Drive___System"

					Select Case iPercent
						Case Is < 11 : lItem.SubItems(1).ForeColor = getSubItemColour("Error")
						Case Is < 21 : lItem.SubItems(1).ForeColor = getSubItemColour("Warning")
						Case Else : lItem.SubItems(1).ForeColor = getSubItemColour("OK")
					End Select

					.Items.Add(lItem)

				Catch ex As Exception
					' Add 'empty' drive for possible cluster resource drive
					lItem = New ListViewItem("(" & mObj("DeviceID").ToString & ") Unknown Drive")
					lItem.SubItems.Add("Unknown")
					lItem.Group = .Groups("Scanned")
					lItem.ImageKey = "_16___Resource___Drive"
					lItem.SubItems(1).ForeColor = getSubItemColour("Unknown")
					.Items.Add(lItem)
				End Try
			Next

			' Make sure the "Excluded" group is always visible
			lItem = New ListViewItem("None")
			lItem.Group = lstDrives.Groups("Excluded")
			lItem.Name = "Blank"
			lItem.SubItems.Add(vbNullString)
			lstDrives.Items.Add(lItem)

			.EndUpdate()
			.Sort()
		End With

		lstDrives.Items("WAIT").Remove()
		lstDrives.Groups.Remove(lstDrives.Groups("WAIT"))
	End Sub

	Private Sub lstDrives_MouseDoubleClick(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles lstDrives.MouseDoubleClick
		Dim hItem As ListViewItem = lstDrives.HitTest(e.X, e.Y).Item
		If (hItem Is Nothing) Then Return
		If (hItem.Name = "WAIT") Then Return
		If (hItem.Name = "Blank") Then Return
		Call changeGroups(hItem)
	End Sub

	Private Sub changeGroups(ByVal hItem As ListViewItem)
		If (hItem.Name = "THIS_ONE") Then
			MessageBox.Show(Me, "The system drive can not be selected for exclusion", APN, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Exit Sub
		End If

		If (hItem.Group.Name = "Excluded") Then
			hItem.Group = lstDrives.Groups("Scanned")
			hItem.ImageKey = "_16___Resource___Drive"
		Else
			hItem.Group = lstDrives.Groups("Excluded")
			hItem.ImageKey = "_16___Resource___Drive___Disabled"
		End If

		For Each gGroup As ListViewGroup In lstDrives.Groups
			If (gGroup.Items.Count = 0) Then
				Dim lItem As New ListViewItem("None")
				lItem.Group = gGroup
				lItem.Name = "Blank"
				lItem.SubItems.Add(" ")
				lstDrives.Items.Add(lItem)
			Else
				If (lstDrives.Items.Find("Blank", True).Count > 0) Then	' TRUE
					If (lstDrives.Items("Blank").Group Is gGroup) Then lstDrives.Items("Blank").Remove()
				End If
			End If
		Next

		lstDrives.Sort()
	End Sub

	Private Sub cmdOK_Click(sender As System.Object, e As System.EventArgs) Handles cmdOK.Click
		Dim sDrives As String = vbNullString

		For Each lItem As ListViewItem In lstDrives.Items
			If (lItem.Group Is lstDrives.Groups("Excluded")) Then
				If (lItem.Text <> "None") Then sDrives = sDrives & lItem.Text.Substring(1, 2) & ","
			End If
		Next

		Dim bResult As Boolean = xml_SetExcludedDrives(sServerGUID, sDrives)
		Application.DoEvents()
		If (bResult = True) Then
			Me.Close()
			Me.Dispose()
		Else
			MsgBox("Error Saving Drive Exclusions")
		End If
	End Sub

	Private Sub lnkHelp_LinkClicked(sender As System.Object, e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lnkHelp.LinkClicked
		frmHelp.sSelectPageByID = "help0033"
		frmHelp.ShowDialog(Me)
	End Sub
End Class