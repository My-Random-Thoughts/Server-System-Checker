Option Explicit On
Imports System.Xml
Imports System.Threading
Imports System.Management
Imports System.Net

Public Class frmPropertiesServerGetAll
	Public sGroupGUID As String		' Currently selected group to which conflicting resource it to be added

	Public Structure infoItem
		Public currState As Integer		' Same values as "ServerScanResults"
		Public Result_Data() As String	' Returned Data
	End Structure
	Private Delegate Sub infoDelegate(ByVal id As String)
	Private Delegate Sub infoClassDelegate(ByVal index As Integer, ByVal status As infoItem)
	Private Shared infoThreadCount As Integer = 0		' Track of current number of threads
	Private Shared iHighestItem As Integer = 0			' Used for "EnsureVisible"
	Private Shared bCancelScan As Boolean = False

	Private bSomeItemsSkipped As Boolean = False
	Private bSkipExistingItems As Boolean = True
	Private bCurrentlyScanning As Boolean = False

	Private img16 As New ImageList
	Private imgSPACING As New ImageList

	Private Sub frmPropertiesServerGetAll_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
		For Each c As Control In Controls
			c.Font = frmMain.sysFont
		Next
		lblTitle.Font = frmMain.sysFontTitle
		lnkHelp.Font = frmMain.sysFontHelp

		lnkHelp.LinkBehavior = LinkBehavior.HoverUnderline
		picIcon.Image = My.Resources._48___Report.ToBitmap
		lblStatus.Text = vbNullString

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
				.Add("_16___Scan___Unknown", My.Resources._16___Scan___Unknown)					' 4 - Grey 'Stop'
				.Add("_16___Eventlog___Warning", My.Resources._16___Eventlog___Warning)			' 5 - Yellow Triangle
				.Add("_16___Scan___Skipped", My.Resources._16___Scan___Skipped)					' 6 - Yellow Bar

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

		With lstServerGroups
			.Groups.Clear()
			.Items.Clear()
			.SmallImageList = imgSPACING
			.View = View.Details
			.FullRowSelect = True
			.HeaderStyle = ColumnHeaderStyle.None
			With .Columns
				.Clear()
				.Add("A", lstServerGroups.Width - 32 - SystemInformation.VerticalScrollBarWidth - 4)
				.Add("B", 32)
			End With
		End With

		If (bUseVisualStyles = True) Then
			Call SetWindowTheme(lstServerGroups.Handle, "Explorer", Nothing)
		End If

		Me.Visible = True
		Application.DoEvents()

		Call buildTreeInListView()
		Application.DoEvents()

		If (bSomeItemsSkipped = True) Then
			bSkipExistingItems = True
			clsMessageBox.CustomMsgBox(New String() {"Rescan", "Skip"})
			Dim sMsg As String = "We already have the system information for some servers." & vbCrLf & "What would you like to do.?"
			Dim dResult As DialogResult = MessageBox.Show(Me, sMsg, APN, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
			If (dResult = Windows.Forms.DialogResult.Yes) Then bSkipExistingItems = False
		End If
		Application.DoEvents()

		Call scanningSetup(True)
		Me.Cursor = Cursors.WaitCursor
		Dim iCnt As Integer = 0
		Dim lArray As New List(Of ListViewItem)
		For Each lItem As ListViewItem In lstServerGroups.Items
			If (lItem.ImageKey.StartsWith("_16___Server") = True) Then
				If ((bSkipExistingItems = True) AndAlso (lItem.SubItems(1).Text = "ICON:2")) Then
				Else
					iCnt = iCnt + 1
					lArray.Add(lItem)
				End If
			End If
		Next

		If (iCnt > 0) Then Call Scan_Threadpool(lArray) Else Call scanningSetup(False)
		Me.Cursor = Cursors.Default
		lArray = Nothing
	End Sub

	Private Sub buildTreeInListView()
		Dim lItem As ListViewItem
		Dim iIndent As Integer
		Dim lFound() As ListViewItem

		' First create the group structure...
		lstServerGroups.BeginUpdate()
		Dim xGroupList As List(Of XmlNode) = xml_getGroupList_FromGroup(sGroupGUID, eDirectionalSearch.ToParents)
		xGroupList.AddRange(xml_getGroupList_FromGroup(sGroupGUID, eDirectionalSearch.ToChildren))
		For Each gItem As XmlNode In xGroupList
			If (gItem.ParentNode.Name = "root") Then
				iIndent = 0
			Else
				lFound = lstServerGroups.Items.Find(gItem.ParentNode.Attributes.ItemOf("guid").Value, False)
				If ((lFound Is Nothing) Or (lFound.Count = 0)) Then iIndent = 0 Else iIndent = lFound(0).IndentCount + 1
			End If

			lFound = Nothing
			lFound = lstServerGroups.Items.Find(gItem.Attributes.ItemOf("guid").Value, False)
			If (lFound.Count = 0) Then
				lItem = New ListViewItem(gItem.Attributes.ItemOf("name").Value)
				lItem.ImageKey = "_16___Group___" & getGroupColour(CInt(gItem.Attributes.ItemOf("colr").Value), False)
				lItem.Name = gItem.Attributes.ItemOf("guid").Value
				lItem.IndentCount = iIndent
				lItem.SubItems.Add("")
				lstServerGroups.Items.Add(lItem)
			End If
		Next

		' Next add the servers to the groups...
		bSomeItemsSkipped = False
		For Each lvItem As ListViewItem In lstServerGroups.Items
			Dim xServerList As List(Of XmlNode) = xml_getServerList_FromGroup(lvItem.Name)
			If (xServerList IsNot Nothing) Then
				xServerList.Reverse()
				For Each sItem As XmlNode In xServerList
					If (sItem.ParentNode.Attributes.ItemOf("guid").Value = lvItem.Name) Then
						lItem = New ListViewItem(sItem.Attributes.ItemOf("name").Value.ToLower)
						If m_UppercaseServerNames = True Then lItem.Text = lItem.Text.ToUpper()
						lItem.ImageKey = "_16___Server"
						lItem.Name = sItem.Attributes.ItemOf("guid").Value
						lItem.IndentCount = lvItem.IndentCount + 1
						lItem.SubItems.Add("ICON:" & ServerScanResults.None)

						If (xml_LoadServerData(lItem.Name) IsNot Nothing) Then
							lItem.ImageKey = "_16___Server_With_Properties"
							Call doSubIcon(lItem, 1, ServerScanResults.Pass, "Data already in database")
							bSomeItemsSkipped = True
						End If

						lstServerGroups.Items.Insert(lvItem.Index + 1, lItem)
					End If
				Next
			End If
		Next
		lstServerGroups.EndUpdate()
		lstServerGroups.Refresh()
		Application.DoEvents()
	End Sub

	Private Sub cmdClose_Click(sender As System.Object, e As System.EventArgs) Handles cmdClose.Click
		If (bCurrentlyScanning = True) Then
			bCancelScan = True
		Else
			img16.Dispose()
			imgSPACING.Dispose()
			Me.Close()
			Me.Dispose()
		End If
	End Sub

	Private Sub scanningSetup(ByVal bIsStarting As Boolean)
		proProgress.Value = 0
		lblStatus.Visible = bIsStarting
		bCurrentlyScanning = bIsStarting
		proProgress.Visible = bIsStarting
		picIcon.Focus()

		If (bIsStarting = True) Then
			lblStatus.Text = "Please wait, scanning..."
			lblSubTitle.Text = "Please wait for the process to complete..."
			cmdClose.Text = "Cancel"
			Me.Cursor = Cursors.WaitCursor
		Else
			lblStatus.Text = vbNullString
			lblSubTitle.Text = "Process complete, click Close below"
			cmdClose.Text = "Close"
			Call xml_SaveXmlDocument(sConfigFile)
			Me.Cursor = Cursors.Default
		End If
		Application.DoEvents()

		If (bCancelScan = True) Then MessageBox.Show(Me, "Scan Cancelled", APN, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
		bCancelScan = False
	End Sub

	Private Shared Sub doSubIcon(ByVal lItem As ListViewItem, ByVal iSubItemIndex As Integer, ByVal iIcon As ServerScanResults, ByVal sDescription As String)
		Dim sText As String
		If (iIcon <> ServerScanResults.StringText) Then sText = "ICON:" & iIcon Else sText = sDescription
		lItem.SubItems(iSubItemIndex).Text = sText
		lItem.SubItems(iSubItemIndex).Name = sDescription
		Application.DoEvents()
	End Sub

	Private Sub lstServerGroups_MouseMove(sender As Object, e As System.Windows.Forms.MouseEventArgs)
		Static prevMousePos As Point = New Point(-1, -1)
		Dim lv As ListView = TryCast(sender, ListView)
		If (lv Is Nothing) Then Exit Sub
		If (prevMousePos = MousePosition) Then Exit Sub
		With lv.HitTest(lv.PointToClient(MousePosition))
			If ((.SubItem IsNot Nothing) AndAlso (Not String.IsNullOrEmpty(.SubItem.Text))) Then
				If (.SubItem.Bounds.Left < 10) Then Exit Sub ' Don't show for first column
				Static t As New ToolTip()
				t.ShowAlways = True
				t.UseFading = True
				t.IsBalloon = False
				t.Show(.SubItem.Name, .Item.ListView, .SubItem.Bounds.Location + New Size(7, .SubItem.Bounds.Height + 1), 5000)
			End If
			prevMousePos = MousePosition
		End With
	End Sub

	Private Sub lnkHelp_LinkClicked(sender As System.Object, e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lnkHelp.LinkClicked
		frmHelp.sSelectPageByID = "help0029"
		frmHelp.ShowDialog(Me)
	End Sub

	Private Class classScan
		Public index As Integer
		Public HostListItem As ListViewItem
		Public infoDelegate As infoClassDelegate

		Public Sub info(ByVal callback As Object)
			Dim iItem As New infoItem
			iItem.currState = ServerScanResults.Checking

			Try
				Call infoDelegate(index, iItem)		' Set "Scanning" icon
				If (bCancelScan = True) Then Exit Sub

				iItem.Result_Data = getServerInfo(HostListItem.Name) ' Using Server GUID here, not the name
				If (iItem.Result_Data(0) <> "Unknown Server Type") Then
					If (xml_SaveServerData(HostListItem.Name.Trim, iItem.Result_Data) = True) Then
						iItem.currState = ServerScanResults.Pass
					Else
						iItem.currState = ServerScanResults.Fail
					End If
				Else
					iItem.currState = ServerScanResults.Unknown
				End If
				Call infoDelegate(index, iItem)

			Catch ex As Exception
				'Do Nothing
				Application.DoEvents()
				iItem.currState = ServerScanResults.Fail
				Call infoDelegate(index, iItem)
			End Try
		End Sub
	End Class

	Private Sub Scan_Threadpool(ByVal lItems As List(Of ListViewItem))
		' Set thread count to be the number of processors in the server (min of 8)
		Dim iProcCount As Integer = Math.Max(Environment.ProcessorCount, 8)
		ThreadPool.SetMaxThreads(iProcCount, iProcCount)

		For Each lItem As ListViewItem In lItems
			If (lItem.ImageKey.StartsWith("_16___Server") = True) Then
				Try
					Dim cScan As New classScan
					cScan.index = lItem.Index
					cScan.HostListItem = lstServerGroups.Items(lItem.Index)
					cScan.infoDelegate = AddressOf infoDone
					Interlocked.Increment(infoThreadCount)
					ThreadPool.QueueUserWorkItem(AddressOf cScan.info)
				Catch ex As Exception
					MessageBox.Show(Me, "Scan_Threadpool" & vbCrLf & ex.Message)
				End Try
			End If
		Next

		Me.lblStatus.Text = "Please wait, scanning..."
		proProgress.Maximum = infoThreadCount
		proProgress.Value = 0
	End Sub

	Private Sub infoDone(ByVal index As Integer, ByVal result As infoItem)
		If lstServerGroups.InvokeRequired Then
			Try
				lstServerGroups.Invoke(New infoClassDelegate(AddressOf infoResult), New Object() {index, result})
			Catch
			End Try
		Else
			infoResult(index, result)
		End If
	End Sub

	Private Sub infoResult(ByVal index As Integer, ByVal sResult As infoItem)
		Dim lItem As ListViewItem = lstServerGroups.Items(index)

		If (bCancelScan = False) Then
			If (index > iHighestItem) Then
				iHighestItem = index
				Call ensureVisible(Me.lstServerGroups, lItem)
			End If

			Application.DoEvents()
			Select Case sResult.currState
				Case 2, 3, 4 ' .Pass, Fail, .Unknown
					' SCANNING COMPLETE...
					Call doSubIcon(lItem, 1, CType(sResult.currState, ServerScanResults), sResult.Result_Data(0))
					If (Interlocked.Decrement(infoThreadCount) = 0) Then Call scanningSetup(False)
					proProgress.Value = proProgress.Maximum - infoThreadCount

					If (sResult.currState = ServerScanResults.Pass) Then
						Try
							lItem.ImageKey = "_16___Server_With_Properties"
							Dim sNode As TreeNode = frmMain.tvwServerList.Nodes.Find(lItem.Name, True)(0)
							With sNode
								.Tag = "SERVWP"
								.ImageKey = "_16___Server_With_Properties"
								.SelectedImageKey = .ImageKey
							End With
						Catch ex As Exception
						End Try
					End If

				Case 1		' .Checking
					Application.DoEvents()
					Call doSubIcon(lItem, 1, ServerScanResults.Checking, "Scanning")

				Case Else
					' SHOULD NEVER SEE THIS.!
					MessageBox.Show(Me, "'infoResult' Error: " & sResult.currState)
			End Select
		Else
			' Cancel scanning...
			If (infoThreadCount > 0) Then
				lblStatus.Text = "Waiting for threads to terminate... (" & infoThreadCount & ")"
				proProgress.Value = 0
				If ((lItem.SubItems(1).Text <> "ICON:0") AndAlso (lItem.SubItems(1).Text <> vbNullString)) Then
					doSubIcon(lItem, 1, ServerScanResults.None, "")
				End If
				If (Interlocked.Decrement(infoThreadCount) = 0) Then Call scanningSetup(False)
			End If
		End If
		lItem = Nothing
	End Sub

	''' <summary>
	''' Gets the server information required from the selected server
	''' </summary>
	''' <param name="sServerGUID">STRING: Server to connect to</param>
	''' <returns>STRING(): Array of data results</returns>
	''' <remarks>Performs multiple WMI queries against the selected server</remarks>
	Public Shared Function getServerInfo(ByVal sServerGUID As String) As String()
		Dim sData(10) As String
		Dim sServerName As String = xml_getServerName(sServerGUID)
		If (m_PingServersBeforeScan = True) Then
			Try
				My.Computer.Network.Ping(sServerName, 500)	   ' This generates a hidden error, I hate it
			Catch ex As Exception
				sData(0) = "Unknown Server Type"
				Return sData
			End Try
		End If

		Try
			Dim oResult As ManagementObjectCollection = wmiConnect("\\" & sServerName & "\root\cimv2", "SELECT * FROM Win32_PhysicalMemory", 5)
			If (oResult IsNot Nothing) Then
				sData(2) = vbNullString
				For Each mObj As ManagementObject In oResult
					sData(2) = sData(2) & mObj("Capacity").ToString & "|"
				Next
			End If
			oResult.Dispose()
		Catch ex As Exception
			sData(0) = "Unknown Server Type"
			Return sData
		End Try

		Try
			Dim oResult As ManagementObjectCollection = wmiConnect("\\" & sServerName & "\root\cimv2", "SELECT * FROM Win32_ComputerSystemProduct", 5)
			If (oResult IsNot Nothing) Then
				For Each mObj As ManagementObject In oResult
					sData(3) = mObj("Vendor").ToString.Trim
					sData(4) = mObj("Name").ToString.Trim
					sData(5) = mObj("IdentifyingNumber").ToString.Trim
					Exit For
				Next
			End If
			oResult.Dispose()
		Catch ex As Exception
		End Try

		Dim iCnt As Integer = 0
		Try
			' (The number of physical hyperthreading-enabled processors or the number of physical multicore processors is incorrectly reported in Windows Server 2003)
			Dim bKB932370 As Boolean = False
			Dim oResult As ManagementObjectCollection = wmiConnect("\\" & sServerName & "\root\cimv2", "SELECT * FROM Win32_Processor", 5)
			If (oResult IsNot Nothing) Then
				Try
					' Windows 2008 and KB932370 Hot-fixed Windows 2003 Servers...
					For Each mObj As ManagementObject In oResult : Console.WriteLine(mObj("NumberOfLogicalProcessors")) : Next
					sData(6) = oResult.Count.ToString.Trim
					bKB932370 = True
				Catch ex As Exception
					' Non Hot-fixed Windows 2003 Servers and Below...
					sData(6) = oResult.Count.ToString.Trim
					bKB932370 = False
				End Try

				For Each mObj As ManagementObject In oResult
					sData(7) = mObj("Name").ToString.Trim
					If bKB932370 = True Then
						sData(8) = mObj("NumberOfCores").ToString.Trim
						If (CInt(mObj("NumberOfCores").ToString) < CInt(mObj("NumberOfLogicalProcessors").ToString)) Then
							sData(9) = "Enabled"
						Else
							sData(9) = "Disabled"
						End If
					Else
						sData(8) = "-"
						sData(9) = "-"
					End If
					Exit For
				Next
			End If
			oResult.Dispose()
		Catch ex As Exception
		End Try

		Try
			Dim oResult As ManagementObjectCollection = wmiConnect("\\" & sServerName & "\root\cimv2", "SELECT * FROM Win32_OperatingSystem", 5)
			If (oResult IsNot Nothing) Then
				For Each mObj As ManagementObject In oResult
					sData(0) = mObj("Caption").ToString.Trim
					sData(1) = mObj("CSDVersion").ToString.Trim
					Exit For
				Next
			End If
			oResult.Dispose()
		Catch ex As Exception
		End Try

		Return sData
	End Function

	''' <summary>
	''' Saves the server information to the XML file
	''' </summary>
	''' <param name="sServerGUID">STRING: Server GUID to save</param>
	''' <param name="sData">STRING(): Array of data to save</param>
	''' <returns>BOOLEAN: True/False state of successful save</returns>
	''' <remarks></remarks>
	Public Shared Function xml_SaveServerData(ByVal sServerGUID As String, ByVal sData() As String) As Boolean
		Try
			' Check if info already exists...
			Dim iNode As XmlNode = xmlDoc.SelectSingleNode("descendant::server[@guid='" & sServerGUID & "']/info")
			If (iNode Is Nothing) Then
				Dim eNode As XmlElement = xmlDoc.CreateElement("info")
				For i As Integer = 0 To sData.Count - 1
					eNode.SetAttribute("i" & i.ToString, sData(i))
				Next
				iNode = xmlDoc.SelectSingleNode("descendant::server[@guid='" & sServerGUID & "']")
				iNode.AppendChild(eNode)
			Else
				For i As Integer = 0 To sData.Count - 1
					Try
						iNode.Attributes.ItemOf("i" & i).Value = sData(i)
					Catch ex As Exception
						Dim xAtt As XmlAttribute = xmlDoc.CreateAttribute("i" & i)
						iNode.Attributes.Append(xAtt).Value = sData(i)
					End Try
				Next
			End If
			Return True
		Catch ex As Exception
			Return False
		End Try
	End Function
End Class