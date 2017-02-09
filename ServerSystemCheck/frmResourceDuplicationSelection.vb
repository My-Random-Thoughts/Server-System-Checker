Imports System.Xml

Public Class frmResourceDuplicationSelection
	Private imgSPACING As New ImageList
	Private iCurrentDupe As Integer = 0
	Public bReturnValue As Boolean = False
	Public xDuplicates As New List(Of XmlElement)

	Private Sub frmResourceDuplicationSelection_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
		For Each c As Control In Controls
			c.Font = frmMain.sysFont
		Next
		lblTitle.Font = frmMain.sysFontTitle

		lnkHelp.LinkBehavior = LinkBehavior.HoverUnderline
		picIcon.Image = My.Resources._48___Search.ToBitmap
		picIconOverlay.Image = My.Resources._16___Eventlog___Warning.ToBitmap

		With imgSPACING
			.ImageSize = New Size(iListViewIconWidth, iListViewLineHeight)
			.Images.Add("_16___Operator___GreaterThan", ResizeImage(My.Resources._16___Operator___GreaterThan.ToBitmap, New Size(16, 16), True))
			.ColorDepth = ColorDepth.Depth32Bit
		End With

		With lstResources
			.Clear()
			.View = View.Details
			.SmallImageList = imgSPACING
			.FullRowSelect = True
			.LabelWrap = True
			.MultiSelect = False
			.HeaderStyle = ColumnHeaderStyle.None

			With .Groups
				.Add("0", "Resource Details")
				.Add("1", "Source Group")
				.Add("2", "Target Group")
			End With

			With .Columns
				.Add("", 125)
				.Add("", 24)
				.Add("", lstResources.Width - 149 - 4)
			End With
		End With

		If (bUseVisualStyles = True) Then
			Call SetWindowTheme(lstResources.Handle, "Explorer", Nothing)
		End If

		Call loadData(0)
	End Sub

	Private Sub loadData(ByVal iCount As Integer)
		lblTitle.Text = "Resource Duplication  (" & (iCount + 1).ToString & " of " & (xDuplicates.Count / 2).ToString & ")"

		With lstResources.Items
			.Clear()
			.Add("Type :").SubItems.Add("")
			.Add("Name :").SubItems.Add("")
		End With

		For i As Integer = 0 To 1
			Dim xNode As XmlElement = xDuplicates((iCount * 2) + i)

			With lstResources
				If i = 0 Then
					.Items(0).SubItems.Add(xNode.Attributes("type").Value)
					.Items(1).SubItems.Add(xNode.Attributes("name").Value)
				End If

				With .Items
					.Add("Group : ").SubItems.Add("")
					.Add("Checking :").SubItems.Add("")
				End With

				Dim sPath As String = vbNullString
				Dim xPath As List(Of XmlNode) = xml_getGroupList_FromResource(xNode.Attributes("guid").Value)
				For Each pPath As XmlNode In xPath
					sPath = sPath & pPath.Attributes("name").Value & "\"
				Next
				.Items(2 + (i * 2)).SubItems.Add(sPath.TrimEnd("\"c))
				.Items(3 + (i * 2)).SubItems.Add(xNode.Attributes("checking").Value)

				If (i = 0) Then cmdSource.Tag = xNode.Attributes("guid").Value
				If (i = 1) Then cmdTarget.Tag = xNode.Attributes("guid").Value
			End With
		Next

		' Grouping...
		For i As Integer = 0 To 1 : lstResources.Items(i).Group = lstResources.Groups("0") : Next
		For i As Integer = 2 To 3 : lstResources.Items(i).Group = lstResources.Groups("1") : Next
		For i As Integer = 4 To 5 : lstResources.Items(i).Group = lstResources.Groups("2") : Next

		' Column Resize...
		Dim iLength As Integer = -1
		Dim g As Graphics = Graphics.FromImage(My.Resources._16___Blank.ToBitmap)
		For i As Integer = 0 To (lstResources.Items.Count - 1)
			Dim iSize As Integer = MeasureDisplayStringWidth(g, lstResources.Items(i).SubItems(1).Text, lstResources.Font)
			If (iSize > iLength) Then iLength = iSize
		Next
		iLength = iLength + 12
		If (iLength > lstResources.Columns(1).Width) Then lstResources.Columns(1).Width = iLength

		If (lstResources.Items(3).SubItems(2).Text <> lstResources.Items(5).SubItems(2).Text) Then
			lstResources.Items(3).SubItems(1).Text = "ICON:0"
			lstResources.Items(5).SubItems(1).Text = "ICON:0"
		End If

		iCurrentDupe = iCount
	End Sub

	Private Sub cmdRemoveOne_Click(sender As System.Object, e As System.EventArgs) Handles cmdSource.Click, cmdTarget.Click
		Dim sGUID As String = cmdSource.Tag.ToString
		Dim cButton As Button = CType(sender, Button)
		If (cButton.Name = "cmdTarget") Then sGUID = cmdTarget.Tag.ToString

		If (xml_setRemoveResource(sGUID) = False) Then
			MessageBox.Show(Me, "There was problem removing the duplicated resource.", APN, MessageBoxButtons.OK, MessageBoxIcon.Warning)
		Else
			If ((iCurrentDupe * 2) < ((xDuplicates.Count / 2) - 1)) Then
				Call loadData(iCurrentDupe + 1)
			Else
				bReturnValue = True
				Close()
			End If
		End If
	End Sub

	Private Sub cmdCancel_Click(sender As System.Object, e As System.EventArgs) Handles cmdCancel.Click
		bReturnValue = False
		Close()
	End Sub
End Class