Public Class frmAddWMIQueryWhereBuilder
	Public lExistingItem As ListViewItem

	Private Sub frmAddWMIQueryWhereBuilder_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
		For Each c As Control In Controls
			c.Font = frmMain.sysFont
		Next

		If (lExistingItem IsNot Nothing) Then Me.Text = Me.Text.Replace("Add", "Edit")

		With cmoWhereProperties
			.ItemHeight = 19
			.DropDownHeight = 152
			.RemoveIconSpacing = True
			.Items.Clear()
		End With
		With cmoWhereOperator
			.ItemHeight = 19
			.DropDownHeight = 152
		End With

		Call populateComboBoxes()

		cmdCalculator.Image = My.Resources._16___Calculator.ToBitmap
		cmoWhereProperties.SelectedIndex = -1
		txtWhereFreeText.Text = vbNullString

		If (lExistingItem IsNot Nothing) Then
			cmoWhereProperties.Text = lExistingItem.Text
			cmoWhereOperator.Text = lExistingItem.SubItems(1).Text
			txtWhereFreeText.Text = lExistingItem.SubItems(2).Text
		End If
	End Sub

	Private Sub populateComboBoxes()
		Dim cmoItem As ctrlComboBox_Icons.IconComboItem

		For Each cmd As Control In frmAddWMIQuery.floProperties.Controls
			If (cmd.Name <> "+") Then
				cmoItem = New ctrlComboBox_Icons.IconComboItem(cmd.Text)
				cmoItem.Data = cmd.Text
				cmoWhereProperties.Items.Add(cmoItem)
			End If
		Next

		Dim cItem As ctrlComboBox_Icons.IconComboItem
		With cmoWhereOperator.Items
			.Clear()
			cItem = New ctrlComboBox_Icons.IconComboItem("Equal To")
			cItem.Data = "="
			cItem.ItemImage = CType(My.Resources.ResourceManager.GetObject("_16___Operator___EqualTo"), Drawing.Icon)
			.Add(cItem)

			cItem = New ctrlComboBox_Icons.IconComboItem("Not Equal To")
			cItem.Data = "<>"
			cItem.ItemImage = CType(My.Resources.ResourceManager.GetObject("_16___Operator___NotEqualTo"), Drawing.Icon)
			.Add(cItem)

			cItem = New ctrlComboBox_Icons.IconComboItem("Greater Than")
			cItem.Data = ">"
			cItem.ItemImage = CType(My.Resources.ResourceManager.GetObject("_16___Operator___GreaterThan"), Drawing.Icon)
			.Add(cItem)

			cItem = New ctrlComboBox_Icons.IconComboItem("Greater Than Or Equal To")
			cItem.Data = ">="
			cItem.ItemImage = CType(My.Resources.ResourceManager.GetObject("_16___Operator___GreaterThanOrEqualTo"), Drawing.Icon)
			.Add(cItem)

			cItem = New ctrlComboBox_Icons.IconComboItem("Less Than")
			cItem.Data = "<"
			cItem.ItemImage = CType(My.Resources.ResourceManager.GetObject("_16___Operator___LessThan"), Drawing.Icon)
			.Add(cItem)

			cItem = New ctrlComboBox_Icons.IconComboItem("Less Than Or Equal To")
			cItem.Data = "<="
			cItem.ItemImage = CType(My.Resources.ResourceManager.GetObject("_16___Operator___LessThanOrEqualTo"), Drawing.Icon)
			.Add(cItem)
		End With
		cItem = Nothing

		cmoWhereProperties.SelectedIndex = 0
		cmoWhereOperator.SelectedIndex = 0
		cmoItem = Nothing
	End Sub

	Private Sub cmdCancel_Click(sender As System.Object, e As System.EventArgs) Handles cmdCancel.Click
		lExistingItem = Nothing
		frmAddWMIQuery.lWhereBuilderItem = Nothing
		Me.Close()
		Me.Dispose()
	End Sub

	Private Sub cmdOK_Click(sender As System.Object, e As System.EventArgs) Handles cmdOK.Click
		If (cmoWhereProperties.Text = vbNullString) Then Exit Sub
		If (cmoWhereOperator.Text = vbNullString) Then Exit Sub
		If (txtWhereFreeText.Text = vbNullString) Then Exit Sub

		lExistingItem = Nothing
		lExistingItem = New ListViewItem(cmoWhereProperties.Text)
		lExistingItem.SubItems.Add(cmoWhereOperator.SelectedItem.Data)
		lExistingItem.SubItems.Add(txtWhereFreeText.Text)
		lExistingItem.SubItems.Add("")
		lExistingItem.SubItems.Add("ICON:0")
		frmAddWMIQuery.lWhereBuilderItem = lExistingItem
		Me.Close()
		Me.Dispose()
	End Sub

	Private Sub cmdCalculator_Click(sender As System.Object, e As System.EventArgs) Handles cmdCalculator.Click
		frmAddWMIQueryWhereBuilderCalc.ShowDialog(Me)
	End Sub

	Private Sub txtWhereFreeText_KeyUp(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles txtWhereFreeText.KeyUp
		If (e.KeyCode = Keys.Enter) Then Call cmdOK_Click(sender, e)
	End Sub
End Class