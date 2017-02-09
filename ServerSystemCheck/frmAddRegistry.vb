Option Explicit On

Public Class frmAddRegistry
	Public lEditValues As ListViewItem				' Are we EDITING an existing item.?
	Private bImporting As Boolean					'
	Private bFlatView As Boolean
	Private ioReader As IO.StreamReader = Nothing	'
	Private img16 As New ImageList
	Private imgSPACING As New ImageList
	Private mnuEditSelections As ContextMenuStrip
	Public sGroupGUID As String
	Private sREGTypes() As String = {"s|REG_SZ", "h|REG_BINARY", "h|REG_DWORD", "h|REG_QWORD", "s|REG_MULTI_SZ", "s|REG_EXPAND_SZ"}

	Private Sub frmAddRegistry_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
		For Each c As Control In Controls
			c.Font = frmMain.sysFont
		Next
		lblTitle.Font = frmMain.sysFontTitle
		lnkHelp.Font = frmMain.sysFontHelp
		cmdSearchRegistry.Font = frmMain.sysFontHelp

		lnkHelp.LinkBehavior = LinkBehavior.HoverUnderline
		picIcon.Image = My.Resources._48___Registry.ToBitmap
		If (lEditValues IsNot Nothing) Then lblTitle.Text = "Edit Existing" Else lblTitle.Text = "Add New"
		lblTitle.Text = lblTitle.Text & " Registry Scan"
		Me.Text = " " & lblTitle.Text

		cmdImportReg.Enabled = True

		With img16
			.ImageSize = New Size(16, 16)
			.ColorDepth = ColorDepth.Depth32Bit
			With .Images
				.Clear()
				.Add("_16___Group___" & m_IconGroup & "___Yellow", CType(My.Resources.ResourceManager.GetObject("_16___Group___" & m_IconGroup & "___Yellow"), Icon))
				.Add("_16___Registry___HEX", My.Resources._16___Registry___HEX)
				.Add("_16___Registry___STR", My.Resources._16___Registry___STR)
				.Add("_16___Scan___Failed", My.Resources._16___Scan___Failed)
			End With
		End With

		With imgSPACING
			.ImageSize = New Size(18, iListViewLineHeight)
			.ColorDepth = ColorDepth.Depth32Bit
			For i As Integer = 0 To img16.Images.Count - 1
				.Images.Add(img16.Images.Keys(i), ResizeImage(img16.Images(i), .ImageSize, True))
			Next
		End With

		txtRegistryKey.Text = vbNullString
		txtValueData.Text = vbNullString
		txtValueName.Text = vbNullString

		Dim cItem As ctrlComboBox_Icons.IconComboItem
		With cmoValueType
			.ItemHeight = 19
			With .Items
				.Clear()
				For Each sREGItem As String In sREGTypes
					Dim sItem() As String = Split(sREGItem, "|")
					cItem = New ctrlComboBox_Icons.IconComboItem(sItem(1))
					If (sItem(0) = "h") Then cItem.ItemImage = My.Resources._16___Registry___HEX Else cItem.ItemImage = My.Resources._16___Registry___STR
					.Add(cItem)
				Next
			End With
			.SelectedIndex = 0	' REG_SZ
		End With

		With cmoMissingKey
			.ItemHeight = 19
			With .Items
				.Clear()
				cItem = New ctrlComboBox_Icons.IconComboItem("OK")
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

		bImporting = False
		Call cmdImportREG_Click(Nothing, Nothing)

		With lstImportReg
			.Clear()
			.BringToFront()
			.Visible = False
			.Location = New Point(lblTop.Left, txtRegistryKey.Top)
			.FullRowSelect = True
			.SmallImageList = imgSPACING
			.View = View.Details
			.MultiSelect = True
			.HeaderStyle = ColumnHeaderStyle.Nonclickable
			.ShowGroups = False
			With .Columns
				.Clear()
				.Add("path", "Registry Path", 0) ' Width set via code in 'cmdImportREG_Click'
				.Add("key", "Key", 150)
				.Add("data", "Data", 250)
			End With
		End With

		cmdImportReg.Visible = True
		cmoValueType.Enabled = True
		cmoValueType.BackColor = SystemColors.Window

		If (lEditValues IsNot Nothing) Then
			Dim iPos As Integer = lEditValues.Text.LastIndexOf("\")
			txtRegistryKey.Text = lEditValues.Text.Substring(0, iPos)
			txtValueName.Text = lEditValues.Text.Substring(iPos + 1)

			Dim sValues() As String = Split(lEditValues.SubItems(2).Text, ", ")
			txtValueData.Text = Replace(sValues(0), "¬", "|")

			If (cmoValueType.FindStringExact(sValues(1)) = -1) Then
				cmoValueType.Enabled = False
				cmoValueType.BackColor = SystemColors.Control
			End If

			cmoValueType.SelectedIndex = cmoValueType.FindStringExact(sValues(1))
			cmoMissingKey.SelectedIndex = cmoMissingKey.FindStringExact(" " & sValues(2))

			cmdImportReg.Visible = False
		End If

		If (bUseVisualStyles = True) Then
			Call SetWindowTheme(lstImportReg.Handle, "Explorer", Nothing)
		End If

		If (bReadOnlyMode = True) Then
			cmdOK.Enabled = False
			cmdSearchRegistry.Enabled = False
		End If

		Me.Visible = True
		Application.DoEvents()
		txtRegistryKey.Focus()
		txtRegistryKey.SelectAll()
	End Sub

	Private Sub cmdCancel_Click(sender As System.Object, e As System.EventArgs) Handles cmdCancel.Click
		img16.Dispose()
		imgSPACING.Dispose()
		frmMain.m_AddRegistry = Nothing
		If (ioReader IsNot Nothing) Then ioReader.Dispose()
		Me.Close()
		Me.Dispose()
	End Sub

	Private Sub cmdOK_Click(sender As System.Object, e As System.EventArgs) Handles cmdOK.Click
		Dim sResults As New List(Of String)

		If (bImporting = True) Then
			If ((lstImportReg.Items.Count = 1) AndAlso (lstImportReg.Items(0).Name = "READ")) Then Exit Sub

			Dim dAllItems As DialogResult = DialogResult.No	' "All Items"
			If (lstImportReg.SelectedItems.Count > 1) Then
				Call clsMessageBox.CustomMsgBox({"Selected", "All Items", "Cancel"})
				dAllItems = MessageBox.Show(Me, "More than one item is selected.  Do you want to" & vbCrLf & "import just the selected entries, or all entries.?", APN, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)
				If (dAllItems = DialogResult.Cancel) Then Exit Sub
			End If

			If dAllItems = DialogResult.No Then
				For Each lItem As ListViewItem In lstImportReg.Items
					sResults.Add(lItem.Name & "|" & lItem.SubItems(2).Text & ", " & lItem.Tag.ToString & ", " & cmoMissingKey.SelectedItem.DisplayText.Trim)
				Next
			Else
				For Each lItem As ListViewItem In lstImportReg.SelectedItems
					sResults.Add(lItem.Name & "|" & lItem.SubItems(2).Text & ", " & lItem.Tag.ToString & ", " & cmoMissingKey.SelectedItem.DisplayText.Trim)
				Next
			End If

		Else
			If ((txtRegistryKey.Text = vbNullString) Or (txtValueName.Text = vbNullString)) Then Exit Sub
			sResults.Add(txtRegistryKey.Text.Trim & "\" & txtValueName.Text.Trim & "|" & _
						 txtValueData.Text.Replace("|", "¬") & ", " & cmoValueType.SelectedItem.DisplayText.Trim & ", " & _
						 cmoMissingKey.SelectedItem.DisplayText.Trim)
		End If

		' RESOURCE DUPLICATE CHECK...
		Dim sScanType As String = "Registry Scan"
		If (bImporting = True) Then sScanType = "Registry Scan Import"
		Dim dResult As DialogResult = resourceDuplicationAlert(sScanType, lEditValues, sResults)
		If ((dResult = Windows.Forms.DialogResult.Yes) Or (dResult = Windows.Forms.DialogResult.OK) Or (dResult = Windows.Forms.DialogResult.Cancel)) Then Exit Sub
		If (dResult = Windows.Forms.DialogResult.No) Then
			If (bImporting = True) Then
				For Each itm As String In lResourceConflicts
					Dim sAttributes As String = xml_getSpecificItem(Split(itm, "|")(1))	' name | type | guid | checking
					For Each lItem As ListViewItem In lstImportReg.Items
						If (lItem.Name = sAttributes.Split("|"c)(0)) Then sResults.Remove(lItem.Name & "|" & lItem.SubItems(2).Text & ", " & lItem.Tag.ToString & ", " & cmoMissingKey.SelectedItem.DisplayText.Trim)
					Next
				Next
			Else
				Exit Sub
			End If
		End If

		' Settings are saved in calling procedure ('frmMain.onClick_addResource_Registry' or 'frmMain.onClick_editProperties')
		lResourceConflicts = Nothing
		img16.Dispose()
		imgSPACING.Dispose()
		frmMain.m_AddRegistry = sResults
		If (ioReader IsNot Nothing) Then ioReader.Dispose()
		Me.Close()
		Me.Dispose()
	End Sub

	Private Sub cmdImportREG_Click(sender As System.Object, e As System.EventArgs) Handles cmdImportReg.Click
		If ((bImporting = True) AndAlso (lstImportReg.Items.Count > 0)) Then
			If (MessageBox.Show(Me, "Are you sure you want to clear the current entries.?", APN, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.No) Then
				Exit Sub
			End If
		End If

		If ((sender IsNot Nothing) AndAlso (e IsNot Nothing)) Then bImporting = Not bImporting
		lstImportReg.Items.Clear()
		cmdBrowse.Visible = bImporting
		lstImportReg.Visible = bImporting

		Me.Width = CInt(IIf(bImporting, 900, 450))
		Me.Height = CInt(IIf(bImporting, 500, 400))
		Me.CenterToParent()

		lstImportReg.Size = New Size(Me.Width - 30, cmoMissingKey.Top - txtRegistryKey.Top - 12)

		If lstImportReg.Columns.Count > 0 Then
			lstImportReg.Columns(0).Width = lstImportReg.Width - (150 + 250) - SystemInformation.VerticalScrollBarWidth - 4
		End If

		cmdImportReg.Text = IIf(bImporting, "Cancel Import", "Import REG File").ToString
	End Sub

	Private Sub cmdBrowse_Click(sender As System.Object, e As System.EventArgs) Handles cmdBrowse.Click
		If (bImporting = True) Then
			Using dlgOpenFile As New OpenFileDialog
				With dlgOpenFile
					.Title = "Select an existing registry file to open..."
					.CheckFileExists = False
					.SupportMultiDottedExtensions = False
					.DefaultExt = "reg"
					.InitialDirectory = Application.StartupPath
					.Filter = "Registry Files (*.reg)|*.reg|All Files|*.*"
				End With
				If (dlgOpenFile.ShowDialog(Me) = Windows.Forms.DialogResult.OK) Then
					Call readREGFile(dlgOpenFile.FileName)
				End If
			End Using
		End If
	End Sub

	Private Sub readREGFile(ByVal sInputFile As String)
		Dim bSkip As Boolean
		Dim bValueLine As Boolean
		Dim sSingleLine As String = vbNullString

		' New ListItem Strings...
		Dim sLI_KeyPath As String = vbNullString
		Dim sLI_ValueName As String
		Dim sLI_ValueType As String
		Dim sLI_ValueData As String
		Dim sLI_IconType As String

		Dim iFile As New System.IO.FileInfo(sInputFile)
		Dim iMazSize As Integer = (10 * 1048576)
		If (iFile Is Nothing) Then Exit Sub

		If ((iFile.Length >= 1048576) AndAlso (iFile.Length < iMazSize)) Then
			Dim sMsg As String = "For a registry export file, this is rather large." & vbCrLf & "Are you really sure you need it all." & vbCrLf & vbCrLf & _
				  "File size: " & getSizeAndUnits(iFile.Length, 0) & vbCrLf & vbCrLf & "Do you want to continue importing this file.?"
			Dim iResult As DialogResult = MessageBox.Show(Me, sMsg, APN, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
			If iResult = Windows.Forms.DialogResult.No Then Exit Sub

		ElseIf (iFile.Length >= iMazSize) Then
			Dim sMsg As String = "For a registry export file, this is too large to import." & vbCrLf & "You have a few options..." & vbCrLf & vbCrLf & _
				  "- Cut the file down into smaller chunks (5MB max)," & vbCrLf & _
				  "- Only use a subset of this export, the stuff you really need." & vbCrLf & vbCrLf & _
				  "The import operation has been cancelled."
			MessageBox.Show(Me, sMsg, APN, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Exit Sub
		End If
		iFile = Nothing

		Try
			ioReader = New IO.StreamReader(New IO.FileStream(sInputFile, IO.FileMode.Open, IO.FileAccess.Read, IO.FileShare.Read))
		Catch ex As Exception
			MessageBox.Show(Me, "ERROR: " & ex.Message)
			Exit Sub
		End Try

		' Read the first line, make sure it's a registry file...
		readSingleLine(sSingleLine)
		If (sSingleLine.StartsWith("Windows Registry Editor Version") = False) Then
			MessageBox.Show(Me, "The selected file does not appear to be a valid exported registry file.", APN, MessageBoxButtons.OK, MessageBoxIcon.Error)
			Exit Sub
		End If

		Dim sPathList As New List(Of String)
		Dim sItemList As New List(Of ListViewItem)

		Me.Cursor = Cursors.WaitCursor
		lstImportReg.Items.Clear()
		lstImportReg.Items.Add("READ", "Reading Selected File...", 0)
		Application.DoEvents()

		Do Until (ioReader.EndOfStream)
			sLI_ValueName = vbNullString
			sLI_ValueType = vbNullString
			sLI_IconType = vbNullString
			sLI_ValueData = vbNullString
			sSingleLine = vbNullString
			bValueLine = False

			readSingleLine(sSingleLine)

			If (sSingleLine <> vbNullString) Then
				Select Case sSingleLine.Substring(0, 1)
					Case "[" ' Key Path
						bSkip = False
						If ((sSingleLine.StartsWith("[")) AndAlso (sSingleLine.EndsWith("]"))) Then
							If ((sSingleLine.StartsWith("[HKEY_LOCAL_MACHINE")) AndAlso (sSingleLine <> "[HKEY_LOCAL_MACHINE]")) Then
								sLI_KeyPath = sSingleLine.TrimStart(CChar("[")).TrimEnd(CChar("]"))
								sLI_KeyPath = sLI_KeyPath.Substring("HKEY_LOCAL_MACHINE\".Length)
								bValueLine = False
								sPathList.Add(sLI_KeyPath)
							Else
								bSkip = True
							End If
						End If

					Case "@" ' (Default) Value Name
						If (bSkip = False) Then
							sLI_ValueName = "(Default)"
							sLI_ValueData = sSingleLine.Substring(2)
							bValueLine = True
						End If

					Case Chr(34) ' Value Name
						If (bSkip = False) Then
							For i As Integer = 1 To sSingleLine.Length - 2
								If (sSingleLine.Substring(i, 1) = Chr(34)) Then
									If ((sSingleLine.Substring(i - 1, 1) <> "\") AndAlso (sSingleLine.Substring(i + 1, 1) = "=")) Then
										sLI_ValueName = sSingleLine.Substring(1, i - 1)
										sLI_ValueData = sSingleLine.Substring(sLI_ValueName.Length + 3)
										sLI_ValueName = sLI_ValueName.Replace("\" & Chr(34), Chr(34))
										bValueLine = True
										Exit For
									End If
								End If
							Next i
						End If

					Case Else
						bSkip = True
						bValueLine = False
				End Select
			End If

			If (bSkip = False) Then
				If (bValueLine = True) Then
					Dim sResult() As String = getRegistryTypeAndData(sLI_ValueData)
					sLI_IconType = sResult(0)
					sLI_ValueType = sResult(1)
					sLI_ValueData = sResult(2)
				End If

				If ((bValueLine = True) AndAlso (sLI_ValueType <> "UNKNOWN")) Then
					Dim lItem As New ListViewItem(sLI_KeyPath)
					lItem.SubItems.Add(sLI_ValueName)
					lItem.SubItems.Add(sLI_ValueData)
					lItem.ImageKey = sLI_IconType
					lItem.Tag = sLI_ValueType
					lItem.Name = sLI_KeyPath & "\" & sLI_ValueName
					sItemList.Add(lItem)
					lItem = Nothing
				End If
			End If
		Loop

		sPathList.Distinct()
		sPathList.Sort()

		lstImportReg.Items("READ").Text = "Importing Items..."
		Application.DoEvents()

		Call buildFlatView(sItemList)
		Me.Cursor = Cursors.Default

		If ((lstImportReg.Items.Count = 1) AndAlso (lstImportReg.Items(0).Name = "READ")) Then
			lstImportReg.Items("READ").Text = "Nothing To Import"
			lstImportReg.Items("READ").ImageKey = "_16___Scan___Failed"
		Else
			lstImportReg.Items("READ").Remove()
			cmdOK.Enabled = True
		End If
	End Sub

	Private Sub readSingleLine(ByRef returnString As String)
		returnString = returnString & ioReader.ReadLine.Trim
		If ((returnString IsNot Nothing) AndAlso (returnString.Length < 65000)) Then
			If (returnString.EndsWith(",\")) Then
				returnString = returnString.TrimEnd(CChar("\"))
				readSingleLine(returnString)
			End If
		Else
			returnString = "(input line too long)"
		End If
	End Sub

	Public Function convertHEXtoSTRING(ByVal sInputString As String) As String
		sInputString = sInputString.Replace(",", "")
		Dim bBytes As New List(Of Byte)
		For Index As Integer = 0 To sInputString.Length - 1 Step 2
			bBytes.Add(Convert.ToByte(sInputString.Substring(Index, 2), 16))
		Next
		Dim sResult As System.Text.Encoding = System.Text.Encoding.Unicode
		Return sResult.GetString(bBytes.ToArray).Replace(Chr(0), " " & vbCrLf)
	End Function

	Public Function convertWORDtoVALUE(ByVal sInputString As String) As String
		Dim sInput As List(Of String) = sInputString.Split(","c).ToList
		sInput.Reverse()
		Dim sOutput As String = Join(sInput.ToArray, "")
		Dim iOutput As Long = Convert.ToInt64(sOutput, 16)
		Return iOutput.ToString
	End Function

	Private Sub lnkHelp_LinkClicked(sender As System.Object, e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lnkHelp.LinkClicked
		frmHelp.sSelectPageByID = "help0023"
		frmHelp.ShowDialog(Me)
	End Sub

	Private Sub buildFlatView(ByVal sItemList As List(Of ListViewItem))
		lstImportReg.BeginUpdate()
		lstImportReg.Items.AddRange(sItemList.ToArray)
		lstImportReg.EndUpdate()
	End Sub

	Private Sub cmdSearchRegistry_Click(sender As System.Object, e As System.EventArgs) Handles cmdSearchRegistry.Click
		frmAddRegistrySearch.sGroupGUID = sGroupGUID
		frmAddRegistrySearch.sCurrentPath = txtRegistryKey.Text & "\" & txtValueName.Text & "\!" ' <-- Leave the end part, it's needed
		frmAddRegistrySearch.ShowDialog(Me)
	End Sub

	Public Function getRegistryTypeAndData(ByVal sRegValueData As String) As String()
		Dim sLI_ValueType As String
		Dim sLI_ValueData As String = sRegValueData
		Dim sLI_IconType As String

		Select Case sRegValueData.Substring(0, 1).ToUpper
			Case Chr(34)		' STRING
				sLI_ValueType = "REG_SZ"
				sLI_IconType = "_16___Registry___STR"
				sLI_ValueData = sLI_ValueData.TrimStart(Chr(34)).TrimEnd(Chr(34))

				sLI_ValueData = sLI_ValueData.Replace("\\", "\")
				sLI_ValueData = sLI_ValueData.Replace("\" & Chr(34), Chr(34))
				'sLI_ValueData = sLI_ValueData.Replace("\\", "\")
				'sLI_ValueData = sLI_ValueData.Replace("\\", "\")

			Case "D"			' DWORD:
				sLI_IconType = "_16___Registry___HEX"
				sLI_ValueType = "REG_DWORD"
				sLI_ValueData = convertWORDtoVALUE(sLI_ValueData.Substring(6))

			Case "H"			' HEX: / HEX(2)
				If (sLI_ValueData.ToUpper.StartsWith("HEX:") = True) Then
					sLI_IconType = "_16___Registry___HEX"
					sLI_ValueType = "REG_BINARY"
					sLI_ValueData = sLI_ValueData.Substring(4).Replace(",", " ").ToLower

				Else
					Dim sStartsWith As String = sLI_ValueData.ToUpper.Substring(0, 7)
					Select Case sStartsWith.ToUpper
						'Case "HEX(3):" : Unknown
						'Case "HEX(5):" : Unknown
						'Case "HEX(6):" : REG_LINK - Not supported
						'Case "HEX(8):" : REG_RESOURCE_LIST - Too much hassle
						'Case "HEX(9):" : REG_FULL_RESOURCE_DESCRIPTOR - Too much hassle

						Case "HEX(2):"
							sLI_IconType = "_16___Registry___STR"
							sLI_ValueType = "REG_EXPAND_SZ"
							sLI_ValueData = convertHEXtoSTRING(sLI_ValueData.Substring(7))

						Case "HEX(4):"
							sLI_IconType = "_16___Registry___HEX"
							sLI_ValueType = "REG_DWORD"
							sLI_ValueData = convertWORDtoVALUE(sLI_ValueData.Substring(7))

						Case "HEX(7):"
							sLI_IconType = "_16___Registry___STR"
							sLI_ValueType = "REG_MULTI_SZ"
							sLI_ValueData = convertHEXtoSTRING(sLI_ValueData.Substring(7))

						Case "HEX(B):"
							sLI_IconType = "_16___Registry___HEX"
							sLI_ValueType = "REG_QWORD"
							sLI_ValueData = convertWORDtoVALUE(sLI_ValueData.Substring(7))

						Case Else
							sLI_IconType = "_16___Scan___Failed"
							sLI_ValueType = "UNKNOWN"
							sLI_ValueData = sLI_ValueData.Substring(7)
					End Select
				End If
			Case Else
				sLI_IconType = "_16___Scan___Failed"
				sLI_ValueType = "UNKNOWN"
		End Select

		Dim sResult(2) As String
		sResult(0) = sLI_IconType
		sResult(1) = sLI_ValueType
		sResult(2) = sLI_ValueData
		Return sResult
	End Function

	Private Sub lstImportReg_DragDrop(sender As Object, e As System.Windows.Forms.DragEventArgs) Handles lstImportReg.DragDrop
		Dim sDroppedFiles() As String = CType(e.Data.GetData(DataFormats.FileDrop), String())
		If (sDroppedFiles.Count > 1) Then
			MessageBox.Show(Me, "More than one file was dropped." & vbCrLf & "Only the first one will be used.", APN, MessageBoxButtons.OK, MessageBoxIcon.Information)
		End If
		Call readREGFile(sDroppedFiles(0))
	End Sub

	Private Sub lstImportReg_DragEnter(sender As Object, e As System.Windows.Forms.DragEventArgs) Handles lstImportReg.DragEnter
		e.Effect = DragDropEffects.None
		If (e.Data.GetDataPresent(DataFormats.FileDrop) = True) Then e.Effect = DragDropEffects.Copy
	End Sub

	Private Sub lstImportReg_MouseUp(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles lstImportReg.MouseUp
		If (e.Button <> MouseButtons.Right) Then Exit Sub
		mnuEditSelections = New ContextMenuStrip
		With mnuEditSelections.Items
			.Clear()
			.Add("Select All", My.Resources._16___Select_All.ToBitmap, AddressOf onClick_MenuItem)
			.Add("Select None", My.Resources._16___Select_None.ToBitmap, AddressOf onClick_MenuItem)
			.Add("Invert Selection", My.Resources._16___Select_Invert.ToBitmap, AddressOf onClick_MenuItem)
			.Add("-")
			.Add("Remove Selected", My.Resources._16___Remove.ToBitmap, AddressOf onClick_MenuItem)
		End With
		mnuEditSelections.Show(lstImportReg, e.Location)
	End Sub

	Private Sub onClick_MenuItem(sender As System.Object, e As System.EventArgs)
		Dim mItem As ToolStripItem = CType(sender, ToolStripItem)
		mnuEditSelections.Dispose()
		Select Case mItem.Text
			Case "Remove Selected"
				For Each lItem As ListViewItem In lstImportReg.SelectedItems
					lItem.Remove()
				Next
			Case Else
				For Each lItem As ListViewItem In lstImportReg.Items
					Select Case mItem.Text
						Case "Select All" : lItem.Selected = True
						Case "Select None" : lItem.Selected = False
						Case "Invert Selection" : lItem.Selected = Not lItem.Selected
					End Select
				Next
		End Select
	End Sub

	Private Sub txtRegistryKey_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles txtRegistryKey.KeyDown
		If (e.KeyValue = Keys.Enter) Then
			e.SuppressKeyPress = True
			txtValueName.Focus()
		End If
	End Sub

	Private Sub txtRegistryKey_KeyUp(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles txtRegistryKey.KeyUp
		If (e.Control And e.KeyCode = Keys.A) Then txtRegistryKey.SelectAll()
	End Sub

	Private Sub txtValueData_KeyUp(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles txtValueData.KeyUp
		If (e.Control And e.KeyCode = Keys.A) Then txtValueData.SelectAll()
	End Sub
End Class