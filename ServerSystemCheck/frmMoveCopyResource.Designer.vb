<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMoveCopyResource
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
		Me.cmdCancel = New System.Windows.Forms.Button()
		Me.cmdOK = New System.Windows.Forms.Button()
		Me.tvwMoveCopy = New System.Windows.Forms.TreeView()
		Me.picIcon = New System.Windows.Forms.PictureBox()
		Me.lblSubTitle = New System.Windows.Forms.Label()
		Me.lblTitle = New System.Windows.Forms.Label()
		Me.lnkHelp = New System.Windows.Forms.LinkLabel()
		Me.chkCopyNotMove = New System.Windows.Forms.CheckBox()
		Me.lstResults = New ServerSystemChecker.ctrlListView_CollapseGroups()
		CType(Me.picIcon, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.SuspendLayout()
		'
		'cmdCancel
		'
		Me.cmdCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
		Me.cmdCancel.Location = New System.Drawing.Point(226, 385)
		Me.cmdCancel.Name = "cmdCancel"
		Me.cmdCancel.Size = New System.Drawing.Size(75, 25)
		Me.cmdCancel.TabIndex = 2
		Me.cmdCancel.Text = "Cancel"
		Me.cmdCancel.UseVisualStyleBackColor = True
		'
		'cmdOK
		'
		Me.cmdOK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.cmdOK.Location = New System.Drawing.Point(307, 385)
		Me.cmdOK.Margin = New System.Windows.Forms.Padding(3, 12, 3, 3)
		Me.cmdOK.Name = "cmdOK"
		Me.cmdOK.Size = New System.Drawing.Size(75, 25)
		Me.cmdOK.TabIndex = 1
		Me.cmdOK.Text = "Move"
		Me.cmdOK.UseVisualStyleBackColor = True
		'
		'tvwMoveCopy
		'
		Me.tvwMoveCopy.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
				  Or System.Windows.Forms.AnchorStyles.Left) _
				  Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.tvwMoveCopy.Location = New System.Drawing.Point(12, 72)
		Me.tvwMoveCopy.Margin = New System.Windows.Forms.Padding(3, 9, 3, 3)
		Me.tvwMoveCopy.Name = "tvwMoveCopy"
		Me.tvwMoveCopy.Size = New System.Drawing.Size(370, 298)
		Me.tvwMoveCopy.TabIndex = 0
		'
		'picIcon
		'
		Me.picIcon.Location = New System.Drawing.Point(12, 12)
		Me.picIcon.Name = "picIcon"
		Me.picIcon.Size = New System.Drawing.Size(48, 48)
		Me.picIcon.TabIndex = 11
		Me.picIcon.TabStop = False
		'
		'lblSubTitle
		'
		Me.lblSubTitle.AutoSize = True
		Me.lblSubTitle.Location = New System.Drawing.Point(66, 40)
		Me.lblSubTitle.Margin = New System.Windows.Forms.Padding(3)
		Me.lblSubTitle.Name = "lblSubTitle"
		Me.lblSubTitle.Size = New System.Drawing.Size(184, 13)
		Me.lblSubTitle.TabIndex = 46
		Me.lblSubTitle.Text = "Resource duplication will be checked"
		'
		'lblTitle
		'
		Me.lblTitle.AutoSize = True
		Me.lblTitle.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.lblTitle.Location = New System.Drawing.Point(66, 18)
		Me.lblTitle.Margin = New System.Windows.Forms.Padding(3, 9, 3, 3)
		Me.lblTitle.Name = "lblTitle"
		Me.lblTitle.Size = New System.Drawing.Size(116, 16)
		Me.lblTitle.TabIndex = 45
		Me.lblTitle.Text = "Select Destination"
		'
		'lnkHelp
		'
		Me.lnkHelp.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.lnkHelp.Location = New System.Drawing.Point(310, 9)
		Me.lnkHelp.Margin = New System.Windows.Forms.Padding(0)
		Me.lnkHelp.Name = "lnkHelp"
		Me.lnkHelp.Size = New System.Drawing.Size(75, 15)
		Me.lnkHelp.TabIndex = 49
		Me.lnkHelp.TabStop = True
		Me.lnkHelp.Text = "Help"
		Me.lnkHelp.TextAlign = System.Drawing.ContentAlignment.TopRight
		'
		'chkCopyNotMove
		'
		Me.chkCopyNotMove.AutoSize = True
		Me.chkCopyNotMove.Location = New System.Drawing.Point(12, 390)
		Me.chkCopyNotMove.Name = "chkCopyNotMove"
		Me.chkCopyNotMove.Size = New System.Drawing.Size(142, 17)
		Me.chkCopyNotMove.TabIndex = 81
		Me.chkCopyNotMove.Text = "Copy selected resources"
		Me.chkCopyNotMove.UseVisualStyleBackColor = True
		'
		'lstResults
		'
		Me.lstResults.Location = New System.Drawing.Point(12, 72)
		Me.lstResults.Name = "lstResults"
		Me.lstResults.Size = New System.Drawing.Size(150, 150)
		Me.lstResults.TabIndex = 50
		Me.lstResults.UseCompatibleStateImageBehavior = False
		'
		'frmMoveCopyResource
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.CancelButton = Me.cmdCancel
		Me.ClientSize = New System.Drawing.Size(394, 422)
		Me.Controls.Add(Me.chkCopyNotMove)
		Me.Controls.Add(Me.lstResults)
		Me.Controls.Add(Me.lnkHelp)
		Me.Controls.Add(Me.lblSubTitle)
		Me.Controls.Add(Me.lblTitle)
		Me.Controls.Add(Me.picIcon)
		Me.Controls.Add(Me.tvwMoveCopy)
		Me.Controls.Add(Me.cmdCancel)
		Me.Controls.Add(Me.cmdOK)
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
		Me.MaximizeBox = False
		Me.MinimizeBox = False
		Me.Name = "frmMoveCopyResource"
		Me.ShowIcon = False
		Me.ShowInTaskbar = False
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
		Me.Text = " Resource Move/Copy..."
		CType(Me.picIcon, System.ComponentModel.ISupportInitialize).EndInit()
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub
	Friend WithEvents cmdCancel As System.Windows.Forms.Button
	Friend WithEvents cmdOK As System.Windows.Forms.Button
	Friend WithEvents tvwMoveCopy As System.Windows.Forms.TreeView
	Friend WithEvents picIcon As System.Windows.Forms.PictureBox
	Friend WithEvents lblSubTitle As System.Windows.Forms.Label
	Friend WithEvents lblTitle As System.Windows.Forms.Label
	Friend WithEvents lnkHelp As System.Windows.Forms.LinkLabel
	Friend WithEvents lstResults As ServerSystemChecker.ctrlListView_CollapseGroups
	Friend WithEvents chkCopyNotMove As System.Windows.Forms.CheckBox
End Class
