Imports System.Management

Public Class frmPropertiesServer
	Public sServerName As String
	Public sServerGUID As String
	Private bFromCache As Boolean = False
	Private img16 As New ImageList
	Private imgSPACING As New ImageList
	Private Const iMaxIndent As Integer = 6
	Private mConnect As New ContextMenuStrip

	Private Sub frmPropertiesServer_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
		For Each c As Control In Controls
			c.Font = frmMain.sysFont
		Next
		lblServerName.Font = frmMain.sysFontTitle
		lnkHelp.Font = frmMain.sysFontHelp

		lnkHelp.LinkBehavior = LinkBehavior.HoverUnderline
		picIcon.Image = My.Resources._48___Server.ToBitmap
		lblServerName.Text = sServerName.ToUpper & " Properties"

		lblInfo_01.Text = vbNullString
		lblToolTip.Visible = False
		lblToolTip.BackColor = SystemColors.Info

		With img16
			.ImageSize = New Size(16, 16)
			.ColorDepth = ColorDepth.Depth32Bit
			With .Images
				.Clear()
				.Add("WAIT", My.Resources._16___Scan___Scanning)
				.Add("WARN", My.Resources._16___Eventlog___Warning)
				.Add("ERRO", My.Resources._16___Eventlog___Error)
				.Add("NONE", My.Resources._16___Blank)
			End With
		End With

		With imgSPACING
			.ImageSize = New Size(iListViewIconWidth, iListViewLineHeight)
			.ColorDepth = ColorDepth.Depth32Bit
			For i As Integer = 0 To img16.Images.Count - 1
				.Images.Add(img16.Images.Keys(i), ResizeImage(img16.Images(i), .ImageSize, True))
			Next
		End With

		With lstInfo
			.Clear()
			.View = View.Details
			With .Columns
				.Clear()
				.Add("Item", (iListViewIconWidth * (iMaxIndent + 1)) + 8)
				.Add("Value", lstInfo.Width - lstInfo.Columns(0).Width - SystemInformation.VerticalScrollBarWidth - 4)
			End With
			.HeaderStyle = ColumnHeaderStyle.None
			.FullRowSelect = True
			.SmallImageList = imgSPACING
			.ShowGroups = True
			With .Groups
				.Add("ERR", "Error Information")
				.Add("INF", "Scanning Server")

				.Add("OPS", "Operating System")
				.Add("HWI", "Hardware Information")
				.Add("CPU", "Processor Information")
				.Add("MEM", "Memory Details")
			End With
		End With

		If (bUseVisualStyles = True) Then
			Call SetWindowTheme(lstInfo.Handle, "Explorer", Nothing)
		End If

		cmdRefreshData.Enabled = Not bReadOnlyMode

		Me.Visible = True
		Application.DoEvents()
		lblCacheData.Visible = False

		Dim sData() As String = xml_LoadServerData(sServerGUID)
		If (sData Is Nothing) Then
			If ((m_NetworkAvailability = True) AndAlso (bReadOnlyMode = False)) Then Call cmdRefreshData_Click(sender, e)
		Else
			lblCacheData.Visible = True
			displayData(sData)
		End If

		If (m_NetworkAvailability = False) Then
			Call addListItem("err1", "ICON:ERRO", "There is no network connection", "ERR", iMaxIndent)
			Call addListItem("err2", "ICON:NONE", "Refresh and connection options are disabled", "ERR", iMaxIndent)
		End If

		If (bReadOnlyMode = True) Then
			Call addListItem("err1", "ICON:ERRO", "The configuration file is currently read-only", "ERR", iMaxIndent)
			Call addListItem("err2", "ICON:NONE", "Refresh and connection options are disabled", "ERR", iMaxIndent)
		End If

		cmdConnect.Enabled = lblCacheData.Visible And (m_NetworkAvailability And Not bReadOnlyMode)
		cmdRefreshData.Enabled = (m_NetworkAvailability And Not bReadOnlyMode)
		cmdExcludeDrives.Enabled = cmdExcludeDrives.Enabled And (m_NetworkAvailability And Not bReadOnlyMode)
	End Sub

	Private Sub cmdClose_Click(sender As System.Object, e As System.EventArgs) Handles cmdClose.Click
		img16.Dispose()
		imgSPACING.Dispose()
		Me.Close()
		Me.Dispose()
	End Sub

	Private Sub setupScanning(ByVal bStart As Boolean)
		If bStart = True Then
			Me.Cursor = Cursors.WaitCursor
			lblInfo_01.Text = vbNullString
			lstInfo.Items.Clear()
			addListItem("wait", "ICON:WAIT", "Please wait...", "INF", iMaxIndent)
			cmdConnect.Enabled = False
			cmdRefreshData.Enabled = False
			cmdExcludeDrives.Enabled = False
			Me.Refresh()
			Application.DoEvents()
		Else
			Me.Cursor = Cursors.Default
			cmdRefreshData.Enabled = True
			cmdConnect.Enabled = lblCacheData.Visible
		End If
	End Sub

	Private Sub addListItem(ByVal sName As String, ByVal sTitle As String, ByVal sData As String, ByVal sGroup As String, ByVal iIndent As Integer)
		Dim lItem As New ListViewItem()

		sData = ReplaceMultiple(sData, "", {"(R)", "(TM)", "®", "™", ","})
		sData = cleanString(sData, {"  "}, {" "})

		If (sTitle.StartsWith("ICON:")) Then
			lItem.Text = vbNullString
			lItem.ImageKey = sTitle.Substring(5)
		Else
			lItem.Text = sTitle
			lItem.ImageKey = vbNullString
		End If
		lItem.Name = sName
		lItem.SubItems.Add(sData)
		lItem.Group = lstInfo.Groups(sGroup)
		lItem.IndentCount = iIndent
		lstInfo.Items.Add(lItem)
		lItem = Nothing
	End Sub

	Private Sub lstInfo_GotFocus(sender As Object, e As System.EventArgs) Handles lstInfo.GotFocus
		picIcon.Focus()
	End Sub

	Private Sub displayData(ByVal sData() As String)
		lblInfo_01.Text = sData(0).ToString
		If (sData(0) = "Unknown Server Type") Then
			lblInfo_01.Text = vbNullString
			lstInfo.Items.Clear()
			Call addListItem("err1", "ICON:ERRO", "Unknown server type or", "ERR", iMaxIndent)
			Call addListItem("err2", "ICON:NONE", "server cannot be contacted", "ERR", iMaxIndent)
			cmdExcludeDrives.Enabled = False
			picIcon.Image = My.Resources._48___Server___Error.ToBitmap
		Else
			picIcon.Image = My.Resources._48___Server_With_Properties.ToBitmap

			For i As Integer = 0 To sData.Count - 1
				If sData(i) = vbNullString Then sData(i) = "-"
			Next

			lstInfo.Items.Clear()

			Call addListItem("o1", "OS Version :", lblInfo_01.Text, "OPS", 0)
			Call addListItem("o2", "Service Pack :", sData(1).ToString, "OPS", 0)

			Call addListItem("h1", "Make :", sData(3), "HWI", 0)
			Call addListItem("h2", "Model :", sData(4), "HWI", 0)
			Call addListItem("h3", "Serial :", sData(5), "HWI", 0)

			Call addListItem("p1", "Count :", sData(6), "CPU", 0)
			Call addListItem("p2", "Details :", sData(7), "CPU", 1)
			Call addListItem("p3", "Cores :", sData(8), "CPU", 1)
			Call addListItem("p4", "Hyper-Threading :", sData(9), "CPU", 1)

			Dim sRAM() As String = convertRAM(sData(2), 2)
			Call addListItem("m1", "Total Size :", sRAM(0), "MEM", 0)
			If (sRAM.Count > 1) Then
				For i As Integer = 1 To sRAM.Count - 1
					addListItem("mm" & i.ToString, "Stick " & i.ToString, sRAM(i), "MEM", 1)
				Next i
			End If

			cmdExcludeDrives.Enabled = True

			Try
				addListItem("SPC", " ", " ", "OPS", 0)
				addListItem("WAIT", "ICON:WAIT", "Getting Uptime...", "OPS", iMaxIndent)
				Me.Cursor = Cursors.WaitCursor
				Application.DoEvents()
				Dim SST As DateTime = getServerShutdownTime(sServerName)
				lstInfo.Items.RemoveByKey("WAIT")
				If (SST.ToString = "01/01/0001 00:00:00") Then
					Call addListItem("o4", "ICON:WARN", "Uptime Unknown", "OPS", iMaxIndent)
				Else
					Call addListItem("o4", "Uptime :", getDateDiff(Now, SST), "OPS", 0)
					Call addListItem("o5", "Since :", SST.ToString, "OPS", 1)
				End If
			Catch ex As Exception
				Call addListItem("o4", "Uptime :", "Unknown", "OPS", 0)
			Finally
				If (lstInfo.Items.ContainsKey("WAIT") = True) Then lstInfo.Items("WAIT").Remove()
				Me.Cursor = Cursors.Default
			End Try
		End If
	End Sub

	Private Sub cmdRefreshData_Click(sender As System.Object, e As System.EventArgs) Handles cmdRefreshData.Click
		lblCacheData.Visible = False
		lstInfo.Items.Clear()
		Call setupScanning(True)
		Call displayData(frmPropertiesServerGetAll.getServerInfo(sServerGUID))
		If (bReadOnlyMode = False) Then Call saveData()
		Call setupScanning(False)
		frmPropertiesServerGetAll.Dispose()
	End Sub

	Private Sub saveData()
		If (lblInfo_01.Text = vbNullString) Then Exit Sub
		Dim sData(9) As String
		sData.SetValue(lstInfo.Items("o1").SubItems(1).Text, 0)	' lblInfo_01.Text
		sData.SetValue(lstInfo.Items("o2").SubItems(1).Text, 1)

		Dim sString As String = vbNullString
		For Each itm As ListViewItem In lstInfo.Items
			If (itm.Name.StartsWith("ms")) Then sString = sString & itm.SubItems(1).Name & "|"
		Next
		If (sString <> vbNullString) Then sData.SetValue(sString, 2) Else sData.SetValue(lstInfo.Items("m1").SubItems(1).Text, 2)

		sData.SetValue(lstInfo.Items("h1").SubItems(1).Text, 3)
		sData.SetValue(lstInfo.Items("h2").SubItems(1).Text, 4)
		sData.SetValue(lstInfo.Items("h3").SubItems(1).Text, 5)
		sData.SetValue(lstInfo.Items("p1").SubItems(1).Text, 6)
		sData.SetValue(lstInfo.Items("p2").SubItems(1).Text, 7)
		sData.SetValue(lstInfo.Items("p3").SubItems(1).Text, 8)
		sData.SetValue(lstInfo.Items("p4").SubItems(1).Text, 9)
		If (frmPropertiesServerGetAll.xml_SaveServerData(sServerGUID, sData) = True) Then
			lblCacheData.Visible = True

			' Find correct node and change it's icon...
			Dim fNode() As TreeNode = frmMain.tvwServerList.Nodes.Find(sServerGUID, True)
			If (fNode.Count > 0) Then
				With fNode(0)
					.Tag = "SERVWP"
					.ImageKey = "_16___Server_With_Properties"
					.SelectedImageKey = .ImageKey
				End With
			End If
			frmPropertiesServerGetAll.Dispose()
		End If
		cmdConnect.Enabled = lblCacheData.Visible
		Call xml_SaveXmlDocument(sConfigFile)
	End Sub

	Private Sub cmdConnect_MouseUp(sender As Object, e As MouseEventArgs) Handles cmdConnect.MouseUp
		Dim sResult As List(Of String) = xml_LoadConnections(Nothing)
		sResult.Sort()

		With mConnect.Items
			.Clear()
			If ((sResult IsNot Nothing) AndAlso (sResult.Count > 0)) Then
				For Each sItem As String In sResult
					.Add(Split(sItem, "|")(0), My.Resources._16___Connection.ToBitmap, AddressOf onClick_Connection)
					.Item(.Count - 1).Name = Split(sItem, "|")(2)
					.Item(.Count - 1).Tag = "Command: " & Split(sItem, "|")(1)
				Next
			Else
				.Add("(no connections)").Enabled = False
			End If
			.Add("-")
			.Add("Configure", My.Resources._16___Properties.ToBitmap, AddressOf onClick_Connection)
			.Item(.Count - 1).Tag = "Configure Server Connections List"
			.Item(.Count - 1).Enabled = Not bReadOnlyMode
		End With

		For Each mItm As Object In mConnect.Items
			If (TypeOf mItm Is ToolStripMenuItem) Then
				AddHandler CType(mItm, ToolStripMenuItem).MouseEnter, AddressOf onClick_tssInfo_MouseEnter
				AddHandler CType(mItm, ToolStripMenuItem).MouseLeave, AddressOf onClick_tssInfo_MouseLeave
			End If
		Next

		mConnect.Show(cmdConnect, e.Location)
	End Sub
	Private Sub onClick_Connection(sender As Object, e As System.EventArgs)
		If (sender.ToString = "Configure") Then
			frmConnectionEditor.ShowDialog(Me)
		Else
			Try
				Dim TSMI As ToolStripMenuItem = CType(sender, ToolStripMenuItem)
				Dim sResult As List(Of String) = xml_LoadConnections(TSMI.Name)
				Dim sCommand As String = sResult(0).Split("|"c)(1)
				sCommand = sCommand.Replace(sServerSubstitution, sServerName)
				Process.Start(sCommand)

			Catch ex As Exception
				Application.DoEvents()
			End Try
		End If
	End Sub

	Private Sub lnkHelp_LinkClicked(sender As System.Object, e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lnkHelp.LinkClicked
		frmHelp.sSelectPageByID = "help0012"
		frmHelp.ShowDialog(Me)
	End Sub

	Private Sub onClick_tssInfo_MouseEnter(sender As System.Object, e As System.EventArgs)
		Dim sText As String = vbNullString
		sText = CType(sender, ToolStripMenuItem).Tag.ToString
		lblToolTip.Visible = True
		lblToolTip.Text = sText
	End Sub

	Private Sub onClick_tssInfo_MouseLeave(sender As System.Object, e As System.EventArgs)
		lblToolTip.Visible = False
	End Sub

	Private Sub mnuConnect_Closing(sender As Object, e As System.Windows.Forms.ToolStripDropDownClosingEventArgs)
		Dim oObject As ContextMenuStrip = CType(sender, ContextMenuStrip)
		Try
			For Each mItm As Object In oObject.Items
				If (TypeOf mItm Is ToolStripMenuItem) Then
					RemoveHandler CType(mItm, ToolStripMenuItem).MouseEnter, AddressOf onClick_tssInfo_MouseEnter

					' Add Resource Sub Items...
					If (CType(mItm, ToolStripMenuItem).HasDropDownItems = True) Then
						For Each dItm As ToolStripMenuItem In (CType(mItm, ToolStripMenuItem).DropDownItems)
							RemoveHandler dItm.MouseEnter, AddressOf onClick_tssInfo_MouseEnter
						Next
					End If
				End If
			Next
			lblToolTip.Visible = False
		Catch ex As Exception
		End Try
	End Sub

	Private Sub cmdExcludeDrives_Click(sender As System.Object, e As System.EventArgs) Handles cmdExcludeDrives.Click
		frmPropertiesServerDriveExclude.sServerGUID = sServerGUID
		frmPropertiesServerDriveExclude.sServerName = sServerName
		frmPropertiesServerDriveExclude.ShowDialog(Me)
	End Sub

	Private Sub picIcon_DoubleClick(sender As Object, e As System.EventArgs) Handles picIcon.DoubleClick
		MessageBox.Show(Me, "GUID: '" & sServerGUID & "'", APN, MessageBoxButtons.OK, MessageBoxIcon.Information)
	End Sub
End Class