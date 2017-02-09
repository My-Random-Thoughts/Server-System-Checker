Option Explicit On

Public Class frmConnectionEditor
	Private bCancelEdit As Boolean
	Private lvwC As ListViewColumnSorter
	Private img16 As New ImageList
	Private imgSPACING As New ImageList

	Dim lLVItem As ListViewItem
	Dim lSubItem As ListViewItem.ListViewSubItem

	Private Sub frmConnections_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
		For Each c As Control In Controls
			c.Font = frmMain.sysFont
		Next
		lblTitle.Font = frmMain.sysFontTitle
		lnkHelp.Font = frmMain.sysFontHelp

		lnkHelp.LinkBehavior = LinkBehavior.HoverUnderline
		picIcon.Image = My.Resources._48___Server.ToBitmap
		lblServerSubstitution.Text = "Use   " & sServerSubstitution & "   as the server name substitution"

		lvwC = New ListViewColumnSorter()
		With img16
			.ImageSize = New Size(16, 16)
			.ColorDepth = ColorDepth.Depth32Bit
			With .Images
				.Clear()
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

		cmdAdd.Image = My.Resources._16___Add.ToBitmap
		cmdDel.Image = My.Resources._16___Remove.ToBitmap
		txtSubItemEditor.Visible = False

		With lstConnectionList
			.Clear()
			.View = View.Details
			.SmallImageList = imgSPACING
			With .Columns
				.Add("name", "Name", 150)
				.Add("cmmd", "Command", lstConnectionList.Width - 150 - SystemInformation.VerticalScrollBarWidth - 4)
			End With
			.FullRowSelect = True
			.HeaderStyle = ColumnHeaderStyle.Nonclickable
			.LabelEdit = True
			.ListViewItemSorter = lvwC
		End With
		lvwC.SortColumn = lstConnectionList.Columns("name").Index
		lvwC.Order = SortOrder.Ascending

		Dim cResults As List(Of String) = xml_LoadConnections(Nothing)
		If ((cResults IsNot Nothing) AndAlso (cResults.Count > 0)) Then
			For Each cItem As String In cResults
				Call addItem(cItem.Split("|"c)(0), cItem.Split("|"c)(1), False)
			Next
		End If
		picIcon.Focus()
		If (bUseVisualStyles = True) Then
			Call SetWindowTheme(lstConnectionList.Handle, "Explorer", Nothing)
		End If

		Me.Visible = True
		Application.DoEvents()
	End Sub

	Private Sub cmdCancel_Click(sender As System.Object, e As System.EventArgs) Handles cmdCancel.Click
		img16.Dispose()
		imgSPACING.Dispose()
		Me.Close()
		Me.Dispose()
	End Sub

	Private Sub cmdAdd_Click(sender As System.Object, e As System.EventArgs) Handles cmdAdd.Click
		Call addItem("", sServerSubstitution, True)
	End Sub

	Private Sub cmdDel_Click(sender As System.Object, e As System.EventArgs) Handles cmdDel.Click
		If (lstConnectionList.SelectedItems.Count = 0) Then Exit Sub
		Dim sResult As DialogResult = MessageBox.Show(Me, "Are you sure you want to delete the selected connections.?", APN, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
		If sResult = Windows.Forms.DialogResult.No Then
			lstConnectionList.Focus()
			Exit Sub
		End If
		For Each lItem As ListViewItem In lstConnectionList.SelectedItems
			lItem.Remove()
		Next
		lstConnectionList.Focus()
	End Sub

	Private Sub addItem(ByVal sItemText As String, ByVal sSubItemText As String, ByVal bEditItem As Boolean)
		If ((sItemText <> vbNullString) AndAlso (sSubItemText <> vbNullString)) Then
			If (lstConnectionList.Items.Find(sItemText, True).Count > 0) Then Exit Sub
		End If

		Dim lItem As New ListViewItem(sItemText)
		lItem.UseItemStyleForSubItems = False
		lItem.ImageKey = "_16___Connection"
		lItem.Name = Guid.NewGuid.ToString.Substring(0, 8)
		lItem.SubItems.Add(sSubItemText)
		lstConnectionList.Items.Add(lItem)
		If (bEditItem = True) Then lItem.BeginEdit()
	End Sub

	Private Sub lstConnectionList_AfterLabelEdit(sender As Object, e As System.Windows.Forms.LabelEditEventArgs) Handles lstConnectionList.AfterLabelEdit
		If (e.Label = Nothing) Then e.CancelEdit = True
		If (e.Label = String.Empty) Then e.CancelEdit = True
		If ((e.CancelEdit = True) And (lstConnectionList.Items(e.Item).Text = vbNullString)) Then lstConnectionList.Items(e.Item).Remove()
		If e.CancelEdit = True Then Exit Sub

		e.CancelEdit = True
		lstConnectionList.Items(e.Item).Text = e.Label

		Dim r As Rectangle = lstConnectionList.Items(e.Item).SubItems(1).Bounds
		Dim m As New MouseEventArgs(Windows.Forms.MouseButtons.Left, 1, r.X + 2, r.Y + 2, 0)
		Call lstConnectionList_MouseDoubleClick(sender, m)
		m = Nothing
		r = Nothing
	End Sub

	Private Sub lstConnectionList_MouseDoubleClick(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles lstConnectionList.MouseDoubleClick
		lLVItem = lstConnectionList.GetItemAt(e.X, e.Y)
		If (lLVItem Is Nothing) Then Exit Sub
		lSubItem = lLVItem.GetSubItemAt(e.X, e.Y)

		If (lLVItem.SubItems.IndexOf(lSubItem) = 0) Then
			lLVItem.BeginEdit()
			Exit Sub
		End If

		Select Case lLVItem.SubItems.IndexOf(lSubItem)
			Case 1
				Dim lLeft = lSubItem.Bounds.Left + 2
				Dim lWidth As Integer = lSubItem.Bounds.Width
				With txtSubItemEditor
					.SetBounds(lLeft + lstConnectionList.Left, lSubItem.Bounds.Top + lstConnectionList.Top, lWidth, lSubItem.Bounds.Height)
					.Text = lSubItem.Text
					.Show()
					.Focus()
				End With
		End Select
	End Sub

	Private Sub txtSubItemEditor_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles txtSubItemEditor.KeyPress
		Select Case e.KeyChar
			Case ChrW(Keys.Return)
				bCancelEdit = False
				e.Handled = True
				txtSubItemEditor.Hide()

			Case ChrW(Keys.Escape)
				bCancelEdit = True
				e.Handled = True
				txtSubItemEditor.Hide()
		End Select
	End Sub

	Private Sub txtSubItemEditor_LostFocus(sender As Object, e As System.EventArgs) Handles txtSubItemEditor.LostFocus
		txtSubItemEditor.Hide()
		If (bCancelEdit = False) Then
			lSubItem.Text = txtSubItemEditor.Text
			Call validateConnection(txtSubItemEditor.Text)
		Else
			bCancelEdit = False
		End If
		lstConnectionList.Focus()
	End Sub

	Private Sub validateConnection(ByVal sInput As String)
		Dim bFailed As Boolean = False
		If (sInput Is Nothing) Then bFailed = True
		If (sInput = vbNullString) Then bFailed = True
		If (InStr(sInput, sServerSubstitution) < 0) Then bFailed = True

		If (bFailed = False) Then Exit Sub
		MessageBox.Show(Me, "Command string does not contain the required server field ( " & sServerSubstitution & " ).",
						APN, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
	End Sub

	Private Sub cmdLoadDefaults_Click(sender As System.Object, e As System.EventArgs) Handles cmdLoadDefaults.Click
		If (lstConnectionList.Items.Count > 0) Then
			Dim sResult As DialogResult = MessageBox.Show(Me, "This will remove all currently configured connections." & vbCrLf & _
															  "Are you sure you want to continue.?", _
															  APN, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
			If (sResult = Windows.Forms.DialogResult.No) Then Exit Sub
		End If

		lstConnectionList.Items.Clear()
		Call addItem("RDP", "mstsc.exe -v:#" & sServerSubstitution, False)
		Call addItem("HTTP", "http://" & sServerSubstitution, False)
		Call addItem("iDRAC", "https://" & sServerSubstitution & ":443", False)
		Call addItem("HP SMH", "https://" & sServerSubstitution & ":2381", False)
		lstConnectionList.Focus()
	End Sub

	Private Sub cmdOK_Click(sender As System.Object, e As System.EventArgs) Handles cmdOK.Click
		Dim sList As New List(Of String)
		For Each itm As ListViewItem In lstConnectionList.Items
			sList.Add(itm.Text & "|" & itm.Name & "|" & itm.SubItems(1).Text)
		Next
		If (xml_AddConnections(sList) = True) Then
			img16.Dispose()
			imgSPACING.Dispose()
			Me.Close()
			Me.Dispose()
		End If
	End Sub

	Private Sub lnkHelp_LinkClicked(sender As System.Object, e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lnkHelp.LinkClicked
		frmHelp.sSelectPageByID = "help0030"
		frmHelp.ShowDialog(Me)
	End Sub
End Class