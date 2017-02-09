Public Class frmAddServicesChangeState

	Public lvwItem As ListViewItem
	Public sReturnResult As String = vbNullString

	Private Sub frmAddServicesChangeState_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
		For Each c As Control In Controls
			c.Font = frmMain.sysFont
		Next
		lblTitle.Font = frmMain.sysFontTitle
		lnkHelp.Font = frmMain.sysFontHelp
		lnkHelp.LinkBehavior = LinkBehavior.HoverUnderline

		picIcon.Image = My.Resources._48___Services.ToBitmap
		Dim cItem As ctrlComboBox_Icons.IconComboItem
		With cmoChecking
			.ItemHeight = 19
			With .Items
				.Clear()
				cItem = New ctrlComboBox_Icons.IconComboItem("Running")
				cItem.ItemImage = CType(My.Resources.ResourceManager.GetObject("_16___Service___Started"), Drawing.Icon)
				.Add(cItem)

				cItem = New ctrlComboBox_Icons.IconComboItem("Stopped")
				cItem.ItemImage = CType(My.Resources.ResourceManager.GetObject("_16___Service___Stopped"), Drawing.Icon)
				.Add(cItem)
			End With
			.SelectedIndex = -1
		End With

		With cmoMissing
			.ItemHeight = 19
			With .Items
				.Clear()
				cItem = New ctrlComboBox_Icons.IconComboItem("OK")	' <<-- Don't add "(Ignore)" here
				cItem.ItemImage = CType(My.Resources.ResourceManager.GetObject("_16___Scan___Pass"), Drawing.Icon)
				.Add(cItem)

				cItem = New ctrlComboBox_Icons.IconComboItem("Warning")
				cItem.ItemImage = CType(My.Resources.ResourceManager.GetObject("_16___Eventlog___Warning"), Drawing.Icon)
				.Add(cItem)

				cItem = New ctrlComboBox_Icons.IconComboItem("Fail")
				cItem.ItemImage = CType(My.Resources.ResourceManager.GetObject("_16___Scan___Failed"), Drawing.Icon)
				.Add(cItem)
			End With
			.SelectedIndex = -1
		End With
		cItem = Nothing

		lblTitle.Text = "(Missing Service Name)"
		Try
			lblTitle.Text = lvwItem.Text
			cmoChecking.Text = lvwItem.SubItems(2).Text.Split("("c)(0).Trim
			cmoMissing.Text = lvwItem.SubItems(2).Text.Split("("c)(1).Trim(")"c).Trim
		Catch
		End Try

		' Adding "(Ignore)" for user clarification...
		cmoMissing.Items(0).DisplayText = "OK (Ignore)"
	End Sub

	Private Sub cmdOK_Click(sender As System.Object, e As System.EventArgs) Handles cmdOK.Click
		If (cmoChecking.SelectedIndex = -1) Then Exit Sub
		If (cmoMissing.SelectedIndex = -1) Then Exit Sub
		sReturnResult = cmoChecking.Text.Trim & " (" & cmoMissing.Text.Trim.Replace(" (Ignore)", "") & ")"
		Me.Close()
		'Me.Dispose()  <<-- NOT HERE.!
	End Sub

	Private Sub cmdCancel_Click(sender As System.Object, e As System.EventArgs) Handles cmdCancel.Click
		sReturnResult = Nothing
		Me.Close()
		Me.Dispose()
	End Sub

	Private Sub lnkHelp_LinkClicked(sender As System.Object, e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lnkHelp.LinkClicked
		frmHelp.sSelectPageByID = "help0021"
		frmHelp.ShowDialog(Me)
	End Sub
End Class