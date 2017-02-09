Option Explicit On

Imports System.Xml
Imports System.Management
Imports System.Text

Public Class frmAddWMIQuery
	Public sGroupGUID As String
	Public lEditValues As ListViewItem				' Are we EDITING an existing item.?
	Public lWhereBuilderItem As ListViewItem		' Comes FROM the frmAddWMIQueryWhereBuilder form
	Public bTestPassesOK As Boolean
	Private iCurrSelected As Integer = -1			' Index of currently selected combobox item
	Private oResult As ManagementScope
	Private bSelectedButton As Button
	Private bEditingProperty As Boolean
	Private mnuProperties As New ContextMenuStrip
	Private mnuDeleteEdit As New ContextMenuStrip
	Private sQueryString As String

	Private Sub frmAddWMIQuery_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
		For Each c As Control In Controls
			c.Font = frmMain.sysFont
		Next
		lblTitle.Font = frmMain.sysFontTitle
		lblClass.Font = frmMain.sysFontBold
		lnkHelp.Font = frmMain.sysFontHelp
		lnkNote.Font = frmMain.sysFontHelp
		lnkNote.Top = lnkHelp.Bottom + 1

		picIcon.Image = My.Resources._48___WMIQuery.ToBitmap

		lnkHelp.LinkBehavior = LinkBehavior.HoverUnderline
		lnkNote.LinkBehavior = LinkBehavior.HoverUnderline
		lnkExpandResults.LinkBehavior = LinkBehavior.HoverUnderline

		lWhereBuilderItem = Nothing
		If (lEditValues IsNot Nothing) Then lblTitle.Text = "Edit Existing" Else lblTitle.Text = "Add New"
		lblTitle.Text = lblTitle.Text & " WMI Query"
		Me.Text = " " & lblTitle.Text

		Dim imgSPACING As New ImageList
		With imgSPACING
			.ImageSize = New Size(1, 21)
			.ColorDepth = ColorDepth.Depth32Bit
			.Images.Add("_16___Scan___Failed", ResizeImage(My.Resources._16___Scan___Failed.ToBitmap, New Size(16, 21), True))
		End With

		With lstWhereList
			.Clear()
			.View = View.Details
			.SmallImageList = imgSPACING
			.HeaderStyle = ColumnHeaderStyle.None
			.FullRowSelect = True
			.HideSelection = True
			With .Columns
				.Clear()
				.Add("A", 125)	
				.Add("B", 35, HorizontalAlignment.Center)	' Operator
				.Add("C", lstWhereList.Width - 220 - SystemInformation.VerticalScrollBarWidth - 4)	' Value
				.Add("D", 40)	' AND / OR
				.Add("E", 20)	' Red 'X' Icon
			End With
		End With

		Dim cItem As ctrlComboBox_Icons.IconComboItem
		With cmoIconComboBox
			.ItemHeight = 19
			.DropDownHeight = 192
			With .Items
				.Clear()
				cItem = New ctrlComboBox_Icons.IconComboItem(LH)	' Local Host
				cItem.ItemImage = My.Resources._16___Resource___Drive
				cItem.IndentCount = 0
				.Add(cItem)
				cmoIconComboBox.SelectedIndex = 0
			End With
		End With
		If (m_NetworkAvailability = True) Then Call buildTreeInListView()

		With cmoQueryResult
			.ItemHeight = 19
			.Items.Clear()
		End With

		With mnuDeleteEdit
			.Items.Clear()
			.Items.Add("Change Property", My.Resources._16___ChangeState.ToBitmap, AddressOf onClick_EditProperty)
			.Items.Add("Delete Property", My.Resources._16___Remove.ToBitmap, AddressOf onClick_DeleteProperty)
			.Items.Add("-")
			.Items.Add("Clear All Properties", My.Resources._16___Scan___Failed.ToBitmap, AddressOf onClick_ClearAllProperty)
		End With

		With floProperties
			.Controls.Clear()
			.AutoScroll = True
			.HorizontalScroll.Visible = False
		End With

		With cmoWhereAndOr
			.Left = (lstWhereList.Left + lstWhereList.Width) - 58 - SystemInformation.VerticalScrollBarWidth - 4
			.Width = 58
			With .Items
				.Clear()
				.Add("AND")
				.Add("OR")
			End With
			.Text = "AND"
		End With

		With cmoClasses
			.Enabled = False
			.Items.Clear()
			.DropDownHeight = ((.Font.Height + 1) * 10)
			.DropDownWidth = .Width
		End With

		lblClass.Text = "(Select from 'Classes' above)"
		lblPropertiesCount.Text = "- Properties"
		iCurrSelected = -1

		If (lEditValues IsNot Nothing) Then
			Call parseEdititem()
		Else
			cmdOK.Enabled = False
			lstWhereList.Enabled = False
			cmoWhereAndOr.Enabled = False
			Call enableControls(False)
			Call showResults(False)
		End If

		cmdTestQuery.Enabled = bTestPassesOK
		cmdShowQuery.Enabled = bTestPassesOK

		Dim iSizingValue As Integer = 1
		If (bUseVisualStyles = True) Then
			Call SetWindowTheme(lstWhereList.Handle, "Explorer", Nothing)
			GroupBox1.Height = 7 : GroupBox2.Height = 7
		Else
			iSizingValue = 2
			GroupBox1.Height = 6 : GroupBox2.Height = 6
		End If

		With lstSelectContainer
			floProperties.Size = New Size(.Width - (iSizingValue * 2), .Height - (iSizingValue * 2))
			floProperties.Location = New Point(.Left + iSizingValue, .Top + iSizingValue)
		End With

		If (bReadOnlyMode = True) Then
			cmdOK.Enabled = False
			cmdConnect.Enabled = False
			cmoIconComboBox.Enabled = False
			cmoIconComboBox.SelectedIndex = -1
			floProperties.Enabled = False
			lstWhereList.Enabled = False
			chkCountOnly.Enabled = False
			cmoWhereAndOr.Enabled = False
			cmdTestQuery.Enabled = False
			cmdShowQuery.Enabled = False
		End If

		Me.Visible = True
		Application.DoEvents()
	End Sub

	Private Sub parseEditItem()
		' SELECT xxx, yyy, zzz FROM Win32_aaa WHERE bbb = 'b' AND ccc != 'c' AND ddd <= 'd' | Exact Match | 23 | True
		' SELECT xxx FROM Win32_aaa
		' SELECT xxx FROM Win32_aaa WHERE bbb = 'b'

		Call enableControls(True)
		Call buildResultDropdown(True)

		Dim sValues() As String = Split(lEditValues.SubItems(2).Text, "|")
		sQueryString = cleanString(sValues(0), {"  "}, {" "})

		' Add BUTTONs
		Dim iFrom As Integer = sQueryString.IndexOf(" FROM ") - "SELECT ".Length
		Dim sProperties() As String = Split(sQueryString.Substring("SELECT ".Length, iFrom), ", ")
		For Each sProp As String In sProperties : Call addButtonProperties(sProp) : Next
		Call sortFlowPanel()

		' Get Win32_ item...
		Dim sWin32 As String = sQueryString.Substring(sQueryString.IndexOf("Win32_"))
		lblClass.Text = sWin32.Split(" "c)(0).Trim

		' Add WHERE items (if they exist)
		If (InStr(sQueryString, " WHERE ") > 0) Then

			Dim iWhere As Integer = sQueryString.IndexOf(" WHERE ") + 7	' or 6?
			Dim sWhere As String = sQueryString.Substring(iWhere).Trim
			Dim sWhereSplit() As String

			' See if there is more than one statement...
			Select Case True
				Case (InStr(sWhere, "' OR ") > 0) : sWhereSplit = Split(sWhere, " OR ") : cmoWhereAndOr.Text = "OR"
				Case (InStr(sWhere, "' AND ") > 0) : sWhereSplit = Split(sWhere, " AND ") : cmoWhereAndOr.Text = "AND"
				Case Else : sWhereSplit = {sWhere} : cmoWhereAndOr.Text = "AND"
			End Select

			For Each sWS As String In sWhereSplit
				Dim sWhereItem() As String = Split(sWS.Trim, " ")	' bbb = 'b'    /    ccc != 'c'
				Dim lItem As New ListViewItem()
				lItem.Text = sWhereItem(0)			' Property 
				lItem.SubItems.Add(sWhereItem(1))	' Operator

				Dim sValue As String = vbNullString
				For i As Integer = 2 To sWhereItem.Count - 1 : sValue = sValue & sWhereItem(i) & " " : Next
				lItem.SubItems.Add(sValue.Trim.Replace("\\", "\").Trim("'"c))	' Value

				lItem.SubItems.Add(vbNullString)	' AND/OR Gap
				lItem.SubItems.Add("ICON:0")		' Red X Icon
				lstWhereList.Items.Insert(lstWhereList.Items.Count - 1, lItem)
				lItem = Nothing
			Next
			Call cmoWhereAndOr_SelectedIndexChanged(Nothing, Nothing)
		End If

		cmoQueryResult.Text = sValues(1)
		lblQueryResult.Text = sValues(2)
		chkCountOnly.Checked = CBool(sValues(3))

		cmdOK.Enabled = True
		lstWhereList.Enabled = True
		cmoWhereAndOr.Enabled = True
		bTestPassesOK = True
		Call enableControls(True)
		Call showResults(True)
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

	Private Sub enableControls(ByVal bEnable As Boolean)
		lblQ_Select.Enabled = bEnable
		lblQ_From.Enabled = bEnable
		lblQ_Where.Enabled = bEnable
		lblClass.Enabled = bEnable
		floProperties.Enabled = bEnable
		floProperties.BackColor = CType(IIf(bEnable, SystemColors.Window, SystemColors.Control), Color)
		chkCountOnly.Enabled = bEnable
	End Sub

	Private Sub showResults(ByVal bEnable As Boolean)
		cmdTestQuery.Visible = Not bEnable
		lnkExpandResults.Visible = False
		lblQueryResult.Visible = bEnable
		cmoQueryResult.Visible = bEnable
	End Sub

	Private Sub cmoIconComboBox_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cmoIconComboBox.SelectedIndexChanged
		If ((iCurrSelected = -1) AndAlso (cmoIconComboBox.Items.Count = 1)) Then Exit Sub
		If (cmoIconComboBox.SelectedIndex = iCurrSelected) Then Exit Sub
		If ((cmoIconComboBox.SelectedItem.IsDivider = True) Or (cmoIconComboBox.SelectedItem.IsFolder = True)) Then
			cmoIconComboBox.SelectedIndex = iCurrSelected
			Exit Sub
		End If

		If (cmoIconComboBox.SelectedItem.IsEnabled = False) Then
			MessageBox.Show(Me, "This server is currently set to disabled, please choose another", APN, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Exit Sub
		End If

		If ((oResult IsNot Nothing) AndAlso (cmoClasses.Items.Count > 0)) Then
			Dim sMsg As String = "Changing servers may result in the current query being reset." & vbCrLf
			sMsg = sMsg & "Only if the selected WMI Class is not available on the selected server" & vbCrLf & vbCrLf
			sMsg = sMsg & "Make sure you click 'Connect' before starting any query operations."
			Dim iResult As DialogResult = MessageBox.Show(Me, sMsg, APN, MessageBoxButtons.OKCancel, MessageBoxIcon.Question)
			If (iResult = Windows.Forms.DialogResult.Cancel) Then
				cmoIconComboBox.SelectedIndex = iCurrSelected
				Exit Sub
			End If
			cmoClasses.Items.Clear()
			cmoClasses.Enabled = False
			lblPropertiesCount.Text = "- Properties"
			oResult = Nothing
		End If

		iCurrSelected = cmoIconComboBox.SelectedIndex
		Call showResults(False)
	End Sub

	Private Sub SortToolStripItemCollection(coll As ToolStripItemCollection)
		Dim oAList As New System.Collections.ArrayList(coll)
		oAList.Sort(New ToolStripItemComparer())
		coll.Clear()
		For Each oItem As ToolStripItem In oAList
			coll.Add(oItem)
		Next
	End Sub
	Private Class ToolStripItemComparer
		Implements System.Collections.IComparer
		Public Function Compare(x As Object, y As Object) As Integer Implements System.Collections.IComparer.Compare
			Dim oItem1 As ToolStripItem = DirectCast(x, ToolStripItem)
			Dim oItem2 As ToolStripItem = DirectCast(y, ToolStripItem)
			Return String.Compare(oItem1.Text, oItem2.Text, True)
		End Function
	End Class

	Private Sub cmdConnect_Click(sender As System.Object, e As System.EventArgs) Handles cmdConnect.Click
		If (cmoIconComboBox.Text.Length = 0) Then Exit Sub

		Me.Cursor = Cursors.WaitCursor
		With cmoClasses
			.Items.Clear()
			.Enabled = False
			.DropDownHeight = (.ItemHeight * 10) + 2
		End With

		Call wmiConnection()
		If ((oResult IsNot Nothing) AndAlso (oResult.IsConnected = True)) Then
			Dim mCollection As ManagementObjectCollection
			Dim oSearcher As New ManagementObjectSearcher(oResult.Path.ToString, "SELECT * FROM META_CLASS")
			mCollection = oSearcher.Get()

			For Each mObj As ManagementObject In mCollection
				Dim oClass As String = mObj("__CLASS").ToString
				If ((oClass.StartsWith("Win32_")) AndAlso (oClass.StartsWith("Win32_Perf") = False)) Then cmoClasses.Items.Add(oClass)
			Next

			cmoClasses.Sorted = True
			cmoClasses.Enabled = True
			If (lblClass.Text.StartsWith("Win32_")) Then
				cmoClasses.Text = lblClass.Text
				Call cmoClasses_SelectedIndexChanged(Nothing, Nothing)
			Else
				cmoClasses.DroppedDown = True
				cmoClasses.Focus()
			End If
		Else

			cmoIconComboBox.SelectedItem.ItemImage = My.Resources._16___Scan___Unknown
			cmoIconComboBox.SelectedItem.IsEnabled = False
			cmoIconComboBox.Refresh()
		End If

		Me.Cursor = Cursors.Default
	End Sub

	Private Sub cmoClasses_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cmoClasses.SelectedIndexChanged
		If (cmoClasses.Text <> lblClass.Text) Then
			If (floProperties.Controls.Count > 1) Then
				Dim iResult As DialogResult
				iResult = MessageBox.Show(Me, "Changing the selected WMI Class will clear the WMI Query below" & vbCrLf & "Do you want to continue.?", APN, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
				If iResult = Windows.Forms.DialogResult.No Then
					cmoClasses.Text = lblClass.Text
					Exit Sub
				End If
			End If
		End If

		If ((cmoClasses.Text = lblClass.Text) AndAlso (sender IsNot Nothing) AndAlso (e IsNot Nothing)) Then Exit Sub

		Me.Cursor = Cursors.WaitCursor
		If (lEditValues Is Nothing) Then
			lstWhereList.Items.Clear()
			If (floProperties.Controls.Count > 1) Then floProperties.Controls.Clear()
		End If
		lblPropertiesCount.Text = "- Properties"

		Call enableControls(True)
		If (floProperties.Controls.Count = 0) Then Call addButtonProperties("+")

		If (oResult.IsConnected = True) Then
			mnuProperties.Items.Clear()
			lblClass.Text = cmoClasses.Text

			Try
				Dim mc As New ManagementClass(cmoClasses.Text)
				For Each pProp As PropertyData In mc.Properties
					Dim bFound As Boolean = False
					For Each ctl As Control In floProperties.Controls
						If (ctl.Text = pProp.Name) Then bFound = True
					Next
					If bFound = False Then
						mnuProperties.Items.Add(pProp.Name, Nothing, AddressOf onClick_AddProperty)
					End If
				Next
				Call SortToolStripItemCollection(mnuProperties.Items)
			Catch ex As Exception
			End Try
			If (mnuProperties.Items.Count = 0) Then mnuProperties.Items.Add("Nothing Found")
		End If
		lblPropertiesCount.Text = mnuProperties.Items.Count & " Properties"
		Me.Cursor = Cursors.Default
	End Sub

	Private Function buildQuery() As String
		Dim qString As String = vbNullString
		Me.Cursor = Cursors.WaitCursor

		Call sortFlowPanel()

		' Start build of query...
		qString = "SELECT * FROM " & lblClass.Text
		For Each bItem As Control In floProperties.Controls
			If (bItem.Name <> "+") Then qString = qString.Replace("*", bItem.Text & ", *")
		Next
		qString = qString.Replace(", *", "")

		If (lstWhereList.Items.Count > 1) Then
			qString = qString & " WHERE "
			For Each lItem As ListViewItem In lstWhereList.Items
				If (lItem.Text <> "Add WHERE Clause") Then
					Dim sEncodeInput As String = lItem.SubItems(2).Text.Replace("\", "\\").ToUpper

					qString = qString & "" & lItem.Text & " " & lItem.SubItems(1).Text & " '" & sEncodeInput & "' "
					If (lItem.SubItems(3).Text <> " ") Then qString = qString & lItem.SubItems(3).Text & " "
				End If
			Next
		End If

		qString = qString.ToString.Trim
		If ((qString.EndsWith("AND")) Or (qString.EndsWith("OR"))) Then qString = qString.Remove(qString.Length - 3)
		If (qString.StartsWith("SELECT * FROM") = True) Then qString = vbNullString
		Me.Cursor = Cursors.Default

		sQueryString = qString
		Return qString
	End Function

	Private Sub cmdTestQuery_Click(sender As System.Object, e As System.EventArgs) Handles cmdTestQuery.Click
		bTestPassesOK = False
		lnkExpandResults.Visible = False

		If (oResult Is Nothing) Then Call cmdConnect_Click(sender, e)

		Dim qString As String = buildQuery()
		If (qString Is Nothing) Then Exit Sub
		If (qString = vbNullString) Then Exit Sub

		If (oResult.IsConnected = True) Then
			Me.Cursor = Cursors.WaitCursor

			' Execute Query...
			Dim cOptions As New EnumerationOptions
			cOptions.Timeout = New TimeSpan(0, 0, 30)
			Dim mCollection As ManagementObjectCollection
			Dim oSearcher As New ManagementObjectSearcher(oResult.Path.ToString, qString, cOptions)
			mCollection = oSearcher.Get()

			If (chkCountOnly.Checked = True) Then
				Try
					bTestPassesOK = True
					lblQueryResult.Text = mCollection.Count.ToString

				Catch ex As Exception
					bTestPassesOK = False
					MessageBox.Show(Me, "WMI Error: " & ex.Message, APN, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					Me.Cursor = Cursors.Default
					Exit Sub
				End Try

			Else
				Try
					Dim sResult As New StringBuilder("")
					For Each mObj As ManagementObject In mCollection
						For Each pProp As PropertyData In mObj.Properties
							Dim oText As Object = mObj(pProp.Name)
							If (oText IsNot Nothing) Then
								sResult.AppendLine(mObj(pProp.Name).ToString)
							Else
								sResult.AppendLine("(no result)")
							End If
						Next
					Next
					bTestPassesOK = True
					lblQueryResult.Text = sResult.ToString
					If (sResult.Length = 0) Then lblQueryResult.Text = "(no result)"

				Catch ex As Exception
					bTestPassesOK = False
					MessageBox.Show(Me, "WMI Error: " & ex.Message, APN, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					Me.Cursor = Cursors.Default
					Exit Sub
				End Try
			End If
			Me.Cursor = Cursors.Default
		End If

		Call showResults(bTestPassesOK)
		Call buildResultDropdown(chkCountOnly.Checked)
		cmdOK.Enabled = bTestPassesOK
		If (lblQueryResult.Text.Split(CChar(vbCrLf)).Count > 2) Then lnkExpandResults.Visible = True
	End Sub

	Private Sub buildResultDropdown(ByVal bShowNumericOptions As Boolean)
		Dim cItem As ctrlComboBox_Icons.IconComboItem
		With cmoQueryResult
			.ItemHeight = 19
			With .Items
				.Clear()
				cItem = New ctrlComboBox_Icons.IconComboItem("Equal To")
				cItem.ItemImage = CType(My.Resources.ResourceManager.GetObject("_16___Operator___EqualTo"), Drawing.Icon)
				.Add(cItem)

				cItem = New ctrlComboBox_Icons.IconComboItem("Not Equal To")
				cItem.ItemImage = CType(My.Resources.ResourceManager.GetObject("_16___Operator___NotEqualTo"), Drawing.Icon)
				.Add(cItem)

				If (bShowNumericOptions = True) Then
					cItem = New ctrlComboBox_Icons.IconComboItem("Greater Than")
					cItem.ItemImage = CType(My.Resources.ResourceManager.GetObject("_16___Operator___GreaterThan"), Drawing.Icon)
					.Add(cItem)

					cItem = New ctrlComboBox_Icons.IconComboItem("Less Than")
					cItem.ItemImage = CType(My.Resources.ResourceManager.GetObject("_16___Operator___LessThan"), Drawing.Icon)
					.Add(cItem)

					cItem = New ctrlComboBox_Icons.IconComboItem("Greater Than Or Equal To")
					cItem.ItemImage = CType(My.Resources.ResourceManager.GetObject("_16___Operator___GreaterThanOrEqualTo"), Drawing.Icon)
					.Add(cItem)

					cItem = New ctrlComboBox_Icons.IconComboItem("Less Than Or Equal To")
					cItem.ItemImage = CType(My.Resources.ResourceManager.GetObject("_16___Operator___LessThanOrEqualTo"), Drawing.Icon)
					.Add(cItem)
				End If
			End With
			.SelectedIndex = 0
			If (lEditValues IsNot Nothing) Then
				.Text = (Split(lEditValues.SubItems(2).Text, "|")(1))
			End If
		End With
	End Sub

	Private Sub wmiConnection()
		oResult = Nothing
		Dim sMsg As String = "There was and error when trying to gather the requested information." & vbCrLf & _
		"The message returned is below..." & vbCrLf & vbCrLf & "REPLACE"
		Try
			Dim cOptions As New ConnectionOptions
			cOptions.Impersonation = ImpersonationLevel.Impersonate
			oResult = New ManagementScope("\\" & cmoIconComboBox.Text & "\root\cimv2", cOptions)
			oResult.Connect()

		Catch ex As System.UnauthorizedAccessException
			sMsg = sMsg.Replace("REPLACE", "Access Denied" & vbCrLf & vbCrLf & "Make sure you have enough permissions to access this server")

		Catch ex As Exception
			If ex.Message.Contains("The RPC server") Then
				sMsg = sMsg.Replace("REPLACE", ex.Message & vbCrLf & vbCrLf & "Make sure the server is up and running, and is contactable")
			Else
				sMsg = sMsg.Replace("REPLACE", ex.Message & vbCrLf & vbCrLf & "Check the error and try to resolve it if possible.")
			End If

		Finally
			If (InStr(sMsg, "REPLACE") = 0) Then
				MessageBox.Show(frmMain, sMsg, APN, MessageBoxButtons.OK, MessageBoxIcon.Error)
			End If
		End Try
	End Sub

	Private Function getButtonWidth(ByVal sText As String) As Integer
		Dim b As Bitmap = New Bitmap(1, 1)
		Dim g As Graphics = Graphics.FromImage(b)
		Dim iResult As Integer = CInt(g.MeasureString(sText, frmMain.sysFont).Width) + 12

		g.Dispose()
		b.Dispose()
		Return iResult
	End Function

	Private Sub addButtonProperties(ByVal sText As String)
		If (sText = vbNullString) Then Exit Sub
		If (floProperties.Controls.ContainsKey("+") = True) Then Exit Sub

		Dim btn As New Button()
		btn.Width = 25
		btn.Height = 25
		btn.Margin = New Padding(1, 1, 1, 1)
		btn.Name = sText.Replace(",", "")
		btn.BackColor = SystemColors.ButtonFace

		Application.DoEvents()

		With btn
			If (sText = "+") Then
				.Name = "+"
				.Image = My.Resources._16___Add.ToBitmap
			Else
				.Text = .Name
				.Width = getButtonWidth(.Name)
			End If

			AddHandler .MouseUp, AddressOf onClick_PropertiesMouseUp
		End With

		floProperties.Controls.Add(btn)
		If (chkAutoSortSelect.Checked = True) Then Call sortFlowPanel()
		floProperties.ScrollControlIntoView(btn)
		Application.DoEvents()
	End Sub

	Private Sub delButtonProperties(ByVal sText As String)
		If (sText = vbNullString) Then Exit Sub
		Dim iResult As DialogResult = Windows.Forms.DialogResult.None
		For Each lItem As ListViewItem In lstWhereList.Items
			If (bSelectedButton.Text = lItem.Text) Then
				If (iResult = Windows.Forms.DialogResult.None) Then
					iResult = MessageBox.Show(Me, "This action will remove a WHERE statement below." & vbCrLf & "Do you want to continue.?", APN, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
				End If

				If (iResult = Windows.Forms.DialogResult.Yes) Then lItem.Remove() Else Exit Sub
			End If
		Next
		Application.DoEvents()
		If (chkAutoSortSelect.Checked = True) Then Call sortFlowPanel()

		' Add item back into stack of available properties, then remove button...
		If (sText <> "+") Then mnuProperties.Items.Add(sText, Nothing, AddressOf onClick_AddProperty)
		Call SortToolStripItemCollection(mnuProperties.Items)
		floProperties.Controls.RemoveByKey(sText)

		' If not ADD, and there are properties items left in stack, and ADD button doesn't
		If ((sText <> "+") AndAlso (mnuProperties.Items.Count > 0) AndAlso (floProperties.Controls.Find("+", True).Count = 0)) Then Call addButtonProperties("+")
	End Sub

	Private Sub onClick_PropertiesMouseUp(sender As Object, e As MouseEventArgs)
		If ((oResult Is Nothing) OrElse (oResult.IsConnected = False)) Then Exit Sub

		Dim btn As Button = CType(sender, Button)
		If (btn.Name = "+") Then
			mnuProperties.Show(btn, e.Location)
		Else
			mnuDeleteEdit.Items(0).Enabled = True
			If (mnuProperties.Items.Count = 0) Then mnuDeleteEdit.Items(0).Enabled = False
			mnuDeleteEdit.Show(btn, e.Location)
		End If
		bEditingProperty = False
		bSelectedButton = btn
	End Sub

	Private Sub onClick_AddProperty(sender As System.Object, e As System.EventArgs)
		Dim mnu As ToolStripMenuItem = CType(sender, ToolStripMenuItem)

		If (bEditingProperty = True) Then
			mnuProperties.Items.Add(bSelectedButton.Text, Nothing, AddressOf onClick_AddProperty)
			bSelectedButton.Text = mnu.Text
			bSelectedButton.Name = mnu.Text
			bSelectedButton.Width = getButtonWidth(mnu.Text)
			mnuProperties.Items.Remove(mnu)
			Call SortToolStripItemCollection(mnuProperties.Items)
			bEditingProperty = False
		Else
			Call delButtonProperties("+")
			Call addButtonProperties(mnu.Text)
			mnuProperties.Items.Remove(mnu)
			If ((mnuProperties.Items.Count > 0) AndAlso (floProperties.Controls.Find("+", True).Count = 0)) Then Call addButtonProperties("+")
		End If
	End Sub

	Private Sub onClick_EditProperty(sender As System.Object, e As System.EventArgs)
		Dim iResult As DialogResult = Windows.Forms.DialogResult.None
		For Each lItem As ListViewItem In lstWhereList.Items
			If (bSelectedButton.Text = lItem.Text) Then
				If (iResult = Windows.Forms.DialogResult.None) Then
					iResult = MessageBox.Show(Me, "This action will remove any matching WHERE" & vbCrLf & "statements below.  Do you want to continue.?", APN, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
				End If

				If (iResult = Windows.Forms.DialogResult.Yes) Then lItem.Remove() Else Exit Sub
			End If
		Next
		Application.DoEvents()
		bEditingProperty = True
		mnuProperties.Show(bSelectedButton, bSelectedButton.Location)
	End Sub

	Private Sub onClick_DeleteProperty(sender As System.Object, e As System.EventArgs)
		Call delButtonProperties(bSelectedButton.Text)
	End Sub

	Private Sub onClick_ClearAllProperty(sender As System.Object, e As System.EventArgs)
		If (lstWhereList.Items.Count > 1) Then
			Dim iResult As DialogResult = MessageBox.Show(Me, "This action will remove a WHERE statements below." & vbCrLf & "Do you want to continue.?", APN, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
			If (iResult = Windows.Forms.DialogResult.No) Then Exit Sub
		End If

		floProperties.Controls.Clear()
		addButtonProperties("+")
		Application.DoEvents()

		Try
			mnuProperties.Items.Clear()
			Dim mc As New ManagementClass(cmoClasses.Text)
			For Each pProp As PropertyData In mc.Properties
				mnuProperties.Items.Add(pProp.Name, Nothing, AddressOf onClick_AddProperty)
			Next
			Call SortToolStripItemCollection(mnuProperties.Items)
		Catch ex As Exception
		End Try
		If (mnuProperties.Items.Count = 0) Then mnuProperties.Items.Add("Nothing Found")
	End Sub

	Private Function checkEntryIsNew(ByVal lLVItem As ListViewItem) As Boolean
		If (lLVItem.Text.Contains("Add WHERE Clause")) Then Return True
		Return False
	End Function

	Private Sub floProperties_ControlAdded(sender As Object, e As System.Windows.Forms.ControlEventArgs) Handles floProperties.ControlAdded
		Call floProperties_ControlRemoved(sender, e)
	End Sub

	Private Sub floProperties_ControlRemoved(sender As Object, e As System.Windows.Forms.ControlEventArgs) Handles floProperties.ControlRemoved
		If (floProperties.Controls.Count > 0) Then
			If (lstWhereList.Items.Find("Add WHERE Clause", False).Count = 0) Then
				Dim lItem As New ListViewItem("Add WHERE Clause")
				lItem.Name = lItem.Text
				lItem.SubItems.Add("")	' Operator
				lItem.SubItems.Add("")	' Free Text
				lItem.SubItems.Add("")	' Add/Or
				lItem.SubItems.Add("")	' Delete Row
				lItem.ForeColor = SystemColors.HotTrack
				lstWhereList.Items.Add(lItem)
			End If

			If (floProperties.Controls.Count > 1) Then
				lstWhereList.Enabled = True
				cmdTestQuery.Enabled = True
				cmdShowQuery.Enabled = True
				chkCountOnly.Enabled = True
				cmdOK.Enabled = False
			Else
				lstWhereList.Enabled = False
				cmdTestQuery.Enabled = False
				cmdShowQuery.Enabled = False
				chkCountOnly.Enabled = False
			End If
			If (floProperties.Controls.Count > 2) Then chkCountOnly.Checked = True
		Else
			lstWhereList.Items.Clear()
		End If

		bTestPassesOK = False
		Call showResults(bTestPassesOK)
		floProperties.HorizontalScroll.Visible = False
	End Sub

	Private Sub cmoWhereAndOr_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cmoWhereAndOr.SelectedIndexChanged
		For Each lItem As ListViewItem In lstWhereList.Items
			If (lItem.Text <> "Add WHERE Clause") Then
				lItem.SubItems(3).Text = cmoWhereAndOr.Text
				If (lItem.ListView.Items.Count < 3) Then lItem.SubItems(3).Text = vbNullString
				If (lItem.Index = lItem.ListView.Items.Count - 2) Then lItem.SubItems(3).Text = vbNullString
			End If
		Next
		Call showResults(False)
	End Sub

	Private Sub cmdOK_Click(sender As System.Object, e As System.EventArgs) Handles cmdOK.Click
		If (bTestPassesOK = False) Then
			MessageBox.Show(Me, "Please test the query first", APN, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Exit Sub
		End If

		' RESOURCE DUPLICATE CHECK...
		Dim dResult As DialogResult = resourceDuplicationAlert("WMI Query", lEditValues, ({lblClass.Text}.ToList))
		If (dResult <> Windows.Forms.DialogResult.None) Then Exit Sub

		' Settings are saved in calling procedure ('frmMain.onClick_addResource_WMIQuery' or 'frmMain.onClick_editProperties')
		frmMain.m_AddWMIQuery = sQueryString.Trim & "|" & cmoQueryResult.Text.Trim & "|" & lblQueryResult.Text.Trim & "|" & chkCountOnly.Checked.ToString
		lResourceConflicts = Nothing
		Me.Close()
		Me.Dispose()
	End Sub

	Private Sub lnkExpandResults_LinkClicked(sender As System.Object, e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lnkExpandResults.LinkClicked
		frmAddWMIQueryTestResults.txtResult.Text = lblQueryResult.Text
		frmAddWMIQueryTestResults.ShowDialog(Me)
	End Sub

	Private Sub lblQueryResult_TextChanged(sender As Object, e As System.EventArgs) Handles lblQueryResult.TextChanged
		If (lblQueryResult.Text.Split(CChar(vbCrLf)).Count > 2) Then lnkExpandResults.Visible = True
	End Sub

	Private Sub lnkHelp_LinkClicked(sender As System.Object, e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lnkHelp.LinkClicked
		frmHelp.sSelectPageByID = "help0035"
		frmHelp.ShowDialog(Me)
	End Sub

	Private Sub lnkNote_LinkClicked(sender As System.Object, e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lnkNote.LinkClicked
		frmHelp.sSelectPageByID = "help0037"
		frmHelp.ShowDialog(Me)
	End Sub

	Private Sub chkCountOnly_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkCountOnly.CheckedChanged
		Call buildResultDropdown(chkCountOnly.Checked)
		Call showResults(False)
	End Sub

	Private Sub cmdShowQuery_Click(sender As System.Object, e As System.EventArgs) Handles cmdShowQuery.Click
		Dim qQuery As String = buildQuery()
		If (qQuery IsNot Nothing) Then MessageBox.Show(Me, qQuery, " WMI Query String")
	End Sub

	Private Sub cmdCancel_Click(sender As System.Object, e As System.EventArgs) Handles cmdCancel.Click
		frmMain.m_AddWMIQuery = Nothing
		Me.Close()
		Me.Dispose()
	End Sub

	Private Sub lstWhereList_MouseMove(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles lstWhereList.MouseMove
		If (Me.Cursor = Cursors.WaitCursor) Then Exit Sub
		Me.Cursor = Cursors.Default

		Dim lLVItem As ListViewItem = lstWhereList.GetItemAt(e.X, e.Y)
		If (lLVItem Is Nothing) Then Exit Sub
		Dim lSubItem As ListViewItem.ListViewSubItem = lLVItem.GetSubItemAt(e.X, e.Y)

		If ((lLVItem.SubItems.IndexOf(lSubItem) = 4) AndAlso (lSubItem.Text = "ICON:0")) Then Me.Cursor = Cursors.Hand
	End Sub

	Private Sub lstWhereList_MouseDoubleClick(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles lstWhereList.MouseDoubleClick
		Dim lLVItem As ListViewItem = lstWhereList.GetItemAt(e.X, e.Y)
		If (lLVItem Is Nothing) Then Exit Sub
		Dim lSubItem As ListViewItem.ListViewSubItem = lLVItem.GetSubItemAt(e.X, e.Y)

		Dim bNewItem As Boolean = checkEntryIsNew(lLVItem)
		With frmAddWMIQueryWhereBuilder
			.lExistingItem = Nothing
			If (bNewItem = False) Then .lExistingItem = lLVItem
			.ShowDialog(Me)
			bTestPassesOK = False
		End With

		If (lWhereBuilderItem IsNot Nothing) Then
			lstWhereList.Items.Insert(lLVItem.Index, lWhereBuilderItem)
			If (bNewItem = False) Then lLVItem.Remove()
			Call cmoWhereAndOr_SelectedIndexChanged(sender, e)
		End If

		If (lstWhereList.Items.Count > 1) Then cmoWhereAndOr.Enabled = True Else cmoWhereAndOr.Enabled = False
		bTestPassesOK = False
		Call showResults(False)
		lLVItem.Selected = False
		picIcon.Focus()
	End Sub

	Private Sub lstWhereList_MouseClick(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles lstWhereList.MouseClick
		If (Me.Cursor = Cursors.WaitCursor) Then Exit Sub
		Dim lLVItem As ListViewItem = lstWhereList.GetItemAt(e.X, e.Y)
		If (lLVItem Is Nothing) Then Exit Sub
		Dim lSubItem As ListViewItem.ListViewSubItem = lLVItem.GetSubItemAt(e.X, e.Y)

		If ((lLVItem.SubItems.IndexOf(lSubItem) = 4) AndAlso (lSubItem.Text = "ICON:0")) Then
			lstWhereList.Items.Remove(lLVItem)
			Call showResults(False)
			Me.Cursor = Cursors.Default
		End If

		bTestPassesOK = False
		If (lstWhereList.Items.Count > 1) Then cmoWhereAndOr.Enabled = True Else cmoWhereAndOr.Enabled = False
	End Sub

	Private Sub sortFlowPanel()
		Dim sortList As SortedList(Of String, Button) = New SortedList(Of String, Button)
		For Each ctl As Button In floProperties.Controls : sortList.Add(ctl.Text, ctl) : Next
		For Each btn As Button In sortList.Values : btn.SendToBack() : Next
		If (floProperties.Controls.ContainsKey("+") = True) Then floProperties.Controls("+").SendToBack()
		sortList = Nothing
	End Sub
End Class
