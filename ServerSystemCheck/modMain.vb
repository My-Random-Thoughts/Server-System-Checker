Option Explicit On
Imports Microsoft.Win32
Imports System.Drawing.Drawing2D
Imports System.Drawing.Imaging
Imports System.IO
Imports System.Management
Imports System.Runtime.InteropServices
Imports System.ServiceProcess
Imports System.Text
Imports System.Xml
Imports WUApiLib
Imports System.Net

Module modMain
	Public bCommandLine As Boolean							' Are we running from a command line.?
	Public sConfigFile As String
	Public sCommand_Group As String
	Public bCommand_BasicReport As Boolean

	Public Const sServerSubstitution As String = "#server#"	' Server Substitution
	Public Const iListViewIconWidth As Integer = 20			' Icon width spacing (icons are 16w)
	Public Const iListViewLineHeight As Integer = 18		' Line height (similar to above) (icons are 16h)
	Public Const sMaxGroupDepth As Integer = 3				' node1/node2/node3/node4/servers  (count number of separators) - applies to GROUP depth only
	Public Const LH As String = "Localhost"					'      ^1    ^2    ^3    ^-
	Public APN As String = " " & Application.ProductName
	Public Const iLength_GroupName As Integer = 32
	Public Const iLength_Description As Integer = 250

	Public Enum ServerScanResults
		None = 0		' \
		Checking = 1	'  |
		Pass = 2		'  |
		Fail = 3		'  |	These are the same index numbers in the
		Unknown = 4		'  |	image list in frmScan.frmScan_Load
		Warning = 5		'  |
		Skipped = 6		'  |
		Exception = 7	' /
		StringText = 99	' >		Used to display subitem text instead of icon
	End Enum

	Private Const GWL_STYLE = (-16)
	Private Const WS_HSCROLL = &H100000
	Private Const WS_VSCROLL = &H200000
	<DllImport("user32.dll", EntryPoint:="GetWindowLong")> _
	Private Function GetWindowLongPtr32(ByVal hWnd As Long, ByVal nIndex As Long) As Long
	End Function
	<DllImport("user32.dll", EntryPoint:="GetWindowLongPtr")> _
	Private Function GetWindowLongPtr64(ByVal hWnd As Long, ByVal nIndex As Long) As Long
	End Function
	Private Function GetWindowLong(ByVal hWnd As Long, ByVal nIndex As Long) As Long
		If (IntPtr.Size = 8) Then Return GetWindowLongPtr64(hWnd, nIndex) Else Return GetWindowLongPtr32(hWnd, nIndex)
	End Function
	<DllImport("shell32")> _
	Public Sub SHChangeNotify(ByVal wEventId As Integer, ByVal flags As Integer, ByVal item1 As IntPtr, ByVal item2 As IntPtr)
	End Sub

	Private Declare Function IsThemeActive Lib "uxtheme.dll" () As Boolean
	Private Declare Function IsAppThemed Lib "uxtheme.dll" () As Boolean
	Public bUseVisualStyles As Boolean = (IsThemeActive And IsAppThemed)

	Public bIsAdminMode As Boolean = False
	Public bReadOnlyMode As Boolean = False

	' Settings...
	Public m_GroupView As String
	Public m_ResourceView As String
	Public m_UseColouredGroups As Boolean
	Public m_UppercaseServerNames As Boolean
	Public m_ShowGroupCounts As Boolean
	Public m_PingServersBeforeScan As Boolean
	Public m_IconGroup As String
	' ...Settings

	Public iGroupIconSetCount As Integer = 4
	Public lResourceConflicts As List(Of String)  ' LIST  : sGroupGUID | sResourceGUID
	Public m_findChildNode_depthNode As TreeNode
	Public m_findChildNode_findNode As TreeNode

	Public m_NetworkAvailability As Boolean = My.Computer.Network.IsAvailable
	Public Const sNetworkAvailability As String = "The following functions will be disabled..." & vbCrLf & _
												  vbCrLf & _
												  "-   Adding new servers via an Active Directory scan," & vbCrLf & _
												  "-   New server IP addresses will not be resolved," & vbCrLf & _
												  "-   Adding any new resources from scanning network servers," & vbCrLf & _
												  "-   Retrieving server properties," & vbCrLf & _
												  "-   Scanning servers" & vbCrLf & _
												  vbCrLf & _
												  "Please connect back to the network to re-enable these functions."

	<DllImport("uxtheme.dll", CharSet:=CharSet.Unicode)> Public Function SetWindowTheme(ByVal hWnd As IntPtr, ByVal pszSubAppName As String, ByVal pszSubIdList As String) As Integer
	End Function
	<DllImport("user32.dll")> Public Function SendMessage(ByVal window As IntPtr, ByVal message As Integer, ByVal wParam As Integer, ByVal lParam As IntPtr) As Integer
	End Function

	' Used for hiding the focus rectangle
	Public Function MakeLong(ByVal wLow As Integer, ByVal wHigh As Integer) As Long
		MakeLong = wHigh * &H10000 + wLow
	End Function
	Public Const WM_ChangeUIState As Long = &H127
	Public Const UIS_HideRectangle As Integer = &H1
	Public Const UIS_ShowRectangle As Integer = &H2
	Public Const UISF_FocusRectangle As Integer = &H1

	' List taken from a Windows 7 laptop, and edited slightly...
	Private sServiceList() As String = {"Application Experience", "Application Identity", "Application Information", "Application Layer Gateway Service",
	   "Application Management", "ASP.NET State Service", "Audio Service", "Background Intelligent Transfer Service", "Base Filtering Engine",
	   "BitLocker Drive Encryption Service", "Block Level Backup Engine Service", "Bluetooth Support Service", "BranchCache", "Certificate Propagation",
	   "CNG Key Isolation", "COM+ Event System", "COM+ System Application", "Computer Browser", "Credential Manager", "Cryptographic Services",
	   "DCOM Server Process Launcher", "Desktop Window Manager Session Manager", "DHCP Client", "Diagnostic Policy Service", "Diagnostic Service Host",
	   "Diagnostic System Host", "Disk Defragmenter", "Distributed Link Tracking Client", "Distributed Transaction Coordinator", "DNS Client",
	   "Encrypting File System (EFS)", "Extensible Authentication Protocol", "Function Discovery Provider Host", "Function Discovery Resource Publication",
	   "Group Policy Client", "Health Key and Certificate Management", "Human Interface Device Access", "IKE and AuthIP IPsec Keying Modules",
	   "Interactive Services Detection", "Internet Connection Sharing (ICS)", "Internet Pass-Through Service", "IP Helper", "IPsec Policy Agent",
	   "KtmRm for Distributed Transaction Coordinator", "Link-Layer Topology Discovery Mapper", "Microsoft iSCSI Initiator Service", "Microsoft Software Shadow Copy Provider",
	   "Multimedia Class Scheduler", "Net.Msmq Listener Adapter", "Net.Pipe Listener Adapter", "Net.Tcp Listener Adapter", "Net.Tcp Port Sharing Service",
	   "Netlogon", "Network Access Protection Agent", "Network Connections", "Network List Service", "Network Location Awareness", "Network Store Interface Service",
	   "Peer Name Resolution Protocol", "Peer Networking Grouping", "Peer Networking Identity Manager", "Performance Counter DLL Host", "Performance Logs & Alerts",
	   "Plug and Play", "Portable Device Enumerator Service", "Power", "Print Spooler", "Protected Storage", "Remote Access Auto Connection Manager",
	   "Remote Access Connection Manager", "Remote Desktop Configuration", "Remote Desktop Services", "Remote Desktop Services UserMode Port Redirector",
	   "Remote Procedure Call (RPC)", "Remote Procedure Call (RPC) Locator", "Remote Registry", "Routing and Remote Access", "RPC Endpoint Mapper", "Secondary Logon",
	   "Secure Socket Tunnelling Protocol Service", "Security Accounts Manager", "Security Centre", "Server", "Shell Hardware Detection", "Smart Card",
	   "Smart Card Removal Policy", "SMS Agent Host", "SMS Task Sequence Agent", "SNMP Trap", "Software Protection", "Storage Service", "Superfetch",
	   "System Event Notification Service", "Task Scheduler", "TCP/IP NetBIOS Helper", "Telephony", "Themes", "Thread Ordering Server", "TPM Base Services", "UCMS",
	   "UPnP Device Host", "User Profile Service", "Virtual Disk", "Volume Shadow Copy", "WebClient", "Windows Activation Technologies Service", "Windows Audio",
	   "Windows Audio Endpoint Builder", "Windows Backup", "Windows Biometric Service", "Windows CardSpace", "Windows Colour System",
	   "Windows Defender", "Windows Driver Foundation - User-mode Driver Framework", "Windows Error Reporting Service", "Windows Event Collector",
	   "Windows Event Log", "Windows Firewall", "Windows Font Cache Service", "Windows Image Acquisition (WIA)", "Windows Installer", "Windows Management Instrumentation",
	   "Windows Modules Installer", "Windows Presentation Foundation Font Cache 3.0.0.0", "Windows Remote Management (WS-Management)", "Windows Search",
	   "Windows Time", "Windows Update", "WinHTTP Web Proxy Auto-Discovery Service", "Wired AutoConfig", "WLAN AutoConfig", "WMI Performance Adapter",
	   "Workstation", "WWAN AutoConfig"}

	'                                          0        1       2       3       4        5         6       7      8        9
	Private sGroupIconColours() As String = {"Black", "Blue", "Cyan", "Grey", "Green", "Orange", "Pink", "Red", "White", "Yellow"}
	Public iIconColourCount As Integer = sGroupIconColours.Count

	' Custom colours for Drive Free Space Thresholds...
	Public cColorR As Color = Color.FromArgb(255, 250, 108, 135)	' RED:    Critical
	Public cColorY As Color = Color.FromArgb(255, 250, 239, 108)	' YELLOW: Warning
	Public cColorG As Color = Color.FromArgb(255, 112, 250, 108)	' GREEN:  OK
	Public bColorR As Brush = New SolidBrush(cColorR)
	Public bColorY As Brush = New SolidBrush(cColorY)
	Public bColorG As Brush = New SolidBrush(cColorG)

	''' <summary>
	''' getGroupColour
	''' </summary>
	''' <param name="iColourIndex">Integer: Indexed item of sGroupIconColours</param>
	''' <param name="bIgnoreSetting">Boolean: Ignore 'm_UseColouredGroups' setting</param>
	''' <returns>Correct colour name, or "Yellow"</returns>
	''' <remarks></remarks>
	Public Function getGroupColour(ByVal iColourIndex As Integer, ByVal bIgnoreSetting As Boolean, Optional ByVal sOverrideGroup As String = Nothing) As String
		Dim sGroup As String = m_IconGroup & "___"
		If (sOverrideGroup IsNot Nothing) Then sGroup = sOverrideGroup & "___"
		If ((bIgnoreSetting = True) OrElse (m_UseColouredGroups = True)) Then Return sGroup & sGroupIconColours(iColourIndex)
		Return sGroup & "Yellow"	' DEFAULT Group Colour
	End Function

	''' <summary>
	''' Takes an input string and converts to a Green, Orange/Yellow or Red colour
	''' </summary>
	''' <param name="sInput">STRING: Input string to check against</param>
	''' <returns>COLOR: Required colour for given string</returns>
	''' <remarks></remarks>
	Public Function getSubItemColour(ByVal sInput As String) As Color
		' Colour return options for Services, Hotfixes and Registry Results, an other colour coded items
		Select Case sInput
			Case "Running", "Installed", "OK"
				Return Color.DarkGreen

			Case "Paused", "Pause Pending", "Continue Pending", "Start Pending", "Stop Pending", "Warning"
				Return Color.DarkOrange

			Case "Stopped", "Not Installed", "Fail", "Missing", "Error"
				Return Color.Red

			Case "Unknown", "Disabled"
				Return Color.Gray

			Case Else
				Return SystemColors.WindowText
		End Select
	End Function

	' #############################################################################################

	''' <summary>
	''' Takes a listview control, creates and orders the contents into groups according to the column selected
	''' </summary>
	''' <param name="lvw">LISTVIEW: Control to use</param>
	''' <param name="sColumn">STRING: Column to group by</param>
	''' <param name="UseTag">BOOLEAN: Use item's tag or text property</param>
	''' <param name="bShowCounts">BOOLEAN: Include (x) count in group name</param>
	''' <remarks></remarks>
	Public Sub doGroupListView(ByVal lvw As ListView, ByVal sColumn As String, ByVal UseTag As Boolean, ByVal bShowCounts As Boolean)
		If (bCommandLine = True) Then Exit Sub
		If (lvw Is Nothing) Then Exit Sub
		If (lvw.Items.Count = 0) Then Exit Sub

		Dim sBlank As String = "(Not Specified)"
		Dim iCol As Integer = lvw.Columns.IndexOfKey(sColumn)
		Dim gGroups As New List(Of String)

		For Each lItem As ListViewItem In lvw.Items
			If (lItem.SubItems(iCol).Text = vbNullString) Then
				gGroups.Add(sBlank)
			Else
				If (UseTag = True) Then
					If ((lItem.SubItems(iCol).Text.StartsWith("ICON:")) Or (lItem.SubItems(iCol).Text.StartsWith("BOTH:"))) Then
						gGroups.Add(lItem.SubItems(iCol).Text.Substring(5) & "|" & lItem.SubItems(iCol).Tag.ToString)
					Else
						gGroups.Add(lItem.SubItems(iCol).Tag.ToString & "|" & lItem.SubItems(iCol).Text)
					End If
				Else
					gGroups.Add(lItem.SubItems(iCol).Text)
				End If
			End If
		Next

		Dim nGroup As ListViewGroup
		Dim dGroups As IEnumerable(Of String) = gGroups.Distinct
		gGroups = Nothing
		lvw.Groups.Clear()

		For Each gItem As String In dGroups
			If (UseTag = True) Then
				nGroup = New ListViewGroup(Split(gItem, "|")(0), Split(gItem, "|")(1))
			Else
				nGroup = New ListViewGroup(gItem, gItem)
			End If
			nGroup.HeaderAlignment = HorizontalAlignment.Left
			lvw.Groups.Add(nGroup)
			nGroup = Nothing
		Next
		dGroups = Nothing

		For Each lItem As ListViewItem In lvw.Items
			If (lItem.SubItems(iCol).Text = vbNullString) Then
				lItem.Group = lvw.Groups(sBlank)
			Else
				If (UseTag = True) Then
					If ((lItem.SubItems(iCol).Text.StartsWith("ICON:")) Or (lItem.SubItems(iCol).Text.StartsWith("BOTH:"))) Then
						lItem.Group = lvw.Groups(lItem.SubItems(iCol).Text.Substring(5))
					Else
						lItem.Group = lvw.Groups(lItem.SubItems(iCol).Tag.ToString)
					End If
				Else
					lItem.Group = lvw.Groups(lItem.SubItems(iCol).Text)
				End If
			End If
		Next

		CType(lvw, ListView_GroupSorter).SortGroups(True, UseTag)
	End Sub

	' #############################################################################################

	''' <summary>
	''' Redraws the selected TreeView control to update the nodes for uppercase or lowercase text and group colour
	''' </summary>
	''' <param name="tvControl">TREEVIEW: Control to use</param>
	''' <param name="bUppercase">BOOLEAN: Use uppercase or lowercase text</param>
	''' <remarks></remarks>
	Public Sub doRedrawTreeNodes(ByVal tvControl As TreeView, ByVal bUppercase As Boolean)
		For Each tvNode As TreeNode In tvControl.Nodes
			Call getSubNodes(tvNode, bUppercase)
		Next
	End Sub
	Private Sub getSubNodes(ByVal tvNode As TreeNode, ByVal bUppercase As Boolean)
		If (tvNode.Tag.ToString <> "GROUP") Then
			tvNode.Text = IIf(bUppercase, tvNode.Text.ToUpper, tvNode.Text.ToLower).ToString
		Else
			Dim sGUID As String = tvNode.Name
			If ((tvNode.ImageKey = "LOST_AND_FOUND") OrElse (tvNode.Name.StartsWith("DYN-"))) Then
				Application.DoEvents()
			Else
				Dim iColr As Integer = xml_getGroupIconColour(sGUID)
				tvNode.ImageKey = "_16___Group___" & getGroupColour(iColr, False)
				tvNode.SelectedImageKey = tvNode.ImageKey
			End If
		End If
		For Each tvSubNode As TreeNode In tvNode.Nodes
			getSubNodes(tvSubNode, bUppercase)
		Next
	End Sub

	' #############################################################################################

	''' <summary>
	''' Checks to see if selected node is a Server item or a Group item
	''' </summary>
	''' <param name="tvwNode">TREENODE: Selected TreeViewItem </param>
	''' <returns>TREENODE: Selected group item (or parent if server is selected)</returns>
	''' <remarks></remarks>
	Public Function getGroupNode(ByVal tvwNode As TreeNode) As TreeNode
		If (tvwNode Is Nothing) Then Return Nothing
		Select Case tvwNode.Tag.ToString
			Case "GROUP" : Return tvwNode
			Case "SERVER", "SERVWP" : Return tvwNode.Parent
			Case Else : Return Nothing
		End Select
	End Function

	''' <summary>
	''' Returns the path (as NAMEs or as GUIDs) of the currently selected group
	''' </summary>
	''' <param name="sGroupGUID">STRING: Group GUID to look at</param>
	''' <param name="bReturnGUID">BOOLEAN: Return GUID or NAME</param>
	''' <returns>STRING: Returns GUID or NAME</returns>
	''' <remarks></remarks>
	Public Function getFullPath(ByVal sGroupGUID As String, ByVal bReturnGUID As Boolean, ByVal sSeperator As String) As String

		Dim sFullPath As String = vbNullString
		Dim sPath As String
		Dim nNode As XmlNode
		Dim pNode As XmlNode

		sPath = "descendant::group[@guid='" & sGroupGUID & "']"
		nNode = xmlDoc.SelectSingleNode(sPath)
		If (nNode Is Nothing) Then Return Nothing
		Do
			Dim sGNI As String
			If (bReturnGUID = True) Then sGNI = "guid" Else sGNI = "name"
			sFullPath = nNode.Attributes.ItemOf(sGNI).Value & sSeperator & sFullPath
			If ((nNode.ParentNode Is Nothing) Or (nNode.ParentNode.Attributes.ItemOf("guid").Value = "root")) Then Exit Do
			pNode = nNode.ParentNode
			nNode = pNode
		Loop
		sFullPath = sFullPath.TrimEnd(CChar("/"))
		Return sFullPath
	End Function

	' #############################################################################################
	' #############################################################################################

	''' <summary>
	''' Checks a Tree/List View object for a specific string in an items name
	''' </summary>
	''' <param name="oObject">TREE/LIST VIEW: Object to scan for GUID</param>
	''' <param name="sGUID">STRING: GUID to check for</param>
	''' <returns>STRING: A valid GUID to use</returns>
	''' <remarks>Returns a valid GUID, either the one given or a new unique ones</remarks>
	Public Function checkGUID(ByVal oObject As TreeView, ByVal sGUID As String) As String
		If ((sGUID = vbNullString) OrElse (oObject.Nodes.Find(sGUID, True).Count > 0)) Then
			sGUID = checkGUID(oObject, Guid.NewGuid.ToString)
		End If
		Return sGUID
	End Function
	Public Function checkGUID(ByVal oObject As ListView, ByVal sGUID As String) As String
		If ((sGUID = vbNullString) OrElse (oObject.Items.Find(sGUID, False).Count > 0)) Then
			sGUID = checkGUID(oObject, Guid.NewGuid.ToString)
		End If
		Return sGUID
	End Function

	''' <summary>
	''' Adds all the resources within the input xResources to the resources listview
	''' </summary>
	''' <param name="xResources">LIST(Of XmlElement): Input list of XML Elements</param>
	''' <param name="sLevel">STRING: Current parent level of xResources list</param>
	''' <remarks></remarks>
	Public Sub addResourceList(xResources As List(Of XmlElement), sLevel As String)
		Dim lItem As ListViewItem
		Dim sChecking As String
		Dim iLevel As Integer = 0
		Dim cColour As Color

		For Each eNode As XmlElement In xResources
			lItem = New ListViewItem(eNode.Attributes.ItemOf("name").Value)

			Dim sGUID As String = eNode.Attributes.ItemOf("guid").Value
			Dim sNewGUID As String = sGUID
			sNewGUID = checkGUID(frmMain.lstResources, sGUID)
			If (sGUID <> sNewGUID) Then
				Call xml_setNewGUID(sGUID, sNewGUID)
			End If

			lItem.Name = sNewGUID
			sChecking = eNode.Attributes.ItemOf("checking").Value

			Select Case eNode.Attributes.ItemOf("type").Value
				Case "Windows Service"
					lItem.ImageKey = "_16___Resource___Services"
					If (InStr(sChecking, " (") > 0) Then
						cColour = getSubItemColour(sChecking.Split("("c)(0).Trim)
					Else
						cColour = getSubItemColour(sChecking)
						sChecking = sChecking & " (OK)"
					End If

				Case "Hotfix Patch"
					lItem.ImageKey = "_16___Resource___Hotfix"
					cColour = getSubItemColour(sChecking)

				Case "Eventlog Scan"
					lItem.ImageKey = "_16___Resource___Eventlog"
					cColour = SystemColors.WindowText

				Case "Registry Scan"
					lItem.ImageKey = "_16___Resource___Registry"
					cColour = getSubItemColour(Split(sChecking, ", ")(2))

				Case "File Check"
					lItem.ImageKey = "_16___Resource___File"
					cColour = SystemColors.WindowText

				Case "Free Space Threshold"
					lItem.ImageKey = "_16___Resource___Drive"
					Dim sData() As String = sChecking.Split("|"c)
					sChecking = "Critical: " & sData(0) & ", Warning: " & sData(1)
					If (sData.Count > 2) Then sChecking = sChecking & ", " & sData(2) & " Non-System Drives"
					cColour = SystemColors.WindowText

				Case "WMI Query"
					lItem.ImageKey = "_16___Resource___WMIQuery"
					cColour = SystemColors.WindowText

				Case Else
					lItem.ImageKey = "_16___Eventlog___Critical"		' Just in case.!
					cColour = SystemColors.WindowText
			End Select

			lItem.UseItemStyleForSubItems = False
			lItem.SubItems.Add(eNode.Attributes.ItemOf("type").Value)
			lItem.SubItems.Add(cleanString(sChecking, {"  "}, {" "}), cColour, SystemColors.Window, SystemFonts.DefaultFont)
			lItem.SubItems.Add(sLevel, SystemColors.ButtonShadow, SystemColors.Window, SystemFonts.DefaultFont).Tag = sLevel
			'lItem.SubItems(3).Tag = sLevel

			frmMain.lstResources.Items.Add(lItem)
		Next
	End Sub

	''' <summary>
	''' Procedure to show or hide the scrollbars in a multi-line textbox, purely for shininess
	''' </summary>
	''' <param name="tTextBox">TEXTBOX: Control to change</param>
	''' <remarks></remarks>
	Public Sub txtShowHideScrollbars(ByVal tTextBox As TextBox)
		Dim StringSize As SizeF = TextRenderer.MeasureText(tTextBox.Text, tTextBox.Font)
		If (StringSize.Height > tTextBox.Height) Then
			tTextBox.ScrollBars = ScrollBars.Vertical
			tTextBox.ScrollToCaret()
		Else
			tTextBox.ScrollBars = ScrollBars.None
		End If
	End Sub

	' #############################################################################################

	''' <summary>
	''' Repeats a given string a specific number of times
	''' </summary>
	''' <param name="iCount">INTEGER: Number of time to repeat the input string</param>
	''' <param name="sInput">STRING: The input string to repeat</param>
	''' <returns>STRING: The given string repleated the specific number of times</returns>
	''' <remarks></remarks>
	Public Function StringRepeat(ByVal iCount As Integer, ByVal sInput As String) As String
		Dim totalLength As Integer = (sInput.Length * iCount)
		Dim resultString As StringBuilder = New StringBuilder(totalLength)
		Dim i As Integer = 0
		Do While (i < iCount)
			resultString.Append(sInput)
			i = (i + 1)
		Loop
		Return resultString.ToString
	End Function

	' #############################################################################################

	''' <summary>
	''' Generates data for use in a new configuration file
	''' </summary>
	''' <remarks></remarks>
	Public Sub prePopulateData()
		Dim tmpGUID_1 As String
		Dim tmpGUID_2 As String
		Dim tmpGUID_3 As String
		'
		' Server Group 1
		'     Domain Controllers
		'         []dc-1
		'         []dc-2
		'     Management Servers
		' Server Group 2
		'     Terminal Servers
		'         Development
		'             []ts-dev-1
		'             []ts-dev-2
		'         Pre-Prod
		'             []ts-tst-1
		'             []ts-tst-2
		'         Production
		'             []ts-prod-1
		'             []ts-prod-2
		'             []ts-prod-3
		'
		m_prePopulating = True

		tmpGUID_1 = Guid.NewGuid.ToString
		tmpGUID_2 = Guid.NewGuid.ToString
		Call xml_setAddGroup("Server Group 1", tmpGUID_1, 9, Nothing)
		For i = 1 To 5 : Call addRandomResources(tmpGUID_1) : Next

		Call xml_setAddGroup("Domain Controllers", tmpGUID_2, 7, tmpGUID_1)
		For i = 1 To 2 : xml_setAddServer("dc-" & i.ToString, Guid.NewGuid.ToString, tmpGUID_2) : Next
		For i = 1 To 5 : Call addRandomResources(tmpGUID_2) : Next

		tmpGUID_2 = Guid.NewGuid.ToString
		Call xml_setAddGroup("Management Servers", tmpGUID_2, 5, tmpGUID_1)
		For i = 1 To 2 : xml_setAddServer("mgt-" & i.ToString, Guid.NewGuid.ToString, tmpGUID_2) : Next
		For i = 1 To 5 : Call addRandomResources(tmpGUID_2) : Next

		tmpGUID_1 = Guid.NewGuid.ToString
		tmpGUID_2 = Guid.NewGuid.ToString
		tmpGUID_3 = Guid.NewGuid.ToString
		Call xml_setAddGroup("Server Group 2", tmpGUID_1, 9, Nothing)
		For i = 1 To 5 : Call addRandomResources(tmpGUID_1) : Next

		tmpGUID_2 = Guid.NewGuid.ToString
		Call xml_setAddGroup("Terminal Servers", tmpGUID_2, 9, tmpGUID_1)
		For i = 1 To 5 : Call addRandomResources(tmpGUID_2) : Next

		Call xml_setAddGroup("Development", tmpGUID_3, 9, tmpGUID_2)
		For i = 1 To 2 : xml_setAddServer("ts-dev-" & i.ToString, Guid.NewGuid.ToString, tmpGUID_3) : Next
		For i = 1 To 5 : Call addRandomResources(tmpGUID_3) : Next

		tmpGUID_3 = Guid.NewGuid.ToString
		Call xml_setAddGroup("Pre-Prod", tmpGUID_3, 9, tmpGUID_2)
		For i = 1 To 2 : xml_setAddServer("ts-tst-" & i.ToString, Guid.NewGuid.ToString, tmpGUID_3) : Next
		For i = 1 To 5 : Call addRandomResources(tmpGUID_3) : Next

		tmpGUID_3 = Guid.NewGuid.ToString
		Call xml_setAddGroup("Production", tmpGUID_3, 9, tmpGUID_2)
		For i = 1 To 9 : xml_setAddServer("ts-prod-" & i.ToString, Guid.NewGuid.ToString, tmpGUID_3) : Next
		For i = 1 To 5 : Call addRandomResources(tmpGUID_3) : Next

		m_prePopulating = False
		Call xmlLoadConfig()
	End Sub

	''' <summary>
	''' Generates random resources for a new configuration file
	''' </summary>
	''' <param name="sGroupGUID">STRING: Group GUID to assign the resources too</param>
	''' <remarks></remarks>
	Private Sub addRandomResources(ByVal sGroupGUID As String)

		Dim sType As String = vbNullString
		Dim sName As String = vbNullString
		Dim sChecking As String = vbNullString

		Randomize()
		Select Case CInt(100 * Rnd())
			Case 0 To 55 : sType = "Windows Service"
			Case 56 To 80 : sType = "Hotfix Patch"
			Case 81 To 90 : sType = "Registry Scan"
			Case 91 To 95 : sType = "Eventlog Scan"
			Case 96 To 100 : sType = "Free Space Threshold" : sName = sType
		End Select

		Select Case sType
			Case "Windows Service"
				sName = sServiceList(CInt((sServiceList.Count - 1) * Rnd()))
				sChecking = IIf(CInt(Rnd()) = 1, "Running", "Stopped").ToString & "|OK"

			Case "Hotfix Patch"
				sName = "KB" & CInt((10000000 * Rnd()) + 1).ToString
				sChecking = IIf(CInt(Rnd()) = 1, "Installed", "Not Installed").ToString

			Case "Eventlog Scan"
				Select Case CInt(2 * Rnd())
					Case 0 : sName = "Application" : sChecking = "Critical, Error, Warning, Excluding: "
					Case 1 : sName = "System" : sChecking = "Critical, Error, Excluding: "
					Case 2 : sName = "Security" : sChecking = "Failure, Excluding: "
				End Select

			Case "Registry Scan"
				sName = "SOFTWARE\Microsoft\Windows\CurrentVersion\Key-" & CInt((100 * Rnd()) + 1).ToString
				sChecking = CInt((10 * Rnd()) + 1).ToString & ", REG_SZ, "
				sChecking = sChecking & IIf(CInt(Rnd()) = 1, "OK", "Warning").ToString

			Case "Free Space Threshold"
				sChecking = (CInt(10 * Rnd()) + 1).ToString & "|" & (CInt(10 * Rnd()) + 10 & "|" & "Include Non-System Drives").ToString

			Case Else : MessageBox.Show(frmMain, "modMain (addRandomResources):" & vbCrLf & sType, APN, MessageBoxButtons.OK, MessageBoxIcon.Error)
		End Select

		If ((sName <> vbNullString) AndAlso (sType <> vbNullString) AndAlso (sChecking <> vbNullString)) Then
			Call xml_setAddResource(sGroupGUID, sType, sName, sChecking, Guid.NewGuid.ToString)
		End If
	End Sub

	''' <summary>
	''' ADMIN MODE ONLY: Return list of installed patches from the Windows Update Session Manager
	''' </summary>
	''' <param name="sServerName">STRING: Server to get updates from</param>
	''' <param name="bReturnJustKBNUmbers">BOOLEAN: Return either the KB Number or include Title and Install Date</param>
	''' <returns>LIST(Of String): List of KB Numbers (and Title and Install Date)</returns>
	''' <remarks></remarks>
	Public Function getWindowsUpdateSessionDetails(ByVal sServerName As String, ByVal bReturnJustKBNUmbers As Boolean) As List(Of String)
		Dim sResult As New List(Of String)
		Try
			Dim gType As Type = Type.GetTypeFromProgID("Microsoft.Update.Session", sServerName, True)
			Dim uSession As UpdateSession = CType(Activator.CreateInstance(gType), UpdateSession)
			Dim uSearcher As IUpdateSearcher = uSession.CreateUpdateSearcher
			Dim iCount As Integer = uSearcher.GetTotalHistoryCount()

			If (iCount > 0) Then
				Dim cEntries As IUpdateHistoryEntryCollection = uSearcher.QueryHistory(0, iCount)
				If (cEntries IsNot Nothing) Then
					For Each eEntries As IUpdateHistoryEntry In cEntries
						Dim sTitle As String = eEntries.Title
						Dim sKB As String = "KB ?"
						Dim iA As Integer = InStr(sTitle, "(KB")
						If (iA > 0) Then
							Dim iB As Integer = InStr(iA, sTitle, ")")
							sKB = sTitle.Substring(iA, (iB - iA) - 1)
						End If
						If bReturnJustKBNUmbers = True Then sResult.Add(sKB) Else sResult.Add(sKB & "|" & sTitle & "|" & eEntries.Date.ToString("yyyy/MM/dd"))
					Next
				End If
			End If
		Catch ex As Exception
			MessageBox.Show(frmMain, "Error: " & vbCrLf & ex.Message, APN, MessageBoxButtons.OK, MessageBoxIcon.Error)
		End Try
		Return sResult
	End Function

	''' <summary>
	''' Resize a given image into a new size (larger or smaller) using a HighQualityBicubic method
	''' </summary>
	''' <param name="SourceImage">IMAGE: Source image to use</param>
	''' <param name="NewSize">SIZE: Size to resize to</param>
	''' <returns>IMAGE: New image in the required size</returns>
	''' <remarks></remarks>
	Public Function ResizeImage(ByVal SourceImage As Image, ByVal NewSize As Size, ByVal bCenterImage As Boolean) As Image
		If (SourceImage Is Nothing) Then Return Nothing
		Try
			Dim newImage As Image = New Bitmap(NewSize.Width, NewSize.Height)
			Using gHandle As Graphics = Graphics.FromImage(newImage)
				gHandle.InterpolationMode = InterpolationMode.HighQualityBicubic

				If (bCenterImage = False) Then
					gHandle.DrawImage(SourceImage, 0, 0, NewSize.Width, NewSize.Height)
				Else
					Dim x As Integer = CInt((NewSize.Width - SourceImage.Width) / 2)
					Dim y As Integer = CInt((NewSize.Height - SourceImage.Height) / 2)
					gHandle.DrawImage(SourceImage, x, y, SourceImage.Width, SourceImage.Height)
				End If
			End Using
			Return newImage
		Catch ex As Exception
			Return Nothing
		End Try
	End Function

	''' <summary>
	''' Convert a given image into a "Grey scale" image
	''' </summary>
	''' <param name="SourceImage">IMAGE: Source image to use</param>
	''' <param name="bWithTransparency">BOOLEAN: Applies a 50% transparency to the resulting image</param>
	''' <returns>IMAGE: New image that appears grey-scaled</returns>
	''' <remarks></remarks>
	Public Function GreyScaleImage(ByVal SourceImage As Image, ByVal bWithTransparency As Boolean) As Image
		If (SourceImage Is Nothing) Then Return Nothing
		Try
			Dim newImage As Image = New Bitmap(SourceImage.Width, SourceImage.Height)
			Using gHandle As Graphics = Graphics.FromImage(newImage)
				Dim cMatrix As ColorMatrix = New ColorMatrix
				Dim iAttributes As ImageAttributes = New ImageAttributes

				cMatrix.Matrix00 = 0.0F		' \
				cMatrix.Matrix10 = 1.0F		'  | These make the
				cMatrix.Matrix11 = 1.0F		'  | image appear to
				cMatrix.Matrix12 = 1.0F		'  | be grey-scale
				cMatrix.Matrix22 = 0.0F		' /
				If (bWithTransparency = True) Then cMatrix.Matrix33 = 0.5F

				iAttributes.SetColorMatrix(cMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap)
				gHandle.DrawImage(SourceImage, New Rectangle(0, 0, SourceImage.Width, SourceImage.Height), 0, 0, newImage.Width, newImage.Height, GraphicsUnit.Pixel, iAttributes)
			End Using
			Return newImage
		Catch ex As Exception
			Return Nothing
		End Try
	End Function
	Public Function GreyScaleIcon(ByVal SourceImage As Icon, ByVal bWithTransparency As Boolean) As Icon
		If (SourceImage Is Nothing) Then Return Nothing
		Try
			Dim newImage As Image = New Bitmap(SourceImage.Width, SourceImage.Height)
			Using gHandle As Graphics = Graphics.FromImage(newImage)
				Dim cMatrix As ColorMatrix = New ColorMatrix
				Dim iAttributes As ImageAttributes = New ImageAttributes

				cMatrix.Matrix00 = 0.0F		' \
				cMatrix.Matrix10 = 1.0F		'  | These make the
				cMatrix.Matrix11 = 1.0F		'  | image appear to
				cMatrix.Matrix12 = 1.0F		'  | be grey-scale
				cMatrix.Matrix22 = 0.0F		' /
				If (bWithTransparency = True) Then cMatrix.Matrix33 = 0.5F

				iAttributes.SetColorMatrix(cMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap)
				gHandle.DrawImage(SourceImage.ToBitmap, New Rectangle(0, 0, SourceImage.Width, SourceImage.Height), 0, 0, newImage.Width, newImage.Height, GraphicsUnit.Pixel, iAttributes)
			End Using
			Dim bmp As Bitmap = CType(newImage, Bitmap)
			Dim ico As Icon = Icon.FromHandle(bmp.GetHicon)
			Return ico
		Catch ex As Exception
			Return Nothing
		End Try
	End Function

	''' <summary>
	''' Checks the depth of a tree node
	''' </summary>
	''' <param name="sFullPath">STRING: Full path of node to check</param>
	''' <param name="sPathSeperator">STRING: Path separator used in full path</param>
	''' <param name="iDepth">INTEGER: Maximum allowable depth</param>
	''' <returns>BOOLEAN: TRUE: Depth is below maximum, FALSE: Depth is too large</returns>
	''' <remarks></remarks>
	Public Function checkGroupDepth(ByVal sFullPath As String, ByVal sPathSeperator As String, ByRef iDepth As Integer) As Boolean
		Dim sNewString As String = Nothing
		sNewString = sFullPath.Replace(sPathSeperator, "")
		iDepth = (sFullPath.Length - sNewString.Length)
		If (iDepth < sMaxGroupDepth) Then Return True
		Return False
	End Function

	''' <summary>
	''' Finds and returns the depth of a child node (using Tree nodes)
	''' </summary>
	''' <param name="selectedNode">TREENODE: Selected node to check</param>
	''' <param name="sPathSeperator">STRING: Path separator used in path</param>
	''' <returns>INTEGER: Depth of selected node</returns>
	''' <remarks></remarks>
	Public Function findChildDepth(ByVal selectedNode As TreeNode, ByVal sPathSeperator As String) As Integer
		Dim iDepth As Integer = 0
		m_findChildNode_depthNode = Nothing
		If ((selectedNode.GetNodeCount(False) > 0) AndAlso (selectedNode.FirstNode.Tag.ToString = "GROUP")) Then
			For Each tvNode As TreeNode In selectedNode.Nodes
				getSubNodes_Depth(tvNode)
			Next
		Else
			m_findChildNode_depthNode = selectedNode
		End If
		Dim sPath As String = m_findChildNode_depthNode.FullPath.Substring(selectedNode.FullPath.Length)
		Call checkGroupDepth(sPath, sPathSeperator, iDepth)
		Return iDepth
	End Function
	Private Sub getSubNodes_Depth(ByVal tvNode As TreeNode)
		If (tvNode.Tag.ToString = "GROUP") Then
			If (m_findChildNode_depthNode Is Nothing) Then m_findChildNode_depthNode = tvNode
			If (tvNode.Level > m_findChildNode_depthNode.Level) Then m_findChildNode_depthNode = tvNode
		End If
		For Each tvSubNode As TreeNode In tvNode.Nodes
			getSubNodes_Depth(tvSubNode)
		Next
	End Sub

	''' <summary>
	''' Checks to see if the specified version of the dotNET framework is installed
	''' </summary>
	''' <param name="VersionToCheck">STRING: Version </param>
	''' <returns>BOOLEAN: TRUE: Installed, FALSE: Not installed or not found</returns>
	''' <remarks>The input 'VersionToCheck' must match exactly the name of the registry key</remarks>
	Public Function checkDotNetVersion(ByVal VersionToCheck As String) As Boolean
		Dim rKey As RegistryKey = RegistryKey.OpenRemoteBaseKey(RegistryHive.LocalMachine, String.Empty)
		Dim rNDP As RegistryKey = rKey.OpenSubKey("SOFTWARE\Microsoft\NET Framework Setup\NDP")
		Dim rNET() As String = rNDP.GetSubKeyNames
		rNDP = Nothing
		rKey = Nothing
		If rNET.Contains(VersionToCheck) Then Return True
		Return False
	End Function

	''' <summary>
	''' Returns a formatted size and unit display of an inputted size, used for drive space
	''' </summary>
	''' <param name="iInputSize">LONG: Size to be converted</param>
	''' <param name="iDecimalPlaces">OPTIONAL INTEGER: Number of decimal places to return (default:2)</param>
	''' <returns>STRING: Nicely formatted size and units</returns>
	''' <remarks></remarks>
	Public Function getSizeAndUnits(ByVal iInputSize As Long, Optional ByVal iDecimalPlaces As Integer = 2) As String
		Dim iSize As Decimal = iInputSize
		Dim sizeTypes() As String = {"B", "KB", "MB", "GB", "TB", "PB", "xx", "xx"}
		Dim iCnt As Integer = 0
		Do While (iSize > 1024)
			iSize = Decimal.Round(iSize / 1024, iDecimalPlaces)
			iCnt = iCnt + 1
			If iCnt >= sizeTypes.Length - 1 Then Exit Do
		Loop
		Return iSize.ToString & " " & sizeTypes(iCnt)
	End Function

	''' <summary>
	''' Finds the next available "New Group xxx" group number
	''' </summary>
	''' <param name="treeControl">TREEVIEW: Control to use</param>
	''' <returns>STRING: Full string of the new group name</returns>
	''' <remarks></remarks>
	Public Function findNewGroupName(ByVal treeControl As TreeView) As String
		Dim iCount As Integer = 1
		Dim nNodes As TreeNode = findNode(treeControl.Nodes, "New Group " & iCount)
		Do Until nNodes Is Nothing
			iCount = iCount + 1
			nNodes = findNode(treeControl.Nodes, "New Group " & iCount)
		Loop
		Return "New Group " & iCount
	End Function
	Public Function findNode(ByVal nCollection As TreeNodeCollection, ByVal sSearchFor As String) As TreeNode
		Dim tmpNode As TreeNode
		For Each tNode As TreeNode In nCollection
			If (tNode.Text = sSearchFor) Then Return tNode
			tmpNode = findNode(tNode.Nodes, sSearchFor)
			If tmpNode IsNot Nothing Then
				Return tmpNode
			End If
		Next
		Return Nothing
	End Function

	''' <summary>
	''' Convert a given image into a "Selected" image
	''' </summary>
	''' <param name="bInputImage">IMAGE: Source image to use</param>
	''' <returns>IMAGE: New Image that appears selected</returns>
	''' <remarks></remarks>
	Public Function SelectImage(ByVal bInputImage As Image) As Image
		Try
			Dim gMask As Bitmap = New Bitmap(bInputImage.Width, bInputImage.Height)
			Dim gOrginal As Bitmap = New Bitmap(bInputImage)
			For y As Integer = 0 To gMask.Height - 1
				For x As Integer = 0 To gMask.Width - 1
					Dim a As Integer = gOrginal.GetPixel(x, y).A - 128
					If (a < 0) Then a = 0
					gMask.SetPixel(x, y, Color.FromArgb(a, SystemColors.Highlight))
				Next
			Next
			Using g As Graphics = Graphics.FromImage(gOrginal)
				g.DrawImage(gMask, 0, 0)
				g.Save()
			End Using
			Return gOrginal
		Catch ex As Exception
			Return Nothing
		End Try
	End Function

	''' <summary>
	''' Connects to the selected server via a WMI connection, using a separate thread and timeout function
	''' </summary>
	''' <param name="sPath">STRING: WMI Path to use (this should include the server name)</param>
	''' <param name="wmiQuery">STRING: WMI Query to perform on the server</param>
	''' <param name="iTimeOut">INTEGER: Time (in Seconds) to wait for the results</param>
	''' <returns>MANAGEMENTOBJECTCOLLECTION: Data returned from the query</returns>
	''' <remarks></remarks>
	Public Function wmiConnect(ByVal sPath As String, ByVal wmiQuery As String, ByVal iTimeOut As Integer) As ManagementObjectCollection
		Try
			Dim bAborted As Boolean
			Dim mTask As New mgtConnection

			'Create new thread to run management connection...
			Dim tThread As Threading.Thread
			mTask.sPath = sPath
			mTask.wmiQuery = wmiQuery

			bAborted = False
			tThread = New Threading.Thread(AddressOf mTask.connect)
			tThread.Start()

			' Start timer...
			For i As Integer = 1 To (iTimeOut * 10)
				Threading.Thread.Sleep(100)
				If (tThread.IsAlive = False) Then Exit For
			Next
			If (tThread.IsAlive = True) Then
				tThread.Abort()
				bAborted = True
			End If

			If (bAborted = True) Then Return Nothing
			Return mTask.mCollection

		Catch ex As Exception
			Return Nothing
		End Try
	End Function

	''' <summary>
	''' Ensures that a given listview item is visible near the bottom of the control
	''' </summary>
	''' <param name="listviewControl">LISTVIEW: Control to use</param>
	''' <param name="lItem">LISTVIEWITEM: Item to use</param>
	''' <remarks></remarks>
	Public Sub ensureVisible(ByVal listviewControl As ListView, ByVal lItem As ListViewItem)
		With listviewControl
			If ((lItem.Index + 3) < .Items.Count) Then
				.Items(lItem.Index + 3).EnsureVisible()
			Else
				.Items(.Items.Count - 1).EnsureVisible()
			End If
		End With
	End Sub

	''' <summary>
	''' WMI query to find the last reboot time of a given server
	''' </summary>
	''' <param name="sServername">STRING: Server to get details from</param>
	''' <returns>DATETIME: Last reboot time</returns>
	''' <remarks></remarks>
	Public Function getServerShutdownTime(ByVal sServerName As String) As DateTime
		Dim oResult As ManagementObjectCollection = wmiConnect("\\" & sServerName & "\root\cimv2", "SELECT * FROM Win32_OperatingSystem", 5)
		For Each mObj As ManagementObject In oResult
			Try
				Console.WriteLine("IGNORE ME: (LastBootUpTime): " & mObj("LastBootUpTime").ToString)	' This is a test to see if "mObj('LastBootUpTime')" is nothing.
				Dim dtDateTime As String = mObj("LastBootUpTime").ToString
				Return Management.ManagementDateTimeConverter.ToDateTime(dtDateTime)
			Catch ex As Exception
				Return Nothing
			Finally
				If (oResult IsNot Nothing) Then oResult.Dispose()
			End Try
		Next
		Return Nothing
	End Function

	''' <summary>
	''' Gets the difference between two given dates
	''' </summary>
	''' <param name="d1">DATETIME: First date value you want to use (Generally, NOW)</param>
	''' <param name="d2">DATETIME: Second date value to want to use (Generally, a date in the past)</param>
	''' <returns>STRING: Formatted string, X days, X hours, X minutes</returns>
	''' <remarks></remarks>
	Public Function getDateDiff(ByVal d1 As DateTime, ByVal d2 As DateTime) As String
		Dim sReturn As String = vbNullString
		Dim dd As Integer = CInt(DateDiff(DateInterval.Minute, d2, d1))
		Dim ts As TimeSpan = TimeSpan.FromMinutes(dd)
		If (ts.Days > 0) Then sReturn = sReturn & ts.Days & " days, "
		sReturn = sReturn & ts.Hours & " hours, " & ts.Minutes & " minutes"
		Return sReturn
	End Function

	''' <summary>
	''' Converts a given image into a Base64 String
	''' </summary>
	''' <param name="image">IMAGE: Input image to use</param>
	''' <param name="imageFormat">IMAGEFORMAT: Format of the inputted image (PNG, ICO, JPG, etc)</param>
	''' <returns>STRING: Base64 representation of the inputted image</returns>
	''' <remarks></remarks>
	Public Function ImageToBase64String(ByVal image As Image, ByVal imageFormat As Imaging.ImageFormat) As String
		Using memStream As New MemoryStream
			image.Save(memStream, imageFormat)
			Dim result As String = Convert.ToBase64String(memStream.ToArray())
			Return result
		End Using
	End Function

	''' <summary>
	''' Checks to see if a given control has it's vertical scrollbar showing or not
	''' </summary>
	''' <param name="hHandle">LONG: Handle of control to check</param>
	''' <returns>BOOLEAN: True or False</returns>
	''' <remarks></remarks>
	Public Function isScrollbarVisibleV(ByVal hHandle As Long) As Boolean
		isScrollbarVisibleV = CBool(GetWindowLong(hHandle, GWL_STYLE) And WS_VSCROLL)
	End Function

	''' <summary>
	''' Checks to see if a given control has it's horizontal scrollbar showing or not
	''' </summary>
	''' <param name="hHandle">LONG: Handle of control to check</param>
	''' <returns>BOOLEAN: True or False</returns>
	''' <remarks></remarks>
	Public Function isScrollbarVisibleH(ByVal hHandle As Long) As Boolean
		isScrollbarVisibleH = CBool(GetWindowLong(hHandle, GWL_STYLE) And WS_HSCROLL)
	End Function

	''' <summary>
	''' Replaces multiple string with one in a given string
	''' </summary>
	''' <param name="sInput">STRING: Input string to search</param>
	''' <param name="sReplaceWith">STRING: Replacement string</param>
	''' <param name="sCheckFor">STRING ARRAY: Strings to search for and replace</param>
	''' <returns>STRING: Newly replaced string</returns>
	''' <remarks></remarks>
	Public Function ReplaceMultiple(ByVal sInput As String, ByVal sReplaceWith As String, ParamArray sCheckFor() As String) As String
		Dim sResult As String = sInput
		For i As Integer = LBound(sCheckFor) To UBound(sCheckFor)
			sResult = Replace(sResult, CStr(sCheckFor(i)), sReplaceWith)
		Next
		Return sResult
	End Function

	Public Function cleanString(ByVal sInput As String, ByVal sCheckFor() As String, ByVal sReplaceWith() As String) As String
		Dim sReturn As String = sInput
		Dim sOutput As String = vbNullString
		Do
			If (sOutput <> vbNullString) Then sReturn = sOutput
			Try
				sOutput = sReturn.Replace(sCheckFor(0), sReplaceWith(0))
				For i As Integer = 1 To sCheckFor.Count - 1
					sOutput = sOutput.Replace(sCheckFor(i), sReplaceWith(i))
				Next
			Catch
			End Try
		Loop Until sOutput = sReturn
		Return sReturn
	End Function

	Public Function cleanEventlogIDList(ByVal sInput As String, Optional ByVal sRemoveID As Interval = Nothing) As String
		Try
			Dim sReturn As String = cleanString(sInput, {" ", "--", ",,", "-,", ",-"}, {"", "-", ",", ",", ","})

			Dim iStore As New IntervalStore
			Dim aIDList() As String = sReturn.Split(","c)
			For Each i As String In aIDList
				If (IsNumeric(i.Replace("-", "")) = True) Then
					If (InStr(i, "-") > 0) Then
						If (i.Split("-"c).Count = 2) Then
							If ((IsNumeric(i.Split("-"c)(0))) AndAlso (IsNumeric(i.Split("-"c)(1)))) Then
								iStore.addInterval(CInt(i.Split("-"c)(0)), CInt(i.Split("-"c)(1)))
							End If
						End If
					Else
						iStore.addInterval(CInt(i), CInt(i))
					End If
				End If
			Next

			If (sRemoveID IsNot Nothing) Then iStore.delInterval(sRemoveID.getLower, sRemoveID.getUpper)

			sReturn = iStore.ReturnIntervals
			iStore = Nothing
			If (sReturn IsNot Nothing) Then Return sReturn Else Return vbNullString

		Catch ex As Exception
			Return sInput
		End Try
	End Function

	Public Sub cleanHotfixDateList(ByVal cListView As ListView, ByVal iSubItemToUse As Integer, Optional pProgressBar As ProgressBar = Nothing)
		Dim sDateString() As String
		Dim iD As Integer
		Dim iM As Integer
		Dim iY As Integer
		With cListView
			For Each itm As ListViewItem In .Items
				If (itm.SubItems(iSubItemToUse).Text.Substring(0, 1) = ":") Then
					Dim sDTText As String = itm.SubItems(iSubItemToUse).Text.Substring(1)
					If (InStr(sDTText, "/") > 0) Then
						sDateString = Split(sDTText, "/")
						If (sDateString(0).Length = 4) Then iY = 0
						If (sDateString(1).Length = 4) Then iY = 1
						If (sDateString(2).Length = 4) Then iY = 2

						If ((iY <> 0) AndAlso (CInt(sDateString(0)) > 12)) Then
							iD = 0
							iM = 1
							Exit For
						End If

						If ((iY <> 1) AndAlso (CInt(sDateString(1)) > 12)) Then
							iD = 1
							iM = 0
							Exit For
						End If

						If ((iY <> 2) AndAlso (CInt(sDateString(2)) > 12)) Then
							iD = 2
							iM = 1
							Exit For
						End If
					End If
				End If
			Next

			Dim sNewDate As String
			If (pProgressBar IsNot Nothing) Then
				pProgressBar.Maximum = .Items.Count
				pProgressBar.Value = 0
			End If

			For Each itm As ListViewItem In .Items
				If (itm.SubItems(iSubItemToUse).Text.Substring(0, 1) = ":") Then
					Dim sDTText As String = itm.SubItems(iSubItemToUse).Text.Substring(1)
					If (InStr(sDTText, "/") > 0) Then
						sNewDate = "yyyy/mm/dd"
						sDateString = Split(sDTText, "/")
						sNewDate = sNewDate.Replace("yyyy", CInt(sDateString(iY)).ToString("00"))
						sNewDate = sNewDate.Replace("mm", CInt(sDateString(iM)).ToString("00"))
						sNewDate = sNewDate.Replace("dd", CInt(sDateString(iD)).ToString("00"))
						itm.SubItems(iSubItemToUse).Text = sNewDate
					End If
				End If
				If (pProgressBar IsNot Nothing) Then pProgressBar.Value = pProgressBar.Value + 1
			Next
		End With
	End Sub

	Public Function readRegistryValue(ByVal oValue As Object) As String
		Select Case oValue.GetType.ToString
			Case "System.String"
				Return Convert.ToString(oValue).Trim

			Case "System.String[]"
				Return Join(TryCast(oValue, String()), " " & vbCrLf).Trim

			Case "System.Byte[]"
				Dim bBytes() As Byte = TryCast(oValue, Byte())
				Dim sResult As String = vbNullString
				For Each bByte As Byte In bBytes
					Dim sValue As String = Hex(bByte.ToString).ToString
					If (sValue.Length = 1) Then sValue = "0" & sValue
					sResult = sResult & sValue & " "
				Next
				Return sResult.ToLower.Trim

			Case "System.Int32", "System.Int64"
				Return Convert.ToString(oValue).ToLower.Trim

			Case Else
				MessageBox.Show(frmMain, "Unknown Type:" & vbCrLf & "    " & oValue.GetType.ToString)
				Return Nothing
		End Select
	End Function

	Function CompactString(ByVal MyString As String, ByVal Width As Integer, ByVal Font As Drawing.Font) As String
		Dim Result As String = String.Copy(MyString)
		TextRenderer.MeasureText(Result, Font, New Drawing.Size(Width, 0), TextFormatFlags.PathEllipsis Or TextFormatFlags.ModifyString)
		Return Result
	End Function

	''' <summary>
	''' Much better replacement for Graphics.MeasureString
	''' </summary>
	''' <param name="graphics">GRAPHICS: Graphics object to use</param>
	''' <param name="text">STRING: Text to measure</param>
	''' <param name="font">FONT: text font to use</param>
	''' <returns>INTEGER: Width of string</returns>
	''' <remarks></remarks>
	Public Function MeasureDisplayStringWidth(ByVal graphics As Graphics, ByVal text As String, ByVal font As Font) As Integer
		If (String.IsNullOrEmpty(text) = True) Then Return 0
		Dim sReturn As Integer = 0
		Dim format As System.Drawing.StringFormat = New System.Drawing.StringFormat
		Dim rect As System.Drawing.RectangleF = New System.Drawing.RectangleF(0, 0, 1000, 1000)
		Dim ranges() As System.Drawing.CharacterRange = New System.Drawing.CharacterRange() {New System.Drawing.CharacterRange(0, text.Length)}
		Dim regions() As System.Drawing.Region = New System.Drawing.Region((1) - 1) {}
		format.SetMeasurableCharacterRanges(ranges)
		regions = graphics.MeasureCharacterRanges(text, font, rect, format)
		rect = regions(0).GetBounds(graphics)
		sReturn = CType((rect.Right + 1.0!), Integer)
		regions = Nothing
		ranges = Nothing
		rect = Nothing
		format.Dispose()
		Return sReturn
	End Function

	''' <summary>
	''' Resolve a valid IP address into a hostname
	''' </summary>
	''' <param name="sIPAddress">STRING: Valid IP address</param>
	''' <returns>STRING: Hostname or IP address</returns>
	''' <remarks>If the IP can't be resolved, it is returned</remarks>
	Public Function resolveIP(ByVal sIPAddress As String) As String
		If (m_NetworkAvailability = False) Then Return sIPAddress
		Dim host As IPHostEntry = Nothing
		Dim sResult As String = vbNullString
		Try
			host = Dns.GetHostEntry(sIPAddress.Trim())
			sResult = host.HostName
			If (sResult <> sIPAddress) Then sResult = sResult.Split("."c)(0)
		Catch
			sResult = sIPAddress
		End Try
		sResult = sResult.ToLower
		If (m_UppercaseServerNames = True) Then sResult = sResult.ToUpper
		Return sResult
	End Function

	''' <summary>
	''' Cleans any string containing slashes (\) and formats a return string with each item on a new line and indented
	''' </summary>
	''' <param name="sInput">STRING: Input string</param>
	''' <returns>STRING: Formatted output string</returns>
	''' <remarks></remarks>
	Public Function prettyPath(ByVal sInput As String, ByVal iIndentStart As Integer) As String
		If (sInput Is Nothing) Then Return Nothing
		Dim sPath() As String = sInput.Split("\"c)
		Dim sReturn As String = vbNullString

		Dim iCnt As Integer = iIndentStart
		For Each sP As String In sPath
			sReturn = sReturn & Space(iCnt * 4) & sP & vbCrLf
			iCnt = iCnt + 1
		Next
		Return sReturn
	End Function

	''' <summary>
	''' Cleans any string containing slashes (\) and formats a return string with each item on a new line and indented
	''' </summary>
	''' <param name="sInput">LIST(OF XMLNODE): Input Path (xml_getGroupPathList)</param>
	''' <returns>STRING: Formatted output string</returns>
	''' <remarks></remarks>
	Public Function prettyPath(ByVal sInput As List(Of XmlNode), ByVal iIndentStart As Integer) As String
		If (sInput Is Nothing) Then Return Nothing
		Dim sReturn As String = vbNullString

		Dim iCnt As Integer = iIndentStart
		For Each xNode As XmlNode In sInput
			sReturn = sReturn & Space(iCnt * 4) & xNode.Attributes.ItemOf("name").Value & vbCrLf
			iCnt = iCnt + 1
		Next
		Return sReturn
	End Function

	''' <summary>
	''' Checks to see if a server is listed in a collection of nodes
	''' </summary>
	''' <param name="nList">TREENODECOLLECTION: Collection of TreeNodes to check</param>
	''' <param name="sServerName">STRING: Server name to check for</param>
	''' <returns>STRING: GUID of exiting server, or Nothing</returns>
	''' <remarks></remarks>
	Public Function isDuplicateServerInGroup(ByVal nList As TreeNodeCollection, ByVal sServerName As String) As String
		If (nList Is Nothing) Then Return Nothing
		If (m_UppercaseServerNames = True) Then sServerName = sServerName.ToUpper Else sServerName = sServerName.ToLower
		Dim tNode As TreeNode = findNode(nList, sServerName)
		If (tNode IsNot Nothing) Then Return tNode.Name Else Return Nothing
		tNode = Nothing
	End Function

	''' <summary>
	''' Shows an alert if a duplicated resource exists
	''' </summary>
	''' <param name="sScanType">STRING: Resource Type</param>
	''' <param name="lEditValues">LISTVIEWITEM: Edited item reference</param>
	''' <param name="sResults">LIST(OF STRING): List of resources to check for</param>
	''' <returns>DIALOGRESULT: Result of the message box shown</returns>
	''' <remarks></remarks>
	Public Function resourceDuplicationAlert(ByVal sScanType As String, ByVal lEditValues As ListViewItem, ByVal sResults As List(Of String)) As DialogResult
		If (lEditValues Is Nothing) Then lEditValues = New ListViewItem
		lResourceConflicts = Nothing
		Dim bFound As Boolean = False
		Dim sGroupGUID As String = getGroupNode(frmMain.tvwServerList.SelectedNode).Name

		For Each sResult As String In sResults
			If (xml_checkResourceExists(sGroupGUID, sScanType.Replace(" Import", ""), sResult.Split("|"c)(0), lEditValues.Name) = True) Then bFound = True
		Next
		If (bFound = False) Then Return DialogResult.None

		Dim sMessage As String = "Unable to save this resource," & vbCrLf & "A duplicate resource exists." & vbCrLf & vbCrLf
		Select Case sScanType
			Case "Eventlog Scan" : sMessage &= "This eventlog is already being scanned."
			Case "File Check" : sMessage &= "This file is already being scanned."
			Case "Hotfix Patch" : sMessage &= "One or more hotfixes already exist."
			Case "Registry Scan Import" : sMessage &= "One or more key/value pairs already exist."
			Case "Registry Scan" : sMessage &= "This key/value pair is already being scanned."
			Case "Windows Service" : sMessage &= "One or more services already exist."
			Case "WMI Query" : sMessage &= "This WMI Class is already being scanned."
			Case Else : sMessage &= "ERROR - You should never see this.!"
		End Select

		Dim dResult As DialogResult
		Select Case sScanType
			Case "Hotfix Patch", "Registry Scan Import", "Windows Service"
				clsMessageBox.CustomMsgBox({"Show Me", "Remove", "Cancel"})
				dResult = MessageBox.Show(sMessage, APN, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Error, MessageBoxDefaultButton.Button3)

			Case Else
				clsMessageBox.CustomMsgBox({"Show Me", "Cancel"})
				dResult = MessageBox.Show(sMessage, APN, MessageBoxButtons.OKCancel, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2)
		End Select

		If ((dResult = DialogResult.OK) Or (dResult = DialogResult.Yes)) Then frmResourceDuplication.ShowDialog()
		' Do not clear "lResourceConflicts", it's used in removing duplicates
		Return dResult
	End Function

	Public Function convertRAM(ByVal sInput As String, ByVal iDecimalPlaces As Integer) As String()
		Dim sRAM() As String = Split(sInput, "|")
		Dim iCnt As Integer = 0
		Dim iTotal As Long = 0
		Dim sReturn(iCnt) As String
		For Each sStick As String In sRAM
			If (IsNumeric(sStick) = True) Then
				iCnt = iCnt + 1
				iTotal = iTotal + CLng(sStick)
				ReDim Preserve sReturn(iCnt)
				sReturn(iCnt) = getSizeAndUnits(CLng(sStick), 0)
			End If
		Next

		If (iTotal > 0) Then sReturn(0) = getSizeAndUnits(iTotal, iDecimalPlaces) Else sReturn(0) = Math.Round(CDbl(Split(sInput, " ")(0))) & " " & Split(sInput, " ")(1)
		Return sReturn
	End Function

	'Public Function getServerInfo(ByVal sServerName As String) As String()
	'    Now located in "frmPropertiesServerGetAll" as it is required to be a SHARED function
	'End Function

	' #############################################################################################
	<StructLayout(LayoutKind.Sequential)> _
	Public Structure HDITEM
		Public mask As Integer
		Public cxy As Integer
		<MarshalAs(UnmanagedType.LPTStr)> Public pszText As String
		Public hbm As IntPtr
		Public cchTextMax As Integer
		Public fmt As Integer
		Public lParam As Integer
		Public iImage As Integer
		Public iOrder As Integer
	End Structure

	Private Const HDI_FORMAT As Integer = 4
	Private Const HDF_SORTUP As Integer = 1024
	Private Const HDF_SORTDOWN As Integer = 512
	Private Const LVM_GETHEADER As Integer = (4096 + 31)
	Private Const HDM_GETITEM As Integer = (4608 + 11)
	Private Const HDM_SETITEM As Integer = (4608 + 12)

	Private Declare Function SendMessageFIND Lib "user32.dll" Alias "SendMessageA" (ByVal hWnd As IntPtr, ByVal Msg As UInteger, ByVal wParam As IntPtr, ByVal lParam As IntPtr) As IntPtr
	Private Declare Function SendMessageITEM Lib "user32.dll" Alias "SendMessageA" (ByVal Handle As IntPtr, ByVal msg As Int32, ByVal wParam As IntPtr, ByRef lParam As HDITEM) As IntPtr

	Public Sub ShowListViewSortImage(ByVal lvControl As ListView, ByVal colIndex As Integer, sOrder As SortOrder)
		'If (bUseVisualStyles = False) Then Exit Sub
		For Each col As ColumnHeader In lvControl.Columns
			Call ShowSortIcon(lvControl, col.Index, SortOrder.None)
		Next
		Call ShowSortIcon(lvControl, colIndex, sOrder)
	End Sub

	Private Sub ShowSortIcon(ByVal lvControl As ListView, ByVal colIndex As Integer, sOrder As SortOrder)
		Dim hColHeader As IntPtr = SendMessageFIND(lvControl.Handle, LVM_GETHEADER, IntPtr.Zero, IntPtr.Zero)
		Dim hdItem As HDITEM = New HDITEM
		Dim colHeader As IntPtr = New IntPtr(colIndex)
		hdItem.mask = HDI_FORMAT
		Dim rtn As IntPtr = SendMessageITEM(hColHeader, HDM_GETITEM, colHeader, hdItem)

		Select Case sOrder
			Case SortOrder.Ascending
				hdItem.fmt = (hdItem.fmt And (Not HDF_SORTDOWN))
				hdItem.fmt = (hdItem.fmt Or HDF_SORTUP)
			Case SortOrder.Descending
				hdItem.fmt = (hdItem.fmt And (Not HDF_SORTUP))
				hdItem.fmt = (hdItem.fmt Or HDF_SORTDOWN)
			Case SortOrder.None
				hdItem.fmt = (hdItem.fmt And (Not HDF_SORTUP))
				hdItem.fmt = (hdItem.fmt And (Not HDF_SORTDOWN))
		End Select
		rtn = SendMessageITEM(hColHeader, HDM_SETITEM, colHeader, hdItem)
	End Sub
