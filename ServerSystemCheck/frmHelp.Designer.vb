<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmHelp
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
		Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
		Me.cmdCancel = New System.Windows.Forms.Button()
		Me.tvwHelp = New System.Windows.Forms.TreeView()
		Me.webHelpText = New System.Windows.Forms.WebBrowser()
		Me.lblStatusLabel = New System.Windows.Forms.ToolStripStatusLabel()
		Me.SplitContainer1.Panel1.SuspendLayout()
		Me.SplitContainer1.Panel2.SuspendLayout()
		Me.SplitContainer1.SuspendLayout()
		Me.SuspendLayout()
		'
		'SplitContainer1
		'
		Me.SplitContainer1.BackColor = System.Drawing.SystemColors.Control
		Me.SplitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
		Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
		Me.SplitContainer1.IsSplitterFixed = True
		Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
		Me.SplitContainer1.Name = "SplitContainer1"
		'
		'SplitContainer1.Panel1
		'
		Me.SplitContainer1.Panel1.Controls.Add(Me.cmdCancel)
		Me.SplitContainer1.Panel1.Controls.Add(Me.tvwHelp)
		'
		'SplitContainer1.Panel2
		'
		Me.SplitContainer1.Panel2.Controls.Add(Me.webHelpText)
		Me.SplitContainer1.Size = New System.Drawing.Size(842, 520)
		Me.SplitContainer1.SplitterDistance = 250
		Me.SplitContainer1.TabIndex = 2
		'
		'cmdCancel
		'
		Me.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
		Me.cmdCancel.Location = New System.Drawing.Point(12, 12)
		Me.cmdCancel.Name = "cmdCancel"
		Me.cmdCancel.Size = New System.Drawing.Size(125, 25)
		Me.cmdCancel.TabIndex = 3
		Me.cmdCancel.Text = "Cancel (hidden)"
		Me.cmdCancel.UseVisualStyleBackColor = True
		'
		'tvwHelp
		'
		Me.tvwHelp.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.tvwHelp.Dock = System.Windows.Forms.DockStyle.Fill
		Me.tvwHelp.Location = New System.Drawing.Point(0, 0)
		Me.tvwHelp.Name = "tvwHelp"
		Me.tvwHelp.Size = New System.Drawing.Size(246, 516)
		Me.tvwHelp.TabIndex = 0
		'
		'webHelpText
		'
		Me.webHelpText.Dock = System.Windows.Forms.DockStyle.Fill
		Me.webHelpText.Location = New System.Drawing.Point(0, 0)
		Me.webHelpText.MinimumSize = New System.Drawing.Size(20, 20)
		Me.webHelpText.Name = "webHelpText"
		Me.webHelpText.Size = New System.Drawing.Size(584, 516)
		Me.webHelpText.TabIndex = 0
		'
		'lblStatusLabel
		'
		Me.lblStatusLabel.Name = "lblStatusLabel"
		Me.lblStatusLabel.Size = New System.Drawing.Size(827, 17)
		Me.lblStatusLabel.Spring = True
		Me.lblStatusLabel.Text = "lblStatusLabel"
		Me.lblStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
		'
		'frmHelp
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.CancelButton = Me.cmdCancel
		Me.ClientSize = New System.Drawing.Size(842, 520)
		Me.Controls.Add(Me.SplitContainer1)
		Me.MinimumSize = New System.Drawing.Size(850, 550)
		Me.Name = "frmHelp"
		Me.ShowIcon = False
		Me.ShowInTaskbar = False
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
		Me.Text = " Help"
		Me.SplitContainer1.Panel1.ResumeLayout(False)
		Me.SplitContainer1.Panel2.ResumeLayout(False)
		Me.SplitContainer1.ResumeLayout(False)
		Me.ResumeLayout(False)

	End Sub
	Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
	Friend WithEvents tvwHelp As System.Windows.Forms.TreeView
	Friend WithEvents webHelpText As System.Windows.Forms.WebBrowser
	Friend WithEvents cmdCancel As System.Windows.Forms.Button
	Friend WithEvents lblStatusLabel As System.Windows.Forms.ToolStripStatusLabel
End Class
