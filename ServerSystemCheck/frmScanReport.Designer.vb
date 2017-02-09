<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmScanReport
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
		Me.cmdExport = New System.Windows.Forms.Button()
		Me.proProgress = New System.Windows.Forms.ProgressBar()
		Me.Label2 = New System.Windows.Forms.Label()
		Me.lblTitle = New System.Windows.Forms.Label()
		Me.picIcon = New System.Windows.Forms.PictureBox()
		Me.cmdCancel = New System.Windows.Forms.Button()
		Me.radDetailed = New System.Windows.Forms.RadioButton()
		Me.radSimple = New System.Windows.Forms.RadioButton()
		Me.chkDriveSytem = New System.Windows.Forms.CheckBox()
		Me.chkEventlog = New System.Windows.Forms.CheckBox()
		Me.chkHotfixes = New System.Windows.Forms.CheckBox()
		Me.chkServices = New System.Windows.Forms.CheckBox()
		Me.Label5 = New System.Windows.Forms.Label()
		Me.Label3 = New System.Windows.Forms.Label()
		Me.lnkHelp = New System.Windows.Forms.LinkLabel()
		Me.chkRegKeys = New System.Windows.Forms.CheckBox()
		Me.chkFiles = New System.Windows.Forms.CheckBox()
		Me.chkIncludeIcons = New System.Windows.Forms.CheckBox()
		Me.Label4 = New System.Windows.Forms.Label()
		Me.chkIncludeUptime = New System.Windows.Forms.CheckBox()
		Me.picImage_S = New System.Windows.Forms.PictureBox()
		Me.picImage_R = New System.Windows.Forms.PictureBox()
		Me.picImage_E = New System.Windows.Forms.PictureBox()
		Me.picImage_H = New System.Windows.Forms.PictureBox()
		Me.picImage_D = New System.Windows.Forms.PictureBox()
		Me.picImage_F = New System.Windows.Forms.PictureBox()
		Me.chkDrives_Excluded = New System.Windows.Forms.CheckBox()
		Me.lblServerName = New System.Windows.Forms.Label()
		Me.chkDrives_All = New System.Windows.Forms.CheckBox()
		Me.picImage_W = New System.Windows.Forms.PictureBox()
		Me.chkWMI = New System.Windows.Forms.CheckBox()
		Me.lnkShowExamples = New System.Windows.Forms.LinkLabel()
		Me.lnkSelect_All = New System.Windows.Forms.LinkLabel()
		Me.lnkSelect_None = New System.Windows.Forms.LinkLabel()
		Me.lblSelect = New System.Windows.Forms.Label()
		CType(Me.picIcon, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.picImage_S, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.picImage_R, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.picImage_E, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.picImage_H, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.picImage_D, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.picImage_F, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.picImage_W, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.SuspendLayout()
		'
		'cmdExport
		'
		Me.cmdExport.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.cmdExport.Location = New System.Drawing.Point(307, 385)
		Me.cmdExport.Margin = New System.Windows.Forms.Padding(3, 12, 3, 3)
		Me.cmdExport.Name = "cmdExport"
		Me.cmdExport.Size = New System.Drawing.Size(75, 25)
		Me.cmdExport.TabIndex = 15
		Me.cmdExport.Text = "Export"
		Me.cmdExport.UseVisualStyleBackColor = True
		'
		'proProgress
		'
		Me.proProgress.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
				  Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.proProgress.Location = New System.Drawing.Point(12, 397)
		Me.proProgress.Margin = New System.Windows.Forms.Padding(3, 3, 9, 3)
		Me.proProgress.Name = "proProgress"
		Me.proProgress.Size = New System.Drawing.Size(202, 13)
		Me.proProgress.Style = System.Windows.Forms.ProgressBarStyle.Continuous
		Me.proProgress.TabIndex = 11
		'
		'Label2
		'
		Me.Label2.AutoSize = True
		Me.Label2.Location = New System.Drawing.Point(66, 40)
		Me.Label2.Margin = New System.Windows.Forms.Padding(3)
		Me.Label2.Name = "Label2"
		Me.Label2.Size = New System.Drawing.Size(155, 13)
		Me.Label2.TabIndex = 29
		Me.Label2.Text = "Select from the options below..."
		'
		'lblTitle
		'
		Me.lblTitle.AutoSize = True
		Me.lblTitle.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.lblTitle.Location = New System.Drawing.Point(66, 18)
		Me.lblTitle.Margin = New System.Windows.Forms.Padding(3, 9, 3, 3)
		Me.lblTitle.Name = "lblTitle"
		Me.lblTitle.Size = New System.Drawing.Size(150, 16)
		Me.lblTitle.TabIndex = 28
		Me.lblTitle.Text = "Export Summary Report"
		'
		'picIcon
		'
		Me.picIcon.Location = New System.Drawing.Point(12, 12)
		Me.picIcon.Name = "picIcon"
		Me.picIcon.Size = New System.Drawing.Size(48, 48)
		Me.picIcon.TabIndex = 31
		Me.picIcon.TabStop = False
		'
		'cmdCancel
		'
		Me.cmdCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
		Me.cmdCancel.Location = New System.Drawing.Point(226, 385)
		Me.cmdCancel.Name = "cmdCancel"
		Me.cmdCancel.Size = New System.Drawing.Size(75, 25)
		Me.cmdCancel.TabIndex = 16
		Me.cmdCancel.Text = "Cancel"
		Me.cmdCancel.UseVisualStyleBackColor = True
		'
		'radDetailed
		'
		Me.radDetailed.AutoSize = True
		Me.radDetailed.Location = New System.Drawing.Point(100, 93)
		Me.radDetailed.Margin = New System.Windows.Forms.Padding(3, 0, 0, 3)
		Me.radDetailed.Name = "radDetailed"
		Me.radDetailed.Size = New System.Drawing.Size(64, 17)
		Me.radDetailed.TabIndex = 1
		Me.radDetailed.Text = "Detailed"
		Me.radDetailed.UseVisualStyleBackColor = True
		'
		'radSimple
		'
		Me.radSimple.AutoSize = True
		Me.radSimple.Checked = True
		Me.radSimple.Location = New System.Drawing.Point(100, 73)
		Me.radSimple.Margin = New System.Windows.Forms.Padding(3, 12, 3, 3)
		Me.radSimple.Name = "radSimple"
		Me.radSimple.Size = New System.Drawing.Size(56, 17)
		Me.radSimple.TabIndex = 0
		Me.radSimple.TabStop = True
		Me.radSimple.Text = "Simple"
		Me.radSimple.UseVisualStyleBackColor = True
		'
		'chkDriveSytem
		'
		Me.chkDriveSytem.AutoSize = True
		Me.chkDriveSytem.Location = New System.Drawing.Point(122, 297)
		Me.chkDriveSytem.Margin = New System.Windows.Forms.Padding(3, 0, 3, 3)
		Me.chkDriveSytem.Name = "chkDriveSytem"
		Me.chkDriveSytem.Size = New System.Drawing.Size(177, 17)
		Me.chkDriveSytem.TabIndex = 12
		Me.chkDriveSytem.Text = "Show system drive space status"
		Me.chkDriveSytem.UseVisualStyleBackColor = True
		'
		'chkEventlog
		'
		Me.chkEventlog.AutoSize = True
		Me.chkEventlog.Location = New System.Drawing.Point(122, 217)
		Me.chkEventlog.Margin = New System.Windows.Forms.Padding(3, 0, 3, 3)
		Me.chkEventlog.Name = "chkEventlog"
		Me.chkEventlog.Size = New System.Drawing.Size(183, 17)
		Me.chkEventlog.TabIndex = 8
		Me.chkEventlog.Text = "List every reported eventlog entry"
		Me.chkEventlog.UseVisualStyleBackColor = True
		'
		'chkHotfixes
		'
		Me.chkHotfixes.AutoSize = True
		Me.chkHotfixes.Location = New System.Drawing.Point(122, 197)
		Me.chkHotfixes.Margin = New System.Windows.Forms.Padding(3, 0, 3, 3)
		Me.chkHotfixes.Name = "chkHotfixes"
		Me.chkHotfixes.Size = New System.Drawing.Size(138, 17)
		Me.chkHotfixes.TabIndex = 7
		Me.chkHotfixes.Text = "List all incorrect hotfixes"
		Me.chkHotfixes.UseVisualStyleBackColor = True
		'
		'chkServices
		'
		Me.chkServices.AutoSize = True
		Me.chkServices.Location = New System.Drawing.Point(122, 177)
		Me.chkServices.Margin = New System.Windows.Forms.Padding(3, 12, 3, 3)
		Me.chkServices.Name = "chkServices"
		Me.chkServices.Size = New System.Drawing.Size(141, 17)
		Me.chkServices.TabIndex = 6
		Me.chkServices.Text = "List all incorrect services"
		Me.chkServices.UseVisualStyleBackColor = True
		'
		'Label5
		'
		Me.Label5.AutoSize = True
		Me.Label5.Location = New System.Drawing.Point(12, 178)
		Me.Label5.Margin = New System.Windows.Forms.Padding(3, 24, 3, 9)
		Me.Label5.Name = "Label5"
		Me.Label5.Size = New System.Drawing.Size(52, 13)
		Me.Label5.TabIndex = 37
		Me.Label5.Text = "Detailed :"
		'
		'Label3
		'
		Me.Label3.AutoSize = True
		Me.Label3.Location = New System.Drawing.Point(12, 75)
		Me.Label3.Margin = New System.Windows.Forms.Padding(3, 12, 3, 9)
		Me.Label3.Name = "Label3"
		Me.Label3.Size = New System.Drawing.Size(72, 13)
		Me.Label3.TabIndex = 36
		Me.Label3.Text = "Report Type :"
		'
		'lnkHelp
		'
		Me.lnkHelp.Location = New System.Drawing.Point(310, 9)
		Me.lnkHelp.Margin = New System.Windows.Forms.Padding(0)
		Me.lnkHelp.Name = "lnkHelp"
		Me.lnkHelp.Size = New System.Drawing.Size(75, 15)
		Me.lnkHelp.TabIndex = 18
		Me.lnkHelp.TabStop = True
		Me.lnkHelp.Text = "Help"
		Me.lnkHelp.TextAlign = System.Drawing.ContentAlignment.TopRight
		'
		'chkRegKeys
		'
		Me.chkRegKeys.AutoSize = True
		Me.chkRegKeys.Location = New System.Drawing.Point(122, 237)
		Me.chkRegKeys.Margin = New System.Windows.Forms.Padding(3, 0, 3, 3)
		Me.chkRegKeys.Name = "chkRegKeys"
		Me.chkRegKeys.Size = New System.Drawing.Size(153, 17)
		Me.chkRegKeys.TabIndex = 9
		Me.chkRegKeys.Text = "List all missing registry keys"
		Me.chkRegKeys.UseVisualStyleBackColor = True
		'
		'chkFiles
		'
		Me.chkFiles.AutoSize = True
		Me.chkFiles.Location = New System.Drawing.Point(122, 257)
		Me.chkFiles.Margin = New System.Windows.Forms.Padding(3, 0, 3, 3)
		Me.chkFiles.Name = "chkFiles"
		Me.chkFiles.Size = New System.Drawing.Size(131, 17)
		Me.chkFiles.TabIndex = 10
		Me.chkFiles.Text = "List every incorrect file"
		Me.chkFiles.UseVisualStyleBackColor = True
		'
		'chkIncludeIcons
		'
		Me.chkIncludeIcons.AutoSize = True
		Me.chkIncludeIcons.Checked = True
		Me.chkIncludeIcons.CheckState = System.Windows.Forms.CheckState.Checked
		Me.chkIncludeIcons.Location = New System.Drawing.Point(100, 125)
		Me.chkIncludeIcons.Margin = New System.Windows.Forms.Padding(3, 12, 3, 3)
		Me.chkIncludeIcons.Name = "chkIncludeIcons"
		Me.chkIncludeIcons.Size = New System.Drawing.Size(241, 17)
		Me.chkIncludeIcons.TabIndex = 4
		Me.chkIncludeIcons.Text = "Include image icons (increases report file size)"
		Me.chkIncludeIcons.UseVisualStyleBackColor = True
		'
		'Label4
		'
		Me.Label4.AutoSize = True
		Me.Label4.Location = New System.Drawing.Point(12, 126)
		Me.Label4.Margin = New System.Windows.Forms.Padding(3, 12, 3, 9)
		Me.Label4.Name = "Label4"
		Me.Label4.Size = New System.Drawing.Size(49, 13)
		Me.Label4.TabIndex = 51
		Me.Label4.Text = "Options :"
		'
		'chkIncludeUptime
		'
		Me.chkIncludeUptime.AutoSize = True
		Me.chkIncludeUptime.Checked = True
		Me.chkIncludeUptime.CheckState = System.Windows.Forms.CheckState.Checked
		Me.chkIncludeUptime.Location = New System.Drawing.Point(100, 145)
		Me.chkIncludeUptime.Margin = New System.Windows.Forms.Padding(3, 0, 3, 3)
		Me.chkIncludeUptime.Name = "chkIncludeUptime"
		Me.chkIncludeUptime.Size = New System.Drawing.Size(186, 17)
		Me.chkIncludeUptime.TabIndex = 5
		Me.chkIncludeUptime.Text = "Include server uptime (if available)"
		Me.chkIncludeUptime.UseVisualStyleBackColor = True
		'
		'picImage_S
		'
		Me.picImage_S.Location = New System.Drawing.Point(100, 177)
		Me.picImage_S.Margin = New System.Windows.Forms.Padding(3, 12, 3, 3)
		Me.picImage_S.Name = "picImage_S"
		Me.picImage_S.Size = New System.Drawing.Size(16, 16)
		Me.picImage_S.TabIndex = 53
		Me.picImage_S.TabStop = False
		'
		'picImage_R
		'
		Me.picImage_R.Location = New System.Drawing.Point(100, 237)
		Me.picImage_R.Margin = New System.Windows.Forms.Padding(3, 1, 3, 3)
		Me.picImage_R.Name = "picImage_R"
		Me.picImage_R.Size = New System.Drawing.Size(16, 16)
		Me.picImage_R.TabIndex = 56
		Me.picImage_R.TabStop = False
		'
		'picImage_E
		'
		Me.picImage_E.Location = New System.Drawing.Point(100, 217)
		Me.picImage_E.Margin = New System.Windows.Forms.Padding(3, 1, 3, 3)
		Me.picImage_E.Name = "picImage_E"
		Me.picImage_E.Size = New System.Drawing.Size(16, 16)
		Me.picImage_E.TabIndex = 55
		Me.picImage_E.TabStop = False
		'
		'picImage_H
		'
		Me.picImage_H.Location = New System.Drawing.Point(100, 197)
		Me.picImage_H.Margin = New System.Windows.Forms.Padding(3, 1, 3, 3)
		Me.picImage_H.Name = "picImage_H"
		Me.picImage_H.Size = New System.Drawing.Size(16, 16)
		Me.picImage_H.TabIndex = 54
		Me.picImage_H.TabStop = False
		'
		'picImage_D
		'
		Me.picImage_D.Location = New System.Drawing.Point(100, 297)
		Me.picImage_D.Margin = New System.Windows.Forms.Padding(3, 1, 3, 3)
		Me.picImage_D.Name = "picImage_D"
		Me.picImage_D.Size = New System.Drawing.Size(16, 16)
		Me.picImage_D.TabIndex = 58
		Me.picImage_D.TabStop = False
		'
		'picImage_F
		'
		Me.picImage_F.Location = New System.Drawing.Point(100, 257)
		Me.picImage_F.Margin = New System.Windows.Forms.Padding(3, 1, 3, 3)
		Me.picImage_F.Name = "picImage_F"
		Me.picImage_F.Size = New System.Drawing.Size(16, 16)
		Me.picImage_F.TabIndex = 57
		Me.picImage_F.TabStop = False
		'
		'chkDrives_Excluded
		'
		Me.chkDrives_Excluded.AutoSize = True
		Me.chkDrives_Excluded.Location = New System.Drawing.Point(122, 337)
		Me.chkDrives_Excluded.Margin = New System.Windows.Forms.Padding(3, 0, 3, 3)
		Me.chkDrives_Excluded.Name = "chkDrives_Excluded"
		Me.chkDrives_Excluded.Padding = New System.Windows.Forms.Padding(18, 0, 0, 0)
		Me.chkDrives_Excluded.Size = New System.Drawing.Size(140, 17)
		Me.chkDrives_Excluded.TabIndex = 14
		Me.chkDrives_Excluded.Text = "Add excluded drives"
		Me.chkDrives_Excluded.UseVisualStyleBackColor = True
		'
		'lblServerName
		'
		Me.lblServerName.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.lblServerName.AutoSize = True
		Me.lblServerName.Location = New System.Drawing.Point(12, 377)
		Me.lblServerName.Margin = New System.Windows.Forms.Padding(3, 0, 3, 1)
		Me.lblServerName.Name = "lblServerName"
		Me.lblServerName.Size = New System.Drawing.Size(76, 13)
		Me.lblServerName.TabIndex = 60
		Me.lblServerName.Text = "lblServerName"
		'
		'chkDrives_All
		'
		Me.chkDrives_All.AutoSize = True
		Me.chkDrives_All.Location = New System.Drawing.Point(122, 317)
		Me.chkDrives_All.Margin = New System.Windows.Forms.Padding(3, 0, 3, 3)
		Me.chkDrives_All.Name = "chkDrives_All"
		Me.chkDrives_All.Padding = New System.Windows.Forms.Padding(9, 0, 0, 0)
		Me.chkDrives_All.Size = New System.Drawing.Size(139, 17)
		Me.chkDrives_All.TabIndex = 13
		Me.chkDrives_All.Text = "List all scanned drives"
		Me.chkDrives_All.UseVisualStyleBackColor = True
		'
		'picImage_W
		'
		Me.picImage_W.Location = New System.Drawing.Point(100, 277)
		Me.picImage_W.Margin = New System.Windows.Forms.Padding(3, 1, 3, 3)
		Me.picImage_W.Name = "picImage_W"
		Me.picImage_W.Size = New System.Drawing.Size(16, 16)
		Me.picImage_W.TabIndex = 63
		Me.picImage_W.TabStop = False
		'
		'chkWMI
		'
		Me.chkWMI.AutoSize = True
		Me.chkWMI.Location = New System.Drawing.Point(122, 277)
		Me.chkWMI.Margin = New System.Windows.Forms.Padding(3, 0, 3, 3)
		Me.chkWMI.Name = "chkWMI"
		Me.chkWMI.Size = New System.Drawing.Size(143, 17)
		Me.chkWMI.TabIndex = 11
		Me.chkWMI.Text = "List all WMI query results"
		Me.chkWMI.UseVisualStyleBackColor = True
		'
		'lnkShowExamples
		'
		Me.lnkShowExamples.AutoSize = True
		Me.lnkShowExamples.Location = New System.Drawing.Point(12, 391)
		Me.lnkShowExamples.Name = "lnkShowExamples"
		Me.lnkShowExamples.Size = New System.Drawing.Size(111, 13)
		Me.lnkShowExamples.TabIndex = 17
		Me.lnkShowExamples.TabStop = True
		Me.lnkShowExamples.Text = "Show report examples"
		'
		'lnkSelect_All
		'
		Me.lnkSelect_All.AutoSize = True
		Me.lnkSelect_All.Location = New System.Drawing.Point(212, 95)
		Me.lnkSelect_All.Margin = New System.Windows.Forms.Padding(0)
		Me.lnkSelect_All.Name = "lnkSelect_All"
		Me.lnkSelect_All.Size = New System.Drawing.Size(18, 13)
		Me.lnkSelect_All.TabIndex = 2
		Me.lnkSelect_All.TabStop = True
		Me.lnkSelect_All.Text = "All"
		'
		'lnkSelect_None
		'
		Me.lnkSelect_None.AutoSize = True
		Me.lnkSelect_None.Location = New System.Drawing.Point(230, 95)
		Me.lnkSelect_None.Margin = New System.Windows.Forms.Padding(0)
		Me.lnkSelect_None.Name = "lnkSelect_None"
		Me.lnkSelect_None.Size = New System.Drawing.Size(33, 13)
		Me.lnkSelect_None.TabIndex = 3
		Me.lnkSelect_None.TabStop = True
		Me.lnkSelect_None.Text = "None"
		'
		'lblSelect
		'
		Me.lblSelect.AutoSize = True
		Me.lblSelect.Location = New System.Drawing.Point(175, 95)
		Me.lblSelect.Margin = New System.Windows.Forms.Padding(0)
		Me.lblSelect.Name = "lblSelect"
		Me.lblSelect.Size = New System.Drawing.Size(37, 13)
		Me.lblSelect.TabIndex = 67
		Me.lblSelect.Text = "Select"
		'
		'frmScanReport
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.CancelButton = Me.cmdCancel
		Me.ClientSize = New System.Drawing.Size(394, 422)
		Me.Controls.Add(Me.lblSelect)
		Me.Controls.Add(Me.lnkSelect_None)
		Me.Controls.Add(Me.lnkSelect_All)
		Me.Controls.Add(Me.lnkShowExamples)
		Me.Controls.Add(Me.picImage_W)
		Me.Controls.Add(Me.chkWMI)
		Me.Controls.Add(Me.chkDrives_All)
		Me.Controls.Add(Me.lblServerName)
		Me.Controls.Add(Me.chkDrives_Excluded)
		Me.Controls.Add(Me.picImage_D)
		Me.Controls.Add(Me.picImage_F)
		Me.Controls.Add(Me.picImage_R)
		Me.Controls.Add(Me.picImage_E)
		Me.Controls.Add(Me.picImage_H)
		Me.Controls.Add(Me.picImage_S)
		Me.Controls.Add(Me.chkIncludeUptime)
		Me.Controls.Add(Me.Label4)
		Me.Controls.Add(Me.chkIncludeIcons)
		Me.Controls.Add(Me.chkFiles)
		Me.Controls.Add(Me.chkRegKeys)
		Me.Controls.Add(Me.lnkHelp)
		Me.Controls.Add(Me.radDetailed)
		Me.Controls.Add(Me.radSimple)
		Me.Controls.Add(Me.chkDriveSytem)
		Me.Controls.Add(Me.chkEventlog)
		Me.Controls.Add(Me.chkHotfixes)
		Me.Controls.Add(Me.chkServices)
		Me.Controls.Add(Me.Label5)
		Me.Controls.Add(Me.Label3)
		Me.Controls.Add(Me.cmdCancel)
		Me.Controls.Add(Me.picIcon)
		Me.Controls.Add(Me.Label2)
		Me.Controls.Add(Me.lblTitle)
		Me.Controls.Add(Me.proProgress)
		Me.Controls.Add(Me.cmdExport)
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
		Me.MaximizeBox = False
		Me.MinimizeBox = False
		Me.Name = "frmScanReport"
		Me.ShowIcon = False
		Me.ShowInTaskbar = False
		Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
		Me.Text = " Export Summary Report"
		CType(Me.picIcon, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.picImage_S, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.picImage_R, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.picImage_E, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.picImage_H, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.picImage_D, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.picImage_F, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.picImage_W, System.ComponentModel.ISupportInitialize).EndInit()
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub
	Friend WithEvents cmdExport As System.Windows.Forms.Button
	Friend WithEvents proProgress As System.Windows.Forms.ProgressBar
	Friend WithEvents Label2 As System.Windows.Forms.Label
	Friend WithEvents lblTitle As System.Windows.Forms.Label
	Friend WithEvents picIcon As System.Windows.Forms.PictureBox
	Friend WithEvents cmdCancel As System.Windows.Forms.Button
	Friend WithEvents radDetailed As System.Windows.Forms.RadioButton
	Friend WithEvents radSimple As System.Windows.Forms.RadioButton
	Friend WithEvents chkDriveSytem As System.Windows.Forms.CheckBox
	Friend WithEvents chkEventlog As System.Windows.Forms.CheckBox
	Friend WithEvents chkHotfixes As System.Windows.Forms.CheckBox
	Friend WithEvents chkServices As System.Windows.Forms.CheckBox
	Friend WithEvents Label5 As System.Windows.Forms.Label
	Friend WithEvents Label3 As System.Windows.Forms.Label
	Friend WithEvents lnkHelp As System.Windows.Forms.LinkLabel
	Friend WithEvents chkRegKeys As System.Windows.Forms.CheckBox
	Friend WithEvents chkFiles As System.Windows.Forms.CheckBox
	Friend WithEvents chkIncludeIcons As System.Windows.Forms.CheckBox
	Friend WithEvents Label4 As System.Windows.Forms.Label
	Friend WithEvents chkIncludeUptime As System.Windows.Forms.CheckBox
	Friend WithEvents picImage_S As System.Windows.Forms.PictureBox
	Friend WithEvents picImage_R As System.Windows.Forms.PictureBox
	Friend WithEvents picImage_E As System.Windows.Forms.PictureBox
	Friend WithEvents picImage_H As System.Windows.Forms.PictureBox
	Friend WithEvents picImage_D As System.Windows.Forms.PictureBox
	Friend WithEvents picImage_F As System.Windows.Forms.PictureBox
	Friend WithEvents chkDrives_Excluded As System.Windows.Forms.CheckBox
	Friend WithEvents lblServerName As System.Windows.Forms.Label
	Friend WithEvents chkDrives_All As System.Windows.Forms.CheckBox
	Friend WithEvents picImage_W As System.Windows.Forms.PictureBox
	Friend WithEvents chkWMI As System.Windows.Forms.CheckBox
	Friend WithEvents lnkShowExamples As System.Windows.Forms.LinkLabel
	Friend WithEvents lnkSelect_All As System.Windows.Forms.LinkLabel
	Friend WithEvents lnkSelect_None As System.Windows.Forms.LinkLabel
	Friend WithEvents lblSelect As System.Windows.Forms.Label
End Class
