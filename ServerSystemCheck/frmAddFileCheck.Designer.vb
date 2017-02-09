<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAddFileCheck
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
		Me.Label2 = New System.Windows.Forms.Label()
		Me.lblTitle = New System.Windows.Forms.Label()
		Me.cmdCancel = New System.Windows.Forms.Button()
		Me.cmdOK = New System.Windows.Forms.Button()
		Me.txtSelectedFile = New System.Windows.Forms.TextBox()
		Me.lblTop = New System.Windows.Forms.Label()
		Me.cmdBrowse = New System.Windows.Forms.Button()
		Me.dtDate = New System.Windows.Forms.DateTimePicker()
		Me.Label1 = New System.Windows.Forms.Label()
		Me.txtVersion = New System.Windows.Forms.TextBox()
		Me.lblCurrentVersion = New System.Windows.Forms.Label()
		Me.lblCurrentDate = New System.Windows.Forms.Label()
		Me.picCopyVersion = New System.Windows.Forms.PictureBox()
		Me.picCopyDate = New System.Windows.Forms.PictureBox()
		Me.optVersion = New System.Windows.Forms.RadioButton()
		Me.optDateTime = New System.Windows.Forms.RadioButton()
		Me.optNotExists = New System.Windows.Forms.RadioButton()
		Me.optExists = New System.Windows.Forms.RadioButton()
		Me.lblSettingsLabel = New System.Windows.Forms.Label()
		Me.Label5 = New System.Windows.Forms.Label()
		Me.cmoDT_ML = New ServerSystemChecker.ctrlComboBox_Icons()
		Me.cmoDT_BA = New ServerSystemChecker.ctrlComboBox_Icons()
		CType(Me.picIcon,System.ComponentModel.ISupportInitialize).BeginInit
		CType(Me.picCopyVersion,System.ComponentModel.ISupportInitialize).BeginInit
		CType(Me.picCopyDate,System.ComponentModel.ISupportInitialize).BeginInit
		Me.SuspendLayout
		'
		'lnkHelp
		'
		Me.lnkHelp.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.lnkHelp.Location = New System.Drawing.Point(360, 9)
		Me.lnkHelp.Margin = New System.Windows.Forms.Padding(0)
		Me.lnkHelp.Name = "lnkHelp"
		Me.lnkHelp.Size = New System.Drawing.Size(75, 15)
		Me.lnkHelp.TabIndex = 46
		Me.lnkHelp.TabStop = True
		Me.lnkHelp.Text = "Help"
		Me.lnkHelp.TextAlign = System.Drawing.ContentAlignment.TopRight
		'
		'picIcon
		'
		Me.picIcon.Location = New System.Drawing.Point(12, 12)
		Me.picIcon.Name = "picIcon"
		Me.picIcon.Size = New System.Drawing.Size(48, 48)
		Me.picIcon.TabIndex = 45
		Me.picIcon.TabStop = False
		'
		'Label2
		'
		Me.Label2.AutoSize = True
		Me.Label2.Location = New System.Drawing.Point(66, 40)
		Me.Label2.Margin = New System.Windows.Forms.Padding(3)
		Me.Label2.Name = "Label2"
		Me.Label2.Size = New System.Drawing.Size(261, 13)
		Me.Label2.TabIndex = 44
		Me.Label2.Text = "Scan to check if a file exists, it's version or date stamp"
		'
		'lblTitle
		'
		Me.lblTitle.AutoSize = True
		Me.lblTitle.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.lblTitle.Location = New System.Drawing.Point(66, 18)
		Me.lblTitle.Margin = New System.Windows.Forms.Padding(3, 9, 3, 3)
		Me.lblTitle.Name = "lblTitle"
		Me.lblTitle.Size = New System.Drawing.Size(163, 16)
		Me.lblTitle.TabIndex = 43
		Me.lblTitle.Text = "Add New File Check Scan"
		'
		'cmdCancel
		'
		Me.cmdCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
		Me.cmdCancel.Location = New System.Drawing.Point(276, 310)
		Me.cmdCancel.Name = "cmdCancel"
		Me.cmdCancel.Size = New System.Drawing.Size(75, 25)
		Me.cmdCancel.TabIndex = 12
		Me.cmdCancel.Text = "Cancel"
		Me.cmdCancel.UseVisualStyleBackColor = True
		'
		'cmdOK
		'
		Me.cmdOK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.cmdOK.Location = New System.Drawing.Point(357, 310)
		Me.cmdOK.Margin = New System.Windows.Forms.Padding(3, 12, 3, 3)
		Me.cmdOK.Name = "cmdOK"
		Me.cmdOK.Size = New System.Drawing.Size(75, 25)
		Me.cmdOK.TabIndex = 11
		Me.cmdOK.Text = "OK"
		Me.cmdOK.UseVisualStyleBackColor = True
		'
		'txtSelectedFile
		'
		Me.txtSelectedFile.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
				  Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.txtSelectedFile.Location = New System.Drawing.Point(100, 72)
		Me.txtSelectedFile.Margin = New System.Windows.Forms.Padding(3, 3, 1, 3)
		Me.txtSelectedFile.Multiline = True
		Me.txtSelectedFile.Name = "txtSelectedFile"
		Me.txtSelectedFile.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
		Me.txtSelectedFile.Size = New System.Drawing.Size(305, 71)
		Me.txtSelectedFile.TabIndex = 0
		'
		'lblTop
		'
		Me.lblTop.AutoSize = True
		Me.lblTop.Location = New System.Drawing.Point(12, 75)
		Me.lblTop.Margin = New System.Windows.Forms.Padding(3, 12, 3, 9)
		Me.lblTop.Name = "lblTop"
		Me.lblTop.Size = New System.Drawing.Size(74, 13)
		Me.lblTop.TabIndex = 50
		Me.lblTop.Text = "Selected File :"
		'
		'cmdBrowse
		'
		Me.cmdBrowse.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.cmdBrowse.Location = New System.Drawing.Point(407, 72)
		Me.cmdBrowse.Margin = New System.Windows.Forms.Padding(1, 3, 3, 3)
		Me.cmdBrowse.Name = "cmdBrowse"
		Me.cmdBrowse.Size = New System.Drawing.Size(25, 20)
		Me.cmdBrowse.TabIndex = 2
		Me.cmdBrowse.Text = "..."
		Me.cmdBrowse.UseVisualStyleBackColor = True
		'
		'dtDate
		'
		Me.dtDate.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.dtDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
		Me.dtDate.Location = New System.Drawing.Point(308, 219)
		Me.dtDate.MinDate = New Date(2000, 1, 1, 0, 0, 0, 0)
		Me.dtDate.Name = "dtDate"
		Me.dtDate.Size = New System.Drawing.Size(124, 20)
		Me.dtDate.TabIndex = 6
		Me.dtDate.Value = New Date(2013, 8, 8, 0, 0, 0, 0)
		'
		'Label1
		'
		Me.Label1.AutoSize = True
		Me.Label1.Location = New System.Drawing.Point(12, 182)
		Me.Label1.Margin = New System.Windows.Forms.Padding(3, 12, 3, 9)
		Me.Label1.Name = "Label1"
		Me.Label1.Size = New System.Drawing.Size(49, 13)
		Me.Label1.TabIndex = 63
		Me.Label1.Text = "Options :"
		'
		'txtVersion
		'
		Me.txtVersion.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.txtVersion.Location = New System.Drawing.Point(308, 239)
		Me.txtVersion.Margin = New System.Windows.Forms.Padding(3, 6, 3, 3)
		Me.txtVersion.Name = "txtVersion"
		Me.txtVersion.Size = New System.Drawing.Size(124, 20)
		Me.txtVersion.TabIndex = 9
		'
		'lblCurrentVersion
		'
		Me.lblCurrentVersion.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
				  Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.lblCurrentVersion.Location = New System.Drawing.Point(100, 164)
		Me.lblCurrentVersion.Margin = New System.Windows.Forms.Padding(0, 1, 3, 1)
		Me.lblCurrentVersion.Name = "lblCurrentVersion"
		Me.lblCurrentVersion.Size = New System.Drawing.Size(305, 15)
		Me.lblCurrentVersion.TabIndex = 71
		Me.lblCurrentVersion.Text = "No Version Details"
		Me.lblCurrentVersion.TextAlign = System.Drawing.ContentAlignment.TopRight
		'
		'lblCurrentDate
		'
		Me.lblCurrentDate.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
				  Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.lblCurrentDate.Location = New System.Drawing.Point(100, 147)
		Me.lblCurrentDate.Margin = New System.Windows.Forms.Padding(0, 1, 3, 1)
		Me.lblCurrentDate.Name = "lblCurrentDate"
		Me.lblCurrentDate.Size = New System.Drawing.Size(305, 15)
		Me.lblCurrentDate.TabIndex = 72
		Me.lblCurrentDate.Text = "00/00/0000"
		Me.lblCurrentDate.TextAlign = System.Drawing.ContentAlignment.TopRight
		'
		'picCopyVersion
		'
		Me.picCopyVersion.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.picCopyVersion.Cursor = System.Windows.Forms.Cursors.Hand
		Me.picCopyVersion.Location = New System.Drawing.Point(286, 242)
		Me.picCopyVersion.Name = "picCopyVersion"
		Me.picCopyVersion.Size = New System.Drawing.Size(16, 16)
		Me.picCopyVersion.TabIndex = 73
		Me.picCopyVersion.TabStop = False
		Me.picCopyVersion.Tag = "Copy version information"
		'
		'picCopyDate
		'
		Me.picCopyDate.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.picCopyDate.Cursor = System.Windows.Forms.Cursors.Hand
		Me.picCopyDate.Location = New System.Drawing.Point(286, 221)
		Me.picCopyDate.Name = "picCopyDate"
		Me.picCopyDate.Size = New System.Drawing.Size(16, 16)
		Me.picCopyDate.TabIndex = 74
		Me.picCopyDate.TabStop = False
		Me.picCopyDate.Tag = "Copy modified date information"
		'
		'optVersion
		'
		Me.optVersion.AutoSize = True
		Me.optVersion.Location = New System.Drawing.Point(100, 240)
		Me.optVersion.Margin = New System.Windows.Forms.Padding(3, 0, 3, 3)
		Me.optVersion.Name = "optVersion"
		Me.optVersion.Size = New System.Drawing.Size(72, 17)
		Me.optVersion.TabIndex = 8
		Me.optVersion.Text = "Version...:"
		Me.optVersion.UseVisualStyleBackColor = True
		'
		'optDateTime
		'
		Me.optDateTime.AutoSize = True
		Me.optDateTime.Location = New System.Drawing.Point(100, 220)
		Me.optDateTime.Margin = New System.Windows.Forms.Padding(3, 0, 3, 3)
		Me.optDateTime.Name = "optDateTime"
		Me.optDateTime.Size = New System.Drawing.Size(103, 17)
		Me.optDateTime.TabIndex = 5
		Me.optDateTime.Text = "Modified Date...:"
		Me.optDateTime.UseVisualStyleBackColor = True
		'
		'optNotExists
		'
		Me.optNotExists.AutoSize = True
		Me.optNotExists.Location = New System.Drawing.Point(100, 200)
		Me.optNotExists.Margin = New System.Windows.Forms.Padding(3, 0, 3, 3)
		Me.optNotExists.Name = "optNotExists"
		Me.optNotExists.Size = New System.Drawing.Size(140, 17)
		Me.optNotExists.TabIndex = 4
		Me.optNotExists.Text = "Check file does not exist"
		Me.optNotExists.UseVisualStyleBackColor = True
		'
		'optExists
		'
		Me.optExists.AutoSize = True
		Me.optExists.Checked = True
		Me.optExists.Location = New System.Drawing.Point(100, 180)
		Me.optExists.Margin = New System.Windows.Forms.Padding(3, 12, 3, 3)
		Me.optExists.Name = "optExists"
		Me.optExists.Size = New System.Drawing.Size(101, 17)
		Me.optExists.TabIndex = 3
		Me.optExists.TabStop = True
		Me.optExists.Text = "Check file exists"
		Me.optExists.UseVisualStyleBackColor = True
		'
		'lblSettingsLabel
		'
		Me.lblSettingsLabel.AutoSize = True
		Me.lblSettingsLabel.Location = New System.Drawing.Point(100, 276)
		Me.lblSettingsLabel.Margin = New System.Windows.Forms.Padding(3, 12, 3, 3)
		Me.lblSettingsLabel.Name = "lblSettingsLabel"
		Me.lblSettingsLabel.Size = New System.Drawing.Size(36, 13)
		Me.lblSettingsLabel.TabIndex = 78
		Me.lblSettingsLabel.Text = "[svc] :"
		'
		'Label5
		'
		Me.Label5.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.Label5.AutoSize = True
		Me.Label5.Location = New System.Drawing.Point(12, 322)
		Me.Label5.Margin = New System.Windows.Forms.Padding(3)
		Me.Label5.Name = "Label5"
		Me.Label5.Size = New System.Drawing.Size(182, 13)
		Me.Label5.TabIndex = 79
		Me.Label5.Text = "Wildcard file scans are not supported"
		'
		'cmoDT_ML
		'
		Me.cmoDT_ML.BackColor = System.Drawing.SystemColors.Window
		Me.cmoDT_ML.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
		Me.cmoDT_ML.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.cmoDT_ML.FormattingEnabled = True
		Me.cmoDT_ML.ItemHeight = 19
		Me.cmoDT_ML.Location = New System.Drawing.Point(12, 270)
		Me.cmoDT_ML.Margin = New System.Windows.Forms.Padding(3, 6, 3, 3)
		Me.cmoDT_ML.Name = "cmoDT_ML"
		Me.cmoDT_ML.RemoveIconSpacing = False
		Me.cmoDT_ML.SelectedItem = Nothing
		Me.cmoDT_ML.Size = New System.Drawing.Size(25, 25)
		Me.cmoDT_ML.TabIndex = 10
		'
		'cmoDT_BA
		'
		Me.cmoDT_BA.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.cmoDT_BA.BackColor = System.Drawing.SystemColors.Window
		Me.cmoDT_BA.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
		Me.cmoDT_BA.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.cmoDT_BA.FormattingEnabled = True
		Me.cmoDT_BA.ItemHeight = 19
		Me.cmoDT_BA.Location = New System.Drawing.Point(257, 270)
		Me.cmoDT_BA.Margin = New System.Windows.Forms.Padding(3, 6, 3, 3)
		Me.cmoDT_BA.Name = "cmoDT_BA"
		Me.cmoDT_BA.RemoveIconSpacing = False
		Me.cmoDT_BA.SelectedItem = Nothing
		Me.cmoDT_BA.Size = New System.Drawing.Size(175, 25)
		Me.cmoDT_BA.TabIndex = 7
		'
		'frmAddFileCheck
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.CancelButton = Me.cmdCancel
		Me.ClientSize = New System.Drawing.Size(444, 347)
		Me.Controls.Add(Me.Label5)
		Me.Controls.Add(Me.lblSettingsLabel)
		Me.Controls.Add(Me.cmoDT_ML)
		Me.Controls.Add(Me.txtVersion)
		Me.Controls.Add(Me.optExists)
		Me.Controls.Add(Me.optNotExists)
		Me.Controls.Add(Me.optDateTime)
		Me.Controls.Add(Me.optVersion)
		Me.Controls.Add(Me.picCopyDate)
		Me.Controls.Add(Me.picCopyVersion)
		Me.Controls.Add(Me.lblCurrentDate)
		Me.Controls.Add(Me.lblCurrentVersion)
		Me.Controls.Add(Me.cmoDT_BA)
		Me.Controls.Add(Me.Label1)
		Me.Controls.Add(Me.dtDate)
		Me.Controls.Add(Me.cmdBrowse)
		Me.Controls.Add(Me.txtSelectedFile)
		Me.Controls.Add(Me.lblTop)
		Me.Controls.Add(Me.lnkHelp)
		Me.Controls.Add(Me.Label2)
		Me.Controls.Add(Me.lblTitle)
		Me.Controls.Add(Me.cmdCancel)
		Me.Controls.Add(Me.cmdOK)
		Me.Controls.Add(Me.picIcon)
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
		Me.MaximizeBox = false
		Me.MinimizeBox = false
		Me.Name = "frmAddFileCheck"
		Me.ShowIcon = false
		Me.ShowInTaskbar = false
		Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
		Me.Text = " Add New File Check Scan"
		CType(Me.picIcon,System.ComponentModel.ISupportInitialize).EndInit
		CType(Me.picCopyVersion,System.ComponentModel.ISupportInitialize).EndInit
		CType(Me.picCopyDate,System.ComponentModel.ISupportInitialize).EndInit
		Me.ResumeLayout(false)
		Me.PerformLayout

