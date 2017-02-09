<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmScanResults
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
		Me.txtEventLog_Message = New System.Windows.Forms.TextBox()
		Me.tabControl = New System.Windows.Forms.TabControl()
		Me.TabPage1 = New System.Windows.Forms.TabPage()
		Me.lblNoIssues_Services = New System.Windows.Forms.Label()
		Me.lstResults_Services = New ServerSystemChecker.ctrlListView_CollapseGroups()
		Me.TabPage2 = New System.Windows.Forms.TabPage()
		Me.lblNoIssues_Hotfixes = New System.Windows.Forms.Label()
		Me.lstResults_Hotfix = New ServerSystemChecker.ctrlListView_CollapseGroups()
		Me.TabPage3 = New System.Windows.Forms.TabPage()
		Me.lblNoIssues_Eventlog = New System.Windows.Forms.Label()
		Me.lstResults_Eventlog = New ServerSystemChecker.ctrlListView_CollapseGroups()
		Me.TabPage4 = New System.Windows.Forms.TabPage()
		Me.lblNoIssues_Registry = New System.Windows.Forms.Label()
		Me.lstResults_Registry = New ServerSystemChecker.ctrlListView_CollapseGroups()
		Me.TabPage5 = New System.Windows.Forms.TabPage()
		Me.lblNoIssues_FileScan = New System.Windows.Forms.Label()
		Me.lstResults_FileChecks = New ServerSystemChecker.ctrlListView_CollapseGroups()
		Me.TabPage6 = New System.Windows.Forms.TabPage()
		Me.lblNoIssues_WMIQuery = New System.Windows.Forms.Label()
		Me.lstResults_WMIQuery = New ServerSystemChecker.ctrlListView_CollapseGroups()
		Me.TabPage7 = New System.Windows.Forms.TabPage()
		Me.lstResults_DriveSpace_Excluded = New System.Windows.Forms.ListView()
		Me.picSpaceG = New System.Windows.Forms.PictureBox()
		Me.picSpaceY = New System.Windows.Forms.PictureBox()
		Me.picSpaceR = New System.Windows.Forms.PictureBox()
		Me.picEndcapR = New System.Windows.Forms.PictureBox()
		Me.picEndcapL = New System.Windows.Forms.PictureBox()
		Me.picDTBackground = New System.Windows.Forms.PictureBox()
		Me.picIcon_DriveState = New System.Windows.Forms.PictureBox()
		Me.lblCurrent_Warning = New System.Windows.Forms.Label()
		Me.lblCurrent_Critical = New System.Windows.Forms.Label()
		Me.Label1 = New System.Windows.Forms.Label()
		Me.picIcon_Drive = New System.Windows.Forms.PictureBox()
		Me.lblDriveLabel = New System.Windows.Forms.Label()
		Me.lblSize = New System.Windows.Forms.Label()
		Me.picDriveSpace = New System.Windows.Forms.PictureBox()
		Me.lstResults_DriveSpace = New ServerSystemChecker.ctrlListView_DriveSpaceTile()
		Me.lstResults_DriveSpace_Background = New System.Windows.Forms.ListView()
		Me.picIcon = New System.Windows.Forms.PictureBox()
		Me.lblSubTitle = New System.Windows.Forms.Label()
		Me.lblServerName = New System.Windows.Forms.Label()
		Me.cmdListAllInstalledHotfixes = New System.Windows.Forms.Button()
		Me.proProgress = New System.Windows.Forms.ProgressBar()
		Me.lnkHelp = New System.Windows.Forms.LinkLabel()
		Me.lnkNote = New System.Windows.Forms.LinkLabel()
		Me.cmdShowCurrentExclusions = New System.Windows.Forms.Button()
		Me.cmdShowServerInfo = New System.Windows.Forms.Button()
		Me.cmdShowDriveExclusions = New System.Windows.Forms.Button()
		Me.tabControl.SuspendLayout()
		Me.TabPage1.SuspendLayout()
		Me.TabPage2.SuspendLayout()
		Me.TabPage3.SuspendLayout()
		Me.TabPage4.SuspendLayout()
		Me.TabPage5.SuspendLayout()
		Me.TabPage6.SuspendLayout()
		Me.TabPage7.SuspendLayout()
		CType(Me.picSpaceG, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.picSpaceY, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.picSpaceR, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.picEndcapR, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.picEndcapL, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.picDTBackground, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.picIcon_DriveState, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.picIcon_Drive, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.picDriveSpace, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.picIcon, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.SuspendLayout()
		'
		'cmdClose
		'
		Me.cmdClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
		Me.cmdClose.Location = New System.Drawing.Point(607, 485)
		Me.cmdClose.Margin = New System.Windows.Forms.Padding(3, 12, 3, 3)
		Me.cmdClose.Name = "cmdClose"
		Me.cmdClose.Size = New System.Drawing.Size(75, 25)
		Me.cmdClose.TabIndex = 4
		Me.cmdClose.Text = "Close"
		Me.cmdClose.UseVisualStyleBackColor = True
		'
		'txtEventLog_Message
		'
		Me.txtEventLog_Message.BackColor = System.Drawing.SystemColors.Window
		Me.txtEventLog_Message.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.txtEventLog_Message.Cursor = System.Windows.Forms.Cursors.IBeam
		Me.txtEventLog_Message.Location = New System.Drawing.Point(0, 282)
		Me.txtEventLog_Message.Margin = New System.Windows.Forms.Padding(0, 3, 0, 0)
		Me.txtEventLog_Message.Multiline = True
		Me.txtEventLog_Message.Name = "txtEventLog_Message"
		Me.txtEventLog_Message.ReadOnly = True
		Me.txtEventLog_Message.Size = New System.Drawing.Size(662, 81)
		Me.txtEventLog_Message.TabIndex = 30
		'
		'tabControl
		'
		Me.tabControl.Appearance = System.Windows.Forms.TabAppearance.Buttons
		Me.tabControl.Controls.Add(Me.TabPage1)
		Me.tabControl.Controls.Add(Me.TabPage2)
		Me.tabControl.Controls.Add(Me.TabPage3)
		Me.tabControl.Controls.Add(Me.TabPage4)
		Me.tabControl.Controls.Add(Me.TabPage5)
		Me.tabControl.Controls.Add(Me.TabPage6)
		Me.tabControl.Controls.Add(Me.TabPage7)
		Me.tabControl.Location = New System.Drawing.Point(12, 72)
		Me.tabControl.Name = "tabControl"
		Me.tabControl.Padding = New System.Drawing.Point(22, 6)
		Me.tabControl.SelectedIndex = 0
		Me.tabControl.Size = New System.Drawing.Size(670, 398)
		Me.tabControl.TabIndex = 31
		'
		'TabPage1
		'
		Me.TabPage1.Controls.Add(Me.lblNoIssues_Services)
		Me.TabPage1.Controls.Add(Me.lstResults_Services)
		Me.TabPage1.Location = New System.Drawing.Point(4, 31)
		Me.TabPage1.Margin = New System.Windows.Forms.Padding(0)
		Me.TabPage1.Name = "TabPage1"
		Me.TabPage1.Padding = New System.Windows.Forms.Padding(0, 9, 0, 0)
		Me.TabPage1.Size = New System.Drawing.Size(662, 363)
		Me.TabPage1.TabIndex = 0
		Me.TabPage1.Text = "Services"
		Me.TabPage1.UseVisualStyleBackColor = True
		'
		'lblNoIssues_Services
		'
		Me.lblNoIssues_Services.Anchor = System.Windows.Forms.AnchorStyles.Top
		Me.lblNoIssues_Services.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.lblNoIssues_Services.Location = New System.Drawing.Point(227, 125)
		Me.lblNoIssues_Services.Name = "lblNoIssues_Services"
		Me.lblNoIssues_Services.Size = New System.Drawing.Size(209, 44)
		Me.lblNoIssues_Services.TabIndex = 7
		Me.lblNoIssues_Services.Text = "No Issues Found"
		Me.lblNoIssues_Services.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
		'
		'lstResults_Services
		'
		Me.lstResults_Services.Location = New System.Drawing.Point(0, 12)
		Me.lstResults_Services.Margin = New System.Windows.Forms.Padding(0, 3, 0, 0)
		Me.lstResults_Services.Name = "lstResults_Services"
		Me.lstResults_Services.Size = New System.Drawing.Size(662, 351)
		Me.lstResults_Services.TabIndex = 8
		Me.lstResults_Services.UseCompatibleStateImageBehavior = False
		'
		'TabPage2
		'
		Me.TabPage2.Controls.Add(Me.lblNoIssues_Hotfixes)
		Me.TabPage2.Controls.Add(Me.lstResults_Hotfix)
		Me.TabPage2.Location = New System.Drawing.Point(4, 31)
		Me.TabPage2.Margin = New System.Windows.Forms.Padding(0)
		Me.TabPage2.Name = "TabPage2"
		Me.TabPage2.Padding = New System.Windows.Forms.Padding(0, 9, 0, 0)
		Me.TabPage2.Size = New System.Drawing.Size(662, 363)
		Me.TabPage2.TabIndex = 1
		Me.TabPage2.Text = "Hotfixes"
		Me.TabPage2.UseVisualStyleBackColor = True
		'
		'lblNoIssues_Hotfixes
		'
		Me.lblNoIssues_Hotfixes.Anchor = System.Windows.Forms.AnchorStyles.Top
		Me.lblNoIssues_Hotfixes.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.lblNoIssues_Hotfixes.Location = New System.Drawing.Point(227, 125)
		Me.lblNoIssues_Hotfixes.Name = "lblNoIssues_Hotfixes"
		Me.lblNoIssues_Hotfixes.Size = New System.Drawing.Size(209, 44)
		Me.lblNoIssues_Hotfixes.TabIndex = 8
		Me.lblNoIssues_Hotfixes.Text = "No Issues Found"
		Me.lblNoIssues_Hotfixes.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
		'
		'lstResults_Hotfix
		'
		Me.lstResults_Hotfix.Location = New System.Drawing.Point(0, 12)
		Me.lstResults_Hotfix.Margin = New System.Windows.Forms.Padding(0, 3, 0, 0)
		Me.lstResults_Hotfix.MultiSelect = False
		Me.lstResults_Hotfix.Name = "lstResults_Hotfix"
		Me.lstResults_Hotfix.Size = New System.Drawing.Size(662, 351)
		Me.lstResults_Hotfix.TabIndex = 1
		Me.lstResults_Hotfix.UseCompatibleStateImageBehavior = False
		'
		'TabPage3
		'
		Me.TabPage3.Controls.Add(Me.lblNoIssues_Eventlog)
		Me.TabPage3.Controls.Add(Me.txtEventLog_Message)
		Me.TabPage3.Controls.Add(Me.lstResults_Eventlog)
		Me.TabPage3.Location = New System.Drawing.Point(4, 31)
		Me.TabPage3.Margin = New System.Windows.Forms.Padding(0)
		Me.TabPage3.Name = "TabPage3"
		Me.TabPage3.Padding = New System.Windows.Forms.Padding(0, 9, 0, 0)
		Me.TabPage3.Size = New System.Drawing.Size(662, 363)
		Me.TabPage3.TabIndex = 2
		Me.TabPage3.Text = "Eventlog"
		Me.TabPage3.UseVisualStyleBackColor = True
		'
		'lblNoIssues_Eventlog
		'
		Me.lblNoIssues_Eventlog.Anchor = System.Windows.Forms.AnchorStyles.Top
		Me.lblNoIssues_Eventlog.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.lblNoIssues_Eventlog.Location = New System.Drawing.Point(227, 125)
		Me.lblNoIssues_Eventlog.Name = "lblNoIssues_Eventlog"
		Me.lblNoIssues_Eventlog.Size = New System.Drawing.Size(209, 44)
		Me.lblNoIssues_Eventlog.TabIndex = 31
		Me.lblNoIssues_Eventlog.Text = "No Issues Found"
		Me.lblNoIssues_Eventlog.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
		'
		'lstResults_Eventlog
		'
		Me.lstResults_Eventlog.Location = New System.Drawing.Point(0, 12)
		Me.lstResults_Eventlog.Margin = New System.Windows.Forms.Padding(0, 3, 0, 3)
		Me.lstResults_Eventlog.MultiSelect = False
		Me.lstResults_Eventlog.Name = "lstResults_Eventlog"
		Me.lstResults_Eventlog.Size = New System.Drawing.Size(662, 264)
		Me.lstResults_Eventlog.TabIndex = 6
		Me.lstResults_Eventlog.UseCompatibleStateImageBehavior = False
		'
		'TabPage4
		'
		Me.TabPage4.Controls.Add(Me.lblNoIssues_Registry)
		Me.TabPage4.Controls.Add(Me.lstResults_Registry)
		Me.TabPage4.Location = New System.Drawing.Point(4, 31)
		Me.TabPage4.Margin = New System.Windows.Forms.Padding(0)
		Me.TabPage4.Name = "TabPage4"
		Me.TabPage4.Padding = New System.Windows.Forms.Padding(0, 9, 0, 0)
		Me.TabPage4.Size = New System.Drawing.Size(662, 363)
		Me.TabPage4.TabIndex = 4
		Me.TabPage4.Text = "Registry"
		Me.TabPage4.UseVisualStyleBackColor = True
		'
		'lblNoIssues_Registry
		'
		Me.lblNoIssues_Registry.Anchor = System.Windows.Forms.AnchorStyles.Top
		Me.lblNoIssues_Registry.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.lblNoIssues_Registry.Location = New System.Drawing.Point(227, 125)
		Me.lblNoIssues_Registry.Name = "lblNoIssues_Registry"
		Me.lblNoIssues_Registry.Size = New System.Drawing.Size(209, 44)
		Me.lblNoIssues_Registry.TabIndex = 9
		Me.lblNoIssues_Registry.Text = "No Issues Found"
		Me.lblNoIssues_Registry.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
		'
		'lstResults_Registry
		'
		Me.lstResults_Registry.Location = New System.Drawing.Point(0, 12)
		Me.lstResults_Registry.Margin = New System.Windows.Forms.Padding(0, 3, 0, 0)
		Me.lstResults_Registry.Name = "lstResults_Registry"
		Me.lstResults_Registry.Size = New System.Drawing.Size(662, 351)
		Me.lstResults_Registry.TabIndex = 10
		Me.lstResults_Registry.UseCompatibleStateImageBehavior = False
		'
		'TabPage5
		'
		Me.TabPage5.Controls.Add(Me.lblNoIssues_FileScan)
		Me.TabPage5.Controls.Add(Me.lstResults_FileChecks)
		Me.TabPage5.Location = New System.Drawing.Point(4, 31)
		Me.TabPage5.Margin = New System.Windows.Forms.Padding(0)
		Me.TabPage5.Name = "TabPage5"
		Me.TabPage5.Padding = New System.Windows.Forms.Padding(0, 9, 0, 0)
		Me.TabPage5.Size = New System.Drawing.Size(662, 363)
		Me.TabPage5.TabIndex = 5
		Me.TabPage5.Text = "File Checks"
		Me.TabPage5.UseVisualStyleBackColor = True
		'
		'lblNoIssues_FileScan
		'
		Me.lblNoIssues_FileScan.Anchor = System.Windows.Forms.AnchorStyles.Top
		Me.lblNoIssues_FileScan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.lblNoIssues_FileScan.Location = New System.Drawing.Point(227, 125)
		Me.lblNoIssues_FileScan.Name = "lblNoIssues_FileScan"
		Me.lblNoIssues_FileScan.Size = New System.Drawing.Size(209, 44)
		Me.lblNoIssues_FileScan.TabIndex = 10
		Me.lblNoIssues_FileScan.Text = "No Issues Found"
		Me.lblNoIssues_FileScan.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
		'
		'lstResults_FileChecks
		'
		Me.lstResults_FileChecks.Location = New System.Drawing.Point(0, 12)
		Me.lstResults_FileChecks.Margin = New System.Windows.Forms.Padding(0, 3, 0, 0)
		Me.lstResults_FileChecks.MultiSelect = False
		Me.lstResults_FileChecks.Name = "lstResults_FileChecks"
		Me.lstResults_FileChecks.Size = New System.Drawing.Size(662, 351)
		Me.lstResults_FileChecks.TabIndex = 9
		Me.lstResults_FileChecks.UseCompatibleStateImageBehavior = False
		'
		'TabPage6
		'
		Me.TabPage6.Controls.Add(Me.lblNoIssues_WMIQuery)
		Me.TabPage6.Controls.Add(Me.lstResults_WMIQuery)
		Me.TabPage6.Location = New System.Drawing.Point(4, 31)
		Me.TabPage6.Name = "TabPage6"
		Me.TabPage6.Padding = New System.Windows.Forms.Padding(3)
		Me.TabPage6.Size = New System.Drawing.Size(662, 363)
		Me.TabPage6.TabIndex = 6
		Me.TabPage6.Text = "WMI Query"
		Me.TabPage6.UseVisualStyleBackColor = True
		'
		'lblNoIssues_WMIQuery
		'
		Me.lblNoIssues_WMIQuery.Anchor = System.Windows.Forms.AnchorStyles.Top
		Me.lblNoIssues_WMIQuery.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.lblNoIssues_WMIQuery.Location = New System.Drawing.Point(227, 125)
		Me.lblNoIssues_WMIQuery.Name = "lblNoIssues_WMIQuery"
		Me.lblNoIssues_WMIQuery.Size = New System.Drawing.Size(209, 44)
		Me.lblNoIssues_WMIQuery.TabIndex = 12
		Me.lblNoIssues_WMIQuery.Text = "No Issues Found"
		Me.lblNoIssues_WMIQuery.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
		'
		'lstResults_WMIQuery
		'
		Me.lstResults_WMIQuery.Location = New System.Drawing.Point(0, 12)
		Me.lstResults_WMIQuery.Margin = New System.Windows.Forms.Padding(0, 3, 0, 0)
		Me.lstResults_WMIQuery.MultiSelect = False
		Me.lstResults_WMIQuery.Name = "lstResults_WMIQuery"
		Me.lstResults_WMIQuery.Size = New System.Drawing.Size(662, 351)
		Me.lstResults_WMIQuery.TabIndex = 11
		Me.lstResults_WMIQuery.UseCompatibleStateImageBehavior = False
		'
		'TabPage7
		'
		Me.TabPage7.Controls.Add(Me.lstResults_DriveSpace_Excluded)
		Me.TabPage7.Controls.Add(Me.picSpaceG)
		Me.TabPage7.Controls.Add(Me.picSpaceY)
		Me.TabPage7.Controls.Add(Me.picSpaceR)
		Me.TabPage7.Controls.Add(Me.picEndcapR)
		Me.TabPage7.Controls.Add(Me.picEndcapL)
		Me.TabPage7.Controls.Add(Me.picDTBackground)
		Me.TabPage7.Controls.Add(Me.picIcon_DriveState)
		Me.TabPage7.Controls.Add(Me.lblCurrent_Warning)
		Me.TabPage7.Controls.Add(Me.lblCurrent_Critical)
		Me.TabPage7.Controls.Add(Me.Label1)
		Me.TabPage7.Controls.Add(Me.picIcon_Drive)
		Me.TabPage7.Controls.Add(Me.lblDriveLabel)
		Me.TabPage7.Controls.Add(Me.lblSize)
		Me.TabPage7.Controls.Add(Me.picDriveSpace)
		Me.TabPage7.Controls.Add(Me.lstResults_DriveSpace)
		Me.TabPage7.Controls.Add(Me.lstResults_DriveSpace_Background)
		Me.TabPage7.Location = New System.Drawing.Point(4, 31)
		Me.TabPage7.Margin = New System.Windows.Forms.Padding(0)
		Me.TabPage7.Name = "TabPage7"
		Me.TabPage7.Padding = New System.Windows.Forms.Padding(0, 9, 0, 0)
		Me.TabPage7.Size = New System.Drawing.Size(662, 363)
		Me.TabPage7.TabIndex = 3
		Me.TabPage7.Text = "Free Space"
		Me.TabPage7.UseVisualStyleBackColor = True
		'
		'lstResults_DriveSpace_Excluded
		'
		Me.lstResults_DriveSpace_Excluded.Location = New System.Drawing.Point(334, 15)
		Me.lstResults_DriveSpace_Excluded.Name = "lstResults_DriveSpace_Excluded"
		Me.lstResults_DriveSpace_Excluded.Size = New System.Drawing.Size(125, 25)
		Me.lstResults_DriveSpace_Excluded.TabIndex = 80
		Me.lstResults_DriveSpace_Excluded.UseCompatibleStateImageBehavior = False
		'
		'picSpaceG
		'
		Me.picSpaceG.Location = New System.Drawing.Point(117, 322)
		Me.picSpaceG.Margin = New System.Windows.Forms.Padding(1)
		Me.picSpaceG.Name = "picSpaceG"
		Me.picSpaceG.Size = New System.Drawing.Size(12, 12)
		Me.picSpaceG.TabIndex = 79
		Me.picSpaceG.TabStop = False
		'
		'picSpaceY
		'
		Me.picSpaceY.Location = New System.Drawing.Point(104, 322)
		Me.picSpaceY.Margin = New System.Windows.Forms.Padding(1)
		Me.picSpaceY.Name = "picSpaceY"
		Me.picSpaceY.Size = New System.Drawing.Size(12, 12)
		Me.picSpaceY.TabIndex = 78
		Me.picSpaceY.TabStop = False
		'
		'picSpaceR
		'
		Me.picSpaceR.Location = New System.Drawing.Point(91, 322)
		Me.picSpaceR.Margin = New System.Windows.Forms.Padding(1)
		Me.picSpaceR.Name = "picSpaceR"
		Me.picSpaceR.Size = New System.Drawing.Size(12, 12)
		Me.picSpaceR.TabIndex = 77
		Me.picSpaceR.TabStop = False
		'
		'picEndcapR
		'
		Me.picEndcapR.Location = New System.Drawing.Point(104, 309)
		Me.picEndcapR.Margin = New System.Windows.Forms.Padding(1)
		Me.picEndcapR.Name = "picEndcapR"
		Me.picEndcapR.Size = New System.Drawing.Size(12, 12)
		Me.picEndcapR.TabIndex = 76
		Me.picEndcapR.TabStop = False
		'
		'picEndcapL
		'
		Me.picEndcapL.Location = New System.Drawing.Point(91, 309)
		Me.picEndcapL.Margin = New System.Windows.Forms.Padding(1)
		Me.picEndcapL.Name = "picEndcapL"
		Me.picEndcapL.Size = New System.Drawing.Size(12, 12)
		Me.picEndcapL.TabIndex = 75
		Me.picEndcapL.TabStop = False
		'
		'picDTBackground
		'
		Me.picDTBackground.Location = New System.Drawing.Point(9, 339)
		Me.picDTBackground.Name = "picDTBackground"
		Me.picDTBackground.Size = New System.Drawing.Size(256, 14)
		Me.picDTBackground.TabIndex = 74
		Me.picDTBackground.TabStop = False
		'
		'picIcon_DriveState
		'
		Me.picIcon_DriveState.BackColor = System.Drawing.SystemColors.Window
		Me.picIcon_DriveState.Location = New System.Drawing.Point(66, 49)
		Me.picIcon_DriveState.Name = "picIcon_DriveState"
		Me.picIcon_DriveState.Size = New System.Drawing.Size(16, 16)
		Me.picIcon_DriveState.TabIndex = 52
		Me.picIcon_DriveState.TabStop = False
		'
		'lblCurrent_Warning
		'
		Me.lblCurrent_Warning.AutoSize = True
		Me.lblCurrent_Warning.BackColor = System.Drawing.SystemColors.Window
		Me.lblCurrent_Warning.Location = New System.Drawing.Point(12, 321)
		Me.lblCurrent_Warning.Name = "lblCurrent_Warning"
		Me.lblCurrent_Warning.Padding = New System.Windows.Forms.Padding(6, 0, 0, 0)
		Me.lblCurrent_Warning.Size = New System.Drawing.Size(73, 13)
		Me.lblCurrent_Warning.TabIndex = 50
		Me.lblCurrent_Warning.Text = "Warning: 0%"
		'
		'lblCurrent_Critical
		'
		Me.lblCurrent_Critical.AutoSize = True
		Me.lblCurrent_Critical.BackColor = System.Drawing.SystemColors.Window
		Me.lblCurrent_Critical.Location = New System.Drawing.Point(12, 308)
		Me.lblCurrent_Critical.Name = "lblCurrent_Critical"
		Me.lblCurrent_Critical.Padding = New System.Windows.Forms.Padding(6, 0, 0, 0)
		Me.lblCurrent_Critical.Size = New System.Drawing.Size(64, 13)
		Me.lblCurrent_Critical.TabIndex = 49
		Me.lblCurrent_Critical.Text = "Critical: 0%"
		'
		'Label1
		'
		Me.Label1.AutoSize = True
		Me.Label1.BackColor = System.Drawing.SystemColors.Window
		Me.Label1.Location = New System.Drawing.Point(12, 292)
		Me.Label1.Margin = New System.Windows.Forms.Padding(3)
		Me.Label1.Name = "Label1"
		Me.Label1.Size = New System.Drawing.Size(148, 13)
		Me.Label1.TabIndex = 48
		Me.Label1.Text = "Current drive size thresholds..."
		'
		'picIcon_Drive
		'
		Me.picIcon_Drive.BackColor = System.Drawing.SystemColors.Window
		Me.picIcon_Drive.Location = New System.Drawing.Point(12, 21)
		Me.picIcon_Drive.Margin = New System.Windows.Forms.Padding(12, 12, 3, 12)
		Me.picIcon_Drive.Name = "picIcon_Drive"
		Me.picIcon_Drive.Size = New System.Drawing.Size(48, 48)
		Me.picIcon_Drive.TabIndex = 35
		Me.picIcon_Drive.TabStop = False
		'
		'lblDriveLabel
		'
		Me.lblDriveLabel.AutoSize = True
		Me.lblDriveLabel.BackColor = System.Drawing.SystemColors.Window
		Me.lblDriveLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.lblDriveLabel.Location = New System.Drawing.Point(63, 23)
		Me.lblDriveLabel.Margin = New System.Windows.Forms.Padding(0, 18, 3, 3)
		Me.lblDriveLabel.Name = "lblDriveLabel"
		Me.lblDriveLabel.Size = New System.Drawing.Size(88, 16)
		Me.lblDriveLabel.TabIndex = 34
		Me.lblDriveLabel.Text = "lblDriveLabel"
		'
		'lblSize
		'
		Me.lblSize.AutoSize = True
		Me.lblSize.BackColor = System.Drawing.SystemColors.Window
		Me.lblSize.Location = New System.Drawing.Point(85, 50)
		Me.lblSize.Margin = New System.Windows.Forms.Padding(0, 1, 3, 3)
		Me.lblSize.Name = "lblSize"
		Me.lblSize.Size = New System.Drawing.Size(138, 13)
		Me.lblSize.TabIndex = 33
		Me.lblSize.Text = "0 GB free of 0 GB  (0% free)"
		'
		'picDriveSpace
		'
		Me.picDriveSpace.BackColor = System.Drawing.SystemColors.Window
		Me.picDriveSpace.Location = New System.Drawing.Point(66, 80)
		Me.picDriveSpace.Margin = New System.Windows.Forms.Padding(12)
		Me.picDriveSpace.Name = "picDriveSpace"
		Me.picDriveSpace.Size = New System.Drawing.Size(200, 200)
		Me.picDriveSpace.TabIndex = 32
		Me.picDriveSpace.TabStop = False
		'
		'lstResults_DriveSpace
		'
		Me.lstResults_DriveSpace.AlertPercentage = 10
		Me.lstResults_DriveSpace.Location = New System.Drawing.Point(334, 15)
		Me.lstResults_DriveSpace.Name = "lstResults_DriveSpace"
		Me.lstResults_DriveSpace.OwnerDraw = True
		Me.lstResults_DriveSpace.Size = New System.Drawing.Size(325, 345)
		Me.lstResults_DriveSpace.TabIndex = 53
		Me.lstResults_DriveSpace.UseCompatibleStateImageBehavior = False
		Me.lstResults_DriveSpace.WarningPercentage = 20
		'
		'lstResults_DriveSpace_Background
		'
		Me.lstResults_DriveSpace_Background.Location = New System.Drawing.Point(0, 12)
		Me.lstResults_DriveSpace_Background.Margin = New System.Windows.Forms.Padding(0, 3, 0, 0)
		Me.lstResults_DriveSpace_Background.Name = "lstResults_DriveSpace_Background"
		Me.lstResults_DriveSpace_Background.Size = New System.Drawing.Size(662, 351)
		Me.lstResults_DriveSpace_Background.TabIndex = 54
		Me.lstResults_DriveSpace_Background.UseCompatibleStateImageBehavior = False
		'
		'picIcon
		'
		Me.picIcon.Location = New System.Drawing.Point(12, 12)
		Me.picIcon.Name = "picIcon"
		Me.picIcon.Size = New System.Drawing.Size(48, 48)
		Me.picIcon.TabIndex = 34
		Me.picIcon.TabStop = False
		'
		'lblSubTitle
		'
		Me.lblSubTitle.AutoSize = True
		Me.lblSubTitle.Location = New System.Drawing.Point(66, 40)
		Me.lblSubTitle.Margin = New System.Windows.Forms.Padding(3)
		Me.lblSubTitle.Name = "lblSubTitle"
		Me.lblSubTitle.Size = New System.Drawing.Size(268, 13)
		Me.lblSubTitle.TabIndex = 33
		Me.lblSubTitle.Text = "Select an page below to see the detailed scan results..."
		'
		'lblServerName
		'
		Me.lblServerName.AutoSize = True
		Me.lblServerName.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.lblServerName.Location = New System.Drawing.Point(66, 18)
		Me.lblServerName.Margin = New System.Windows.Forms.Padding(3, 9, 3, 3)
		Me.lblServerName.Name = "lblServerName"
		Me.lblServerName.Size = New System.Drawing.Size(99, 16)
		Me.lblServerName.TabIndex = 32
		Me.lblServerName.Text = "lblServerName"
		'
		'cmdListAllInstalledHotfixes
		'
		Me.cmdListAllInstalledHotfixes.Location = New System.Drawing.Point(206, 485)
		Me.cmdListAllInstalledHotfixes.Name = "cmdListAllInstalledHotfixes"
		Me.cmdListAllInstalledHotfixes.Size = New System.Drawing.Size(175, 25)
		Me.cmdListAllInstalledHotfixes.TabIndex = 35
		Me.cmdListAllInstalledHotfixes.Text = "List All Installed Hotfixes"
		Me.cmdListAllInstalledHotfixes.UseVisualStyleBackColor = True
		'
		'proProgress
		'
		Me.proProgress.Location = New System.Drawing.Point(16, 497)
		Me.proProgress.Margin = New System.Windows.Forms.Padding(3, 3, 9, 3)
		Me.proProgress.Name = "proProgress"
		Me.proProgress.Size = New System.Drawing.Size(579, 13)
		Me.proProgress.Style = System.Windows.Forms.ProgressBarStyle.Continuous
		Me.proProgress.TabIndex = 36
		'
		'lnkHelp
		'
		Me.lnkHelp.Location = New System.Drawing.Point(610, 9)
		Me.lnkHelp.Margin = New System.Windows.Forms.Padding(0)
		Me.lnkHelp.Name = "lnkHelp"
		Me.lnkHelp.Size = New System.Drawing.Size(75, 15)
		Me.lnkHelp.TabIndex = 41
		Me.lnkHelp.TabStop = True
		Me.lnkHelp.Text = "Help"
		Me.lnkHelp.TextAlign = System.Drawing.ContentAlignment.TopRight
		'
		'lnkNote
		'
		Me.lnkNote.LinkColor = System.Drawing.Color.Blue
		Me.lnkNote.Location = New System.Drawing.Point(610, 30)
		Me.lnkNote.Margin = New System.Windows.Forms.Padding(0, 6, 0, 0)
		Me.lnkNote.Name = "lnkNote"
		Me.lnkNote.Size = New System.Drawing.Size(75, 15)
		Me.lnkNote.TabIndex = 44
		Me.lnkNote.TabStop = True
		Me.lnkNote.Text = "Admin Note"
		Me.lnkNote.TextAlign = System.Drawing.ContentAlignment.TopRight
		'
		'cmdShowCurrentExclusions
		'
		Me.cmdShowCurrentExclusions.Location = New System.Drawing.Point(206, 457)
		Me.cmdShowCurrentExclusions.Name = "cmdShowCurrentExclusions"
		Me.cmdShowCurrentExclusions.Size = New System.Drawing.Size(175, 25)
		Me.cmdShowCurrentExclusions.TabIndex = 46
		Me.cmdShowCurrentExclusions.Text = "Show Scan Settings"
		Me.cmdShowCurrentExclusions.UseVisualStyleBackColor = True
		'
		'cmdShowServerInfo
		'
		Me.cmdShowServerInfo.Location = New System.Drawing.Point(16, 485)
		Me.cmdShowServerInfo.Margin = New System.Windows.Forms.Padding(3, 3, 12, 3)
		Me.cmdShowServerInfo.Name = "cmdShowServerInfo"
		Me.cmdShowServerInfo.Size = New System.Drawing.Size(175, 25)
		Me.cmdShowServerInfo.TabIndex = 52
		Me.cmdShowServerInfo.Text = "Show Server Details..."
		Me.cmdShowServerInfo.UseVisualStyleBackColor = True
		'
		'cmdShowDriveExclusions
		'
		Me.cmdShowDriveExclusions.Location = New System.Drawing.Point(206, 429)
		Me.cmdShowDriveExclusions.Name = "cmdShowDriveExclusions"
		Me.cmdShowDriveExclusions.Size = New System.Drawing.Size(175, 25)
		Me.cmdShowDriveExclusions.TabIndex = 53
		Me.cmdShowDriveExclusions.Text = "Show Drive Exclusions"
		Me.cmdShowDriveExclusions.UseVisualStyleBackColor = True
		'
		'frmScanResults
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(694, 522)
		Me.Controls.Add(Me.cmdShowDriveExclusions)
		Me.Controls.Add(Me.cmdShowServerInfo)
		Me.Controls.Add(Me.cmdListAllInstalledHotfixes)
		Me.Controls.Add(Me.lnkNote)
		Me.Controls.Add(Me.lnkHelp)
		Me.Controls.Add(Me.picIcon)
		Me.Controls.Add(Me.cmdShowCurrentExclusions)
		Me.Controls.Add(Me.lblSubTitle)
		Me.Controls.Add(Me.lblServerName)
		Me.Controls.Add(Me.tabControl)
		Me.Controls.Add(Me.cmdClose)
		Me.Controls.Add(Me.proProgress)
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
		Me.MinimizeBox = False
		Me.Name = "frmScanResults"
		Me.ShowIcon = False
		Me.ShowInTaskbar = False
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
		Me.Text = " Detailed Scan Results"
		Me.tabControl.ResumeLayout(False)
		Me.TabPage1.ResumeLayout(False)
		Me.TabPage2.ResumeLayout(False)
		Me.TabPage3.ResumeLayout(False)
		Me.TabPage3.PerformLayout()
		Me.TabPage4.ResumeLayout(False)
		Me.TabPage5.ResumeLayout(False)
		Me.TabPage6.ResumeLayout(False)
		Me.TabPage7.ResumeLayout(False)
		Me.TabPage7.PerformLayout()
		CType(Me.picSpaceG, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.picSpaceY, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.picSpaceR, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.picEndcapR, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.picEndcapL, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.picDTBackground, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.picIcon_DriveState, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.picIcon_Drive, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.picDriveSpace, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.picIcon, System.ComponentModel.ISupportInitialize).EndInit()
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub
	Friend WithEvents cmdClose As System.Windows.Forms.Button
	Friend WithEvents txtEventLog_Message As System.Windows.Forms.TextBox
	Friend WithEvents tabControl As System.Windows.Forms.TabControl
	Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
	Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
	Friend WithEvents TabPage3 As System.Windows.Forms.TabPage
	Friend WithEvents lblNoIssues_Services As System.Windows.Forms.Label
	Friend WithEvents lblNoIssues_Hotfixes As System.Windows.Forms.Label
	Friend WithEvents lblNoIssues_Eventlog As System.Windows.Forms.Label
	Friend WithEvents TabPage7 As System.Windows.Forms.TabPage
	Friend WithEvents picIcon_Drive As System.Windows.Forms.PictureBox
	Friend WithEvents lblDriveLabel As System.Windows.Forms.Label
	Friend WithEvents lblSize As System.Windows.Forms.Label
	Friend WithEvents picDriveSpace As System.Windows.Forms.PictureBox
	Friend WithEvents lblCurrent_Critical As System.Windows.Forms.Label
	Friend WithEvents Label1 As System.Windows.Forms.Label
	Friend WithEvents lblCurrent_Warning As System.Windows.Forms.Label
	Friend WithEvents picIcon As System.Windows.Forms.PictureBox
	Friend WithEvents lblSubTitle As System.Windows.Forms.Label
	Friend WithEvents lblServerName As System.Windows.Forms.Label
	Friend WithEvents cmdListAllInstalledHotfixes As System.Windows.Forms.Button
	Friend WithEvents proProgress As System.Windows.Forms.ProgressBar
	Friend WithEvents lnkHelp As System.Windows.Forms.LinkLabel
	Friend WithEvents lnkNote As System.Windows.Forms.LinkLabel
	Friend WithEvents cmdShowCurrentExclusions As System.Windows.Forms.Button
	Friend WithEvents TabPage4 As System.Windows.Forms.TabPage
	Friend WithEvents lblNoIssues_Registry As System.Windows.Forms.Label
	Friend WithEvents TabPage5 As System.Windows.Forms.TabPage
	Friend WithEvents lblNoIssues_FileScan As System.Windows.Forms.Label
	Friend WithEvents picIcon_DriveState As System.Windows.Forms.PictureBox
	Friend WithEvents lstResults_DriveSpace As ServerSystemChecker.ctrlListView_DriveSpaceTile
	Friend WithEvents cmdShowServerInfo As System.Windows.Forms.Button
	Friend WithEvents picSpaceG As System.Windows.Forms.PictureBox
	Friend WithEvents picSpaceY As System.Windows.Forms.PictureBox
	Friend WithEvents picSpaceR As System.Windows.Forms.PictureBox
	Friend WithEvents picEndcapR As System.Windows.Forms.PictureBox
	Friend WithEvents picEndcapL As System.Windows.Forms.PictureBox
	Friend WithEvents picDTBackground As System.Windows.Forms.PictureBox
	Friend WithEvents lstResults_DriveSpace_Background As System.Windows.Forms.ListView
	Friend WithEvents lstResults_Eventlog As ServerSystemChecker.ctrlListView_CollapseGroups
	Friend WithEvents lstResults_Hotfix As ServerSystemChecker.ctrlListView_CollapseGroups
	Friend WithEvents lstResults_FileChecks As ServerSystemChecker.ctrlListView_CollapseGroups
	Friend WithEvents lstResults_Services As ServerSystemChecker.ctrlListView_CollapseGroups
	Friend WithEvents lstResults_Registry As ServerSystemChecker.ctrlListView_CollapseGroups
	Friend WithEvents TabPage6 As System.Windows.Forms.TabPage
	Friend WithEvents lblNoIssues_WMIQuery As System.Windows.Forms.Label
	Friend WithEvents lstResults_WMIQuery As ServerSystemChecker.ctrlListView_CollapseGroups
	Friend WithEvents lstResults_DriveSpace_Excluded As System.Windows.Forms.ListView
	Friend WithEvents cmdShowDriveExclusions As System.Windows.Forms.Button
End Class
