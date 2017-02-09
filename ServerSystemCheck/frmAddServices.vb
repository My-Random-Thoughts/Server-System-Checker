Option Explicit On
Imports System.ServiceProcess
Imports System.Management
Imports System.Xml
Imports Microsoft.Win32

Public Class frmAddServices
	Public sServerName As String = "."
	Public sGroupGUID As String
	Private bLoading As Boolean
	Private lvwColumnSorter As ListViewColumnSorter
	Private iCurrSelected As Integer = 0	' Index of currently selected combobox item
	Private img16 As New ImageList
	Private imgSPACING As New ImageList
	Private lstHidden As New ListView
	Private mnuService As ContextMenuStrip

	Private Sub frmServicesList_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
		For Each c As Control In Controls
			c.Font = frmMain.sysFont
		Next
		lblTitle.Font = frmMain.sysFontTitle
		lnkHelp.Font = frmMain.sysFontHelp

		lnkHelp.LinkBehavior = LinkBehavior.HoverUnderline
		picIcon.Image = My.Resources._48___Services.ToBitmap
		If (sServerName = ".") Then sServerName = LH

		With img16
			.ImageSize = New Size(16, 16)
			.ColorDepth = ColorDepth.Depth32Bit
			With .Images
				.Clear()
				.Add("_16___Resource___Services", My.Resources._16___Resource___Services)
				.Add("_16___Connection", My.Resources._16___Connection)
			End With
		End With

		With imgSPACING
			.ImageSize = New Size(iListViewIconWidth, iListViewLineHeight)
			.ColorDepth = ColorDepth.Depth32Bit
			For i As Integer = 0 To img16.Images.Count - 1
				.Images.Add(img16.Images.Keys(i), ResizeImage(img16.Images(i), .ImageSize, True))
			Next
		End With

		bLoading = True
		lvwColumnSorter = New ListViewColumnSorter()
		lvwColumnSorter.Order = SortOrder.Ascending
		lvwColumnSorter.SortColumn = 0

		With lstHidden
			.Items.Clear()
			.Columns.Clear()
			.View = View.Details
			.SmallImageList = imgSPACING
			With .Columns
				.Add("a")	' Display Name
				.Add("b")	' Current State
				.Add("c")	' Start Mode
				.Add("d")	' Start As User Name
			End With
			.Visible = False
		End With

		With lstServices
			.Items.Clear()
			.Columns.Clear()
			.FullRowSelect = True
			.View = View.Details
			.SmallImageList = imgSPACING
			.ListViewItemSorter = lvwColumnSorter
			.Columns.Add("a")
		End With

		Call ShowListViewSortImage(lstServices, lvwColumnSorter.SortColumn, lvwColumnSorter.Order)

		Dim cItem As ctrlComboBox_Icons.IconComboItem
		With cmoIconComboBox
			.ItemHeight = 19
			.DropDownHeight = 192
			With .Items
				.Clear()
				cItem = New ctrlComboBox_Icons.IconComboItem(LH)
				cItem.ItemImage = My.Resources._16___Resource___Drive
				cItem.IndentCount = 0
				.Add(cItem)
			End With
			.SelectedIndex = 0
			If (m_NetworkAvailability = True) Then Call buildTreeInListView()
		End With

		With cmoMissingService
			.ItemHeight = 19
			With .Items
				.Clear()
				cItem = New ctrlComboBox_Icons.IconComboItem("OK (Ignore)")
				cItem.ItemImage = CType(My.Resources.ResourceManager.GetObject("_16___Scan___Pass"), Drawing.Icon)
				.Add(cItem)

				cItem = New ctrlComboBox_Icons.IconComboItem("Warning")
				cItem.ItemImage = CType(My.Resources.ResourceManager.GetObject("_16___Eventlog___Warning"), Drawing.Icon)
				.Add(cItem)

				cItem = New ctrlComboBox_Icons.IconComboItem("Fail")
				cItem.ItemImage = CType(My.Resources.ResourceManager.GetObject("_16___Scan___Failed"), Drawing.Icon)
				.Add(cItem)
			End With
			.SelectedIndex = 0
		End With
		cItem = Nothing

		If (bUseVisualStyles = True) Then
			Call SetWindowTheme(lstServices.Handle, "Explorer", Nothing)
		End If

		iCurrSelected = 0
		cmdShowFullDetails.Text = "<<  Hide Details"
		Call cmdShowFullDetails_Click(Nothing, Nothing)

		Me.Visible = True
		Application.DoEvents()
		Call getServiceList(LH)
		bLoading = False
	End Sub

	Private Sub buildTreeInListView()
		Dim cmoItem As ctrlComboBox_Icons.IconComboItem
		Dim iIndent As Integer

		' Localhost already added, and more than one server exists, so add dividing bar...
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
						If m_UppercaseServerNames = True Then cmoItem.DisplayText = cmoItem.DisplayText.ToUpper()
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

	Private Function getServiceList(ByVal sServerName As String) As Boolean
		Dim bSkipRegistryCheck As Boolean = False
		Dim scService() As ServiceController = Nothing
		Dim lItem As ListViewItem

		Me.Cursor = Cursors.WaitCursor
		Application.DoEvents()

		lstHidden.Items.Clear()
		With lstServices
			.Items.Clear()
			.Items.Add("WAIT", "Please wait, retrieving services list...", "_16___Connection")
			.Refresh()
		End With

		Try
			Dim oResult As ManagementObjectCollection = wmiConnect("\\" & sServerName & "\root\cimv2", "SELECT * FROM Win32_Service", 15)
			If (oResult Is Nothing) Then Return False ' Will still run "Finally" code below

			If (oResult.Count > 0) Then
				Dim rKey As RegistryKey = RegistryKey.OpenRemoteBaseKey(RegistryHive.LocalMachine, sServerName)
				If (rKey Is Nothing) Then bSkipRegistryCheck = True

				For Each obj In oResult
					Dim sStartMode As String = obj("StartMode").ToString
					If (sStartMode = "Auto") Then sStartMode = "Automatic"

					If (bSkipRegistryCheck = False) Then
						Try
							Dim rSub As RegistryKey = rKey.OpenSubKey("SYSTEM\CurrentControlSet\Services\" & obj("Name").ToString)
							If (rSub IsNot Nothing) Then
								If ((rSub.GetValue("DelayedAutoStart") IsNot Nothing) AndAlso _
								 (rSub.GetValue("DelayedAutoStart").ToString = "1") AndAlso _
								 (sStartMode = "Automatic")) Then sStartMode = "Automatic (Delayed Start)"
							End If
						Catch
							bSkipRegistryCheck = True
						End Try
					End If

					lItem = lstHidden.Items.Add(obj("DisplayName").ToString, "_16___Resource___Services")
					lItem.Name = lItem.Text
					lItem.UseItemStyleForSubItems = False

					Try
						If (obj("Description") IsNot Nothing) Then lItem.Tag = obj("Description").ToString Else lItem.Tag = "(No Description)"
					Catch
						lItem.Tag = "(No Description - Failed)"
					End Try

					lItem.SubItems.Add(obj("State").ToString, getSubItemColour(obj("State").ToString), lstServices.BackColor, lstServices.Font)
					lItem.SubItems.Add(sStartMode)
					lItem.SubItems.Add(obj("StartName").ToString.ToLower)
				Next
			End If

			Call displayServices()

			oResult.Dispose()
			lstServices.Sort()
			Return True
			MsgBox("TRUE")
		Catch
			Return False
			MsgBox("FALSE")
		Finally
			lstServices.Items("WAIT").Remove()
			Me.Cursor = Cursors.Default
		End Try
	End Function

	Private Sub displayServices()
		Dim lItem As ListViewItem
		Dim lFind As ListViewItem()

		lstServices.BeginUpdate()
		For Each lHidden As ListViewItem In lstHidden.Items

			lFind = lstServices.Items.Find(lHidden.Text, False)
			If ((lFind IsNot Nothing) AndAlso (lFind.Count > 0)) Then
				' SELECT Existing Item
				lItem = lFind(0)
			Else
				' ADD New Item
				lItem = lstServices.Items.Add(lHidden.Text, lHidden.ImageKey)
			End If

			lItem.Tag = lHidden.Tag
			lItem.Name = lHidden.Text
			lItem.UseItemStyleForSubItems = False

			If (cmdShowFullDetails.Text = "<<  Hide Details") Then
				Try
					lItem.SubItems.Add(lHidden.SubItems(1).Text, lHidden.SubItems(1).ForeColor, lHidden.BackColor, lHidden.Font)
					lItem.SubItems.Add(lHidden.SubItems(2).Text)
					lItem.SubItems.Add(lHidden.SubItems(3).Text)
				Catch
				End Try
			Else
				Try
					lItem.SubItems.RemoveAt(3)
					lItem.SubItems.RemoveAt(2)
					lItem.SubItems.RemoveAt(1)
				Catch
				End Try
			End If

			lItem = Nothing
			lFind = Nothing
		Next
		lstServices.EndUpdate()
	End Sub

	Private Sub lstServices_ColumnClick(sender As Object, e As System.Windows.Forms.ColumnClickEventArgs) Handles lstServices.ColumnClick
		If (e.Column = lvwColumnSorter.SortColumn) Then
			If (lvwColumnSorter.Order = SortOrder.Ascending) Then
				lvwColumnSorter.Order = SortOrder.Descending
			Else
				lvwColumnSorter.Order = SortOrder.Ascending
			End If
		Else
			lvwColumnSorter.SortColumn = e.Column
			lvwColumnSorter.Order = SortOrder.Ascending
		End If

		lstServices.Sort()
		Application.DoEvents()
		Call ShowListViewSortImage(lstServices, lvwColumnSorter.SortColumn, lvwColumnSorter.Order)
	End Sub

	Private Sub cmdOK_Click(sender As System.Object, e As System.EventArgs) Handles cmdOK.Click
		Dim sResults As New List(Of String)
		frmMain.m_AddServices = Nothing
		For Each lItem As ListViewItem In lstServices.SelectedItems
			Dim lFind() As ListViewItem = lstHidden.Items.Find(lItem.Text, False)
			sResults.Add(lItem.Text & "|" & lFind(0).SubItems(1).Text & " (" & cmoMissingService.Text.Trim.Replace(" (Ignore)", "") & ")")
		Next

		' RESOURCE DUPLICATE CHECK...
		Dim dResult As DialogResult = resourceDuplicationAlert("Windows Service", Nothing, sResults)
		If ((dResult = Windows.Forms.DialogResult.Yes) Or (dResult = Windows.Forms.DialogResult.Cancel)) Then Exit Sub
		If (dResult = Windows.Forms.DialogResult.No) Then
			For Each itm As String In lResourceConflicts
				Dim sAttributes As String = xml_getSpecificItem(Split(itm, "|")(1))
				For Each lItem As ListViewItem In lstServices.Items
					If (lItem.Text = sAttributes.Split("|"c)(0)) Then
						Dim lFind() As ListViewItem = lstHidden.Items.Find(lItem.Text, False)
						sResults.Remove(lItem.Text & "|" & lFind(0).SubItems(1).Text & " (" & cmoMissingService.Text.Trim.Replace(" (Ignore)", "") & ")")
					End If
				Next
			Next
		End If

		' Settings are saved in calling procedure ('frmMain.onClick_addResource_Services')
		img16.Dispose()
		imgSPACING.Dispose()
		lstHidden.Dispose()
		lResourceConflicts = Nothing
		frmMain.m_AddServices = sResults
		Me.Close()
		Me.Dispose()
	End Sub

	Private Sub cmdCancel_Click(sender As System.Object, e As System.EventArgs) Handles cmdCancel.Click
		img16.Dispose()
		imgSPACING.Dispose()
		lstHidden.Dispose()
		frmMain.m_AddServices = Nothing
		Me.Close()
		Me.Dispose()
	End Sub

	Private Sub cmoIconComboBox_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cmoIconComboBox.SelectedIndexChanged
		If (bLoading = True) Then Exit Sub
		If (cmoIconComboBox.SelectedIndex = iCurrSelected) Then Exit Sub
		If ((cmoIconComboBox.SelectedItem.IsDivider = True) Or (cmoIconComboBox.SelectedItem.IsFolder = True)) Then
			cmoIconComboBox.SelectedIndex = iCurrSelected
			Exit Sub
		End If

		If (cmoIconComboBox.SelectedItem.IsEnabled = False) Then
			MessageBox.Show(Me, "This server is currently set to disabled, please choose another", APN, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Exit Sub
		End If


		Dim iResult As DialogResult = MessageBox.Show(Me, "Do you want to load the services list from " & vbcrlf & "'" & Trim(cmoIconComboBox.Text.ToUpper) & "'.?", _
													  APN, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
		If (iResult = Windows.Forms.DialogResult.No) Then
			cmoIconComboBox.SelectedIndex = iCurrSelected
			Exit Sub
		Else
			If (getServiceList(Trim(cmoIconComboBox.Text)) = False) Then
				cmoIconComboBox.SelectedItem.ItemImage = My.Resources._16___Scan___Unknown
				cmoIconComboBox.SelectedItem.IsEnabled = False
				cmoIconComboBox.Refresh()
			End If
			iCurrSelected = cmoIconComboBox.SelectedIndex
		End If
	End Sub

	Private Sub lnkHelp_LinkClicked(sender As System.Object, e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lnkHelp.LinkClicked
		frmHelp.sSelectPageByID = "help0007"
		frmHelp.ShowDialog(Me)
	End Sub

	Private Sub lstServices_MouseDoubleClick(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles lstServices.MouseDoubleClick
		Call cmdOK_Click(sender, e)
	End Sub

	Private Sub cmdShowFullDetails_Click(sender As System.Object, e As System.EventArgs) Handles cmdShowFullDetails.Click
		If (cmdShowFullDetails.Text = "More Details  >>") Then
			cmdShowFullDetails.Text = "<<  Hide Details"
			Me.Width = 800
			Me.CenterToParent()

			With lstServices.Columns
				.Clear()
				.Add("Service Name").Width = lstServices.Width - 350 - SystemInformation.VerticalScrollBarWidth - 4
				.Add("State").Width = 75
				.Add("Start Mode").Width = 100
				.Add("Logon Account").Width = 175
			End With
		Else
			cmdShowFullDetails.Text = "More Details  >>"
			Me.Width = 450
			Me.CenterToParent()

			lstServices.Columns.Clear()
			lstServices.Columns.Add("Service Name").Width = lstServices.Width - SystemInformation.VerticalScrollBarWidth - 4
		End If

		Me.Refresh()
		lstServices.Focus()
		Application.DoEvents()
		Call displayServices()
	End Sub

	Private Sub lstServices_MouseUp(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles lstServices.MouseUp
		If (lstServices.SelectedItems.Count <> 1) Then Exit Sub
		mnuService = New ContextMenuStrip
		With mnuService.Items
			.Clear()
			.Add("Show Description", My.Resources._16___Properties.ToBitmap, AddressOf onClick_ShowDescription)
		End With
		mnuService.Show(lstServices, e.Location)
	End Sub

	Private Sub onClick_ShowDescription(sender As System.Object, e As System.EventArgs)
		MessageBox.Show(Me, lstServices.SelectedItems(0).Tag.ToString, " " & lstServices.SelectedItems(0).Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
		mnuService.Dispose()
	End Sub
End Class