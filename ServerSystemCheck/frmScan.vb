Option Explicit On
Imports System.ServiceProcess
Imports System.Management
Imports System.Xml
Imports Microsoft.Win32
Imports System.Text
Imports System.ComponentModel
Imports System.Threading

Public Class frmScan
	Public Structure scanItem
		Public currState As Integer							' Same values as "ServerScanResults"
		Public currSubItem As Integer						' Current Item.SubItem
		Public ShutdownTime As DateTime						' DateTime of shutdown event for server
		Public Result_State() As Integer					' "Skipped", "All OK", etc
		Public Result_S As List(Of ServiceController)		' Services
		Public Result_H As List(Of String)					' Hotfixes
		Public Result_E As List(Of List(Of EventLogEntry))	' Eventlog
		Public Result_R As List(Of String)					' Registry
		Public Result_F As List(Of String)					' Files
		Public Result_W As List(Of String)					' WMI Query
		Public Result_D As List(Of ManagementObject)		' Free Space
	End Structure

	Private swStopWatch As Stopwatch
	Private Delegate Sub scanDelegate(ByVal id As String)
	Private Delegate Sub scanClassDelegate(ByVal index As Integer, ByVal status As scanItem)

	Private Shared scanThreadCount As Integer = 0		' Track of current number of threads
	Private Shared iHighestItem As Integer = 0			' Used for "EnsureVisible"

	Public sGroupGUID As String
	Private Shared iScanCount As Integer = 8			' Count of each scan item (Services, Hotfixes, EventLog, etc...) + 1

	Private Shared bCancelScan As Boolean = False
	Public bCurrentlyScanning As Boolean = False
	Private bRescanServer As Boolean = False
	Private img16 As New ImageList
	Private imgSPACING As New ImageList

	Private iResultWidths() As Integer = {60, 60, 60, 60, 60, 60, 70}
	Private sColumnNames() As String = {"Services", "Hotfixes", "Eventlog", "Registry", "File Scan", "WMI", "Free Space"}
	Private sScanNames() As String = {"Windows Services", "Hotfixes And Patches", "Eventlog Entries", "Registry Entries", "File Scans", "WMI Queries", "Free Space Checks"}
	Private bLockColumns As Boolean = False
	Private mRescan As New ContextMenuStrip

	Private Sub frmScan_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
		For Each c As Control In Controls
			c.Font = frmMain.sysFont
		Next
		lblTitle.Font = frmMain.sysFontTitle
		lnkHelp.Font = frmMain.sysFontHelp
		lnkNote.Font = frmMain.sysFontHelp
		lnkNote.Top = lnkHelp.Bottom + 1

		lnkHelp.LinkBehavior = LinkBehavior.HoverUnderline
		lnkNote.LinkBehavior = LinkBehavior.HoverUnderline

		bLockColumns = False
		picIcon.Image = My.Resources._48___Search.ToBitmap

		With img16
			.ImageSize = New Size(16, 16)
			.ColorDepth = ColorDepth.Depth32Bit
			With .Images
				.Clear()
				' This order MUST match modMain.ServerScanResults...
				.Add("_16___Blank", My.Resources._16___Blank)									' 0 - No Icon
				.Add("_16___Scan___Scanning", My.Resources._16___Scan___Scanning)				' 1 - Searching Icon
				.Add("_16___Scan___Pass", My.Resources._16___Scan___Pass)						' 2 - Green Tick
				.Add("_16___Scan___Failed", My.Resources._16___Scan___Failed)					' 3 - Red Cross
				.Add("_16___Scan___Unknown", My.Resources._16___Scan___Unknown)					' 4 - Torn Page
				.Add("_16___Eventlog___Warning", My.Resources._16___Eventlog___Warning)			' 5 - Yellow Triangle
				.Add("_16___Scan___Skipped", My.Resources._16___Scan___Skipped)					' 6 - Blue Tick
				.Add("_16___Scan___Exception", My.Resources._16___Scan___Exception)				' 7 - Faded Red Cross with Yellow Triangle

				.Add("_16___View_Details", My.Resources._16___View_Details)
				.Add("_16___Server", My.Resources._16___Server)									'     Server Icon
				.Add("_16___Server_With_Properties", My.Resources._16___Server_With_Properties)	'     Server Icon

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

		With lstResults
			.Clear()
			.FullRowSelect = True
			.SmallImageList = imgSPACING
			.View = View.Details
			.HeaderStyle = ColumnHeaderStyle.Nonclickable
			With .Columns
				.Clear()
				.Add("x", "Server Name", lstResults.Width - iResultWidths.Sum - SystemInformation.VerticalScrollBarWidth - 4)
				For i As Integer = 0 To (iResultWidths.Count - 1)
					.Add("k" & i.ToString, sColumnNames(i), iResultWidths(i), HorizontalAlignment.Center, -1)
				Next
			End With
		End With

		With lstStats
			.Clear()
			.SmallImageList = imgSPACING
			.View = View.Details
			.HeaderStyle = ColumnHeaderStyle.None
			With .Columns
				.Clear()
				.Add("x", vbNullString, lstResults.Width - iResultWidths.Sum - SystemInformation.VerticalScrollBarWidth - 4)
				For i As Integer = 0 To (iResultWidths.Count - 1)
					.Add("k" & i.ToString, vbNullString, iResultWidths(i), HorizontalAlignment.Center, -1)
				Next
			End With
			With .Items
				Dim lItem As New ListViewItem("Scanning Totals", "_16___Scan___Scanning")
				For i As Integer = 0 To (iResultWidths.Count - 1)
					lItem.SubItems.Add("0")
				Next
				.Add(lItem)
			End With
			.Enabled = False
		End With

		With mRescan.Items
			.Clear()
			.Add("Rescan Server", My.Resources._16___Scan___Scanning.ToBitmap, AddressOf onClick_RescanServer)
			.Add("Show Details...", My.Resources._16___Properties.ToBitmap, AddressOf onClick_ShowDetails)
		End With

		lblStatus.Text = vbNullString
		cmdExport.Visible = False
		cmdLegend.Visible = True

		If (bUseVisualStyles = True) Then
			Call SetWindowTheme(lstResults.Handle, "Explorer", Nothing)
			Call SetWindowTheme(lstStats.Handle, "Explorer", Nothing)
		End If

		Me.Visible = True
		If (bCommandLine = True) Then Me.CenterToScreen()
		Application.DoEvents()

		Me.Cursor = Cursors.WaitCursor
		bLockColumns = True
		swStopWatch = New Stopwatch

		Call buildTreeInListView(lstResults)
		Call scanningSetup(True)

		Dim iCnt As Integer = 0
		Dim lArray As New List(Of ListViewItem)
		For Each lItem As ListViewItem In lstResults.Items
			If (lItem.ImageKey.StartsWith("_16___Server") = True) Then
				iCnt = iCnt + 1
				lArray.Add(lItem)
			End If
		Next

		If (iCnt > 0) Then Call Scan_Threadpool(lArray) Else Call scanningSetup(False)
		Me.Cursor = Cursors.Default
		lArray = Nothing
	End Sub

	Public Sub buildTreeInListView(ByRef cListViewControl As ctrlListView_SubIcons)
		Dim lItem As ListViewItem
		Dim iIndent As Integer
		Dim lFound() As ListViewItem

		' Create the group structure...
		Dim xGroupList As List(Of XmlNode) = xml_getGroupList_FromGroup(sGroupGUID, eDirectionalSearch.ToParents)
		xGroupList.AddRange(xml_getGroupList_FromGroup(sGroupGUID, eDirectionalSearch.ToChildren))
		If (xGroupList Is Nothing) Then
			MessageBox.Show(Me, "Invalid Group Specified", APN, MessageBoxButtons.OK, MessageBoxIcon.Error)
			Me.Close()
			Exit Sub
		End If

		For Each gItem As XmlNode In xGroupList
			If (gItem.ParentNode.Name = "root") Then
				iIndent = 0
			Else
				lFound = cListViewControl.Items.Find(gItem.ParentNode.Attributes.ItemOf("guid").Value, False)
				If ((lFound Is Nothing) Or (lFound.Count = 0)) Then iIndent = 0 Else iIndent = lFound(0).IndentCount + 1
			End If

			lFound = Nothing
			lFound = cListViewControl.Items.Find(gItem.Attributes.ItemOf("guid").Value, False)
			If (lFound.Count = 0) Then
				lItem = New ListViewItem(gItem.Attributes.ItemOf("name").Value)
				lItem.ImageKey = "_16___Group___" & getGroupColour(CInt(gItem.Attributes.ItemOf("colr").Value), False)
				lItem.Name = gItem.Attributes.ItemOf("guid").Value
				lItem.IndentCount = iIndent
				cListViewControl.Items.Add(lItem)
			End If
		Next

		' Next add the servers to the groups...
		For Each lvItem As ListViewItem In cListViewControl.Items
			Dim xServerList As List(Of XmlNode) = xml_getServerList_FromGroup(lvItem.Name)
			If (xServerList IsNot Nothing) Then
				xServerList.Reverse()
				For Each sItem As XmlNode In xServerList
					If (sItem.ParentNode.Attributes.ItemOf("guid").Value = lvItem.Name) Then
						lItem = New ListViewItem(sItem.Attributes.ItemOf("name").Value.ToLower)
						If m_UppercaseServerNames = True Then lItem.Text = lItem.Text.ToUpper()
						lItem.ImageKey = "_16___Server"
						If (xml_LoadServerData(sItem.Attributes.ItemOf("guid").Value) IsNot Nothing) Then lItem.ImageKey = "_16___Server_With_Properties"
						lItem.Name = sItem.Attributes.ItemOf("guid").Value & "|Uptime: Unknown"
						lItem.IndentCount = lvItem.IndentCount + 1
						lItem.Tag = sItem
						For i As Integer = 1 To iScanCount - 1
							lItem.SubItems.Add("ICON:" & ServerScanResults.None)
						Next i
						cListViewControl.Items.Insert(lvItem.Index + 1, lItem)
					End If
				Next
			End If
		Next
	End Sub

	Private Sub cmdClose_Click(sender As System.Object, e As System.EventArgs) Handles cmdClose.Click
		If (bCurrentlyScanning = True) Then
			cmdClose.Enabled = False
			bCancelScan = True
			proProgress.Visible = False
			lblStatus.Location = New Point(proProgress.Left, CInt(cmdLegend.Top + (cmdLegend.Height - lblStatus.Height) / 2) - 1)
		Else
			img16.Dispose()
			imgSPACING.Dispose()
			frmScanLegend.Dispose()
			Me.Close()
			Me.Dispose()
		End If
	End Sub

	Private Sub lstResults_ColumnWidthChanged(sender As Object, e As System.Windows.Forms.ColumnWidthChangedEventArgs) Handles lstResults.ColumnWidthChanged
		If (bLockColumns = False) Then Exit Sub
		lstResults.Columns(e.ColumnIndex).Width = iResultWidths(e.ColumnIndex)
	End Sub

	Private Sub lstResults_ColumnWidthChanging(sender As Object, e As System.Windows.Forms.ColumnWidthChangingEventArgs) Handles lstResults.ColumnWidthChanging
		If (bLockColumns = False) Then Exit Sub
		e.Cancel = True
		e.NewWidth = lstResults.Columns(e.ColumnIndex).Width
	End Sub

	Private Sub lstResults_GotFocus(sender As Object, e As System.EventArgs) Handles lstResults.GotFocus
		If (bCurrentlyScanning = True) Then picIcon.Focus()
	End Sub

	Private Sub lstResults_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles lstResults.SelectedIndexChanged
		If ((bCurrentlyScanning = False) AndAlso (lstResults.SelectedItems.Count > 0)) Then
			Dim sText As String = lstResults.SelectedItems(0).Name
			If (InStr(sText, "|") > 0) Then sText = Split(sText, "|")(1) Else sText = vbNullString
			lblStatus.Text = sText
		Else
			lblStatus.Text = vbNullString
		End If
	End Sub

	Private Sub lstResults_MouseClick(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles lstResults.MouseClick
		If (e.Button <> Windows.Forms.MouseButtons.Right) Then Exit Sub
		Dim lItem As ListViewItem = lstResults.GetItemAt(e.X, e.Y)
		If (lItem Is Nothing) Then Exit Sub
		If (lItem.ImageKey.StartsWith("_16___Server") = False) Then Exit Sub
		mRescan.Show(lstResults, New Point(e.X, e.Y))
	End Sub

	Private Sub lstResults_MouseDoubleClick(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles lstResults.MouseDoubleClick
		If (bCurrentlyScanning = True) Then Exit Sub
		Dim lItem As ListViewItem = Nothing
		If ((sender Is Nothing) AndAlso (e Is Nothing)) Then lItem = lstResults.SelectedItems(0) Else lItem = lstResults.GetItemAt(e.X, e.Y)
		If (lItem Is Nothing) Then Exit Sub
		If (lItem.ImageKey.StartsWith("_16___Server") = False) Then Exit Sub

		Dim sMSG As String
		Select Case lItem.SubItems(1).Text
			Case "ICON:" & ServerScanResults.None : sMSG = "Failed to connect to this server." & vbCrLf & "Please rescan this server, or choose another result."
			Case "ICON:" & ServerScanResults.Unknown : sMSG = "The scan was cancelled on this server, or some other error occurred." & vbCrLf & "Please rescan this server, or choose another result."
			Case Else : sMSG = vbNullString
		End Select
		If (sMSG <> vbNullString) Then
			MessageBox.Show(Me, sMSG, APN, MessageBoxButtons.OK, MessageBoxIcon.Error)
			Exit Sub
		End If

		With frmScanResults
			.sServerName = lItem.Text.Trim
			.sServerGUID = lItem.Name
			.sServiceEntries = CType(lItem.SubItems(1).Tag, List(Of System.ServiceProcess.ServiceController))
			.hHotfixEntries = CType(lItem.SubItems(2).Tag, List(Of String))
			.eEventLogEntries = CType(lItem.SubItems(3).Tag, List(Of List(Of System.Diagnostics.EventLogEntry)))
			.rRegistryEntries = CType(lItem.SubItems(4).Tag, List(Of String))
			.fFileChecks = CType(lItem.SubItems(5).Tag, List(Of String))
			.wWMIQueries = CType(lItem.SubItems(6).Tag, List(Of String))
			.dFreeSpaceSize = CType(lItem.SubItems(7).Tag, List(Of System.Management.ManagementObject))
			.ShowDialog(Me)
		End With
	End Sub

	Private Sub lstResults_MouseMove(sender As Object, e As MouseEventArgs) Handles lstResults.MouseMove
		Static prevMousePos As Point = New Point(-1, -1)
		If (prevMousePos = MousePosition) Then Exit Sub

		Static t As New ToolTip()
		Dim lv As ListView = TryCast(sender, ListView)
		If (lv Is Nothing) Then
			t.Hide(lv)
			Exit Sub
		End If

		With lv.HitTest(lv.PointToClient(MousePosition))
			If ((.SubItem IsNot Nothing) AndAlso (Not String.IsNullOrEmpty(.SubItem.Text))) Then
				If (.SubItem.Bounds.Left < 10) Then Exit Sub ' Don't show for first column
				t.IsBalloon = False
				t.UseFading = True
				t.Show(.SubItem.Name, lv, e.X + 16, e.Y + 16, 5000)
			Else
				t.Hide(lv)
			End If
			prevMousePos = MousePosition
		End With
	End Sub

	Private Sub scanningSetup(ByVal bIsStarting As Boolean)
		proProgress.Value = 0
		bCurrentlyScanning = bIsStarting
		proProgress.Visible = bIsStarting
		picIcon.Focus()

		If (bIsStarting = True) Then
			bCancelScan = False
			lblStatus.Text = vbNullString
			lblStatus.Location = New Point(proProgress.Left, proProgress.Top - lblStatus.Height - 3)
			lblSubTitle.Text = "Please wait for the process to complete..."
			cmdClose.Text = "Cancel"
			cmdExport.Visible = False
		Else
			swStopWatch.Stop()
			lblStatus.Text = vbNullString
			lblStatus.Location = New Point(proProgress.Left, CInt(cmdLegend.Top + (cmdLegend.Height - lblStatus.Height) / 2) - 1)
			lblSubTitle.Text = "Double-click a server name to view the results; hover over icons to show quick information."
			cmdClose.Text = "Close"
			cmdClose.Enabled = True

			Me.Cursor = Cursors.Default
			If ((bCancelScan = False) AndAlso (bCommandLine = False)) Then
				Dim sMsg As String = "Server scan complete." & vbCrLf & "Check the results for any errors or issues."
				If (bRescanServer = False) Then
					Dim sTime As TimeSpan = New TimeSpan(0, 0, CInt(swStopWatch.Elapsed.TotalSeconds))
					sMsg = sMsg & vbCrLf & vbCrLf & "Scan time: "
					If (sTime.Minutes > 0) Then sMsg = sMsg & sTime.Minutes & " minutes, "
					sMsg = sMsg & sTime.Seconds & " seconds (approx)."

					' Stats...
					sMsg = sMsg & vbCrLf & vbCrLf & "Overall Stats: "
					For i As Integer = 1 To iScanCount - 1
						sMsg = sMsg & vbCrLf & "    " & lstStats.Items(0).SubItems(i).Text & vbTab & sScanNames(i - 1)
					Next
				End If

				MessageBox.Show(Me, sMsg, APN, MessageBoxButtons.OK, MessageBoxIcon.Information)
				cmdExport.Visible = True
			End If
			bRescanServer = False
			lblStatus.Text = "Server scan complete."

			' Run reports...
			If (bCommandLine = True) Then
				With frmScanReport
					.Visible = True
					.Location = New Point(-1000, -1000)
					If (bCommand_BasicReport = True) Then
						Call .radSimple_CheckedChanged(Nothing, Nothing)
					Else
						Call .radDetailed_CheckedChanged(Nothing, Nothing)
						.chkDriveSytem.Checked = True
						.chkEventlog.Checked = True
						.chkFiles.Checked = True
						.chkHotfixes.Checked = True
						.chkRegKeys.Checked = True
						.chkServices.Checked = True
						.chkWMI.Checked = True
					End If
					Call .cmdExport_Click(Nothing, Nothing)
				End With
				Me.Close()
			End If
		End If

		Application.DoEvents()
		If ((bCancelScan = True) AndAlso (bCommandLine = False)) Then
			Dim sMsg As String = "Server scan was cancelled." & vbCrLf & "Please correct any mistake you think you made, and try again."
			MessageBox.Show(Me, sMsg, APN, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			cmdExport.Visible = False
		End If
	End Sub

	Private Shared Sub doSubIcon(ByVal lItem As ListViewItem, ByVal iSubItemIndex As Integer, ByVal iIcon As ServerScanResults, ByVal sDescription As String)
		Dim sText As String
		If (iIcon <> ServerScanResults.StringText) Then sText = "ICON:" & iIcon Else sText = sDescription
		lItem.SubItems(iSubItemIndex).Text = sText
		lItem.SubItems(iSubItemIndex).Name = sDescription
		Application.DoEvents()
	End Sub

	Private Shared Sub updateStats(ByVal lItem As ListViewItem, ByVal iSubItemIndex As Integer, ByVal iIncreaseCount As Integer)
		lItem.SubItems(iSubItemIndex).Text = (CInt(lItem.SubItems(iSubItemIndex).Text) + iIncreaseCount).ToString
	End Sub

	' #############################################################################################

	Private Shared Function getDriveToolTip(ByVal dResult As List(Of ManagementObject)) As String
		Dim sToolTip As New StringBuilder
		For Each mObj As ManagementObject In dResult
			Try
				Console.WriteLine("IGNORE ME: VolumeName: " & mObj("VolumeName").ToString)
				Dim sVolumeName As String = mObj("VolumeName").ToString
				If (sVolumeName = vbNullString) Then sVolumeName = "Local Disk"
				Dim iPercent As Integer = CInt((100 / CLng(mObj("Size"))) * CLng(mObj("FreeSpace")))

				sToolTip.Append(sVolumeName & " (" & mObj("DeviceID").ToString & ")" & vbTab & iPercent.ToString & "% free" & vbCrLf)
			Catch ex As Exception
				Application.DoEvents()
			End Try
		Next
		sToolTip = sToolTip.Remove(sToolTip.Length - vbCrLf.Length, vbCrLf.Length)
		Return sToolTip.ToString
	End Function

	' #############################################################################################
	Private Shared Function scanServer_Services(ByVal lItem As ListViewItem, ByRef returnState As Integer) As List(Of ServiceController)
		Dim sOKSoFar As New List(Of ServiceController)
		Dim xItem As XmlElement = CType(lItem.Tag, XmlElement)
		Dim scService() As ServiceController = Nothing

		If (bCancelScan = True) Then
			returnState = ServerScanResults.Skipped
			Return Nothing
		End If

		' Get list of resources we are looking for: current group and any parents
		Dim lResources As List(Of XmlElement) = Nothing
		Dim lResourcesFiltered As List(Of XmlElement) = Nothing
		Try
			lResources = xml_getResourceList(xItem.ParentNode.Attributes("guid").Value, eDirectionalSearch.ToParents, True)
			lResourcesFiltered = lResources.FindAll(Function(f) f.Attributes("type").Value = "Windows Service")

			' Nothing to scan for...
			If (lResourcesFiltered.Count = 0) Then
				returnState = ServerScanResults.Skipped
				Return Nothing
			End If
		Catch
			returnState = ServerScanResults.Exception
			Return Nothing
		End Try

		Try
			scService = ServiceController.GetServices(Trim(lItem.Text.ToUpper))
		Catch
			returnState = ServerScanResults.Exception
			Return Nothing
		End Try

		Dim sTempList As New List(Of XmlElement)
		sTempList.AddRange(lResourcesFiltered)

		Try
			returnState = ServerScanResults.Pass
			For Each rNode As XmlElement In lResourcesFiltered
				Dim sChecking As String = rNode.Attributes.ItemOf("checking").Value
				Dim sResult As String = vbNullString
				If (sChecking.Contains(" (") = True) Then sResult = sChecking.Split("("c)(1).Trim(")"c).Trim() Else sResult = "OK"

				For Each sServ As ServiceController In scService
					If (sServ.DisplayName = rNode.Attributes.ItemOf("name").Value) Then
						If (sServ.Status.ToString <> sChecking.Split("("c)(0).Trim) Then sOKSoFar.Add(sServ)
						sTempList.Remove(rNode)
					End If
				Next
			Next
			If (sOKSoFar.Count > 0) Then returnState = ServerScanResults.Fail
		Catch
			returnState = ServerScanResults.Exception
			Return Nothing
		End Try


		' Run though "missing" services
		Dim iCurrentState As Integer = 0
		Try
			If (sTempList.Count > 0) Then
				For Each rNode As XmlElement In sTempList
					Dim sResult As String = "OK"
					Dim sChecking As String = rNode.Attributes.ItemOf("checking").Value
					If InStr(sChecking, "(") <> 0 Then sResult = sChecking.Split("("c)(1).Trim(")"c).Trim
					If (sResult <> "OK") Then
						If ((sResult = "Warning") AndAlso (iCurrentState <= 1)) Then iCurrentState = 1
						If ((sResult = "Fail") AndAlso (iCurrentState <= 2)) Then iCurrentState = 2

						Using scMissing As New ServiceController
							scMissing.DisplayName = "Missing|" & rNode.Attributes.ItemOf("name").Value
							sOKSoFar.Add(scMissing)
						End Using
					End If
				Next
			End If
		Catch
		End Try

		' Check if there are ONLY "Missing" services, and warn...
		If ((returnState = ServerScanResults.Pass) And (sOKSoFar.Count > 0)) Then
			Select Case iCurrentState
				Case 0 : returnState = ServerScanResults.Pass
				Case 1 : returnState = ServerScanResults.Warning
				Case 2 : returnState = ServerScanResults.Fail
			End Select
		End If

		Return sOKSoFar
	End Function

	Private Shared Function scanServer_Hotfixes(ByVal lItem As ListViewItem, ByRef returnState As Integer) As List(Of String)
		Dim sOKSoFar As New List(Of String)
		Dim xItem As XmlElement = CType(lItem.Tag, XmlElement)
		Dim bFound As Boolean = False
		Dim hfList As List(Of String) = Nothing
		Dim sHotFixList As New List(Of String)

		If (bCancelScan = True) Then
			returnState = ServerScanResults.Skipped
			Return Nothing
		End If

		' Get list of resources we are looking for: current group and any parents
		Dim lResources As List(Of XmlElement) = Nothing
		Dim lResourcesFiltered As List(Of XmlElement) = Nothing
		Try
			lResources = xml_getResourceList(xItem.ParentNode.Attributes("guid").Value, eDirectionalSearch.ToParents, True)
			lResourcesFiltered = lResources.FindAll(Function(f) f.Attributes("type").Value = "Hotfix Patch")

			' Nothing to scan for...
			If (lResourcesFiltered.Count = 0) Then
				returnState = ServerScanResults.Skipped
				Return Nothing
			End If
		Catch
			returnState = ServerScanResults.Exception
			Return Nothing
		End Try


		Dim oResult As ManagementObjectCollection
		Try
			oResult = wmiConnect("\\" & Trim(lItem.Text.ToUpper) & "\root\cimv2", "SELECT HotFixID, Description, InstalledOn FROM Win32_QuickFixEngineering", 30)
			If (oResult Is Nothing) Then
				returnState = ServerScanResults.Fail
				Return Nothing
			End If
		Catch
			returnState = ServerScanResults.Exception
			Return Nothing
		End Try

		' Get extra "Admin-Only" Hotfixes...
		Try
			Dim sAdminList As New List(Of String)
			If (bIsAdminMode = True) Then sAdminList = getWindowsUpdateSessionDetails(Trim(lItem.Text.ToUpper), True)

			Dim iCount As Integer = 0
			Dim sComment As String = vbNullString
			Dim sResourceListI As New List(Of String)	' Should be installed
			Dim sResourceListN As New List(Of String)	' Should not be installed

			For Each sHM As ManagementObject In oResult
				sHotFixList.Add(sHM("HotFixID").ToString.ToUpper)
			Next
			If (sAdminList IsNot Nothing) Then sHotFixList.AddRange(sAdminList)

			For Each sRM As XmlElement In lResourcesFiltered
				If sRM.Attributes.ItemOf("checking").Value.ToUpper = "INSTALLED" Then
					sResourceListI.Add(sRM.Attributes.ItemOf("name").Value.ToUpper)
				Else
					sResourceListN.Add(sRM.Attributes.ItemOf("name").Value.ToUpper)
				End If
			Next

			' Check list of "SHOULD BE INSTALLED"
			Dim sExceptResult As IEnumerable(Of String) = sResourceListI.Except(sHotFixList)
			If (sExceptResult.Count > 0) Then
				For Each sItem As String In sExceptResult
					sOKSoFar.Add(sItem & "|Not Installed|Installed")
				Next
			End If

			' Check list of "SHOULD NOT BE INSTALLED"
			Dim sIntersectResult As IEnumerable(Of String) = sResourceListN.Intersect(sHotFixList)
			If (sIntersectResult.Count > 0) Then
				For Each sItem As String In sIntersectResult
					sOKSoFar.Add(sItem & "|Installed|Not Installed")
				Next
			End If
		Catch
			returnState = ServerScanResults.Exception
			Return Nothing
		End Try

		If (sOKSoFar.Count > 0) Then returnState = ServerScanResults.Fail
		oResult.Dispose()
		Return sOKSoFar
	End Function

	Private Shared Function scanServer_EventLog(ByVal lItem As ListViewItem, ByVal dtShutdowntime As DateTime, ByRef returnState As Integer) As List(Of List(Of EventLogEntry))
		Dim iCount As Integer = 0
		Dim iErrorCount As Integer = 0
		Dim sOKSoFar As New List(Of List(Of EventLogEntry))		' Lists: Application, System and Security (so far)
		Dim sOKApp As New List(Of EventLogEntry)
		Dim sOKSys As New List(Of EventLogEntry)
		Dim sOKSec As New List(Of EventLogEntry)
		Dim xItem As XmlElement = CType(lItem.Tag, XmlElement)

		If (bCancelScan = True) Then
			returnState = ServerScanResults.Skipped
			Return Nothing
		End If

		' Get list of resources we are looking for: current group and any parents
		Dim lResources As List(Of XmlElement) = Nothing
		Dim lResourcesFiltered As List(Of XmlElement) = Nothing
		Try
			lResources = xml_getResourceList(xItem.ParentNode.Attributes("guid").Value, eDirectionalSearch.ToParents, True)
			lResourcesFiltered = lResources.FindAll(Function(f) f.Attributes("type").Value = "Eventlog Scan")

			' Nothing to scan for...
			If (lResourcesFiltered.Count = 0) Then
				returnState = ServerScanResults.Skipped
				Return Nothing
			End If
		Catch
			returnState = ServerScanResults.Exception
			Return Nothing
		End Try

		Dim evtLog As EventLog = Nothing
		Dim evtLogDisplayName As String
		If (dtShutdowntime.Year < 2010) Then dtShutdowntime = (Now - New TimeSpan(30, 0, 0, 0))

		Try
			For Each rItem As XmlElement In lResourcesFiltered
				Dim bEntryType As String = rItem.Attributes("checking").Value
				Dim sEventIDs As String = vbNullString

				If (bEntryType.Contains("Excluding:") = True) Then
					sEventIDs = bEntryType.Substring(InStr(bEntryType, "Excluding: ", CompareMethod.Text) + 10)
				Else
					sEventIDs = bEntryType.Substring(InStr(bEntryType, "Specifically: ", CompareMethod.Text) + 13)
				End If
				evtLogDisplayName = rItem.Attributes("name").Value.Trim

				Try
					Application.DoEvents()
					evtLog = New EventLog(rItem.Attributes("name").Value, Trim(lItem.Text))
					Console.WriteLine("IGNORE ME: " & evtLog.LogDisplayName & " : " & evtLog.Entries.Count)		' This is a test to see if "evtLog" is nothing.
					Try
						Dim evtLogArr(evtLog.Entries.Count - 1) As EventLogEntry
						evtLog.Entries.CopyTo(evtLogArr, 0)

						Dim iEnQuery As IEnumerable(Of EventLogEntry) = evtLogArr.ToList
						Dim iQuery As IQueryable(Of EventLogEntry) = iEnQuery.AsQueryable()
						Dim eQueryResult As IQueryable(Of EventLogEntry) = Nothing

						If (bEntryType.Contains("Excluding:") = True) Then
							' Search everything, excluding the specified IDs...
							eQueryResult = From Q As EventLogEntry In iQuery Where
							 (Q.TimeGenerated > dtShutdowntime) And
							 (
							  (If(InStr(bEntryType, "Critical, ") > 0, Q.EntryType = 0, Nothing)) Or
							  (If(InStr(bEntryType, "Error, ") > 0, Q.EntryType = EventLogEntryType.Error, Nothing)) Or
							  (If(InStr(bEntryType, "Warning, ") > 0, Q.EntryType = EventLogEntryType.Warning, Nothing)) Or
							  (If(InStr(bEntryType, "Information, ") > 0, Q.EntryType = EventLogEntryType.Information, Nothing)) Or
							  (If(InStr(bEntryType, "Success, ") > 0, Q.EntryType = EventLogEntryType.SuccessAudit, Nothing)) Or
							  (If(InStr(bEntryType, "Failure, ") > 0, Q.EntryType = EventLogEntryType.FailureAudit, Nothing))
							 ) And (
							  (If(checkEventID(CInt(Q.InstanceId And 65535), sEventIDs), Nothing, Q.Source = Q.Source))
							 )
							 Order By Q.TimeGenerated Descending
							 Select Q
						Else
							' Search only for the specific IDs...
							eQueryResult = From Q As EventLogEntry In iQuery Where
							 (Q.TimeGenerated > dtShutdowntime) And
							 (If(checkEventID(CInt(Q.InstanceId And 65535), sEventIDs), Q.Source = Q.Source, Nothing))
							 Order By Q.TimeGenerated Descending
							 Select Q
						End If

						Select Case evtLogDisplayName
							Case "Application" : sOKApp.AddRange(eQueryResult.ToList)
							Case "System" : sOKSys.AddRange(eQueryResult.ToList)
							Case "Security" : sOKSec.AddRange(eQueryResult.ToList)
						End Select

					Catch ex As Exception
						Application.DoEvents()
					End Try
				Catch ex As Exception
					returnState = ServerScanResults.Exception
					Return Nothing
				Finally
					evtLog.Dispose()
				End Try
			Next
		Catch
			returnState = ServerScanResults.Exception
			Return Nothing
		End Try

		sOKSoFar.Clear()
		sOKSoFar.Add(sOKApp)	' Add the APPLICATION eventlog entries
		sOKSoFar.Add(sOKSys)	' Add the SYSTEM eventlog entries
		sOKSoFar.Add(sOKSec)	' Add the SECURITY eventlog entries

		If (sOKSoFar.Count > 0) Then returnState = ServerScanResults.Fail
		Return sOKSoFar
	End Function

	Private Shared Function scanServer_Registry(ByVal lItem As ListViewItem, ByRef returnState As Integer) As List(Of String)
		Dim sOKSoFar As New List(Of String)
		Dim xItem As XmlElement = CType(lItem.Tag, XmlElement)
		Dim regList As List(Of String) = Nothing

		If (bCancelScan = True) Then
			returnState = ServerScanResults.Skipped
			Return Nothing
		End If

		' Get list of resources we are looking for: current group and any parents
		Dim lResources As List(Of XmlElement) = Nothing
		Dim lResourcesFiltered As List(Of XmlElement) = Nothing
		Try
			lResources = xml_getResourceList(xItem.ParentNode.Attributes("guid").Value, eDirectionalSearch.ToParents, True)
			lResourcesFiltered = lResources.FindAll(Function(f) f.Attributes("type").Value = "Registry Scan")

			' Nothing to scan for...
			If (lResourcesFiltered.Count = 0) Then
				returnState = ServerScanResults.Skipped
				Return Nothing
			End If
		Catch
			returnState = ServerScanResults.Exception
			Return Nothing
		End Try

		Dim sAlertMissing As String = vbNullString
		Try
			For Each rNode As XmlElement In lResourcesFiltered
				Dim sMeta(1) As String
				Dim iPos As Integer = rNode.Attributes("name").Value.LastIndexOf("\")
				sMeta(0) = rNode.Attributes("name").Value.Substring(0, iPos)
				sMeta(1) = rNode.Attributes("name").Value.Substring(iPos + 1)
				If sMeta(1) = "(Default)" Then sMeta(1) = vbNullString

				Dim sData() As String = Split(rNode.Attributes("checking").Value, ", ")
				Try
					Dim rKey As RegistryKey
					Dim rOSK As RegistryKey
					Dim oResult As Object
					Dim sResult As String = vbNullString

					Select Case sData(2)
						Case "Warning" : sAlertMissing = "W"
						Case "Fail" : sAlertMissing = "F"
						Case Else : sAlertMissing = vbNullString
					End Select

					rKey = RegistryKey.OpenRemoteBaseKey(RegistryHive.LocalMachine, Trim(lItem.Text.ToUpper))
					If (rKey IsNot Nothing) Then
						Try
							rOSK = rKey.OpenSubKey(sMeta(0), False)
							oResult = rOSK.GetValue(sMeta(1), vbNullString, RegistryValueOptions.DoNotExpandEnvironmentNames)
							' The above line WILL produce a hidden error is the value does not exist.
							If (oResult IsNot Nothing) Then sResult = readRegistryValue(oResult)
							If (sResult <> sData(0).Trim.Replace("¬", "|")) Then
								' Incorrect Value Data
								sOKSoFar.Add("F|I|" & rNode.Attributes("name").Value & "|" & rNode.Attributes("checking").Value)
							End If
						Catch ex As Exception
							' Value Name does not exist
							If (sAlertMissing <> vbNullString) Then
								sOKSoFar.Add(sAlertMissing & "|M|" & rNode.Attributes("name").Value & "|" & rNode.Attributes("checking").Value)
							End If
						End Try
					End If

				Catch ex As Exception
					' Registry Key does not exist
					If (sAlertMissing <> vbNullString) Then
						sOKSoFar.Add(sAlertMissing & "|M|" & rNode.Attributes("name").Value & "|" & rNode.Attributes("checking").Value)
					End If
				End Try
			Next
		Catch
			returnState = ServerScanResults.Exception
			Return Nothing
		End Try

		If (sOKSoFar.Count > 0) Then returnState = ServerScanResults.Fail
		Return sOKSoFar
	End Function

	Private Shared Function scanServer_FileChecks(ByVal lItem As ListViewItem, ByRef returnState As Integer) As List(Of String)
		Dim sOKSoFar As New List(Of String)
		Dim xItem As XmlElement = CType(lItem.Tag, XmlElement)

		If (bCancelScan = True) Then
			returnState = ServerScanResults.Skipped
			Return Nothing
		End If

		' Get list of resources we are looking for: current group and any parents
		Dim lResources As List(Of XmlElement) = Nothing
		Dim lResourcesFiltered As List(Of XmlElement) = Nothing
		Try
			lResources = xml_getResourceList(xItem.ParentNode.Attributes("guid").Value, eDirectionalSearch.ToParents, True)
			lResourcesFiltered = lResources.FindAll(Function(f) f.Attributes("type").Value = "File Check")

			' Nothing to scan for...
			If (lResourcesFiltered.Count = 0) Then
				returnState = ServerScanResults.Skipped
				Return Nothing
			End If
		Catch
			returnState = ServerScanResults.Exception
			Return Nothing
		End Try

		Try
			For Each rNode As XmlElement In lResourcesFiltered
				Dim sFileName As String = rNode.Attributes.ItemOf("name").Value
				Dim sCheckFile As String = "\\" & Trim(lItem.Text.ToUpper) & "\" & sFileName.Replace(":", "$")
				Dim sData() As String = Split(rNode.Attributes.ItemOf("checking").Value, "|")

				Try
					If (System.IO.File.Exists(sCheckFile) = True) Then
						If (sData(0).ToUpper.StartsWith("EXISTS")) Then
							If (sData(1).StartsWith("Date:")) Then
								sData(1) = sData(1).Substring(6)
								Dim sDT As Date = System.IO.File.GetLastWriteTimeUtc(sCheckFile).Date
								Dim cDT As Date = DateTime.Parse(Split(sData(1), ":")(1))

								Select Case (Split(sData(1), ":")(0))
									Case "Before" : If (sDT > cDT) Then sOKSoFar.Add(sFileName & "|wdb|" & sDT & "|" & cDT & "") ' Wrong Date (Should Be On Or Before)
									Case "After" : If (sDT < cDT) Then sOKSoFar.Add(sFileName & "|wda|" & sDT & "|" & cDT & "") ' Wrong Date (Should Be On Or After)
								End Select

							ElseIf (sData(1).StartsWith("Version:")) Then
								sData(1) = sData(1).Substring(9)
								Dim sSVer As String = Split(FileVersionInfo.GetVersionInfo(sCheckFile).FileVersion, " ")(0)
								Dim cSVer As String = Split(sData(1), ":")(1)
								Dim sLVer As Long = CLng((sSVer).Replace(".", ""))
								Dim cLVer As Long = CLng((cSVer).Replace(".", ""))

								If (IsNumeric(sLVer) = False) Then
									sOKSoFar.Add(sFileName & "|ver|" & sSVer & "|" & cSVer & "") ' Version Mismatch
								Else
									Select Case (Split(sData(1), ":")(0))
										Case "Exactly" : If (sLVer <> cLVer) Then sOKSoFar.Add(sFileName & "|ver|" & sSVer & "|" & cSVer & "") '    Version Should Be Equal To
										Case "LessThan" : If (sLVer > cLVer) Then sOKSoFar.Add(sFileName & "|vrl|" & sSVer & "|" & cSVer & "") '    Version Should Be Equal Or Less Than
										Case "GreaterThan" : If (sLVer < cLVer) Then sOKSoFar.Add(sFileName & "|vrg|" & sSVer & "|" & cSVer & "") ' Version Should Be Equal Or Greater Than
									End Select
								End If
							End If
						Else
							sOKSoFar.Add(sFileName & "|fif|Exists|Not Exists")	' File Should Not Exist
						End If
					Else
						If (sData(0).ToUpper = "EXISTS") Then
							Dim sChecking As String
							Select Case True
								Case sData(1).StartsWith("Date:") : sChecking = "Date"
								Case sData(1).StartsWith("Version:") : sChecking = "Version"
								Case Else : sChecking = "Exists"
							End Select
							sOKSoFar.Add(sFileName & "|fnf|Not Found|" & sChecking)	' File Not Found
						End If
					End If
				Catch ex As Exception
					sOKSoFar.Add(sFileName & "|" & ex.Message)
				End Try
			Next
		Catch
			returnState = ServerScanResults.Exception
			Return Nothing
		End Try

		If (sOKSoFar.Count > 0) Then returnState = ServerScanResults.Fail
		Return sOKSoFar
	End Function

	Private Shared Function scanServer_WMIQuery(ByVal lItem As ListViewItem, ByRef returnState As Integer) As List(Of String)
		Dim sOKSoFar As New List(Of String)
		Dim xItem As XmlElement = CType(lItem.Tag, XmlElement)
		Dim regList As List(Of String) = Nothing

		If (bCancelScan = True) Then
			returnState = ServerScanResults.Skipped
			Return Nothing
		End If

		' Get list of resources we are looking for: current group and any parents
		Dim lResources As List(Of XmlElement) = Nothing
		Dim lResourcesFiltered As List(Of XmlElement) = Nothing
		Try
			lResources = xml_getResourceList(xItem.ParentNode.Attributes("guid").Value, eDirectionalSearch.ToParents, True)
			lResourcesFiltered = lResources.FindAll(Function(f) f.Attributes("type").Value = "WMI Query")

			' Nothing to scan for...
			If (lResourcesFiltered.Count = 0) Then
				returnState = ServerScanResults.Skipped
				Return Nothing
			End If
		Catch
			returnState = ServerScanResults.Exception
			Return Nothing
		End Try

		Try
			For Each sRM As XmlElement In lResourcesFiltered
				Dim sChecking() As String = Split(sRM.Attributes.ItemOf("checking").Value, "|")
				Dim sQuery As String = sChecking(0)
				Dim sOperator As String = sChecking(1)
				Dim sCheck As String = sChecking(2)
				'Dim bCountOnly As Boolean = CBool(sChecking(3))

				Dim oResult As ManagementObjectCollection = wmiConnect("\\" & Trim(lItem.Text.ToUpper) & "\root\cimv2", sQuery, 30)
				If (oResult Is Nothing) Then
					sOKSoFar.Add(sQuery & "|Unknown|" & sCheck)
				Else
					Dim bAdd As Boolean = True
					If (sChecking(3).ToUpper = "TRUE") Then
						' Checking COUNT of returned results
						Select Case sOperator.Trim
							Case "Equal To" : If (oResult.Count = CInt(sCheck)) Then bAdd = False
							Case "Not Equal To" : If (oResult.Count <> CInt(sCheck)) Then bAdd = False
							Case "Greater Than" : If (oResult.Count > CInt(sCheck)) Then bAdd = False
							Case "Less Than" : If (oResult.Count < CInt(sCheck)) Then bAdd = False
							Case "Less Than Or Equal To" : If (oResult.Count <= CInt(sCheck)) Then bAdd = False
							Case "Greater Than Or Equal To" : If (oResult.Count >= CInt(sCheck)) Then bAdd = False
							Case Else : bAdd = True
						End Select
					Else
						' Checking STRING of returned results
						Dim cResult As New StringBuilder("")
						Try
							For Each mObj As ManagementObject In oResult
								For Each pProp As PropertyData In mObj.Properties
									Dim oText As Object = mObj(pProp.Name)
									If (oText IsNot Nothing) Then
										cResult.AppendLine(mObj(pProp.Name).ToString)
									Else
										cResult.AppendLine("(no result)")
									End If
								Next
							Next
						Catch ex As Exception
							bAdd = True
						End Try
						Select Case sOperator.Trim
							Case "Equal To" : If (cResult.ToString = sCheck) Then bAdd = False
							Case "Not Equal To" : If (cResult.ToString <> sCheck) Then bAdd = False
							Case Else : bAdd = True
						End Select
					End If

					If (bAdd = True) Then sOKSoFar.Add(sQuery & "|" & oResult.Count & "|" & sOperator.Trim & ": " & sCheck)
				End If
			Next
		Catch
			returnState = ServerScanResults.Exception
			Return Nothing
		End Try

		If (sOKSoFar.Count > 0) Then returnState = ServerScanResults.Fail
		Return sOKSoFar
	End Function

	Private Shared Function scanServer_DriveSpace(ByVal lItem As ListViewItem, ByRef returnState As Integer) As List(Of ManagementObject)
		Dim sOKSoFar As New List(Of ManagementObject)
		Dim xItem As XmlElement = CType(lItem.Tag, XmlElement)
		Dim bFound As Boolean = False
		Dim hfList As List(Of String) = Nothing
		Dim sThresholds As String = "10|20|Exclude"	' Default values

		If (bCancelScan = True) Then
			returnState = ServerScanResults.Skipped
			Return Nothing
		End If

		' Get list of resources we are looking for: current group and any parents
		Dim lResources As List(Of XmlElement) = Nothing
		Dim lResourcesFiltered As List(Of XmlElement) = Nothing
		Try
			lResources = xml_getResourceList(xItem.ParentNode.Attributes("guid").Value, eDirectionalSearch.ToParents, True)
			lResourcesFiltered = lResources.FindAll(Function(f) f.Attributes("type").Value = "Free Space Threshold")

			' Nothing to scan for...
			If (lResourcesFiltered.Count = 0) Then
				returnState = ServerScanResults.Skipped
				Return Nothing
			End If
		Catch
			returnState = ServerScanResults.Exception
			Return Nothing
		End Try

		Try
			Dim sChecking As String = lResourcesFiltered(0).Attributes.ItemOf("checking").Value
			sThresholds = sChecking
		Catch
			returnState = ServerScanResults.Exception
			Return Nothing
		End Try

		Dim oResult As ManagementObjectCollection
		Dim sResult As String
		Dim rKey As RegistryKey
		Try
			' First, get the remote registry value for the system drive letter (might not be C:)
			rKey = RegistryKey.OpenRemoteBaseKey(RegistryHive.LocalMachine, lItem.Text.Trim).OpenSubKey("SOFTWARE\Microsoft\Windows NT\CurrentVersion", False)
			If (rKey Is Nothing) Then Return Nothing
			sResult = rKey.GetValue("SystemRoot", vbNullString, RegistryValueOptions.None).ToString
			If (sResult Is vbNullString) Then Return Nothing

			oResult = wmiConnect("\\" & Trim(lItem.Text.ToUpper) & "\root\cimv2", "SELECT * FROM Win32_LogicalDisk WHERE DriveType='3'", 5)
		Catch ex As Exception
			returnState = ServerScanResults.Exception
			Return Nothing
		End Try

		' Make sure system drive is first in list, then add the rest...
		Try
			For Each mObj As ManagementObject In oResult
				If (mObj("DeviceID").ToString = sResult.Substring(0, 2)) Then
					mObj("Name") = "THIS_ONE"
					mObj("Purpose") = sThresholds ' Sneaky - Using a unused item to hold the current thresholds.
					sOKSoFar.Insert(0, mObj)
				Else
					sOKSoFar.Add(mObj)
				End If
			Next
		Catch
			returnState = ServerScanResults.Exception
			Return Nothing
		End Try

		oResult.Dispose()
		Return sOKSoFar
	End Function
	' #############################################################################################

	Private Shared Function checkEventID(ByVal iEventID As Integer, ByVal sEventIDList As String) As Boolean
		If (sEventIDList = vbNullString) Then Return False
		Dim sExclusions() As String = Split(sEventIDList, ",")
		For Each sExclusion As String In sExclusions
			If (InStr(sExclusion, "-") = 0) Then
				' Compare Single Value
				If sExclusion <> vbNullString Then
					If (CInt(sExclusion) = iEventID) Then Return True
				End If
			Else
				' Compare Range
				Dim sRange As String() = Split(sExclusion, "-")
				If (iEventID >= CInt(Trim(sRange(0))) And (iEventID <= CInt(Trim(sRange(1))))) Then Return True
			End If
		Next
		Return False
	End Function

	Private Sub onClick_RescanServer(sender As System.Object, e As System.EventArgs)
		Call scanningSetup(True)

		' Remove current counts from total...
		Dim lItem As ListViewItem = lstResults.SelectedItems(0)
		For i As Integer = 1 To iScanCount - 1
			Dim sIssues As String = lItem.SubItems(i).Name.Substring(0, InStr(lItem.SubItems(i).Name, " issue"))
			If (IsNumeric(sIssues) = True) Then lstStats.Items(0).SubItems(i).Text = (CInt(lstStats.Items(0).SubItems(i).Text) - CInt(sIssues)).ToString
		Next
		lItem = Nothing

		bRescanServer = True
		Dim lArray As New List(Of ListViewItem)
		lArray.Add(lstResults.SelectedItems(0))
		Call Scan_Threadpool(lArray)
		lArray = Nothing
	End Sub

	Private Sub onClick_ShowDetails(sender As System.Object, e As System.EventArgs)
		Call lstResults_MouseDoubleClick(Nothing, Nothing)
	End Sub

	Private Sub cmdExport_Click(sender As System.Object, e As System.EventArgs) Handles cmdExport.Click
		frmScanReport.sTimeTaken = New TimeSpan(0, 0, CInt(swStopWatch.Elapsed.TotalSeconds))
		frmScanReport.ShowDialog(Me)
	End Sub

	Private Sub cmdLegend_Click(sender As System.Object, e As System.EventArgs) Handles cmdLegend.Click
		If (frmScanLegend.Visible = False) Then frmScanLegend.Show(Me)
	End Sub

	Private Sub lnkHelp_LinkClicked(sender As System.Object, e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lnkHelp.LinkClicked
		frmHelp.sSelectPageByID = "help0017"
		frmHelp.ShowDialog(Me)
	End Sub

	Private Sub lnkNote_LinkClicked(sender As System.Object, e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lnkNote.LinkClicked
		frmHelp.sSelectPageByID = "help0022"
		frmHelp.ShowDialog(Me)
	End Sub

	' #############################################################################################
	' #############################################################################################
	' ##                                                                                         ##
	' ##   Scan Threading Below...                                                               ##
	' ##                                                                                         ##
	' #############################################################################################
	' #############################################################################################

	Private Class classScan
		Public index As Integer
		Public HostListItem As ListViewItem
		Public scanDelegate As scanClassDelegate

		' Set scan order and change item icons...
		Public Sub scan(ByVal callback As Object)
			Dim bSkipCurrentServer As Boolean
			Dim sItem As New scanItem
			sItem.currState = ServerScanResults.Checking
			sItem.currSubItem = 1
			Array.Resize(sItem.Result_State, iScanCount)

			Try
				' Set "SCANNING" icon
				Call scanDelegate(index, sItem)
				If (bCancelScan = True) Then Exit Sub

				If (m_PingServersBeforeScan = True) Then
					Try
						bSkipCurrentServer = Not My.Computer.Network.Ping(HostListItem.Text.Trim, 500)	' This generates a hidden error, I hate it
					Catch ex As Exception
						bSkipCurrentServer = True
					End Try
				Else
					bSkipCurrentServer = False
				End If

				If (bSkipCurrentServer = True) Then
					' Failed getting PING, so fail everything...
					sItem.currState = ServerScanResults.Unknown
					Call scanDelegate(index, sItem)
					Exit Sub
				Else
					sItem.ShutdownTime = getServerShutdownTime(HostListItem.Text.Trim)

					For i As Integer = 1 To iScanCount - 1
						sItem.currSubItem = i
						Call scanDelegate(index, sItem)		' Set "Scanning" icon
						Select Case i
							Case 1 : sItem.Result_S = scanServer_Services(HostListItem, sItem.Result_State(1))
							Case 2 : sItem.Result_H = scanServer_Hotfixes(HostListItem, sItem.Result_State(2))
							Case 3 : sItem.Result_E = scanServer_EventLog(HostListItem, sItem.ShutdownTime, sItem.Result_State(3))
							Case 4 : sItem.Result_R = scanServer_Registry(HostListItem, sItem.Result_State(4))
							Case 5 : sItem.Result_F = scanServer_FileChecks(HostListItem, sItem.Result_State(5))
							Case 6 : sItem.Result_W = scanServer_WMIQuery(HostListItem, sItem.Result_State(6))
							Case 7 : sItem.Result_D = scanServer_DriveSpace(HostListItem, sItem.Result_State(7))
						End Select

						Application.DoEvents()
						If (sItem.Result_State(i) = ServerScanResults.Unknown) Then
							sItem.currState = ServerScanResults.Unknown
							Call scanDelegate(index, sItem)
							Exit Try
						End If
					Next
					sItem.currSubItem = iScanCount
					Call scanDelegate(index, sItem)
				End If

				Application.DoEvents()
				sItem.currState = ServerScanResults.Pass
				Call scanDelegate(index, sItem)
			Catch ex As Exception
				'Do Nothing
				Application.DoEvents()
				sItem.currState = ServerScanResults.Unknown
				Call scanDelegate(index, sItem)
			End Try
		End Sub
	End Class

	' Set scanning list...
	Private Sub Scan_Threadpool(ByVal lItems As List(Of ListViewItem))
		' Set thread count to be the number of processors in the server (min of 8)
		Dim iProcCount As Integer = Math.Max(Environment.ProcessorCount, 8)
		ThreadPool.SetMaxThreads(iProcCount, iProcCount)

		swStopWatch.Reset()
		swStopWatch.Start()

		For Each lItem As ListViewItem In lItems
			If (lItem.ImageKey.StartsWith("_16___Server") = True) Then
				Try
					Dim cScan As New classScan
					cScan.index = lItem.Index
					cScan.HostListItem = lstResults.Items(lItem.Index)
					cScan.scanDelegate = AddressOf scanDone
					Interlocked.Increment(scanThreadCount)
					ThreadPool.QueueUserWorkItem(AddressOf cScan.scan)
				Catch ex As Exception
					MessageBox.Show(Me, "Scan_Threadpool" & vbCrLf & ex.Message)
				End Try
			End If
		Next

		Me.lblStatus.Text = "Please wait, scanning..."
		proProgress.Maximum = scanThreadCount
		proProgress.Value = 0
	End Sub

	' Delegate sub called from the thread
	Private Sub scanDone(ByVal index As Integer, ByVal result As scanItem)
		If lstResults.InvokeRequired Then
			Try
				lstResults.Invoke(New scanClassDelegate(AddressOf scanResult), New Object() {index, result})
			Catch
			End Try
		Else
			scanResult(index, result)
		End If
	End Sub

	'Result runs on the UI thread, but is called via a delegate so it is allowed to update the control.
	Private Sub scanResult(ByVal index As Integer, ByVal sResult As scanItem)
		Dim iCount As Integer
		Dim lItem As ListViewItem = lstResults.Items(index)
		Dim lStat As ListViewItem = lstStats.Items(0)
		Dim iDriveThreshold_Fail As Integer
		Dim iDriveThreshold_Warn As Integer
		Dim iDriveThreshold_AllDrives As String = "Exclude"

		If (bCancelScan = False) Then
			If (index > iHighestItem) Then
				iHighestItem = index
				Call ensureVisible(Me.lstResults, lItem)
			End If

			Application.DoEvents()
			Select Case sResult.currState
				Case 2, 4	' .Pass , .Unknown
					' SCANNING COMPLETE...
					If (sResult.currState = 4) Then
						For i = 1 To iScanCount - 1 : doSubIcon(lItem, i, ServerScanResults.None, "") : Next i
						doSubIcon(lItem, 1, ServerScanResults.Unknown, "Failed to connect to server")
					End If
					If (sResult.ShutdownTime <> Nothing) Then lItem.Name = lItem.Name.Replace("Unknown", getDateDiff(Now, sResult.ShutdownTime))
					If (Interlocked.Decrement(scanThreadCount) = 0) Then Call scanningSetup(False)
					proProgress.Value = proProgress.Maximum - scanThreadCount

				Case 1		' .Checking
					' SCANNING...
					Application.DoEvents()
					Select Case sResult.currSubItem
						Case 1
							For i As Integer = 1 To iScanCount - 1 : doSubIcon(lItem, i, ServerScanResults.None, "") : Next
							doSubIcon(lItem, 1, ServerScanResults.Checking, "")
						Case 2 : lItem.SubItems(1).Tag = sResult.Result_S : If (sResult.Result_S Is Nothing) Then iCount = -1 Else iCount = sResult.Result_S.Count
						Case 3 : lItem.SubItems(2).Tag = sResult.Result_H : If (sResult.Result_H Is Nothing) Then iCount = -1 Else iCount = sResult.Result_H.Count
						Case 4 : lItem.SubItems(3).Tag = sResult.Result_E : If (sResult.Result_E Is Nothing) Then iCount = -1 Else iCount = sResult.Result_E.Count
						Case 5 : lItem.SubItems(4).Tag = sResult.Result_R : If (sResult.Result_R Is Nothing) Then iCount = -1 Else iCount = sResult.Result_R.Count
						Case 6 : lItem.SubItems(5).Tag = sResult.Result_F : If (sResult.Result_F Is Nothing) Then iCount = -1 Else iCount = sResult.Result_F.Count
						Case 7 : lItem.SubItems(6).Tag = sResult.Result_W : If (sResult.Result_W Is Nothing) Then iCount = -1 Else iCount = sResult.Result_W.Count
						Case 8 : lItem.SubItems(7).Tag = sResult.Result_D : If (sResult.Result_D Is Nothing) Then iCount = -1 Else iCount = 1
						Case Else
							Application.DoEvents()
					End Select

					Application.DoEvents()
					Dim sExceptionMessage As String = "Error occurred during scan"
					Select Case sResult.currSubItem
						Case 1
							Application.DoEvents()

						Case 2, 3, 6, 7
							Application.DoEvents()
							If (iCount > 0) Then
								Call doSubIcon(lItem, sResult.currSubItem - 1, CType(sResult.Result_State(sResult.currSubItem - 1), ServerScanResults), iCount & " issue" & IIf(iCount > 1, "s", "").ToString)
								Call updateStats(lStat, sResult.currSubItem - 1, iCount)

							ElseIf (CType(sResult.Result_State(sResult.currSubItem - 1), ServerScanResults) = ServerScanResults.Exception) Then
								Call doSubIcon(lItem, sResult.currSubItem - 1, ServerScanResults.Exception, sExceptionMessage)

							Else
								Call SkippedOK(lItem, sResult)
							End If
							Call doSubIcon(lItem, sResult.currSubItem, ServerScanResults.Checking, "")

						Case 4
							Application.DoEvents()
							If (iCount > 0) Then
								Dim eResultAPP As List(Of EventLogEntry) = sResult.Result_E(0)
								Dim eResultSYS As List(Of EventLogEntry) = sResult.Result_E(1)
								Dim eResultSEC As List(Of EventLogEntry) = sResult.Result_E(2)
								If ((eResultAPP IsNot Nothing) Or (eResultSYS IsNot Nothing) Or (eResultSEC IsNot Nothing)) Then
									If ((eResultAPP.Count > 0) Or (eResultSYS.Count > 0) Or (eResultSEC.Count > 0)) Then
										Dim eCnt As Integer = eResultAPP.Count + eResultSYS.Count + eResultSEC.Count
										Dim sTip As String = eCnt & " issue" & IIf(eCnt > 1, "s", "").ToString & vbCrLf
										If (eResultAPP.Count > 0) Then sTip = sTip & "    Application:" & vbTab & eResultAPP.Count & vbCrLf
										If (eResultSYS.Count > 0) Then sTip = sTip & "    System:     " & vbTab & eResultSYS.Count & vbCrLf
										If (eResultSEC.Count > 0) Then sTip = sTip & "    Security:   " & vbTab & eResultSEC.Count & vbCrLf
										sTip = sTip.Trim(CChar(vbCrLf))
										Call doSubIcon(lItem, sResult.currSubItem - 1, CType(sResult.Result_State(sResult.currSubItem - 1), ServerScanResults), sTip)
										Call updateStats(lStat, sResult.currSubItem - 1, eCnt)

									Else
										Call SkippedOK(lItem, sResult)
									End If
								End If

							ElseIf (CType(sResult.Result_State(sResult.currSubItem - 1), ServerScanResults) = ServerScanResults.Exception) Then
								Call doSubIcon(lItem, sResult.currSubItem - 1, ServerScanResults.Exception, sExceptionMessage)

							Else
								Call doSubIcon(lItem, sResult.currSubItem - 1, ServerScanResults.Skipped, "Skipped")
							End If
							Call doSubIcon(lItem, sResult.currSubItem, ServerScanResults.Checking, "")

						Case 5
							Application.DoEvents()
							If (iCount > 0) Then
								Dim iCountF As Integer = 0
								Dim iCountW As Integer = 0
								For Each rItem As String In sResult.Result_R
									If (rItem.Substring(0, 1) = "W") Then iCountF = iCountF + 1
									If (rItem.Substring(0, 1) = "F") Then iCountW = iCountW + 1
								Next
								Dim sIcon As ServerScanResults = CType(IIf(iCountW = 0, ServerScanResults.Warning, ServerScanResults.Fail), ServerScanResults)
								Call doSubIcon(lItem, sResult.currSubItem - 1, sIcon, iCount & " issue" & IIf(iCount > 1, "s", "").ToString)
								Call updateStats(lStat, sResult.currSubItem - 1, iCount)

							ElseIf (CType(sResult.Result_State(sResult.currSubItem - 1), ServerScanResults) = ServerScanResults.Exception) Then
								Call doSubIcon(lItem, sResult.currSubItem - 1, ServerScanResults.Exception, sExceptionMessage)

							Else
								Call SkippedOK(lItem, sResult)
							End If
							Call doSubIcon(lItem, sResult.currSubItem, ServerScanResults.Checking, "")

						Case 8
							' Get Thresholds...
							'	"Purpose" is being set in 'scanServer_DriveSpace'
							Application.DoEvents()
							If (iCount > 0) Then
								Try
									Dim sDT As String = sResult.Result_D(0)("Purpose").ToString
									If (sDT IsNot Nothing) Then
										Dim sData() As String = sDT.Split("|"c)
										iDriveThreshold_Fail = CInt(sData(0))
										iDriveThreshold_Warn = CInt(sData(1))
										If (sData.Count > 2) Then iDriveThreshold_AllDrives = sData(2)
									End If
								Catch
									Application.DoEvents()
									Call doSubIcon(lItem, sResult.currSubItem - 1, ServerScanResults.Unknown, "Unknown")
									Exit Select
								End Try

								Dim iPercentAll As Integer = 100
								Dim sToolTip As New StringBuilder
								Dim eDrives As String = xml_GetExcludedDrives(lItem.Name.Split("|"c)(0))
								If (eDrives Is Nothing) Then eDrives = " "

								For Each mObj As ManagementObject In sResult.Result_D
									Try
										' Check for excluded drives, and ignore them...
										If (eDrives.Contains(mObj("DeviceID").ToString & ",") = False) Then
											Console.WriteLine("IGNORE ME: VolumeName: " & mObj("VolumeName").ToString)

											Dim iPercent As Integer = CInt((100 / CLng(mObj("Size"))) * CLng(mObj("FreeSpace")))
											If (iPercent < iPercentAll) Then iPercentAll = iPercent
											If (sToolTip.Length > 0) Then sToolTip.Append(vbCrLf)
											sToolTip.Append(mObj("DeviceID").ToString & vbTab & iPercent.ToString & "% free")
										End If
									Catch ex As Exception
										If (sToolTip.Length > 0) Then sToolTip.Append(vbCrLf)
										sToolTip.Append(mObj("DeviceID").ToString & vbTab & "(Unknown)")
									End Try
								Next

								Try
									If (iDriveThreshold_AllDrives = "Exclude") Then iPercentAll = CInt((100 / CLng(sResult.Result_D(0)("Size"))) * CLng(sResult.Result_D(0)("FreeSpace")))
									Select Case iPercentAll
										Case 0 To iDriveThreshold_Fail
											Call doSubIcon(lItem, sResult.currSubItem - 1, ServerScanResults.Fail, sToolTip.ToString)
											Call updateStats(lStat, sResult.currSubItem - 1, iCount)

										Case (iDriveThreshold_Fail + 1) To iDriveThreshold_Warn
											Call doSubIcon(lItem, sResult.currSubItem - 1, ServerScanResults.Warning, sToolTip.ToString)
											Call updateStats(lStat, sResult.currSubItem - 1, iCount)

										Case Else
											Call doSubIcon(lItem, sResult.currSubItem - 1, ServerScanResults.Pass, sToolTip.ToString)
									End Select
								Catch
									Application.DoEvents()
									Call doSubIcon(lItem, sResult.currSubItem - 1, ServerScanResults.Unknown, "Unknown")
								End Try
							Else
								Call SkippedOK(lItem, sResult)
								'Call doSubIcon(lItem, sResult.currSubItem - 1, ServerScanResults.Unknown, "Unknown")
							End If

						Case Else
							' SHOULD NEVER SEE THIS.!
							MessageBox.Show(Me, "ERROR SCANNING:" & lItem.Text & vbCrLf & sResult.currSubItem.ToString)
					End Select
				Case Else
					' SHOULD NEVER SEE THIS.!
					MessageBox.Show(Me, "COMPELTE ERROR: " & sResult.currState)
			End Select
		Else

			' Cancel scanning...
			If (scanThreadCount > 0) Then
				lblStatus.Text = "Waiting for threads to terminate... (" & scanThreadCount & ")"
				proProgress.Value = 0
				If ((lItem.SubItems(1).Text <> "ICON:0") AndAlso (lItem.SubItems(1).Text <> vbNullString)) Then
					For i = 1 To iScanCount - 1
						doSubIcon(lItem, i, ServerScanResults.None, "")
					Next i
				End If
				If (Interlocked.Decrement(scanThreadCount) = 0) Then Call scanningSetup(False)
			End If
		End If
		lItem = Nothing
	End Sub

	Private Sub SkippedOK(ByVal lItem As ListViewItem, ByVal sResult As scanItem)
		If sResult.Result_State(sResult.currSubItem - 1) = ServerScanResults.Skipped Then
			Call doSubIcon(lItem, sResult.currSubItem - 1, ServerScanResults.Skipped, "Skipped")
		Else
			Call doSubIcon(lItem, sResult.currSubItem - 1, ServerScanResults.Pass, "All OK")
		End If
	End Sub
End Class