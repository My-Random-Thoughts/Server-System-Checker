<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAddRegistry
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
		Me.cmdCancel = New System.Windows.Forms.Button()
		Me.cmdOK = New System.Windows.Forms.Button()
		Me.Label2 = New System.Windows.Forms.Label()
		Me.lblTitle = New System.Windows.Forms.Label()
		Me.lblTop = New System.Windows.Forms.Label()
		Me.txtRegistryKey = New System.Windows.Forms.TextBox()
		Me.txtValueName = New System.Windows.Forms.TextBox()
		Me.txtValueData = New System.Windows.Forms.TextBox()
		Me.Label6 = New System.Windows.Forms.Label()
		Me.Label7 = New System.Windows.Forms.Label()
		Me.Label8 = New System.Windows.Forms.Label()
		Me.Label10 = New System.Windows.Forms.Label()
		Me.cmdImportReg = New System.Windows.Forms.Button()
		Me.cmdBrowse = New System.Windows.Forms.Button()
		Me.cmdSearchRegistry = New System.Windows.Forms.Button()
		Me.lstImportReg = New ServerSystemChecker.ctrlListView_SubIcons()
		Me.cmoValueType = New ServerSystemChecker.ctrlComboBox_Icons()
		Me.cmoMissingKey = New ServerSystemChecker.ctrlComboBox_Icons()
		CType(Me.picIcon, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.SuspendLayout()
		'
		'lnkHelp
		'
		Me.lnkHelp.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.lnkHelp.Location = New System.Drawing.Point(360, 9)
		Me.lnkHelp.Margin = New System.Windows.Forms.Padding(0)
		Me.lnkHelp.Name = "lnkHelp"
		Me.lnkHelp.Size = New System.Drawing.Size(75, 15)
		Me.lnkHelp.TabIndex = 47
		Me.lnkHelp.TabStop = True
		Me.lnkHelp.Text = "Help"
		Me.lnkHelp.TextAlign = System.Drawing.ContentAlignment.TopRight
		'
		'picIcon
		'
		Me.picIcon.Location = New System.Drawing.Point(12, 12)
		Me.picIcon.Name = "picIcon"
		Me.picIcon.Size = New System.Drawing.Size(48, 48)
		Me.picIcon.TabIndex = 46
		Me.picIcon.TabStop = False
		'
		'cmdCancel
		'
		Me.cmdCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
		Me.cmdCancel.Location = New System.Drawing.Point(276, 335)
		Me.cmdCancel.Name = "cmdCancel"
		Me.cmdCancel.Size = New System.Drawing.Size(75, 25)
		Me.cmdCancel.TabIndex = 6
		Me.cmdCancel.Text = "Cancel"
		Me.cmdCancel.UseVisualStyleBackColor = True
		'
		'cmdOK
		'
		Me.cmdOK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.cmdOK.Location = New System.Drawing.Point(357, 335)
		Me.cmdOK.Margin = New System.Windows.Forms.Padding(3, 12, 3, 3)
		Me.cmdOK.Name = "cmdOK"
		Me.cmdOK.Size = New System.Drawing.Size(75, 25)
		Me.cmdOK.TabIndex = 5
		Me.cmdOK.Text = "OK"
		Me.cmdOK.UseVisualStyleBackColor = True
		'
		'Label2
		'
		Me.Label2.AutoSize = True
		Me.Label2.Location = New System.Drawing.Point(66, 40)
		Me.Label2.Margin = New System.Windows.Forms.Padding(3)
		Me.Label2.Name = "Label2"
		Me.Label2.Size = New System.Drawing.Size(284, 13)
		Me.Label2.TabIndex = 43
		Me.Label2.Text = "HKEY_LOCAL_MACHINE is the only registry hive scanned"
		'
		'lblTitle
		'
		Me.lblTitle.AutoSize = True
		Me.lblTitle.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.lblTitle.Location = New System.Drawing.Point(66, 18)
		Me.lblTitle.Margin = New System.Windows.Forms.Padding(3, 9, 3, 3)
		Me.lblTitle.Name = "lblTitle"
		Me.lblTitle.Size = New System.Drawing.Size(146, 16)
		Me.lblTitle.TabIndex = 42
		Me.lblTitle.Text = "Add New Registy Scan"
		'
		'lblTop
		'
		Me.lblTop.AutoSize = True
		Me.lblTop.Location = New System.Drawing.Point(12, 75)
		Me.lblTop.Margin = New System.Windows.Forms.Padding(3, 12, 3, 9)
		Me.lblTop.Name = "lblTop"
		Me.lblTop.Size = New System.Drawing.Size(72, 13)
		Me.lblTop.TabIndex = 48
		Me.lblTop.Text = "Registry Key :"
		'
		'txtRegistryKey
		'
		Me.txtRegistryKey.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
				  Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.txtRegistryKey.Location = New System.Drawing.Point(100, 72)
		Me.txtRegistryKey.Margin = New System.Windows.Forms.Padding(0, 3, 3, 3)
		Me.txtRegistryKey.Multiline = True
		Me.txtRegistryKey.Name = "txtRegistryKey"
		Me.txtRegistryKey.Size = New System.Drawing.Size(304, 55)
		Me.txtRegistryKey.TabIndex = 0
		'
		'txtValueName
		'
		Me.txtValueName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
				  Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.txtValueName.Location = New System.Drawing.Point(100, 139)
		Me.txtValueName.Margin = New System.Windows.Forms.Padding(3, 9, 3, 3)
		Me.txtValueName.Name = "txtValueName"
		Me.txtValueName.Size = New System.Drawing.Size(332, 20)
		Me.txtValueName.TabIndex = 1
		'
		'txtValueData
		'
		Me.txtValueData.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
				  Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.txtValueData.Location = New System.Drawing.Point(100, 208)
		Me.txtValueData.Margin = New System.Windows.Forms.Padding(3, 9, 3, 3)
		Me.txtValueData.Multiline = True
		Me.txtValueData.Name = "txtValueData"
		Me.txtValueData.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
		Me.txtValueData.Size = New System.Drawing.Size(332, 55)
		Me.txtValueData.TabIndex = 3
		'
		'Label6
		'
		Me.Label6.AutoSize = True
		Me.Label6.Location = New System.Drawing.Point(12, 142)
		Me.Label6.Margin = New System.Windows.Forms.Padding(3)
		Me.Label6.Name = "Label6"
		Me.Label6.Size = New System.Drawing.Size(71, 13)
		Me.Label6.TabIndex = 54
		Me.Label6.Text = "Value Name :"
		'
		'Label7
		'
		Me.Label7.AutoSize = True
		Me.Label7.Location = New System.Drawing.Point(12, 177)
		Me.Label7.Margin = New System.Windows.Forms.Padding(3)
		Me.Label7.Name = "Label7"
		Me.Label7.Size = New System.Drawing.Size(37, 13)
		Me.Label7.TabIndex = 56
		Me.Label7.Text = "Type :"
		'
		'Label8
		'
		Me.Label8.AutoSize = True
		Me.Label8.Location = New System.Drawing.Point(12, 211)
		Me.Label8.Margin = New System.Windows.Forms.Padding(3)
		Me.Label8.Name = "Label8"
		Me.Label8.Size = New System.Drawing.Size(36, 13)
		Me.Label8.TabIndex = 57
		Me.Label8.Text = "Data :"
		'
		'Label10
		'
		Me.Label10.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.Label10.AutoSize = True
		Me.Label10.Location = New System.Drawing.Point(12, 301)
		Me.Label10.Margin = New System.Windows.Forms.Padding(3)
		Me.Label10.Name = "Label10"
		Me.Label10.Size = New System.Drawing.Size(200, 13)
		Me.Label10.TabIndex = 62
		Me.Label10.Text = "Select scan result for missing key/name :"
		'
		'cmdImportReg
		'
		Me.cmdImportReg.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.cmdImportReg.Location = New System.Drawing.Point(12, 335)
		Me.cmdImportReg.Margin = New System.Windows.Forms.Padding(3, 3, 12, 3)
		Me.cmdImportReg.Name = "cmdImportReg"
		Me.cmdImportReg.Size = New System.Drawing.Size(125, 25)
		Me.cmdImportReg.TabIndex = 7
		Me.cmdImportReg.Text = "Import .REG File..."
		Me.cmdImportReg.UseVisualStyleBackColor = True
		'
		'cmdBrowse
		'
		Me.cmdBrowse.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.cmdBrowse.Location = New System.Drawing.Point(152, 335)
		Me.cmdBrowse.Name = "cmdBrowse"
		Me.cmdBrowse.Size = New System.Drawing.Size(125, 25)
		Me.cmdBrowse.TabIndex = 8
		Me.cmdBrowse.Text = "Select File"
		Me.cmdBrowse.UseVisualStyleBackColor = True
		'
		'cmdSearchRegistry
		'
		Me.cmdSearchRegistry.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.cmdSearchRegistry.Location = New System.Drawing.Point(407, 72)
		Me.cmdSearchRegistry.Margin = New System.Windows.Forms.Padding(0, 3, 3, 3)
		Me.cmdSearchRegistry.Name = "cmdSearchRegistry"
		Me.cmdSearchRegistry.Size = New System.Drawing.Size(25, 20)
		Me.cmdSearchRegistry.TabIndex = 72
		Me.cmdSearchRegistry.Text = "..."
		Me.cmdSearchRegistry.UseVisualStyleBackColor = True
		'
		'lstImportReg
		'
		Me.lstImportReg.AllowDrop = True
		Me.lstImportReg.Location = New System.Drawing.Point(12, 12)
		Me.lstImportReg.Name = "lstImportReg"
		Me.lstImportReg.OwnerDraw = True
		Me.lstImportReg.Size = New System.Drawing.Size(25, 25)
		Me.lstImportReg.TabIndex = 73
		Me.lstImportReg.UseCompatibleStateImageBehavior = False
		'
		'cmoValueType
		'
		Me.cmoValueType.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
				  Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.cmoValueType.BackColor = System.Drawing.SystemColors.Window
		Me.cmoValueType.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
		Me.cmoValueType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.cmoValueType.FormattingEnabled = True
		Me.cmoValueType.ItemHeight = 19
		Me.cmoValueType.Location = New System.Drawing.Point(100, 171)
		Me.cmoValueType.Margin = New System.Windows.Forms.Padding(3, 9, 3, 3)
		Me.cmoValueType.Name = "cmoValueType"
		Me.cmoValueType.RemoveIconSpacing = False
		Me.cmoValueType.SelectedItem = Nothing
		Me.cmoValueType.Size = New System.Drawing.Size(175, 25)
		Me.cmoValueType.TabIndex = 2
		'
		'cmoMissingKey
		'
		Me.cmoMissingKey.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.cmoMissingKey.BackColor = System.Drawing.SystemColors.Window
		Me.cmoMissingKey.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
		Me.cmoMissingKey.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.cmoMissingKey.FormattingEnabled = True
		Me.cmoMissingKey.ItemHeight = 19
		Me.cmoMissingKey.Location = New System.Drawing.Point(307, 295)
		Me.cmoMissingKey.Margin = New System.Windows.Forms.Padding(3, 9, 3, 3)
		Me.cmoMissingKey.Name = "cmoMissingKey"
		Me.cmoMissingKey.RemoveIconSpacing = False
		Me.cmoMissingKey.SelectedItem = Nothing
		Me.cmoMissingKey.Size = New System.Drawing.Size(125, 25)
		Me.cmoMissingKey.TabIndex = 4
		'
		'frmAddRegistry
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.CancelButton = Me.cmdCancel
		Me.ClientSize = New System.Drawing.Size(444, 372)
		Me.Controls.Add(Me.lstImportReg)
		Me.Controls.Add(Me.cmdSearchRegistry)
		Me.Controls.Add(Me.cmdImportReg)
		Me.Controls.Add(Me.cmoValueType)
		Me.Controls.Add(Me.cmoMissingKey)
		Me.Controls.Add(Me.Label10)
		Me.Controls.Add(Me.Label8)
		Me.Controls.Add(Me.Label7)
		Me.Controls.Add(Me.Label6)
		Me.Controls.Add(Me.txtValueData)
		Me.Controls.Add(Me.txtValueName)
		Me.Controls.Add(Me.txtRegistryKey)
		Me.Controls.Add(Me.lblTop)
		Me.Controls.Add(Me.lnkHelp)
		Me.Controls.Add(Me.picIcon)
		Me.Controls.Add(Me.cmdCancel)
		Me.Controls.Add(Me.cmdOK)
		Me.Controls.Add(Me.Label2)
		Me.Controls.Add(Me.lblTitle)
		Me.Controls.Add(Me.cmdBrowse)
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
		Me.MaximizeBox = False
		Me.MinimizeBox = False
		Me.Name = "frmAddRegistry"
		Me.ShowIcon = False
		Me.ShowInTaskbar = False
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
		Me.Text = " Add New Registry Scan"
		CType(Me.picIcon, System.ComponentModel.ISupportInitialize).EndInit()
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub
	Friend WithEvents lnkHelp As System.Windows.Forms.LinkLabel
	Friend WithEvents picIcon As System.Windows.Forms.PictureBox
	Friend WithEvents cmdCancel As System.Windows.Forms.Button
	Friend WithEvents cmdOK As System.Windows.Forms.Button
	Friend WithEvents Label2 As System.Windows.Forms.Label
	Friend WithEvents lblTitle As System.Windows.Forms.Label
	Friend WithEvents lblTop As System.Windows.Forms.Label
	Friend WithEvents txtRegistryKey As System.Windows.Forms.TextBox
	Friend WithEvents txtValueName As System.Windows.Forms.TextBox
	Friend WithEvents txtValueData As System.Windows.Forms.TextBox
	Friend WithEvents Label6 As System.Windows.Forms.Label
	Friend WithEvents Label7 As System.Windows.Forms.Label
	Friend WithEvents Label8 As System.Windows.Forms.Label
	Friend WithEvents Label10 As System.Windows.Forms.Label
	Friend WithEvents cmoMissingKey As ServerSystemChecker.ctrlComboBox_Icons
	Friend WithEvents cmoValueType As ServerSystemChecker.ctrlComboBox_Icons
	Friend WithEvents cmdImportReg As System.Windows.Forms.Button
	Friend WithEvents cmdBrowse As System.Windows.Forms.Button
	Friend WithEvents cmdSearchRegistry As System.Windows.Forms.Button
	Friend WithEvents lstImportReg As ServerSystemChecker.ctrlListView_SubIcons
End Class