End Module

Class mgtConnection
	Public sPath As String
	Public wmiQuery As String
	Public mCollection As ManagementObjectCollection
	Sub connect()
		Dim sMsg As String = "There was and error when trying to gather the requested information." & vbCrLf & _
		   "The message returned is below..." & vbCrLf & vbCrLf & "REPLACE"
		Try
			Dim cOptions As New ConnectionOptions
			cOptions.Impersonation = ImpersonationLevel.Impersonate
			Dim mScope As ManagementScope = New ManagementScope(sPath, cOptions)
			mScope.Connect()

			If (mScope.IsConnected = True) Then
				Dim oSearcher As New ManagementObjectSearcher(mScope.Path.ToString, wmiQuery)
				mCollection = oSearcher.Get()
			End If

		Catch ex As System.UnauthorizedAccessException
			sMsg = sMsg.Replace("REPLACE", "Access Denied" & vbCrLf & vbCrLf & "Make sure you have enough permissions to access this server")
			mCollection = Nothing

		Catch ex As Exception
			sMsg = sMsg.Replace("REPLACE", ex.Message & vbCrLf & vbCrLf & "Check the error and try to resolve it if possible.")
			mCollection = Nothing

		Finally
			If (InStr(sMsg, "REPLACE") = 0) Then
				If ((frmScan.Visible = True) AndAlso (frmScan.bCurrentlyScanning = False)) Then
					MessageBox.Show(frmMain, sMsg, APN, MessageBoxButtons.OK, MessageBoxIcon.Error)
				End If
			End If
		End Try
	End Sub
End Class