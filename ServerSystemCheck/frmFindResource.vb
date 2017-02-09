Imports System.Xml

Public Class frmFindResource
	Private img16 As New ImageList
	Private imgSPACING As New ImageList
	Private WithEvents mLocate As New ContextMenuStrip

	Private Sub frmFindResource_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
		For Each c As Control In Controls
			c.Font = frmMain.sysFont
		Next

		Me.Size = New Size(550, 135)
		picIcon.Image = My.Resources._48___Search.ToBitmap

		mLocate.Items.Clear()
		mLocate.Items.Add("Locate Resource", My.Resources._16___Scan___Scanning.ToBitmap, AddressOf onClick_Locate)

		With img16
			.ImageSize = New Size(16, 16)
			.ColorDepth = ColorDepth.Depth32Bit
			With .Images
				.Clear()
				.Add("_16___Scan___Failed", My.Resources._16___Scan___Failed)
				.Add("_16___Resource___Services", My.Resources._16___Resource___Services)
				.Add("_16___Resource___Hotfix", My.Resources._16___Resource___Hotfix)
				.Add("_16___Resource___Eventlog", My.Resources._16___Resource___Eventlog)
				.Add("_16___Resource___Drive", My.Resources._16___Resource___Drive)
				.Add("_16___Resource___Registry", My.Resources._16___Resource___Registry)
				.Add("_16___Resource___WMIQuery", My.Resources._16___Resource___WMIQuery)

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

		txtSearchFor.Text = vbNullString

		Dim cItem As ctrlComboBox_Icons.IconComboItem
		With cmoSearchType
			.ItemHeight = 19
			.DropDownHeight = 192
			.DropDownWidth = 175
			.Visible = True
			With .Items
				.Clear()
				cItem = New ctrlComboBox_Icons.IconComboItem("Eventlog Scan")
				cItem.ItemImage = My.Resources._16___Resource___Eventlog
				.Add(cItem)

				cItem = New ctrlComboBox_Icons.IconComboItem("File Check")
				cItem.ItemImage = My.Resources._16___Resource___File
				.Add(cItem)

				cItem = New ctrlComboBox_Icons.IconComboItem("Free Space Threshold")
				cItem.ItemImage = My.Resources._16___Resource___Drive
				.Add(cItem)

				cItem = New ctrlComboBox_Icons.IconComboItem("Hotfix Patch")
				cItem.ItemImage = My.Resources._16___Resource___Hotfix
				.Add(cItem)

				cItem = New ctrlComboBox_Icons.IconComboItem("Registry Scan")
				cItem.ItemImage = My.Resources._16___Resource___Registry
				.Add(cItem)

				cItem = New ctrlComboBox_Icons.IconComboItem("Windows Service")
				cItem.ItemImage = My.Resources._16___Resource___Services
				.Add(cItem)

				cItem = New ctrlComboBox_Icons.IconComboItem("WMI Query")
				cItem.ItemImage = My.Resources._16___Resource___WMIQuery
				.Add(cItem)
			End With
			.SelectedIndex = 0
		End With
		cItem = Nothing

		With lstResults
			.Clear()
			.Groups.Clear()
			.Items.Clear()
			.FullRowSelect = True
			.SmallImageList = imgSPACING
			.View = View.Details
			.HeaderStyle = ColumnHeaderStyle.None
			With .Columns
				.Clear()
				.Add("", "", lstResults.Width - SystemInformation.VerticalScrollBarWidth - 4)
			End With
		End With

		If (bUseVisualStyles = True) Then
			Call SetWindowTheme(lstResults.Handle, "Explorer", Nothing)
		End If
	End Sub

	Private Sub cmdSearch_Click(sender As System.Object, e As System.EventArgs) Handles cmdSearch.Click
		If (cmoSearchType.SelectedIndex = -1) Then Exit Sub
		If (txtSearchFor.Text.Trim = vbNullString) Then Exit Sub

		lstResults.Items.Clear()
		lblResults.Text = "0 results"

		Dim lResources As New List(Of XmlElement)
		For Each nNode As TreeNode In frmMain.tvwServerList.Nodes
			lResources.AddRange(xml_getResourceList(nNode.Name, eDirectionalSearch.ToChildren, True))
		Next

		Dim lResourcesFiltered As List(Of XmlElement) = lResources.FindAll(Function(f) f.Attributes("type").Value = cmoSearchType.Text.Trim)
		If (lResourcesFiltered.Count = 0) Then
			Call addItem("", -1, 0)
			Call addItem("There are no resources of that type found", 0, 1)
		End If

		Dim lResourcesFound As New List(Of XmlElement)
		For Each xResource As XmlElement In lResourcesFiltered
			If ((xResource.Attributes.ItemOf("name").Value.ToLower.Contains(txtSearchFor.Text.ToLower)) Or _
				(xResource.Attributes.ItemOf("checking").Value.ToLower.Contains(txtSearchFor.Text.ToLower))) Then lResourcesFound.Add(xResource)
		Next

		If ((lResourcesFiltered.Count > 0) AndAlso (lResourcesFound.Count = 0)) Then
			Call addItem("", -1, 0)
			Call addItem("There are no resources matching that query", 0, 1)
		End If

		Dim iCount As Integer = lResourcesFound.Count
		lblResults.Text = iCount.ToString & " result" & IIf(iCount = 1, "", "s").ToString

		lstResults.Enabled = True
		If (Me.Height = 135) Then Me.Size = New Size(550, 350)
		If (iCount > 0) Then Call buildTreeInListView(lResourcesFound) Else lstResults.Enabled = False
	End Sub

	Private Sub buildTreeInListView(ByVal lResourcesFound As List(Of XmlElement))
		Dim lItem As ListViewItem
		Dim iIndent As Integer
		Dim lFound() As ListViewItem

		lstResults.BeginUpdate()
		For Each xNode As XmlElement In lResourcesFound
			Dim xGroupList As New List(Of XmlNode)
			Dim sResourceGUID As String = xNode.Attributes.ItemOf("guid").Value
			xGroupList = xml_getGroupList_FromResource(sResourceGUID)

			For Each gItem As XmlNode In xGroupList
				If (lstResults.Items.Find(gItem.Attributes.ItemOf("guid").Value, False).Count = 0) Then
					If (gItem.ParentNode.Name = "root") Then
						iIndent = 0
					Else
						lFound = lstResults.Items.Find(gItem.ParentNode.Attributes.ItemOf("guid").Value, False)
						If ((lFound Is Nothing) Or (lFound.Count = 0)) Then iIndent = 0 Else iIndent = lFound(0).IndentCount + 1
					End If

					lItem = New ListViewItem(gItem.Attributes.ItemOf("name").Value)
					lItem.ImageKey = "_16___Group___" & getGroupColour(CInt(gItem.Attributes.ItemOf("colr").Value), False)
					lItem.Name = gItem.Attributes.ItemOf("guid").Value
					lItem.IndentCount = iIndent
					lstResults.Items.Add(lItem)
					lItem = Nothing
				End If
			Next

			' Add Existing Resource Entry...
			Dim sImageKey16 As String
			Dim sAttributes As String = xml_getSpecificItem(sResourceGUID) ' name | type | guid | checking
			If (sAttributes IsNot Nothing) Then
				Dim sChecking As String = Split(sAttributes, "|")(3) ' checking
				Dim gList As List(Of XmlNode) = xml_getGroupList_FromResource(sResourceGUID)
				If (gList Is Nothing) Then Exit Sub

				lFound = lstResults.Items.Find(gList(gList.Count - 1).Attributes.ItemOf("guid").Value, False)

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

				lItem = New ListViewItem(Split(sAttributes, "|")(0) & "  -  " & sChecking)
				lItem.Name = (Split(sAttributes, "|")(2))			 ' guid
				lItem.ImageKey = sImageKey16
				lItem.IndentCount = lFound(0).IndentCount + 1
				lstResults.Items.Insert(lFound(0).Index + 1, lItem)
			End If
		Next
		lstResults.EndUpdate()
	End Sub

	Private Sub addItem(ByVal sString As String, ByVal iIconIndex As Integer, ByVal iIndent As Integer)
		Dim lvItem As New ListViewItem(sString, iIconIndex)
		lvItem.IndentCount = iIndent
		lstResults.Items.Add(lvItem)
		lvItem = Nothing
	End Sub

	Private Sub txtSearchFor_KeyUp(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles txtSearchFor.KeyUp
		If (e.KeyCode = Keys.Enter) Then Call cmdSearch_Click(sender, e)
	End Sub

	Private Sub lstResults_MouseClick(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles lstResults.MouseClick
		If (e.Button <> Windows.Forms.MouseButtons.Right) Then Exit Sub
		Dim lsNode As ListViewItem = lstResults.GetItemAt(e.X, e.Y)
		If (lsNode Is Nothing) Then Exit Sub
		If (lsNode.ImageKey.StartsWith("_16___Group___")) Then Exit Sub
		mLocate.Show(lstResults, New Point(e.X, e.Y))
	End Sub

	Private Sub lstResults_MouseDoubleClick(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles lstResults.MouseDoubleClick
		If (lstResults.SelectedItems(0).ImageKey.StartsWith("_16___Group___")) Then Exit Sub
		Call onClick_Locate(sender, e)
	End Sub

	Private Sub onClick_Locate(sender As System.Object, e As System.EventArgs)
		Dim sGroupGUID As String = lstResults.SelectedItems(0).Name
		Dim lNode As List(Of XmlNode) = xml_getGroupList_FromResource(sGroupGUID)

		frmMain.tvwServerList.SelectedNode = frmMain.tvwServerList.Nodes.Find(lNode(lNode.Count - 1).Attributes.ItemOf("guid").Value, True)(0)
		Application.DoEvents()
		frmMain.lstResources.MultiSelect = False
		frmMain.lstResources.Items.Find(sGroupGUID, False)(0).Selected = True
		frmMain.lstResources.Items.Find(sGroupGUID, False)(0).EnsureVisible()
		frmMain.lstResources.MultiSelect = True
	End Sub

	Private Sub cmdCancel_Click(sender As System.Object, e As System.EventArgs) Handles cmdCancel.Click
		Me.Close()
		Me.Dispose()
	End Sub

	Private Sub lstResults_Resize(sender As Object, e As System.EventArgs) Handles lstResults.Resize
		If (Me.Visible = False) Then Exit Sub
		If (Me.Height > 175) Then
			lstResults.Columns(0).Width = lstResults.Width - SystemInformation.VerticalScrollBarWidth - 4
		End If
	End Sub
End Class