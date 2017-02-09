Imports System.Xml

Public Class frmMoveCopyServers
	Private img16 As New ImageList
	Private imgSPACING As New ImageList
	Public sGroupGUID As String
	Public sSelectedServer As String

	Private Sub frmMultiMove_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
		For Each c As Control In Controls
			c.Font = frmMain.sysFont
		Next
		lblTitle.Font = frmMain.sysFontTitle
		lnkHelp.Font = frmMain.sysFontHelp

		lnkHelp.LinkBehavior = LinkBehavior.HoverUnderline
		picIcon.Image = My.Resources._48___Copy.ToBitmap

		With img16
			.ImageSize = New Size(16, 16)
			.ColorDepth = ColorDepth.Depth32Bit
			With .Images
				.Clear()
				.Add("_16___CheckBox___Off", My.Resources._16___Blank)							' 0
				.Add("_16___CheckBox___On", My.Resources._16___Blank)							' 1
				.Add("_16___Server", My.Resources._16___Server)									' 2
				.Add("_16___Server_With_Properties", My.Resources._16___Server_With_Properties)	' 3
				.Add("_16___Blank", My.Resources._16___Blank)									' 4

				Dim sCol As String
				For i As Integer = 0 To iIconColourCount - 1
					sCol = getGroupColour(i, True)
					.Add("_16___Group___" & sCol, CType(My.Resources.ResourceManager.GetObject("_16___Group___" & sCol), System.Drawing.Icon))
				Next
			End With
		End With

		With imgSPACING
			.ImageSize = New Size(iListViewIconWidth, iListViewLineHeight)
			.ColorDepth = ColorDepth.Depth32Bit
			For i As Integer = 0 To img16.Images.Count - 1
				.Images.Add(img16.Images.Keys(i), ResizeImage(img16.Images(i), .ImageSize, True))
			Next
		End With

		With lstTreeView
			.Clear()
			.FullRowSelect = True
			.MultiSelect = False
			.HeaderStyle = ColumnHeaderStyle.None
			.SmallImageList = imgSPACING
			.View = View.Details
			.Visible = True
			With .Columns
				.Add("A", lstTreeView.Width - 32 - SystemInformation.VerticalScrollBarWidth - 4)
				.Add("B", 32)
			End With
		End With

		With lstDestination
			.Clear()
			.FullRowSelect = True
			.HeaderStyle = ColumnHeaderStyle.None
			.MultiSelect = False
			.Size = lstTreeView.Size
			.SmallImageList = imgSPACING
			.View = View.Details
			.Visible = False
			With .Columns
				.Add("A", lstTreeView.Width - SystemInformation.VerticalScrollBarWidth - 4)
				.Add("B", 0)
			End With
		End With

		lstHidden.Items.Clear()

		If (bUseVisualStyles = True) Then
			Call SetWindowTheme(lstTreeView.Handle, "Explorer", Nothing)
			Call SetWindowTheme(lstDestination.Handle, "Explorer", Nothing)
		End If

		cmdManage.Text = "Next  >"
		cmdCancel.Text = "Cancel"
		lblSubTitle.Text = "Select the servers you want to manage"
		cmdManage.Enabled = False
		chkCopyNotMove.Visible = False
		chkCopyNotMove.Checked = False

		Me.Visible = True
		Application.DoEvents()
		Call buildTreeInListView()
		lstTreeView.Focus()
	End Sub

	Private Sub buildTreeInListView()
		Dim lItem As ListViewItem
		Dim cItem As ListViewItem
		Dim iIndent As Integer
		Dim lFound() As ListViewItem

		' First create the group structure...
		lstTreeView.BeginUpdate()
		lstDestination.BeginUpdate()
		Dim xGroupList As XmlNodeList = xmlDoc.SelectNodes("descendant::group")
		For Each gItem As XmlNode In xGroupList
			If (gItem.ParentNode.Name = "root") Then
				iIndent = 0
			Else
				lFound = lstTreeView.Items.Find(gItem.ParentNode.Attributes.ItemOf("guid").Value, False)
				If ((lFound Is Nothing) Or (lFound.Count = 0)) Then iIndent = 0 Else iIndent = lFound(0).IndentCount + 1
			End If

			lItem = New ListViewItem(gItem.Attributes.ItemOf("name").Value)
			lItem.ImageKey = "_16___Group___" & getGroupColour(CInt(gItem.Attributes.ItemOf("colr").Value), False)
			lItem.Name = gItem.Attributes.ItemOf("guid").Value
			lItem.IndentCount = iIndent
			lItem.SubItems.Add("ICON:4")
			lstTreeView.Items.Add(lItem)

			cItem = CType(lItem.Clone, ListViewItem)
			cItem.Name = lItem.Name
			lstDestination.Items.Add(cItem)

			lItem = Nothing
			cItem = Nothing
		Next

		' Next add the servers to the groups...
		For Each lvItem As ListViewItem In lstTreeView.Items
			Dim xServerList As List(Of XmlNode) = xml_getServerList_FromGroup(lvItem.Name)
			If (xServerList IsNot Nothing) Then
				xServerList.Reverse()
				For Each sItem As XmlNode In xServerList
					If (sItem.ParentNode.Attributes.ItemOf("guid").Value = lvItem.Name) Then
						lItem = New ListViewItem(sItem.Attributes.ItemOf("name").Value.ToLower)
						If m_UppercaseServerNames = True Then lItem.Text = lItem.Text.ToUpper()

						lItem.ImageKey = "_16___Server"
						If (xml_LoadServerData(sItem.Attributes.ItemOf("guid").Value) IsNot Nothing) Then lItem.ImageKey = "_16___Server_With_Properties"

						lItem.Name = sItem.Attributes.ItemOf("guid").Value
						lItem.Tag = lvItem.Name	' PARENT GUID
						lItem.IndentCount = lvItem.IndentCount + 1
						lItem.SubItems.Add("ICON:0")

						lstTreeView.Items.Insert(lvItem.Index + 1, lItem)
					End If
				Next
			End If
		Next
		lstTreeView.EndUpdate()
		lstDestination.EndUpdate()
		Application.DoEvents()

		lstTreeView.Items(lstTreeView.Items.IndexOfKey(sSelectedServer)).Selected = True
		Try
			For i As Integer = 1 To 4
				lstTreeView.Items(lstTreeView.Items.IndexOfKey(sSelectedServer) + i).EnsureVisible()
			Next
		Catch
		End Try
	End Sub

	Private Sub lstTreeView_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles lstTreeView.KeyDown
		If (e.KeyCode = Keys.Space) Then
			Dim lSubItem As ListViewItem.ListViewSubItem = lstTreeView.SelectedItems(0).SubItems(1)
			Call changeTickBox(lSubItem)
		End If
	End Sub

	Private Sub lstTreeView_MouseClick(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles lstTreeView.MouseClick
		Dim lLVItem As ListViewItem = lstTreeView.GetItemAt(e.X, e.Y)
		If (lLVItem Is Nothing) Then Exit Sub
		Dim lSubItem As ListViewItem.ListViewSubItem = lLVItem.GetSubItemAt(e.X, e.Y)
		If (lLVItem.SubItems.IndexOf(lSubItem) = 0) Then Exit Sub
		Call changeTickBox(lSubItem)
	End Sub

	Private Sub lstTreeView_MouseDoubleClick(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles lstTreeView.MouseDoubleClick
		Dim lLVItem As ListViewItem = lstTreeView.GetItemAt(e.X, e.Y)
		If (lLVItem Is Nothing) Then Exit Sub
		Dim lSubItem As ListViewItem.ListViewSubItem = lLVItem.GetSubItemAt(e.X, e.Y)
		If (lLVItem.SubItems.IndexOf(lSubItem) = 1) Then Exit Sub
		Call changeTickBox(lLVItem.SubItems(1))
	End Sub

	Private Sub changeTickBox(ByVal lViewItem As ListViewItem.ListViewSubItem)
		Select Case lViewItem.Text
			Case "ICON:0" : lViewItem.Text = "ICON:1"
			Case "ICON:1" : lViewItem.Text = "ICON:0"
			Case Else : Return
		End Select

		cmdManage.Enabled = False
		For Each lItem As ListViewItem In lstTreeView.Items
			If (lItem.SubItems(1).Text = "ICON:1") Then
				cmdManage.Enabled = True
				Exit For
			End If
		Next
	End Sub

	Private Sub cmdManage_Click(sender As System.Object, e As System.EventArgs) Handles cmdManage.Click
		If (cmdManage.Text = "Next  >") Then

			lstTreeView.Visible = False
			lstDestination.Visible = True
			lblSubTitle.Text = "Select the destination group for these servers"
			cmdManage.Text = "Move"
			cmdManage.Enabled = False
			cmdCancel.Text = "<  Back"
			chkCopyNotMove.Visible = True
			picIcon.Focus()
			Return

		Else

			lstHidden.Items.Clear()
			For Each lItem As ListViewItem In lstTreeView.Items
				If (lItem.SubItems(1).Text = "ICON:1") Then
					If (chkCopyNotMove.Checked = True) Then
						' Copy servers...
						If (xml_setAddServer(lItem.Text, Guid.NewGuid.ToString, lstDestination.SelectedItems(0).Name) = False) Then lstHidden.Items.Add(lItem)
					Else
						' Change server location...
						If (xml_setNewParent(lItem.Name, lItem.Tag.ToString, lstDestination.SelectedItems(0).Name) = False) Then lstHidden.Items.Add(lItem)
					End If
				End If
			Next
			Call xml_SaveXmlDocument(sConfigFile)

			If (lstHidden.Items.Count > 0) Then
				Dim sMsg As String = "The following servers were not " & IIf(chkCopyNotMove.Checked, "copied", "moved").ToString & "..." & vbCrLf & vbCrLf
				For Each lItem As ListViewItem In lstHidden.Items
					sMsg = sMsg & "    " & lItem.Text & vbCrLf
				Next
				MessageBox.Show(Me, sMsg, APN, MessageBoxButtons.OK, MessageBoxIcon.Error)
			Else
				MessageBox.Show(Me, "All servers " & IIf(chkCopyNotMove.Checked, "copied", "moved").ToString & " successfully", _
				 APN, MessageBoxButtons.OK, MessageBoxIcon.Information)
			End If
		End If

		img16.Dispose()
		imgSPACING.Dispose()
		frmMain.bMultiSelectDone = True
		Me.Close()
		Me.Dispose()
	End Sub

	Private Sub cmdCancel_Click(sender As System.Object, e As System.EventArgs) Handles cmdCancel.Click
		If (cmdCancel.Text = "Cancel") Then
			img16.Dispose()
			imgSPACING.Dispose()
			frmMain.bMultiSelectDone = False
			Me.Close()
			Me.Dispose()
		Else
			lstTreeView.Visible = True
			lstDestination.Visible = False
			cmdManage.Text = "Next  >"
			cmdCancel.Text = "Cancel"
			lblSubTitle.Text = "Select the servers you want to manage"
			chkCopyNotMove.Visible = False
			cmdManage.Enabled = True
		End If
	End Sub

	Private Sub chkCopyNotMove_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkCopyNotMove.CheckedChanged
		If (cmdManage.Text <> "Next  >") Then cmdManage.Text = IIf(chkCopyNotMove.Checked, "Copy", "Move").ToString
	End Sub

	Private Sub lstDestination_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles lstDestination.SelectedIndexChanged
		' Check if destination location is valid

		cmdManage.Enabled = False
		If lstDestination.SelectedItems.Count = 0 Then Return

		Dim targetNode As TreeNode = (frmMain.tvwServerList.Nodes.Find(lstDestination.SelectedItems(0).Name, True)(0))
		If (targetNode.GetNodeCount(False) > 0) Then
			If (targetNode.FirstNode.ImageKey.StartsWith("_16___Group___") = True) Then Exit Sub
		End If

		cmdManage.Enabled = True
	End Sub

	Private Sub lnkHelp_LinkClicked(sender As System.Object, e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lnkHelp.LinkClicked
		frmHelp.sSelectPageByID = "help0034"
		frmHelp.ShowDialog(Me)
	End Sub
End Class