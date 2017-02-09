Imports System.Xml
Imports Microsoft.Win32

Public Class frmAddRegistrySearch
	Private iCurrSelected As Integer = -1	' Index of currently selected combo-box item
	Private iTimerCount As Integer = 0
	Public sGroupGUID As String
	Public sCurrentPath As String
	Private tt As New ToolTip
	Private img16 As New ImageList
	Private WithEvents timTimer As New Timer
	Private bFirstLoad As Boolean = True

	Private Sub frmAddRegistrySearch_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
		For Each c As Control In Controls
			c.Font = frmMain.sysFont
		Next
		lblTitle.Font = frmMain.sysFontTitle
		lnkHelp.Font = frmMain.sysFontHelp
		lnkNote.Font = frmMain.sysFontHelp
		lnkNote.Top = lnkHelp.Bottom + 1

		lnkHelp.LinkBehavior = LinkBehavior.HoverUnderline
		lnkNote.LinkBehavior = LinkBehavior.HoverUnderline
		picIcon.Image = My.Resources._48___Registry___Search.ToBitmap

		With img16
			.ImageSize = New Size(16, 16)
			.ColorDepth = ColorDepth.Depth32Bit
			With .Images
				.Clear()
				.Add("_16___Group___" & m_IconGroup & "___Yellow", CType(My.Resources.ResourceManager.GetObject("_16___Group___" & m_IconGroup & "___Yellow"), Icon))
				.Add("_16___Properties", My.Resources._16___Properties)
				.Add("_16___Group___" & m_IconGroup & "____Denied", CType(My.Resources.ResourceManager.GetObject("_16___Group___" & m_IconGroup & "____Denied"), Icon))
			End With
		End With

		With timTimer
			.Enabled = False
			.Interval = 1000	' 1 second
		End With
		lblToolTip.Visible = False

		With tvwRegistryTree
			.Nodes.Clear()
			.ImageList = img16
			.ShowPlusMinus = True
			.ShowLines = True
			.ShowRootLines = True
			.ItemHeight = 18
		End With
		lblFullPath.Text = "HKEY_LOCAL_MACHINE"
		tt.SetToolTip(lblFullPath, lblFullPath.Text)

		cmdLocatePath.Visible = True
		cmdLocatePath.Image = My.Resources._16___Locate_Path.ToBitmap
		tt.SetToolTip(cmdLocatePath, "Try to browse entered path...")
		If (sCurrentPath = "\\!") Then cmdLocatePath.Visible = False

		bFirstLoad = True
		Dim cItem As ctrlComboBox_Icons.IconComboItem
		cmoIconComboBox.ItemHeight = 19
		cmoIconComboBox.DropDownHeight = 192
		With cmoIconComboBox.Items
			.Clear()
			cItem = New ctrlComboBox_Icons.IconComboItem(LH)	' Local Host
			cItem.ItemImage = My.Resources._16___Resource___Drive
			cItem.IndentCount = 0
			.Add(cItem)

			If (m_NetworkAvailability = True) Then Call buildTreeInListView()
			cmoIconComboBox.SelectedIndex = 0
		End With
		iCurrSelected = 0

		' Try to browse to current path...
		Call cmdLocatePath_Click(sender, e)
	End Sub

	Private Sub buildTreeInListView()
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


		Dim iResult As DialogResult = Windows.Forms.DialogResult.Yes
		If (bFirstLoad = False) Then
			iResult = MessageBox.Show(Me, "Do you want to load the registry data from '" & Trim(cmoIconComboBox.Text.ToUpper) & "'.?", _
			  APN, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
		End If
		bFirstLoad = False
		If (iResult = Windows.Forms.DialogResult.No) Then
			cmoIconComboBox.SelectedIndex = iCurrSelected
			Exit Sub
		Else
			tvwRegistryTree.Nodes.Clear()
			Application.DoEvents()
			Me.Refresh()

			Dim sTestServer() As List(Of String) = LoadRegistryKeys(cmoIconComboBox.Text, "HKEY_LOCAL_MACHINE")
			If (sTestServer Is Nothing) Then
				cmoIconComboBox.SelectedItem.ItemImage = My.Resources._16___Scan___Unknown
				cmoIconComboBox.SelectedItem.IsEnabled = False
				cmoIconComboBox.Refresh()
			Else
				tvwRegistryTree.Nodes.Add("HKEY_LOCAL_MACHINE", "HKEY_LOCAL_MACHINE", 0)
				Dim t As New TreeViewEventArgs(tvwRegistryTree.Nodes("HKEY_LOCAL_MACHINE"))
				Call tvwRegistryTree_AfterSelect(Nothing, t)
				tvwRegistryTree.Nodes("HKEY_LOCAL_MACHINE").Expand()
			End If
			iCurrSelected = cmoIconComboBox.SelectedIndex
		End If
	End Sub

	Private Function LoadRegistryKeys(ByVal sServer As String, ByVal sParentKey As String) As List(Of String)()
		Dim rKey As RegistryKey
		Dim rSub As RegistryKey
		Dim rGSK As List(Of String)
		Dim rGVN As List(Of String)
		Dim sResult(2) As List(Of String)
		sResult(0) = New List(Of String)
		sResult(1) = New List(Of String)
		sResult(2) = New List(Of String)

		Try
			Me.Cursor = Cursors.WaitCursor
			rKey = RegistryKey.OpenRemoteBaseKey(RegistryHive.LocalMachine, sServer)
			If (rKey IsNot Nothing) Then
				Try
					If (sParentKey = "HKEY_LOCAL_MACHINE") Then
						rSub = rKey
					Else
						sParentKey = sParentKey.Replace("HKEY_LOCAL_MACHINE\", vbNullString)
						rSub = rKey.OpenSubKey(sParentKey)
					End If

					' Get and add sub keys...
					rGSK = rSub.GetSubKeyNames.ToList
					rGSK.Sort()
					sResult(0).AddRange(rGSK)

					' Get and add value names...
					rGVN = rSub.GetValueNames.ToList
					rGVN.Sort()
					sResult(1).AddRange(rGVN)

				Catch ex As Exception
					sResult(2).Add(ex.Message)
				End Try
			End If
			Return sResult
		Catch
			Return Nothing
		Finally
			Me.Cursor = Cursors.Default
		End Try
	End Function

	Private Sub tvwRegistryTree_AfterSelect(sender As System.Object, e As System.Windows.Forms.TreeViewEventArgs) Handles tvwRegistryTree.AfterSelect
		cmdOK.Enabled = False
		lblFullPath.Text = CompactString(e.Node.FullPath, lblFullPath.Width, lblFullPath.Font)
		tt.SetToolTip(lblFullPath, prettyPath(e.Node.FullPath, 0))
		If (e.Node.ImageIndex = 1) Then
			tt.SetToolTip(lblFullPath, prettyPath(e.Node.Parent.FullPath, 0))
			lblFullPath.Text = CompactString(e.Node.Parent.FullPath, lblFullPath.Width, lblFullPath.Font)
			If (getValueData(e.Node.Parent.FullPath, e.Node.Text) IsNot Nothing) Then cmdOK.Enabled = True
			Exit Sub
		End If
		If (e.Node.ImageIndex = 2) Then Exit Sub ' Don't re-scan failed folders
		If (e.Node.GetNodeCount(False) > 0) Then
			If (chkAutoExpand.Checked = True) Then
				e.Node.Expand()
				e.Node.EnsureVisible()
			End If
			Exit Sub ' Don't re-scan existing folders
		End If

		If (lblToolTip.Visible = True) Then lblToolTip.Visible = False

		Dim sChildren() As List(Of String) = LoadRegistryKeys(cmoIconComboBox.Text, e.Node.FullPath)
		If (sChildren(2).Count > 0) Then
			' ERROR
			e.Node.ImageIndex = 2
			e.Node.SelectedImageIndex = 2
			lblToolTip.Visible = True
			timTimer.Enabled = True

		Else
			' FOLDERS
			If (sChildren(0).Count > 0) Then
				For Each sItem As String In sChildren(0)
					e.Node.Nodes.Add(e.Node.FullPath & tvwRegistryTree.PathSeparator & sItem, sItem, 0, 0)
				Next
			End If

			' KEYS
			If (sChildren(1).Count > 0) Then
				For Each sItem As String In sChildren(1)
					If (sItem = vbNullString) Then sItem = "(Default)"
					e.Node.Nodes.Add(e.Node.FullPath & tvwRegistryTree.PathSeparator & sItem, sItem, 1, 1)
				Next
			End If

			If (chkAutoExpand.Checked = True) Then
				e.Node.Expand()
				e.Node.EnsureVisible()
			End If
		End If
	End Sub

	Private Function getValueData(ByVal sKeyPath As String, ByVal sKeyName As String) As List(Of String)
		Dim rKey As RegistryKey
		Dim rOSK As RegistryKey
		Dim sResult As New List(Of String)

		If sKeyName = "(Default)" Then sKeyName = vbNullString
		rKey = RegistryKey.OpenRemoteBaseKey(RegistryHive.LocalMachine, cmoIconComboBox.Text)
		If (rKey IsNot Nothing) Then
			Try
				If sKeyPath.Contains("\") Then sKeyPath = sKeyPath.Replace("HKEY_LOCAL_MACHINE\", vbNullString)
				rOSK = rKey.OpenSubKey(sKeyPath, False)

				Dim sVAL As String = vbNullString
				Dim sGVK As String = rOSK.GetValueKind(sKeyName).ToString
				Select Case sGVK.ToLower
					Case "binary" : sVAL = "REG_BINARY"
					Case "string" : sVAL = "REG_SZ"
					Case "dword" : sVAL = "REG_DWORD"
					Case "qword" : sVAL = "REG_QWORD"
					Case "multistring" : sVAL = "REG_MULTI_SZ"
					Case "expandstring" : sVAL = "REG_EXPAND_SZ"
					Case Else : sVAL = "Unknown"
				End Select

				If (sVAL <> "Unknown") Then
					Dim oVAL As Object = rOSK.GetValue(sKeyName, vbNullString, RegistryValueOptions.DoNotExpandEnvironmentNames)
					Dim rVAL As String = readRegistryValue(oVAL)
					sResult.Add(rVAL)
					sResult.Add(sVAL)
					Return sResult
				End If
			Catch ex As Exception
				Return Nothing
			End Try
		End If
		Return Nothing
	End Function

	Private Sub cmdOK_Click(sender As System.Object, e As System.EventArgs) Handles cmdOK.Click
		If (tvwRegistryTree.SelectedNode.ImageIndex <> 1) Then Exit Sub

		Dim sSelected As TreeNode = tvwRegistryTree.SelectedNode
		Dim sResult As List(Of String) = getValueData(sSelected.Parent.FullPath, sSelected.Text)

		With frmAddRegistry
			Dim sParentPath As String = sSelected.Parent.FullPath
			If (sParentPath.Contains("\")) Then sParentPath = sParentPath.Replace("HKEY_LOCAL_MACHINE\", vbNullString)
			.txtRegistryKey.Text = sParentPath
			.txtValueName.Text = tvwRegistryTree.SelectedNode.Text
			If (sResult IsNot Nothing) Then
				.txtValueData.Text = sResult(0)
				.cmoValueType.Text = sResult(1)
			End If
		End With
		Call cmdCancel_Click(sender, e)
	End Sub

	Private Sub cmdCancel_Click(sender As System.Object, e As System.EventArgs) Handles cmdCancel.Click
		img16.Dispose()
		Me.Close()
		Me.Dispose()
	End Sub

	Private Sub lnkHelp_LinkClicked(sender As System.Object, e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lnkHelp.LinkClicked
		frmHelp.sSelectPageByID = "help0023"
		frmHelp.ShowDialog(Me)
	End Sub

	Private Sub timTimer_Tick(sender As System.Object, e As System.EventArgs)
		iTimerCount = iTimerCount + 1
		If iTimerCount > 3 Then
			iTimerCount = 0
			lblToolTip.Visible = False
			timTimer.Enabled = False
		End If
	End Sub

	Private Sub lnkNote_LinkClicked(sender As System.Object, e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lnkNote.LinkClicked
		frmHelp.sSelectPageByID = "help0022"
		frmHelp.ShowDialog(Me)
	End Sub

	Private Sub tvwRegistryTree_KeyUp(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles tvwRegistryTree.KeyUp
		If (e.KeyValue = Keys.Enter) Then
			e.SuppressKeyPress = True
			Call cmdCancel_Click(sender, Nothing)
		End If
	End Sub

	Private Sub cmdLocatePath_Click(sender As System.Object, e As System.EventArgs) Handles cmdLocatePath.Click
		If ((sCurrentPath IsNot Nothing) AndAlso (sCurrentPath <> vbNullString)) Then
			If (sCurrentPath.StartsWith("HKEY_LOCAL_MACHINE") = False) Then sCurrentPath = "HKEY_LOCAL_MACHINE\" & sCurrentPath
			sCurrentPath = sCurrentPath.Replace("\\", "\")

			Dim sPathSplit() As String = Split(sCurrentPath, "\")
			Dim sParent As String = sPathSplit(0)
			For Each sPath As String In sPathSplit
				Dim lFound() As TreeNode = tvwRegistryTree.Nodes.Find(sParent, True)
				If (lFound.Count > 0) Then
					lFound(0).EnsureVisible()
					tvwRegistryTree.SelectedNode = lFound(0)
					lFound(0).Expand()
				End If
				If (sParent <> sPath) Then sParent = sParent & tvwRegistryTree.PathSeparator & sPath
			Next
			tvwRegistryTree.Focus()
		End If
	End Sub
End Class