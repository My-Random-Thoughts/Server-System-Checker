<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPropertiesServerGetAll
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
		Me.picIcon = New System.Windows.Forms.PictureBox()
		Me.lblSubTitle = New System.Windows.Forms.Label()
		Me.lblTitle = New System.Windows.Forms.Label()
		Me.cmdClose = New System.Windows.Forms.Button()
		Me.proProgress = New System.Windows.Forms.ProgressBar()
		Me.lstServerGroups = New ServerSystemChecker.ctrlListView_SubIcons()
		Me.lblStatus = New System.Windows.Forms.Label()
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
		Me.lnkHelp.TabIndex = 54
		Me.lnkHelp.TabStop = True
		Me.lnkHelp.Text = "Help"
		Me.lnkHelp.TextAlign = System.Drawing.ContentAlignment.TopRight
		'
		'picIcon
		'
		Me.picIcon.Location = New System.Drawing.Point(12, 12)
		Me.picIcon.Name = "picIcon"
		Me.picIcon.Size = New System.Drawing.Size(48, 48)
		Me.picIcon.TabIndex = 53
		Me.picIcon.TabStop = False
		'
		'lblSubTitle
		'
		Me.lblSubTitle.AutoSize = True
		Me.lblSubTitle.Location = New System.Drawing.Point(66, 40)
		Me.lblSubTitle.Margin = New System.Windows.Forms.Padding(3)
		Me.lblSubTitle.Name = "lblSubTitle"
		Me.lblSubTitle.Size = New System.Drawing.Size(201, 13)
		Me.lblSubTitle.TabIndex = 52
		Me.lblSubTitle.Text = "Please wait for the process to complete..."
		'
		'lblTitle
		'
		Me.lblTitle.AutoSize = True
		Me.lblTitle.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.lblTitle.Location = New System.Drawing.Point(66, 18)
		Me.lblTitle.Margin = New System.Windows.Forms.Padding(3, 9, 3, 3)
		Me.lblTitle.Name = "lblTitle"
		Me.lblTitle.Size = New System.Drawing.Size(155, 16)
		Me.lblTitle.TabIndex = 51
		Me.lblTitle.Text = "Get All Server Properties"
		'
		'cmdClose
		'
		Me.cmdClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.cmdClose.Location = New System.Drawing.Point(307, 385)
		Me.cmdClose.Margin = New System.Windows.Forms.Padding(3, 12, 3, 3)
		Me.cmdClose.Name = "cmdClose"
		Me.cmdClose.Size = New System.Drawing.Size(75, 25)
		Me.cmdClose.TabIndex = 50
		Me.cmdClose.Text = "Close"
		Me.cmdClose.UseVisualStyleBackColor = True
		'
		'proProgress
		'
		Me.proProgress.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
				  Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.proProgress.Location = New System.Drawing.Point(12, 397)
		Me.proProgress.Margin = New System.Windows.Forms.Padding(3, 3, 9, 3)
		Me.proProgress.Name = "proProgress"
		Me.proProgress.Size = New System.Drawing.Size(283, 13)
		Me.proProgress.Style = System.Windows.Forms.ProgressBarStyle.Continuous
		Me.proProgress.TabIndex = 56
		'
		'lstServerGroups
		'
		Me.lstServerGroups.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
				  Or System.Windows.Forms.AnchorStyles.Left) _
				  Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.lstServerGroups.Location = New System.Drawing.Point(12, 72)
		Me.lstServerGroups.Margin = New System.Windows.Forms.Padding(3, 9, 3, 3)
		Me.lstServerGroups.Name = "lstServerGroups"
		Me.lstServerGroups.OwnerDraw = True
		Me.lstServerGroups.Size = New System.Drawing.Size(370, 298)
		Me.lstServerGroups.TabIndex = 57
		Me.lstServerGroups.UseCompatibleStateImageBehavior = False
		'
		'lblStatus
		'
		Me.lblStatus.AutoSize = True
		Me.lblStatus.Location = New System.Drawing.Point(12, 380)
		Me.lblStatus.Margin = New System.Windows.Forms.Padding(3, 3, 3, 1)
		Me.lblStatus.Name = "lblStatus"
		Me.lblStatus.Size = New System.Drawing.Size(47, 13)
		Me.lblStatus.TabIndex = 58
		Me.lblStatus.Text = "lblStatus"
		Me.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
		'
		'frmPropertiesServerGetAll
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(394, 422)
		Me.Controls.Add(Me.lblStatus)
		Me.Controls.Add(Me.lstServerGroups)
		Me.Controls.Add(Me.proProgress)
		Me.Controls.Add(Me.lnkHelp)
		Me.Controls.Add(Me.picIcon)
		Me.Controls.Add(Me.lblSubTitle)
		Me.Controls.Add(Me.lblTitle)
		Me.Controls.Add(Me.cmdClose)
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
		Me.MaximizeBox = False
		Me.MinimizeBox = False
		Me.Name = "frmPropertiesServerGetAll"
		Me.ShowIcon = False
		Me.ShowInTaskbar = False
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
		Me.Text = " Get All Server Properties"
		CType(Me.picIcon, System.ComponentModel.ISupportInitialize).EndInit()
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub
	Friend WithEvents lnkHelp As System.Windows.Forms.LinkLabel
	Friend WithEvents picIcon As System.Windows.Forms.PictureBox
	Friend WithEvents lblSubTitle As System.Windows.Forms.Label
	Friend WithEvents lblTitle As System.Windows.Forms.Label
	Friend WithEvents cmdClose As System.Windows.Forms.Button
	Friend WithEvents proProgress As System.Windows.Forms.ProgressBar
	Friend WithEvents lstServerGroups As ServerSystemChecker.ctrlListView_SubIcons
	Friend WithEvents lblStatus As System.Windows.Forms.Label
End Class