End Sub
	Friend WithEvents lnkHelp As System.Windows.Forms.LinkLabel
	Friend WithEvents picIcon As System.Windows.Forms.PictureBox
	Friend WithEvents Label2 As System.Windows.Forms.Label
	Friend WithEvents lblTitle As System.Windows.Forms.Label
	Friend WithEvents cmdCancel As System.Windows.Forms.Button
	Friend WithEvents cmdOK As System.Windows.Forms.Button
	Friend WithEvents txtSelectedFile As System.Windows.Forms.TextBox
	Friend WithEvents lblTop As System.Windows.Forms.Label
	Friend WithEvents cmdBrowse As System.Windows.Forms.Button
	Friend WithEvents dtDate As System.Windows.Forms.DateTimePicker
	Friend WithEvents Label1 As System.Windows.Forms.Label
	Friend WithEvents cmoDT_BA As ServerSystemChecker.ctrlComboBox_Icons
	Friend WithEvents txtVersion As System.Windows.Forms.TextBox
	Friend WithEvents lblCurrentVersion As System.Windows.Forms.Label
	Friend WithEvents lblCurrentDate As System.Windows.Forms.Label
	Friend WithEvents picCopyVersion As System.Windows.Forms.PictureBox
	Friend WithEvents picCopyDate As System.Windows.Forms.PictureBox
	Friend WithEvents optVersion As System.Windows.Forms.RadioButton
	Friend WithEvents optDateTime As System.Windows.Forms.RadioButton
	Friend WithEvents optNotExists As System.Windows.Forms.RadioButton
	Friend WithEvents optExists As System.Windows.Forms.RadioButton
	Friend WithEvents cmoDT_ML As ServerSystemChecker.ctrlComboBox_Icons
	Friend WithEvents lblSettingsLabel As System.Windows.Forms.Label
	Friend WithEvents Label5 As System.Windows.Forms.Label
End Class
