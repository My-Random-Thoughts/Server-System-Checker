Imports Microsoft.Win32

Public Class frmOptions
	Private Sub frmOptions_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
		For Each c As Control In Controls
			c.Font = frmMain.sysFont
		Next
		lblTitle1.Font = frmMain.sysFontTitle
		lblTitle2.Font = frmMain.sysFontTitle
		lblTitle3.Font = frmMain.sysFontTitle
		lnkHelp.Font = frmMain.sysFontHelp

		lnkHelp.LinkBehavior = LinkBehavior.HoverUnderline

		Dim cItem As ctrlComboBox_Icons.IconComboItem
		cmoServerNames.ItemHeight = 19
		cmoServerNames.DropDownHeight = 192
		With cmoServerNames.Items
			.Clear()
			cItem = New ctrlComboBox_Icons.IconComboItem("Uppercase")
			cItem.ItemImage = My.Resources._16___Case___Upper
			cItem.IndentCount = 0
			.Add(cItem)
			cItem = New ctrlComboBox_Icons.IconComboItem("Lowercase")
			cItem.ItemImage = My.Resources._16___Case___Lower
			cItem.IndentCount = 0
			.Add(cItem)
			cmoServerNames.SelectedIndex = 1
		End With

		cmoGroupIcons.ItemHeight = 19
		cmoGroupIcons.DropDownHeight = 192
		With cmoGroupIcons.Items
			.Clear()
			For i As Integer = 1 To iGroupIconSetCount
				cItem = New ctrlComboBox_Icons.IconComboItem("Set " & i.ToString)
				cItem.ItemImage = CType(My.Resources.ResourceManager.GetObject("_16___Group___" & i.ToString & "___Yellow"), Icon)
				cItem.IndentCount = 0
				.Add(cItem)
			Next i
			cmoGroupIcons.SelectedIndex = 0
		End With

		cmoView.ItemHeight = 19
		cmoView.DropDownHeight = 192
		With cmoView.Items
			.Clear()
			cItem = New ctrlComboBox_Icons.IconComboItem("Details")
			cItem.ItemImage = My.Resources._16___View_Details
			cItem.IndentCount = 0
			.Add(cItem)
			cItem = New ctrlComboBox_Icons.IconComboItem("Tiles")
			cItem.ItemImage = My.Resources._16___View_Tiles
			cItem.IndentCount = 0
			.Add(cItem)
			cmoView.SelectedIndex = 0
		End With

		cmoGroup.ItemHeight = 19
		cmoGroup.DropDownHeight = 192
		With cmoGroup.Items
			.Clear()
			cItem = New ctrlComboBox_Icons.IconComboItem("None")
			cItem.ItemImage = My.Resources._16___GroupBy_None
			cItem.IndentCount = 0
			.Add(cItem)
			cItem = New ctrlComboBox_Icons.IconComboItem("Parent")
			cItem.ItemImage = My.Resources._16___GroupBy_Parent
			cItem.IndentCount = 0
			.Add(cItem)
			cItem = New ctrlComboBox_Icons.IconComboItem("Type")
			cItem.ItemImage = My.Resources._16___GroupBy_Type
			cItem.IndentCount = 0
			.Add(cItem)
			cmoGroup.SelectedIndex = 2
		End With
		cItem = Nothing

		If (bUseVisualStyles = True) Then
			Call SetWindowTheme(cmoGroup.Handle, "Explorer", Nothing)
			Call SetWindowTheme(cmoGroupIcons.Handle, "Explorer", Nothing)
			Call SetWindowTheme(cmoServerNames.Handle, "Explorer", Nothing)
			Call SetWindowTheme(cmoView.Handle, "Explorer", Nothing)
		End If

		Call getCurrentValues()
	End Sub

	Private Sub getCurrentValues()
		chkPingServers.Checked = m_PingServersBeforeScan
		chkShowFullpathHeader.Checked = frmMain.picHeaderBar.Visible
		chkShowGroupCounts.Checked = m_ShowGroupCounts
		chkUseColouredGroups.Checked = m_UseColouredGroups

		cmoView.SelectedIndex = 0
		cmoGroup.SelectedIndex = 2
		cmoGroupIcons.SelectedIndex = 0
		cmoServerNames.SelectedIndex = 0

		Try
			Dim sSettings As List(Of String) = xml_readSettings()
			If ((sSettings Is Nothing) OrElse (sSettings.Count = 0)) Then Exit Sub
			cmoGroupIcons.SelectedIndex = CInt(sSettings.Item(4)) - 1
			If (m_UppercaseServerNames = False) Then cmoServerNames.SelectedIndex = 1 Else cmoServerNames.SelectedIndex = 0
			If (sSettings.Item(1).ToLower = "tile") Then cmoView.SelectedIndex = 1 Else cmoView.SelectedIndex = 0
			Select Case sSettings.Item(0).ToLower
				Case "none" : cmoGroup.SelectedIndex = 0
				Case "parent" : cmoGroup.SelectedIndex = 1
				Case Else : cmoGroup.SelectedIndex = 2
			End Select
		Catch
		End Try
	End Sub

	Private Sub showIconSet(ByVal bColoured As Boolean)
		Dim cPB As PictureBox
		Dim sCol As String
		Dim tt As New ToolTip

		For Each c As Control In floIconSet.Controls : c.Dispose() : Next
		floIconSet.Controls.Clear()
		Application.DoEvents()

		tt.RemoveAll()
		For i As Integer = 0 To iIconColourCount - 1
			cPB = New PictureBox
			sCol = "_16___Group___" & getGroupColour(i, True)
			sCol = sCol.Replace("_" & m_IconGroup & "_", "_" & (CInt(cmoGroupIcons.SelectedIndex.ToString) + 1) & "_")
			If (bColoured = False) Then sCol = sCol.Substring(0, 18) & "Yellow"

			With cPB
				.Size = New Size(16, 16)
				.Margin = New Padding(0, 0, 2, 1)
				.Image = CType(My.Resources.ResourceManager.GetObject(sCol), Icon).ToBitmap
				.Name = sCol.Substring(InStrRev(sCol, "_"))
				.Visible = True
			End With
			tt.SetToolTip(cPB, cPB.Name)
			floIconSet.Controls.Add(cPB)
		Next
	End Sub

	Private Sub cmdResetOptions_Click(sender As System.Object, e As System.EventArgs) Handles cmdResetOptions.Click
		chkPingServers.Checked = True
		chkShowFullpathHeader.Checked = True
		chkShowGroupCounts.Checked = True
		chkUseColouredGroups.Checked = True
		cmoServerNames.SelectedIndex = 1
		cmoGroupIcons.SelectedIndex = 0
		cmoView.SelectedIndex = 0
		cmoGroup.SelectedIndex = 2
	End Sub

	Private Sub cmdCancel_Click(sender As System.Object, e As System.EventArgs) Handles cmdCancel.Click
		Me.Close()
		Me.Dispose()
	End Sub

	Private Sub cmdOK_Click(sender As System.Object, e As System.EventArgs) Handles cmdOK.Click
		Me.Cursor = Cursors.WaitCursor

		' Non-GUI changes...
		m_PingServersBeforeScan = chkPingServers.Checked

		' Group tree changes...
		m_UppercaseServerNames = CType(IIf(cmoServerNames.SelectedIndex = 0, True, False), Boolean)
		m_UseColouredGroups = chkUseColouredGroups.Checked

		' Resource list changes
		m_ShowGroupCounts = chkShowGroupCounts.Checked
		If (cmoView.SelectedIndex = 0) Then
			m_ResourceView = "detail"
			frmMain.lstResources.View = View.Details
		Else
			m_ResourceView = "tile"
			frmMain.lstResources.View = View.Tile
		End If

		m_IconGroup = (cmoGroupIcons.SelectedIndex + 1).ToString
		Call frmMain.loadIconSet()

		Select Case cmoGroup.SelectedIndex
			Case 0 : m_GroupView = "none" : frmMain.lstResources.ShowGroups = False
			Case 1 : m_GroupView = "parent" : Call doGroupListView(frmMain.lstResources, "Defined", True, m_ShowGroupCounts)
			Case 2 : m_GroupView = "type" : Call doGroupListView(frmMain.lstResources, "Type", False, m_ShowGroupCounts)
		End Select

		' Make the changes...
		Call doRedrawTreeNodes(frmMain.tvwServerList, m_UppercaseServerNames)
		If (chkShowFullpathHeader.Checked <> frmMain.picHeaderBar.Visible) Then Call frmMain.ShowHideFullPathHeader()
		Call frmMain.drawFullPathHeader(frmMain.tvwServerList.SelectedNode)
		Call frmMain.tvwServerList_NodeMouseClick(Nothing, Nothing)

		Call xml_saveSettings()

		Me.Cursor = Cursors.Default
		Me.Close()
		Me.Dispose()
	End Sub

	Private Sub lnkHelp_LinkClicked(sender As System.Object, e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lnkHelp.LinkClicked
		frmHelp.sSelectPageByID = "help0013"
		frmHelp.ShowDialog(Me)
	End Sub

	Private Sub cmoGroupIcons_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cmoGroupIcons.SelectedIndexChanged
		Call showIconSet(chkUseColouredGroups.Checked)
	End Sub

	Private Sub chkUseColouredGroups_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkUseColouredGroups.CheckedChanged
		If (Me.Visible = False) Then Exit Sub
		showIconSet(chkUseColouredGroups.Checked)
	End Sub
End Class