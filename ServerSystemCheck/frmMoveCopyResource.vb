Option Explicit On
Imports System.Xml
Imports System.Text

Public Class frmMoveCopyResource
	Public sNode As TreeNode
	Private img16 As New ImageList
	Private imgSPACING As New ImageList

	Private Sub frmResourceMoveCopy_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
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
				.Add("_16___YouAreHere", My.Resources._16___YouAreHere)
				.Add("_16___Scan___Pass", My.Resources._16___Scan___Pass)
				.Add("_16___Scan___Failed", My.Resources._16___Scan___Failed)
				.Add("_16___Eventlog___Warning", My.Resources._16___Eventlog___Warning)

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

		With lstResults
			.Clear()
			.Size = tvwMoveCopy.Size
			.SmallImageList = imgSPACING
			.View = View.Details
			With .Groups
				.Add("F", "Failed")					' _16___Eventlog___Warning
				.Add("D", "Duplicated Resource")	' _16___Scan___Failed
				.Add("S", "Successful")				' _16___Scan___Pass
			End With
			With .Columns
				.Add("A", lstResults.Width - SystemInformation.VerticalScrollBarWidth - 4)
			End With
			.HeaderStyle = ColumnHeaderStyle.None
			.Visible = False
			.MultiSelect = False
		End With

		With tvwMoveCopy
			.ImageList = img16
			.ItemHeight = 19
			.Nodes.Clear()
			.HideSelection = False
			.Visible = True
		End With

		chkCopyNotMove.Checked = False
		chkCopyNotMove.Visible = True
		cmdCancel.Visible = True

		Dim gNode As XmlElement
		Dim xList As XmlNodeList = xmlDoc.SelectNodes("descendant::group")

		If (xList Is Nothing) Then Exit Sub
		For Each gNode In xList
			Dim pParent As String = gNode.ParentNode.Attributes.ItemOf("guid").Value
			If (pParent = "root") Then pParent = Nothing
			Call addGroup(gNode.Attributes.ItemOf("name").Value, gNode.Attributes.ItemOf("guid").Value, CInt(gNode.Attributes.ItemOf("colr").Value), pParent)
		Next

		If (bUseVisualStyles = True) Then
			Call SetWindowTheme(tvwMoveCopy.Handle, "Explorer", Nothing)
			Call SetWindowTheme(lstResults.Handle, "Explorer", Nothing)
		End If

		tvwMoveCopy.Focus()
	End Sub

	Public Sub addGroup(ByVal sGroupName As String, ByVal sGUID As String, ByVal iIconColour As Integer, ByVal sParentGUID As String)
		Dim tvNode As New TreeNode
		Dim tvParent As New TreeNode
		If ((sGroupName = "Lost And Found") AndAlso (iIconColour = 99)) Then Exit Sub

		' New Node Details...
		With tvNode
			.Text = sGroupName
			.Tag = "GROUP"
			.Name = IIf(sGUID = vbNullString, Guid.NewGuid.ToString, sGUID).ToString
			.ImageKey = "_16___Group___" & getGroupColour(iIconColour, False)
			.SelectedImageKey = .ImageKey
		End With

		' Check if folders are too deep, rest to root level...
		If (sParentGUID <> vbNullString) Then
			tvParent = tvwMoveCopy.Nodes.Find(sParentGUID, True)(0)
			If (checkGroupDepth(tvParent.FullPath, tvwMoveCopy.PathSeparator, vbNull) = False) Then
				tvParent = Nothing
				sParentGUID = vbNullString
			End If
		End If

		If (sParentGUID = vbNullString) Then
			' Creating a brand new node...
			If (sGUID = vbNullString) Then
				If (xml_setAddGroup(tvNode.Text, tvNode.Name, iIconColour, Nothing) = True) Then tvwMoveCopy.Nodes.Add(tvNode)
				tvwMoveCopy.LabelEdit = True
				tvNode.BeginEdit()
			Else
				tvwMoveCopy.Nodes.Add(tvNode)
				tvwMoveCopy.Sort()
			End If
		Else
			' Create a sub node...
			tvParent = tvwMoveCopy.Nodes.Find(sParentGUID, True)(0)
			If (sGUID = vbNullString) Then
				If (xml_setAddGroup(tvNode.Text, tvNode.Name, iIconColour, tvParent.Name) = True) Then tvParent.Nodes.Add(tvNode)
				tvwMoveCopy.SelectedNode = tvNode
				tvNode.BeginEdit()
			Else
				tvParent.Nodes.Add(tvNode)
				tvParent.Expand()
				tvwMoveCopy.Sort()
			End If
		End If

		tvNode = Nothing
		tvParent = Nothing
	End Sub

	Private Sub cmdCancel_Click(sender As System.Object, e As System.EventArgs) Handles cmdCancel.Click
		img16.Dispose()
		imgSPACING.Dispose()
		Me.Close()
		Me.Dispose()
	End Sub

	Private Sub cmdOK_Click(sender As System.Object, e As System.EventArgs) Handles cmdOK.Click
		Dim bDoesResourceExist As Boolean
		Dim bSkipDuplicationCheck As Boolean
		Dim iCountS As Integer = 0
		Dim iCountF As Integer = 0

		If (cmdOK.Text = "Close") Then Call cmdCancel_Click(sender, e)
		lstResults.Items.Clear()

		For Each lvItem As ListViewItem In frmMain.lstResources.SelectedItems
			' Check Resource Conflict...
			Dim sDestGUID As String = getGroupNode(tvwMoveCopy.SelectedNode).Name
			Dim sName As String = lvItem.Text
			Dim sType As String = lvItem.SubItems(1).Text
			Dim sChecking As String = lvItem.SubItems(2).Text

			' Check Resource Duplication, but only if moving to different tree.
			' If moving within it's own "FullPath" then don't bother.
			bDoesResourceExist = False
			bSkipDuplicationCheck = False

			Dim gfpSource As String = getFullPath(sNode.Name, False, tvwMoveCopy.PathSeparator)
			If (chkCopyNotMove.Checked = False) Then
				If (InStr(tvwMoveCopy.SelectedNode.FullPath, gfpSource) > 0) Then
					bSkipDuplicationCheck = True
				End If
				If (InStr(gfpSource, tvwMoveCopy.SelectedNode.FullPath) > 0) Then
					bSkipDuplicationCheck = True
				End If
			End If

			Dim lItem As New ListViewItem(lvItem.Text)
			If (bSkipDuplicationCheck = False) Then bDoesResourceExist = xml_checkResourceExists(sDestGUID, sType, sName)
			If (bDoesResourceExist = False) Then
				If (xml_MoveCopyResource(Not chkCopyNotMove.Checked, sNode.Name, sDestGUID, lvItem.Name) = False) Then
					' Error Moving/Copying Resource
					' Message handled via XML_ Function
					iCountF = iCountF + 1

					' Failed Move/Copy
					lItem.Group = lstResults.Groups("F")
					lItem.ImageKey = "_16___Eventlog___Warning"
				Else
					' Successful Move/Copy
					lItem.Group = lstResults.Groups("S")
					lItem.ImageKey = "_16___Scan___Pass"
				End If
			Else
				' Duplicated Resource
				lItem.Group = lstResults.Groups("D")
				lItem.ImageKey = "_16___Scan___Failed"
			End If
			iCountS = iCountS + 1
			lstResults.Items.Add(lItem)
		Next

		If (bUseVisualStyles = True) Then
			lstResults.SetGroupState(ListViewGroupState.Collapsible)
			For Each grp As ListViewGroup In lstResults.Groups
				Call lstResults.SetGroupFooter(grp)
			Next
		End If

		lstResults.Visible = True
		tvwMoveCopy.Visible = False
		chkCopyNotMove.Visible = False

		cmdOK.Text = "Close"
		cmdCancel.Visible = False
	End Sub

	'Private Sub tvwMoveCopy_AfterSelect(sender As System.Object, e As System.Windows.Forms.TreeViewEventArgs) Handles tvwMoveCopy.AfterSelect
	'If (e.Node.ImageKey = "_16___YouAreHere") Then cmdOK.Enabled = False Else cmdOK.Enabled = True
	'End Sub

	Private Sub lnkHelp_LinkClicked(sender As System.Object, e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lnkHelp.LinkClicked
		frmHelp.sSelectPageByID = "help0028"
		frmHelp.ShowDialog(Me)
	End Sub

	Private Sub chkCopyNotMove_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkCopyNotMove.CheckedChanged
		cmdOK.Text = IIf(chkCopyNotMove.Checked, "Copy", "Move").ToString
	End Sub
End Class