Option Explicit On
Imports Microsoft.Win32
Imports System.ServiceProcess
Imports System.Xml
Imports System.Management
Imports System.Drawing.Drawing2D
Imports System.Runtime.InteropServices

Public Class frmScanResults
	Private lvwCS_S As ListViewColumnSorter			' Services
	Private lvwCS_H As ListViewColumnSorter			' Hotfixes
	Private lvwCS_E As ListViewColumnSorter			' Eventlogs
	Private lvwCS_R As ListViewColumnSorter			' Registry
	Private lvwCS_F As ListViewColumnSorter			' File Scan
	Private lvwCS_W As ListViewColumnSorter			' WMI Query
	Private lvwCS_D As ListViewColumnSorter			' Free Space
	Private iDriveThreshold_Fail As Integer = 10
	Private iDriveThreshold_Warn As Integer = 20

	Dim mnuHotFixes As ContextMenuStrip
	Dim mnuEventLog As ContextMenuStrip
	Dim mnuServices As ContextMenuStrip

	Private Enum FormatMessageFlags As Integer
		FORMAT_MESSAGE_ALLOCATE_BUFFER = &H100
		FORMAT_MESSAGE_ARGUMENT_ARRAY = &H2000
		FORMAT_MESSAGE_FROM_HMODULE = &H800
		FORMAT_MESSAGE_FROM_STRING = &H400
		FORMAT_MESSAGE_FROM_SYSTEM = &H1000
		FORMAT_MESSAGE_IGNORE_INSERTS = &H200
		FORMAT_MESSAGE_MAX_WIDTH_MASK = &HFF
	End Enum
	Private Enum LoadLibraryFlags As UInteger
		DONT_RESOLVE_DLL_REFERENCES = &H1
		LOAD_IGNORE_CODE_AUTHZ_LEVEL = &H10
		LOAD_LIBRARY_AS_DATAFILE = &H2
		LOAD_LIBRARY_AS_DATAFILE_EXCLUSIVE = &H40
		LOAD_LIBRARY_AS_IMAGE_RESOURCE = &H20
		LOAD_WITH_ALTERED_SEARCH_PATH = &H8
	End Enum
	Private Enum Languages As Short
		LANG_NEUTRAL = &H0
		SUBLANG_DEFAULT = &H1
	End Enum

	Private Declare Function FreeLibrary Lib "kernel32.dll" (ByVal hModule As IntPtr) As Boolean
	Private Declare Function LoadLibrary Lib "kernel32" Alias "LoadLibraryA" (ByVal lpLibFileName As String) As Long

	Private Declare Function FormatMessageA Lib "kernel32.dll" (ByVal dwFlags As Long, lpSource As Long, ByVal dwMessageId As Long, ByVal dwLanguageId As Long, ByVal lpBuffer As String, ByVal nSize As Long, ByVal Arguments() As String) As Long
	<DllImport("Kernel32.dll", EntryPoint:="FormatMessageW", SetLastError:=True, CharSet:=CharSet.Unicode, CallingConvention:=CallingConvention.StdCall)> _
	Private Shared Function FormatMessageW(ByVal dwFlags As Integer, ByRef lpSource As IntPtr, ByVal dwMessageId As Integer, ByVal dwLanguageId As Integer, ByRef lpBuffer As [String], ByVal nSize As Integer, ByRef Arguments As IntPtr) As Integer
	End Function
	<DllImport("kernel32.dll")> _
	Private Shared Function LoadLibraryEx(lpFileName As String, hReservedNull As IntPtr, dwFlags As LoadLibraryFlags) As IntPtr
	End Function

	Public sServerName As String
	Public sServerGUID As String

	Public sServiceEntries As List(Of ServiceController)
	Public hHotfixEntries As List(Of String)
	Public eEventLogEntries As List(Of List(Of EventLogEntry))
	Public rRegistryEntries As List(Of String)
	Public fFileChecks As List(Of String)
	Public wWMIQueries As List(Of String)
	Public dFreeSpaceSize As List(Of ManagementObject)
	Private img16 As New ImageList
	Private img48 As New ImageList
	Private imgSPACING As New ImageList

	Private Sub frmScan_Results_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
		For Each c As Control In Controls
			c.Font = frmMain.sysFont
		Next
		lblServerName.Font = frmMain.sysFontTitle
		lnkHelp.Font = frmMain.sysFontHelp
		lnkNote.Font = frmMain.sysFontHelp
		lnkNote.Top = lnkHelp.Bottom + 1

		lnkHelp.LinkBehavior = LinkBehavior.HoverUnderline
		lnkNote.LinkBehavior = LinkBehavior.HoverUnderline
		Me.Text = " " & sServerName.ToUpper & " - Server Scan Results"
		picIcon.Image = My.Resources._48___Server.ToBitmap

		lblServerName.Text = sServerName.ToUpper
		txtEventLog_Message.Text = vbNullString
		txtEventLog_Message.ScrollBars = ScrollBars.None
		cmdShowServerInfo.Visible = False

		With cmdListAllInstalledHotfixes
			.Visible = False
			.Text = "List All Installed Hotfixes" ' will be either this or "Show Hotfix Patch Issues"
		End With
		With cmdShowCurrentExclusions
			.Location = cmdListAllInstalledHotfixes.Location
			.Visible = False
		End With
		With cmdShowDriveExclusions
			.Location = cmdListAllInstalledHotfixes.Location
			.Visible = False
		End With

		With img16
			.ImageSize = New Size(16, 16)
			.ColorDepth = ColorDepth.Depth32Bit
			With .Images
				.Clear()
				.Add("_16___Eventlog___Critical", My.Resources._16___Eventlog___Critical)			' 0
				.Add("_16___Eventlog___Error", My.Resources._16___Eventlog___Error)					' 1
				.Add("_16___Eventlog___Warning", My.Resources._16___Eventlog___Warning)				' 2
				.Add("_16___Eventlog___Information", My.Resources._16___Eventlog___Information)		' 3
				.Add("_16___Eventlog___Failure", My.Resources._16___Eventlog___Failure)				' 4 - Audit Failure
				.Add("_16___Eventlog___Success", My.Resources._16___Eventlog___Success)				' 5 - Audit Success
				.Add("_16___Registry___HEX", My.Resources._16___Registry___HEX)						' 6
				.Add("_16___Registry___STR", My.Resources._16___Registry___STR)						' 7
				.Add("_16___Scan___Failed", My.Resources._16___Scan___Failed)						' 8
				.Add("_16___Scan___Unknown", My.Resources._16___Scan___Unknown)						' 9

				.Add("_16___Resource___Services", My.Resources._16___Resource___Services)			'
				.Add("_16___Resource___Hotfix", My.Resources._16___Resource___Hotfix)				'
				.Add("_16___Resource___Eventlog", My.Resources._16___Resource___Eventlog)			'
				.Add("_16___Resource___Registry", My.Resources._16___Resource___Registry)			'
				.Add("_16___Resource___File", My.Resources._16___Resource___File)					'
				.Add("_16___Resource___Drive", My.Resources._16___Resource___Drive)					'
				.Add("_16___Resource___WMIQuery", My.Resources._16___Resource___WMIQuery)			'
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
				.Add("_48___Drive_Local___OK", My.Resources._48___Drive_Local___OK)
				.Add("_48___Drive_Local___Warning", My.Resources._48___Drive_Local___Warning)
				.Add("_48___Drive_Local___Critical", My.Resources._48___Drive_Local___Critical)
				.Add("_48___Drive_Local___System", My.Resources._48___Drive_Local___System)
				.Add("_48___Drive_Local___Unknown", My.Resources._48___Drive_Local___Unknown)
			End With
		End With

		With tabControl
			.Padding = New Point(6, 6)
			.ImageList = img16
			.TabPages(0).Text = "Services  " : .TabPages(0).ImageKey = "_16___Resource___Services"
			.TabPages(1).Text = "Hotfixes  " : .TabPages(1).ImageKey = "_16___Resource___Hotfix"
			.TabPages(2).Text = "Eventlog  " : .TabPages(2).ImageKey = "_16___Resource___Eventlog"
			.TabPages(3).Text = "Registry  " : .TabPages(3).ImageKey = "_16___Resource___Registry"
			.TabPages(4).Text = "File Check  " : .TabPages(4).ImageKey = "_16___Resource___File"
			.TabPages(5).Text = "WMI Query  " : .TabPages(5).ImageKey = "_16___Resource___WMIQuery"
			.TabPages(6).Text = "Free Space  " : .TabPages(6).ImageKey = "_16___Resource___Drive"
			.SelectedTab = .TabPages(0)
		End With

		' Separated this, as it was quite long...
		Call configureListViews()
		Call configureMenuOptions()

		lblNoIssues_Services.Visible = True
		lblNoIssues_Hotfixes.Visible = True
		lblNoIssues_Eventlog.Visible = True
		lblNoIssues_Registry.Visible = True
		lblNoIssues_FileScan.Visible = True
		lblNoIssues_WMIQuery.Visible = True
		lblDriveLabel.Text = vbNullString
		lstResults_DriveSpace_Excluded.Visible = False

		Dim sDefaultNoIssuesText As String = "No Issues Found"
		Dim sExceptionMessage As String = "Error occurred during scan"
		Dim lItem As ListViewItem = frmScan.lstResults.SelectedItems(0)
		lblNoIssues_Services.Text = IIf(lItem.SubItems(1).Text = "ICON:7", sExceptionMessage, sDefaultNoIssuesText).ToString
		lblNoIssues_Hotfixes.Text = IIf(lItem.SubItems(2).Text = "ICON:7", sExceptionMessage, sDefaultNoIssuesText).ToString
		lblNoIssues_Eventlog.Text = IIf(lItem.SubItems(3).Text = "ICON:7", sExceptionMessage, sDefaultNoIssuesText).ToString
		lblNoIssues_Registry.Text = IIf(lItem.SubItems(4).Text = "ICON:7", sExceptionMessage, sDefaultNoIssuesText).ToString
		lblNoIssues_FileScan.Text = IIf(lItem.SubItems(5).Text = "ICON:7", sExceptionMessage, sDefaultNoIssuesText).ToString
		lblNoIssues_WMIQuery.Text = IIf(lItem.SubItems(6).Text = "ICON:7", sExceptionMessage, sDefaultNoIssuesText).ToString
		lItem = Nothing

		With lstResults_Services
			.Location = New Point(0, 12)
			.Width = tabControl.TabPages(0).Size.Width
			.Height = tabControl.TabPages(0).Size.Height - .Top

			lstResults_Hotfix.Size = .Size
			lstResults_Registry.Size = .Size
			lstResults_FileChecks.Size = .Size
			lstResults_WMIQuery.Size = .Size
			lstResults_DriveSpace_Background.Size = .Size
		End With
		txtEventLog_Message.Height = tabControl.TabPages(0).Size.Height - txtEventLog_Message.Top


		Me.Visible = True
		Application.DoEvents()
		tabControl.Enabled = False

		' Load Results...
		If (sServiceEntries IsNot Nothing) Then Call loadResults_Services()
		If (hHotfixEntries IsNot Nothing) Then Call loadResults_Hotfixes()
		If (eEventLogEntries IsNot Nothing) Then Call loadResults_Eventlog()
		If (rRegistryEntries IsNot Nothing) Then Call loadResults_Registry()
		If (fFileChecks IsNot Nothing) Then Call loadResults_FileChecks()
		If (wWMIQueries IsNot Nothing) Then Call loadResults_WMIQuery()
		If (dFreeSpaceSize IsNot Nothing) Then Call loadResults_DriveSpace() Else Call loadResults_DriveSpace_NULL()
		Call displayGraphic()
		tabControl.Enabled = True

		If (bUseVisualStyles = True) Then
			Dim cListView() As ctrlListView_CollapseGroups =
			 {lstResults_Services, lstResults_Hotfix, lstResults_Eventlog, lstResults_Registry, lstResults_FileChecks, lstResults_WMIQuery}

			For Each cView As ctrlListView_CollapseGroups In cListView
				Call SetWindowTheme(cView.Handle, "Explorer", Nothing)
				cView.SetGroupState(ListViewGroupState.Collapsible)
				For Each grp As ListViewGroup In cView.Groups
					Call cView.SetGroupFooter(grp)
				Next
			Next
		End If

		' Just in case.!
		proProgress.Visible = False
		Me.Cursor = Cursors.Default
		cmdShowServerInfo.Visible = True
	End Sub

	Private Sub cmdClose_Click(sender As System.Object, e As System.EventArgs) Handles cmdClose.Click
		img16.Dispose()
		img48.Dispose()
		imgSPACING.Dispose()
		mnuEventLog.Dispose()
		mnuHotFixes.Dispose()
		mnuServices.Dispose()
		Me.Close()
		Me.Dispose()
	End Sub

	' #############################################################################################
	Private Sub loadResults_Services()
		Dim lItem As ListViewItem
		proProgress.Maximum = sServiceEntries.Count
		proProgress.Value = 0
		proProgress.Visible = True
		Me.Cursor = Cursors.WaitCursor

		With lstResults_Services
			.BeginUpdate()
			For Each sSC As ServiceController In sServiceEntries
				If (proProgress.Value < proProgress.Maximum) Then proProgress.Value = proProgress.Value + 1
				Application.DoEvents()

				lItem = New ListViewItem(sSC.DisplayName, "_16___Resource___Services")
				lItem.UseItemStyleForSubItems = False

				If (sSC.DisplayName.StartsWith("Missing|") = False) Then
					lItem.SubItems.Add(sSC.Status.ToString, getSubItemColour(sSC.Status.ToString), SystemColors.Window, SystemFonts.DefaultFont)

					Dim gParent As String = getParent(sServerGUID)
					Dim lResources As List(Of XmlElement) = xml_getResourceList(gParent, eDirectionalSearch.ToParents, True)
					If (lResources IsNot Nothing) Then
						For Each xEl As XmlElement In lResources
							If (xEl.GetAttribute("name") = sSC.DisplayName.ToString) Then
								Dim sChecking As String = xEl.GetAttribute("checking").Split("("c)(0).Trim
								lItem.SubItems.Add(sChecking, getSubItemColour(sChecking), SystemColors.Window, SystemFonts.DefaultFont)
							End If
						Next
					End If
					lItem.Group = lstResults_Services.Groups("installed")
					lItem.Tag = sSC.ServiceName

				Else

					lItem.Text = lItem.Text.Remove(0, "Missing|".Length)
					lItem.ForeColor = getSubItemColour("Disabled")
					lItem.SubItems.Add("Missing", getSubItemColour("Disabled"), SystemColors.Window, SystemFonts.DefaultFont)
					lItem.Group = lstResults_Services.Groups("missing")
				End If

				.Items.Add(lItem)
			Next
			.EndUpdate()

			proProgress.Visible = False
			If (.Items.Count > 0) Then lblNoIssues_Services.Visible = False
			.Sort()
			Me.Cursor = Cursors.Default
		End With
	End Sub

	Private Sub loadResults_Hotfixes()
		Dim lItem As ListViewItem
		proProgress.Maximum = hHotfixEntries.Count
		proProgress.Value = 0
		proProgress.Visible = True
		Me.Cursor = Cursors.WaitCursor

		With lstResults_Hotfix
			.BeginUpdate()
			For Each hHF As String In hHotfixEntries
				If (proProgress.Value < proProgress.Maximum) Then proProgress.Value = proProgress.Value + 1
				Application.DoEvents()
				Dim hHFs() As String = hHF.Split("|"c)

				lItem = New ListViewItem(hHFs(0).Trim, "_16___Resource___Hotfix")
				lItem.UseItemStyleForSubItems = False
				lItem.SubItems.Add(hHFs(1).Trim, getSubItemColour(hHFs(1).Trim), SystemColors.Window, SystemFonts.DefaultFont)
				lItem.SubItems.Add(hHFs(2).Trim, getSubItemColour(hHFs(2).Trim), SystemColors.Window, SystemFonts.DefaultFont)
				lItem.Group = CType(IIf(hHFs(1) = "Not Installed", .Groups("noti"), .Groups("inst")), ListViewGroup)
				.Items.Add(lItem)
			Next
			.EndUpdate()
			proProgress.Visible = False
			If (.Items.Count > 0) Then lblNoIssues_Hotfixes.Visible = False
			.Sort()
			Me.Cursor = Cursors.Default
		End With
	End Sub

	Private Sub loadResults_Eventlog()
		Dim lItem As ListViewItem
		proProgress.Value = 0
		proProgress.Visible = True
		Me.Cursor = Cursors.WaitCursor

		With lstResults_Eventlog
			.BeginUpdate()
			If (eEventLogEntries IsNot Nothing) Then
				For i As Integer = 0 To eEventLogEntries.Count - 1
					proProgress.Maximum = proProgress.Maximum + eEventLogEntries.Item(i).Count
				Next
				For i As Integer = 0 To eEventLogEntries.Count - 1
					For Each eELE As EventLogEntry In eEventLogEntries.Item(i)
						If (proProgress.Value < proProgress.Maximum) Then proProgress.Value = proProgress.Value + 1
						Application.DoEvents()

						lItem = New ListViewItem(eELE.EntryType.ToString.Replace("Audit", ""))
						Select Case eELE.EntryType
							Case 0 : lItem.ImageKey = "_16___Eventlog___Critical"
							Case EventLogEntryType.Error : lItem.ImageKey = "_16___Eventlog___Error"
							Case EventLogEntryType.Warning : lItem.ImageKey = "_16___Eventlog___Warning"
							Case EventLogEntryType.Information : lItem.ImageKey = "_16___Eventlog___Information"
							Case EventLogEntryType.SuccessAudit : lItem.ImageKey = "_16___Eventlog___Success"
							Case EventLogEntryType.FailureAudit : lItem.ImageKey = "_16___Eventlog___Failure"
						End Select

						If (lItem.Text.Trim = "0") Then lItem.Text = "Critical"
						lItem.SubItems.Add(eELE.TimeWritten.ToString)
						lItem.SubItems.Add(eELE.Source)
						lItem.SubItems.Add((CLng(eELE.InstanceId) And 65535).ToString)
						Select Case i
							Case 0 : lItem.Group = .Groups("Application")
							Case 1 : lItem.Group = .Groups("System")
							Case 2 : lItem.Group = .Groups("Security")
						End Select
						Dim eData As String = System.Text.Encoding.Unicode.GetString(eELE.Data)
						lItem.Tag = eELE.Message.ToString & vbCrLf & vbCrLf & eData
						.Items.Add(lItem)
					Next
				Next
			End If
			.EndUpdate()
			proProgress.Visible = False
			If (.Items.Count > 0) Then lblNoIssues_Eventlog.Visible = False
			lvwCS_E.SortColumn = .Columns("datetime").Index
			lvwCS_E.Order = SortOrder.Descending
			.Sort()
			Me.Cursor = Cursors.Default
		End With
	End Sub

	Private Sub loadResults_Registry()
		Dim lItem As ListViewItem
		proProgress.Maximum = rRegistryEntries.Count
		proProgress.Value = 0
		proProgress.Visible = True
		Me.Cursor = Cursors.WaitCursor

		With lstResults_Registry
			.BeginUpdate()
			For Each rRK As String In rRegistryEntries
				If (proProgress.Value < proProgress.Maximum) Then proProgress.Value = proProgress.Value + 1
				Application.DoEvents()

				Dim sMeta() As String = Split(rRK, "|")			' Failed/Warning | Incorrect/Missing | [RegistryKey\ValueName]
				Dim sData() As String = Split(sMeta(3), ", ")	' Value Data ,  Value Type ,  MissingState

				Dim iPos As Integer = sMeta(2).LastIndexOf("\")
				lItem = New ListViewItem("HKLM\" & sMeta(2).Substring(0, iPos), IIf(sMeta(1) = "I", "_16___Scan___Failed", "_16___Scan___Unknown").ToString)
				lItem.UseItemStyleForSubItems = False
				lItem.SubItems.Add(sMeta(2).Substring(iPos + 1))
				lItem.SubItems.Add(sData(0))
				lItem.Group = .Groups(IIf(sMeta(1) = "I", "IM", "FW").ToString)
				lItem.Tag = IIf(sMeta(1) = "I", "Incorrect Value Data", "Missing Registry Key Or Value Name").ToString
				.Items.Add(lItem)
			Next
			.EndUpdate()

			proProgress.Visible = False
			If (.Items.Count > 0) Then lblNoIssues_Registry.Visible = False
			.Sort()
			Me.Cursor = Cursors.Default
		End With
	End Sub

	Private Sub loadResults_FileChecks()
		Dim lItem As ListViewItem
		proProgress.Maximum = fFileChecks.Count
		proProgress.Value = 0
		proProgress.Visible = True
		Me.Cursor = Cursors.WaitCursor

		With lstResults_FileChecks
			.BeginUpdate()

			For Each fFC As String In fFileChecks
				If (proProgress.Value < proProgress.Maximum) Then proProgress.Value = proProgress.Value + 1
				Application.DoEvents()

				Dim sData() As String = Split(fFC, "|")
				lItem = New ListViewItem(sData(0), "_16___Resource___File")
				lItem.Group = lstResults_FileChecks.Groups(sData(1))
				lItem.UseItemStyleForSubItems = False
				lItem.SubItems.Add(sData(2))			' Checking

				If (sData(2).ToUpper <> "NOT FOUND") Then	' Actual
					lItem.SubItems.Add(sData(3))
					lItem.SubItems(1).ForeColor = getSubItemColour("Default")
					lItem.SubItems(2).ForeColor = getSubItemColour("Default")
				Else
					lItem.SubItems.Add(sData(3))
					lItem.SubItems(1).ForeColor = getSubItemColour("Error")
					lItem.SubItems(2).ForeColor = getSubItemColour("Disabled")
				End If

				.Items.Add(lItem)
			Next
			.EndUpdate()
			proProgress.Visible = False
			If (.Items.Count > 0) Then lblNoIssues_FileScan.Visible = False
			.Sort()
			Me.Cursor = Cursors.Default
		End With
	End Sub

	Private Sub loadResults_WMIQuery()
		Dim lItem As ListViewItem
		proProgress.Maximum = wWMIQueries.Count
		proProgress.Value = 0
		proProgress.Visible = True
		Me.Cursor = Cursors.WaitCursor

		With lstResults_WMIQuery
			.BeginUpdate()
			For Each wWQ As String In wWMIQueries
				If (proProgress.Value < proProgress.Maximum) Then proProgress.Value = proProgress.Value + 1
				Application.DoEvents()
				Dim wWqs() As String = wWQ.Split("|"c)

				lItem = New ListViewItem(wWqs(0).Trim, "_16___Resource___WMIQuery")
				lItem.UseItemStyleForSubItems = False
				lItem.SubItems.Add(wWqs(1).Trim, getSubItemColour("Stopped"), SystemColors.Window, SystemFonts.DefaultFont)
				lItem.SubItems.Add(wWqs(2).Trim, getSubItemColour("Running"), SystemColors.Window, SystemFonts.DefaultFont)
				Select Case True
					Case wWqs(2).StartsWith("Equ") : lItem.Group = .Groups("et")
					Case wWqs(2).StartsWith("Not") : lItem.Group = .Groups("nt")
					Case wWqs(2).StartsWith("Gre") : lItem.Group = .Groups("gt")
					Case wWqs(2).StartsWith("Les") : lItem.Group = .Groups("lt")
				End Select
				.Items.Add(lItem)
			Next
			.EndUpdate()
			proProgress.Visible = False
			If (.Items.Count > 0) Then lblNoIssues_WMIQuery.Visible = False
			.Sort()
			Me.Cursor = Cursors.Default
		End With
	End Sub

	Private Sub loadResults_DriveSpace()
		Dim lItem As ListViewItem
		Dim iPercent As Integer
		Dim sDrives As String = xml_GetExcludedDrives(sServerGUID.Split("|"c)(0))
		If (sDrives Is Nothing) Then sDrives = " "

		lstResults_DriveSpace_Excluded.Items.Clear()
		With lstResults_DriveSpace
			.BeginUpdate()
			For Each mObj As ManagementObject In dFreeSpaceSize
				If (mObj("Name").ToString = "THIS_ONE") Then

					Dim sDT As String = mObj("Purpose").ToString
					If (sDT IsNot Nothing) Then
						Try
							iDriveThreshold_Fail = CInt(Split(sDT, "|")(0))
							iDriveThreshold_Warn = CInt(Split(sDT, "|")(1))
						Catch ex As Exception
							iDriveThreshold_Fail = 10
							iDriveThreshold_Warn = 20
						End Try
					End If

					' Custom Properties...
					.AlertPercentage = iDriveThreshold_Fail
					.WarningPercentage = iDriveThreshold_Warn

					iPercent = CInt((100 / CLng(mObj("Size"))) * CLng(mObj("FreeSpace")))
					picIcon_Drive.Image = My.Resources._48___Drive_Local___System.ToBitmap

					Dim sVolumeName As String = mObj("VolumeName").ToString
					If (sVolumeName = vbNullString) Then sVolumeName = "Local Disk"
					lblDriveLabel.Text = sVolumeName & " (" & mObj("DeviceID").ToString & ")"
					lblSize.Text = getSizeAndUnits(CLng(mObj("FreeSpace"))) & " free of " & getSizeAndUnits(CLng(mObj("Size"))) & "  (" & iPercent & "% free)"

					' Display system drive state icon...
					Select Case iPercent
						Case 0 To (iDriveThreshold_Fail - 1) : picIcon_DriveState.Image = My.Resources._16___Eventlog___Critical.ToBitmap
						Case iDriveThreshold_Fail To (iDriveThreshold_Warn - 1) : picIcon_DriveState.Image = My.Resources._16___Eventlog___Warning.ToBitmap
						Case Else : picIcon_DriveState.Image = My.Resources._16___Scan___Pass.ToBitmap
					End Select
					DrawGauge(iPercent, picDriveSpace)

				Else
					Try
						' This is a test to see if "mObj('VolumeName')" is nothing (possible cluster resource)
						Console.WriteLine("IGNORE ME: VolumeName: " & mObj("VolumeName").ToString)

						Dim sVolumeName As String = mObj("VolumeName").ToString
						If (sVolumeName = vbNullString) Then sVolumeName = "Local Disk"
						lItem = New ListViewItem("(" & mObj("DeviceID").ToString & ") " & sVolumeName)

						iPercent = CInt((100 / CLng(mObj("Size"))) * CLng(mObj("FreeSpace")))
						lItem.SubItems.Add(iPercent.ToString)
						lItem.SubItems.Add(getSizeAndUnits(CLng(mObj("FreeSpace"))) & " free of " & getSizeAndUnits(CLng(mObj("Size"))) & "  (" & iPercent & "% free)")
						Select Case iPercent
							Case 0 To iDriveThreshold_Fail : lItem.ImageKey = "_48___Drive_Local___Critical"
							Case (iDriveThreshold_Fail + 1) To iDriveThreshold_Warn : lItem.ImageKey = "_48___Drive_Local___Warning"
							Case Else : lItem.ImageKey = "_48___Drive_Local___OK"
						End Select

						If (sDrives.Contains(mObj("DeviceID").ToString & ",") = True) Then
							lItem.Group = .Groups("Excluded")
							lItem.ImageKey = "_48___Drive_Local___Disabled"
							lstResults_DriveSpace_Excluded.Items.Add(lItem)
							lItem = Nothing
						Else
							lItem.Group = .Groups("Drives")
						End If
						If (lItem IsNot Nothing) Then .Items.Add(lItem)

					Catch ex As Exception
						' Add 'empty' drive for possible cluster resource drive
						lItem = New ListViewItem("(" & mObj("DeviceID").ToString & ") Unknown")
						lItem.SubItems.Add("-1")
						lItem.SubItems.Add("")
						If (sDrives.Contains(mObj("DeviceID").ToString & ",") = True) Then
							lItem.Group = .Groups("Excluded")
							lItem.ImageKey = "_48___Drive_Local___Disabled"
						Else
							lItem.Group = .Groups("Unknown")
							lItem.ImageKey = "_48___Drive_Local___Unknown"
						End If
						.Items.Add(lItem)
					End Try
				End If
			Next

			If (lstResults_DriveSpace_Excluded.Items.Count > 0) Then
				lItem = New ListViewItem("Double-click here to", "_48___Drive_Local___Disabled___Show", .Groups("Excluded"))
				lItem.SubItems.Add("show excluded drives " & "(" & lstResults_DriveSpace_Excluded.Items.Count.ToString & ")")
				lItem.SubItems.Add("")
				lstResults_DriveSpace.Items.Add(lItem)
			End If

			.EndUpdate()
			.Sort()

			If .Items.Count = 0 Then
				lItem = New ListViewItem("", -1)
				lItem.Group = .Groups("Drives")
				.Items.Add(lItem)
			End If
		End With
	End Sub
	Private Sub loadResults_DriveSpace_NULL()
		Dim lItem As ListViewItem
		With lstResults_DriveSpace
			lItem = New ListViewItem("Unknown")
			lItem.SubItems.Add("-1")
			lItem.SubItems.Add("0 MB free of 0 MB (0% free)")
			lItem.ImageKey = "_48___Drive_Local___Unknown"
			lItem.Group = .Groups("Drives")
			.Items.Add(lItem)
		End With

		lblDriveLabel.Text = "Unknown System Drive"
		lblSize.Text = "0 MB free of 0 MB (0% free)"

		picIcon_Drive.Image = My.Resources._48___Drive_Local___System.ToBitmap
		picIcon_DriveState.Image = My.Resources._16___Scan___Unknown.ToBitmap
		DrawGauge(-1, picDriveSpace)
	End Sub
	' #############################################################################################

	Private Sub lstResults_Services_MouseUp(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles lstResults_Services.MouseUp
		If (e.Button <> Windows.Forms.MouseButtons.Right) Then Exit Sub
		'If (bIsAdminMode = False) Then Exit Sub
		Dim lItem As ListViewItem = lstResults_Services.GetItemAt(e.X, e.Y)
		If (lItem Is Nothing) Then Exit Sub
		If (lItem.Group.Name = "missing") Then Exit Sub

		mnuServices.Items(0).Enabled = CBool(IIf(lItem.SubItems(1).Text = "Running", False, True))
		mnuServices.Items(1).Enabled = CBool(IIf(lItem.SubItems(1).Text = "Running", True, False))
		mnuServices.Show(lstResults_Services, New Point(e.X, e.Y))
	End Sub

	Private Sub lstResults_Hotfix_Mouseup(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles lstResults_Hotfix.MouseUp
		If (e.Button <> Windows.Forms.MouseButtons.Right) Then Exit Sub
		Dim lItem As ListViewItem = lstResults_Hotfix.GetItemAt(e.X, e.Y)
		If (lItem Is Nothing) Then Exit Sub
		mnuHotFixes.Show(lstResults_Hotfix, New Point(e.X, e.Y))
	End Sub

	Private Sub lstResults_DriveSpace_MouseDoubleClick(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles lstResults_DriveSpace.MouseDoubleClick
		Dim lItem As ListViewItem = lstResults_DriveSpace.SelectedItems(0)
		If ((lItem.ImageKey = "_48___Drive_Local___Disabled___Show") AndAlso (lItem.Group.Name = "Excluded")) Then
			For Each eLVI As ListViewItem In lstResults_DriveSpace_Excluded.Items
				Dim nLVI As New ListViewItem(eLVI.Text, eLVI.ImageKey, lItem.Group)
				nLVI.SubItems.Add(eLVI.SubItems(1).Text)
				nLVI.SubItems.Add(eLVI.SubItems(2).Text)
				lstResults_DriveSpace.Items.Add(nLVI)
				nLVI = Nothing
			Next
			lItem.Remove()
		End If
	End Sub

	Private Sub lstResults_Eventlog_MouseUp(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles lstResults_Eventlog.MouseUp
		If (e.Button <> Windows.Forms.MouseButtons.Right) Then Exit Sub
		Dim lsNode As ListViewItem = lstResults_Eventlog.GetItemAt(e.X, e.Y)
		If (lsNode Is Nothing) Then Exit Sub

		' Check and show menu only if scan method was "Excluding", but not "Specifically"
		Dim sEventlogType As String = Split(lstResults_Eventlog.SelectedItems(0).Group.Name, " ")(0)
		Dim lResources As List(Of XmlElement) = xml_getResourceList(getParent(sServerGUID), eDirectionalSearch.ToParents, True)

		If (lResources IsNot Nothing) Then
			For Each xEl As XmlElement In lResources
				If ((xEl.Attributes.ItemOf("type").Value = "Eventlog Scan") AndAlso (xEl.Attributes.ItemOf("name").Value = sEventlogType)) Then
					If (xEl.Attributes.ItemOf("checking").Value.Contains("Specifically:")) Then
						mnuEventLog.Items(0).Enabled = False
						mnuEventLog.Items(1).Enabled = Not bReadOnlyMode
					Else
						mnuEventLog.Items(0).Enabled = Not bReadOnlyMode
						mnuEventLog.Items(1).Enabled = False
					End If
				End If
			Next
		End If

		' Show "Add EventID To Excluded List" menu...
		mnuEventLog.Show(lstResults_Eventlog, New Point(e.X, e.Y))
	End Sub

	Private Sub lstResults_Eventlog_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles lstResults_Eventlog.SelectedIndexChanged
		If (lstResults_Eventlog.SelectedItems.Count = 1) Then
			Dim lItem As ListViewItem = lstResults_Eventlog.SelectedItems(0)
			Dim i As Integer = 0
			Dim sMsg As String = lItem.Tag.ToString

			i = InStr(sMsg, "The local computer may not have the necessary registry information or message DLL files to display the message")
			If (i > 0) Then
				Dim sGroup = Split(lItem.Group.ToString, " ")(0)
				Dim sID As String = sMsg.Substring(InStr(sMsg, "'"))
				sID = sID.Substring(0, InStr(sID, "'") - 1)

				i = InStr(sMsg, "The following information is part of the event:") ' Length of string : 47
				Dim sArgs() As String = Nothing
				If (i > 0) Then sArgs = sMsg.Substring(i + 46).Split(CChar(","))
				sMsg = getExernalErrorFile(sGroup, lItem.SubItems(2).Text, sID, sArgs)
			End If

			i = InStr(sMsg, "The following information is part of the event:") ' Length of string : 47
			If (i > 0) Then sMsg = sMsg.Substring(i + 46)

			i = InStr(sMsg, "%%")
			If (i > 0) Then sMsg = sMsg.Substring(0, i - 1) & DllErrorInfo(CInt(sMsg.Substring(i + 1)))

			If (sMsg = vbNullString) Then sMsg = lItem.Tag.ToString
			txtEventLog_Message.Text = sMsg.Trim
			Call txtShowHideScrollbars(txtEventLog_Message)
		End If
	End Sub
	Private Function DllErrorInfo(ByVal ErrN As Integer) As String
		Dim sBuffer As String = Space(254)
		FormatMessageA(FormatMessageFlags.FORMAT_MESSAGE_FROM_SYSTEM, 0, ErrN, Languages.LANG_NEUTRAL, sBuffer, sBuffer.Length, Nothing)
		Return sBuffer.Trim
	End Function

	' Given event log source and ID number, read registry of remote computer and get file name of event log messages.
	' HKLM\System\CurrentControlSet\services\eventlog\[Application|System|Security]\(source)\eventmessagefile
	' read semi-colon separated file list...
	Private Function getExernalErrorFile(ByVal sEventLog As String, ByVal sSource As String, ByVal sMessageID As String, ByVal sArgs() As String) As String
		Dim sResult As String = vbNullString
		Dim rKey As RegistryKey

		Try
			rKey = RegistryKey.OpenRemoteBaseKey(RegistryHive.LocalMachine, sServerName).OpenSubKey("System\CurrentControlSet\services\eventlog\" & sEventLog & "\" & sSource, False)
			If (rKey Is Nothing) Then Return Nothing

			' Get DLL/OCX/EXE name of message resource file (old method)...
			Try
				sResult = rKey.GetValue("eventmessagefile", vbNullString, RegistryValueOptions.DoNotExpandEnvironmentNames).ToString
			Catch ex As Exception
			End Try
			If (sResult Is vbNullString) Then
				' Not found, try the 'new' method...
				sResult = rKey.GetValue("providerGuid", vbNullString, RegistryValueOptions.DoNotExpandEnvironmentNames).ToString
				If (sResult Is vbNullString) Then Return Nothing

				' Get DLL/OCX/EXE name of message resource file (new method)...
				rKey = RegistryKey.OpenRemoteBaseKey(RegistryHive.LocalMachine, sServerName).OpenSubKey("SOFTWARE\Microsoft\Windows\CurrentVersion\WINEVT\Publishers\" & sResult, False)
				If (rKey Is Nothing) Then Return Nothing
				sResult = rKey.GetValue("MessageFileName", vbNullString, RegistryValueOptions.DoNotExpandEnvironmentNames).ToString
				If (sResult = vbNullString) Then Return Nothing
			End If
			rKey.Close()
		Catch ex As Exception
			Return Nothing
		End Try

		Dim sBuffer As String = vbNullString
		For Each sFile As String In Split(sResult, ";")
			Try
				sBuffer = Space(5120)
				sFile = sFile.ToLower
				sFile = ReplaceMultiple(sFile, vbNullString, {"%windir%", "%systemroot%", "c:\windows"})

				Dim sFullPath As String = "\\" & sServerName & "\admin$" & sFile

				Dim sMUI As String = "\\" & sServerName & "\admin$\system32\en-us\" & sFile.Substring(sFile.LastIndexOf("\") + 1) & ".mui"
				If (System.IO.File.Exists(sMUI) = True) Then sFullPath = sMUI

				Dim ll As Long = 0
				If (System.IO.File.Exists(sFullPath) = True) Then
					ll = LoadLibrary(sFullPath)
					Dim fm As Long = FormatMessageA(FormatMessageFlags.FORMAT_MESSAGE_FROM_HMODULE Or FormatMessageFlags.FORMAT_MESSAGE_ARGUMENT_ARRAY, ll, CInt(sMessageID), Languages.LANG_NEUTRAL, sBuffer, sBuffer.Length, sArgs)
				End If
				FreeLibrary(CType(ll, IntPtr))
				Return sBuffer.Trim
			Catch ex As Exception
				Return Nothing		' Can't get remote file
			End Try
		Next

		If ((Err.LastDllError = 1812) Or (Err.LastDllError = 1813)) Then Return Nothing
		Return sBuffer.Trim
	End Function

	' #############################################################################################

	Private Sub displayGraphic()
		picDTBackground.Image = My.Resources.fs_bg_long
		picDTBackground.BringToFront()

		With picSpaceG
			.Image = My.Resources.fs_c_g
			.Location = New Point((picDTBackground.Left + 1), (picDTBackground.Top + 1))
			.Size = New Size(picDTBackground.Width - 2, 12)
			.BringToFront()
		End With

		With picEndcapL
			.Image = My.Resources.fs_l_r
			.Location = New Point((picDTBackground.Left + 1), (picDTBackground.Top + 1))
			.Size = New Size(2, 12)
			.BringToFront()
		End With

		With picEndcapR
			.Image = My.Resources.fs_r_g
			.Location = New Point((picDTBackground.Left + picDTBackground.Width - 3), (picDTBackground.Top + 1))
			.Size = New Size(2, 12)
			.BringToFront()
		End With

		Dim iWidth As Integer = CInt((((picDTBackground.Width) - 2) / 100) * iDriveThreshold_Fail)
		With picSpaceR
			.Image = My.Resources.fs_c_r
			.Location = New Point((picDTBackground.Left + 1), (picDTBackground.Top + 1))
			.Size = New Size(iWidth, (picDTBackground.Height - 2))
			.BringToFront()
		End With

		iWidth = CInt(((picDTBackground.Width - 2) / 100) * iDriveThreshold_Warn) - picSpaceR.Width
		With picSpaceY
			.Image = My.Resources.fs_c_y
			.Location = New Point((picSpaceR.Left + picSpaceR.Width), (picDTBackground.Top + 1))
			.Size = New Size(iWidth, (picDTBackground.Height - 2))
			.BringToFront()
		End With

		lblCurrent_Critical.Text = "Critical: " & iDriveThreshold_Fail & "%"
		lblCurrent_Warning.Text = "Warning: " & iDriveThreshold_Warn & "%"
	End Sub

	Public Sub DrawGauge(ByVal iInputValue As Integer, ByVal PieGraphic As PictureBox)
		Dim BackBuffer As New Bitmap(PieGraphic.Width, PieGraphic.Height, Imaging.PixelFormat.Format32bppArgb)
		Using g As Graphics = Graphics.FromImage(BackBuffer)
			g.SmoothingMode = SmoothingMode.AntiAlias

			Dim sAngle As Integer = 45
			Dim eAngle As Integer = 315
			Dim nAngle As Integer = iDriveThreshold_Fail
			Dim StartAngle As Single = sAngle
			Dim SweepAngle As Single = CSng(((eAngle - sAngle) / 100) * nAngle)
			Dim sPoint As PointF
			Dim rPieSize As Rectangle = New Rectangle(15, 15, 170, 170)		' Outside Size
			Dim rPieHole As Rectangle = New Rectangle(55, 55, 110, 110)		' Inside Size
			Dim rPointOt As Rectangle = New Rectangle(5, 5, 190, 190)		' Pointer Outside
			Dim rPointIn As Rectangle = New Rectangle(95, 95, 10, 10)		' Pointer Inside

			If (iInputValue > -1) Then
				' Error
				g.FillPie(bColorR, rPieSize, StartAngle, SweepAngle)
				nAngle = (iDriveThreshold_Warn - iDriveThreshold_Fail)
				StartAngle = StartAngle + SweepAngle
				SweepAngle = CSng(((eAngle - sAngle) / 100) * nAngle)

				' Warning
				g.FillPie(bColorY, rPieSize, StartAngle, SweepAngle)
				nAngle = (100 - iDriveThreshold_Warn)
				StartAngle = (StartAngle + SweepAngle)
				SweepAngle = CSng(((eAngle - sAngle) / 100) * nAngle)

				' OK()
				g.FillPie(bColorG, rPieSize, StartAngle, SweepAngle)
			Else
				g.FillPie(SystemBrushes.Control, rPieSize, sAngle, eAngle - sAngle)
			End If

			' Doughnut Hole
			g.FillPie(SystemBrushes.Window, rPieHole, 0, 360)

			' Borders
			g.DrawArc(SystemPens.ControlDarkDark, rPieSize, sAngle, eAngle - sAngle)
			g.DrawArc(SystemPens.ControlDarkDark, rPieHole, sAngle, eAngle - (sAngle + 17))

			' Draw Pointer
			If (iInputValue > -1) Then
				' Get X/Y point of current input value 
				Using gp As New GraphicsPath
					gp.AddArc(rPointOt, CSng(((eAngle - sAngle) / 100) * iInputValue) + sAngle, 1)
					sPoint = gp.PathPoints(0)
				End Using

				For i As Integer = 0 To 360 Step 5
					Using gp As GraphicsPath = New GraphicsPath
						gp.AddArc(rPointIn, CSng(((eAngle - sAngle) / 100) * i) + sAngle, 1)
						Dim ePoint As PointF = gp.PathPoints(0)
						g.DrawLine(Pens.Black, sPoint, ePoint)
					End Using
				Next
			End If

			BackBuffer.RotateFlip(RotateFlipType.Rotate90FlipNone)
			PieGraphic.Image = BackBuffer
		End Using
		BackBuffer = Nothing
	End Sub

	Private Sub configureListViews()
		lvwCS_S = New ListViewColumnSorter()
		lvwCS_H = New ListViewColumnSorter()
		lvwCS_E = New ListViewColumnSorter()
		lvwCS_R = New ListViewColumnSorter()
		lvwCS_F = New ListViewColumnSorter()
		lvwCS_W = New ListViewColumnSorter()
		lvwCS_D = New ListViewColumnSorter()

		With lstResults_Services
			.Clear()
			.FullRowSelect = True
			.SmallImageList = imgSPACING
			.View = View.Details
			.HeaderStyle = ColumnHeaderStyle.Clickable
			.ListViewItemSorter = lvwCS_S
			.MultiSelect = False
			.ShowGroups = True
			With .Groups
				.Add("installed", "Installed Services")
				.Add("missing", "Missing Services")
			End With
			With .Columns
				.Add("name", "Service Name", lstResults_Services.Width - (75 + 75) - SystemInformation.VerticalScrollBarWidth - 4)
				.Add("current", "State", 75, HorizontalAlignment.Center, -1)
				.Add("checking", "Should Be", 75, HorizontalAlignment.Center, -1)
			End With
			lvwCS_S.SortColumn = .Columns("name").Index
			lvwCS_S.Order = SortOrder.Ascending
		End With

		With lstResults_Hotfix
			.Clear()
			.FullRowSelect = True
			.SmallImageList = imgSPACING
			.View = View.Details
			.HeaderStyle = ColumnHeaderStyle.Clickable
			.ListViewItemSorter = lvwCS_H
			.MultiSelect = False
			.ShowGroups = True
			With .Groups
				.Add("noti", "Not Installed")
				.Add("inst", "Installed")
			End With
			With .Columns
				.Add("name", "Hotfix ID", lstResults_Hotfix.Width - (75 + 75) - SystemInformation.VerticalScrollBarWidth - 4)
				.Add("state", "State", 75, HorizontalAlignment.Center, -1)
				.Add("checking", "Should Be", 75, HorizontalAlignment.Center, -1)
			End With
			lvwCS_H.SortColumn = .Columns("name").Index
			lvwCS_H.Order = SortOrder.Ascending
		End With

		With lstResults_Eventlog
			.Clear()
			.FullRowSelect = True
			.SmallImageList = imgSPACING
			.View = View.Details
			.HeaderStyle = ColumnHeaderStyle.Clickable
			.ListViewItemSorter = lvwCS_E
			.MultiSelect = False
			.ShowGroups = True
			With .Groups
				.Add("Application", "Application Log")
				.Add("System", "System Log")
				.Add("Security", "Security Log")
			End With
			With .Columns
				.Add("level", "Level", 100, HorizontalAlignment.Left, -1)
				.Add("datetime", "Date Time", 125, HorizontalAlignment.Left, -1)
				.Add("source", "Source", lstResults_Eventlog.Width - (100 + 125 + 75) - SystemInformation.VerticalScrollBarWidth - 4)
				.Add("eventid", "Event ID", 75, HorizontalAlignment.Right, -1)
			End With
			lvwCS_E.SortColumn = .Columns("datetime").Index
			lvwCS_E.Order = SortOrder.Ascending
		End With

		With lstResults_Registry
			.Clear()
			.FullRowSelect = True
			.SmallImageList = imgSPACING
			.View = View.Details
			.HeaderStyle = ColumnHeaderStyle.Clickable
			.ListViewItemSorter = lvwCS_R
			.MultiSelect = False
			.ShowGroups = True
			With .Groups
				.Add("IM", "Incorrect Value Data")
				.Add("FW", "Missing Registry Key Or Value Name")
			End With
			With .Columns
				.Add("key", "Registry Key", lstResults_Registry.Width - (75 + 75) - SystemInformation.VerticalScrollBarWidth - 4)
				.Add("name", "Name", 75)
				.Add("data", "Data", 75)
			End With
			lvwCS_R.SortColumn = .Columns("key").Index
			lvwCS_R.Order = SortOrder.Ascending
		End With

		With lstResults_FileChecks
			.Clear()
			.FullRowSelect = True
			.SmallImageList = imgSPACING
			.View = View.Details
			.HeaderStyle = ColumnHeaderStyle.Clickable
			.ListViewItemSorter = lvwCS_F
			.MultiSelect = False
			.ShowGroups = True
			With .Groups
				.Add("fnf", "File Not Found")
				.Add("fif", "File Should Not Exist")
				.Add("ver", "Version Mismatch (Should Be Equal To)")
				.Add("vrl", "Version Mismatch (Should Be Equal Or Less Than)")
				.Add("vrg", "Version Mismatch (Should Be Equal Or Greater Than)")
				.Add("wdb", "Wrong Date (Should Be On Or Before)")
				.Add("wda", "Wrong Date (Should Be On Or After)")
			End With
			With .Columns
				.Add("name", "Filename", lstResults_FileChecks.Width - (75 + 75) - SystemInformation.VerticalScrollBarWidth - 4)
				.Add("actual", "Actual", 75, HorizontalAlignment.Center, -1)
				.Add("checking", "Checking", 75, HorizontalAlignment.Center, -1)
			End With
			lvwCS_F.SortColumn = .Columns("name").Index
			lvwCS_F.Order = SortOrder.Ascending
		End With

		With lstResults_WMIQuery
			.Clear()
			.FullRowSelect = True
			.SmallImageList = imgSPACING
			.View = View.Details
			.HeaderStyle = ColumnHeaderStyle.Clickable
			.ListViewItemSorter = lvwCS_W
			.MultiSelect = False
			.ShowGroups = True
			With .Groups
				.Add("et", "Equal To")
				.Add("nt", "Not Equal To")
				.Add("gt", "Greater Than")
				.Add("lt", "Less Than")
			End With
			With .Columns
				.Add("query", "WMI Query", lstResults_WMIQuery.Width - (75 + 125) - SystemInformation.VerticalScrollBarWidth - 4)
				.Add("actual", "Actual", 75, HorizontalAlignment.Center, -1)
				.Add("checking", "Checking", 125, HorizontalAlignment.Center, -1)
			End With
			lvwCS_W.SortColumn = .Columns("query").Index
			lvwCS_W.Order = SortOrder.Ascending
		End With

		With lstResults_DriveSpace
			.BorderStyle = BorderStyle.None
			.Clear()
			.FullRowSelect = True
			.LargeImageList = img48
			.View = View.Tile
			.ListViewItemSorter = lvwCS_D
			.ShowGroups = True
			With .Groups
				.Add("Drives", "Local Server Drives")
				.Add("Unknown", "Unknown Drives")
				.Add("Excluded", "Excluded Drives")
			End With
			With .Columns
				.Add("label", "Drive Label")			' No spacing required,
				.Add("size", "Size And Free Space")		' using Large Image List
				.Add("free", "Free Space Percentage")	' and a custom drawn list
			End With
			.TileSize = New Size(.Width - SystemInformation.VerticalScrollBarWidth - 4, 52)
			lvwCS_D.SortColumn = .Columns("label").Index
			lvwCS_D.Order = SortOrder.Ascending
		End With

		' ####################

		Call ShowListViewSortImage(lstResults_Eventlog, lvwCS_E.SortColumn, lvwCS_E.Order)
		Call ShowListViewSortImage(lstResults_FileChecks, lvwCS_F.SortColumn, lvwCS_F.Order)
		Call ShowListViewSortImage(lstResults_Hotfix, lvwCS_H.SortColumn, lvwCS_H.Order)
		Call ShowListViewSortImage(lstResults_Registry, lvwCS_R.SortColumn, lvwCS_R.Order)
		Call ShowListViewSortImage(lstResults_Services, lvwCS_S.SortColumn, lvwCS_S.Order)
		Call ShowListViewSortImage(lstResults_WMIQuery, lvwCS_W.SortColumn, lvwCS_W.Order)
	End Sub

	Private Sub configureMenuOptions()
		mnuHotFixes = New ContextMenuStrip
		mnuEventLog = New ContextMenuStrip
		mnuServices = New ContextMenuStrip

		With mnuHotFixes.Items
			.Clear()
			.Add("Open Hotfix Information Page", My.Resources._16___Copy.ToBitmap, AddressOf OpenHotfixInformationPageToolStripMenuItem_Click)
		End With

		With mnuEventLog.Items
			.Clear()
			.Add("Add To Excluded List", My.Resources._16___Add.ToBitmap, AddressOf AddToExcludedListToolStripMenuItem_Click)
			.Add("Remove From Specific List", My.Resources._16___Remove.ToBitmap, AddressOf RemoveFromSpecificListToolStripMenuItem_Click)
			.Item(0).Enabled = Not bReadOnlyMode
			.Item(1).Enabled = Not bReadOnlyMode
		End With

		With mnuServices.Items
			.Clear()
			.Add("Start Service", My.Resources._16___Blank.ToBitmap, AddressOf ServiceToolStripMenuItem_Click)
			.Add("Stop Service", My.Resources._16___Blank.ToBitmap, AddressOf ServiceToolStripMenuItem_Click)
			.Item(0).Enabled = bIsAdminMode
			.Item(1).Enabled = bIsAdminMode
		End With
	End Sub

	Private Function getParent(ByVal sGUID As String) As String
		Try
			Dim nNode As XmlNode
			If (sGUID.Contains("|") = True) Then sGUID = Split(sGUID, "|")(0).ToString
			nNode = xmlDoc.SelectSingleNode("descendant::*[@guid='" & sGUID & "']")
			Return nNode.ParentNode.Attributes("guid").Value
		Catch
			Return Nothing
		End Try
	End Function

	Private Sub OpenHotfixInformationPageToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs)
		Process.Start("http://support.microsoft.com/kb/" & lstResults_Hotfix.SelectedItems(0).Text.Replace("KB", ""))
	End Sub

	Private Sub ServiceToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs)
		Dim scService As ServiceController = Nothing
		scService = New ServiceController(lstResults_Services.SelectedItems(0).Text.Trim, sServerName)
		frmServiceControl.sService = scService
		frmServiceControl.bCheckStarting = sender.ToString.StartsWith("Start")
		frmServiceControl.sServerName = sServerName
		frmServiceControl.ShowDialog(Me)

		If (scService IsNot Nothing) Then scService.Dispose()
		If (frmServiceControl.bResult = True) Then
			lstResults_Services.SelectedItems(0).Remove()
			frmServiceControl.Close()
		End If
	End Sub

	Private Sub AddToExcludedListToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs)
		Dim lELS As New List(Of XmlElement)
		Dim sEventlogType As String = Split(lstResults_Eventlog.SelectedItems(0).Group.Name, " ")(0)
		Dim lResources As List(Of XmlElement) = xml_getResourceList(getParent(sServerGUID), eDirectionalSearch.ToParents, True)

		lELS.Clear()
		If (lResources IsNot Nothing) Then
			For Each xEl As XmlElement In lResources
				If ((xEl.Attributes.ItemOf("type").Value = "Eventlog Scan") AndAlso (xEl.Attributes.ItemOf("name").Value = sEventlogType)) Then
					lELS.Add(xEl)
					Exit For
				End If
			Next
		End If

		' Add exception
		Dim sEventID As String = lstResults_Eventlog.SelectedItems(0).SubItems(3).Text
		Dim sException As String = lELS(0).GetAttribute("checking")
		If (sException.Contains("Specifically:") = True) Then Exit Sub

		If (sException.EndsWith("Excluding: ") = True) Then
			sException = sException & sEventID
		Else
			Dim sExcept As String = sException.Substring(0, InStr(sException, "Excluding: ") + 10)
			Dim sIDList As String = sException.Substring(InStr(sException, "Excluding: ") + 10) & ", " & sEventID
			sExcept = sExcept & cleanEventlogIDList(sIDList)
			sException = sExcept
		End If

		If (xml_setResourceValue(lELS(0).GetAttribute("guid"), lELS(0).GetAttribute("name"), sException) = True) Then
			' Remove all of this type from current list
			For Each lItem As ListViewItem In lstResults_Eventlog.Items
				If (lItem.Group.Header.ToString = (sEventlogType & " Log")) Then
					If (lItem.SubItems(3).Text = sEventID) Then lItem.Remove()
				End If
			Next

			Call lstResults_Eventlog.SetGroupFooter(lstResults_Eventlog.Groups(sEventlogType))
		Else
			MessageBox.Show(Me, "Could not add entry to Eventlog Scan.", APN, MessageBoxButtons.OK, MessageBoxIcon.Error)
		End If
	End Sub

	Private Sub RemoveFromSpecificListToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs)
		Dim lELS As New List(Of XmlElement)
		Dim sEventlogType As String = Split(lstResults_Eventlog.SelectedItems(0).Group.Name, " ")(0)
		Dim lResources As List(Of XmlElement) = xml_getResourceList(getParent(sServerGUID), eDirectionalSearch.ToParents, True)

		lELS.Clear()
		If (lResources IsNot Nothing) Then
			For Each xEl As XmlElement In lResources
				If ((xEl.Attributes.ItemOf("type").Value = "Eventlog Scan") AndAlso (xEl.Attributes.ItemOf("name").Value = sEventlogType)) Then
					lELS.Add(xEl)
					Exit For
				End If
			Next
		End If

		' Remove entry
		Dim sEventID As Integer = CInt(lstResults_Eventlog.SelectedItems(0).SubItems(3).Text)
		Dim sSpecific As String = lELS(0).GetAttribute("checking")
		If (sSpecific.Contains("Excluding:") = True) Then Exit Sub

		Dim sCleanIDs As String = "Specifically: " & cleanEventlogIDList(Split(sSpecific, "Specifically: ")(1), New Interval(sEventID, sEventID))
		If (xml_setResourceValue(lELS(0).GetAttribute("guid"), lELS(0).GetAttribute("name"), sCleanIDs) = True) Then
			' Remove all of this type from current list
			For Each lItem As ListViewItem In lstResults_Eventlog.Items
				If (lItem.Group.Header.ToString = sEventlogType & " Log") Then
					If (lItem.SubItems(3).Text = sEventID.ToString) Then lItem.Remove()
				End If
			Next

			Call lstResults_Eventlog.SetGroupFooter(lstResults_Eventlog.Groups(sEventlogType))
		Else
			MessageBox.Show(Me, "Could not add entry to Eventlog Scan.", APN, MessageBoxButtons.OK, MessageBoxIcon.Error)
		End If
	End Sub

	Private Sub cmdListAllInstalledHotfixes_Click(sender As System.Object, e As System.EventArgs) Handles cmdListAllInstalledHotfixes.Click

		cmdListAllInstalledHotfixes.Enabled = False
		If (cmdListAllInstalledHotfixes.Text = "Show Hotfix Patch Issues") Then
			' SHOW ISSUE LIST
			Call loadResults_Hotfixes()
			cmdListAllInstalledHotfixes.Text = "List All Installed Hotfixes"
			cmdListAllInstalledHotfixes.Enabled = True
		Else
			' SHOW EXISTING LIST
			lblNoIssues_Hotfixes.Visible = False
			Me.Cursor = Cursors.WaitCursor

			Dim oResult As ManagementObjectCollection = wmiConnect("\\" & Trim(sServerName) & "\root\cimv2", "SELECT HotFixID, Description, InstalledOn FROM Win32_QuickFixEngineering", 30)
			If (oResult Is Nothing) Then
				Me.Cursor = Cursors.Default
				Dim sMSG As String = "Can't get list of installed Hotfixes" & vbCrLf & "Please try again later."
				MessageBox.Show(Me, sMSG, APN, MessageBoxButtons.OK, MessageBoxIcon.Error)
				lblNoIssues_Hotfixes.Visible = True
				Exit Sub
			End If
			oResult.Dispose()

			With lstResults_Hotfix
				.Clear()
				With .Groups
					.Add("Pri", "Security Updates And Hotfix Patches")
					.Add("Sec", "Software Updates And Patches")
				End With
				With .Columns
					.Add("name", "Hotfix ID", 125, HorizontalAlignment.Left, -1)
					.Add("type", "Type / Description", 390, HorizontalAlignment.Left, -1)
					.Add("date", "Installed", 75, HorizontalAlignment.Right, -1)
				End With
			End With

			Dim lItem As ListViewItem
			With lstResults_Hotfix
				.BeginUpdate()
				For Each obj As ManagementObject In oResult
					If (obj("HotFixID").ToString.StartsWith("KB") = True) Then
						lItem = New ListViewItem(obj("HotFixID").ToString)
						lItem.ImageKey = "_16___Resource___Hotfix"
						lItem.SubItems.Add(obj("Description").ToString)
						lItem.Group = .Groups("Pri")
						lItem.SubItems.Add(":")

						Dim dtValue As String = obj("InstalledOn").ToString
						If (dtValue <> vbNullString) Then
							If (dtValue.Length = 16) Then
								lItem.SubItems(2).Text = DateTime.FromFileTimeUtc(CLng("&H" & dtValue)).ToString("yyyy/MM/dd")
							Else
								Dim dtDate As DateTime
								If (DateTime.TryParse(dtValue, dtDate) = True) Then lItem.SubItems(2).Text = dtDate.ToString("yyyy/MM/dd")
							End If
							If (lItem.SubItems(2).Text = ":") Then lItem.SubItems(2).Text = ":" & dtValue

						Else
							lItem.SubItems(1).Text = "Unknown"
						End If
						.Items.Add(lItem)
					End If
				Next
				.EndUpdate()
				oResult.Dispose()
			End With

			' Now format incorrect the dates correctly...
			Call cleanHotfixDateList(lstResults_Hotfix, 2)

			' Software Updates - only accessable if running as an admin
			'
			If (bIsAdminMode = True) Then
				Dim sResult As List(Of String) = getWindowsUpdateSessionDetails(sServerName, False)
				If (sResult IsNot Nothing) Then
					With lstResults_Hotfix
						.BeginUpdate()
						For Each sItem As String In sResult
							lItem = New ListViewItem(Split(sItem, "|")(0))
							lItem.ImageKey = "_16___Resource___Hotfix"
							lItem.SubItems.Add(Split(sItem, "|")(1))
							lItem.SubItems.Add(Split(sItem, "|")(2))
							lItem.Group = .Groups("Sec")
							.Items.Add(lItem)
						Next
						.EndUpdate()
					End With
				End If
			End If

			lstResults_Hotfix.HeaderStyle = ColumnHeaderStyle.Clickable
			Me.Cursor = Cursors.Default

			cmdListAllInstalledHotfixes.Text = "Show Hotfix Patch Issues"
			cmdListAllInstalledHotfixes.Enabled = True
		End If
	End Sub

	Private Sub tabControl_MouseUp(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles tabControl.MouseUp
		cmdListAllInstalledHotfixes.Visible = False
		cmdShowCurrentExclusions.Visible = False
		cmdShowDriveExclusions.Visible = False

		Select Case tabControl.SelectedIndex
			Case 0 ' Services
			Case 1 ' Hotfixes
				cmdListAllInstalledHotfixes.Visible = True
			Case 2 ' Eventlog
				cmdShowCurrentExclusions.Visible = True
			Case 3 ' Registry
			Case 4 ' File Checks
			Case 5 ' WMI Query
			Case 6 ' Free Space
				cmdShowDriveExclusions.Visible = True
			Case Else
		End Select
	End Sub

	Private Sub lnkHelp_LinkClicked(sender As System.Object, e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lnkHelp.LinkClicked
		frmHelp.sSelectPageByID = "help0018"
		frmHelp.ShowDialog(Me)
	End Sub
	Private Sub lnkNote_LinkClicked(sender As System.Object, e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lnkNote.LinkClicked
		frmHelp.sSelectPageByID = "help0022"
		frmHelp.ShowDialog(Me)
	End Sub

	Private Sub cmdShowCurrentExclusions_Click(sender As System.Object, e As System.EventArgs) Handles cmdShowCurrentExclusions.Click
		With frmResourceExclusionsEventLog
			.sGroupGUID = getParent(sServerGUID.Split("|"c)(0))
			.ShowDialog(Me)
		End With
	End Sub

	Private Sub cmdShowDriveExclusions_Click(sender As System.Object, e As System.EventArgs) Handles cmdShowDriveExclusions.Click
		With frmPropertiesServerDriveExclude
			.sServerGUID = sServerGUID.Split("|"c)(0)
			.sServerName = sServerName
			.ShowDialog(Me)
		End With
		Call showChangeMessagebox()
	End Sub

	Private Sub cmdShowServerInfo_Click(sender As System.Object, e As System.EventArgs) Handles cmdShowServerInfo.Click
		frmPropertiesServer.sServerName = sServerName
		frmPropertiesServer.sServerGUID = sServerGUID.Split("|"c)(0)
		frmPropertiesServer.ShowDialog(Me)
		Call showChangeMessagebox()
	End Sub

	Private Sub txtEventLog_Message_KeyUp(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles txtEventLog_Message.KeyUp
		If (e.Control And e.KeyCode = Keys.A) Then txtEventLog_Message.SelectAll()
	End Sub

	Private Sub showChangeMessagebox()
		MessageBox.Show(Me, "Any changes will not be seen until this server is rescanned.", APN, MessageBoxButtons.OK, MessageBoxIcon.Information)
	End Sub

	' #############################################################################################
	Private Sub lstResults_Eventlog_ColumnClick(sender As Object, e As System.Windows.Forms.ColumnClickEventArgs) Handles lstResults_Eventlog.ColumnClick
		Call changeSort(lstResults_Eventlog, e.Column, lvwCS_E)
	End Sub
	Private Sub lstResults_FileChecks_ColumnClick(sender As Object, e As System.Windows.Forms.ColumnClickEventArgs) Handles lstResults_FileChecks.ColumnClick
		Call changeSort(lstResults_FileChecks, e.Column, lvwCS_F)
	End Sub
	Private Sub lstResults_Hotfix_ColumnClick(sender As Object, e As System.Windows.Forms.ColumnClickEventArgs) Handles lstResults_Hotfix.ColumnClick
		Call changeSort(lstResults_Hotfix, e.Column, lvwCS_H)
	End Sub
	Private Sub lstResults_Registry_ColumnClick(sender As Object, e As System.Windows.Forms.ColumnClickEventArgs) Handles lstResults_Registry.ColumnClick
		Call changeSort(lstResults_Registry, e.Column, lvwCS_R)
	End Sub
	Private Sub lstResults_Services_ColumnClick(sender As Object, e As System.Windows.Forms.ColumnClickEventArgs) Handles lstResults_Services.ColumnClick
		Call changeSort(lstResults_Services, e.Column, lvwCS_S)
	End Sub
	Private Sub lstResults_WMIQuery_ColumnClick(sender As Object, e As System.Windows.Forms.ColumnClickEventArgs) Handles lstResults_WMIQuery.ColumnClick
		Call changeSort(lstResults_WMIQuery, e.Column, lvwCS_W)
	End Sub
	Private Sub changeSort(ByVal lvObject As ListView, ByVal iColumn As Integer, ByRef lvSort As ListViewColumnSorter)
		If (iColumn = lvSort.SortColumn) Then
			If (lvSort.Order = SortOrder.Ascending) Then
				lvSort.Order = SortOrder.Descending
			Else
				lvSort.Order = SortOrder.Ascending
			End If
		Else
			lvSort.SortColumn = iColumn
			lvSort.Order = SortOrder.Ascending
		End If

		Application.DoEvents()
		Call ShowListViewSortImage(lvObject, lvSort.SortColumn, lvSort.Order)

		With lvObject
			If (bUseVisualStyles = False) Then
				' Remove groups, sort, then apply groups - this is a fudge.!
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
	' #############################################################################################
End Class