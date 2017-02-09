<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmResourceDuplication
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
		Me.cmdClose = New System.Windows.Forms.Button()
		Me.picIcon = New System.Windows.Forms.PictureBox()
		Me.Label2 = New System.Windows.Forms.Label()
		Me.lblTitle = New System.Windows.Forms.Label()
		Me.lstConflict = New System.Windows.Forms.ListView()
		Me.Label3 = New System.Windows.Forms.Label()
		Me.lnkHelp = New System.Windows.Forms.LinkLabel()
		Me.picIconOverlay = New System.Windows.Forms.PictureBox()
		CType(Me.picIcon, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.picIconOverlay, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.SuspendLayout()
		'
		'cmdClose
		'
		Me.cmdClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.cmdClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
		Me.cmdClose.Location = New System.Drawing.Point(407, 285)
		Me.cmdClose.Margin = New System.Windows.Forms.Padding(12, 12, 3, 3)
		Me.cmdClose.Name = "cmdClose"
		Me.cmdClose.Size = New System.Drawing.Size(75, 25)
		Me.cmdClose.TabIndex = 8
		Me.cmdClose.Text = "Close"
		Me.cmdClose.UseVisualStyleBackColor = True
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
		Me.Label2.Size = New System.Drawing.Size(174, 13)
		Me.Label2.TabIndex = 10
		Me.Label2.Text = "A resource duplication error exists..."
		'
		'lblTitle
		'
		Me.lblTitle.AutoSize = True
		Me.lblTitle.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.lblTitle.Location = New System.Drawing.Point(66, 18)
		Me.lblTitle.Margin = New System.Windows.Forms.Padding(0, 9, 3, 3)
		Me.lblTitle.Name = "lblTitle"
		Me.lblTitle.Size = New System.Drawing.Size(137, 16)
		Me.lblTitle.TabIndex = 9
		Me.lblTitle.Text = "Resource Duplication"
		'
		'lstConflict
		'
		Me.lstConflict.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
				  Or System.Windows.Forms.AnchorStyles.Left) _
				  Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.lstConflict.Location = New System.Drawing.Point(12, 72)
		Me.lstConflict.Margin = New System.Windows.Forms.Padding(3, 9, 3, 3)
		Me.lstConflict.Name = "lstConflict"
		Me.lstConflict.Size = New System.Drawing.Size(470, 198)
		Me.lstConflict.TabIndex = 13
		Me.lstConflict.UseCompatibleStateImageBehavior = False
		'
		'Label3
		'
		Me.Label3.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.Label3.Location = New System.Drawing.Point(12, 276)
		Me.Label3.Margin = New System.Windows.Forms.Padding(3)
		Me.Label3.Name = "Label3"
		Me.Label3.Size = New System.Drawing.Size(380, 34)
		Me.Label3.TabIndex = 14
		Me.Label3.Text = "Edit the existing resource, or remove it before creating a new one."
		Me.Label3.TextAlign = System.Drawing.ContentAlignment.BottomLeft
		'
		'lnkHelp
		'
		Me.lnkHelp.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.lnkHelp.Location = New System.Drawing.Point(410, 9)
		Me.lnkHelp.Margin = New System.Windows.Forms.Padding(0)
		Me.lnkHelp.Name = "lnkHelp"
		Me.lnkHelp.Size = New System.Drawing.Size(75, 15)
		Me.lnkHelp.TabIndex = 41
		Me.lnkHelp.TabStop = True
		Me.lnkHelp.Text = "Help"
		Me.lnkHelp.TextAlign = System.Drawing.ContentAlignment.TopRight
		'
		'picIconOverlay
		'
		Me.picIconOverlay.BackColor = System.Drawing.Color.Transparent
		Me.picIconOverlay.Location = New System.Drawing.Point(44, 44)
		Me.picIconOverlay.Name = "picIconOverlay"
		Me.picIconOverlay.Size = New System.Drawing.Size(16, 16)
		Me.picIconOverlay.TabIndex = 42
		Me.picIconOverlay.TabStop = False
		'
		'frmResourceDuplication
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.CancelButton = Me.cmdClose
		Me.ClientSize = New System.Drawing.Size(494, 322)
		Me.Controls.Add(Me.picIconOverlay)
		Me.Controls.Add(Me.lnkHelp)
		Me.Controls.Add(Me.Label3)
		Me.Controls.Add(Me.lstConflict)
		Me.Controls.Add(Me.picIcon)
		Me.Controls.Add(Me.Label2)
		Me.Controls.Add(Me.lblTitle)
		Me.Controls.Add(Me.cmdClose)
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
		Me.MaximizeBox = False
		Me.MinimizeBox = False
		Me.Name = "frmResourceDuplication"
		Me.ShowIcon = False
		Me.ShowInTaskbar = False
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
		Me.Text = " Resource Duplication Issue"
		CType(Me.picIcon, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.picIconOverlay, System.ComponentModel.ISupportInitialize).EndInit()
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub
	Friend WithEvents cmdClose As System.Windows.Forms.Button
	Friend WithEvents picIcon As System.Windows.Forms.PictureBox
	Friend WithEvents Label2 As System.Windows.Forms.Label
	Friend WithEvents lblTitle As System.Windows.Forms.Label
	Friend WithEvents lstConflict As System.Windows.Forms.ListView
	Friend WithEvents Label3 As System.Windows.Forms.Label
	Friend WithEvents lnkHelp As System.Windows.Forms.LinkLabel
	Friend WithEvents picIconOverlay As System.Windows.Forms.PictureBox
End Class
