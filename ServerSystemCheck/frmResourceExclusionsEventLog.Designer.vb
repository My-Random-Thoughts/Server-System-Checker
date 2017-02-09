<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmResourceExclusionsEventLog
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
		Me.lstConflict = New System.Windows.Forms.ListView()
		Me.picIcon = New System.Windows.Forms.PictureBox()
		Me.Label2 = New System.Windows.Forms.Label()
		Me.lblTitle = New System.Windows.Forms.Label()
		Me.cmdCancel = New System.Windows.Forms.Button()
		CType(Me.picIcon, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.SuspendLayout()
		'
		'lnkHelp
		'
		Me.lnkHelp.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.lnkHelp.Location = New System.Drawing.Point(510, 9)
		Me.lnkHelp.Margin = New System.Windows.Forms.Padding(0)
		Me.lnkHelp.Name = "lnkHelp"
		Me.lnkHelp.Size = New System.Drawing.Size(75, 15)
		Me.lnkHelp.TabIndex = 49
		Me.lnkHelp.TabStop = True
		Me.lnkHelp.Text = "Help"
		Me.lnkHelp.TextAlign = System.Drawing.ContentAlignment.TopRight
		'
		'lstConflict
		'
		Me.lstConflict.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
		 Or System.Windows.Forms.AnchorStyles.Left) _
		 Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.lstConflict.Location = New System.Drawing.Point(12, 72)
		Me.lstConflict.Margin = New System.Windows.Forms.Padding(3, 9, 3, 3)
		Me.lstConflict.Name = "lstConflict"
		Me.lstConflict.Size = New System.Drawing.Size(570, 198)
		Me.lstConflict.TabIndex = 47
		Me.lstConflict.UseCompatibleStateImageBehavior = False
		'
		'picIcon
		'
		Me.picIcon.Location = New System.Drawing.Point(12, 12)
		Me.picIcon.Name = "picIcon"
		Me.picIcon.Size = New System.Drawing.Size(48, 48)
		Me.picIcon.TabIndex = 46
		Me.picIcon.TabStop = False
		'
		'Label2
		'
		Me.Label2.AutoSize = True
		Me.Label2.Location = New System.Drawing.Point(66, 40)
		Me.Label2.Margin = New System.Windows.Forms.Padding(3)
		Me.Label2.Name = "Label2"
		Me.Label2.Size = New System.Drawing.Size(227, 13)
		Me.Label2.TabIndex = 45
		Me.Label2.Text = "List of current exclusions for this eventlog scan"
		'
		'lblTitle
		'
		Me.lblTitle.AutoSize = True
		Me.lblTitle.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.lblTitle.Location = New System.Drawing.Point(66, 18)
		Me.lblTitle.Margin = New System.Windows.Forms.Padding(3, 9, 3, 3)
		Me.lblTitle.Name = "lblTitle"
		Me.lblTitle.Size = New System.Drawing.Size(178, 16)
		Me.lblTitle.TabIndex = 44
		Me.lblTitle.Text = "Eventlog Scan Exclusion List"
		'
		'cmdCancel
		'
		Me.cmdCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
		Me.cmdCancel.Location = New System.Drawing.Point(507, 285)
		Me.cmdCancel.Margin = New System.Windows.Forms.Padding(3, 12, 3, 3)
		Me.cmdCancel.Name = "cmdCancel"
		Me.cmdCancel.Size = New System.Drawing.Size(75, 25)
		Me.cmdCancel.TabIndex = 43
		Me.cmdCancel.Text = "Close"
		Me.cmdCancel.UseVisualStyleBackColor = True
		'
		'frmScanResultsExclusions
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.CancelButton = Me.cmdCancel
		Me.ClientSize = New System.Drawing.Size(594, 322)
		Me.Controls.Add(Me.lnkHelp)
		Me.Controls.Add(Me.lstConflict)
		Me.Controls.Add(Me.picIcon)
		Me.Controls.Add(Me.Label2)
		Me.Controls.Add(Me.lblTitle)
		Me.Controls.Add(Me.cmdCancel)
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
		Me.MaximizeBox = False
		Me.MinimizeBox = False
		Me.Name = "frmScanResultsExclusions"
		Me.ShowIcon = False
		Me.ShowInTaskbar = False
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
		Me.Text = " Current Eventlog Exclusion List"
		CType(Me.picIcon, System.ComponentModel.ISupportInitialize).EndInit()
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub
	Friend WithEvents lnkHelp As System.Windows.Forms.LinkLabel
	Friend WithEvents lstConflict As System.Windows.Forms.ListView
	Friend WithEvents picIcon As System.Windows.Forms.PictureBox
	Friend WithEvents Label2 As System.Windows.Forms.Label
	Friend WithEvents lblTitle As System.Windows.Forms.Label
	Friend WithEvents cmdCancel As System.Windows.Forms.Button
End Class
