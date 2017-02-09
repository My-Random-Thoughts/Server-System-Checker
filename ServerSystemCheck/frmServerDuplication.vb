Public Class frmServerDuplication
	Private imgSPACING As New ImageList
	Public sServerGUIDList() As String

	Private Sub frmPropertiesServerDuplication_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
		For Each c As Control In Controls
			c.Font = frmMain.sysFont
		Next
		lblTitle.Font = frmMain.sysFontTitle

		lnkHelp.LinkBehavior = LinkBehavior.HoverUnderline
		picIcon.Image = My.Resources._48___Server_Compare.ToBitmap

		With imgSPACING
			.ImageSize = New Size(iListViewIconWidth, iListViewLineHeight)
			.ColorDepth = ColorDepth.Depth32Bit
		End With

		With lstServerList
			.Clear()
			.View = View.Details
			.ShowGroups = True
			.SmallImageList = imgSPACING
			.FullRowSelect = True
			.LabelWrap = False
			.HeaderStyle = ColumnHeaderStyle.Nonclickable

			With .Columns
				.Add("Section", lstServerList.Width - 400 - SystemInformation.VerticalScrollBarWidth - 4)
				.Add("Selected Server", 200, HorizontalAlignment.Center)
				.Add("Existing Duplicate", 200, HorizontalAlignment.Center)
			End With

			With .Groups
				.Add("G", "General")
				.Add("O", "Operating System")
				.Add("M", "Manufacture")
				.Add("H", "Hardware")
			End With

			With .Items
				.Add("Server Name").Group = lstServerList.Groups("G")
				.Add("Operating System").Group = lstServerList.Groups("O")
				.Add("Service Pack").Group = lstServerList.Groups("O")
				.Add("Make").Group = lstServerList.Groups("M")
				.Add("Model").Group = lstServerList.Groups("M")
				.Add("Serial").Group = lstServerList.Groups("M")
				.Add("Processor").Group = lstServerList.Groups("H")
				.Add("Memory").Group = lstServerList.Groups("H")
			End With
		End With

		cmdRemoveLeft.Enabled = Not bReadOnlyMode
		cmdRemoveLeft.Enabled = Not bReadOnlyMode
		cmdRemoveRight.Left = lstServerList.Right - 200 - 2 - SystemInformation.VerticalScrollBarWidth
		cmdRemoveLeft.Left = cmdRemoveRight.Left - 200

		If (bUseVisualStyles = True) Then
			Call SetWindowTheme(lstServerList.Handle, "Explorer", Nothing)
		End If

		Me.Visible = True
		Application.DoEvents()

		Call loadData()
	End Sub

	Private Sub cmdCancel_Click(sender As System.Object, e As System.EventArgs) Handles cmdCancel.Click
		Close()
		Me.Dispose()
	End Sub

	Private Sub loadData()
		For Each sGUID As String In sServerGUIDList
			Dim sData() As String = xml_LoadServerData(sGUID)
			If (sData Is Nothing) Then Array.Resize(sData, 10)

			With lstServerList
				.Items(0).SubItems.Add(xml_getServerName(sGUID)).Name = sGUID
				.Items(1).SubItems.Add(sData(0))
				.Items(2).SubItems.Add(sData(1))
				.Items(3).SubItems.Add(sData(3))
				.Items(4).SubItems.Add(sData(4))
				.Items(5).SubItems.Add(sData(5))

				Dim s7Data As String = sData(7)
				If (sData(7) IsNot Nothing) Then s7Data = sData(7).Split("@"c)(0)
				.Items(6).SubItems.Add(s7Data)

				Dim sRAM() As String = Split(sData(2), "|")
				Dim iTotal As Long = 0
				For Each sStick As String In sRAM
					If (IsNumeric(sStick) = True) Then iTotal = iTotal + CLng(sStick)
				Next
				If (iTotal > 0) Then .Items(7).SubItems.Add(getSizeAndUnits(iTotal)) Else .Items(7).SubItems.Add(sData(2))

				For Each lItem As ListViewItem In .Items
					If (lItem.SubItems(lItem.SubItems.Count - 1).Text = Nothing) Then
						lItem.SubItems(lItem.SubItems.Count - 1).Text = "(no data)"
						lItem.SubItems(lItem.SubItems.Count - 1).ForeColor = getSubItemColour("Disabled")
					End If
				Next
			End With
		Next
	End Sub

	Private Sub cmdRemove_LRClick(sender As System.Object, e As System.EventArgs) Handles cmdRemoveLeft.Click, cmdRemoveRight.Click
		Dim iColumn As Integer = 1
		Dim cmd As Button = CType(sender, Button)
		If (cmd.Name = "cmdRemoveRight") Then iColumn = 2

		Dim sMsg As String = "You are about to remove server '" & lstServerList.Items(0).SubItems(iColumn).Text & "'" & vbCrLf
		sMsg = sMsg & "This operation can not be undone." & vbCrLf
		sMsg = sMsg & vbCrLf
		sMsg = sMsg & "      " & lstServerList.Items(1).SubItems(iColumn).Text & vbCrLf
		sMsg = sMsg & "      " & lstServerList.Items(2).SubItems(iColumn).Text & vbCrLf
		sMsg = sMsg & "      " & lstServerList.Items(3).SubItems(iColumn).Text & vbCrLf
		sMsg = sMsg & "      " & lstServerList.Items(4).SubItems(iColumn).Text & vbCrLf
		sMsg = sMsg & "      " & lstServerList.Items(5).SubItems(iColumn).Text & vbCrLf
		sMsg = sMsg & "      " & lstServerList.Items(6).SubItems(iColumn).Text & vbCrLf
		sMsg = sMsg & "      " & lstServerList.Items(7).SubItems(iColumn).Text & vbCrLf
		sMsg = sMsg & vbCrLf
		sMsg = sMsg & "Are you sure you want to remove this server.?"

		Dim dResult As DialogResult = MessageBox.Show(Me, sMsg, APN, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
		If (dResult = Windows.Forms.DialogResult.No) Then Exit Sub

		If (xml_setRemoveServer(lstServerList.Items(0).SubItems(iColumn).Name) = True) Then
			Dim dNode() As TreeNode = frmMain.tvwServerList.Nodes.Find(lstServerList.Items(0).SubItems(iColumn).Name, True)
			If (dNode IsNot Nothing) Then frmMain.tvwServerList.Nodes.Remove(dNode(0))
			MessageBox.Show(Me, "Server removed successfully", APN, MessageBoxButtons.OK, MessageBoxIcon.Information)
			Call cmdCancel_Click(sender, e)
		Else
			MessageBox.Show(Me, "Server removal failed", APN, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
		End If
	End Sub

	Private Sub lstServerList_ColumnWidthChanged(sender As System.Object, e As System.EventArgs) Handles lstServerList.ColumnWidthChanged
		With lstServerList
			If (.Columns.Count <> 3) Then Exit Sub
			If (.Columns(0).Width <> .Width - 400 - SystemInformation.VerticalScrollBarWidth - 4) Then .Columns(0).Width = .Width - 400 - SystemInformation.VerticalScrollBarWidth - 4
			If (.Columns(1).Width <> 200) Then .Columns(1).Width = 200
			If (.Columns(2).Width <> 200) Then .Columns(2).Width = 200
		End With
	End Sub

	Private Sub lstServerList_ColumnWidthChanging(sender As Object, e As System.Windows.Forms.ColumnWidthChangingEventArgs) Handles lstServerList.ColumnWidthChanging
		Select Case e.ColumnIndex
			Case 0 : e.NewWidth = lstServerList.Width - 400 - SystemInformation.VerticalScrollBarWidth - 4
			Case 1 : e.NewWidth = 200
			Case 2 : e.NewWidth = 200
		End Select
		e.Cancel = True
	End Sub

	Private Sub lnkHelp_LinkClicked(sender As System.Object, e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lnkHelp.LinkClicked
		'frmHelp.sSelectPageByID = "help0011"
		frmHelp.ShowDialog(Me)
	End Sub
End Class