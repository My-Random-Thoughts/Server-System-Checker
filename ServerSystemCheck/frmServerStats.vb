Imports System.Xml

Public Class frmServerStats

	Public sGroupGUID As String		' Currently selected group
	Private img16 As New ImageList
	Private imgSPACING As New ImageList

	Private Sub frmServerStats_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
		For Each c As Control In Controls
			c.Font = frmMain.sysFont
		Next
		lblTitle.Font = frmMain.sysFontTitle
		lnkHelp.Font = frmMain.sysFontHelp

		lnkHelp.LinkBehavior = LinkBehavior.HoverUnderline
		picIcon.Image = My.Resources._48___Statistics.ToBitmap

		With img16
			.ImageSize = New Size(16, 16)
			.ColorDepth = ColorDepth.Depth32Bit
			With .Images
				.Clear()
				.Add("0", CType(My.Resources.ResourceManager.GetObject("_16___Group___" & m_IconGroup & "___Yellow"), Drawing.Icon))
				.Add("1", My.Resources._16___Server_With_Properties)
			End With
		End With

		With tvwStats
			.ImageList = img16
			.ItemHeight = 18
			.Nodes.Clear()
			.ShowLines = False
			.ShowRootLines = True
			.FullRowSelect = True
			.ShowNodeToolTips = True
		End With

		cmoListBy.ItemHeight = 19
		cmoListBy.DropDownHeight = 192
		cmoListBy.RemoveIconSpacing = True
		With cmoListBy.Items
			.Clear()
			.Add(New ctrlComboBox_Icons.IconComboItem("Operating System"))
			.Add(New ctrlComboBox_Icons.IconComboItem("Hardware Model"))
			.Add(New ctrlComboBox_Icons.IconComboItem("Memory Size"))
			.Add(New ctrlComboBox_Icons.IconComboItem("Processor Type"))
		End With

		If (bUseVisualStyles = True) Then
			Call SetWindowTheme(tvwStats.Handle, "Explorer", Nothing)
		End If

		Me.Visible = True
		Application.DoEvents()
		cmoListBy.SelectedIndex = 0
	End Sub

	Private Sub cmdOK_Click(sender As System.Object, e As System.EventArgs) Handles cmdOK.Click
		Me.Close()
		Me.Dispose()
	End Sub

	Private Sub loadData()
		Me.Cursor = Cursors.WaitCursor
		tvwStats.Nodes.Clear()
		tvwStats.BeginUpdate()
		Dim iCount As Integer = 0
		Dim xServers As New List(Of XmlNode)

		If (sGroupGUID <> "-1") Then
			xServers.AddRange(xml_getServerList_FromGroup(sGroupGUID))
		Else
			For Each nNode As TreeNode In frmMain.tvwServerList.Nodes
				xServers.AddRange(xml_getServerList_FromGroup(nNode.Name))
			Next
		End If

		If (xServers IsNot Nothing) Then
			For Each xServer As XmlNode In xServers
				Dim sData() As String = xml_LoadServerData(xServer.Attributes.ItemOf("guid").Value)
				If (sData IsNot Nothing) Then
					iCount = iCount + 1
					Select Case cmoListBy.SelectedIndex
						Case 0 : Call addItem(sData(1), sData(0), xServer.Attributes.ItemOf("name").Value)
						Case 1 : Call addItem(sData(4), sData(3), xServer.Attributes.ItemOf("name").Value)
						Case 2 : Call addItem(convertRAM(sData(2), 0)(0), "RAM Size", xServer.Attributes.ItemOf("name").Value)
						Case 3 : Call addItem(sData(7), "Processor Type", xServer.Attributes.ItemOf("name").Value)
					End Select
				End If
				sData = Nothing
			Next
		End If

		For Each pNode As TreeNode In tvwStats.Nodes
			Dim iTotal As Integer = 0
			For Each nNode As TreeNode In pNode.Nodes
				iTotal = iTotal + nNode.Nodes.Count
				nNode.Text = nNode.Text & " (" & nNode.Nodes.Count & ")"
			Next
			pNode.Text = pNode.Text & " (" & iTotal & ")"
		Next

		lblTotals.Text = iCount & " server" & IIf(iCount = 1, "", "s").ToString & ", out of a total of " & xServers.Count

		tvwStats.Sort()
		tvwStats.EndUpdate()
		If (tvwStats.Nodes.Count = 1) Then tvwStats.Nodes(0).Expand()
		Me.Cursor = Cursors.Default
	End Sub

	Private Sub addItem(ByVal sLabel As String, ByVal sParent As String, sServer As String)
		If (sParent = vbNullString) Then Exit Sub
		If (sLabel = vbNullString) Then sLabel = "Unknown"

		sLabel = ReplaceMultiple(sLabel, "", {"(R)", "(TM)", "®", "™", ","})
		sParent = ReplaceMultiple(sParent, "", {"(R)", "(TM)", "®", "™", ","})
		sParent = sParent.Replace("Microsoft Windows", "Windows")
		sLabel = cleanString(sLabel, {"  "}, {" "})

		Dim pNode() As TreeNode = tvwStats.Nodes.Find(sParent, False)
		If ((pNode Is Nothing) OrElse (pNode.Count = 0)) Then tvwStats.Nodes.Add(sParent, sParent, 0, 0)

		Dim nNode() As TreeNode = tvwStats.Nodes(sParent).Nodes.Find(sLabel, False)
		If ((nNode Is Nothing) OrElse (nNode.Count = 0)) Then tvwStats.Nodes(sParent).Nodes.Add(sLabel, sLabel, 0, 0)

		tvwStats.Nodes(sParent).Nodes(sLabel).Nodes.Add(sServer, sServer, 1, 1)
	End Sub

	Private Sub cmoListBy_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cmoListBy.SelectedIndexChanged
		Call loadData()
	End Sub
End Class