<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmServerDuplication
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
		Me.lblTitle = New System.Windows.Forms.Label()
		Me.lnkHelp = New System.Windows.Forms.LinkLabel()
		Me.cmdCancel = New System.Windows.Forms.Button()
		Me.Label2 = New System.Windows.Forms.Label()
		Me.cmdRemoveLeft = New System.Windows.Forms.Button()
		Me.cmdRemoveRight = New System.Windows.Forms.Button()
		Me.lstServerList = New ServerSystemChecker.ctrlListView_SubIcons()
		CType(Me.picIcon, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.SuspendLayout()
		'
		'picIcon
		'
		Me.picIcon.Location = New System.Drawing.Point(12, 12)
		Me.picIcon.Name = "picIcon"
		Me.picIcon.Size = New System.Drawing.Size(48, 48)
		Me.picIcon.TabIndex = 48
		Me.picIcon.TabStop = False
		'
		'lblTitle
		'
		Me.lblTitle.AutoSize = True
		Me.lblTitle.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.lblTitle.Location = New System.Drawing.Point(66, 18)
		Me.lblTitle.Margin = New System.Windows.Forms.Padding(3, 9, 3, 3)
		Me.lblTitle.Name = "lblTitle"
		Me.lblTitle.Size = New System.Drawing.Size(118, 16)
		Me.lblTitle.TabIndex = 46
		Me.lblTitle.Text = "Server Duplication"
		'
		'lnkHelp
		'
		Me.lnkHelp.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.lnkHelp.Enabled = False
		Me.lnkHelp.Location = New System.Drawing.Point(510, 9)
		Me.lnkHelp.Margin = New System.Windows.Forms.Padding(0)
		Me.lnkHelp.Name = "lnkHelp"
		Me.lnkHelp.Size = New System.Drawing.Size(75, 15)
		Me.lnkHelp.TabIndex = 66
		Me.lnkHelp.TabStop = True
		Me.lnkHelp.Text = "Help"
		Me.lnkHelp.TextAlign = System.Drawing.ContentAlignment.TopRight
		'
		'cmdCancel
		'
		Me.cmdCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
		Me.cmdCancel.Location = New System.Drawing.Point(507, 335)
		Me.cmdCancel.Margin = New System.Windows.Forms.Padding(12, 12, 3, 3)
		Me.cmdCancel.Name = "cmdCancel"
		Me.cmdCancel.Size = New System.Drawing.Size(75, 25)
		Me.cmdCancel.TabIndex = 67
		Me.cmdCancel.Text = "Cancel"
		Me.cmdCancel.UseVisualStyleBackColor = True
		'
		'Label2
		'
		Me.Label2.AutoSize = True
		Me.Label2.Location = New System.Drawing.Point(66, 40)
		Me.Label2.Margin = New System.Windows.Forms.Padding(3)
		Me.Label2.Name = "Label2"
		Me.Label2.Size = New System.Drawing.Size(257, 13)
		Me.Label2.TabIndex = 69
		Me.Label2.Text = "Select which duplicated server you want to remove..."
		'
		'cmdRemoveLeft
		'
		Me.cmdRemoveLeft.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.cmdRemoveLeft.Location = New System.Drawing.Point(170, 295)
		Me.cmdRemoveLeft.Margin = New System.Windows.Forms.Padding(3, 0, 3, 3)
		Me.cmdRemoveLeft.Name = "cmdRemoveLeft"
		Me.cmdRemoveLeft.Size = New System.Drawing.Size(200, 25)
		Me.cmdRemoveLeft.TabIndex = 71
		Me.cmdRemoveLeft.Text = "Remove"
		Me.cmdRemoveLeft.UseVisualStyleBackColor = True
		'
		'cmdRemoveRight
		'
		Me.cmdRemoveRight.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.cmdRemoveRight.Location = New System.Drawing.Point(376, 295)
		Me.cmdRemoveRight.Margin = New System.Windows.Forms.Padding(3, 0, 3, 3)
		Me.cmdRemoveRight.Name = "cmdRemoveRight"
		Me.cmdRemoveRight.Size = New System.Drawing.Size(200, 25)
		Me.cmdRemoveRight.TabIndex = 72
		Me.cmdRemoveRight.Text = "Remove"
		Me.cmdRemoveRight.UseVisualStyleBackColor = True
		'
		'lstServerList
		'
		Me.lstServerList.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
				  Or System.Windows.Forms.AnchorStyles.Left) _
				  Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.lstServerList.Location = New System.Drawing.Point(12, 72)
		Me.lstServerList.Margin = New System.Windows.Forms.Padding(3, 9, 3, 3)
		Me.lstServerList.Name = "lstServerList"
		Me.lstServerList.OwnerDraw = True
		Me.lstServerList.Size = New System.Drawing.Size(570, 220)
		Me.lstServerList.TabIndex = 49
		Me.lstServerList.UseCompatibleStateImageBehavior = False
		'
		'frmServerDuplication
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.CancelButton = Me.cmdCancel
		Me.ClientSize = New System.Drawing.Size(594, 372)
		Me.Controls.Add(Me.cmdRemoveRight)
		Me.Controls.Add(Me.cmdRemoveLeft)
		Me.Controls.Add(Me.Label2)
		Me.Controls.Add(Me.cmdCancel)
		Me.Controls.Add(Me.lnkHelp)
		Me.Controls.Add(Me.lstServerList)
		Me.Controls.Add(Me.picIcon)
		Me.Controls.Add(Me.lblTitle)
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
		Me.MaximizeBox = False
		Me.MinimizeBox = False
		Me.Name = "frmServerDuplication"
		Me.ShowIcon = False
		Me.ShowInTaskbar = False
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
		Me.Text = " Server Duplication Properties"
		CType(Me.picIcon, System.ComponentModel.ISupportInitialize).EndInit()
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub
	Friend WithEvents picIcon As System.Windows.Forms.PictureBox
	Friend WithEvents lblTitle As System.Windows.Forms.Label
	Friend WithEvents lstServerList As ServerSystemChecker.ctrlListView_SubIcons
	Friend WithEvents lnkHelp As System.Windows.Forms.LinkLabel
	Friend WithEvents cmdCancel As System.Windows.Forms.Button
	Friend WithEvents Label2 As System.Windows.Forms.Label
	Friend WithEvents cmdRemoveLeft As System.Windows.Forms.Button
	Friend WithEvents cmdRemoveRight As System.Windows.Forms.Button
End Class
