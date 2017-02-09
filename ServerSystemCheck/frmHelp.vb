Option Explicit On

Imports System.Resources

Public Class frmHelp
	Private img16 As New ImageList
	Public sSelectPageByID As String
	Private mnuExpandCollapse As New ContextMenuStrip

	Private Sub frmHelp_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
		For Each c As Control In Controls
			c.Font = frmMain.sysFont
		Next

		With img16
			.ImageSize = New Size(16, 16)
			.ColorDepth = ColorDepth.Depth32Bit
			With .Images
				.Clear()
				.Add("_16___Help___Closed", My.Resources._16___Book___Closed___Help)
				.Add("_16___Help___Open", My.Resources._16___Book___Open___Help)
			End With
		End With

		cmdCancel.SendToBack()		' We don't see this, only use it for the ESC key to cancel

		With tvwHelp
			.ImageList = img16
			.ItemHeight = 20
			.Nodes.Clear()
			.ShowRootLines = True
			.ShowPlusMinus = True
			.ShowLines = False
		End With

		With SplitContainer1
			.SplitterDistance = 250
			.FixedPanel = FixedPanel.Panel1
		End With

		With webHelpText
			.AllowWebBrowserDrop = False
			.IsWebBrowserContextMenuEnabled = False
			.ScriptErrorsSuppressed = True
			.WebBrowserShortcutsEnabled = False
		End With

		With mnuExpandCollapse
			.Items.Clear()
			.Items.Add("Expand All", My.Resources.Expand_ico.ToBitmap, AddressOf onClick_Expand)
			.Items.Add("Collapse All", My.Resources.Collapse_ico.ToBitmap, AddressOf onClick_Collapse)
		End With

		Call createHELP()
		lblStatusLabel.Text = vbNullString

		If (bUseVisualStyles = True) Then
			Call SetWindowTheme(tvwHelp.Handle, "Explorer", Nothing)
		End If

		' Load the selected page, if specified...
		Me.Cursor = Cursors.WaitCursor
		If (sSelectPageByID IsNot Nothing) Then
			Dim sFind() As TreeNode = tvwHelp.Nodes.Find(sSelectPageByID, True)
			If ((sFind IsNot Nothing) AndAlso (sFind.Count > 0)) Then
				tvwHelp.SelectedNode = sFind(0)
				tvwHelp.SelectedNode.EnsureVisible()
				tvwHelp.SelectedNode.Expand()
			Else
				tvwHelp.SelectedNode = tvwHelp.Nodes("help0000")
				tvwHelp.Nodes("help0000").Expand()
			End If
		Else
			tvwHelp.SelectedNode = tvwHelp.Nodes("help0000")
			tvwHelp.Nodes("help0000").Expand()
		End If

		Me.Cursor = Cursors.Default
		Me.Visible = True
	End Sub

	' #############################################################################################
	Private Sub createHELP()
		'        Topic Title (indents here for readability only)    Page        Parent
		'       ----------------------------------------------------------------------------
		addHelp("Introduction                                   ", "help0000", "        ")
		addHelp("    Getting Started                            ", "help0001", "help0000")
		addHelp("    Running As Administrator                   ", "help0022", "help0000")

		addHelp("Main Menu                                      ", "help0003", "        ")
		addHelp("    Settings                                   ", "help0013", "help0003")

		addHelp("Add Group                                      ", "help0004", "        ")
		addHelp("    Group Properties                           ", "help0011", "help0004")
		addHelp("        Get All Server Properties              ", "help0029", "help0011")
		addHelp("        Server Statistics                      ", "help0014", "help0011")

		addHelp("Add Server                                     ", "help0005", "        ")
		addHelp("    Multi-Select Servers                       ", "help0034", "help0005")
		addHelp("    Server Properties                          ", "help0012", "help0005")
		addHelp("        Exclude Drives From Scanning           ", "help0033", "help0012")

		addHelp("Add Resource                                   ", "help0006", "        ")
		addHelp("    Eventlog Scan                              ", "help0009", "help0006")
		addHelp("    File Check                                 ", "help0026", "help0006")
		addHelp("    Free Space Threshold                       ", "help0010", "help0006")
		addHelp("    Hotfix Patch Check                         ", "help0008", "help0006")
		addHelp("    Registry Scan                              ", "help0023", "help0006")
		addHelp("    Windows Services                           ", "help0007", "help0006")
		addHelp("    WMI Query									", "help0035", "help0006")
		addHelp("        WMI Examples							", "help0037", "help0035")

		addHelp("Resource Options								", "help0027", "        ")
		addHelp("    Change State			                    ", "help0021", "help0027")
		addHelp("    Resource Duplication		                ", "help0024", "help0027")
		addHelp("    Move Or Copy Resources			            ", "help0028", "help0027")

		addHelp("Scan Group                                     ", "help0017", "        ")
		addHelp("    Detailed Scan Report                       ", "help0018", "help0017")
		addHelp("        EventLog Exclusions                    ", "help0025", "help0018")
		addHelp("    Export Result Summary                      ", "help0019", "help0017")

		addHelp("Miscellaneous                                  ", "help0020", "        ")
		addHelp("    Server Connection Editor                   ", "help0030", "help0020")
		addHelp("    Icons And Their Meanings                   ", "help0031", "help0020")
		addHelp("    Command Line Options                       ", "help0032", "help0020")
		addHelp("    Read-Only File Restrictions                ", "help0036", "help0020")

		' Next FREE Numbers: 15, 16, 38
	End Sub
	' #############################################################################################

	Public Sub addHelp(ByVal sFolderName As String, ByVal sFolderID As String, ByVal sParentFolderID As String)
		Dim tvNode As New TreeNode
		Dim tvParent As New TreeNode
		With tvNode
			.Text = Trim(sFolderName)
			.ImageKey = "_16___Help___Closed"
			.SelectedImageKey = "_16___Help___Open"
			.Name = Trim(sFolderID)
			.Tag = Trim(sFolderID)
		End With

		If (Trim(sParentFolderID) = vbNullString) Then
			tvwHelp.Nodes.Add(tvNode)
		Else
			tvParent = tvwHelp.Nodes.Find(Trim(sParentFolderID), True)(0)
			tvParent.Nodes.Add(tvNode)
		End If
		Application.DoEvents()
		tvNode = Nothing
		tvParent = Nothing
	End Sub

	Private Sub tvwHelp_AfterSelect(sender As Object, e As System.Windows.Forms.TreeViewEventArgs) Handles tvwHelp.AfterSelect
		Dim tvItem As TreeNode = tvwHelp.SelectedNode
		If (tvItem Is Nothing) Then Exit Sub
		If (tvItem.Tag.ToString = vbNullString) Then
			webHelpText.DocumentText = vbNullString
			Exit Sub
		End If

		Me.Cursor = Cursors.WaitCursor
		Dim sSource As String
		Try
			Dim sBreadcrumb As String = tvItem.FullPath.Replace(tvwHelp.PathSeparator, "&nbsp; &gt; &nbsp;")
			lblStatusLabel.Text = tvItem.FullPath.Replace(tvwHelp.PathSeparator, "  >  ")

			sSource = My.Resources.ResourceManager.GetObject(tvItem.Tag.ToString).ToString
			sSource = sSource.Replace("<!-- STYLESHEET -->", "<style>" & vbCrLf & My.Resources.stylesheet & vbCrLf & "</style>")
			sSource = sSource.Replace("<!-- BREADCRUMB -->", sBreadcrumb)
		Catch ex As Exception
			webHelpText.DocumentText = "Missing Text (" & tvItem.Tag.ToString & ")"
			Me.Cursor = Cursors.Default
			Exit Sub
		End Try

		' Search for an insert images from the resources...
		Dim iPosS As Integer = sSource.IndexOf("data:image/png;base64,")
		If (iPosS > 0) Then
			Do While iPosS > 0
				Dim iPosE As Integer = sSource.IndexOf(Chr(34), iPosS)
				Dim sImage As String = sSource.Substring(iPosS + 22, iPosE - (iPosS + 22))
				Dim iImage As Image = Nothing
				Dim base64 As String = vbNullString

				Dim tmpObj As Object = My.Resources.ResourceManager.GetObject(sImage)
				If (tmpObj IsNot Nothing) Then
					Select Case tmpObj.GetType.ToString
						Case "System.Drawing.Icon"
							iImage = CType(My.Resources.ResourceManager.GetObject(sImage), Icon).ToBitmap
							base64 = ImageToBase64String(iImage, Imaging.ImageFormat.Png)

						Case "System.Drawing.Bitmap"
							iImage = CType(My.Resources.ResourceManager.GetObject(sImage), Bitmap)
							base64 = ImageToBase64String(iImage, Imaging.ImageFormat.Jpeg)

						Case Else
							iImage = Nothing
							Me.Cursor = Cursors.Default
							MessageBox.Show(Me, "frmHelp (tvwHelp_AfterSelect): " & vbCrLf & tmpObj.GetType.ToString, APN, MessageBoxButtons.OK, MessageBoxIcon.Error)
					End Select
				End If

				If (iImage Is Nothing) Then base64 = Chr(34) & " alt=" & Chr(34) & "Image '" & sImage & "' Missing"
				sSource = sSource.Substring(0, iPosS + 22) & base64 & sSource.Substring(iPosE)
				iPosS = sSource.IndexOf("data:image/png;base64,", iPosE + 1)
			Loop
		End If

		webHelpText.DocumentText = sSource
		Me.Cursor = Cursors.Default
	End Sub

	Private Sub cmdCancel_Click(sender As System.Object, e As System.EventArgs) Handles cmdCancel.Click
		sSelectPageByID = Nothing
		mnuExpandCollapse.Dispose()
		img16.Dispose()
		Me.Close()
		Me.Dispose()
	End Sub
	Private Sub frmHelp_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
		sSelectPageByID = Nothing
	End Sub

	Private Sub onClick_Expand(sender As System.Object, e As System.EventArgs)
		For Each tvw As TreeNode In tvwHelp.Nodes
			tvw.ExpandAll()
		Next
	End Sub

	Private Sub onClick_Collapse(sender As System.Object, e As System.EventArgs)
		For Each tvw As TreeNode In tvwHelp.Nodes
			tvw.Collapse()
		Next
	End Sub

	Private Sub tvwHelp_MouseUp(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles tvwHelp.MouseUp
		If (e.Button = MouseButtons.Right) Then mnuExpandCollapse.Show(tvwHelp, e.Location)
	End Sub
End Class