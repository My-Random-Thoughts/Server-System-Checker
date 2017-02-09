Option Explicit On

Imports System.Xml
Imports System.Management
Imports System.IO

Public Class frmAddHotFix
	Private Const ETF As String = "External Text File"
	Private lvwH As ListViewColumnSorter
	Private lvwL As ListViewColumnSorter
	Private iCurrSelected As Integer = -1	' Index of currently selected combobox item
	Private img16 As New ImageList
	Private imgSPACING As New ImageList
	Public sGroupGUID As String
	Private mnuRightClick As New ContextMenuStrip

	Private Sub frmAddHotFix_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
		For Each c As Control In Controls
			c.Font = frmMain.sysFont
		Next
		lblTitle.Font = frmMain.sysFontTitle
		lnkHelp.Font = frmMain.sysFontHelp
		lnkNote.Font = frmMain.sysFontHelp
		lnkNote.Top = lnkHelp.Bottom + 1

		lnkHelp.LinkBehavior = LinkBehavior.HoverUnderline
		lnkNote.LinkBehavior = LinkBehavior.HoverUnderline
		picIcon.Image = My.Resources._48___Hotfix.ToBitmap

		lvwH = New ListViewColumnSorter()
		lvwL = New ListViewColumnSorter()

		With img16
			.ImageSize = New Size(16, 16)
			.ColorDepth = ColorDepth.Depth32Bit
			With .Images
				.Clear()
				.Add("_16___Resource___Hotfix", My.Resources._16___Resource___Hotfix)
				.Add("_16___Connection", My.Resources._16___Connection)
				.Add("_16___Add", My.Resources._16___Add)
				.Add("_16___Remove", My.Resources._16___Remove)
			End With
		End With

		With imgSPACING
			.ImageSize = New Size(iListViewIconWidth, iListViewLineHeight)
			.ColorDepth = ColorDepth.Depth32Bit
			For i As Integer = 0 To img16.Images.Count - 1
				.Images.Add(img16.Images.Keys(i), ResizeImage(img16.Images(i), .ImageSize, True))
			Next
		End With

		cmdAdd.ImageList = img16
		cmdDel.ImageList = img16
		cmdAdd.ImageKey = "_16___Add"
		cmdDel.ImageKey = "_16___Remove"
		cmdCloseImportList.Visible = False

		With SplitContainer1
			.Panel2Collapsed = True
			.SplitterDistance = 200
			.IsSplitterFixed = True
		End With
		proProgress.Visible = False

		With mnuRightClick.Items
			.Clear()
		End With

		With lstImportList
			.Clear()
			.View = View.Details
			With .Groups
				.Add("Pri", "Hotfix Patches")
				.Add("Sec", "Other Updates")
				.Add("WAIT", "Please Wait")
			End With
			With .Columns
				.Add("list", "Import List", lstImportList.Width - 75 - SystemInformation.VerticalScrollBarWidth - 4)
				.Add("date", "Install Date", 75, HorizontalAlignment.Right, -1)
			End With
			.FullRowSelect = True
			.HeaderStyle = ColumnHeaderStyle.Clickable
			.SmallImageList = imgSPACING
			.LabelEdit = True
			.ListViewItemSorter = lvwL
		End With
		lvwL.SortColumn = lstImportList.Columns("list").Index
		lvwL.Order = SortOrder.Descending
		Call ShowListViewSortImage(lstImportList, lvwL.SortColumn, lvwL.Order)

		With lstHotfixList
			.Clear()
			.View = View.Details
			With .Columns
				.Add("list", "Hotfix ID", lstHotfixList.Width - 85 - SystemInformation.VerticalScrollBarWidth - 4)
				.Add("check", "Check", 85, HorizontalAlignment.Center, -1)
			End With
			.FullRowSelect = True
			.HeaderStyle = ColumnHeaderStyle.Nonclickable
			.SmallImageList = imgSPACING
			.LabelEdit = True
			.ListViewItemSorter = lvwH
		End With
		lvwH.SortColumn = lstHotfixList.Columns("list").Index
		lvwH.Order = SortOrder.Ascending

		Dim cItem As ctrlComboBox_Icons.IconComboItem
		cmoIconComboBox.ItemHeight = 19
		cmoIconComboBox.DropDownHeight = 192
		With cmoIconComboBox.Items
			.Clear()
			cItem = New ctrlComboBox_Icons.IconComboItem(LH)	' Local Host
			cItem.ItemImage = My.Resources._16___Resource___Drive
			cItem.IndentCount = 0
			.Add(cItem)

			cItem = New ctrlComboBox_Icons.IconComboItem(ETF)	' External Text File
			cItem.ItemImage = My.Resources._16___Properties
			cItem.IndentCount = 0
			.Add(cItem)

			cmoIconComboBox.SelectedIndex = -1
			If (m_NetworkAvailability = True) Then Call buildTreeInListView()
		End With

		iCurrSelected = -1

		If (bUseVisualStyles = True) Then
			Call SetWindowTheme(lstHotfixList.Handle, "Explorer", Nothing)
			Call SetWindowTheme(lstImportList.Handle, "Explorer", Nothing)
		End If

		Me.Visible = True
		Application.DoEvents()
	End Sub

	Private Sub cmdCancel_Click(sender As System.Object, e As System.EventArgs) Handles cmdCancel.Click
		img16.Dispose()
		imgSPACING.Dispose()
		iCurrSelected = -1
		Me.Close()
		Me.Dispose()
	End Sub

	Private Sub lnkHelp_LinkClicked(sender As System.Object, e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lnkHelp.LinkClicked
		frmHelp.sSelectPageByID = "help0008"
		frmHelp.ShowDialog(Me)
	End Sub

	Private Sub cmdAdd_Click(sender As System.Object, e As System.EventArgs) Handles cmdAdd.Click
		Call addItem(vbNullString, "Installed", True, lstHotfixList, vbNullString)
	End Sub

	Private Sub lstHotfixes_AfterLabelEdit(sender As Object, e As System.Windows.Forms.LabelEditEventArgs) Handles lstHotfixList.AfterLabelEdit
		If ((e.Label = Nothing) OrElse (e.Label = String.Empty)) Then e.CancelEdit = True
		If ((e.CancelEdit = True) And (lstHotfixList.Items(e.Item).Text = vbNullString)) Then lstHotfixList.Items(e.Item).Remove()
		If e.CancelEdit = True Then Exit Sub

		Dim sKBItem As String = validateKBInput(e.Label)
		If (sKBItem IsNot Nothing) Then
			e.CancelEdit = True
			lstHotfixList.Items(e.Item).Text = sKBItem
			lstHotfixList.Items(e.Item).Name = sKBItem
			lstHotfixList.Items(e.Item).Selected = False
			lstHotfixList.Sort()
		Else
			e.CancelEdit = True
			MessageBox.Show(Me, "Invalid KB number format or duplicate KB number found.", APN, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			lstHotfixList.Items(e.Item).BeginEdit()
		End If
	End Sub

	Private Sub cmdDel_Click(sender As System.Object, e As System.EventArgs) Handles cmdDel.Click
		If (lstHotfixList.SelectedItems.Count = 0) Then Exit Sub
		Dim sResult As DialogResult = MessageBox.Show(Me, "Are you sure you want to delete the selected hotfixes.?", APN, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
		If sResult = Windows.Forms.DialogResult.No Then Exit Sub
		For Each lItem As ListViewItem In lstHotfixList.SelectedItems
			lItem.Remove()
		Next
	End Sub

	Private Sub lstHotfixList_MouseDoubleClick(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles lstHotfixList.MouseDoubleClick
		If (e.Button = Windows.Forms.MouseButtons.Left) Then Call onClick_changeState(sender, e)
	End Sub

	Private Sub lstHotfixes_MouseUp(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles lstHotfixList.MouseUp
		If (e.Button <> Windows.Forms.MouseButtons.Right) Then Exit Sub
		Dim lsNode As ListViewItem = lstHotfixList.GetItemAt(e.X, e.Y)
		If lsNode Is Nothing Then Exit Sub
		With mnuRightClick.Items
			.Clear()
			.Add("Change Checking State", My.Resources._16___ChangeState.ToBitmap, AddressOf onClick_changeState)
		End With
		mnuRightClick.Show(lstHotfixList, New Point(e.X, e.Y))
	End Sub

	Private Sub importList()
		Call disableControls(True)
		Dim sMsg As String = ""
		sMsg = sMsg & "Select a plain text file containing the list of hotfixes you" & vbCrLf
		sMsg = sMsg & "want to import.  Make sure each hotfix is on its own line"

		Dim sResult As DialogResult = MessageBox.Show(Me, sMsg, APN, MessageBoxButtons.OKCancel, MessageBoxIcon.Information)
		If sResult = Windows.Forms.DialogResult.Cancel Then
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
					lstHotfixList.BeginUpdate()
					Using sr As StreamReader = New StreamReader(dlgOpenFile.FileName)
						Dim sLine As String
						Do
							sLine = validateKBInput(sr.ReadLine())
							If (sLine IsNot Nothing) Then Call addItem(sLine, "Installed", False, lstHotfixList, vbNullString)
						Loop Until sr.EndOfStream
					End Using
				Catch ex As Exception
					Console.WriteLine("The file could not be read:" & vbCrLf & ex.Message)
				Finally
					Me.Cursor = Cursors.Default
					lstHotfixList.EndUpdate()
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

	Private Function validateKBInput(ByVal sInput As String) As String
		' This just checks that the inputted value starts with 'KB' and is followed by at least 5 numbers.
		' No other checking is done.  KB numbers (particularly from the registry) can contain versions and other add ons.

		If (sInput Is Nothing) Then Return Nothing
		If (sInput.Trim = vbNullString) Then Return Nothing
		If (sInput.ToUpper.StartsWith("KB") = False) Then sInput = "KB" & sInput
		If (sInput.Length < 7) Then Return Nothing ' KB34567 (All KB numbers are larger then 5 numbers.!

		Try
			Dim sKBNumber As String
			If (sInput.Contains("-") = True) Then sKBNumber = sInput.Substring(2, InStr(sInput, "-") - 2) Else sKBNumber = sInput.Substring(2)
			If (IsNumeric(sKBNumber) = False) Then Return Nothing

			' Remove duplicates...
			If (lstHotfixList.Items.Find(sInput, False).Count > 0) Then
				Return Nothing
			End If
			Return sInput.ToUpper
		Catch
			Return Nothing
		End Try
	End Function

	Sub buildTreeInListView()
		Dim cmoItem As ctrlComboBox_Icons.IconComboItem
		Dim iIndent As Integer

		cmoIconComboBox.AddDivider()

		' Check if any servers actually exist...
		Dim iServerCount As Integer = xml_getServerList_FromGroup(sGroupGUID).Count
		If (iServerCount = 0) Then
			cmoItem = New ctrlComboBox_Icons.IconComboItem("No servers exist in group")
			cmoItem.ItemImage = CType(My.Resources.ResourceManager.GetObject("_16___ServerGroup___Nope"), Drawing.Icon)
			cmoItem.Data = "NOSERVERS"
			cmoItem.IndentCount = 0
			cmoItem.IsFolder = True
			cmoIconComboBox.Items.Add(cmoItem)
			Exit Sub
		End If

		' First create the group structure...
		Dim xGroupList As List(Of XmlNode) = xml_getGroupList_FromGroup(sGroupGUID, eDirectionalSearch.ToParents)
		xGroupList.AddRange(xml_getGroupList_FromGroup(sGroupGUID, eDirectionalSearch.ToChildren))
		If (xGroupList Is Nothing) Then Exit Sub
		For Each gItem As XmlNode In xGroupList
			If (gItem.ParentNode.Name = "root") Then
				iIndent = 0
			Else
				iIndent = 0
				For i As Integer = 0 To cmoIconComboBox.Items.Count - 1
					If (cmoIconComboBox.Items(i).Data = gItem.ParentNode.Attributes.ItemOf("guid").Value) Then
						iIndent = cmoIconComboBox.Items(i).IndentCount + 1
					End If
				Next
			End If

			cmoItem = New ctrlComboBox_Icons.IconComboItem(gItem.Attributes.ItemOf("name").Value)
			cmoItem.ItemImage = CType(My.Resources.ResourceManager.GetObject("_16___Group___" & getGroupColour(CInt(gItem.Attributes("colr").Value), False)), Drawing.Icon)
			cmoItem.Data = gItem.Attributes.ItemOf("guid").Value
			cmoItem.IndentCount = iIndent
			cmoItem.IsFolder = True
			If (cmoIconComboBox.Items.Contains(cmoItem) = False) Then cmoIconComboBox.Items.Add(cmoItem)
		Next

		' Next add the servers to the groups...
		For i As Integer = cmoIconComboBox.Items.Count - 1 To 0 Step -1
			Dim lItem As ctrlComboBox_Icons.IconComboItem = cmoIconComboBox.Items(i)

			Dim xServerList As List(Of XmlNode) = xml_getServerList_FromGroup(lItem.Data)
			If (xServerList IsNot Nothing) Then
				xServerList.Reverse()
				For Each sItem As XmlNode In xServerList
					If (sItem.ParentNode.Attributes.ItemOf("guid").Value = lItem.Data) Then
						cmoItem = New ctrlComboBox_Icons.IconComboItem(sItem.Attributes.ItemOf("name").Value.ToLower)
						If (m_UppercaseServerNames = True) Then cmoItem.DisplayText = cmoItem.DisplayText.ToUpper()
						cmoItem.ItemImage = CType(My.Resources.ResourceManager.GetObject("_16___Server"), Drawing.Icon)
						If (xml_LoadServerData(sItem.Attributes.ItemOf("guid").Value) IsNot Nothing) Then
							cmoItem.ItemImage = CType(My.Resources.ResourceManager.GetObject("_16___Server_With_Properties"), Drawing.Icon)
						End If
						cmoItem.Data = sItem.Attributes.ItemOf("guid").Value
						cmoItem.IndentCount = lItem.IndentCount + 1
						cmoIconComboBox.Items.Insert(i + 1, cmoItem)
					End If
				Next
			End If
		Next
	End Sub

	Private Sub cmoIconComboBox_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cmoIconComboBox.SelectedIndexChanged
		If (cmoIconComboBox.SelectedIndex = iCurrSelected) Then Exit Sub
		If ((cmoIconComboBox.SelectedItem.IsDivider = True) Or (cmoIconComboBox.SelectedItem.IsFolder = True)) Then
			cmoIconComboBox.SelectedIndex = iCurrSelected
			Exit Sub
		End If

		If (cmoIconComboBox.SelectedItem.IsEnabled = False) Then
			MessageBox.Show(Me, "This server is currently set to disabled, please choose another", _
				APN, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Exit Sub
		End If

		If (Trim(cmoIconComboBox.Text) = ETF) Then
			Call importList()
			Exit Sub
		End If

		Dim iResult As DialogResult = Windows.Forms.DialogResult.No
		iResult = MessageBox.Show(Me, "Do you want to load the hotfix list from '" & Trim(cmoIconComboBox.Text.ToUpper) & "'.?", _
				APN, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
		If (iResult = Windows.Forms.DialogResult.No) Then
			cmoIconComboBox.SelectedIndex = iCurrSelected
			Exit Sub
		Else
			SplitContainer1.Panel2Collapsed = False
			lstHotfixList.Columns("list").Width = lstHotfixList.Width - 85 - SystemInformation.VerticalScrollBarWidth - 4

			Me.Refresh()
			picIcon.Focus()
			Application.DoEvents()
			Call disableControls(True)

			If (getHotfixList(Trim(cmoIconComboBox.Text)) = False) Then
				cmoIconComboBox.SelectedItem.ItemImage = My.Resources._16___Scan___Unknown
				cmoIconComboBox.SelectedItem.IsEnabled = False
				cmoIconComboBox.Refresh()
			End If
			Call disableControls(False)
			cmdCloseImportList.Visible = True
			cmoIconComboBox.Refresh()
		End If

		iCurrSelected = cmoIconComboBox.SelectedIndex
	End Sub

	Private Function getHotfixList(ByVal sServerName As String) As Boolean
		Me.Cursor = Cursors.WaitCursor
		lstImportList.Items.Clear()

		lstImportList.Columns("date").Width = 0
		lstImportList.Columns("list").Width = lstImportList.Width - 4

		proProgress.Value = 0
		proProgress.Visible = True
		cmdCloseImportList.Visible = False
		lstImportList.Items.Add("WAIT", "Retrieving Hotfixes...", "_16___Connection")
		lstImportList.Items("WAIT").Group = lstImportList.Groups("WAIT")
		lstImportList.Refresh()

		Dim oResult As ManagementObjectCollection = wmiConnect("\\" & sServerName & "\root\cimv2", "SELECT HotFixID, Description, InstalledOn FROM Win32_QuickFixEngineering", 30)
		If (oResult Is Nothing) Then
			lstImportList.Items("WAIT").Remove()
			'lstImportList.Groups.Remove(lstImportList.Groups("WAIT"))
			cmdCloseImportList.Visible = True
			proProgress.Visible = False
			Me.Cursor = Cursors.Default
			Return False
		End If

		proProgress.Maximum = oResult.Count
		lstImportList.BeginUpdate()
		For Each mObj As ManagementObject In oResult
			If (mObj("HotFixID").ToString.StartsWith("KB") = True) Then
				Dim lItem As ListViewItem
				lItem = New ListViewItem(mObj("HotFixID").ToString.ToUpper)
				lItem.ImageKey = "_16___Resource___Hotfix"
				lItem.Name = lItem.Text
				lItem.Group = lstImportList.Groups("Pri")
				lItem.SubItems.Add(":")

				Dim dtValue As String = mObj("InstalledOn").ToString
				If (dtValue <> vbNullString) Then
					If (dtValue.Length = 16) Then
						lItem.SubItems(1).Text = DateTime.FromFileTimeUtc(CLng("&H" & dtValue)).ToString("yyyy/MM/dd")
					Else
						Dim dtDate As DateTime
						If (DateTime.TryParse(dtValue, dtDate) = True) Then lItem.SubItems(1).Text = dtDate.ToString("yyyy/MM/dd")
					End If
					If (lItem.SubItems(1).Text = ":") Then lItem.SubItems(1).Text = ":" & dtValue
				Else
					lItem.SubItems(1).Text = "Unknown"
				End If

				lstImportList.Items.Add(lItem)
			End If
			proProgress.Value = proProgress.Value + 1
		Next
		oResult.Dispose()

		Dim sAdminList As New List(Of String)
		If (bIsAdminMode = True) Then sAdminList = getWindowsUpdateSessionDetails(sServerName, False)
		If (sAdminList IsNot Nothing) Then
			proProgress.Value = 0
			proProgress.Maximum = sAdminList.Count
			For Each lItem As String In sAdminList
				Call addItem(Split(lItem, "|")(0), Split(lItem, "|")(2), False, lstImportList, "Sec")
				proProgress.Value = proProgress.Value + 1
			Next
		End If

		lstImportList.SetGroupState(ListViewGroupState.Collapsible)
		For Each grp As ListViewGroup In lstImportList.Groups
			Call lstImportList.SetGroupFooter(grp)
		Next

		lstImportList.Columns("date").Width = 75
		lstImportList.Columns("list").Width = lstImportList.Width - 75 - SystemInformation.VerticalScrollBarWidth - 4
		lvwL.SortColumn = lstImportList.Columns("date").Index

		lstImportList.EndUpdate()
		lstImportList.Items("WAIT").Remove()
		lstImportList.Groups.Remove(lstImportList.Groups("WAIT"))

		Call cleanHotfixDateList(lstImportList, 1, proProgress)
		cmdCloseImportList.Visible = True
		proProgress.Visible = False
		Me.Cursor = Cursors.Default
		picIcon.Focus()
		Return True
	End Function

	Private Sub addItem(ByVal sItemText As String, ByVal sSubItemText As String, ByVal bEditItem As Boolean, ByVal lvListView As ListView, ByVal sGroup As String)
		If (sItemText <> vbNullString) Then
			If (lvListView.Items.Find(sItemText, False).Count > 0) Then Exit Sub
		End If

		Dim lItem As New ListViewItem(vbNullString)
		If (sItemText <> vbNullString) Then lItem.Text = sItemText.ToUpper ' ToUpper ONLY as "KB" should be in upper-case
		lItem.UseItemStyleForSubItems = False
		lItem.ImageKey = "_16___Resource___Hotfix"
		lItem.Name = sItemText
		lItem.SubItems.Add(sSubItemText)

		If (sSubItemText = "Installed") Then lItem.SubItems(1).ForeColor = getSubItemColour("Installed")
		If (sGroup <> vbNullString) Then lItem.Group = lvListView.Groups(sGroup)

		lvListView.Items.Add(lItem)
		If (bEditItem = True) Then lItem.BeginEdit()
	End Sub

	Private Sub cmdOK_Click(sender As System.Object, e As System.EventArgs) Handles cmdOK.Click
		Dim sResults As New List(Of String)
		iCurrSelected = -1

		For Each lItem As ListViewItem In lstHotfixList.Items
			sResults.Add(lItem.Text & "|" & lItem.SubItems(1).Text)
		Next

		' RESOURCE DUPLICATE CHECK...
		Dim dResult As DialogResult = resourceDuplicationAlert("Hotfix Patch", Nothing, sResults)
		If ((dResult = Windows.Forms.DialogResult.Yes) Or (dResult = Windows.Forms.DialogResult.Cancel)) Then Exit Sub
		If (dResult = Windows.Forms.DialogResult.No) Then
			For Each itm As String In lResourceConflicts
				Dim sAttributes As String = xml_getSpecificItem(Split(itm, "|")(1))
				For Each lItem As ListViewItem In lstHotfixList.Items
					If (lItem.Text = sAttributes.Split("|"c)(0)) Then sResults.Remove(lItem.Text & "|" & lItem.SubItems(1).Text)
				Next
			Next
		End If

		' Settings are saved in calling procedure ('frmMain.onClick_addResource_Hotfix')
		img16.Dispose()
		imgSPACING.Dispose()
		frmMain.m_AddHotfixes = sResults
		Me.Close()
		Me.Dispose()
	End Sub

	Private Sub lnkNote_LinkClicked(sender As System.Object, e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lnkNote.LinkClicked
		frmHelp.sSelectPageByID = "help0022"
		frmHelp.ShowDialog(Me)
	End Sub

	Private Sub lstImportList_ColumnClick(sender As Object, e As System.Windows.Forms.ColumnClickEventArgs) Handles lstImportList.ColumnClick
		If (e.Column = lvwL.SortColumn) Then
			If (lvwL.Order = SortOrder.Ascending) Then
				lvwL.Order = SortOrder.Descending
			Else
				lvwL.Order = SortOrder.Ascending
			End If
		Else
			lvwL.SortColumn = e.Column
			lvwL.Order = SortOrder.Ascending
		End If

		Application.DoEvents()
		Call ShowListViewSortImage(lstImportList, lvwL.SortColumn, lvwL.Order)

		With lstImportList
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

	Private Sub lstImportList_MouseDoubleClick(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles lstImportList.MouseDoubleClick
		Dim lNode As ListViewItem = lstImportList.GetItemAt(e.X, e.Y)
		If (lNode Is Nothing) Then Exit Sub
		addItem(lNode.Text, "Installed", False, lstHotfixList, vbNullString)
	End Sub

	Private Sub onClick_changeState(sender As System.Object, e As System.EventArgs)
		Dim sState As String
		For Each lItem As ListViewItem In lstHotfixList.SelectedItems
			If (lItem.SubItems(1).Text = "Installed") Then sState = "Not Installed" Else sState = "Installed"
			lItem.SubItems(1).Text = sState
			lItem.SubItems(1).ForeColor = getSubItemColour(sState)
		Next
	End Sub

	Private Sub onClick_addHotfix(sender As System.Object, e As System.EventArgs)
		For Each lItem As ListViewItem In lstImportList.SelectedItems
			addItem(lItem.Text, "Installed", False, lstHotfixList, vbNullString)
		Next
	End Sub

	Private Sub onClick_lookupHotfix(sender As System.Object, e As System.EventArgs)
		Dim sKB As String = lstImportList.SelectedItems(0).Text.Replace("KB", "")
		Process.Start("https://" & "support.microsoft.com/kb/" & sKB)
	End Sub

	Private Sub lstImportList_MouseUp(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles lstImportList.MouseUp
		If (e.Button <> Windows.Forms.MouseButtons.Right) Then Exit Sub
		Dim lNode As ListViewItem = lstImportList.GetItemAt(e.X, e.Y)
		If (lNode Is Nothing) Then Exit Sub

		With mnuRightClick.Items
			.Clear()
			.Add("Add Hotfixes", My.Resources._16___Add.ToBitmap, AddressOf onClick_addHotfix)
			If (lstImportList.SelectedItems.Count = 1) Then
				.Item(0).Text = "Add Hotfix"
				.Add("-", Nothing)
				.Add("Lookup Hotfix", My.Resources._16___Scan___Scanning.ToBitmap, AddressOf onClick_lookupHotfix)
			End If
		End With

		mnuRightClick.Show(lstImportList, New Point(e.X, e.Y))
	End Sub

	Private Sub cmdCloseImportList_Click(sender As System.Object, e As System.EventArgs) Handles cmdCloseImportList.Click
		cmdCloseImportList.Visible = False
		lstImportList.Items.Clear()
		SplitContainer1.Panel2Collapsed = True
		lstHotfixList.Columns("list").Width = lstHotfixList.Width - 75 - SystemInformation.VerticalScrollBarWidth - 4
		cmoIconComboBox.SelectedIndex = -1
		iCurrSelected = -1
	End Sub

	Private Sub disableControls(ByRef bDisable As Boolean)
		cmdCloseImportList.Enabled = Not bDisable
		cmoIconComboBox.BackColor = CType(IIf(bDisable, SystemColors.ControlLight, SystemColors.Window), Color)
		cmoIconComboBox.Enabled = Not bDisable
		cmdCancel.Enabled = Not bDisable
		cmdAdd.Enabled = Not bDisable
		cmdDel.Enabled = Not bDisable
		cmdOK.Enabled = Not bDisable
	End Sub
End Class