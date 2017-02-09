<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmServiceControl
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
		Me.proProgress = New System.Windows.Forms.ProgressBar()
		Me.lblStatus = New System.Windows.Forms.Label()
		CType(Me.picIcon, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.SuspendLayout()
		'
		'picIcon
		'
		Me.picIcon.Location = New System.Drawing.Point(12, 12)
		Me.picIcon.Name = "picIcon"
		Me.picIcon.Size = New System.Drawing.Size(48, 48)
		Me.picIcon.TabIndex = 37
		Me.picIcon.TabStop = False
		'
		'lblTitle
		'
		Me.lblTitle.AutoSize = True
		Me.lblTitle.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.lblTitle.Location = New System.Drawing.Point(66, 12)
		Me.lblTitle.Margin = New System.Windows.Forms.Padding(3, 9, 3, 3)
		Me.lblTitle.Name = "lblTitle"
		Me.lblTitle.Size = New System.Drawing.Size(48, 16)
		Me.lblTitle.TabIndex = 35
		Me.lblTitle.Text = "lblTitle"
		'
		'proProgress
		'
		Me.proProgress.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
				  Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.proProgress.Location = New System.Drawing.Point(66, 55)
		Me.proProgress.Name = "proProgress"
		Me.proProgress.Size = New System.Drawing.Size(266, 13)
		Me.proProgress.TabIndex = 38
		'
		'lblStatus
		'
		Me.lblStatus.Location = New System.Drawing.Point(66, 34)
		Me.lblStatus.Margin = New System.Windows.Forms.Padding(3, 3, 3, 1)
		Me.lblStatus.Name = "lblStatus"
		Me.lblStatus.Size = New System.Drawing.Size(266, 17)
		Me.lblStatus.TabIndex = 39
		Me.lblStatus.Text = "lblStatus"
		Me.lblStatus.TextAlign = System.Drawing.ContentAlignment.BottomLeft
		'
		'frmServiceControl
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(344, 80)
		Me.ControlBox = False
		Me.Controls.Add(Me.lblStatus)
		Me.Controls.Add(Me.proProgress)
		Me.Controls.Add(Me.picIcon)
		Me.Controls.Add(Me.lblTitle)
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
		Me.MaximizeBox = False
		Me.MinimizeBox = False
		Me.Name = "frmServiceControl"
		Me.ShowIcon = False
		Me.ShowInTaskbar = False
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
		Me.Text = " Service Control"
		CType(Me.picIcon, System.ComponentModel.ISupportInitialize).EndInit()
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub
	Friend WithEvents picIcon As System.Windows.Forms.PictureBox
	Friend WithEvents lblTitle As System.Windows.Forms.Label
	Friend WithEvents proProgress As System.Windows.Forms.ProgressBar
	Friend WithEvents lblStatus As System.Windows.Forms.Label
End Class
