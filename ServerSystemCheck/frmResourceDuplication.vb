Option Explicit On
Imports System.Xml

Public Class frmResourceDuplication
	Private img16 As New ImageList
	Private imgSPACING As New ImageList

	Private Sub frmResourceConflict_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
		For Each c As Control In Controls
			c.Font = frmMain.sysFont
		Next
		lblTitle.Font = frmMain.sysFontTitle
		lnkHelp.Font = frmMain.sysFontHelp

		lnkHelp.LinkBehavior = LinkBehavior.HoverUnderline
		picIcon.Image = My.Resources._48___Search.ToBitmap
		picIconOverlay.Image = My.Resources._16___Eventlog___Critical.ToBitmap

		With img16
			.ImageSize = New Size(16, 16)
			.ColorDepth = ColorDepth.Depth32Bit
			With .Images
				.Clear()
				.Add("Error", My.Resources._16___Scan___Failed)
				.Add("_16___Resource___Services", My.Resources._16___Resource___Services)
				.Add("_16___Resource___Hotfix", My.Resources._16___Resource___Hotfix)
				.Add("_16___Resource___Eventlog", My.Resources._16___Resource___Eventlog)
				.Add("_16___Resource___Drive", My.Resources._16___Resource___Drive)
				.Add("_16___Resource___Registry", My.Resources._16___Resource___Registry)
				.Add("_16___Resource___WMIQuery", My.Resources._16___Resource___WMIQuery)
				.Add("_16___YouAreHere", My.Resources._16___YouAreHere)
				Dim sCol As String
				For i As Integer = 0 To iIconColourCount - 1
					sCol = getGroupColour(i, True)
					.Add("_16___Group___" & sCol, CType(My.Resources.ResourceManager.GetObject("_16___Group___" & sCol), Drawing.Icon))
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

		With lstConflict
			.Groups.Clear()
			.Items.Clear()
			.FullRowSelect = True
			.SmallImageList = imgSPACING
			.View = View.Details
			.HeaderStyle = ColumnHeaderStyle.None
			With .Columns
				.Clear()
				.Add("", "Resource Location", lstConflict.Width - 150 - SystemInformation.VerticalScrollBarWidth - 4)
				.Add("", "Details", 150, HorizontalAlignment.Left, -1)
			End With
		End With

		If (bUseVisualStyles = True) Then
			Call SetWindowTheme(lstConflict.Handle, "Explorer", Nothing)
		End If

		Me.Visible = True
		Application.DoEvents()

		Me.Cursor = Cursors.WaitCursor
		Call buildTreeInListView()
		Me.Cursor = Cursors.Default
	End Sub

	Private Sub buildTreeInListView()
		Dim lItem As ListViewItem
		Dim iIndent As Integer
		Dim lFound() As ListViewItem

		lstConflict.BeginUpdate()
		For Each itm As String In lResourceConflicts
			Dim sGroupGUID As String = Split(itm, "|")(0)
			Dim sResourceGUID As String = Split(itm, "|")(1)

			For i As Integer = 0 To 1
				Dim xGroupList As New List(Of XmlNode)
				If (i = 0) Then xGroupList = xml_getGroupList_FromGroup(sGroupGUID, eDirectionalSearch.ToParents)
				If (i = 1) Then xGroupList = xml_getGroupList_FromResource(sResourceGUID)

				For Each gItem As XmlNode In xGroupList
					If (lstConflict.Items.Find(gItem.Attributes.ItemOf("guid").Value, False).Count = 0) Then
						If (gItem.ParentNode.Name = "root") Then
							iIndent = 0
						Else
							lFound = lstConflict.Items.Find(gItem.ParentNode.Attributes.ItemOf("guid").Value, False)
							If ((lFound Is Nothing) Or (lFound.Count = 0)) Then iIndent = 0 Else iIndent = lFound(0).IndentCount + 1
						End If

						lItem = New ListViewItem(gItem.Attributes.ItemOf("name").Value)
						lItem.ImageKey = "_16___Group___" & getGroupColour(CInt(gItem.Attributes.ItemOf("colr").Value), False)
						lItem.Name = gItem.Attributes.ItemOf("guid").Value
						lItem.IndentCount = iIndent
						If (lItem.Name = frmMain.tvwServerList.SelectedNode.Name) Then lItem.SubItems.Add("<-- Adding here")
						lstConflict.Items.Add(lItem)
						lItem = Nothing
					End If
				Next
			Next

			' Add Existing Resource Entry...
			Dim sImageKey16 As String
			Dim sAttributes As String = xml_getSpecificItem(sResourceGUID) ' name | type | guid | checking
			If (sAttributes IsNot Nothing) Then
				Dim sChecking As String = Split(sAttributes, "|")(3) ' checking
				Dim gList As List(Of XmlNode) = xml_getGroupList_FromResource(sResourceGUID)
				If (gList Is Nothing) Then Exit Sub

				lFound = lstConflict.Items.Find(gList(gList.Count - 1).Attributes.ItemOf("guid").Value, False)

				' Create Resource Item...
				Select Case Split(sAttributes, "|")(1) ' type
					Case "Windows Service"
						sImageKey16 = "_16___Resource___Services"

					Case "Hotfix Patch"
						sImageKey16 = "_16___Resource___Hotfix"

					Case "Eventlog Scan"
						sImageKey16 = "_16___Resource___Eventlog"

					Case "Free Space Threshold"
						sImageKey16 = "_16___Resource___Drive"
						sChecking = "Critical: " & Split(sAttributes, "|")(3) & ", Warning: " & Split(sAttributes, "|")(4)

					Case "Registry Scan"
						sImageKey16 = "_16___Resource___Registry"

					Case "WMI Query"
						sImageKey16 = "_16___Resource___WMIQuery"

					Case Else
						sImageKey16 = "_16___Blank"
				End Select

				lItem = New ListViewItem(Split(sAttributes, "|")(0)) ' name
				lItem.Name = (Split(sAttributes, "|")(2))			 ' guid
				lItem.ImageKey = sImageKey16
				lItem.ForeColor = SystemColors.ControlDarkDark
				lItem.IndentCount = lFound(0).IndentCount + 1
				lItem.SubItems.Add(sChecking)
				lstConflict.Items.Insert(lFound(0).Index + 1, lItem)
			End If
		Next
		lstConflict.EndUpdate()
	End Sub

	Private Sub cmdClose_Click(sender As System.Object, e As System.EventArgs) Handles cmdClose.Click
		img16.Dispose()
		imgSPACING.Dispose()
		lResourceConflicts.Clear()
		Me.Close()
		Me.Dispose()
	End Sub

	Private Sub lstConflict_Enter(sender As Object, e As System.EventArgs) Handles lstConflict.Enter
		picIcon.Focus()
	End Sub

	Private Sub lnkHelp_LinkClicked(sender As System.Object, e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lnkHelp.LinkClicked
		frmHelp.sSelectPageByID = "help0024"
		frmHelp.ShowDialog(Me)
	End Sub
End Class