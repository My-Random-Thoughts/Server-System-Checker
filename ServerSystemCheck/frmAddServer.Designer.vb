<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAddServer
	Inherits System.Windows.Forms.Form

	'Form overrides dispose to clean up the component list.
	<System.Diagnostics.DebuggerNonUserCode()> _
	Protected Overrides Sub Dispose(ByVal disposing As Boolean)
		Try
			If disposing AndAlso components IsNot Nothing Then
				components.Dispose()
			End If
		Finally
			MyBase.Dispose(disposing)
		End Try
	End Sub

	'Required by the Windows Form Designer
	Private components As System.ComponentModel.IContainer

	'NOTE: The following procedure is required by the Windows Form Designer
	'It can be modified using the Windows Form Designer.  
	'Do not modify it using the code editor.
	<System.Diagnostics.DebuggerStepThrough()> _
	Private Sub InitializeComponent()
		Me.picIcon = New System.Windows.Forms.PictureBox()
		Me.Label2 = New System.Windows.Forms.Label()
		Me.lblTitle = New System.Windows.Forms.Label()
		Me.lnkHelp = New System.Windows.Forms.LinkLabel()
		Me.cmdCancel = New System.Windows.Forms.Button()
		Me.cmdOK = New System.Windows.Forms.Button()
		Me.cmdDel = New System.Windows.Forms.Button()
		Me.cmdAdd = New System.Windows.Forms.Button()
		Me.lstServerList = New System.Windows.Forms.ListView()
		Me.lblLabel = New System.Windows.Forms.Label()
		Me.proProgress = New System.Windows.Forms.ProgressBar()
		Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
		Me.tvOUList = New System.Windows.Forms.TreeView()
		Me.lstHidden = New System.Windows.Forms.ListBox()
		Me.cmdCloseADResults = New System.Windows.Forms.Button()
		Me.cmdRescanAD = New System.Windows.Forms.Button()
		Me.lstAddOU = New System.Windows.Forms.ListBox()
		Me.chkResolveIP = New System.Windows.Forms.CheckBox()
		Me.cmdSearchAD = New System.Windows.Forms.Button()
		Me.cmoIconComboBox = New ServerSystemChecker.ctrlComboBox_Icons()
		CType(Me.picIcon, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.SplitContainer1.Panel1.SuspendLayout()
		Me.SplitContainer1.Panel2.SuspendLayout()
		Me.SplitContainer1.SuspendLayout()
		Me.SuspendLayout()
		'
		'picIcon
		'
		Me.picIcon.Location = New System.Drawing.Point(12, 12)
		Me.picIcon.Name = "picIcon"
		Me.picIcon.Size = New System.Drawing.Size(48, 48)
		Me.picIcon.TabIndex = 11
		Me.picIcon.TabStop = False
		'
		'Label2
		'
		Me.Label2.AutoSize = True
		Me.Label2.Location = New System.Drawing.Point(66, 40)
		Me.Label2.Margin = New System.Windows.Forms.Padding(3)
		Me.Label2.Name = "Label2"
		Me.Label2.Size = New System.Drawing.Size(222, 13)
		Me.Label2.TabIndex = 10
		Me.Label2.Text = "Enter a list of servers by name or IP address..."
		'
		'lblTitle
		'
		Me.lblTitle.AutoSize = True
		Me.lblTitle.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.lblTitle.Location = New System.Drawing.Point(66, 18)
		Me.lblTitle.Margin = New System.Windows.Forms.Padding(3, 9, 3, 3)
		Me.lblTitle.Name = "lblTitle"
		Me.lblTitle.Size = New System.Drawing.Size(113, 16)
		Me.lblTitle.TabIndex = 9
		Me.lblTitle.Text = "Add New Servers"
		'
		'lnkHelp
		'
		Me.lnkHelp.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.lnkHelp.Location = New System.Drawing.Point(360, 9)
		Me.lnkHelp.Margin = New System.Windows.Forms.Padding(0)
		Me.lnkHelp.Name = "lnkHelp"
		Me.lnkHelp.Size = New System.Drawing.Size(75, 15)
		Me.lnkHelp.TabIndex = 42
		Me.lnkHelp.TabStop = True
		Me.lnkHelp.Text = "Help"
		Me.lnkHelp.TextAlign = System.Drawing.ContentAlignment.TopRight
		'
		'cmdCancel
		'
		Me.cmdCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
		Me.cmdCancel.Location = New System.Drawing.Point(276, 335)
		Me.cmdCancel.Name = "cmdCancel"
		Me.cmdCancel.Size = New System.Drawing.Size(75, 25)
		Me.cmdCancel.TabIndex = 44
		Me.cmdCancel.Text = "Cancel"
		Me.cmdCancel.UseVisualStyleBackColor = True
		'
		'cmdOK
		'
		Me.cmdOK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.cmdOK.Location = New System.Drawing.Point(357, 335)
		Me.cmdOK.Margin = New System.Windows.Forms.Padding(3, 12, 3, 3)
		Me.cmdOK.Name = "cmdOK"
		Me.cmdOK.Size = New System.Drawing.Size(75, 25)
		Me.cmdOK.TabIndex = 43
		Me.cmdOK.Text = "OK"
		Me.cmdOK.UseVisualStyleBackColor = True
		'
		'cmdDel
		'
		Me.cmdDel.ImageKey = "(none)"
		Me.cmdDel.Location = New System.Drawing.Point(43, 72)
		Me.cmdDel.Name = "cmdDel"
		Me.cmdDel.Size = New System.Drawing.Size(25, 25)
		Me.cmdDel.TabIndex = 70
		Me.cmdDel.UseVisualStyleBackColor = True
		'
		'cmdAdd
		'
		Me.cmdAdd.ImageKey = "(none)"
		Me.cmdAdd.Location = New System.Drawing.Point(12, 72)
		Me.cmdAdd.Name = "cmdAdd"
		Me.cmdAdd.Size = New System.Drawing.Size(25, 25)
		Me.cmdAdd.TabIndex = 69
		Me.cmdAdd.UseVisualStyleBackColor = True
		'
		'lstServerList
		'
		Me.lstServerList.Dock = System.Windows.Forms.DockStyle.Fill
		Me.lstServerList.Location = New System.Drawing.Point(0, 0)
		Me.lstServerList.Margin = New System.Windows.Forms.Padding(3, 9, 3, 3)
		Me.lstServerList.Name = "lstServerList"
		Me.lstServerList.Size = New System.Drawing.Size(166, 217)
		Me.lstServerList.TabIndex = 68
		Me.lstServerList.UseCompatibleStateImageBehavior = False
		'
		'lblLabel
		'
		Me.lblLabel.Location = New System.Drawing.Point(74, 72)
		Me.lblLabel.Name = "lblLabel"
		Me.lblLabel.Size = New System.Drawing.Size(102, 25)
		Me.lblLabel.TabIndex = 72
		Me.lblLabel.Text = "Import From :"
		Me.lblLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight
		'
		'proProgress
		'
		Me.proProgress.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
				  Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.proProgress.Location = New System.Drawing.Point(12, 347)
		Me.proProgress.Margin = New System.Windows.Forms.Padding(3, 3, 9, 3)
		Me.proProgress.Name = "proProgress"
		Me.proProgress.Size = New System.Drawing.Size(252, 13)
		Me.proProgress.TabIndex = 75
		'
		'SplitContainer1
		'
		Me.SplitContainer1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
				  Or System.Windows.Forms.AnchorStyles.Left) _
				  Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.SplitContainer1.Location = New System.Drawing.Point(12, 103)
		Me.SplitContainer1.Name = "SplitContainer1"
		'
		'SplitContainer1.Panel1
		'
		Me.SplitContainer1.Panel1.Controls.Add(Me.lstServerList)
		'
		'SplitContainer1.Panel2
		'
		Me.SplitContainer1.Panel2.Controls.Add(Me.tvOUList)
		Me.SplitContainer1.Size = New System.Drawing.Size(420, 217)
		Me.SplitContainer1.SplitterDistance = 166
		Me.SplitContainer1.TabIndex = 76
		'
		'tvOUList
		'
		Me.tvOUList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.tvOUList.Dock = System.Windows.Forms.DockStyle.Fill
		Me.tvOUList.HideSelection = False
		Me.tvOUList.Location = New System.Drawing.Point(0, 0)
		Me.tvOUList.Margin = New System.Windows.Forms.Padding(3, 9, 3, 3)
		Me.tvOUList.Name = "tvOUList"
		Me.tvOUList.Size = New System.Drawing.Size(250, 217)
		Me.tvOUList.TabIndex = 74
		'
		'lstHidden
		'
		Me.lstHidden.FormattingEnabled = True
		Me.lstHidden.Location = New System.Drawing.Point(382, 52)
		Me.lstHidden.Margin = New System.Windows.Forms.Padding(0)
		Me.lstHidden.Name = "lstHidden"
		Me.lstHidden.Size = New System.Drawing.Size(50, 17)
		Me.lstHidden.TabIndex = 78
		Me.lstHidden.Visible = False
		'
		'cmdCloseADResults
		'
		Me.cmdCloseADResults.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.cmdCloseADResults.Location = New System.Drawing.Point(12, 335)
		Me.cmdCloseADResults.Name = "cmdCloseADResults"
		Me.cmdCloseADResults.Size = New System.Drawing.Size(125, 25)
		Me.cmdCloseADResults.TabIndex = 79
		Me.cmdCloseADResults.Text = "Close AD List"
		Me.cmdCloseADResults.UseVisualStyleBackColor = True
		'
		'cmdRescanAD
		'
		Me.cmdRescanAD.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.cmdRescanAD.Location = New System.Drawing.Point(307, 72)
		Me.cmdRescanAD.Name = "cmdRescanAD"
		Me.cmdRescanAD.Size = New System.Drawing.Size(125, 25)
		Me.cmdRescanAD.TabIndex = 81
		Me.cmdRescanAD.Text = "Rescan AD"
		Me.cmdRescanAD.UseVisualStyleBackColor = True
		'
		'lstAddOU
		'
		Me.lstAddOU.FormattingEnabled = True
		Me.lstAddOU.Location = New System.Drawing.Point(332, 52)
		Me.lstAddOU.Margin = New System.Windows.Forms.Padding(0)
		Me.lstAddOU.Name = "lstAddOU"
		Me.lstAddOU.Size = New System.Drawing.Size(50, 17)
		Me.lstAddOU.TabIndex = 82
		Me.lstAddOU.Visible = False
		'
		'chkResolveIP
		'
		Me.chkResolveIP.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.chkResolveIP.AutoSize = True
		Me.chkResolveIP.Location = New System.Drawing.Point(12, 343)
		Me.chkResolveIP.Name = "chkResolveIP"
		Me.chkResolveIP.Size = New System.Drawing.Size(189, 17)
		Me.chkResolveIP.TabIndex = 84
		Me.chkResolveIP.Text = "Automatically resolve IP addresses"
		Me.chkResolveIP.UseVisualStyleBackColor = False
		'
		'cmdSearchAD
		'
		Me.cmdSearchAD.Location = New System.Drawing.Point(182, 72)
		Me.cmdSearchAD.Name = "cmdSearchAD"
		Me.cmdSearchAD.Size = New System.Drawing.Size(25, 25)
		Me.cmdSearchAD.TabIndex = 85
		Me.cmdSearchAD.UseVisualStyleBackColor = True
		'
		'cmoIconComboBox
		'
		Me.cmoIconComboBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
				  Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.cmoIconComboBox.BackColor = System.Drawing.SystemColors.Window
		Me.cmoIconComboBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
		Me.cmoIconComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.cmoIconComboBox.FormattingEnabled = True
		Me.cmoIconComboBox.ItemHeight = 19
		Me.cmoIconComboBox.Location = New System.Drawing.Point(182, 72)
		Me.cmoIconComboBox.Name = "cmoIconComboBox"
		Me.cmoIconComboBox.RemoveIconSpacing = False
		Me.cmoIconComboBox.SelectedItem = Nothing
		Me.cmoIconComboBox.Size = New System.Drawing.Size(250, 25)
		Me.cmoIconComboBox.TabIndex = 71
		'
		'frmAddServer
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(444, 372)
		Me.Controls.Add(Me.cmdSearchAD)
		Me.Controls.Add(Me.chkResolveIP)
		Me.Controls.Add(Me.lstAddOU)
		Me.Controls.Add(Me.lstHidden)
		Me.Controls.Add(Me.cmdCloseADResults)
		Me.Controls.Add(Me.cmdRescanAD)
		Me.Controls.Add(Me.SplitContainer1)
		Me.Controls.Add(Me.lblLabel)
		Me.Controls.Add(Me.cmoIconComboBox)
		Me.Controls.Add(Me.cmdDel)
		Me.Controls.Add(Me.cmdAdd)
		Me.Controls.Add(Me.cmdCancel)
		Me.Controls.Add(Me.cmdOK)
		Me.Controls.Add(Me.lnkHelp)
		Me.Controls.Add(Me.picIcon)
		Me.Controls.Add(Me.Label2)
		Me.Controls.Add(Me.lblTitle)
		Me.Controls.Add(Me.proProgress)
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
		Me.MaximizeBox = False
		Me.MinimizeBox = False
		Me.Name = "frmAddServer"
		Me.ShowIcon = False
		Me.ShowInTaskbar = False
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
		Me.Text = " Add New Servers"
		CType(Me.picIcon, System.ComponentModel.ISupportInitialize).EndInit()
		Me.SplitContainer1.Panel1.ResumeLayout(False)
		Me.SplitContainer1.Panel2.ResumeLayout(False)
		Me.SplitContainer1.ResumeLayout(False)
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub
	Friend WithEvents picIcon As System.Windows.Forms.PictureBox
	Friend WithEvents Label2 As System.Windows.Forms.Label
	Friend WithEvents lblTitle As System.Windows.Forms.Label
	Friend WithEvents lnkHelp As System.Windows.Forms.LinkLabel
	Friend WithEvents cmdCancel As System.Windows.Forms.Button
	Friend WithEvents cmdOK As System.Windows.Forms.Button
	Friend WithEvents cmdDel As System.Windows.Forms.Button
	Friend WithEvents cmdAdd As System.Windows.Forms.Button
	Friend WithEvents lstServerList As System.Windows.Forms.ListView
	Friend WithEvents lblLabel As System.Windows.Forms.Label
	Friend WithEvents cmoIconComboBox As ServerSystemChecker.ctrlComboBox_Icons
	Friend WithEvents proProgress As System.Windows.Forms.ProgressBar
	Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
	Friend WithEvents tvOUList As System.Windows.Forms.TreeView
	Friend WithEvents lstHidden As System.Windows.Forms.ListBox
	Friend WithEvents cmdCloseADResults As System.Windows.Forms.Button
	Friend WithEvents cmdRescanAD As System.Windows.Forms.Button
	Friend WithEvents lstAddOU As System.Windows.Forms.ListBox
	Friend WithEvents chkResolveIP As System.Windows.Forms.CheckBox
	Friend WithEvents cmdSearchAD As System.Windows.Forms.Button
End Class
