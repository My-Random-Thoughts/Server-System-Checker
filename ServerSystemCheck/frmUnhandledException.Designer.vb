<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmUnhandledException
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
		Me.lblSubTitle = New System.Windows.Forms.Label()
		Me.lblTitle = New System.Windows.Forms.Label()
		Me.txtStackTrace = New System.Windows.Forms.TextBox()
		Me.cmdExit = New System.Windows.Forms.Button()
		Me.Label1 = New System.Windows.Forms.Label()
		Me.lnkCopyClipboard = New System.Windows.Forms.LinkLabel()
		CType(Me.picIcon, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.SuspendLayout()
		'
		'picIcon
		'
		Me.picIcon.Location = New System.Drawing.Point(12, 12)
		Me.picIcon.Name = "picIcon"
		Me.picIcon.Size = New System.Drawing.Size(48, 48)
		Me.picIcon.TabIndex = 28
		Me.picIcon.TabStop = False
		'
		'lblSubTitle
		'
		Me.lblSubTitle.AutoSize = True
		Me.lblSubTitle.Location = New System.Drawing.Point(66, 40)
		Me.lblSubTitle.Margin = New System.Windows.Forms.Padding(3)
		Me.lblSubTitle.Name = "lblSubTitle"
		Me.lblSubTitle.Size = New System.Drawing.Size(16, 13)
		Me.lblSubTitle.TabIndex = 27
		Me.lblSubTitle.Text = "..."
		'
		'lblTitle
		'
		Me.lblTitle.AutoSize = True
		Me.lblTitle.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.lblTitle.Location = New System.Drawing.Point(66, 18)
		Me.lblTitle.Margin = New System.Windows.Forms.Padding(3, 9, 3, 3)
		Me.lblTitle.Name = "lblTitle"
		Me.lblTitle.Size = New System.Drawing.Size(209, 16)
		Me.lblTitle.TabIndex = 26
		Me.lblTitle.Text = "An Unhandled Exception Occured"
		'
		'txtStackTrace
		'
		Me.txtStackTrace.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
				  Or System.Windows.Forms.AnchorStyles.Left) _
				  Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.txtStackTrace.Location = New System.Drawing.Point(12, 72)
		Me.txtStackTrace.Margin = New System.Windows.Forms.Padding(3, 9, 3, 3)
		Me.txtStackTrace.Multiline = True
		Me.txtStackTrace.Name = "txtStackTrace"
		Me.txtStackTrace.ScrollBars = System.Windows.Forms.ScrollBars.Both
		Me.txtStackTrace.Size = New System.Drawing.Size(568, 246)
		Me.txtStackTrace.TabIndex = 29
		Me.txtStackTrace.WordWrap = False
		'
		'cmdExit
		'
		Me.cmdExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.cmdExit.Location = New System.Drawing.Point(455, 333)
		Me.cmdExit.Margin = New System.Windows.Forms.Padding(3, 12, 3, 3)
		Me.cmdExit.Name = "cmdExit"
		Me.cmdExit.Size = New System.Drawing.Size(125, 25)
		Me.cmdExit.TabIndex = 30
		Me.cmdExit.Text = "Exit Application"
		Me.cmdExit.UseVisualStyleBackColor = True
		'
		'Label1
		'
		Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.Label1.Location = New System.Drawing.Point(12, 324)
		Me.Label1.Margin = New System.Windows.Forms.Padding(3)
		Me.Label1.Name = "Label1"
		Me.Label1.Size = New System.Drawing.Size(300, 34)
		Me.Label1.TabIndex = 33
		Me.Label1.Text = "Please file a bug request if this is a reproducible issue." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Error log saved to ap" & _
		  "plication folder."
		Me.Label1.TextAlign = System.Drawing.ContentAlignment.BottomLeft
		'
		'lnkCopyClipboard
		'
		Me.lnkCopyClipboard.Location = New System.Drawing.Point(275, 333)
		Me.lnkCopyClipboard.Margin = New System.Windows.Forms.Padding(3)
		Me.lnkCopyClipboard.Name = "lnkCopyClipboard"
		Me.lnkCopyClipboard.Size = New System.Drawing.Size(174, 25)
		Me.lnkCopyClipboard.TabIndex = 35
		Me.lnkCopyClipboard.TabStop = True
		Me.lnkCopyClipboard.Text = "Copy error to the clipboard"
		Me.lnkCopyClipboard.TextAlign = System.Drawing.ContentAlignment.BottomRight
		'
		'frmUnhandledException
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(592, 370)
		Me.ControlBox = False
		Me.Controls.Add(Me.lnkCopyClipboard)
		Me.Controls.Add(Me.Label1)
		Me.Controls.Add(Me.cmdExit)
		Me.Controls.Add(Me.txtStackTrace)
		Me.Controls.Add(Me.picIcon)
		Me.Controls.Add(Me.lblSubTitle)
		Me.Controls.Add(Me.lblTitle)
		Me.MinimizeBox = False
		Me.MinimumSize = New System.Drawing.Size(550, 200)
		Me.Name = "frmUnhandledException"
		Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
		Me.Text = " Unhandled Exception"
		CType(Me.picIcon, System.ComponentModel.ISupportInitialize).EndInit()
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub
	Friend WithEvents picIcon As System.Windows.Forms.PictureBox
	Friend WithEvents lblSubTitle As System.Windows.Forms.Label
	Friend WithEvents lblTitle As System.Windows.Forms.Label
	Friend WithEvents txtStackTrace As System.Windows.Forms.TextBox
	Friend WithEvents cmdExit As System.Windows.Forms.Button
	Friend WithEvents Label1 As System.Windows.Forms.Label
	Friend WithEvents lnkCopyClipboard As System.Windows.Forms.LinkLabel
End Class
