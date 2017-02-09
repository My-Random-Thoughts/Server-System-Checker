Option Explicit On
Imports System.Xml
Imports System.Text

Public Class frmPropertiesGroup
	Public sGroupName As String
	Public sGroupGUID As String
	Private img16 As New ImageList
	Private imgSPACING As New ImageList

	Private Sub frmGroup_Properties_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
		For Each c As Control In Controls
			c.Font = frmMain.sysFont
		Next
		lblTitle.Font = frmMain.sysFontTitle
		lnkHelp.Font = frmMain.sysFontHelp

		lnkHelp.LinkBehavior = LinkBehavior.HoverUnderline
		picIcon.Image = My.Resources._48___Folder.ToBitmap
		txtGroupName.Text = sGroupName

		With img16
			.ImageSize = New Size(16, 16)
			.ColorDepth = ColorDepth.Depth32Bit
			With .Images
				.Clear()
				.Add("_16___Group___" & m_IconGroup & "___Yellow", CType(My.Resources.ResourceManager.GetObject("_16___Group___" & m_IconGroup & "___Yellow"), Icon))
				.Add("_16___Server", My.Resources._16___Server)										' 1
				.Add("_16___Server_With_Properties", My.Resources._16___Server_With_Properties)		' 2
				.Add("_16___Resource___Services", My.Resources._16___Resource___Services)			' 3
				.Add("_16___Resource___Hotfix", My.Resources._16___Resource___Hotfix)				' 4
				.Add("_16___Resource___Eventlog", My.Resources._16___Resource___Eventlog)			' 5
				.Add("_16___Resource___Registry", My.Resources._16___Resource___Registry)			' 6 
				.Add("_16___Resource___File", My.Resources._16___Resource___File)					' 7
				.Add("_16___Resource___WMIQuery", My.Resources._16___Resource___WMIQuery)			' 8
			End With
		End With

		With imgSPACING
			.ImageSize = New Size(iListViewIconWidth, iListViewLineHeight)
			.ColorDepth = ColorDepth.Depth32Bit
			For i As Integer = 0 To img16.Images.Count - 1
				.Images.Add(img16.Images.Keys(i), ResizeImage(img16.Images(i), .ImageSize, True))
			Next
		End With

		Dim cItem As ctrlComboBox_Icons.IconComboItem
		With cmoGroupColour
			.ItemHeight = 19
			.DropDownHeight = 192
			.Items.Clear()

			If (m_UseColouredGroups = True) Then
				Dim sCol As String
				For i As Integer = 0 To iIconColourCount - 1
					sCol = getGroupColour(i, True)
					cItem = New ctrlComboBox_Icons.IconComboItem("" & sCol.Replace(m_IconGroup & "___", vbNullString))
					cItem.ItemImage = CType(My.Resources.ResourceManager.GetObject("_16___Group___" & sCol), Drawing.Icon)
					.Items.Add(cItem)
				Next
				.SelectedIndex = xml_getGroupIconColour(sGroupGUID)
				.Items(.Items.Count - 1).DisplayText = "Yellow (default)" ' Change "Yellow" colour item to add '(default)' to the end

			Else
				cItem = New ctrlComboBox_Icons.IconComboItem("Disabled")
				cItem.ItemImage = CType(My.Resources.ResourceManager.GetObject("_16___Group___" & m_IconGroup & "___Yellow"), Drawing.Icon)
				cItem.IsEnabled = False
				.Items.Add(cItem)
				.SelectedIndex = 0
			End If
		End With

		With lstResourceStats
			.Clear()
			.SmallImageList = imgSPACING
			.View = View.Details
			.MultiSelect = False
			.HeaderStyle = ColumnHeaderStyle.None
			.ShowGroups = True
			With .Groups
				.Add("R", "Resources")
				.Add("G", "Groups / Servers")
			End With
			With .Columns
				.Add("A", "", lstResourceStats.Width - 74 - SystemInformation.VerticalScrollBarWidth - 4)
				.Add("Z", "", 24, HorizontalAlignment.Center, -1)
				.Add("B", "", 50, HorizontalAlignment.Right, -1)
				If (bUseVisualStyles = True) Then .Item("A").Width = .Item("A").Width + SystemInformation.VerticalScrollBarWidth
			End With
		End With

		tabProperties.SelectTab(0)
		cmdServerStats.Visible = False
		cmoGroupColour.Enabled = m_UseColouredGroups
		lblInfoGroupColours.Visible = Not m_UseColouredGroups
		cmdEnableColours.Visible = Not m_UseColouredGroups

		If (bUseVisualStyles = True) Then
			Call SetWindowTheme(lstResourceStats.Handle, "Explorer", Nothing)
		End If

		cmdGetServerDetails.Enabled = m_NetworkAvailability
		Dim iServerCount As Integer = xml_getServerList_FromGroup(sGroupGUID).Count
		If (iServerCount = 0) Then cmdGetServerDetails.Enabled = False

		txtDescription.Text = xml_getGroupDescription(sGroupGUID)
		Call buildServerDetailsDescription()

		If (bReadOnlyMode = True) Then
			cmdOK.Enabled = False
			txtGroupName.Enabled = False
			txtDescription.Enabled = False
			cmoGroupColour.Enabled = False
			cmdEnableColours.Enabled = False
			cmdGetServerDetails.Enabled = False
		End If

		Me.Visible = True
		Application.DoEvents()

		txtGroupName.Focus()
		txtGroupName.SelectAll()
		Call getResourceCounts()
	End Sub

	Private Sub buildServerDetailsDescription()
		Dim sDesc As New StringBuilder
		sDesc.AppendLine("By clicking the button below you can gather some system information from every server within this group, and any child groups.")
		sDesc.AppendLine("")
		sDesc.AppendLine("Collect details such as...")
		sDesc.AppendLine("")
		sDesc.AppendLine("    Operating system type and version")
		sDesc.AppendLine("    System uptime")
		sDesc.AppendLine("    Hardware make, model and serial number")
		sDesc.AppendLine("    CPU speed And type")
		sDesc.AppendLine("    Memory size")
		sDesc.AppendLine("")
		lblServerDetailsDescription.Text = sDesc.ToString
	End Sub

	Private Sub cmdCancel_Click(sender As System.Object, e As System.EventArgs) Handles cmdCancel.Click
		img16.Dispose()
		imgSPACING.Dispose()
		Me.Close()
		Me.Dispose()
	End Sub

	Private Sub getResourceCounts()
		' --------------------------------------------
		'    Windows Services				[o]		xx
		'    Hotfix Patching				[o]		xx
		'    Eventlog Scanning				[o]		xx
		'    Registry Scanning				[o]		xx
		'    File Checking					[o]		xx
		'    WMI Query						[o]		xx
		' --------------------------------------------
		'    Child Groups					[o]		xx		(Child Groups Only)
		'    Servers (Total)				[o]		xx
		'    Servers With Properties		[o]		xx

		With lstResourceStats
			.FullRowSelect = True
			.BackColor = SystemColors.Window
			With .Items
				.Clear()
				.Add("Eventlog Scanning").SubItems.Add("ICON:5")
				.Add("File Checking").SubItems.Add("ICON:7")
				.Add("Hotfix Patching").SubItems.Add("ICON:4")
				.Add("Registry Scanning").SubItems.Add("ICON:6")
				.Add("Windows Services").SubItems.Add("ICON:3")
				.Add("WMI Query").SubItems.Add("ICON:8")

				.Add("Child Groups").SubItems.Add("ICON:0")
				.Add("Servers (Total)").SubItems.Add("ICON:1")
				.Add("Servers With Properties").SubItems.Add("ICON:2")
			End With

			For i As Integer = 0 To 5 : .Items(i).Group = .Groups("R") : Next
			For i As Integer = 6 To 8 : .Items(i).Group = .Groups("G") : Next
		End With

		Dim iEL As Integer = 0
		Dim iFC As Integer = 0
		Dim iHP As Integer = 0
		Dim iRS As Integer = 0
		Dim iWS As Integer = 0
		Dim iWQ As Integer = 0

		Dim xResources As List(Of XmlElement) = xml_getResourceList(sGroupGUID, eDirectionalSearch.ToChildren, True)
		If (xResources IsNot Nothing) Then
			For Each xEle As XmlElement In xResources
				Select Case xEle.GetAttribute("type")
					Case "Eventlog Scan" : iEL = iEL + 1
					Case "File Check" : iFC = iFC + 1
					Case "Hotfix Patch" : iHP = iHP + 1
					Case "Registry Scan" : iRS = iRS + 1
					Case "Windows Service" : iWS = iWS + 1
					Case "WMI Query" : iWQ = iWQ + 1
					Case Else
						Application.DoEvents()
				End Select
			Next
		End If

		Dim iGroups As String
		Dim iServers As String
		Dim iServersWithProperties As String
		Dim xGroups As List(Of XmlNode) = xml_getGroupList_FromGroup(sGroupGUID, eDirectionalSearch.ToChildren)
		Dim xServers As List(Of XmlNode) = xml_getServerList_FromGroup(sGroupGUID)
		If (xGroups IsNot Nothing) Then iGroups = (xGroups.Count - 1).ToString Else iGroups = "-"
		If (xServers IsNot Nothing) Then
			iServers = xServers.Count.ToString
			Dim iCnt As Integer = 0
			For Each xServer As XmlNode In xServers
				If (xml_LoadServerData(xServer.Attributes.ItemOf("guid").Value) IsNot Nothing) Then
					iCnt = iCnt + 1
				End If
			Next
			iServersWithProperties = iCnt.ToString.Trim
		Else
			iServers = "-"
			iServersWithProperties = "-"
		End If

		With lstResourceStats
			.Items(0).SubItems.Add(iEL.ToString)
			.Items(1).SubItems.Add(iFC.ToString)
			.Items(2).SubItems.Add(iHP.ToString)
			.Items(3).SubItems.Add(iRS.ToString)
			.Items(4).SubItems.Add(iWS.ToString)
			.Items(5).SubItems.Add(iWQ.ToString)
			.Items(6).SubItems.Add(iGroups)
			.Items(7).SubItems.Add(iServers)
			.Items(8).SubItems.Add(iServersWithProperties)
		End With

		cmdServerStats.Enabled = True
		lblStats.Text = iServers & vbCrLf & iServersWithProperties
		If (CInt(iServersWithProperties) = 0) Then cmdServerStats.Enabled = False
	End Sub

	Private Sub cmdOK_Click(sender As System.Object, e As System.EventArgs) Handles cmdOK.Click
		txtGroupName.Text = txtGroupName.Text.Trim
		If (txtGroupName.Text.Length = 0) Then Exit Sub
		If (txtGroupName.Text.Length > iLength_GroupName) Then txtGroupName.Text = txtGroupName.Text.Substring(0, iLength_GroupName)
		If (txtDescription.Text.Length > iLength_Description) Then txtDescription.Text = txtDescription.Text.Substring(0, iLength_Description)

		' Get current colour selected, however if disabled, get current colour...
		Dim iGroupColour As Integer = cmoGroupColour.SelectedIndex
		If (cmoGroupColour.Enabled = False) Then iGroupColour = xml_getGroupIconColour(sGroupGUID)

		If (xml_setRenameGroup(sGroupGUID, txtGroupName.Text, iGroupColour, txtDescription.Text) = True) Then
			With frmMain.tvwServerList.SelectedNode
				.ImageKey = "_16___Group___" & getGroupColour(iGroupColour, False)
				.SelectedImageKey = .ImageKey
			End With
			If (txtGroupName.Text <> sGroupName) Then
				With frmMain.tvwServerList
					.SelectedNode.Text = txtGroupName.Text
					.Sort()
				End With
			End If

			img16.Dispose()
			imgSPACING.Dispose()
			Me.Close()
			Me.Dispose()
		End If
	End Sub

	Private Sub lstResourceStats_ColumnWidthChanged(sender As Object, e As System.Windows.Forms.ColumnWidthChangedEventArgs) Handles lstResourceStats.ColumnWidthChanged
		With lstResourceStats
			Dim iWidth As Integer = SystemInformation.VerticalScrollBarWidth
			If (bUseVisualStyles = True) Then iWidth = 0
			If (.Columns.Count <> 3) Then Exit Sub
			If (.Columns(0).Width <> .Width - 74 - iWidth - 4) Then .Columns(0).Width = .Width - 74 - iWidth - 4
			If (.Columns(1).Width <> 24) Then .Columns(1).Width = 24
			If (.Columns(2).Width <> 50) Then .Columns(2).Width = 50
		End With
	End Sub

	Private Sub lstResourceStats_ColumnWidthChanging(sender As Object, e As System.Windows.Forms.ColumnWidthChangingEventArgs) Handles lstResourceStats.ColumnWidthChanging
		Dim iWidth As Integer = SystemInformation.VerticalScrollBarWidth
		If (bUseVisualStyles = True) Then iWidth = 0
		Select Case e.ColumnIndex
			Case 0 : e.NewWidth = lstResourceStats.Width - 74 - iWidth - 4
			Case 1 : e.NewWidth = 24
			Case 2 : e.NewWidth = 50
		End Select
		e.Cancel = True
	End Sub

	Private Sub lnkHelp_LinkClicked(sender As System.Object, e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lnkHelp.LinkClicked
		frmHelp.sSelectPageByID = "help0011"
		frmHelp.ShowDialog(Me)
	End Sub

	Private Sub cmdGetServerDetails_Click(sender As System.Object, e As System.EventArgs) Handles cmdGetServerDetails.Click
		frmPropertiesServerGetAll.sGroupGUID = sGroupGUID
		frmPropertiesServerGetAll.ShowDialog(Me)
		Call doRedrawTreeNodes(frmMain.tvwServerList, m_UppercaseServerNames)

		' Just in case we found some more server details...
		Call getResourceCounts()
	End Sub

	Private Sub cmdEnableColours_Click(sender As System.Object, e As System.EventArgs) Handles cmdEnableColours.Click
		m_UseColouredGroups = Not m_UseColouredGroups
		If (m_UseColouredGroups = True) Then
			cmdEnableColours.Enabled = False
			lblInfoGroupColours.Enabled = False

			With cmoGroupColour
				.Enabled = True
				.ItemHeight = 19
				.DropDownHeight = 192
				.Items.Clear()

				Dim cItem As ctrlComboBox_Icons.IconComboItem
				Dim sCol As String
				For i As Integer = 0 To iIconColourCount - 1
					sCol = getGroupColour(i, True)
					cItem = New ctrlComboBox_Icons.IconComboItem("" & sCol)
					cItem.ItemImage = CType(My.Resources.ResourceManager.GetObject("_16___Group___" & sCol), Drawing.Icon)
					.Items.Add(cItem)
				Next
				.SelectedIndex = xml_getGroupIconColour(sGroupGUID)
			End With
		End If
	End Sub

	Private Sub txtDescription_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles txtDescription.KeyDown
		If (txtDescription.TextLength > iLength_Description) Then e.SuppressKeyPress = True
	End Sub

	Private Sub txtDescription_KeyUp(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles txtDescription.KeyUp
		If (e.Control And e.KeyCode = Keys.A) Then txtDescription.SelectAll()
	End Sub

	Private Sub txtDescription_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtDescription.TextChanged
		If (txtDescription.TextLength > iLength_Description) Then txtDescription.Text = txtDescription.Text.Substring(0, 250)
		lblDescriptionCount.Text = "(" & (iLength_Description - txtDescription.TextLength) & " characters remaining)"
	End Sub

	Private Sub tabProperties_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles tabProperties.SelectedIndexChanged
		If (tabProperties.SelectedIndex = 2) Then cmdServerStats.Visible = True Else cmdServerStats.Visible = False
	End Sub

	Private Sub cmdServerStats_Click(sender As System.Object, e As System.EventArgs) Handles cmdServerStats.Click
		Dim sMsg As String = vbNullString
		sMsg = sMsg & "Would you want to see the stats for this selected group," & vbCrLf
		sMsg = sMsg & "and it's children, or all groups in this config file.?"

		Call clsMessageBox.CustomMsgBox({"Selected", "All Groups", "Cancel"})
		Dim dResult As DialogResult = MessageBox.Show(Me, sMsg, APN, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
		Select Case dResult
			Case Windows.Forms.DialogResult.Cancel : Exit Sub
			Case Windows.Forms.DialogResult.Yes : frmServerStats.sGroupGUID = sGroupGUID
			Case Windows.Forms.DialogResult.No : frmServerStats.sGroupGUID = "-1"
			Case Else : frmServerStats.sGroupGUID = "-1"
		End Select

		frmServerStats.ShowDialog(Me)
	End Sub
End Class