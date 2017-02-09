Option Explicit On
Imports System.Text
Imports System.Runtime.InteropServices
Imports System.Xml
Imports Microsoft.Win32
Imports System.Net

Public Class frmMain
	Private Enum dragDropType
		server
		group
	End Enum

	Public sysFont As New Font(SystemFonts.MessageBoxFont.Name, SystemFonts.MessageBoxFont.SizeInPoints)
	Public sysFontBold As New Font(SystemFonts.MessageBoxFont.Name, SystemFonts.MessageBoxFont.SizeInPoints, FontStyle.Bold)
	Public sysFontHelp As New Font(SystemFonts.MessageBoxFont.Name, SystemFonts.MessageBoxFont.SizeInPoints - 1)
	Public sysFontTitle As New Font(SystemFonts.MessageBoxFont.Name, SystemFonts.MessageBoxFont.SizeInPoints + 2)
	Public sysFontSplash As New Font(SystemFonts.MessageBoxFont.Name, SystemFonts.MessageBoxFont.SizeInPoints + 9)

	Private img16 As New ImageList
	Private img48 As New ImageList
	Private img64 As New ImageList
	Public imgSPACING As New ImageList

	Private WithEvents mContextTree As New ContextMenuStrip
	Private WithEvents mContextResource As New ContextMenuStrip

	Private lvwCS As ListViewColumnSorter
	Private Const Ready As String = "Ready"

	Public m_AddedServers As List(Of String)		' LIST  : Server Name/IP Address

	Public m_AddFileScan As String					' FORMAT: Path\Filename | True/False | Date,a/b | Version
	Public m_AddFreeSpace As String					' FORMAT: Critical % | Warning % | ScanOption
	Public m_AddEventLog As String					' FORMAT: Application | Critical, Error, Warning, Excluding: 1,3,5-99
	Public m_AddHotfixes As List(Of String)			' LIST  : KBnumbers | Checking State
	Public m_AddRegistry As List(Of String)			' LIST  : RegKey\RegName | RegData | RegType | MissingResult
	Public m_AddServices As List(Of String)			' LIST  : Service Names | Service State
	Public m_AddWMIQuery As String					' FORMAT: Full WMI Query with =/!=/</> and result check

	Private m_dragDropType As dragDropType
	Private m_CurrentNode As TreeNode				' Will always be a GROUP node
	Public bLoadingXML As Boolean = False
	Public sLostAndFound As String = vbNullString
	Public bMultiSelectDone As Boolean = False

	Private Delegate Sub SortTreeDelegate(SelectedNode As TreeNode)

	Public iColumnWidths() As Integer = {0, 125, 225, 100}	 ' 0, Type, Checking, Defined
	Private Const lNoRes1 As String = "No resources at this level, or above," & vbCrLf & "right click here to add some to the current group"
	Private Const lNoRes2 As String = "Add a group or two before adding resources," & vbCrLf & "right click on the left to add groups"
	Private Const lNoGroups As String = "Right click here to add" & vbCrLf & "groups and servers."

	Private Sub frmMain_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
		Me.Icon = My.Resources._16___frmMainIcon
		For Each c As Control In Controls
			c.Font = sysFont
		Next
		lblSplashTitle.Font = sysFontSplash

		KeyPreview = True		' Required for CTRL+F/F3/F9 key presses
		With img16
			.ImageSize = New Size(16, 16)
			.ColorDepth = ColorDepth.Depth32Bit
			With .Images
				.Clear()
				.Add("LOST_AND_FOUND", My.Resources._16___Eventlog___Warning)	' MUST BE FIRST TO INDICATE AN ICON ERROR
				' ------------------------------
				.Add("_16___Server", My.Resources._16___Server)
				.Add("_16___Server_With_Properties", My.Resources._16___Server_With_Properties)
				.Add("_16___Resource___Services", My.Resources._16___Resource___Services)
				.Add("_16___Resource___Hotfix", My.Resources._16___Resource___Hotfix)
				.Add("_16___Resource___Eventlog", My.Resources._16___Resource___Eventlog)
				.Add("_16___Resource___Registry", My.Resources._16___Resource___Registry)
				.Add("_16___Resource___File", My.Resources._16___Resource___File)
				.Add("_16___Resource___Drive", My.Resources._16___Resource___Drive)
				.Add("_16___Resource___WMIQuery", My.Resources._16___Resource___WMIQuery)
			End With
		End With

		With imgSPACING
			.ImageSize = New Size(iListViewIconWidth, iListViewLineHeight)
			.ColorDepth = ColorDepth.Depth32Bit
			For i As Integer = 0 To img16.Images.Count - 1
				.Images.Add(img16.Images.Keys(i), ResizeImage(img16.Images(i), .ImageSize, True))
			Next
		End With

		With img48
			.ImageSize = New Size(48, 48)
			.ColorDepth = ColorDepth.Depth32Bit
			With .Images
				.Clear()
				.Add("_16___Resource___Services", My.Resources._48___Services)											' \
				.Add("_16___Resource___Hotfix", My.Resources._48___Hotfix)												'  |
				.Add("_16___Resource___Eventlog", My.Resources._48___Eventlog)											'  |  These NAMES must match the names
				.Add("_16___Resource___Registry", My.Resources._48___Registry)											'  |  of the 16x16 icons listed above.
				.Add("_16___Resource___File", My.Resources._48___File)													'  |
				.Add("_16___Resource___Drive", My.Resources._48___Drive_Local___OK)										'  |
				.Add("_16___Resource___WMIQuery", My.Resources._48___WMIQuery)											' /
			End With
		End With

		With img64
			.ImageSize = New Size(64, 64)
			.ColorDepth = ColorDepth.Depth32Bit
			With .Images
				.Clear()
				.Add("_64___Task___New", My.Resources._64___Task___New)
				.Add("_64___Task___Open", My.Resources._64___Task___Open)
				.Add("_64___Task___Help", My.Resources._64___Task___Help)
			End With
		End With

		With conSplitContainer
			.Panel1MinSize = 150
			.Panel2MinSize = 300
			.SplitterDistance = 200
			.FixedPanel = FixedPanel.Panel1
			.Dock = DockStyle.Fill
		End With

		NetworkAvailabilityToolStripStatus.Visible = Not m_NetworkAvailability
		NetworkAvailabilityToolStripStatus.DisplayStyle = ToolStripItemDisplayStyle.Image
		NetworkAvailabilityToolStripStatus.Image = My.Resources.Network_ico.ToBitmap

		ToolStripStatusReadOnly.Visible = False
		ToolStripStatusDiv0.Visible = False

		lvwCS = New ListViewColumnSorter()
		lvwCS.Order = SortOrder.Ascending
		lvwCS.SortColumn = 0
		lvwCS.TagSort = False

		With lstResources
			.Groups.Clear()
			.Items.Clear()
			.FullRowSelect = True
			.SmallImageList = imgSPACING
			.LargeImageList = img48
			.TileSize = New Size(250, 50)
			.View = System.Windows.Forms.View.Details
			.ShowGroups = True
			.HeaderStyle = ColumnHeaderStyle.Nonclickable
			.ListViewItemSorter = lvwCS
			With .Columns
				.Clear()
				.Add("name", "Resource Name", lstResources.Width - iColumnWidths.Sum - SystemInformation.VerticalScrollBarWidth, HorizontalAlignment.Left, -1)
				.Add("type", "Type", iColumnWidths(1), HorizontalAlignment.Left, -1)
				.Add("check", "Checking", iColumnWidths(2), HorizontalAlignment.Left, -1)
				.Add("defined", "Defined", iColumnWidths(3), HorizontalAlignment.Left, -1)
			End With
		End With

		conSplitLeft.Panel2Collapsed = True
		conSplitLeft.IsSplitterFixed = True
		conSplitLeft.SplitterDistance = 425
		conSplitLeft.SplitterWidth = 1

		With ToolStripMenuDescriptionExpandCollapse
			.Image = My.Resources._16___Description___Expand.ToBitmap
			.DisplayStyle = ToolStripItemDisplayStyle.Image
			.Text = "Show/Hide Group Description Window"
			.Tag = "Hide"
		End With
		With ToolStripMenuEditDescription
			.Image = My.Resources._16___Properties.ToBitmap
			.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText
			.TextImageRelation = TextImageRelation.ImageBeforeText
			.Text = "Properties"
		End With
		mnuProperties.Enabled = False
		Call ToolStripMenuDescriptionExpandCollapse_Click(Nothing, Nothing)

		txtDescription.Location = New Point(2, 2)
		txtDescription.Size = New Size(tvwDescription.Width - 4, tvwDescription.Height - 4)
		txtDescription.Text = vbNullString

		Call clearTreeAndResourceList()

		lblNoResourcesAtThisLevel.Visible = False
		lblNoGroupsAtThisLevel.Visible = False
		conSplitContainer.IsSplitterFixed = True

		CloseToolStripMenuItem.Enabled = False
		SearchToolStripMenuItem.Visible = False
		OptionsToolStripMenuItem.Visible = False

		ExpandAllToolStripMenuItem.Visible = False
		ExpandAllToolStripMenuItem.DisplayStyle = ToolStripItemDisplayStyle.Image
		ExpandAllToolStripMenuItem.Image = My.Resources.Expand_ico.ToBitmap

		CollapseAllToolStripMenuItem.Visible = False
		CollapseAllToolStripMenuItem.DisplayStyle = ToolStripItemDisplayStyle.Image
		CollapseAllToolStripMenuItem.Image = My.Resources.Collapse_ico.ToBitmap

		ShowFullPathToolStripMenuItem.Visible = False
		ShowFullPathToolStripMenuItem.DisplayStyle = ToolStripItemDisplayStyle.Image
		ShowFullPathToolStripMenuItem.Image = My.Resources.FullPath_Hide_ico.ToBitmap

		Call createSplashFrame()

		If (bUseVisualStyles = True) Then
			Call SetWindowTheme(lstResources.Handle, "Explorer", Nothing)
			Call SetWindowTheme(tvwServerList.Handle, "Explorer", Nothing)
			Call SetWindowTheme(lstSplashOptions.Handle, "Explorer", Nothing)

			AddHandler ExpandAllToolStripMenuItem.MouseEnter, AddressOf onClick_tssInfo_MouseEnter
			AddHandler ExpandAllToolStripMenuItem.MouseLeave, AddressOf onClick_tssInfo_MouseLeave
			AddHandler CollapseAllToolStripMenuItem.MouseEnter, AddressOf onClick_tssInfo_MouseEnter
			AddHandler CollapseAllToolStripMenuItem.MouseLeave, AddressOf onClick_tssInfo_MouseLeave
		End If
		AddHandler ShowFullPathToolStripMenuItem.MouseEnter, AddressOf onClick_tssInfo_MouseEnter
		AddHandler ShowFullPathToolStripMenuItem.MouseLeave, AddressOf onClick_tssInfo_MouseLeave

		Call SendMessage(lstResources.Handle, WM_ChangeUIState, CInt(MakeLong(UIS_HideRectangle, UISF_FocusRectangle)), IntPtr.Zero)
		Call SendMessage(tvwServerList.Handle, WM_ChangeUIState, CInt(MakeLong(UIS_HideRectangle, UISF_FocusRectangle)), IntPtr.Zero)
		Call SendMessage(lstSplashOptions.Handle, WM_ChangeUIState, CInt(MakeLong(UIS_HideRectangle, UISF_FocusRectangle)), IntPtr.Zero)

		Me.Text = Application.ProductName
		lblVersion.Text = "v" & Application.ProductVersion
		bIsAdminMode = My.User.IsInRole(ApplicationServices.BuiltInRole.Administrator)
		ToolStripStatusAdmin.Text = "Admin Options " & IIf(bIsAdminMode, "Enabled", "Disabled").ToString
		AssociateSSCFilesToolStripMenuItem.Enabled = bIsAdminMode

		With picHeaderBar
			.Anchor = AnchorStyles.Left Or AnchorStyles.Right Or AnchorStyles.Top
			.BackColor = lstResources.BackColor
			If (bUseVisualStyles = True) Then
				.Location = New Point(1, 1)
				.Width = lstHeaderBar.Width - 2
			Else
				.Location = New Point(2, 2)
				.Width = lstHeaderBar.Width - 4
			End If
		End With

		ShowFullPathToolStripMenuItem.Checked = False
		Call ShowHideFullPathHeader()

		' Preload DragDrop Form...
		frm_DragDrop.Show()
		frm_DragDrop.Hide()

		If ((bCommandLine = False) AndAlso (sConfigFile <> vbNullString)) Then Call openSelectedFile(sConfigFile)
		tssInfo.Text = "Welcome To " & Application.ProductName
	End Sub

	Private Sub frmMain_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
		sysFont.Dispose()
		sysFontBold.Dispose()
		sysFontHelp.Dispose()
		sysFontTitle.Dispose()
		img16.Dispose()
		img48.Dispose()
		img64.Dispose()
		imgSPACING.Dispose()
		xmlDoc = Nothing
	End Sub

	Private Sub frmMain_DragDrop(sender As Object, e As System.Windows.Forms.DragEventArgs) Handles Me.DragDrop
		Dim sDroppedFiles() As String = CType(e.Data.GetData(DataFormats.FileDrop), String())
		If (sDroppedFiles.Count > 1) Then
			MessageBox.Show(Me, "More than one file was dropped." & vbCrLf & "Only the first one will be used.", APN, MessageBoxButtons.OK, MessageBoxIcon.Information)
		End If
		Call openSelectedFile(sDroppedFiles(0))
	End Sub
	Private Sub frmMain_DragEnter(sender As Object, e As System.Windows.Forms.DragEventArgs) Handles Me.DragEnter
		e.Effect = DragDropEffects.None
		If (e.Data.GetDataPresent(DataFormats.FileDrop) = True) Then e.Effect = DragDropEffects.Copy
	End Sub

	Private Sub frmMain_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
		If (e.KeyCode = Keys.F9) Then
			' These are the same checks in 'tvwServerList_MouseUp'...
			If (tvwServerList.SelectedNode.Tag.ToString = "GROUP") Then
				If (tvwServerList.SelectedNode.ImageKey <> "LOST_AND_FOUND") Then
					If (m_NetworkAvailability = True) Then
						Dim iServerCount As Integer = 0
						Dim xServers As List(Of XmlNode) = xml_getServerList_FromGroup(tvwServerList.SelectedNode.Name)
						If (xServers IsNot Nothing) Then iServerCount = xServers.Count
						If (iServerCount > 0) Then
							' Finally, do the scan...
							Call onClick_scanGroup(Nothing, Nothing)
						End If
					End If
				End If
			End If
			e.Handled = True

			'ElseIf (((e.KeyCode = Keys.F) AndAlso (e.Control = True)) Or (e.KeyCode = Keys.F3)) Then
			'	' SEARCH/FIND
			'	If (SearchToolStripMenuItem.Visible = True) Then
			'		Call FindServerToolStripMenuItem_Click(sender, e)
			'		e.Handled = True
			'	Else
			'		e.Handled = False
			'	End If

		Else
			e.Handled = False
		End If
	End Sub

	Public Sub loadIconSet()
		With img16.Images
			' Clear current icon sets first...
			For j As Integer = 1 To iGroupIconSetCount
				If (.ContainsKey("_16___Group___" & j & "___Yellow") = True) Then
					For i As Integer = 0 To iIconColourCount - 1
						.RemoveByKey("_16___Group___" & getGroupColour(i, True, j.ToString))
					Next
				End If
			Next

			' Add selected icon set...
			Dim sCol As String
			For i As Integer = 0 To iIconColourCount - 1
				sCol = getGroupColour(i, True)
				.Add("_16___Group___" & sCol, CType(My.Resources.ResourceManager.GetObject("_16___Group___" & sCol), Icon))
			Next
		End With
	End Sub

	Public Sub readSettings()
		If (xmlDoc Is Nothing) Then Exit Sub
		Dim sSettings As List(Of String) = xml_readSettings()
		If (sSettings Is Nothing) Then Exit Sub

		m_GroupView = sSettings.Item(0)
		m_ResourceView = sSettings.Item(1)
		m_UseColouredGroups = CBool(sSettings.Item(2))
		m_UppercaseServerNames = CBool(sSettings.Item(3))
		m_IconGroup = sSettings.Item(4)
		m_ShowGroupCounts = CBool(sSettings.Item(5))
		m_PingServersBeforeScan = CBool(sSettings.Item(6))

		Call loadIconSet()
		Call tvwServerList_NodeMouseClick(Nothing, Nothing)

		Select Case m_GroupView
			Case "none" : lstResources.ShowGroups = False
			Case "parent" : Call doGroupListView(lstResources, "Defined", True, m_ShowGroupCounts)
			Case Else : Call doGroupListView(lstResources, "Type", False, m_ShowGroupCounts)
		End Select

		Select Case m_ResourceView
			Case "tile" : lstResources.View = View.Tile
			Case Else : lstResources.View = View.Details
		End Select
	End Sub

	Public Sub clearTreeAndResourceList()
		With tvwServerList
			.ImageList = img16
			.ItemHeight = 18
			.Nodes.Clear()
			.AllowDrop = True
			.ShowLines = False
			.ShowRootLines = True
			.FullRowSelect = True
			.ShowNodeToolTips = True
		End With
		With lstResources
			.Items.Clear()
			.Groups.Clear()
		End With
		Call drawFullPathHeader(Nothing)
	End Sub

	Private Sub createSplashFrame()
		With panSplashPanel
			.Dock = DockStyle.Fill
			.BackColor = SystemColors.Window
			.BorderStyle = BorderStyle.FixedSingle
		End With

		With lstSplashOptions
			.BeginUpdate()
			.Visible = True
			.Groups.Clear()
			.Items.Clear()
			.Columns.Clear()
			.FullRowSelect = False
			.LargeImageList = img64
			.MultiSelect = False
			.View = System.Windows.Forms.View.Tile
			.TileSize = New Size(.Width - 5, 95)
			With .Columns
				.Add("A")	' Name
				.Add("B")	' Line 1
				.Add("C")	' Line 2
			End With

			.BackColor = SystemColors.Window

			Dim lItem As ListViewItem
			With .Items
				lItem = New ListViewItem("NEW")
				lItem.SubItems.Add("Create a new configuration file and")
				lItem.SubItems.Add("start taking control of your estate")
				lItem.ImageKey = "_64___Task___New"
				.Add(lItem)

				lItem = New ListViewItem("OPEN")
				lItem.SubItems.Add("Open an existing configuration file and")
				lItem.SubItems.Add("continue taking control of your estate")
				lItem.ImageKey = "_64___Task___Open"
				.Add(lItem)

				lItem = New ListViewItem("HELP")
				lItem.SubItems.Add("Show help for this application")
				lItem.SubItems.Add("using the extensive help system")
				lItem.ImageKey = "_64___Task___Help"
				.Add(lItem)
			End With
			.EndUpdate()
		End With

		picSplashLogo.Image = My.Resources._48___MainIcon.ToBitmap
		lblSplashSubTitle.Width = lblSplashTitle.Width

		lblSplash1.Text = "Eventlog Scans"
		lblSplash2.Text = "File Checking"
		lblSplash3.Text = "Free Space Thresholds"
		lblSplash4.Text = "Hotfix Patch Checks"
		lblSplash5.Text = "Registry Scanning"
		lblSplash6.Text = "WMI Queries"
		lblSplash7.Text = "Windows Services Checks"

		picSplashIcon1.Image = My.Resources._16___Resource___Eventlog.ToBitmap
		picSplashIcon2.Image = My.Resources._16___Resource___File.ToBitmap
		picSplashIcon3.Image = My.Resources._16___Resource___Drive.ToBitmap
		picSplashIcon4.Image = My.Resources._16___Resource___Hotfix.ToBitmap
		picSplashIcon5.Image = My.Resources._16___Resource___Registry.ToBitmap
		picSplashIcon6.Image = My.Resources._16___Resource___WMIQuery.ToBitmap
		picSplashIcon7.Image = My.Resources._16___Resource___Services.ToBitmap
	End Sub

	Private Sub lblNoGroupsAtThisLevel_MouseUp(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles lblNoGroupsAtThisLevel.MouseUp
		Dim ee As New MouseEventArgs(e.Button, e.Clicks, e.X + lblNoGroupsAtThisLevel.Left, e.Y + lblNoGroupsAtThisLevel.Top, e.Delta)
		Call tvwServerList_MouseUp(sender, ee)
	End Sub

	Private Sub tvwServerList_BeforeCollapse(sender As Object, e As System.Windows.Forms.TreeViewCancelEventArgs) Handles tvwServerList.BeforeCollapse
		If (tssInfo.Text = Ready) Then tvwServerList.SelectedNode = e.Node
	End Sub

	Private Sub tvwServerList_BeforeExpand(sender As Object, e As System.Windows.Forms.TreeViewCancelEventArgs) Handles tvwServerList.BeforeExpand
		If (tssInfo.Text = Ready) Then tvwServerList.SelectedNode = e.Node
	End Sub

	Private Sub tvwServerList_MouseDoubleClick(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles tvwServerList.MouseDoubleClick
		Dim tvNode As TreeNode = tvwServerList.GetNodeAt(e.X, e.Y)
		If tvNode Is Nothing Then Exit Sub
		If (tvNode.ImageKey.StartsWith("_16___Group___") = True) Then Exit Sub
		Call onClick_serverProperties(sender, e)
	End Sub

	Private Sub tvwServerList_MouseUp(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles tvwServerList.MouseUp
		tvwServerList.Capture = False
		If (xmlDoc Is Nothing) Then Exit Sub
		If (e.Button <> Windows.Forms.MouseButtons.Right) Then Exit Sub
		Dim tvNode As TreeNode = tvwServerList.GetNodeAt(e.X, e.Y)
		'Do not check for (tvNode Is Nothing)

		Dim bSubGroupFound As Boolean = False
		Dim bSubServerFound As Boolean = False

		If (tvNode IsNot Nothing) Then tvwServerList.SelectedNode = tvNode
		Dim iTSMI As ToolStripMenuItem
		With mContextTree.Items
			.Clear()
			Application.DoEvents()

			If (tvNode Is Nothing) Then
				tvwServerList.SelectedNode = Nothing
				' Add Group
				.Add("Add Group", CType(My.Resources.ResourceManager.GetObject("_16___Group___" & m_IconGroup & "____New"), Icon).ToBitmap, AddressOf onClick_addGroup)
				.Item(.Count - 1).Enabled = Not bReadOnlyMode
			Else
				Select Case tvNode.Tag.ToString
					Case "GROUP"
						If (tvNode.ImageKey <> "LOST_AND_FOUND") Then

							' Add Resource       >
							' --------------------
							' Add Group
							' Add Server
							' --------------------
							' Remove Group
							' Remove Server
							' Scan Group
							' --------------------
							' Properties

							Dim mResourceSubMenu As ToolStripMenuItem = getToolStripMenuItem()		' "Add Resource" submenu items
							.Add(mResourceSubMenu)
							mResourceSubMenu.Enabled = Not bReadOnlyMode
							.Add("-")

							' Do sub-items exist, and what are they...
							Dim bDepth As Boolean = checkGroupDepth(tvNode.FullPath, tvwServerList.PathSeparator, vbNull)
							If (tvNode.Nodes.Count > 0) Then
								For Each chNode As TreeNode In tvNode.Nodes
									If (chNode.Tag.ToString = "GROUP") Then bSubGroupFound = True
									If (chNode.Tag.ToString = "SERVER") Then bSubServerFound = True
									If (chNode.Tag.ToString = "SERVWP") Then bSubServerFound = True
								Next
								If (bSubGroupFound = True) Then
									If (bDepth = True) Then
										.Add("Add Group", CType(My.Resources.ResourceManager.GetObject("_16___Group___" & m_IconGroup & "____New"), Icon).ToBitmap, AddressOf onClick_addGroup)
										.Item(.Count - 1).Enabled = Not bReadOnlyMode
									End If
								Else
									.Add("Add Server(s)...", My.Resources._16___Server___New.ToBitmap, AddressOf onClick_addServer)
									.Item(.Count - 1).Enabled = Not bReadOnlyMode
								End If
								.Add("-")
							Else
								If (bDepth = True) Then
									.Add("Add Group", CType(My.Resources.ResourceManager.GetObject("_16___Group___" & m_IconGroup & "____New"), Icon).ToBitmap, AddressOf onClick_addGroup)
									.Item(.Count - 1).Enabled = Not bReadOnlyMode
								End If
								.Add("Add Server(s)...", My.Resources._16___Server___New.ToBitmap, AddressOf onClick_addServer)
								.Item(.Count - 1).Enabled = Not bReadOnlyMode
								.Add("-")
							End If
							.Add("Rename Group", My.Resources._16___Edit.ToBitmap, AddressOf onClick_renameGroup)
							.Item(.Count - 1).Enabled = Not bReadOnlyMode

							.Add("Delete Group", My.Resources._16___Remove.ToBitmap, AddressOf onClick_removeGroup)
							.Item(.Count - 1).Enabled = Not bReadOnlyMode

							iTSMI = New ToolStripMenuItem("Scan Group...", My.Resources._16___Scan___Scanning.ToBitmap, AddressOf onClick_scanGroup)
							iTSMI.ShortcutKeys = Keys.F9
							.Add(iTSMI)
							.Item(.Count - 1).Enabled = m_NetworkAvailability
							.Item(.Count - 1).Font = New Font(.Item(.Count - 1).Font, FontStyle.Bold)

							Dim iServerCount As Integer = 0
							Dim xServers As List(Of XmlNode) = xml_getServerList_FromGroup(tvNode.Name)
							If (xServers IsNot Nothing) Then iServerCount = xServers.Count
							If (iServerCount = 0) Then .Item(.Count - 1).Enabled = False

							.Add("-")
							.Add("Properties...", My.Resources._16___Properties.ToBitmap, AddressOf onClick_groupProperties)
						Else
							If (tvNode.ImageKey = "LOST_AND_FOUND") Then
								' Lost And Found Group - Remove Only
								.Add("Delete Group", My.Resources._16___Remove.ToBitmap, AddressOf onClick_removeGroup)
								.Item(.Count - 1).Enabled = Not bReadOnlyMode
							End If
						End If

					Case "SERVER", "SERVWP"
						If ((tvNode.Text.Contains(".") = True) AndAlso (IPAddress.TryParse(tvNode.Text, System.Net.IPAddress.None) = True)) Then
							.Add("Resolve Hostname", My.Resources._16___Edit.ToBitmap, AddressOf onClick_resolveHostName)
							.Item(.Count - 1).Enabled = Not bReadOnlyMode
						End If

						.Add("Delete Server", My.Resources._16___Remove.ToBitmap, AddressOf onClick_removeServer)
						.Item(.Count - 1).Enabled = Not bReadOnlyMode
						.Add("Move/Copy...", My.Resources._16___Copy.ToBitmap, AddressOf onClick_movecopyServers)
						.Item(.Count - 1).Enabled = Not bReadOnlyMode
						.Add("-")
						.Add("Properties...", My.Resources._16___Properties.ToBitmap, AddressOf onClick_serverProperties)

					Case Else
						MessageBox.Show(Me, "ERROR: " & tvNode.Tag.ToString, APN, MessageBoxButtons.OK, MessageBoxIcon.Error)
				End Select
			End If
		End With

		Try
			For Each mItm As Object In mContextTree.Items
				If (TypeOf mItm Is ToolStripMenuItem) Then
					AddHandler CType(mItm, ToolStripMenuItem).MouseEnter, AddressOf onClick_tssInfo_MouseEnter
				End If
			Next
		Catch ex As Exception
		End Try

		mContextTree.Show(tvwServerList, New Point(e.X, e.Y))
	End Sub

	Private Function getToolStripMenuItem() As ToolStripMenuItem
		' Add Resource  >  Eventlog Scan
		'				   File Check
		'                  Free Space Threshold *
		'                  Hotfix Patch Check
		'				   Registry Scan
		'				   Windows Services
		'				   WMI Query
		Dim mTSMI As New ToolStripMenuItem("Add Resource", My.Resources._16___Add.ToBitmap)
		mTSMI.DropDownItems.Add("Eventlog Scan", My.Resources._16___Resource___Eventlog.ToBitmap, AddressOf onClick_addResource_Eventlog)
		mTSMI.DropDownItems.Add("File Check", My.Resources._16___Resource___File.ToBitmap, AddressOf onClick_addResource_FileCheck)

		' If one already exists, don't allow it again...
		mTSMI.DropDownItems.Add("Free Space Threshold", My.Resources._16___Resource___Drive.ToBitmap, AddressOf onClick_addResource_FreeSpace)

		Try
			If (tvwServerList.SelectedNode IsNot Nothing) Then
				If (xml_checkResourceExists(getGroupNode(tvwServerList.SelectedNode).Name, "Free Space Threshold", "Free Space Threshold") = True) Then
					mTSMI.DropDownItems(mTSMI.DropDownItems.Count - 1).Enabled = False
				End If
			End If
		Catch
		End Try

		mTSMI.DropDownItems.Add("Hotfix Patch Check", My.Resources._16___Resource___Hotfix.ToBitmap, AddressOf onClick_addResource_Hotfix)
		mTSMI.DropDownItems.Add("Registry Scan", My.Resources._16___Resource___Registry.ToBitmap, AddressOf onClick_addResource_Registry)
		mTSMI.DropDownItems.Add("Windows Services", My.Resources._16___Resource___Services.ToBitmap, AddressOf onClick_addResource_Services)
		mTSMI.DropDownItems.Add("WMI Query", My.Resources._16___Resource___WMIQuery.ToBitmap, AddressOf onClick_addResource_WMIQuery)

		For Each mItm As ToolStripMenuItem In mTSMI.DropDownItems
			AddHandler CType(mItm, ToolStripMenuItem).MouseEnter, AddressOf onClick_tssInfo_MouseEnter
		Next

		Return mTSMI
	End Function

	Private Sub lstResources_KeyUp(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles lstResources.KeyUp
		Call SendMessage(lstResources.Handle, WM_ChangeUIState, CInt(MakeLong(UIS_HideRectangle, UISF_FocusRectangle)), IntPtr.Zero)
		Select Case e.KeyCode
			Case Keys.Delete : If (lstResources.SelectedItems.Count > 0) Then Call onClick_removeResource(sender, e)
			Case Keys.Enter : If (lstResources.SelectedItems.Count = 1) Then Call onClick_editProperties(sender, e)
			Case Else
				If (e.Control And e.KeyCode = Keys.A) Then
					For Each itm As ListViewItem In lstResources.Items
						itm.Selected = True
					Next
				End If
		End Select
		e.Handled = True
	End Sub

	Private Sub lstResources_MouseDoubleClick(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles lstResources.MouseDoubleClick
		If (lstResources.HitTest(e.X, e.Y) Is Nothing) Then Exit Sub

		Select Case lstResources.SelectedItems(0).SubItems(1).Text
			Case "Eventlog Scan", "File Check", "Free Space Threshold", "Registry Scan", "WMI Query"
				Call onClick_editProperties(sender, e)

			Case "Hotfix Patch", "Windows Service"
				Call onClick_changeState(sender, e)

			Case Else
				MessageBox.Show(Me, "You should never see this error..." & vbCrLf & vbCrLf & "lstResources_MouseDoubleClick" & vbCrLf & _
				 lstResources.SelectedItems(0).SubItems(1).Text, APN, MessageBoxButtons.OK, MessageBoxIcon.Error)
		End Select
	End Sub

	Private Sub lblNoResourcesAtThisLevel_MouseUp(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles lblNoResourcesAtThisLevel.MouseUp
		Dim ee As New MouseEventArgs(e.Button, e.Clicks, e.X + lblNoResourcesAtThisLevel.Left, e.Y + lblNoResourcesAtThisLevel.Top, e.Delta)
		Call lstResources_MouseUp(sender, ee)
	End Sub

	Private Sub lstResources_MouseUp(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles lstResources.MouseUp
		If (xmlDoc Is Nothing) Then Exit Sub
		If (e.Button <> Windows.Forms.MouseButtons.Right) Then Exit Sub
		If (tvwServerList.SelectedNode) Is Nothing Then Exit Sub
		Dim lsNode As ListViewItem = lstResources.GetItemAt(e.X, e.Y)
		' Do not check for (lsNode is Nothing)

		Dim bMultiState As Boolean = False		' Determines the "Change Checking State" option
		Dim bMultiSelect As Boolean = False		' Is more than one item selected
		If (lstResources.SelectedItems.Count > 1) Then
			bMultiSelect = True
			Dim sFirstMultiItem As String = lsNode.SubItems(1).Text
			For Each lItem As ListViewItem In lstResources.SelectedItems
				If (lItem.SubItems(1).Text <> sFirstMultiItem) Then
					bMultiState = True
					Exit For
				End If
			Next
		End If

		Dim mSub As ToolStripMenuItem = getToolStripMenuItem()
		With mContextResource.Items
			.Clear()
			If (getGroupNode(tvwServerList.SelectedNode).ImageKey <> "LOST_AND_FOUND") Then
				If (lsNode Is Nothing) Then
					.Add(mSub)
					mSub.Enabled = Not bReadOnlyMode
				Else
					Select Case lsNode.SubItems(1).Text
						Case "Windows Service", "Hotfix Patch"
							.Add("Delete Resource" & IIf(bMultiSelect, "s", "").ToString, My.Resources._16___Remove.ToBitmap, AddressOf onClick_removeResource)
							.Item(.Count - 1).Enabled = Not bReadOnlyMode
							If (bMultiState = False) Then
								.Add("Change Checking State", My.Resources._16___ChangeState.ToBitmap, AddressOf onClick_changeState)
								.Item(.Count - 1).Enabled = Not bReadOnlyMode
							End If

						Case "Eventlog Scan", "Free Space Threshold", "Registry Scan", "File Check", "WMI Query"
							.Add("Delete Resource" & IIf(bMultiSelect, "s", "").ToString, My.Resources._16___Remove.ToBitmap, AddressOf onClick_removeResource)
							.Item(.Count - 1).Enabled = Not bReadOnlyMode
							If (bMultiSelect = False) Then .Add("Properties...", My.Resources._16___Properties.ToBitmap, AddressOf onClick_editProperties)

						Case Else
							MessageBox.Show(Me, "ERROR: " & lsNode.SubItems(1).Text, APN, MessageBoxButtons.OK, MessageBoxIcon.Error)
					End Select
				End If

				' Add Copy/Move Functions...
				If (lstResources.SelectedItems.Count > 0) Then
					.Add("-")
					.Add("Move/Copy...", My.Resources._16___Copy.ToBitmap, AddressOf onClick_resourceMoveCopy)
					.Item(.Count - 1).Enabled = Not bReadOnlyMode
				End If
			Else
				.Add("Not Permitted In 'Lost And Found'", My.Resources._16___Eventlog___Error.ToBitmap, Nothing)
			End If
		End With

		Try
			For Each mItm As Object In mContextResource.Items
				If (TypeOf mItm Is ToolStripMenuItem) Then
					AddHandler CType(mItm, ToolStripMenuItem).MouseEnter, AddressOf onClick_tssInfo_MouseEnter
				End If
			Next
		Catch ex As Exception
		End Try

		mContextResource.Show(lstResources, New Point(e.X, e.Y))
	End Sub

	Private Sub lstResources_ColumnClick(sender As Object, e As System.Windows.Forms.ColumnClickEventArgs) Handles lstResources.ColumnClick
		If (e.Column = lvwCS.SortColumn) Then
			If (lvwCS.Order = SortOrder.Ascending) Then
				lvwCS.Order = SortOrder.Descending
			Else
				lvwCS.Order = SortOrder.Ascending
			End If
		Else
			lvwCS.SortColumn = e.Column
			lvwCS.Order = SortOrder.Ascending
		End If

		Application.DoEvents()
		Call ShowListViewSortImage(lstResources, lvwCS.SortColumn, lvwCS.Order)

		With lstResources
			If (bUseVisualStyles = False) Then
				' Remove groups, sort, then apply groups - this is a fudge (that doesn't really work).!
				.BeginUpdate()
				.ShowGroups = False
				.Sort()
				.ShowGroups = True
				.EndUpdate()
			Else
				.Sort()
			End If
		End With
	End Sub

	Private Sub tvwServerList_AfterLabelEdit(sender As Object, e As System.Windows.Forms.NodeLabelEditEventArgs) Handles tvwServerList.AfterLabelEdit
		If (e.Label = Nothing) Then e.CancelEdit = True
		If (e.Label = String.Empty) Then e.CancelEdit = True
		If (e.Label = e.Node.Text) Then e.CancelEdit = True
		If (e.CancelEdit = True) Then Exit Sub

		Dim bLAFConflict As Boolean = False
		If ((InStr(e.Label.ToUpper, "LOST") > 0) AndAlso (InStr(e.Label.ToUpper, "FOUND") > 0)) Then bLAFConflict = True

		If (bLAFConflict = True) Then
			MessageBox.Show(Me, "This is a reserved name, please choose another.", APN, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			e.CancelEdit = True
			e.Node.BeginEdit()
		End If

		If (e.Node.Tag.ToString = "GROUP") Then
			If (e.Label.Length > iLength_GroupName) Then
				MessageBox.Show(Me, "Group names are limited to " & iLength_GroupName & " characters." & vbCrLf & _
					"The entered name has been truncated.", APN, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			End If

			Dim eLabel As String = e.Label
			If (eLabel.Length > iLength_GroupName) Then eLabel = e.Label.Substring(0, iLength_GroupName)

			If (xml_setRenameGroup(e.Node.Name, eLabel, xml_getGroupIconColour(e.Node.Name), Nothing) = True) Then
				e.Node.EndEdit(False)
			Else
				e.Node.EndEdit(True)
				e.Node.Text = eLabel
			End If
		End If
		BeginInvoke(New SortTreeDelegate(AddressOf SortTree), e.Node)
	End Sub

	Private Sub SortTree(SelectedNode As TreeNode)
		Me.tvwServerList.Sort()
		Me.tvwServerList.SelectedNode = SelectedNode
	End Sub

	Private Sub tvwServerList_BeforeLabelEdit(sender As Object, e As System.Windows.Forms.NodeLabelEditEventArgs) Handles tvwServerList.BeforeLabelEdit
		' Rename GROUPS only
		If (bReadOnlyMode = True) Then e.CancelEdit = True
		If (tvwServerList.SelectedNode IsNot Nothing) Then
			If (tvwServerList.SelectedNode.Tag.ToString <> "GROUP") Then e.CancelEdit = True
		End If
	End Sub

	' ### onClick Functions #######################################################################
	' ### onClick_xxx Functions ###################################################################
	Private Sub onClick_addGroup(sender As System.Object, e As System.EventArgs)
		If (tvwServerList.SelectedNode Is Nothing) Then
			Call onClick_addGroup_()
		Else
			Call onClick_addGroup_(, , , tvwServerList.SelectedNode.Name)
		End If
	End Sub
	Private Sub onClick_addServer(sender As System.Object, e As System.EventArgs)
		Call onClick_addServer_("SHOW_DIALOG", "SHOW_DIALOG", tvwServerList.SelectedNode.Name)
	End Sub

	Public Sub onClick_addGroup_(Optional ByVal sGroupName As String = Nothing, Optional ByVal sGUID As String = vbNullString, Optional ByVal iIconColour As Integer = 9, Optional ByVal sParentGUID As String = vbNullString)
		Dim tvNode As New TreeNode
		Dim tvParent As New TreeNode
		tssInfo.Text = "Adding Group..."
		If (sGroupName Is Nothing) Then sGroupName = findNewGroupName(tvwServerList)
		If (sGroupName.Length > iLength_GroupName) Then sGroupName = sGroupName.Substring(0, iLength_GroupName)

		' New Node Details...
		With tvNode
			Dim sNewGUID As String = sGUID
			sNewGUID = checkGUID(tvwServerList, sGUID)
			If (sGUID <> sNewGUID) Then Call xml_setNewGUID(sGUID, sNewGUID)

			.Text = sGroupName
			.Tag = "GROUP"
			.Name = sNewGUID

			If ((sGroupName = "Lost And Found") AndAlso (iIconColour = 99)) Then
				sLostAndFound = .Name
				.ImageKey = "LOST_AND_FOUND"
			Else
				.ImageKey = "_16___Group___" & getGroupColour(iIconColour, False)
			End If

			.SelectedImageKey = .ImageKey
		End With

		' Check if folders are too deep, rest to root level...
		If (sParentGUID <> vbNullString) Then
			tvParent = tvwServerList.Nodes.Find(sParentGUID, True)(0)
			If (checkGroupDepth(tvParent.FullPath, tvwServerList.PathSeparator, vbNull) = False) Then
				tvParent = Nothing
				sParentGUID = vbNullString
			End If
		End If

		lblNoGroupsAtThisLevel.Visible = False
		If (sParentGUID = vbNullString) Then
			' Creating a brand new node...
			If (sGUID = vbNullString) Then
				If (xml_setAddGroup(tvNode.Text, tvNode.Name, iIconColour, Nothing) = True) Then tvwServerList.Nodes.Add(tvNode)
				tvwServerList.LabelEdit = True
				tvNode.BeginEdit()
			Else
				tvwServerList.Nodes.Add(tvNode)
				tvwServerList.Sort()
			End If
		Else
			' Create a sub node...
			tvParent = tvwServerList.Nodes.Find(sParentGUID, True)(0)
			If (sGUID = vbNullString) Then
				If (xml_setAddGroup(tvNode.Text, tvNode.Name, iIconColour, tvParent.Name) = True) Then tvParent.Nodes.Add(tvNode)
				tvwServerList.SelectedNode = tvNode
				tvNode.BeginEdit()
			Else
				tvParent.Nodes.Add(tvNode)
				tvParent.Expand()
				tvwServerList.Sort()
			End If
		End If

		Call drawFullPathHeader(tvNode)
		tvNode = Nothing
		tvParent = Nothing
		tssInfo.Text = Ready
	End Sub

	Public Function onClick_addServer_(Optional ByVal sServerName As String = vbNullString, Optional ByVal sGUID As String = vbNullString, Optional ByVal sParentGUID As String = vbNullString) As Boolean
		If ((sServerName = "SHOW_DIALOG") AndAlso (sGUID = "SHOW_DIALOG")) Then
			tssInfo.Text = "Adding Server..."
			frmAddServer.ShowDialog(Me)
			sServerName = vbNullString
			sGUID = vbNullString
			Call onClick_addServer_Multi(sParentGUID)
			tssInfo.Text = Ready
			Return False
		End If

		sServerName = sServerName.Trim.ToLower
		If (m_UppercaseServerNames = True) Then sServerName = sServerName.Trim.ToUpper
		If ((sParentGUID = "root") Or ((sParentGUID Is Nothing))) Then sParentGUID = createLostAndFound(sGUID, Nothing)

		Dim tvNode As TreeNode
		Dim nSelected As TreeNode = tvwServerList.Nodes.Find(sParentGUID, True)(0)
		If (nSelected.Tag.ToString <> "GROUP") Then
			Dim nOld As TreeNode = tvwServerList.Nodes.Find(sParentGUID, True)(0)
			sLostAndFound = createLostAndFound(sGUID, nOld)
			nSelected = tvwServerList.Nodes.Find(sLostAndFound, True)(0)
		End If

		' Don't add the same server to the same group...
		Dim sExisting As String = isDuplicateServerInGroup(nSelected.Nodes, sServerName)
		If (sExisting Is Nothing) Then
			tvNode = New TreeNode
			With tvNode
				.Text = sServerName

				If ((sGUID = vbNullString) Or (xml_LoadServerData(sGUID) Is Nothing)) Then
					.Tag = "SERVER"
					.ImageKey = "_16___Server"
					.SelectedImageKey = .ImageKey
				Else
					.Tag = "SERVWP"
					.ImageKey = "_16___Server_With_Properties"
					.SelectedImageKey = .ImageKey
				End If

				Dim sNewGUID As String = sGUID
				sNewGUID = checkGUID(tvwServerList, sGUID)
				If (sGUID <> sNewGUID) Then Call xml_setNewGUID(sGUID, sNewGUID)
				.Name = sNewGUID
			End With

			With tvwServerList
				.SelectedNode = nSelected	' This is the servers parent
				.SelectedNode.Nodes.Add(tvNode)
				.SelectedNode.Expand()
				If (sGUID = vbNullString) Then
					Call xml_setAddServer(tvNode.Text, tvNode.Name, tvNode.Parent.Name)
				End If
			End With
			tvNode = Nothing
		Else
			tssInfo.Text = Ready
			Return False
		End If

		If ((bLoadingXML = False) AndAlso (m_multiAdding = False)) Then Call xml_SaveXmlDocument(sConfigFile)
		Return True
		tssInfo.Text = Ready
	End Function
	Public Sub onClick_addServer_Multi(ByVal sParentGUID As String)
		If ((m_AddedServers Is Nothing) OrElse (m_AddedServers.Count = 0)) Then
			tssInfo.Text = Ready
			Exit Sub
		End If

		Dim sSkippedServers As New List(Of String)

		m_multiAdding = True
		For Each itm As String In m_AddedServers
			If (itm <> "") Then
				If (onClick_addServer_(itm, Nothing, sParentGUID) = False) Then sSkippedServers.Add(itm)
			End If
		Next
		m_multiAdding = False
		m_AddedServers = Nothing
		Call xml_SaveXmlDocument(sConfigFile)

		If (sSkippedServers.Count > 0) Then
			Dim sMsg As String = "The following servers are duplicates and were not added." & vbCrLf
			sMsg = sMsg & "Use the search function if you can't find then." & vbCrLf & vbCrLf
			For Each sServer As String In sSkippedServers
				sMsg = sMsg & "    " & sServer & vbCrLf
			Next
			MessageBox.Show(Me, sMsg, APN, MessageBoxButtons.OK, MessageBoxIcon.Information)
		End If
		tssInfo.Text = Ready
	End Sub

	Private Sub onClick_removeGroup(sender As System.Object, e As System.EventArgs)
		tssInfo.Text = "Removing group..."

		Dim sMsg As String = "Are you sure you want to remove this group.?" & vbCrLf &
		   "All child groups and/or servers, as well as any resources will be deleted." & vbCrLf & vbCrLf

		Dim xResources As List(Of XmlElement) = xml_getResourceList(tvwServerList.SelectedNode.Name, eDirectionalSearch.ToChildren, True)
		If ((xResources IsNot Nothing) AndAlso (xResources.Count > 0)) Then sMsg = sMsg & "    Resource Count: " & vbTab & xResources.Count & vbCrLf

		Dim xGroups As List(Of XmlNode) = xml_getGroupList_FromGroup(tvwServerList.SelectedNode.Name, eDirectionalSearch.ToChildren)
		If ((xGroups IsNot Nothing) AndAlso (xGroups.Count > 1)) Then sMsg = sMsg & "    Group Count: " & vbTab & xGroups.Count - 1 & vbCrLf

		Dim xServers As List(Of XmlNode) = xml_getServerList_FromGroup(tvwServerList.SelectedNode.Name)
		If ((xServers IsNot Nothing) AndAlso (xServers.Count > 0)) Then sMsg = sMsg & "    Server Count: " & vbTab & xServers.Count & vbCrLf

		If ((InStr(sMsg, "Resource Count") > 0) Or (InStr(sMsg, "Group Count") > 0) Or (InStr(sMsg, "Server Count") > 0)) Then sMsg = sMsg & vbCrLf
		sMsg = sMsg & "This action can not be undone."

		xResources = Nothing
		xGroups = Nothing
		xServers = Nothing

		Dim iResult As DialogResult = MessageBox.Show(Me, sMsg, APN, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2)
		If (iResult = Windows.Forms.DialogResult.No) Then
			tssInfo.Text = Ready
			Exit Sub
		End If

		Dim nParent As TreeNode = tvwServerList.SelectedNode.Parent
		If (nParent Is Nothing) Then
			If (tvwServerList.Nodes.Count > 0) Then nParent = tvwServerList.Nodes(0)
		End If

		If (xml_setRemoveGroup(tvwServerList.SelectedNode.Name) = True) Then tvwServerList.SelectedNode.Remove()
		If (tvwServerList.Nodes.Count > 0) Then tvwServerList.SelectedNode = tvwServerList.Nodes(0)
		If (tvwServerList.Nodes.Count = 0) Then
			lblNoGroupsAtThisLevel.Visible = True
			lblNoResourcesAtThisLevel.Text = lNoRes2
		End If

		tvwServerList.SelectedNode = nParent
		Call tvwServerList_NodeMouseClick(Nothing, Nothing)
		Call drawFullPathHeader(tvwServerList.SelectedNode)
		tssInfo.Text = Ready
	End Sub

	Private Sub onClick_renameGroup(sender As System.Object, e As System.EventArgs)
		tvwServerList.SelectedNode.BeginEdit()
	End Sub

	Private Sub onClick_removeServer(sender As System.Object, e As System.EventArgs)
		tssInfo.Text = "Removing server..."
		Dim iResult As DialogResult = MessageBox.Show(Me, "Are you sure you want to remove this server.?", APN, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
		If (iResult = Windows.Forms.DialogResult.No) Then
			tssInfo.Text = Ready
			Exit Sub
		End If
		If (xml_setRemoveServer(tvwServerList.SelectedNode.Name) = True) Then tvwServerList.SelectedNode.Remove()
		tssInfo.Text = Ready
	End Sub

	Private Sub onClick_removeResource(sender As System.Object, e As System.EventArgs)
		tssInfo.Text = "Removing resource..."
		Dim sMSG As String
		If lstResources.SelectedItems.Count > 1 Then sMSG = "these resources.?" Else sMSG = "this resource.?"
		Dim iResult As DialogResult = MessageBox.Show(Me, "Are you sure you want to remove " & sMSG, _
		  APN, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2)
		If (iResult = Windows.Forms.DialogResult.No) Then
			tssInfo.Text = Ready
			Exit Sub
		End If
		Me.Cursor = Cursors.WaitCursor
		lstResources.BeginUpdate()
		For Each lItem As ListViewItem In lstResources.SelectedItems
			If (xml_setRemoveResource(lItem.Name) = True) Then lItem.Remove()
		Next
		lstResources.EndUpdate()
		Me.Cursor = Cursors.Default
		Call tvwServerList_NodeMouseClick(Nothing, Nothing)
		tssInfo.Text = Ready
	End Sub

	Private Sub onClick_scanGroup(sender As System.Object, e As System.EventArgs)
		If (xmlDoc Is Nothing) Then Exit Sub
		If (tvwServerList.Nodes.Count = 0) Then Exit Sub
		If (m_NetworkAvailability = False) Then Exit Sub

		' Check for Free Space Threshold resource and alert if not found...
		Dim bFound As Boolean = False
		'xml_getResourceList(tvwServerList.SelectedNode.Name, eDirectionalSearch.ToChildren, True)
		Dim lResources As List(Of XmlElement) = xml_getResourceList(tvwServerList.SelectedNode.Name, eDirectionalSearch.ToParents, True)
		lResources.AddRange(xml_getResourceList(tvwServerList.SelectedNode.Name, eDirectionalSearch.ToChildren, True))
		Dim lResourcesFiltered As List(Of XmlElement) = lResources.FindAll(Function(f) f.Attributes("type").Value = "Free Space Threshold")
		If (lResourcesFiltered.Count > 0) Then bFound = True

		If (bFound = False) Then
			Dim sMsg As String = "There is no 'Free Space Threshold' resource." & vbCrLf
			sMsg = sMsg & "You can either skip this scan or use the defaults." & vbCrLf & vbCrLf
			sMsg = sMsg & "- Continue" & vbTab & "Skip Free Space scanning" & vbCrLf
			sMsg = sMsg & "- Use Defaults" & vbTab & "Use default values for thresholds" & vbCrLf & vbCrLf
			sMsg = sMsg & "What would you like to do.?"

			clsMessageBox.CustomMsgBox(New String() {"Continue", "Use Defaults", "Cancel"})
			Dim dResult As DialogResult = MessageBox.Show(Me, sMsg, APN, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)
			If (dResult = DialogResult.Cancel) Then Exit Sub
			If (dResult = DialogResult.No) Then
				Call xml_setAddResource(getGroupNode(tvwServerList.SelectedNode).Name, "Free Space Threshold", "Free Space Threshold", "10|20|Exclude", Guid.NewGuid.ToString)
			End If
		End If

		tssInfo.Text = "Please wait, scanning selected group..."
		If (tvwServerList.SelectedNode.Tag.ToString <> "GROUP") Then tvwServerList.SelectedNode = tvwServerList.SelectedNode.Parent
		frmScan.sGroupGUID = tvwServerList.SelectedNode.Name
		frmScan.ShowDialog(Me)
		Call tvwServerList_NodeMouseClick(Nothing, Nothing)
		tssInfo.Text = Ready
	End Sub

	Private Sub onClick_resourceMoveCopy(sender As System.Object, e As System.EventArgs)
		Dim mItem As ToolStripMenuItem = CType(sender, ToolStripMenuItem)
		tssInfo.Text = "Resource Move/Copy..."

		frmMoveCopyResource.sNode = getGroupNode(tvwServerList.SelectedNode)
		frmMoveCopyResource.ShowDialog(Me)
		mItem.Dispose()
		Call tvwServerList_NodeMouseClick(Nothing, Nothing)
		tssInfo.Text = Ready
	End Sub

	Private Sub onClick_editProperties(sender As System.Object, e As System.EventArgs)
		If (lstResources.SelectedItems.Count = 0) Then Exit Sub
		tssInfo.Text = "Editing " & lstResources.SelectedItems(0).SubItems(1).Text & " resource..."
		Me.Cursor = Cursors.WaitCursor

		Select Case lstResources.SelectedItems(0).SubItems(1).Text
			Case "Eventlog Scan"
				frmAddEventlog.lEditValues = lstResources.SelectedItems(0)
				frmAddEventlog.ShowDialog(Me)
				If (m_AddEventLog = vbNullString) Then Exit Select
				Dim sData() As String = Split(m_AddEventLog, "|")
				Call xml_setResourceValue(lstResources.SelectedItems(0).Name, sData(0), sData(1))

			Case "Free Space Threshold"
				frmAddFreeSpace.sResourceGUID = lstResources.SelectedItems(0).Name
				frmAddFreeSpace.ShowDialog(Me)
				If (m_AddFreeSpace = vbNullString) Then Exit Select
				Call xml_setResourceValue(lstResources.SelectedItems(0).Name, "Free Space Threshold", m_AddFreeSpace)

			Case "Registry Scan"
				frmAddRegistry.sGroupGUID = getGroupNode(tvwServerList.SelectedNode).Name
				frmAddRegistry.lEditValues = lstResources.SelectedItems(0)
				frmAddRegistry.ShowDialog(Me)
				If (m_AddRegistry Is Nothing) Then Exit Select
				Dim sData() As String = Split(m_AddRegistry.Item(0), "|")
				Call xml_setResourceValue(lstResources.SelectedItems(0).Name, sData(0), sData(1))

			Case "File Check"
				frmAddFileCheck.lEditValues = lstResources.SelectedItems(0)
				frmAddFileCheck.ShowDialog(Me)
				If (m_AddFileScan Is Nothing) Then Exit Select
				Dim sData() As String = Split(m_AddFileScan, "|")
				Call xml_setResourceValue(lstResources.SelectedItems(0).Name, sData(0), sData(1) & "|" & sData(2))

			Case "WMI Query"
				frmAddWMIQuery.sGroupGUID = getGroupNode(tvwServerList.SelectedNode).Name
				frmAddWMIQuery.lEditValues = lstResources.SelectedItems(0)
				frmAddWMIQuery.ShowDialog(Me)
				If (m_AddWMIQuery Is Nothing) Then Exit Select
				Call xml_setResourceValue(lstResources.SelectedItems(0).Name, lstResources.SelectedItems(0).Text, m_AddWMIQuery)

			Case Else
				'MessageBox.Show(Me, "You should never see this.!" & vbCrLf & lstResources.SelectedItems(0).SubItems(1).Text)
				Application.DoEvents()
		End Select

		Call tvwServerList_NodeMouseClick(Nothing, Nothing)
		Me.Cursor = Cursors.Default
		tssInfo.Text = Ready
	End Sub

	Private Sub onClick_changeState(sender As System.Object, e As System.EventArgs)
		Dim sNewChecking As String = vbNullString

		For Each lItem As ListViewItem In lstResources.SelectedItems
			If (lItem.SubItems(1).Text = "Windows Service") Then
				' Windows Service, change Running/Stopped
				Dim sChecking As String = lItem.SubItems(2).Text
				If (lstResources.SelectedItems.Count > 1) Then
					If (sChecking.Split("("c)(0).Trim = "Running") Then sNewChecking = "Stopped" Else sNewChecking = "Running"
					sNewChecking = sNewChecking & " (" & sChecking.Split("("c)(1).Trim(")"c).Trim & ")"
				Else
					frmAddServicesChangeState.lvwItem = lItem
					frmAddServicesChangeState.ShowDialog(Me)
					If (frmAddServicesChangeState.sReturnResult Is Nothing) Then sNewChecking = sChecking Else sNewChecking = frmAddServicesChangeState.sReturnResult
					frmAddServicesChangeState.Dispose()
				End If
			Else
				' Hotfix Patch, change Installed/Not Installed
				If (lItem.SubItems(2).Text = "Installed") Then sNewChecking = "Not Installed" Else sNewChecking = "Installed"
			End If

			If (xml_setResourceValue(lItem.Name, lItem.Text, sNewChecking) = True) Then
				lItem.SubItems(2).Text = sNewChecking
				lItem.SubItems(2).ForeColor = getSubItemColour(sNewChecking.Split("("c)(0).Trim)
			End If
		Next
	End Sub

	Private Sub onClick_groupProperties(sender As System.Object, e As System.EventArgs)
		If (tvwServerList.SelectedNode.ImageKey = "_16___Eventlog___Error") Then Exit Sub
		If (tvwServerList.SelectedNode.ImageKey.StartsWith("_16___Server") = True) Then tvwServerList.SelectedNode = tvwServerList.SelectedNode.Parent
		tssInfo.Text = "Group properties..."
		Dim cNode As TreeNode = tvwServerList.SelectedNode
		frmPropertiesGroup.sGroupName = tvwServerList.SelectedNode.Text
		frmPropertiesGroup.sGroupGUID = tvwServerList.SelectedNode.Name
		frmPropertiesGroup.ShowDialog(Me)
		tvwServerList.SelectedNode = cNode
		Call tvwServerList_NodeMouseClick(Nothing, Nothing)
		tssInfo.Text = Ready
		cNode = Nothing
	End Sub

	Private Sub onClick_serverProperties(sender As System.Object, e As System.EventArgs)
		tssInfo.Text = "Server properties..."
		frmPropertiesServer.sServerName = tvwServerList.SelectedNode.Text.Trim
		frmPropertiesServer.sServerGUID = tvwServerList.SelectedNode.Name.Trim
		frmPropertiesServer.ShowDialog(Me)
		tssInfo.Text = Ready
	End Sub

	Private Sub onClick_tssInfo_MouseEnter(sender As System.Object, e As System.EventArgs)
		Dim sText As String = vbNullString
		If (TypeOf sender Is ToolStripMenuItem) Then
			sText = CType(sender, ToolStripMenuItem).Name
			If sText = vbNullString Then sText = CType(sender, ToolStripMenuItem).Text
			sText = ReplaceMultiple(sText, vbNullString, {" ", "(", ")", "'", "...", "-", "/"})
			tssInfo.Text = My.Resources.ResourceManager.GetString("mnu_" & sText)
		Else
			tssInfo.Text = Ready
		End If
	End Sub

	Private Sub onClick_tssInfo_MouseLeave(sender As System.Object, e As System.EventArgs)
		tssInfo.Text = Ready
	End Sub

	Private Sub onClick_movecopyServers(sender As System.Object, e As System.EventArgs)
		bMultiSelectDone = False
		frmMoveCopyServers.sGroupGUID = getGroupNode(tvwServerList.SelectedNode).Name
		frmMoveCopyServers.sSelectedServer = tvwServerList.SelectedNode.Name
		frmMoveCopyServers.ShowDialog(Me)
		If (bMultiSelectDone = True) Then Call openSelectedFile(sConfigFile)
	End Sub

	Private Sub onClick_resolveHostName(sender As System.Object, e As System.EventArgs)
		Me.Cursor = Cursors.WaitCursor
		Dim tNode As TreeNode = tvwServerList.SelectedNode
		Dim sOldName As String = tNode.Text
		Dim bFailed As Boolean = False

		Dim sNewName As String = resolveIP(sOldName)
		If (sOldName <> sNewName) Then
			' IP has been resolved, check for duplicate...
			Dim sExisting As String = isDuplicateServerInGroup(tNode.Parent.Nodes, sNewName)
			If (sExisting IsNot Nothing) Then
				' Check both servers for info and remove one without - ask user
				Me.Cursor = Cursors.Default
				frmServerDuplication.sServerGUIDList = {sOldName, sExisting}
				frmServerDuplication.ShowDialog(Me)

				' Check again to see if duplicate was removed...
				If (isDuplicateServerInGroup(tNode.Parent.Nodes, sNewName) IsNot Nothing) Then Exit Sub
			End If

			If (xml_setRenameServer(tNode.Name, sNewName) = True) Then
				tvwServerList.SelectedNode.Text = sNewName
				MessageBox.Show(Me, "Name resolution successful" & vbCrLf & sOldName & " --> " & sNewName, APN, MessageBoxButtons.OK, MessageBoxIcon.Information)
			Else
				bFailed = True
			End If
		Else
			bFailed = True
		End If

		Me.Cursor = Cursors.Default
		If (bFailed = True) Then MessageBox.Show(Me, "Name resolution failed", APN, MessageBoxButtons.OK, MessageBoxIcon.Information)
	End Sub

	' #############################################################################################
	' ### onClick_addResource Functions ###########################################################
	Private Sub onClick_addResource_Services(sender As System.Object, e As System.EventArgs)
		tssInfo.Text = "Adding Windows Services resource..."
		m_AddServices = Nothing
		frmAddServices.sGroupGUID = getGroupNode(tvwServerList.SelectedNode).Name
		frmAddServices.ShowDialog(Me)

		If (m_AddServices Is Nothing) Then
			tssInfo.Text = Ready
			Exit Sub
		End If

		Me.Cursor = Cursors.WaitCursor
		m_multiAdding = True
		For Each sService As String In m_AddServices
			Dim sData() As String = Split(sService, "|")

			Dim sGUID As String = Guid.NewGuid.ToString
			Dim sNewGUID As String = sGUID
			sNewGUID = checkGUID(lstResources, sGUID)
			If (sGUID <> sNewGUID) Then Call xml_setNewGUID(sGUID, sNewGUID)

			Call xml_setAddResource(getGroupNode(tvwServerList.SelectedNode).Name, "Windows Service", sData(0), sData(1), sNewGUID)
		Next
		m_multiAdding = False
		Call xml_SaveXmlDocument(sConfigFile)

		Me.Cursor = Cursors.Default
		Call tvwServerList_NodeMouseClick(Nothing, Nothing)
		tssInfo.Text = Ready
	End Sub

	Private Sub onClick_addResource_Hotfix(sender As System.Object, e As System.EventArgs)
		tssInfo.Text = "Adding Hotfix Patch resource..."
		m_AddHotfixes = Nothing
		frmAddHotFix.sGroupGUID = getGroupNode(tvwServerList.SelectedNode).Name
		frmAddHotFix.ShowDialog(Me)

		If (m_AddHotfixes Is Nothing) Then
			tssInfo.Text = Ready
			Exit Sub
		End If

		Me.Cursor = Cursors.WaitCursor
		m_multiAdding = True
		For Each hfItem As String In m_AddHotfixes
			If (hfItem <> "") Then
				Dim sData() As String = Split(hfItem, "|")

				Dim sGUID As String = Guid.NewGuid.ToString
				Dim sNewGUID As String = sGUID
				sNewGUID = checkGUID(lstResources, sGUID)
				If (sGUID <> sNewGUID) Then Call xml_setNewGUID(sGUID, sNewGUID)
				Call xml_setAddResource(getGroupNode(tvwServerList.SelectedNode).Name, "Hotfix Patch", sData(0), sData(1), sNewGUID)
			End If
		Next
		m_multiAdding = False
		Call xml_SaveXmlDocument(sConfigFile)

		Me.Cursor = Cursors.Default
		Call tvwServerList_NodeMouseClick(Nothing, Nothing)
		tssInfo.Text = Ready
	End Sub

	Private Sub onClick_addResource_Eventlog(sender As System.Object, e As System.EventArgs)
		tssInfo.Text = "Adding Eventlog Scan resource..."
		m_AddEventLog = Nothing
		frmAddEventlog.lEditValues = Nothing
		frmAddEventlog.ShowDialog(Me)

		If (m_AddEventLog = vbNullString) Then
			tssInfo.Text = Ready
			Exit Sub
		End If

		Me.Cursor = Cursors.WaitCursor
		Dim sData() As String = Split(m_AddEventLog, "|")

		Dim sGUID As String = Guid.NewGuid.ToString
		Dim sNewGUID As String = sGUID
		sNewGUID = checkGUID(lstResources, sGUID)
		If (sGUID <> sNewGUID) Then Call xml_setNewGUID(sGUID, sNewGUID)
		Call xml_setAddResource(getGroupNode(tvwServerList.SelectedNode).Name, "Eventlog Scan", sData(0), sData(1), sNewGUID)

		Me.Cursor = Cursors.Default
		Call tvwServerList_NodeMouseClick(Nothing, Nothing)
		tssInfo.Text = Ready
	End Sub

	Private Sub onClick_addResource_Registry(sender As System.Object, e As System.EventArgs)
		tssInfo.Text = "Adding Registry Scan resource..."
		m_AddRegistry = Nothing
		frmAddRegistry.sGroupGUID = getGroupNode(tvwServerList.SelectedNode).Name
		frmAddRegistry.lEditValues = Nothing
		frmAddRegistry.ShowDialog(Me)

		If (m_AddRegistry Is Nothing) Then
			tssInfo.Text = Ready
			Exit Sub
		End If

		m_multiAdding = True
		Me.Cursor = Cursors.WaitCursor
		For Each reItem As String In m_AddRegistry.GetRange(0, m_AddRegistry.Count)
			If (reItem <> "") Then
				Dim sData() As String = Split(reItem, "|")

				Dim sGUID As String = Guid.NewGuid.ToString
				Dim sNewGUID As String = sGUID
				sNewGUID = checkGUID(lstResources, sGUID)
				If (sGUID <> sNewGUID) Then Call xml_setNewGUID(sGUID, sNewGUID)

				Call xml_setAddResource(getGroupNode(tvwServerList.SelectedNode).Name, "Registry Scan", sData(0), sData(1), sNewGUID)
			End If
		Next
		m_multiAdding = False
		Call xml_SaveXmlDocument(sConfigFile)

		Me.Cursor = Cursors.Default
		Call tvwServerList_NodeMouseClick(Nothing, Nothing)
		tssInfo.Text = Ready
	End Sub

	Private Sub onClick_addResource_FileCheck(sender As System.Object, e As System.EventArgs)
		tssInfo.Text = "Adding File Check resource..."
		m_AddFileScan = Nothing
		frmAddFileCheck.lEditValues = Nothing
		frmAddFileCheck.ShowDialog(Me)

		If (m_AddFileScan Is Nothing) Then
			tssInfo.Text = Ready
			Exit Sub
		End If

		Me.Cursor = Cursors.WaitCursor
		Dim sData() As String = Split(m_AddFileScan, "|")

		Dim sGUID As String = Guid.NewGuid.ToString
		Dim sNewGUID As String = sGUID
		sNewGUID = checkGUID(lstResources, sGUID)
		If (sGUID <> sNewGUID) Then Call xml_setNewGUID(sGUID, sNewGUID)
		Call xml_setAddResource(getGroupNode(tvwServerList.SelectedNode).Name, "File Check", sData(0), sData(1) & "|" & sData(2), sNewGUID)

		Me.Cursor = Cursors.Default
		Call tvwServerList_NodeMouseClick(Nothing, Nothing)
		tssInfo.Text = Ready
	End Sub

	Private Sub onClick_addResource_FreeSpace(sender As System.Object, e As System.EventArgs)
		tssInfo.Text = "Adding Free Space Threshold resource..."
		m_AddFreeSpace = Nothing
		frmAddFreeSpace.sResourceGUID = Nothing
		frmAddFreeSpace.ShowDialog(Me)

		If (m_AddFreeSpace = vbNullString) Then
			tssInfo.Text = Ready
			Exit Sub
		End If

		Me.Cursor = Cursors.WaitCursor

		Dim sGUID As String = Guid.NewGuid.ToString
		Dim sNewGUID As String = sGUID
		sNewGUID = checkGUID(lstResources, sGUID)
		If (sGUID <> sNewGUID) Then Call xml_setNewGUID(sGUID, sNewGUID)
		Call xml_setAddResource(getGroupNode(tvwServerList.SelectedNode).Name, "Free Space Threshold", "Free Space Threshold", m_AddFreeSpace, sNewGUID)

		Me.Cursor = Cursors.Default
		Call tvwServerList_NodeMouseClick(Nothing, Nothing)
		tssInfo.Text = Ready
	End Sub

	Private Sub onClick_addResource_WMIQuery(sender As System.Object, e As System.EventArgs)
		tssInfo.Text = "Adding WMI Query resource..."
		m_AddWMIQuery = Nothing
		frmAddWMIQuery.lEditValues = Nothing
		frmAddWMIQuery.sGroupGUID = getGroupNode(tvwServerList.SelectedNode).Name
		frmAddWMIQuery.ShowDialog(Me)

		If (m_AddWMIQuery Is Nothing) Then
			tssInfo.Text = Ready
			Exit Sub
		End If

		Me.Cursor = Cursors.WaitCursor

		Dim sGUID As String = Guid.NewGuid.ToString
		Dim sNewGUID As String = sGUID
		sNewGUID = checkGUID(lstResources, sGUID)
		If (sGUID <> sNewGUID) Then Call xml_setNewGUID(sGUID, sNewGUID)
		Call xml_setAddResource(getGroupNode(tvwServerList.SelectedNode).Name, "WMI Query", "WMI Query (" & sGUID.Substring(0, 8) & ")", m_AddWMIQuery, sNewGUID)

		Me.Cursor = Cursors.Default
		Call tvwServerList_NodeMouseClick(Nothing, Nothing)
		tssInfo.Text = Ready
	End Sub
	' ### onClick_addResource Functions ###########################################################
	' #############################################################################################

	Private Function createLostAndFound(ByVal sGUID As String, ByVal sOldParent As TreeNode) As String
		If (sLostAndFound = vbNullString) Then Call onClick_addGroup_("Lost And Found", , 99)
		xml_setNewParent(sGUID, sOldParent.Name, tvwServerList.Nodes.Find(sLostAndFound, True)(0).Name)
		Return sLostAndFound
	End Function

	Private Sub NewToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles NewToolStripMenuItem.Click
		Me.Cursor = Cursors.WaitCursor
		Using dlgSaveFile As New SaveFileDialog
			With dlgSaveFile
				.Title = "Create a new configuration file..."
				tssInfo.Text = .Title
				.CheckFileExists = False
				.SupportMultiDottedExtensions = False
				.FileName = "New Configuration.ssc"
				.DefaultExt = "scf"
				.InitialDirectory = Application.StartupPath
				.Filter = "Server System Check Files (*.ssc)|*.ssc|All Files|*.*"
			End With
			If (dlgSaveFile.ShowDialog(Me) = Windows.Forms.DialogResult.OK) Then
				Me.Cursor = Cursors.WaitCursor
				sConfigFile = dlgSaveFile.FileName
				tssInfo.Text = "Creating new configuration file..."
				Call xmlCreateNewConfig()
				Call openSelectedFile(sConfigFile)

				lblNoGroupsAtThisLevel.Visible = False
				lblNoResourcesAtThisLevel.Visible = False

				Me.Cursor = Cursors.Default
				Dim iResult As DialogResult = MessageBox.Show(Me, "Do you want to pre-populate with an example layout.?", APN, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
				If (iResult = Windows.Forms.DialogResult.Yes) Then
					Call prePopulateData()
				Else
					lblNoGroupsAtThisLevel.Visible = True
					lblNoResourcesAtThisLevel.Visible = True
				End If

				conSplitContainer.IsSplitterFixed = False
				CloseToolStripMenuItem.Enabled = True
			End If
			Me.Cursor = Cursors.Default
			tssInfo.Text = Ready
		End Using
	End Sub

	Private Sub OpenToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles OpenToolStripMenuItem.Click
		Using dlgOpenFile As New OpenFileDialog
			With dlgOpenFile
				.Title = "Select an existing configuration file to open..."
				tssInfo.Text = .Title
				.CheckFileExists = False
				.SupportMultiDottedExtensions = False
				.InitialDirectory = Application.StartupPath
				.Filter = "Server System Check Files (*.ssc)|*.ssc|All Files|*.*"
			End With
			If (dlgOpenFile.ShowDialog(Me) = Windows.Forms.DialogResult.OK) Then
				Dim sOldConfig As String = sConfigFile
				Call openSelectedFile(dlgOpenFile.FileName)
			End If
		End Using
	End Sub

	Private Sub CloseToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles CloseToolStripMenuItem.Click
		Me.Cursor = Cursors.WaitCursor
		sConfigFile = Nothing
		xmlDoc = Nothing
		Me.Text = Application.ProductName

		lstResources.Items.Clear()
		tvwServerList.Nodes.Clear()

		mnuProperties.Enabled = False
		txtDescription.Text = vbNullString
		If (ToolStripMenuDescriptionExpandCollapse.Tag.ToString = "Hide") Then Call ToolStripMenuDescriptionExpandCollapse_Click(sender, e)

		CloseToolStripMenuItem.Enabled = False
		SearchToolStripMenuItem.Visible = False
		OptionsToolStripMenuItem.Visible = False
		ExpandAllToolStripMenuItem.Visible = False
		CollapseAllToolStripMenuItem.Visible = False
		ShowFullPathToolStripMenuItem.Visible = False

		lblNoGroupsAtThisLevel.Visible = False
		lblNoResourcesAtThisLevel.Visible = False

		m_PingServersBeforeScan = True
		m_IconGroup = "1"
		m_UppercaseServerNames = False
		m_UseColouredGroups = True
		m_ShowGroupCounts = True
		m_ResourceView = "detail"
		m_GroupView = "type"

		lstResources.View = View.Details
		Call doGroupListView(lstResources, "Type", False, m_ShowGroupCounts)
		Call drawFullPathHeader(Nothing)

		panSplashPanel.Visible = True
		lstSplashOptions.Items(0).Selected = True
		lstSplashOptions.Items(0).Selected = False
		picHeaderBar.Select()

		Me.Cursor = Cursors.Default
	End Sub

	Private Sub openSelectedFile(ByVal sFileName As String)

		If (sFileName.ToUpper.EndsWith(".SSC") = False) Then
			Dim sMsg As String = "The selected file is not a valid Server System Checker file." & vbCrLf & _
			   "The file type required should have an extension of .SSC"
			MessageBox.Show(Me, sMsg, APN, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Exit Sub
		End If

		Me.Cursor = Cursors.WaitCursor
		Dim sOldConfig As String = sConfigFile
		sConfigFile = sFileName

		tssInfo.Text = "Loading configuration..."
		Application.DoEvents()

		If (xmlLoadConfig() = True) Then
			If (tvwServerList.Nodes.Count > 0) Then lblNoResourcesAtThisLevel.Text = lNoRes1 Else lblNoResourcesAtThisLevel.Text = lNoRes2
			lblNoResourcesAtThisLevel.Visible = True
			lstResources.HeaderStyle = ColumnHeaderStyle.Clickable
			panSplashPanel.Visible = False
			SearchToolStripMenuItem.Visible = True
			OptionsToolStripMenuItem.Visible = True
			Application.DoEvents()

			ShowFullPathToolStripMenuItem.Margin = New Padding(0, 0, 6, 0)
			ShowFullPathToolStripMenuItem.Visible = True

			If (bUseVisualStyles = True) Then
				ExpandAllToolStripMenuItem.Visible = True
				CollapseAllToolStripMenuItem.Visible = True
				ShowFullPathToolStripMenuItem.Margin = New Padding(0, 0, 0, 0)
			End If

			If (tvwServerList.Nodes.Count = 0) Then lblNoGroupsAtThisLevel.Visible = True
			Me.Text = sConfigFile.Substring(InStrRev(sConfigFile, "\")) & " - " & Application.ProductName
			conSplitContainer.IsSplitterFixed = False
			Application.DoEvents()
			Call tvwServerList_NodeMouseClick(Nothing, Nothing)
			CloseToolStripMenuItem.Enabled = True
			mnuProperties.Enabled = True
			tssInfo.Text = Ready
		Else
			lblNoGroupsAtThisLevel.Visible = True
			mnuProperties.Enabled = False
			sConfigFile = sOldConfig
			sOldConfig = Nothing
			tssInfo.Text = "Error opening file."
		End If
		Me.Cursor = Cursors.Default
		Call ShowListViewSortImage(lstResources, lvwCS.SortColumn, lvwCS.Order)
	End Sub

	Private Sub ExitToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles ExitToolStripMenuItem.Click
		tssInfo.Text = "Goodbye..."
		frm_DragDrop.Dispose()
		Me.Dispose()
		Close()
	End Sub

	' #############################################################################################

	Private Sub tvwServerList_KeyUp(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles tvwServerList.KeyUp
		If (tvwServerList.SelectedNode Is Nothing) Then Exit Sub
		Try
			Select Case e.KeyCode
				Case Keys.Delete
					' Delete GROUP or SERVER...
					If (tvwServerList.SelectedNode.Tag.ToString = "GROUP") Then Call onClick_removeGroup(sender, e)
					If (tvwServerList.SelectedNode.Tag.ToString = "SERVER") Then Call onClick_removeServer(sender, e)
					If (tvwServerList.SelectedNode.Tag.ToString = "SERVWP") Then Call onClick_removeServer(sender, e)

				Case Keys.F2
					' Rename GROUP...
					If (tvwServerList.SelectedNode.Tag.ToString = "GROUP") Then tvwServerList.SelectedNode.BeginEdit()

				Case Keys.Enter
					' Edit GROUP Properties
					'If (tvwServerList.SelectedNode.Tag.ToString = "GROUP") Then Call onClick_groupProperties(sender, e)
			End Select
			e.Handled = True
		Catch ex As Exception
			e.Handled = False
		End Try
	End Sub

	Private Sub tvwServerList_AfterSelect(sender As Object, e As System.Windows.Forms.TreeViewEventArgs) Handles tvwServerList.AfterSelect
		Call tvwServerList_NodeMouseClick(Nothing, Nothing)
	End Sub
	Public Sub tvwServerList_NodeMouseClick(sender As Object, e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles tvwServerList.NodeMouseClick
		Dim tvItem As TreeNode
		Dim sImageKey As String = vbNullString

		If (xmlDoc Is Nothing) Then Exit Sub
		If ((sender Is Nothing) AndAlso (e Is Nothing)) Then tvItem = tvwServerList.SelectedNode Else tvItem = tvwServerList.HitTest(e.X, e.Y).Node
		If (tvItem Is Nothing) Then Exit Sub

		Me.Cursor = Cursors.WaitCursor
		lstResources.BeginUpdate()
		lstResources.Items.Clear()
		txtDescription.Text = vbNullString

		Select Case tvItem.Tag.ToString
			Case "GROUP"
				Call xml_getResourceList(tvItem.Name, eDirectionalSearch.ToParents, False)
				txtDescription.Text = xml_getGroupDescription(tvItem.Name)
				sImageKey = tvItem.ImageKey
				tvwServerList.LabelEdit = True
				m_CurrentNode = tvItem

			Case "SERVER", "SERVWP"
				Call xml_getResourceList(tvItem.Parent.Name, eDirectionalSearch.ToParents, False)
				Dim sServerData As String() = xml_LoadServerData(tvItem.Name)
				If (sServerData IsNot Nothing) Then txtDescription.Text = sServerData(0) & vbCrLf & sServerData(1) & vbCrLf & vbCrLf & sServerData(3) & vbCrLf & sServerData(4) & vbCrLf & sServerData(5)
				sImageKey = tvItem.Parent.ImageKey
				tvwServerList.LabelEdit = False
				m_CurrentNode = tvItem.Parent
		End Select

		txtDescription.Enabled = True
		If (txtDescription.Text.Length = 0) Then
			txtDescription.Text = "(No Description Available)"
			txtDescription.Enabled = False
		End If

		' Set groups and sort...
		If ((lstResources.Items.Count = 0) AndAlso (sImageKey <> "LOST_AND_FOUND")) Then
			CollapseAllToolStripMenuItem.Enabled = False
			ExpandAllToolStripMenuItem.Enabled = False
			lblNoResourcesAtThisLevel.Visible = True
			lblNoResourcesAtThisLevel.Text = lNoRes1

		Else
			CollapseAllToolStripMenuItem.Enabled = True
			ExpandAllToolStripMenuItem.Enabled = True
			lblNoResourcesAtThisLevel.Visible = False

			Select Case m_GroupView
				Case "none" : lstResources.ShowGroups = False
				Case "parent" : Call doGroupListView(lstResources, "Defined", True, m_ShowGroupCounts)
				Case "type" : Call doGroupListView(lstResources, "Type", False, m_ShowGroupCounts)
			End Select

			lstResources.Sort()
			If (bUseVisualStyles = True) Then
				lstResources.SetGroupState(ListViewGroupState.Collapsible)
				For Each grp As ListViewGroup In lstResources.Groups
					Call lstResources.SetGroupFooter(grp)
				Next
			Else
				For Each grp As ListViewGroup In lstResources.Groups
					If (m_ShowGroupCounts = True) Then
						grp.Header = grp.Header.Split("("c)(0) & " (" & grp.Items.Count & ")"
					Else
						grp.Header = grp.Header.Split("("c)(0)
					End If
				Next
			End If
		End If

		lstResources.EndUpdate()
		Me.Cursor = Cursors.Default
		Call drawFullPathHeader(m_CurrentNode)
	End Sub

	Private Sub AboutToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles AboutToolStripMenuItem.Click
		tssInfo.Text = "About Me..."
		frmAbout.ShowDialog(Me)
		tssInfo.Text = Ready
	End Sub

	Private Sub lstSplashOptions_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles lstSplashOptions.KeyPress
		If (e.KeyChar = ChrW(Keys.Enter)) Then
			Dim lItem As ListViewItem = lstSplashOptions.SelectedItems(0)
			If (lItem Is Nothing) Then Exit Sub
			Select Case lItem.Index
				Case 0 : Call NewToolStripMenuItem_Click(sender, e)
				Case 1 : Call OpenToolStripMenuItem_Click(sender, e)
				Case 2 : Call HelpToolStripMenuItem_Click(sender, e)
			End Select
		End If
	End Sub

	Private Sub lstSplashOptions_MouseDoubleClick(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles lstSplashOptions.MouseDoubleClick
		If (e.Button <> Windows.Forms.MouseButtons.Left) Then Exit Sub
		Dim lItem As ListViewItem = lstSplashOptions.GetItemAt(e.X, e.Y)
		If (lItem Is Nothing) Then Exit Sub
		Select Case lItem.Index
			Case 0 : Call NewToolStripMenuItem_Click(sender, e)
			Case 1 : Call OpenToolStripMenuItem_Click(sender, e)
			Case 2 : Call HelpToolStripMenuItem_Click(sender, e)
		End Select
	End Sub

	Private Sub HelpToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles OpenHelpToolStripMenuItem.Click
		tssInfo.Text = "Help.!"
		frmHelp.ShowDialog(Me)
		tssInfo.Text = Ready
	End Sub

	Private Sub ToolStripStatusAdmin_Click(sender As System.Object, e As System.EventArgs) Handles ToolStripStatusAdmin.Click
		tssInfo.Text = "Help.!"
		frmHelp.sSelectPageByID = "help0022"
		frmHelp.ShowDialog(Me)
		tssInfo.Text = Ready
	End Sub
	Private Sub ToolStripStatusAdmin_MouseLeave(sender As Object, e As System.EventArgs) Handles ToolStripStatusAdmin.MouseLeave
		ToolStripStatusAdmin.ForeColor = SystemColors.ControlText
		Me.Cursor = Cursors.Default
	End Sub
	Private Sub ToolStripStatusAdmin_MouseMove(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles ToolStripStatusAdmin.MouseMove
		ToolStripStatusAdmin.ForeColor = ToolStripStatusAdmin.LinkColor
		Me.Cursor = Cursors.Hand
	End Sub

	Private Sub ToolStripStatusReadOnly_Click(sender As System.Object, e As System.EventArgs) Handles ToolStripStatusReadOnly.Click
		tssInfo.Text = "Help.!"
		frmHelp.sSelectPageByID = "help0036"
		frmHelp.ShowDialog(Me)
		tssInfo.Text = Ready
	End Sub
	Private Sub ToolStripStatusReadOnly_MouseLeave(sender As Object, e As System.EventArgs) Handles ToolStripStatusReadOnly.MouseLeave
		ToolStripStatusReadOnly.ForeColor = SystemColors.ControlText
		Me.Cursor = Cursors.Default
	End Sub
	Private Sub ToolStripStatusReadOnly_MouseMove(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles ToolStripStatusReadOnly.MouseMove
		ToolStripStatusReadOnly.ForeColor = ToolStripStatusReadOnly.LinkColor
		Me.Cursor = Cursors.Hand
	End Sub

	' ### Drag Drop Group/Server ##################################################################
	Private Sub tvwServerList_ItemDrag(sender As Object, e As System.Windows.Forms.ItemDragEventArgs) Handles tvwServerList.ItemDrag
		Dim sNode As TreeNode = CType(e.Item, TreeNode)
		If (sNode Is Nothing) Then Exit Sub
		If (bReadOnlyMode = True) Then Exit Sub
		tssInfo.Text = "Drag and drop operation in progress..."
		Try
			If (sNode.ImageKey.StartsWith("_16___Server") = True) Then m_dragDropType = dragDropType.server Else m_dragDropType = dragDropType.group
			DoDragDrop(e.Item, DragDropEffects.Move)
		Catch ex As Exception
			frm_DragDrop.Visible = False
			tssInfo.Text = Ready
		End Try
	End Sub
	Private Sub tvwServerList_DragEnter(sender As Object, e As System.Windows.Forms.DragEventArgs) Handles tvwServerList.DragEnter
		e.Effect = DragDropEffects.None

		If (e.Data.GetDataPresent(DataFormats.FileDrop) = True) Then
			e.Effect = DragDropEffects.Copy
			Exit Sub
		End If

		Dim sNode As TreeNode = CType(e.Data.GetData("System.Windows.Forms.TreeNode"), TreeNode)		' Source Node
		If (sNode IsNot Nothing) Then
			e.Effect = DragDropEffects.Move
			frm_DragDrop.Visible = True
			Me.Focus()
		End If
	End Sub
	Private Sub tvwServerList_DragLeave(sender As Object, e As System.EventArgs) Handles tvwServerList.DragLeave
		tssInfo.Text = Ready
		frm_DragDrop.Visible = False
	End Sub
	Private Sub tvwServerList_DragOver(sender As Object, e As System.Windows.Forms.DragEventArgs) Handles tvwServerList.DragOver
		If (e.Data.GetDataPresent("System.Windows.Forms.TreeNode", True) = False) Then Exit Sub
		Dim sNode As TreeNode = CType(e.Data.GetData("System.Windows.Forms.TreeNode"), TreeNode)		' Source Node
		Dim cTreeView As TreeView = CType(sender, TreeView)												' TreeView Control
		Dim pt As Point = CType(sender, TreeView).PointToClient(New Point(e.X, e.Y))
		Dim tNode As TreeNode = cTreeView.GetNodeAt(pt)													' Target (Parent) Node
		Dim bAllowed As Boolean = True

		frm_DragDrop.Location = New Point(e.X + 10, e.Y + 16)
		Dim sDDText As String = vbNullString
		Dim bDDMove As Boolean = True

		If (tNode IsNot Nothing) Then
			' Drag Over Existing group/server
			If (tNode.ImageKey.StartsWith("_16___Server") = True) Then tNode = tNode.Parent
			bDDMove = True
			sDDText = tNode.Text.Trim
			bAllowed = checkDropTarget(sNode, tNode)
		Else
			If (m_dragDropType = dragDropType.group) Then
				' No destination node (over blank area)
				bDDMove = False
				sDDText = "Create New Group"
				If ((sNode.Parent Is Nothing) AndAlso (tNode Is Nothing)) Then bAllowed = False
			Else
				' Change to error for servers over blank area
				bDDMove = False
				sDDText = "!-ERROR-!"
				bAllowed = False
			End If
		End If

		If (bAllowed = False) Then
			cTreeView.SelectedNode = Nothing
			frm_DragDrop.lblMoveNode.Text = "!-ERROR-!"
			frm_DragDrop.lblMoveTo.Visible = False
			Call frm_DragDrop.changeSize()
			Exit Sub
		End If

		frm_DragDrop.lblMoveTo.Visible = bDDMove
		frm_DragDrop.lblMoveNode.Text = sDDText
		Call frm_DragDrop.changeSize()

		If Not (cTreeView.SelectedNode Is tNode) Then
			cTreeView.SelectedNode = tNode
			Do Until tNode Is Nothing
				If (tNode Is sNode) Then
					e.Effect = DragDropEffects.None
					Exit Sub
				End If
				tNode = tNode.Parent
			Loop
		End If

		e.Effect = DragDropEffects.Move
	End Sub
	Private Sub tvwServerList_DragDrop(sender As Object, e As System.Windows.Forms.DragEventArgs) Handles tvwServerList.DragDrop

		Dim sDroppedFiles() As String = CType(e.Data.GetData(DataFormats.FileDrop), String())
		If ((sDroppedFiles IsNot Nothing) AndAlso (sDroppedFiles.Count > 0)) Then
			Call openSelectedFile(sDroppedFiles(0))
			Exit Sub
		End If

		Dim bAllowed As Boolean = True
		If (e.Data.GetDataPresent("System.Windows.Forms.TreeNode", True) = False) Then bAllowed = False
		If (e.Effect <> DragDropEffects.Move) Then bAllowed = False
		If (frm_DragDrop.lblMoveNode.Text = "!-ERROR-!") Then bAllowed = False
		If (bAllowed = False) Then
			frm_DragDrop.Visible = False
			tssInfo.Text = Ready
			Exit Sub
		End If

		Try
			Dim cTreeView As TreeView = CType(sender, TreeView)											' TreeView Control
			Dim sNode As TreeNode = CType(e.Data.GetData("System.Windows.Forms.TreeNode"), TreeNode)	' Source Node
			Dim tNode As TreeNode = cTreeView.SelectedNode												' Target (Parent) Node

			' Check the Target and Source Nodes for issues...
			If (checkDropTarget(sNode, tNode) = False) Then
				frm_DragDrop.Visible = False
				tssInfo.Text = Ready
				Exit Sub
			End If

			Dim sParent As String
			Dim sTarget As String
			If (sNode.Parent Is Nothing) Then sParent = "root" Else sParent = sNode.Parent.Name
			If (tNode Is Nothing) Then sTarget = Nothing Else sTarget = tNode.Name

			frm_DragDrop.Visible = False

			' DragDrop valid, need to check for duplicated resources and resolve conflicts...
			Dim bContinue As Boolean = True
			If (sNode.ImageKey.StartsWith("_16___Server") = False) Then
				If (sTarget IsNot Nothing) Then bContinue = checkDragDropResourceDuplication(sNode.Name, sTarget)
			End If

			If (bContinue = True) Then
				If (xml_setNewParent(sNode.Name, sParent, sTarget) = True) Then
					sNode.Remove()
					Application.DoEvents()
					If (sTarget IsNot Nothing) Then tNode.Nodes.Add(sNode) Else tvwServerList.Nodes.Add(sNode)
					sNode.EnsureVisible()
					cTreeView.SelectedNode = sNode

				Else
					MessageBox.Show(Me, "Failed to set new location, please try again.", APN, MessageBoxButtons.OK, MessageBoxIcon.Error)
					Call xmlLoadConfig()
				End If
			Else
				MessageBox.Show(Me, "Drag/drop operation cancelled by the user." & vbCrLf & "No resources have been removed.", APN, MessageBoxButtons.OK, MessageBoxIcon.Information)
			End If

		Catch ex As Exception
			frm_DragDrop.Visible = False
			MessageBox.Show(Me, "Error in drag/drop operation, please try again." & vbCrLf & ex.Message, APN, MessageBoxButtons.OK, MessageBoxIcon.Error)
		End Try
		tssInfo.Text = Ready
	End Sub

	Private Function checkDropTarget(ByVal sourceNode As TreeNode, ByVal targetNode As TreeNode) As Boolean
		If ((sourceNode.ImageKey.StartsWith("_16___Server") = True) AndAlso (targetNode Is Nothing)) Then Return False
		If ((targetNode Is Nothing) AndAlso (sourceNode.Parent Is Nothing)) Then Return False
		If ((targetNode IsNot Nothing) AndAlso (sourceNode.Parent IsNot Nothing) AndAlso (sourceNode.Parent.Equals(targetNode))) Then Return False
		If (sourceNode.Equals(targetNode) = True) Then Return False
		If (targetNode Is Nothing) Then Return True
		If (bReadOnlyMode = True) Then Return False

		' Can't drop over server...
		If (targetNode.ImageKey.StartsWith("_16___Server") = True) Then Return False

		' Check if existing nodes exist and check what they are...
		If (targetNode.GetNodeCount(False) > 0) Then
			If (m_dragDropType = dragDropType.server) Then
				' Moving Server... Does a GROUP already exist.?
				If (targetNode.FirstNode.ImageKey.StartsWith("_16___Group") = True) Then Return False
			Else
				' Moving Group... Does a SERVER already exist.?
				If (targetNode.FirstNode.ImageKey.StartsWith("_16___Server") = True) Then Return False
			End If
		End If

		' Are we within ourself...
		If (InStr(getFullPath(targetNode.Name, True, tvwServerList.PathSeparator), sourceNode.Name) > 0) Then Return False

		' Check group depth and make sure we are not over it..
		If (m_dragDropType = dragDropType.group) Then
			Dim iDepth As Integer
			If (checkGroupDepth(targetNode.FullPath, tvwServerList.PathSeparator, iDepth) = False) Then Return False
			Dim iSourcePathCount As Integer = findChildDepth(sourceNode, tvwServerList.PathSeparator)
			If ((iDepth + iSourcePathCount) >= sMaxGroupDepth) Then Return False
		End If

		Return True
	End Function

	Private Function checkDragDropResourceDuplication(ByVal sMovingGUID As String, ByVal sTargetGUID As String) As Boolean
		Dim rMoving As List(Of XmlElement) = xml_getResourceList(sMovingGUID, eDirectionalSearch.ToChildren, True)
		Dim rTarget As List(Of XmlElement) = xml_getResourceList(sTargetGUID, eDirectionalSearch.ToParents, True)
		Dim rSameDupes As New List(Of XmlElement)
		Dim rDiffDupes As New List(Of XmlElement)
		Dim bReturn As Boolean = True

		For Each eMoving As XmlElement In rMoving
			For Each eTarget As XmlElement In rTarget
				If (eMoving.Attributes("type").Value.Split("("c)(0).Trim = eTarget.Attributes("type").Value.Split("("c)(0).Trim) Then
					If (eMoving.Attributes("name").Value = eTarget.Attributes("name").Value) Then
						If (eMoving.Attributes("checking").Value = eTarget.Attributes("checking").Value) Then
							rSameDupes.Add(eMoving)
						Else
							rDiffDupes.Add(eMoving)
							rDiffDupes.Add(eTarget)
						End If
						Exit For
					End If
				End If
			Next
		Next

		If ((rDiffDupes.Count > 0) Or (rSameDupes.Count > 0)) Then
			Dim sMsg As String = "This drag/drop operation has duplicate resources." & vbCrLf
			sMsg = sMsg & "Details are listed below..." & vbCrLf & vbCrLf

			If (rSameDupes.Count = 1) Then
				sMsg = sMsg & "      There is 1 duplicate that matches." & vbCrLf
			Else
				sMsg = sMsg & "      There are " & (rSameDupes.Count) & " duplicates that match." & vbCrLf
			End If

			If (rDiffDupes.Count = 2) Then
				sMsg = sMsg & "      There is 1 duplicate that differs." & vbCrLf
			Else
				sMsg = sMsg & "      There are " & (rDiffDupes.Count / 2) & " duplicates that differ." & vbCrLf
			End If

			If (rSameDupes.Count > 0) Then sMsg = sMsg & vbCrLf & "The exact matches will be automatically removed."
			If (rDiffDupes.Count > 0) Then sMsg = sMsg & vbCrLf & "Click OK to select which resources to keep." Else sMsg = sMsg & vbCrLf & "Click OK to continue"

			Dim dResult As DialogResult = MessageBox.Show(Me, sMsg, APN, MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation)
			If (dResult = Windows.Forms.DialogResult.Cancel) Then Return False

			If (rDiffDupes.Count > 0) Then
				frmResourceDuplicationSelection.xDuplicates = rDiffDupes
				frmResourceDuplicationSelection.ShowDialog(Me)
				bReturn = frmResourceDuplicationSelection.bReturnValue
				frmResourceDuplicationSelection.Dispose()
			End If

			If (bReturn = True) Then
				For Each eDupe As XmlElement In rSameDupes
					Call xml_setRemoveResource(eDupe.Attributes("guid").Value)
				Next
			End If
		End If
		Return bReturn
	End Function

	' #############################################################################################

	Private Sub ResetSplitterBarToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles ResetSplitterBarToolStripMenuItem.Click
		conSplitContainer.SplitterDistance = 200
		picHeaderBar.Focus()
	End Sub

	Private Sub conSplitContainer_MouseUp(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles conSplitContainer.MouseUp
		If (e.Button <> Windows.Forms.MouseButtons.Right) Then Exit Sub
		With mContextResource.Items
			.Clear()
			.Add("Reset Splitter Position", My.Resources._16___ChangeState.ToBitmap, AddressOf ResetSplitterBarToolStripMenuItem_Click)
		End With
		mContextResource.Show(conSplitContainer, New Point(e.X - 32, e.Y))
	End Sub

	Private Sub ConfigureServerConnectionsToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles ConfigureServerConnectionsToolStripMenuItem.Click
		frmConnectionEditor.ShowDialog(Me)
	End Sub

	Private Sub mnuContext_xxx_Closing(sender As Object, e As System.Windows.Forms.ToolStripDropDownClosingEventArgs) Handles mContextTree.Closing, mContextResource.Closing
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
			tssInfo.Text = Ready
		Catch ex As Exception
		End Try
	End Sub

	Private Sub ExpandAllToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles ExpandAllToolStripMenuItem.Click
		Call lstResources.SetGroupState(ListViewGroupState.Collapsible Or ListViewGroupState.Normal)
	End Sub

	Private Sub CollapseAllToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles CollapseAllToolStripMenuItem.Click
		Call lstResources.SetGroupState(ListViewGroupState.Collapsible Or ListViewGroupState.Collapsed)
	End Sub

	Private Sub ResetColumnWidthsToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles ResetColumnWidthsToolStripMenuItem.Click
		Dim iCnt As Integer = 0
		For Each col As ColumnHeader In lstResources.Columns
			col.Width = iColumnWidths(iCnt)
			iCnt = iCnt + 1
		Next
		lstResources.Columns(0).Width = lstResources.Width - iColumnWidths.Sum - SystemInformation.VerticalScrollBarWidth - 4
	End Sub

	Private Sub NetworkAvailabilityToolStripStatus_Click(sender As System.Object, e As System.EventArgs) Handles NetworkAvailabilityToolStripStatus.Click
		Dim sMsg As String = "Network availability has been lost." & vbCrLf & sNetworkAvailability
		MessageBox.Show(Me, sMsg, APN, MessageBoxButtons.OK, MessageBoxIcon.Information)
	End Sub

	Private Sub NetworkAvailabilityToolStripStatus_MouseEnter(sender As Object, e As System.EventArgs) Handles NetworkAvailabilityToolStripStatus.MouseEnter
		Me.Cursor = Cursors.Hand
	End Sub

	Public Function drawFullPathHeader(ByVal node As TreeNode, Optional ByVal MousePosition As Integer = -1) As TreeNode
		Dim imgHeader As Image = New Bitmap(picHeaderBar.Width, picHeaderBar.Height)
		Dim returnNode As TreeNode = Nothing

		Me.Cursor = Cursors.Default
		Using g As Graphics = Graphics.FromImage(imgHeader)
			g.Clear(lstResources.BackColor)

			If (node IsNot Nothing) Then
				Dim nNodes As New List(Of TreeNode)
				Dim nNode As TreeNode = node
				Do
					Try
						nNodes.Insert(0, nNode)
						nNode = nNode.Parent
					Catch
						nNode = Nothing
					End Try
				Loop Until nNode Is Nothing

				Dim iMin As Integer = 4
				Dim iPos As Integer = 4
				Dim iText As Integer = 0
				For Each n As TreeNode In nNodes
					g.DrawIcon(CType(My.Resources.ResourceManager.GetObject(n.ImageKey), Icon), New Rectangle(iPos, 3, 16, 16))
					iMin = iPos
					iPos = iPos + 19
					iText = iPos

					' Draw separator between text...
					If (n.Text <> node.Text) Then
						iPos = iPos + MeasureDisplayStringWidth(g, n.Text, sysFont)
						g.DrawIcon(My.Resources.Header_Separator, New Rectangle(iPos - 21, 0, 32, 23))
						iPos = iPos + 16
					Else
						iPos = 0
					End If

					' Draw text on top of header_separator...
					Dim iTextTop As Integer = CInt((picHeaderBar.Height - sysFont.Height) / 2) - 1
					If ((MousePosition > (iMin)) And (MousePosition < (iPos - 16))) Then
						' BLUE TEXT
						g.DrawString(n.Text, sysFont, SystemBrushes.MenuHighlight, New Point(iText, iTextTop))
						Me.Cursor = Cursors.Hand
						returnNode = n
					Else
						' BLACK TEXT
						g.DrawString(n.Text, sysFont, SystemBrushes.WindowText, New Point(iText, iTextTop))
					End If
				Next

				nNode = Nothing
				nNodes = Nothing
			End If
			picHeaderBar.Image = imgHeader
		End Using
		imgHeader = Nothing
		Return returnNode
	End Function

	Public Sub ShowHideFullPathHeader()
		Dim bEnabled As Boolean = ShowFullPathToolStripMenuItem.Checked

		ShowFullPathToolStripMenuItem.Checked = Not bEnabled
		lstHeaderBar.Visible = Not bEnabled
		picHeaderBar.Visible = Not bEnabled

		If (bEnabled = True) Then
			lstResources.Top = 0
			ShowFullPathToolStripMenuItem.Image = My.Resources.FullPath_Show_ico.ToBitmap
		Else
			lstResources.Top = CInt(IIf(bUseVisualStyles, 24, 25))
			ShowFullPathToolStripMenuItem.Image = My.Resources.FullPath_Hide_ico.ToBitmap
		End If

		lstResources.Height = conSplitContainer.Panel2.Height - lstResources.Top
	End Sub

	Private Sub ShowFullPathToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles ShowFullPathToolStripMenuItem.Click
		Call ShowHideFullPathHeader()
	End Sub

	Private Sub tssInfo_MouseUp(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles tssInfo.MouseUp
		If (e.Button = MouseButtons.Right) Then Application.DoEvents()
	End Sub

	Private Sub ConfigurationSettingsToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles SettingsToolStripMenuItem.Click
		frmOptions.ShowDialog(Me)
	End Sub

	Private Sub ToolStripMenuDescriptionExpandCollapse_Click(sender As System.Object, e As System.EventArgs) Handles ToolStripMenuDescriptionExpandCollapse.Click
		Dim p0 As New Padding(0, 1, 0, 1)
		Dim p1 As New Padding(0, 1, 0, 0)

		With ToolStripMenuDescriptionExpandCollapse
			If (.Tag.ToString = "Show") Then
				conSplitLeft.Panel2Collapsed = False
				.Image = My.Resources._16___Description___Collapse.ToBitmap
				.Margin = p1
				.Tag = "Hide"
				ToolStripMenuEditDescription.Margin = p1
			Else
				conSplitLeft.Panel2Collapsed = True
				.Image = My.Resources._16___Description___Expand.ToBitmap
				.Margin = p0
				.Tag = "Show"
				ToolStripMenuEditDescription.Margin = p0
			End If
		End With

		p0 = Nothing
		p1 = Nothing
	End Sub

	Private Sub tvwDescription_Resize(sender As Object, e As System.EventArgs) Handles tvwDescription.Resize
		txtDescription.Size = New Size(tvwDescription.Width - 4, tvwDescription.Height - 4)
	End Sub

	Private Sub txtDescription_GotFocus(sender As Object, e As System.EventArgs) Handles txtDescription.GotFocus
		picHeaderBar.Focus()
	End Sub

	Private Sub txtDescription_MouseDown(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles txtDescription.MouseDown
		picHeaderBar.Focus()
	End Sub

	Private Sub ToolStripMenuEditDescription_Click(sender As System.Object, e As System.EventArgs) Handles ToolStripMenuEditDescription.Click
		If (tvwServerList.Nodes.Count = 0) Then Exit Sub
		If (tvwServerList.SelectedNode Is Nothing) Then Exit Sub
		If (tvwServerList.SelectedNode.Tag.ToString = "GROUP") Then
			Call onClick_groupProperties(sender, e)
		Else
			Call onClick_serverProperties(sender, e)
		End If
	End Sub

	Private Sub picHeaderBar_MouseMove(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles picHeaderBar.MouseMove
		Dim nNode As TreeNode = tvwServerList.SelectedNode
		If nNode Is Nothing Then Exit Sub

		If (tvwServerList.SelectedNode.ImageKey.StartsWith("_16___Server") = True) Then nNode = tvwServerList.SelectedNode.Parent
		Call drawFullPathHeader(nNode, e.Location.X)
	End Sub

	Private Sub picHeaderBar_MouseUp(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles picHeaderBar.MouseUp
		Dim nNode As TreeNode = tvwServerList.SelectedNode
		If nNode Is Nothing Then Exit Sub

		If (tvwServerList.SelectedNode.ImageKey.StartsWith("_16___Server") = True) Then nNode = tvwServerList.SelectedNode.Parent
		nNode = drawFullPathHeader(nNode, e.Location.X)
		If nNode IsNot Nothing Then tvwServerList.SelectedNode = nNode
	End Sub

	Private Sub AssociateSSCFilesToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles AssociateSSCFilesToolStripMenuItem.Click
		If (bIsAdminMode = False) Then Exit Sub

		' Delete any old references first...
		Try
			Dim rExt As RegistryKey = My.Computer.Registry.CurrentUser.OpenSubKey("Software\Microsoft\Windows\CurrentVersion\Explorer\FileExts")
			Dim sExt() As String = rExt.GetSubKeyNames
			If sExt.Contains(".ssc") Then
				My.Computer.Registry.CurrentUser.DeleteSubKeyTree("Software\Microsoft\Windows\CurrentVersion\Explorer\FileExts\.ssc")
			End If
		Catch
		End Try

		' Add the new association...
		Try
			My.Computer.Registry.ClassesRoot.CreateSubKey(".ssc").SetValue("", "SSCFile", Microsoft.Win32.RegistryValueKind.String)
			My.Computer.Registry.ClassesRoot.CreateSubKey("SSCFile").SetValue("", "Server System Checker File", Microsoft.Win32.RegistryValueKind.String)
			My.Computer.Registry.ClassesRoot.CreateSubKey("SSCFile\shell\open\command").SetValue("", Application.ExecutablePath & " ""%l"" ", Microsoft.Win32.RegistryValueKind.String)
			My.Computer.Registry.ClassesRoot.CreateSubKey("SSCFile\DefaultIcon").SetValue("", Application.ExecutablePath & ",1", Microsoft.Win32.RegistryValueKind.String)

			' Funky Sub to force refresh the icon associations
			Call SHChangeNotify(&H8000000, 0, Nothing, Nothing)
			MessageBox.Show(Me, "File association created successfully", APN, MessageBoxButtons.OK, MessageBoxIcon.Information)

		Catch ex As Exception
			MessageBox.Show(Me, "Error creating file association:" & vbCrLf & ex.Message, APN, MessageBoxButtons.OK, MessageBoxIcon.Error)
		End Try
	End Sub

	Private Sub FindServerToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles FindServerToolStripMenuItem.Click
		If (SearchToolStripMenuItem.Visible = True) Then
			Me.Cursor = Cursors.WaitCursor
			frmFindServer.cTreeView = tvwServerList
			frmFindServer.Show(Me)
			Me.Cursor = Cursors.Default
		End If
	End Sub

	Private Sub FindResourceToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles FindResourceToolStripMenuItem.Click
		If (SearchToolStripMenuItem.Visible = True) Then
			Me.Cursor = Cursors.WaitCursor
			frmFindResource.ShowDialog(Me)
			Me.Cursor = Cursors.Default
		End If
	End Sub

	Private Sub tssInfo_Click(sender As System.Object, e As System.EventArgs) Handles tssInfo.Click
		Application.DoEvents()
	End Sub

	Private Sub ShowServerStatsToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles ShowServerStatsToolStripMenuItem.Click
		frmServerStats.sGroupGUID = "-1"
		frmServerStats.ShowDialog(Me)
	End Sub
End Class