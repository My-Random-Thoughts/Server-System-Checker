Public Class frmFindServer
	Public cTreeView As TreeView								' SOURCE Data
	Private iResultPointer As Integer = 0
	Private lServerList As New List(Of TreeNode)				' List Of Servers
	Private lQueryList As IQueryable(Of TreeNode) = Nothing		' Query List
	Public lSearchResults As IQueryable(Of TreeNode)			' Search Results

	Private Sub frmFind_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
		If (cTreeView Is Nothing) Then Close()

		For Each c As Control In Controls
			c.Font = frmMain.sysFont
		Next

		picIcon.Image = My.Resources._48___Search.ToBitmap

		iResultPointer = 0
		lSearchResults = Nothing
		lblResultsCount_BL.Visible = True		' Bottom Left Label
		lblResultsCount_BL.Text = vbNullString
		lblResultsCount_TR.Visible = False		' Top Right Label
		lblResultsCount_TR.Text = vbNullString
		picPlaceHolder.Visible = False

		With cmoFind
			.AutoCompleteMode = AutoCompleteMode.Suggest
			.AutoCompleteSource = AutoCompleteSource.ListItems
			.DropDownStyle = ComboBoxStyle.DropDown
		End With

		Call RecurseNodes(cTreeView.Nodes)
		lQueryList = lServerList.AsQueryable
		cmdFind.Enabled = True
		If (cmoFind.Items.Count = 0) Then cmdFind.Enabled = False
	End Sub

	Private Sub frmFind_Activated(sender As Object, e As System.EventArgs) Handles Me.Activated
		If (cTreeView Is Nothing) Then Close()
		Me.CenterToParent()

		If (Me.Controls.Find("btnCustomButton", False).Count = 1) Then
			lblResultsCount_BL.Visible = False
			lblResultsCount_TR.Visible = True
		End If
	End Sub

	' Add all "SERVER" nodes to the list collection
	Private Sub RecurseNodes(ByVal col As TreeNodeCollection)
		For Each tn As TreeNode In col
			If (tn.ImageKey.StartsWith("_16___Server")) Then
				lServerList.Add(tn)
				If (cmoFind.Items.Contains(tn.Text) = False) Then cmoFind.Items.Add(tn.Text)
			End If

			If (tn.Nodes.Count) > 0 Then RecurseNodes(tn.Nodes)
		Next tn
	End Sub

	Private Sub cmdFind_Click(sender As System.Object, e As System.EventArgs) Handles cmdFind.Click
		If (cmdFind.Enabled = False) Then Exit Sub
		If (cmoFind.Text = vbNullString) Then Exit Sub
		Try
			If (lSearchResults Is Nothing) Then
				iResultPointer = 0
				lSearchResults = From Q As TreeNode In lQueryList Where (Q.Text.ToLower.Contains(cmoFind.Text.ToLower)) Select Q
				Dim iCount As Integer = lSearchResults.Count
				lblResultsCount_BL.Text = iCount.ToString & " result" & IIf(iCount = 1, "", "s").ToString
				lblResultsCount_TR.Text = iCount.ToString & " result" & IIf(iCount = 1, "", "s").ToString
				If (iCount = 0) Then Exit Sub
			End If

		Catch ex As Exception
			MessageBox.Show(Me, "There was an error trying to search." & vbCrLf & "The error returned was..." & vbCrLf & vbCrLf & ex.Message, _
			  APN, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Exit Sub
		End Try

		lSearchResults(iResultPointer).EnsureVisible()
		cTreeView.SelectedNode = lSearchResults(iResultPointer)
		iResultPointer = iResultPointer + 1
		If (iResultPointer >= lSearchResults.Count) Then iResultPointer = 0
		cmdFind.Text = "Next >"
		cmdFind.Focus()
	End Sub

	Private Sub cmoFind_KeyUp(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles cmoFind.KeyUp
		If (e.KeyCode = Keys.Enter) Then Call cmdFind_Click(sender, e)
	End Sub

	Private Sub cmoFind_TextChanged(sender As Object, e As System.EventArgs) Handles cmoFind.TextChanged
		lSearchResults = Nothing
		cmdFind.Text = "Search"
		iResultPointer = 0
	End Sub

	Public Sub cmdCancel_Click(sender As System.Object, e As System.EventArgs) Handles cmdCancel.Click
		iResultPointer = 0
		cTreeView = Nothing
		lServerList = Nothing
		lQueryList = Nothing
		lSearchResults = Nothing
		Close()
	End Sub
End Class