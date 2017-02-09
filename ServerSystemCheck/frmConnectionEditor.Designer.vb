<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmConnectionEditor
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
		Me.cmdDel = New System.Windows.Forms.Button()
		Me.cmdAdd = New System.Windows.Forms.Button()
		Me.lstConnectionList = New System.Windows.Forms.ListView()
		Me.lnkHelp = New System.Windows.Forms.LinkLabel()
		Me.picIcon = New System.Windows.Forms.PictureBox()
		Me.cmdCancel = New System.Windows.Forms.Button()
		Me.cmdOK = New System.Windows.Forms.Button()
		Me.Label2 = New System.Windows.Forms.Label()
		Me.lblTitle = New System.Windows.Forms.Label()
		Me.cmdLoadDefaults = New System.Windows.Forms.Button()
		Me.txtSubItemEditor = New System.Windows.Forms.TextBox()
		Me.lblServerSubstitution = New System.Windows.Forms.Label()
		CType(Me.picIcon, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.SuspendLayout()
		'
		'cmdDel
		'
		Me.cmdDel.ImageKey = "(none)"
		Me.cmdDel.Location = New System.Drawing.Point(43, 72)
		Me.cmdDel.Name = "cmdDel"
		Me.cmdDel.Size = New System.Drawing.Size(25, 25)
		Me.cmdDel.TabIndex = 67
		Me.cmdDel.UseVisualStyleBackColor = True
		'
		'cmdAdd
		'
		Me.cmdAdd.ImageKey = "(none)"
		Me.cmdAdd.Location = New System.Drawing.Point(12, 72)
		Me.cmdAdd.Name = "cmdAdd"
		Me.cmdAdd.Size = New System.Drawing.Size(25, 25)
		Me.cmdAdd.TabIndex = 66
		Me.cmdAdd.UseVisualStyleBackColor = True
		'
		'lstConnectionList
		'
		Me.lstConnectionList.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
				  Or System.Windows.Forms.AnchorStyles.Left) _
				  Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.lstConnectionList.Location = New System.Drawing.Point(12, 103)
		Me.lstConnectionList.Margin = New System.Windows.Forms.Padding(3, 9, 3, 3)
		Me.lstConnectionList.Name = "lstConnectionList"
		Me.lstConnectionList.Size = New System.Drawing.Size(470, 167)
		Me.lstConnectionList.TabIndex = 65
		Me.lstConnectionList.UseCompatibleStateImageBehavior = False
		'
		'lnkHelp
		'
		Me.lnkHelp.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.lnkHelp.Location = New System.Drawing.Point(410, 9)
		Me.lnkHelp.Margin = New System.Windows.Forms.Padding(0)
		Me.lnkHelp.Name = "lnkHelp"
		Me.lnkHelp.Size = New System.Drawing.Size(75, 15)
		Me.lnkHelp.TabIndex = 64
		Me.lnkHelp.TabStop = True
		Me.lnkHelp.Text = "Help"
		Me.lnkHelp.TextAlign = System.Drawing.ContentAlignment.TopRight
		'
		'picIcon
		'
		Me.picIcon.Location = New System.Drawing.Point(12, 12)
		Me.picIcon.Name = "picIcon"
		Me.picIcon.Size = New System.Drawing.Size(48, 48)
		Me.picIcon.TabIndex = 63
		Me.picIcon.TabStop = False
		'
		'cmdCancel
		'
		Me.cmdCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
		Me.cmdCancel.Location = New System.Drawing.Point(326, 285)
		Me.cmdCancel.Name = "cmdCancel"
		Me.cmdCancel.Size = New System.Drawing.Size(75, 25)
		Me.cmdCancel.TabIndex = 62
		Me.cmdCancel.Text = "Cancel"
		Me.cmdCancel.UseVisualStyleBackColor = True
		'
		'cmdOK
		'
		Me.cmdOK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.cmdOK.Location = New System.Drawing.Point(407, 285)
		Me.cmdOK.Margin = New System.Windows.Forms.Padding(3, 12, 3, 3)
		Me.cmdOK.Name = "cmdOK"
		Me.cmdOK.Size = New System.Drawing.Size(75, 25)
		Me.cmdOK.TabIndex = 61
		Me.cmdOK.Text = "OK"
		Me.cmdOK.UseVisualStyleBackColor = True
		'
		'Label2
		'
		Me.Label2.AutoSize = True
		Me.Label2.Location = New System.Drawing.Point(66, 40)
		Me.Label2.Margin = New System.Windows.Forms.Padding(3)
		Me.Label2.Name = "Label2"
		Me.Label2.Size = New System.Drawing.Size(209, 13)
		Me.Label2.TabIndex = 60
		Me.Label2.Text = "Configure the connections used for servers"
		'
		'lblTitle
		'
		Me.lblTitle.AutoSize = True
		Me.lblTitle.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.lblTitle.Location = New System.Drawing.Point(66, 18)
		Me.lblTitle.Margin = New System.Windows.Forms.Padding(3, 9, 3, 3)
		Me.lblTitle.Name = "lblTitle"
		Me.lblTitle.Size = New System.Drawing.Size(156, 16)
		Me.lblTitle.TabIndex = 59
		Me.lblTitle.Text = "Server Connection Editor"
		'
		'cmdLoadDefaults
		'
		Me.cmdLoadDefaults.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.cmdLoadDefaults.Location = New System.Drawing.Point(12, 285)
		Me.cmdLoadDefaults.Name = "cmdLoadDefaults"
		Me.cmdLoadDefaults.Size = New System.Drawing.Size(125, 25)
		Me.cmdLoadDefaults.TabIndex = 68
		Me.cmdLoadDefaults.Text = "Load Defaults"
		Me.cmdLoadDefaults.UseVisualStyleBackColor = True
		'
		'txtSubItemEditor
		'
		Me.txtSubItemEditor.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.txtSubItemEditor.Location = New System.Drawing.Point(143, 288)
		Me.txtSubItemEditor.Name = "txtSubItemEditor"
		Me.txtSubItemEditor.Size = New System.Drawing.Size(20, 20)
		Me.txtSubItemEditor.TabIndex = 69
		'
		'lblServerSubstitution
		'
		Me.lblServerSubstitution.AutoSize = True
		Me.lblServerSubstitution.Location = New System.Drawing.Point(100, 78)
		Me.lblServerSubstitution.Margin = New System.Windows.Forms.Padding(24, 0, 3, 0)
		Me.lblServerSubstitution.Name = "lblServerSubstitution"
		Me.lblServerSubstitution.Size = New System.Drawing.Size(210, 13)
		Me.lblServerSubstitution.TabIndex = 70
		Me.lblServerSubstitution.Text = "Use   [svc]   as the servername substitution"
		'
		'frmConnectionEditor
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.CancelButton = Me.cmdCancel
		Me.ClientSize = New System.Drawing.Size(494, 322)
		Me.Controls.Add(Me.lblServerSubstitution)
		Me.Controls.Add(Me.txtSubItemEditor)
		Me.Controls.Add(Me.cmdLoadDefaults)
		Me.Controls.Add(Me.cmdDel)
		Me.Controls.Add(Me.cmdAdd)
		Me.Controls.Add(Me.lstConnectionList)
		Me.Controls.Add(Me.lnkHelp)
		Me.Controls.Add(Me.picIcon)
		Me.Controls.Add(Me.cmdCancel)
		Me.Controls.Add(Me.cmdOK)
		Me.Controls.Add(Me.Label2)
		Me.Controls.Add(Me.lblTitle)
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
		Me.MaximizeBox = False
		Me.MinimizeBox = False
		Me.Name = "frmConnectionEditor"
		Me.ShowIcon = False
		Me.ShowInTaskbar = False
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
		Me.Text = " Server Connection Editor"
		CType(Me.picIcon, System.ComponentModel.ISupportInitialize).EndInit()
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub
	Friend WithEvents cmdDel As System.Windows.Forms.Button
	Friend WithEvents cmdAdd As System.Windows.Forms.Button
	Friend WithEvents lstConnectionList As System.Windows.Forms.ListView
	Friend WithEvents lnkHelp As System.Windows.Forms.LinkLabel
	Friend WithEvents picIcon As System.Windows.Forms.PictureBox
	Friend WithEvents cmdCancel As System.Windows.Forms.Button
	Friend WithEvents cmdOK As System.Windows.Forms.Button
	Friend WithEvents Label2 As System.Windows.Forms.Label
	Friend WithEvents lblTitle As System.Windows.Forms.Label
	Friend WithEvents cmdLoadDefaults As System.Windows.Forms.Button
	Friend WithEvents txtSubItemEditor As System.Windows.Forms.TextBox
	Friend WithEvents lblServerSubstitution As System.Windows.Forms.Label
End Class
