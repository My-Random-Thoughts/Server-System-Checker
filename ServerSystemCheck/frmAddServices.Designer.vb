<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAddServices
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
		Me.lblTitle = New System.Windows.Forms.Label()
		Me.picIcon = New System.Windows.Forms.PictureBox()
		Me.Label2 = New System.Windows.Forms.Label()
		Me.lnkHelp = New System.Windows.Forms.LinkLabel()
		Me.cmdShowFullDetails = New System.Windows.Forms.Button()
		Me.lblLabel = New System.Windows.Forms.Label()
		Me.Label10 = New System.Windows.Forms.Label()
		Me.cmoMissingService = New ServerSystemChecker.ctrlComboBox_Icons()
		Me.lstServices = New ServerSystemChecker.ctrlListView_SubIcons()
		Me.cmoIconComboBox = New ServerSystemChecker.ctrlComboBox_Icons()
		CType(Me.picIcon, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.SuspendLayout()
		'
		'cmdCancel
		'
		Me.cmdCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
		Me.cmdCancel.Location = New System.Drawing.Point(276, 435)
		Me.cmdCancel.Name = "cmdCancel"
		Me.cmdCancel.Size = New System.Drawing.Size(75, 25)
		Me.cmdCancel.TabIndex = 1
		Me.cmdCancel.Text = "Cancel"
		Me.cmdCancel.UseVisualStyleBackColor = True
		'
		'cmdOK
		'
		Me.cmdOK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.cmdOK.Location = New System.Drawing.Point(357, 435)
		Me.cmdOK.Margin = New System.Windows.Forms.Padding(3, 12, 3, 3)
		Me.cmdOK.Name = "cmdOK"
		Me.cmdOK.Size = New System.Drawing.Size(75, 25)
		Me.cmdOK.TabIndex = 2
		Me.cmdOK.Text = "OK"
		Me.cmdOK.UseVisualStyleBackColor = True
		'
		'lblTitle
		'
		Me.lblTitle.AutoSize = True
		Me.lblTitle.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.lblTitle.Location = New System.Drawing.Point(66, 18)
		Me.lblTitle.Margin = New System.Windows.Forms.Padding(3, 9, 0, 3)
		Me.lblTitle.Name = "lblTitle"
		Me.lblTitle.Size = New System.Drawing.Size(147, 16)
		Me.lblTitle.TabIndex = 6
		Me.lblTitle.Text = "Add Windows Services"
		'
		'picIcon
		'
		Me.picIcon.Location = New System.Drawing.Point(12, 12)
		Me.picIcon.Name = "picIcon"
		Me.picIcon.Size = New System.Drawing.Size(48, 48)
		Me.picIcon.TabIndex = 9
		Me.picIcon.TabStop = False
		'
		'Label2
		'
		Me.Label2.AutoSize = True
		Me.Label2.Location = New System.Drawing.Point(66, 40)
		Me.Label2.Margin = New System.Windows.Forms.Padding(3)
		Me.Label2.Name = "Label2"
		Me.Label2.Size = New System.Drawing.Size(180, 13)
		Me.Label2.TabIndex = 10
		Me.Label2.Text = "Use CTRL to select multiple services"
		'
		'lnkHelp
		'
		Me.lnkHelp.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.lnkHelp.Location = New System.Drawing.Point(360, 9)
		Me.lnkHelp.Margin = New System.Windows.Forms.Padding(0)
		Me.lnkHelp.Name = "lnkHelp"
		Me.lnkHelp.Size = New System.Drawing.Size(75, 15)
		Me.lnkHelp.TabIndex = 41
		Me.lnkHelp.TabStop = True
		Me.lnkHelp.Text = "Help"
		Me.lnkHelp.TextAlign = System.Drawing.ContentAlignment.TopRight
		'
		'cmdShowFullDetails
		'
		Me.cmdShowFullDetails.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.cmdShowFullDetails.Location = New System.Drawing.Point(12, 435)
		Me.cmdShowFullDetails.Margin = New System.Windows.Forms.Padding(3, 3, 9, 3)
		Me.cmdShowFullDetails.Name = "cmdShowFullDetails"
		Me.cmdShowFullDetails.Size = New System.Drawing.Size(125, 25)
		Me.cmdShowFullDetails.TabIndex = 43
		Me.cmdShowFullDetails.Text = "More Details  >>"
		Me.cmdShowFullDetails.UseVisualStyleBackColor = True
		'
		'lblLabel
		'
		Me.lblLabel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.lblLabel.Location = New System.Drawing.Point(74, 72)
		Me.lblLabel.Name = "lblLabel"
		Me.lblLabel.Size = New System.Drawing.Size(102, 25)
		Me.lblLabel.TabIndex = 73
		Me.lblLabel.Text = "Import From :"
		Me.lblLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight
		'
		'Label10
		'
		Me.Label10.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.Label10.AutoSize = True
		Me.Label10.Location = New System.Drawing.Point(12, 401)
		Me.Label10.Margin = New System.Windows.Forms.Padding(3)
		Me.Label10.Name = "Label10"
		Me.Label10.Size = New System.Drawing.Size(186, 13)
		Me.Label10.TabIndex = 76
		Me.Label10.Text = "Select scan result for missing service :"
		'
		'cmoMissingService
		'
		Me.cmoMissingService.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.cmoMissingService.BackColor = System.Drawing.SystemColors.Window
		Me.cmoMissingService.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
		Me.cmoMissingService.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.cmoMissingService.FormattingEnabled = True
		Me.cmoMissingService.ItemHeight = 19
		Me.cmoMissingService.Location = New System.Drawing.Point(307, 395)
		Me.cmoMissingService.Margin = New System.Windows.Forms.Padding(3, 9, 3, 3)
		Me.cmoMissingService.Name = "cmoMissingService"
		Me.cmoMissingService.RemoveIconSpacing = False
		Me.cmoMissingService.SelectedItem = Nothing
		Me.cmoMissingService.Size = New System.Drawing.Size(125, 25)
		Me.cmoMissingService.TabIndex = 75
		'
		'lstServices
		'
		Me.lstServices.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
				  Or System.Windows.Forms.AnchorStyles.Left) _
				  Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.lstServices.Location = New System.Drawing.Point(12, 103)
		Me.lstServices.Name = "lstServices"
		Me.lstServices.OwnerDraw = True
		Me.lstServices.Size = New System.Drawing.Size(420, 280)
		Me.lstServices.TabIndex = 74
		Me.lstServices.UseCompatibleStateImageBehavior = False
		'
		'cmoIconComboBox
		'
		Me.cmoIconComboBox.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.cmoIconComboBox.BackColor = System.Drawing.SystemColors.Window
		Me.cmoIconComboBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
		Me.cmoIconComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.cmoIconComboBox.FormattingEnabled = True
		Me.cmoIconComboBox.ItemHeight = 19
		Me.cmoIconComboBox.Location = New System.Drawing.Point(182, 72)
		Me.cmoIconComboBox.Name = "cmoIconComboBox"
		Me.cmoIconComboBox.RemoveIconSpacing = False
		Me.cmoIconComboBox.SelectedItem = Nothing
		Me.cmoIconComboBox.Size = New System.Drawing.Size(250, 25)
		Me.cmoIconComboBox.TabIndex = 12
		'
		'frmAddServices
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.CancelButton = Me.cmdCancel
		Me.ClientSize = New System.Drawing.Size(444, 472)
		Me.Controls.Add(Me.cmoMissingService)
		Me.Controls.Add(Me.Label10)
		Me.Controls.Add(Me.lstServices)
		Me.Controls.Add(Me.lblLabel)
		Me.Controls.Add(Me.cmdShowFullDetails)
		Me.Controls.Add(Me.cmoIconComboBox)
		Me.Controls.Add(Me.lnkHelp)
		Me.Controls.Add(Me.Label2)
		Me.Controls.Add(Me.picIcon)
		Me.Controls.Add(Me.lblTitle)
		Me.Controls.Add(Me.cmdOK)
		Me.Controls.Add(Me.cmdCancel)
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
		Me.MaximizeBox = False
		Me.MinimizeBox = False
		Me.Name = "frmAddServices"
		Me.ShowIcon = False
		Me.ShowInTaskbar = False
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
		Me.Text = " Add Windows Services"
		CType(Me.picIcon, System.ComponentModel.ISupportInitialize).EndInit()
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub
	Friend WithEvents cmdCancel As System.Windows.Forms.Button
	Friend WithEvents cmdOK As System.Windows.Forms.Button
	Friend WithEvents lblTitle As System.Windows.Forms.Label
	Friend WithEvents picIcon As System.Windows.Forms.PictureBox
	Friend WithEvents Label2 As System.Windows.Forms.Label
	Friend WithEvents cmoIconComboBox As ServerSystemChecker.ctrlComboBox_Icons
	Friend WithEvents lnkHelp As System.Windows.Forms.LinkLabel
	Friend WithEvents cmdShowFullDetails As System.Windows.Forms.Button
	Friend WithEvents lblLabel As System.Windows.Forms.Label
	Friend WithEvents lstServices As ServerSystemChecker.ctrlListView_SubIcons
	Friend WithEvents cmoMissingService As ServerSystemChecker.ctrlComboBox_Icons
	Friend WithEvents Label10 As System.Windows.Forms.Label
End Class
