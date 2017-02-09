Option Explicit On
Imports System.Net
Imports System.DirectoryServices
Imports System.IO

Public Class frmAddServer
	Private Const ETF As String = "External Text File"
	Private lvwS As ListViewColumnSorter
	Private iCurrSelected As Integer = -1	' Index of currently selected combobox item
	Private img16 As New ImageList
	Private imgSPACING As New ImageList
	Private mnuRightClick As ContextMenuStrip

	Private Sub frmAddServer_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
		For Each c As Control In Controls
			c.Font = frmMain.sysFont
		Next
		lblTitle.Font = frmMain.sysFontTitle
		lnkHelp.Font = frmMain.sysFontHelp
		lnkHelp.LinkBehavior = LinkBehavior.HoverUnderline

		KeyPreview = True		' Required for CTRL+F key press for searching
		picIcon.Image = My.Resources._48___Server.ToBitmap

		With img16
			.ImageSize = New Size(16, 16)
			.ColorDepth = ColorDepth.Depth32Bit
			With .Images
				.Clear()
				.Add("_16___Server", My.Resources._16___Server)
				.Add("_16___Server___New", My.Resources._16___Server___New)
				.Add("_16___Scan___Failed", My.Resources._16___Scan___Failed)
				.Add("OUs_Closed", My.Resources._16___Book___Closed___AD)
				.Add("OUs_Opened", My.Resources._16___Book___Open___AD)
				.Add("All_Closed", GreyScaleImage(My.Resources._16___Book___Closed___AD.ToBitmap, True))
			End With
		End With

		With imgSPACING
			.ImageSize = New Size(iListViewIconWidth, iListViewLineHeight)
			.ColorDepth = ColorDepth.Depth32Bit
			For i As Integer = 0 To img16.Images.Count - 1
				.Images.Add(img16.Images.Keys(i), ResizeImage(img16.Images(i), .ImageSize, True))
			Next
		End With

		cmdAdd.Image = My.Resources._16___Add.ToBitmap
		cmdDel.Image = My.Resources._16___Remove.ToBitmap
		cmdSearchAD.Image = My.Resources._16___Scan___Scanning.ToBitmap
		cmdCloseADResults.Visible = False
		cmdRescanAD.Visible = False
		cmdSearchAD.Visible = False
		chkResolveIP.Checked = True

		lstAddOU.Visible = False
		lstHidden.Visible = False

		With SplitContainer1
			.Panel2Collapsed = True
			.SplitterDistance = 166
			.IsSplitterFixed = True
		End With
		proProgress.Visible = False

		lvwS = New ListViewColumnSorter()
		With lstServerList
			.Clear()
			.View = View.Details
			.LabelEdit = True
			.Columns.Add("list", "Server List", .Width - SystemInformation.VerticalScrollBarWidth - 4)
			.HeaderStyle = ColumnHeaderStyle.Nonclickable
			.SmallImageList = imgSPACING
			.ListViewItemSorter = lvwS
		End With
		lvwS.SortColumn = lstServerList.Columns("list").Index
		lvwS.Order = SortOrder.Ascending

		Dim cItem As ctrlComboBox_Icons.IconComboItem
		With cmoIconComboBox
			.ItemHeight = 19
			.DropDownHeight = 192
			.Visible = True
			With .Items
				.Clear()
				If (m_NetworkAvailability = True) Then
					cItem = New ctrlComboBox_Icons.IconComboItem("Active Directory")
					cItem.ItemImage = My.Resources._16___Book___Closed___AD
					cItem.IndentCount = 0
					.Add(cItem)
				End If

				cItem = New ctrlComboBox_Icons.IconComboItem(ETF)
				cItem.ItemImage = My.Resources._16___Properties
				cItem.IndentCount = 0
				.Add(cItem)
			End With
			.SelectedIndex = iCurrSelected
		End With

		With tvOUList
			.Nodes.Clear()
			.ItemHeight = 19
			.ImageList = img16
			.ShowRootLines = False
			.ShowLines = False
		End With

		If (bUseVisualStyles = True) Then
			Call SetWindowTheme(lstServerList.Handle, "Explorer", Nothing)
			Call SetWindowTheme(tvOUList.Handle, "Explorer", Nothing)
		End If

		Me.Visible = True
		Application.DoEvents()
		Call disableControls(False)
	End Sub

	Private Sub frmAddServer_KeyUp(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyUp
		If ((e.KeyCode = Keys.F) AndAlso (e.Control)) Then
			If (cmdSearchAD.Visible = True) Then
				Call cmdSearchAD_Click(sender, e)
				e.Handled = True
			End If
		End If
	End Sub

	Private Sub cmdCancel_Click(sender As System.Object, e As System.EventArgs) Handles cmdCancel.Click
		img16.Dispose()
		imgSPACING.Dispose()
		Me.Close()
		Me.Dispose()
	End Sub

	Private Sub lnkHelp_LinkClicked(sender As System.Object, e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lnkHelp.LinkClicked
		frmHelp.sSelectPageByID = "help0005"
		frmHelp.ShowDialog(Me)
	End Sub

	Private Sub cmdAdd_Click(sender As System.Object, e As System.EventArgs) Handles cmdAdd.Click
		Dim lItem As New ListViewItem("", "_16___Server___New")
		lItem.EnsureVisible()
		lstServerList.Items.Add(lItem)
		lItem.BeginEdit()
	End Sub

	Private Sub lstServerList_AfterLabelEdit(sender As Object, e As System.Windows.Forms.LabelEditEventArgs) Handles lstServerList.AfterLabelEdit
		If ((e.Label = Nothing) OrElse (e.Label = String.Empty)) Then e.CancelEdit = True
		If ((e.CancelEdit = True) And (lstServerList.Items(e.Item).Text = vbNullString)) Then lstServerList.Items(e.Item).Remove()
		If e.CancelEdit = True Then Exit Sub

		Dim sServer As String = validateInput(e.Label)
		If (sServer IsNot Nothing) Then
			e.CancelEdit = True
			lstServerList.Items(e.Item).Text = sServer
			lstServerList.Items(e.Item).Name = sServer
			lstServerList.Items(e.Item).Selected = False
			lstServerList.Sort()
			Call cmdAdd_Click(sender, e)
		Else
			e.CancelEdit = True
			MessageBox.Show(Me, "Entries must start with a letter, or be a valid IP address.", APN, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			lstServerList.Items(e.Item).BeginEdit()
		End If
	End Sub

	Private Sub cmdDel_Click(sender As System.Object, e As System.EventArgs) Handles cmdDel.Click
		If (lstServerList.SelectedItems.Count = 0) Then Exit Sub
		Dim sResult As DialogResult = MessageBox.Show(Me, "Are you sure you want to delete the selected servers.?", APN, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
		If sResult = Windows.Forms.DialogResult.No Then Exit Sub
		lstServerList.BeginUpdate()
		For Each lItem As ListViewItem In lstServerList.SelectedItems
			lItem.Remove()
		Next
		lstServerList.EndUpdate()
	End Sub

	Private Function validateInput(ByVal sInput As String) As String
		If (sInput Is Nothing) Then Return Nothing
		sInput = sInput.Trim
		If (sInput = vbNullString) Then Return Nothing
		If (sInput.Contains(" ") = True) Then Return Nothing
		If (sInput.Contains(".") = True) Then
			' Contains a full-stop, is it an IP address...
			If (IPAddress.TryParse(sInput, System.Net.IPAddress.None) = True) Then
				If (chkResolveIP.Checked = True) Then
					Me.Cursor = Cursors.WaitCursor
					sInput = resolveIP(sInput)
					Me.Cursor = Cursors.Default
				End If
			End If
		Else
			If (Not sInput.Substring(0, 1).ToLower Like "[a-z]") Then Return Nothing
		End If
		If (lstServerList.Items.Find(sInput, False).Count > 0) Then Return Nothing
		Return sInput
	End Function

	Private Sub importList()
		Call disableControls(True)
		Dim sResult As DialogResult = MessageBox.Show(Me, "Select a plain text file containing the list of servers you want to import." & vbCrLf & _
					  "Make sure each server to be added is on its own line" & vbCrLf, _
					  APN, MessageBoxButtons.OKCancel, MessageBoxIcon.Information)
		If (sResult = Windows.Forms.DialogResult.Cancel) Then
			iCurrSelected = -1
			cmoIconComboBox.SelectedIndex = iCurrSelected
			Call disableControls(False)
			Exit Sub
		End If

		Using dlgOpenFile As New OpenFileDialog
			With dlgOpenFile
				.Title = "Select a plain text file to import..."
				.CheckFileExists = False
				.SupportMultiDottedExtensions = False
				.InitialDirectory = Application.StartupPath
				.FileName = vbNullString
				.Filter = "Text Files (*.txt)|*.txt|All Files|*.*"
			End With

			If (dlgOpenFile.ShowDialog(Me) = Windows.Forms.DialogResult.OK) Then
				Try
					Me.Cursor = Cursors.WaitCursor
					lstServerList.BeginUpdate()
					Using sr As StreamReader = New StreamReader(dlgOpenFile.FileName)
						Dim sLine As String
						Do
							sLine = validateInput(sr.ReadLine())
							If (sLine IsNot Nothing) Then Call addItem(sLine, False)
						Loop Until sr.EndOfStream
					End Using
				Catch ex As Exception
					Console.WriteLine("The file could not be read:" & vbCrLf & ex.Message)
				Finally
					lstServerList.EndUpdate()
					Me.Cursor = Cursors.Default
					Call disableControls(False)
				End Try
			Else
				Call disableControls(False)
			End If
		End Using

		iCurrSelected = -1
		cmoIconComboBox.SelectedIndex = iCurrSelected
		picIcon.Focus()
	End Sub

	Private Sub addItem(ByVal sItemText As String, ByVal bEditItem As Boolean)
		If (sItemText <> vbNullString) Then
			If (lstServerList.Items.Find(sItemText, False).Count > 0) Then Exit Sub
		End If

		Dim lItem As New ListViewItem(vbNullString)
		If (sItemText <> vbNullString) Then lItem.Text = IIf(m_UppercaseServerNames, sItemText.ToUpper, sItemText.ToLower).ToString

		lItem.ImageKey = "_16___Server___New"
		lItem.Name = sItemText

		lstServerList.Items.Add(lItem)
		If (bEditItem = True) Then lItem.BeginEdit()
	End Sub

	Private Sub cmdOK_Click(sender As System.Object, e As System.EventArgs) Handles cmdOK.Click
		Dim sFinalList As New List(Of String)
		iCurrSelected = -1

		For Each lItem As ListViewItem In lstServerList.Items
			sFinalList.Add(lItem.Text)
		Next

		' RESOURCE DUPLICATE CHECK...
		' This is done during the AddServer function

		' Settings are saved in calling procedure ('frmMain.onClick_addServer_')
		img16.Dispose()
		imgSPACING.Dispose()
		frmMain.m_AddedServers = sFinalList
		Me.Close()
		Me.Dispose()
	End Sub

	Private Sub cmoIconComboBox_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cmoIconComboBox.SelectedIndexChanged
		If (cmoIconComboBox.SelectedIndex = iCurrSelected) Then Exit Sub
		Select Case Trim(cmoIconComboBox.Text)
			Case ETF : Call importList()
			Case "Active Directory" : Call importAD(False)
		End Select
	End Sub

	Private Sub importAD(ByVal bRescan As Boolean)
		SplitContainer1.Panel2Collapsed = False
		lstServerList.Columns("list").Width = lstServerList.Width - SystemInformation.VerticalScrollBarWidth - 4
		Call disableControls(True)

		Me.Refresh()
		Application.DoEvents()

		' Check to see if we have already done a scan and just show that...
		If ((tvOUList.Nodes.Count = 0) Or (bRescan = True)) Then
			With tvOUList
				.Nodes.Clear()
				.Nodes.Add("WAIT", "Connecting To Active Directory...", "OUs_Opened", "OUs_Opened")
			End With

			chkResolveIP.Visible = False
			With proProgress
				.Value = 0
				.Visible = True
				.BringToFront()
			End With

			Application.DoEvents()
			lstHidden.Items.Clear()
			Dim sADPath As String = getADServers(lstHidden)		' Connect to AD
			If (sADPath Is Nothing) Then
				tvOUList.Nodes.Clear()
				proProgress.Visible = False
				Call disableControls(False)
				Call cmdCloseADResults_Click(Nothing, Nothing)
				Cursor = Cursors.Default
				Exit Sub
			End If

			If (lstHidden.Items.Count = 0) Then
				tvOUList.Nodes("WAIT").Text = "There was an error..."
				tvOUList.Nodes("WAIT").Nodes.Add("ERROR", sADPath.Replace("ERROR: ", vbNullString))
				tvOUList.Nodes("WAIT").Nodes("ERROR").ImageKey = "_16___Scan___Failed"
				tvOUList.Nodes("WAIT").Nodes("ERROR").SelectedImageKey = "_16___Scan___Failed"
				tvOUList.Nodes("WAIT").ExpandAll()
				Cursor = Cursors.Default
				proProgress.Visible = False
				cmdCancel.Enabled = True
				Exit Sub
			End If

			tvOUList.Nodes("WAIT").Text = "Building OU Tree..."
			Application.DoEvents()

			tvOUList.BeginUpdate()
			tvOUList.Nodes.Add("DOMAIN", sADPath.Replace("DC=", "").Replace(",", ".").ToLower, "OUs_Opened", "OUs_Opened")
			tvOUList.Nodes("DOMAIN").Nodes.Add("LOSTFOUND", "_Lost and Found", "All_Closed", "All_Closed")

			proProgress.Maximum = lstHidden.Items.Count
			For Each lItem As String In lstHidden.Items
				Dim sADOU As String = lItem.Substring(7, lItem.Length - sADPath.Length - 8)
				If (sADOU.Contains("CN=Computers")) Then sADOU = sADOU.Replace("CN=Computers", "OU=Computers")
				Dim sOU() As String = Split(sADOU, ",").AsEnumerable.Reverse.ToArray

				Call buildTreeView(sOU)
				If (proProgress.Value < proProgress.Maximum) Then proProgress.Value = proProgress.Value + 1
			Next
			tvOUList.Nodes("WAIT").Remove()
			tvOUList.Nodes("DOMAIN").Expand()
			If (tvOUList.Nodes("DOMAIN").Nodes("LOSTFOUND").Nodes.Count = 0) Then tvOUList.Nodes("DOMAIN").Nodes("LOSTFOUND").Remove()
			tvOUList.EndUpdate()
		End If

		proProgress.Visible = False
		Call disableControls(False)
		Cursor = Cursors.Default
		picIcon.Focus()
	End Sub

	Private Function getADServers(ByVal lstViewControl As ListBox) As String
		Dim sPath As String = vbNullString
		Dim dirE As DirectoryEntry = Nothing
		Cursor = Cursors.WaitCursor
		' Connect to AD
		Try
			sPath = "LDAP://RootDSE"
			dirE = New DirectoryEntry(sPath)
			sPath = dirE.Properties("defaultNamingContext")(0).ToString
		Catch ex As Exception
			Cursor = Cursors.Default
			Return "ERROR: " & ex.Message
		Finally
			dirE.Close()
			dirE.Dispose()
		End Try
		Application.DoEvents()

		' Search AD
		tvOUList.Nodes("WAIT").Text = "Scanning Domain, Please Wait..."
		tvOUList.Refresh()

		Try
			dirE = New DirectoryEntry("LDAP://" & sPath)
			Console.WriteLine("AD PAth: " & sPath)

			Dim queryResults As SearchResultCollection
			Using searcher As New DirectorySearcher(dirE)
				searcher.PropertiesToLoad.AddRange({"Name"})
				searcher.PageSize = 500
				searcher.ServerTimeLimit = New TimeSpan(0, 0, 120)
				searcher.Filter = "(&(objectCategory=computer)(operatingSystem=*Server*))"
				searcher.Tombstone = False
				searcher.CacheResults = False

				queryResults = searcher.FindAll()
			End Using

			lstViewControl.Items.Clear()
			lstViewControl.BeginUpdate()
			For Each sResult As SearchResult In queryResults
				lstViewControl.Items.Add(sResult.Path.ToString)
			Next
			lstViewControl.Sorted = True
			lstViewControl.EndUpdate()
			queryResults.Dispose()

		Catch ex As Exception
			Cursor = Cursors.Default
			Return "ERROR: " & ex.Message
		Finally
			dirE.Close()
			dirE.Dispose()
		End Try
		Return sPath
	End Function

	Private Sub buildTreeView(ByVal sOUPath() As String)
		Dim tFound() As TreeNode = Nothing
		Dim tNode As TreeNode = Nothing
		Dim tParent As TreeNode = Nothing
		Dim sParent As String = vbNullString
		Dim sPath As String = vbNullString

		For Each sItem As String In sOUPath
			If (sItem.StartsWith("CN=") = True) Then
				tFound = tvOUList.Nodes.Find(sPath, True)
				If (tFound.Count > 0) Then
					sItem = sItem.Replace("CN=", vbNullString)
					sItem = IIf(m_UppercaseServerNames, sItem.ToUpper, sItem.ToLower).ToString
					tFound(0).Nodes.Add(sItem, sItem, "_16___Server")	' Add server to current node's tag
				Else
					tvOUList.Nodes("LOSTFOUND").Nodes.Add(sItem, sItem, "_16___Server")
				End If
				Exit Sub
			Else
				Application.DoEvents()
			End If

			If (sItem.StartsWith("OU=") = True) Then
				sPath = sPath & sItem & ","
				tFound = tvOUList.Nodes.Find(sPath, True)
				If (tFound.Count = 0) Then
					' Create New Node...
					tNode = New TreeNode
					With tNode
						.Text = sItem.Replace("OU=", vbNullString)
						.Name = sPath
						.ImageKey = "OUs_Closed"
						.SelectedImageKey = "OUs_Closed"
						.Tag = Nothing
					End With

					If (sParent = vbNullString) Then
						tvOUList.Nodes("DOMAIN").Nodes.Add(tNode)
					Else
						tParent = tvOUList.Nodes.Find(sParent, True)(0)
						tParent.Nodes.Add(tNode)
					End If
					tvOUList.Sort()
					tNode = Nothing
				End If
				sParent = sPath
			End If
		Next
	End Sub

	Private Sub tvOUList_BeforeCollapse(sender As Object, e As System.Windows.Forms.TreeViewCancelEventArgs) Handles tvOUList.BeforeCollapse
		If (e.Node.Name = "DOMAIN") Then e.Cancel = True
	End Sub

	Private Sub tvOUList_MouseDoubleClick(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles tvOUList.MouseDoubleClick
		Dim tvNode As TreeNode = tvOUList.GetNodeAt(e.X, e.Y)
		If (tvOUList Is Nothing) Then Exit Sub
		tvOUList.SelectedNode = tvNode
		If (tvNode.ImageKey = "_16___Server") Then addItem(tvNode.Text, False)
	End Sub

	Private Sub tvOUList_MouseUp(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles tvOUList.MouseUp
		If (e.Button <> Windows.Forms.MouseButtons.Right) Then Exit Sub
		Dim tvNode As TreeNode = tvOUList.GetNodeAt(e.X, e.Y)
		If (tvNode Is Nothing) Then Exit Sub
		If (tvNode.Name = "WAIT") Then Exit Sub
		If (tvNode.Name = "DOMAIN") Then Exit Sub
		tvOUList.SelectedNode = tvNode

		mnuRightClick = New ContextMenuStrip
		With mnuRightClick.Items
			.Clear()
			If (tvNode.ImageKey = "_16___Server") Then
				.Add("Add Server", My.Resources._16___Add.ToBitmap, AddressOf onClick_AddServer)
			Else
				.Add("Add All Child Items", My.Resources._16___Add.ToBitmap, AddressOf onClick_AddFolder)
			End If
		End With
		mnuRightClick.Show(tvOUList, e.Location)
	End Sub

	Private Sub onClick_AddServer(sender As System.Object, e As System.EventArgs)
		Call addItem(tvOUList.SelectedNode.Text, True)
		mnuRightClick.Dispose()
	End Sub

	Private Sub onClick_AddFolder(sender As System.Object, e As System.EventArgs)
		lstAddOU.Items.Clear()
		mnuRightClick.Dispose()
		For Each nNode As TreeNode In tvOUList.SelectedNode.Nodes
			Call FindNodes(nNode)
		Next
		Dim iCnt As Integer = lstAddOU.Items.Count

		If (iCnt = 0) Then
			MessageBox.Show(Me, "No servers were found in this OU and its children.", APN, MessageBoxButtons.OK, MessageBoxIcon.Information)
			Exit Sub
		End If

		Dim sMessage As String = "There " & IIf(iCnt = 1, "is ", "are ").ToString & iCnt & " server" & IIf(iCnt = 1, "", "s").ToString & " in this OU and its children."
		sMessage = sMessage & vbCrLf & "Do you want to add " & IIf(iCnt = 1, "it", "them").ToString & " to the server list.?"
		Dim iResult As DialogResult = MessageBox.Show(Me, sMessage, APN, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
		If (iResult = Windows.Forms.DialogResult.No) Then Exit Sub

		lstServerList.BeginUpdate()
		For Each sItem As String In lstAddOU.Items
			Call addItem(sItem, False)
		Next
		lstServerList.EndUpdate()
	End Sub

	Private Sub FindNodes(ByVal nNode As TreeNode)
		If (nNode.ImageKey = "_16___Server") Then lstAddOU.Items.Add(nNode.Text)
		For Each aNode As TreeNode In nNode.Nodes
			Call FindNodes(aNode)
		Next
	End Sub

	Private Sub cmdCloseADResults_Click(sender As System.Object, e As System.EventArgs) Handles cmdCloseADResults.Click
		iCurrSelected = -1
		cmoIconComboBox.SelectedIndex = iCurrSelected
		SplitContainer1.Panel2Collapsed = True
		lstServerList.Columns("list").Width = lstServerList.Width - SystemInformation.VerticalScrollBarWidth - 4

		cmdCloseADResults.Visible = False
		cmdRescanAD.Visible = False
		cmdSearchAD.Visible = False
		cmoIconComboBox.Visible = True
		lblLabel.Visible = True
		chkResolveIP.Visible = True
	End Sub

	Private Sub cmdRescanAD_Click(sender As System.Object, e As System.EventArgs) Handles cmdRescanAD.Click
		Call importAD(True)
	End Sub

	Private Sub disableControls(ByRef bDisable As Boolean)
		If (cmoIconComboBox.Text = "Active Directory") Then
			lblLabel.Visible = bDisable
			chkResolveIP.Visible = bDisable
			cmoIconComboBox.Visible = bDisable

			cmdCloseADResults.Visible = Not bDisable
			cmdRescanAD.Visible = Not bDisable
			cmdSearchAD.Visible = Not bDisable
		End If

		cmoIconComboBox.BackColor = CType(IIf(bDisable, SystemColors.ControlLight, SystemColors.Window), Color)
		cmoIconComboBox.Enabled = Not bDisable
		cmdCancel.Enabled = Not bDisable
		cmdAdd.Enabled = Not bDisable
		cmdDel.Enabled = Not bDisable
		cmdOK.Enabled = Not bDisable
	End Sub

	Private Sub cmdSearchAD_Click(sender As System.Object, e As System.EventArgs) Handles cmdSearchAD.Click
		Me.Cursor = Cursors.WaitCursor

		Dim b As New Button
		b.Text = "Add All"
		b.Name = "btnCustomButton"
		b.Size = New Size(125, 25)
		b.Location = New Point(12, 74)
		AddHandler b.Click, AddressOf Me.addCustomButton

		frmFindServer.Controls.Add(b)
		frmFindServer.cTreeView = tvOUList
		frmFindServer.Show(Me)

		Me.Cursor = Cursors.Default
	End Sub

	Public Sub addCustomButton(sender As System.Object, e As System.EventArgs)
		If (frmFindServer.lSearchResults Is Nothing) Then Exit Sub

		For Each lResult As TreeNode In frmFindServer.lSearchResults
			addItem(lResult.Text, False)
		Next

		Dim b As Button = CType(frmFindServer.Controls("btnCustomButton"), Button)
		If (b IsNot Nothing) Then
			RemoveHandler b.Click, AddressOf addCustomButton
			b.Dispose()
		End If
		Call frmFindServer.cmdCancel_Click(sender, e)
		frmFindServer.Dispose()
	End Sub

	Private Sub lstServerList_MouseUp(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles lstServerList.MouseUp
		If (e.Button <> Windows.Forms.MouseButtons.Right) Then Exit Sub

		Dim bAnyToResolve As Boolean = False
		For i As Integer = 0 To lstServerList.SelectedItems.Count - 1
			If (IPAddress.TryParse(lstServerList.SelectedItems(i).Text, System.Net.IPAddress.None) = True) Then
				bAnyToResolve = True
				Exit For
			End If
		Next

		If (bAnyToResolve = True) Then
			mnuRightClick = New ContextMenuStrip
			With mnuRightClick.Items
				.Clear()
				.Add("Resolve IP Address" & IIf(lstServerList.SelectedItems.Count = 1, "", "es").ToString, My.Resources._16___Edit.ToBitmap, AddressOf onClick_ResolveIP)
			End With
			mnuRightClick.Show(lstServerList, e.Location)
		End If
	End Sub

	Private Sub onClick_ResolveIP(sender As System.Object, e As System.EventArgs)
		Me.Cursor = Cursors.WaitCursor
		For i As Integer = 0 To lstServerList.SelectedItems.Count - 1
			lstServerList.SelectedItems(i).Text = resolveIP(lstServerList.SelectedItems(i).Text)
		Next i
		Me.Cursor = Cursors.Default
	End Sub
End Class