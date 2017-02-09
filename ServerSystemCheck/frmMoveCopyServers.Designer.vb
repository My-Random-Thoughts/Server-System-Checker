<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMoveCopyServers
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
		Me.lnkHelp = New System.Windows.Forms.LinkLabel()
		Me.lblSubTitle = New System.Windows.Forms.Label()
		Me.lblTitle = New System.Windows.Forms.Label()
		Me.picIcon = New System.Windows.Forms.PictureBox()
		Me.cmdCancel = New System.Windows.Forms.Button()
		Me.cmdManage = New System.Windows.Forms.Button()
		Me.lstDestination = New System.Windows.Forms.ListView()
		Me.lstHidden = New System.Windows.Forms.ListBox()
		Me.chkCopyNotMove = New System.Windows.Forms.CheckBox()
		Me.lstTreeView = New ServerSystemChecker.ctrlListView_SubIcons()
		CType(Me.picIcon, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.SuspendLayout()
		'
		'lnkHelp
		'
		Me.lnkHelp.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.lnkHelp.Location = New System.Drawing.Point(310, 9)
		Me.lnkHelp.Margin = New System.Windows.Forms.Padding(0)
		Me.lnkHelp.Name = "lnkHelp"
		Me.lnkHelp.Size = New System.Drawing.Size(75, 15)
		Me.lnkHelp.TabIndex = 55
		Me.lnkHelp.TabStop = True
		Me.lnkHelp.Text = "Help"
		Me.lnkHelp.TextAlign = System.Drawing.ContentAlignment.TopRight
		'
		'lblSubTitle
		'
		Me.lblSubTitle.AutoSize = True
		Me.lblSubTitle.Location = New System.Drawing.Point(66, 40)
		Me.lblSubTitle.Margin = New System.Windows.Forms.Padding(3)
		Me.lblSubTitle.Name = "lblSubTitle"
		Me.lblSubTitle.Size = New System.Drawing.Size(200, 13)
		Me.lblSubTitle.TabIndex = 54
		Me.lblSubTitle.Text = "Select the servers you want to manage..."
		'
		'lblTitle
		'
		Me.lblTitle.AutoSize = True
		Me.lblTitle.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.lblTitle.Location = New System.Drawing.Point(66, 18)
		Me.lblTitle.Margin = New System.Windows.Forms.Padding(3, 9, 3, 3)
		Me.lblTitle.Name = "lblTitle"
		Me.lblTitle.Size = New System.Drawing.Size(96, 16)
		Me.lblTitle.TabIndex = 53
		Me.lblTitle.Text = "Select Servers"
		'
		'picIcon
		'
		Me.picIcon.Location = New System.Drawing.Point(12, 12)
		Me.picIcon.Name = "picIcon"
		Me.picIcon.Size = New System.Drawing.Size(48, 48)
		Me.picIcon.TabIndex = 52
		Me.picIcon.TabStop = False
		'
		'cmdCancel
		'
		Me.cmdCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.cmdCancel.Location = New System.Drawing.Point(226, 385)
		Me.cmdCancel.Name = "cmdCancel"
		Me.cmdCancel.Size = New System.Drawing.Size(75, 25)
		Me.cmdCancel.TabIndex = 51
		Me.cmdCancel.Text = "Cancel"
		Me.cmdCancel.UseVisualStyleBackColor = True
		'
		'cmdManage
		'
		Me.cmdManage.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.cmdManage.Location = New System.Drawing.Point(307, 385)
		Me.cmdManage.Margin = New System.Windows.Forms.Padding(3, 12, 3, 3)
		Me.cmdManage.Name = "cmdManage"
		Me.cmdManage.Size = New System.Drawing.Size(75, 25)
		Me.cmdManage.TabIndex = 50
		Me.cmdManage.Text = "Next  >"
		Me.cmdManage.UseVisualStyleBackColor = True
		'
		'lstDestination
		'
		Me.lstDestination.Location = New System.Drawing.Point(12, 72)
		Me.lstDestination.Name = "lstDestination"
		Me.lstDestination.Size = New System.Drawing.Size(150, 150)
		Me.lstDestination.TabIndex = 58
		Me.lstDestination.UseCompatibleStateImageBehavior = False
		'
		'lstHidden
		'
		Me.lstHidden.FormattingEnabled = True
		Me.lstHidden.Location = New System.Drawing.Point(18, 78)
		Me.lstHidden.Name = "lstHidden"
		Me.lstHidden.Size = New System.Drawing.Size(50, 17)
		Me.lstHidden.TabIndex = 79
		Me.lstHidden.Visible = False
		'
		'chkCopyNotMove
		'
		Me.chkCopyNotMove.AutoSize = True
		Me.chkCopyNotMove.Location = New System.Drawing.Point(12, 390)
		Me.chkCopyNotMove.Name = "chkCopyNotMove"
		Me.chkCopyNotMove.Size = New System.Drawing.Size(130, 17)
		Me.chkCopyNotMove.TabIndex = 80
		Me.chkCopyNotMove.Text = "Copy selected servers"
		Me.chkCopyNotMove.UseVisualStyleBackColor = True
		'
		'lstTreeView
		'
		Me.lstTreeView.Location = New System.Drawing.Point(12, 72)
		Me.lstTreeView.Margin = New System.Windows.Forms.Padding(3, 9, 3, 3)
		Me.lstTreeView.Name = "lstTreeView"
		Me.lstTreeView.OwnerDraw = True
		Me.lstTreeView.Size = New System.Drawing.Size(370, 298)
		Me.lstTreeView.TabIndex = 57
		Me.lstTreeView.UseCompatibleStateImageBehavior = False
		'
		'frmMoveCopyServers
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(394, 422)
		Me.Controls.Add(Me.chkCopyNotMove)
		Me.Controls.Add(Me.lstHidden)
		Me.Controls.Add(Me.lstDestination)
		Me.Controls.Add(Me.lstTreeView)
		Me.Controls.Add(Me.lnkHelp)
		Me.Controls.Add(Me.lblSubTitle)
		Me.Controls.Add(Me.lblTitle)
		Me.Controls.Add(Me.picIcon)
		Me.Controls.Add(Me.cmdCancel)
		Me.Controls.Add(Me.cmdManage)
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
		Me.MaximizeBox = False
		Me.MinimizeBox = False
		Me.Name = "frmMoveCopyServers"
		Me.ShowIcon = False
		Me.ShowInTaskbar = False
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
		Me.Text = " Server Move/Copy..."
		CType(Me.picIcon, System.ComponentModel.ISupportInitialize).EndInit()
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub
	Friend WithEvents lnkHelp As System.Windows.Forms.LinkLabel
	Friend WithEvents lblSubTitle As System.Windows.Forms.Label
	Friend WithEvents lblTitle As System.Windows.Forms.Label
	Friend WithEvents picIcon As System.Windows.Forms.PictureBox
	Friend WithEvents cmdCancel As System.Windows.Forms.Button
	Friend WithEvents cmdManage As System.Windows.Forms.Button
	Friend WithEvents lstTreeView As ServerSystemChecker.ctrlListView_SubIcons
	Friend WithEvents lstDestination As System.Windows.Forms.ListView
	Friend WithEvents lstHidden As System.Windows.Forms.ListBox
	Friend WithEvents chkCopyNotMove As System.Windows.Forms.CheckBox
End Class
