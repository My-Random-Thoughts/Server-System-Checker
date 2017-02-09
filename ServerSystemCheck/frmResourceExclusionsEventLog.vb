Option Explicit On
Imports System.Xml

Public Class frmResourceExclusionsEventLog
	Public sGroupGUID As String		' Current location
	Private img16 As New ImageList
	Private imgSPACING As New ImageList

	Private Sub frmScan_Results_Exclusions_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
		For Each c As Control In Controls
			c.Font = frmMain.sysFont
		Next
		lblTitle.Font = frmMain.sysFontTitle
		lnkHelp.Font = frmMain.sysFontHelp

		lnkHelp.LinkBehavior = LinkBehavior.HoverUnderline
		picIcon.Image = My.Resources._48___Eventlog.ToBitmap

		With img16
			.ImageSize = New Size(16, 16)
			.ColorDepth = ColorDepth.Depth32Bit
			With .Images
				.Clear()
				.Add("_16___Resource___Eventlog", My.Resources._16___Resource___Eventlog)
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
				.Add("", "Resource Location", 150)
				.Add("", "Details", lstConflict.Width - 150 - SystemInformation.VerticalScrollBarWidth - 4)
			End With
		End With
		Call buildTreeInListView()
		If (bUseVisualStyles = True) Then
			Call SetWindowTheme(lstConflict.Handle, "Explorer", Nothing)
		End If

		Me.Visible = True
		Application.DoEvents()
	End Sub

	Private Sub buildTreeInListView()
		Dim lItem As ListViewItem
		Dim iIndent As Integer
		Dim lFound() As ListViewItem

		' First create the group structure...
		Dim xGroupList As List(Of XmlNode) = xml_getGroupList_FromGroup(sGroupGUID, eDirectionalSearch.ToParents)
		For Each gItem As XmlNode In xGroupList
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
			lstConflict.Items.Add(lItem)
		Next

		' Build list of resources...
		Dim lELS As New List(Of XmlElement)
		Dim lResources As List(Of XmlElement) = xml_getResourceList(sGroupGUID, eDirectionalSearch.ToParents, True)
		If (lResources IsNot Nothing) Then
			For Each xEl As XmlElement In lResources
				If (xEl.Attributes.ItemOf("type").Value = "Eventlog Scan") Then lELS.Add(xEl)
			Next
		End If

		' Add Resource...
		For Each xResource As XmlElement In lELS
			Dim sResource As String = xResource.GetAttribute("guid").ToString
			Dim gList As List(Of XmlNode) = xml_getGroupList_FromResource(sResource)

			If (gList IsNot Nothing) Then
				For i As Integer = 0 To gList.Count - 1
					lFound = lstConflict.Items.Find(gList.Item(i).Attributes.ItemOf("guid").Value, False)
					If (lFound.Count = 0) Then
						' Create Missing Node...
						lItem = New ListViewItem(gList.Item(i).Attributes.ItemOf("name").Value)
						lItem.ImageKey = "_16___Group___" & getGroupColour(CInt(gList.Item(i).Attributes.ItemOf("colr").Value), False)
						lItem.Name = gList.Item(i).Attributes.ItemOf("guid").Value
						lItem.IndentCount = i
						lstConflict.Items.Add(lItem)
					End If
				Next

				Dim sAttributes As String = xml_getSpecificItem(sResource) ' name | type | guid | checking
				If (sAttributes IsNot Nothing) Then
					Dim sCheck() As String
					Dim sChecking As String = Split(sAttributes, "|")(3) ' checking
					Dim sOperation = "Excluding:"
					If (sChecking.Contains(sOperation) = False) Then sOperation = "Specifically:"

					sCheck = Split(sChecking, sOperation)
					If (sCheck.Length <> 2) Then Exit Sub
					If (sCheck(0) = vbNullString) Then sCheck(0) = "All Eventlog Levels, "
					If (sCheck(1) = vbNullString) Then sCheck(1) = "(nothing)"

					gList = Nothing
					gList = xml_getGroupList_FromResource(sResource)
					If (gList Is Nothing) Then Exit Sub
					lFound = lstConflict.Items.Find(gList(gList.Count - 1).Attributes.ItemOf("guid").Value, False)

					' Create Resource Item...
					' List First...
					lItem = New ListViewItem(" ")
					lItem.IndentCount = lFound(0).IndentCount + 1
					lItem.SubItems.Add("      " & sCheck(1))
					lstConflict.Items.Insert(lFound(0).Index + 1, lItem)

					' Main Item Second...
					lItem = New ListViewItem(Split(sAttributes, "|")(0)) ' name
					lItem.ImageKey = "_16___Resource___Eventlog"
					lItem.IndentCount = lFound(0).IndentCount + 1
					lItem.SubItems.Add(sCheck(0) & sOperation)
					lstConflict.Items.Insert(lFound(0).Index + 1, lItem)
				End If
			End If
		Next
	End Sub

	Private Sub cmdCancel_Click(sender As System.Object, e As System.EventArgs) Handles cmdCancel.Click
		img16.Dispose()
		imgSPACING.Dispose()
		Me.Close()
		Me.Dispose()
	End Sub

	Private Sub lstConflict_Enter(sender As Object, e As System.EventArgs) Handles lstConflict.Enter
		picIcon.Focus()
	End Sub

	Private Sub lnkHelp_LinkClicked(sender As System.Object, e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lnkHelp.LinkClicked
		frmHelp.sSelectPageByID = "help0025"
		frmHelp.ShowDialog(Me)
	End Sub

	Private Sub frmScan_Results_Exclusions_Resize(sender As Object, e As System.EventArgs) Handles Me.Resize
		If (lstConflict.Columns.Count > 0) Then
			With lstConflict.Columns
				.Item(0).Width = 150
				.Item(1).Width = (lstConflict.Width - 160)
			End With
		End If
	End Sub
End Class