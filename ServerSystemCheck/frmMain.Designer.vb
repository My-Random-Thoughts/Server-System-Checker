<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMain
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
		Me.stsStatus = New System.Windows.Forms.StatusStrip()
		Me.tssInfo = New System.Windows.Forms.ToolStripStatusLabel()
		Me.ToolStripStatusSpacer = New System.Windows.Forms.ToolStripStatusLabel()
		Me.NetworkAvailabilityToolStripStatus = New System.Windows.Forms.ToolStripStatusLabel()
		Me.ToolStripStatusDiv0 = New System.Windows.Forms.ToolStripStatusLabel()
		Me.ToolStripStatusReadOnly = New System.Windows.Forms.ToolStripStatusLabel()
		Me.ToolStripStatusDiv1 = New System.Windows.Forms.ToolStripStatusLabel()
		Me.ToolStripStatusAdmin = New System.Windows.Forms.ToolStripStatusLabel()
		Me.conSplitContainer = New System.Windows.Forms.SplitContainer()
		Me.conSplitLeft = New System.Windows.Forms.SplitContainer()
		Me.lblNoGroupsAtThisLevel = New System.Windows.Forms.Label()
		Me.tvwServerList = New System.Windows.Forms.TreeView()
		Me.mnuProperties = New System.Windows.Forms.MenuStrip()
		Me.ToolStripMenuDescriptionExpandCollapse = New System.Windows.Forms.ToolStripMenuItem()
		Me.ToolStripMenuEditDescription = New System.Windows.Forms.ToolStripMenuItem()
		Me.txtDescription = New System.Windows.Forms.TextBox()
		Me.tvwDescription = New System.Windows.Forms.TreeView()
		Me.lblNoResourcesAtThisLevel = New System.Windows.Forms.Label()
		Me.panSplashPanel = New System.Windows.Forms.Panel()
		Me.lblVersion = New System.Windows.Forms.Label()
		Me.picSplashIcon6 = New System.Windows.Forms.PictureBox()
		Me.lblSplash6 = New System.Windows.Forms.Label()
		Me.lblSplashSubTitle = New System.Windows.Forms.Label()
		Me.picSplashLogo = New System.Windows.Forms.PictureBox()
		Me.lblSplashTitle = New System.Windows.Forms.Label()
		Me.lstSplashOptions = New System.Windows.Forms.ListView()
		Me.picSplashIcon7 = New System.Windows.Forms.PictureBox()
		Me.lblSplash7 = New System.Windows.Forms.Label()
		Me.picSplashIcon5 = New System.Windows.Forms.PictureBox()
		Me.lblSplash5 = New System.Windows.Forms.Label()
		Me.lblSplash4 = New System.Windows.Forms.Label()
		Me.lblSplash3 = New System.Windows.Forms.Label()
		Me.lblSplash2 = New System.Windows.Forms.Label()
		Me.lblSplash1 = New System.Windows.Forms.Label()
		Me.picSplashIcon1 = New System.Windows.Forms.PictureBox()
		Me.picSplashIcon2 = New System.Windows.Forms.PictureBox()
		Me.picSplashIcon3 = New System.Windows.Forms.PictureBox()
		Me.picSplashIcon4 = New System.Windows.Forms.PictureBox()
		Me.lstResources = New ServerSystemChecker.ctrlListView_CollapseGroups()
		Me.picHeaderBar = New System.Windows.Forms.PictureBox()
		Me.lstHeaderBar = New System.Windows.Forms.ListView()
		Me.ListView1 = New System.Windows.Forms.ListView()
		Me.lstHeaderBarBorder = New System.Windows.Forms.ListView()
		Me.FileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.NewToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.OpenToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.ToolStripMenuItem5 = New System.Windows.Forms.ToolStripSeparator()
		Me.CloseToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.ToolStripMenuItem4 = New System.Windows.Forms.ToolStripSeparator()
		Me.AssociateSSCFilesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripSeparator()
		Me.ExitToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.OptionsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.ResetSplitterBarToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.ResetColumnWidthsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.ToolStripMenuItem6 = New System.Windows.Forms.ToolStripSeparator()
		Me.ConfigureServerConnectionsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.ToolStripMenuItem2 = New System.Windows.Forms.ToolStripSeparator()
		Me.SettingsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.HelpToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.OpenHelpToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.ToolStripMenuItem3 = New System.Windows.Forms.ToolStripSeparator()
		Me.AboutToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.CollapseAllToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.ExpandAllToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.ShowFullPathToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.mnuMenu = New System.Windows.Forms.MenuStrip()
		Me.SearchToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.FindServerToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.FindResourceToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.ToolStripMenuItem7 = New System.Windows.Forms.ToolStripSeparator()
		Me.ShowServerStatsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.stsStatus.SuspendLayout()
		Me.conSplitContainer.Panel1.SuspendLayout()
		Me.conSplitContainer.Panel2.SuspendLayout()
		Me.conSplitContainer.SuspendLayout()
		Me.conSplitLeft.Panel1.SuspendLayout()
		Me.conSplitLeft.Panel2.SuspendLayout()
		Me.conSplitLeft.SuspendLayout()
		Me.mnuProperties.SuspendLayout()
		Me.panSplashPanel.SuspendLayout()
		CType(Me.picSplashIcon6, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.picSplashLogo, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.picSplashIcon7, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.picSplashIcon5, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.picSplashIcon1, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.picSplashIcon2, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.picSplashIcon3, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.picSplashIcon4, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.picHeaderBar, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.mnuMenu.SuspendLayout()
		Me.SuspendLayout()
		'
		'stsStatus
		'
		Me.stsStatus.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tssInfo, Me.ToolStripStatusSpacer, Me.NetworkAvailabilityToolStripStatus, Me.ToolStripStatusDiv0, Me.ToolStripStatusReadOnly, Me.ToolStripStatusDiv1, Me.ToolStripStatusAdmin})
		Me.stsStatus.Location = New System.Drawing.Point(0, 548)
		Me.stsStatus.Name = "stsStatus"
		Me.stsStatus.Size = New System.Drawing.Size(992, 22)
		Me.stsStatus.TabIndex = 0
		'
		'tssInfo
		'
		Me.tssInfo.Name = "tssInfo"
		Me.tssInfo.Size = New System.Drawing.Size(733, 17)
		Me.tssInfo.Spring = True
		Me.tssInfo.Text = "Ready"
		Me.tssInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
		'
		'ToolStripStatusSpacer
		'
		Me.ToolStripStatusSpacer.Name = "ToolStripStatusSpacer"
		Me.ToolStripStatusSpacer.Size = New System.Drawing.Size(10, 17)
		Me.ToolStripStatusSpacer.Text = " "
		'
		'NetworkAvailabilityToolStripStatus
		'
		Me.NetworkAvailabilityToolStripStatus.Margin = New System.Windows.Forms.Padding(0, 3, 3, 2)
		Me.NetworkAvailabilityToolStripStatus.Name = "NetworkAvailabilityToolStripStatus"
		Me.NetworkAvailabilityToolStripStatus.Size = New System.Drawing.Size(16, 17)
		Me.NetworkAvailabilityToolStripStatus.Text = "N"
		'
		'ToolStripStatusDiv0
		'
		Me.ToolStripStatusDiv0.BorderSides = CType((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right), System.Windows.Forms.ToolStripStatusLabelBorderSides)
		Me.ToolStripStatusDiv0.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter
		Me.ToolStripStatusDiv0.Margin = New System.Windows.Forms.Padding(3, 3, 1, 2)
		Me.ToolStripStatusDiv0.Name = "ToolStripStatusDiv0"
		Me.ToolStripStatusDiv0.Size = New System.Drawing.Size(4, 17)
		'
		'ToolStripStatusReadOnly
		'
		Me.ToolStripStatusReadOnly.Name = "ToolStripStatusReadOnly"
		Me.ToolStripStatusReadOnly.Size = New System.Drawing.Size(63, 17)
		Me.ToolStripStatusReadOnly.Text = "Read-Only"
		'
		'ToolStripStatusDiv1
		'
		Me.ToolStripStatusDiv1.BorderSides = CType((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right), System.Windows.Forms.ToolStripStatusLabelBorderSides)
		Me.ToolStripStatusDiv1.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter
		Me.ToolStripStatusDiv1.Margin = New System.Windows.Forms.Padding(3, 3, 1, 2)
		Me.ToolStripStatusDiv1.Name = "ToolStripStatusDiv1"
		Me.ToolStripStatusDiv1.Size = New System.Drawing.Size(4, 17)
		'
		'ToolStripStatusAdmin
		'
		Me.ToolStripStatusAdmin.Name = "ToolStripStatusAdmin"
		Me.ToolStripStatusAdmin.Size = New System.Drawing.Size(136, 17)
		Me.ToolStripStatusAdmin.Text = "Admin Options Disabled"
		'
		'conSplitContainer
		'
		Me.conSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill
		Me.conSplitContainer.Location = New System.Drawing.Point(0, 24)
		Me.conSplitContainer.Name = "conSplitContainer"
		'
		'conSplitContainer.Panel1
		'
		Me.conSplitContainer.Panel1.Controls.Add(Me.conSplitLeft)
		'
		'conSplitContainer.Panel2
		'
		Me.conSplitContainer.Panel2.Controls.Add(Me.lblNoResourcesAtThisLevel)
		Me.conSplitContainer.Panel2.Controls.Add(Me.panSplashPanel)
		Me.conSplitContainer.Panel2.Controls.Add(Me.lstResources)
		Me.conSplitContainer.Panel2.Controls.Add(Me.picHeaderBar)
		Me.conSplitContainer.Panel2.Controls.Add(Me.lstHeaderBar)
		Me.conSplitContainer.Size = New System.Drawing.Size(992, 524)
		Me.conSplitContainer.SplitterDistance = 200
		Me.conSplitContainer.TabIndex = 2
		'
		'conSplitLeft
		'
		Me.conSplitLeft.Dock = System.Windows.Forms.DockStyle.Fill
		Me.conSplitLeft.Location = New System.Drawing.Point(0, 0)
		Me.conSplitLeft.Name = "conSplitLeft"
		Me.conSplitLeft.Orientation = System.Windows.Forms.Orientation.Horizontal
		'
		'conSplitLeft.Panel1
		'
		Me.conSplitLeft.Panel1.Controls.Add(Me.lblNoGroupsAtThisLevel)
		Me.conSplitLeft.Panel1.Controls.Add(Me.tvwServerList)
		Me.conSplitLeft.Panel1.Controls.Add(Me.mnuProperties)
		'
		'conSplitLeft.Panel2
		'
		Me.conSplitLeft.Panel2.Controls.Add(Me.txtDescription)
		Me.conSplitLeft.Panel2.Controls.Add(Me.tvwDescription)
		Me.conSplitLeft.Size = New System.Drawing.Size(200, 524)
		Me.conSplitLeft.SplitterDistance = 425
		Me.conSplitLeft.SplitterWidth = 1
		Me.conSplitLeft.TabIndex = 8
		'
		'lblNoGroupsAtThisLevel
		'
		Me.lblNoGroupsAtThisLevel.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
				  Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.lblNoGroupsAtThisLevel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.lblNoGroupsAtThisLevel.Location = New System.Drawing.Point(12, 85)
		Me.lblNoGroupsAtThisLevel.Margin = New System.Windows.Forms.Padding(12, 0, 12, 0)
		Me.lblNoGroupsAtThisLevel.Name = "lblNoGroupsAtThisLevel"
		Me.lblNoGroupsAtThisLevel.Size = New System.Drawing.Size(176, 75)
		Me.lblNoGroupsAtThisLevel.TabIndex = 7
		Me.lblNoGroupsAtThisLevel.Text = "Right click here to add" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "groups and servers"
		Me.lblNoGroupsAtThisLevel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
		'
		'tvwServerList
		'
		Me.tvwServerList.AllowDrop = True
		Me.tvwServerList.Dock = System.Windows.Forms.DockStyle.Fill
		Me.tvwServerList.FullRowSelect = True
		Me.tvwServerList.HideSelection = False
		Me.tvwServerList.Location = New System.Drawing.Point(0, 0)
		Me.tvwServerList.Name = "tvwServerList"
		Me.tvwServerList.Size = New System.Drawing.Size(200, 401)
		Me.tvwServerList.TabIndex = 0
		'
		'mnuProperties
		'
		Me.mnuProperties.BackColor = System.Drawing.Color.Transparent
		Me.mnuProperties.Dock = System.Windows.Forms.DockStyle.Bottom
		Me.mnuProperties.GripMargin = New System.Windows.Forms.Padding(0)
		Me.mnuProperties.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuDescriptionExpandCollapse, Me.ToolStripMenuEditDescription})
		Me.mnuProperties.Location = New System.Drawing.Point(0, 401)
		Me.mnuProperties.Name = "mnuProperties"
		Me.mnuProperties.Padding = New System.Windows.Forms.Padding(0)
		Me.mnuProperties.ShowItemToolTips = True
		Me.mnuProperties.Size = New System.Drawing.Size(200, 24)
		Me.mnuProperties.TabIndex = 2
		Me.mnuProperties.Text = "MenuStrip1"
		'
		'ToolStripMenuDescriptionExpandCollapse
		'
		Me.ToolStripMenuDescriptionExpandCollapse.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
		Me.ToolStripMenuDescriptionExpandCollapse.Name = "ToolStripMenuDescriptionExpandCollapse"
		Me.ToolStripMenuDescriptionExpandCollapse.Size = New System.Drawing.Size(25, 24)
		Me.ToolStripMenuDescriptionExpandCollapse.Tag = "show"
		Me.ToolStripMenuDescriptionExpandCollapse.Text = "v"
		Me.ToolStripMenuDescriptionExpandCollapse.ToolTipText = "Show/Hide Group Description Window"
		'
		'ToolStripMenuEditDescription
		'
		Me.ToolStripMenuEditDescription.AutoToolTip = True
		Me.ToolStripMenuEditDescription.Name = "ToolStripMenuEditDescription"
		Me.ToolStripMenuEditDescription.Size = New System.Drawing.Size(84, 24)
		Me.ToolStripMenuEditDescription.Text = "e  Properties"
		Me.ToolStripMenuEditDescription.ToolTipText = "Edit Group Properties"
		'
		'txtDescription
		'
		Me.txtDescription.BackColor = System.Drawing.SystemColors.Window
		Me.txtDescription.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.txtDescription.Cursor = System.Windows.Forms.Cursors.Arrow
		Me.txtDescription.Location = New System.Drawing.Point(76, 3)
		Me.txtDescription.Multiline = True
		Me.txtDescription.Name = "txtDescription"
		Me.txtDescription.ReadOnly = True
		Me.txtDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
		Me.txtDescription.Size = New System.Drawing.Size(121, 92)
		Me.txtDescription.TabIndex = 3
		'
		'tvwDescription
		'
		Me.tvwDescription.Dock = System.Windows.Forms.DockStyle.Fill
		Me.tvwDescription.Location = New System.Drawing.Point(0, 0)
		Me.tvwDescription.Margin = New System.Windows.Forms.Padding(0)
		Me.tvwDescription.Name = "tvwDescription"
		Me.tvwDescription.Size = New System.Drawing.Size(200, 98)
		Me.tvwDescription.TabIndex = 2
		'
		'lblNoResourcesAtThisLevel
		'
		Me.lblNoResourcesAtThisLevel.Anchor = System.Windows.Forms.AnchorStyles.Top
		Me.lblNoResourcesAtThisLevel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.lblNoResourcesAtThisLevel.Location = New System.Drawing.Point(243, 85)
		Me.lblNoResourcesAtThisLevel.Name = "lblNoResourcesAtThisLevel"
		Me.lblNoResourcesAtThisLevel.Size = New System.Drawing.Size(300, 75)
		Me.lblNoResourcesAtThisLevel.TabIndex = 6
		Me.lblNoResourcesAtThisLevel.Text = "No resources at this level, or above" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Right click here to add some to the current" & _
		  " group"
		Me.lblNoResourcesAtThisLevel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
		'
		'panSplashPanel
		'
		Me.panSplashPanel.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
				  Or System.Windows.Forms.AnchorStyles.Left) _
				  Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.panSplashPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.panSplashPanel.Controls.Add(Me.lblVersion)
		Me.panSplashPanel.Controls.Add(Me.picSplashIcon6)
		Me.panSplashPanel.Controls.Add(Me.lblSplash6)
		Me.panSplashPanel.Controls.Add(Me.lblSplashSubTitle)
		Me.panSplashPanel.Controls.Add(Me.picSplashLogo)
		Me.panSplashPanel.Controls.Add(Me.lblSplashTitle)
		Me.panSplashPanel.Controls.Add(Me.lstSplashOptions)
		Me.panSplashPanel.Controls.Add(Me.picSplashIcon7)
		Me.panSplashPanel.Controls.Add(Me.lblSplash7)
		Me.panSplashPanel.Controls.Add(Me.picSplashIcon5)
		Me.panSplashPanel.Controls.Add(Me.lblSplash5)
		Me.panSplashPanel.Controls.Add(Me.lblSplash4)
		Me.panSplashPanel.Controls.Add(Me.lblSplash3)
		Me.panSplashPanel.Controls.Add(Me.lblSplash2)
		Me.panSplashPanel.Controls.Add(Me.lblSplash1)
		Me.panSplashPanel.Controls.Add(Me.picSplashIcon1)
		Me.panSplashPanel.Controls.Add(Me.picSplashIcon2)
		Me.panSplashPanel.Controls.Add(Me.picSplashIcon3)
		Me.panSplashPanel.Controls.Add(Me.picSplashIcon4)
		Me.panSplashPanel.Location = New System.Drawing.Point(12, 12)
		Me.panSplashPanel.Margin = New System.Windows.Forms.Padding(12, 12, 6, 12)
		Me.panSplashPanel.Name = "panSplashPanel"
		Me.panSplashPanel.Size = New System.Drawing.Size(761, 500)
		Me.panSplashPanel.TabIndex = 8
		'
		'lblVersion
		'
		Me.lblVersion.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.lblVersion.Location = New System.Drawing.Point(607, 11)
		Me.lblVersion.Margin = New System.Windows.Forms.Padding(3)
		Me.lblVersion.Name = "lblVersion"
		Me.lblVersion.Size = New System.Drawing.Size(143, 16)
		Me.lblVersion.TabIndex = 30
		Me.lblVersion.Text = "v0.0000.0000.0000"
		Me.lblVersion.TextAlign = System.Drawing.ContentAlignment.TopRight
		'
		'picSplashIcon6
		'
		Me.picSplashIcon6.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.picSplashIcon6.Location = New System.Drawing.Point(731, 448)
		Me.picSplashIcon6.Margin = New System.Windows.Forms.Padding(0, 3, 3, 3)
		Me.picSplashIcon6.Name = "picSplashIcon6"
		Me.picSplashIcon6.Size = New System.Drawing.Size(16, 16)
		Me.picSplashIcon6.TabIndex = 29
		Me.picSplashIcon6.TabStop = False
		'
		'lblSplash6
		'
		Me.lblSplash6.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.lblSplash6.Enabled = False
		Me.lblSplash6.FlatStyle = System.Windows.Forms.FlatStyle.Popup
		Me.lblSplash6.Location = New System.Drawing.Point(603, 448)
		Me.lblSplash6.Margin = New System.Windows.Forms.Padding(3)
		Me.lblSplash6.Name = "lblSplash6"
		Me.lblSplash6.Size = New System.Drawing.Size(125, 16)
		Me.lblSplash6.TabIndex = 28
		Me.lblSplash6.Text = "lblSplash6"
		Me.lblSplash6.TextAlign = System.Drawing.ContentAlignment.TopRight
		'
		'lblSplashSubTitle
		'
		Me.lblSplashSubTitle.Location = New System.Drawing.Point(81, 57)
		Me.lblSplashSubTitle.Margin = New System.Windows.Forms.Padding(3)
		Me.lblSplashSubTitle.Name = "lblSplashSubTitle"
		Me.lblSplashSubTitle.Size = New System.Drawing.Size(304, 15)
		Me.lblSplashSubTitle.TabIndex = 27
		Me.lblSplashSubTitle.Text = "System health checker for local and remove servers"
		Me.lblSplashSubTitle.TextAlign = System.Drawing.ContentAlignment.TopRight
		'
		'picSplashLogo
		'
		Me.picSplashLogo.Location = New System.Drawing.Point(24, 24)
		Me.picSplashLogo.Margin = New System.Windows.Forms.Padding(24, 24, 3, 12)
		Me.picSplashLogo.Name = "picSplashLogo"
		Me.picSplashLogo.Size = New System.Drawing.Size(48, 48)
		Me.picSplashLogo.TabIndex = 26
		Me.picSplashLogo.TabStop = False
		'
		'lblSplashTitle
		'
		Me.lblSplashTitle.AutoSize = True
		Me.lblSplashTitle.Location = New System.Drawing.Point(81, 24)
		Me.lblSplashTitle.Margin = New System.Windows.Forms.Padding(3)
		Me.lblSplashTitle.Name = "lblSplashTitle"
		Me.lblSplashTitle.Size = New System.Drawing.Size(178, 13)
		Me.lblSplashTitle.TabIndex = 25
		Me.lblSplashTitle.Text = "Welcome to Server System Checker"
		'
		'lstSplashOptions
		'
		Me.lstSplashOptions.BackColor = System.Drawing.SystemColors.Window
		Me.lstSplashOptions.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.lstSplashOptions.Location = New System.Drawing.Point(81, 100)
		Me.lstSplashOptions.Name = "lstSplashOptions"
		Me.lstSplashOptions.Size = New System.Drawing.Size(350, 300)
		Me.lstSplashOptions.TabIndex = 24
		Me.lstSplashOptions.UseCompatibleStateImageBehavior = False
		'
		'picSplashIcon7
		'
		Me.picSplashIcon7.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.picSplashIcon7.Location = New System.Drawing.Point(731, 470)
		Me.picSplashIcon7.Margin = New System.Windows.Forms.Padding(0, 3, 12, 12)
		Me.picSplashIcon7.Name = "picSplashIcon7"
		Me.picSplashIcon7.Size = New System.Drawing.Size(16, 16)
		Me.picSplashIcon7.TabIndex = 22
		Me.picSplashIcon7.TabStop = False
		'
		'lblSplash7
		'
		Me.lblSplash7.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.lblSplash7.Enabled = False
		Me.lblSplash7.FlatStyle = System.Windows.Forms.FlatStyle.Popup
		Me.lblSplash7.Location = New System.Drawing.Point(603, 470)
		Me.lblSplash7.Margin = New System.Windows.Forms.Padding(3)
		Me.lblSplash7.Name = "lblSplash7"
		Me.lblSplash7.Size = New System.Drawing.Size(125, 16)
		Me.lblSplash7.TabIndex = 21
		Me.lblSplash7.Text = "lblSplash7"
		Me.lblSplash7.TextAlign = System.Drawing.ContentAlignment.TopRight
		'
		'picSplashIcon5
		'
		Me.picSplashIcon5.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.picSplashIcon5.Location = New System.Drawing.Point(731, 426)
		Me.picSplashIcon5.Margin = New System.Windows.Forms.Padding(0, 3, 3, 3)
		Me.picSplashIcon5.Name = "picSplashIcon5"
		Me.picSplashIcon5.Size = New System.Drawing.Size(16, 16)
		Me.picSplashIcon5.TabIndex = 20
		Me.picSplashIcon5.TabStop = False
		'
		'lblSplash5
		'
		Me.lblSplash5.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.lblSplash5.Enabled = False
		Me.lblSplash5.FlatStyle = System.Windows.Forms.FlatStyle.Popup
		Me.lblSplash5.Location = New System.Drawing.Point(603, 426)
		Me.lblSplash5.Margin = New System.Windows.Forms.Padding(3)
		Me.lblSplash5.Name = "lblSplash5"
		Me.lblSplash5.Size = New System.Drawing.Size(125, 16)
		Me.lblSplash5.TabIndex = 18
		Me.lblSplash5.Text = "lblSplash5"
		Me.lblSplash5.TextAlign = System.Drawing.ContentAlignment.TopRight
		'
		'lblSplash4
		'
		Me.lblSplash4.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.lblSplash4.Enabled = False
		Me.lblSplash4.Location = New System.Drawing.Point(603, 404)
		Me.lblSplash4.Margin = New System.Windows.Forms.Padding(3)
		Me.lblSplash4.Name = "lblSplash4"
		Me.lblSplash4.Size = New System.Drawing.Size(125, 16)
		Me.lblSplash4.TabIndex = 17
		Me.lblSplash4.Text = "lblSplash4"
		Me.lblSplash4.TextAlign = System.Drawing.ContentAlignment.TopRight
		'
		'lblSplash3
		'
		Me.lblSplash3.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.lblSplash3.Enabled = False
		Me.lblSplash3.Location = New System.Drawing.Point(603, 382)
		Me.lblSplash3.Margin = New System.Windows.Forms.Padding(3)
		Me.lblSplash3.Name = "lblSplash3"
		Me.lblSplash3.Size = New System.Drawing.Size(125, 16)
		Me.lblSplash3.TabIndex = 16
		Me.lblSplash3.Text = "lblSplash3"
		Me.lblSplash3.TextAlign = System.Drawing.ContentAlignment.TopRight
		'
		'lblSplash2
		'
		Me.lblSplash2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.lblSplash2.Enabled = False
		Me.lblSplash2.Location = New System.Drawing.Point(603, 360)
		Me.lblSplash2.Margin = New System.Windows.Forms.Padding(3)
		Me.lblSplash2.Name = "lblSplash2"
		Me.lblSplash2.Size = New System.Drawing.Size(125, 16)
		Me.lblSplash2.TabIndex = 15
		Me.lblSplash2.Text = "lblSplash2"
		Me.lblSplash2.TextAlign = System.Drawing.ContentAlignment.TopRight
		'
		'lblSplash1
		'
		Me.lblSplash1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.lblSplash1.Enabled = False
		Me.lblSplash1.Location = New System.Drawing.Point(603, 338)
		Me.lblSplash1.Margin = New System.Windows.Forms.Padding(3)
		Me.lblSplash1.Name = "lblSplash1"
		Me.lblSplash1.Size = New System.Drawing.Size(125, 16)
		Me.lblSplash1.TabIndex = 14
		Me.lblSplash1.Text = "lblSplash1"
		Me.lblSplash1.TextAlign = System.Drawing.ContentAlignment.TopRight
		'
		'picSplashIcon1
		'
		Me.picSplashIcon1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.picSplashIcon1.Location = New System.Drawing.Point(731, 338)
		Me.picSplashIcon1.Margin = New System.Windows.Forms.Padding(0, 3, 3, 3)
		Me.picSplashIcon1.Name = "picSplashIcon1"
		Me.picSplashIcon1.Size = New System.Drawing.Size(16, 16)
		Me.picSplashIcon1.TabIndex = 11
		Me.picSplashIcon1.TabStop = False
		'
		'picSplashIcon2
		'
		Me.picSplashIcon2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.picSplashIcon2.Location = New System.Drawing.Point(731, 360)
		Me.picSplashIcon2.Margin = New System.Windows.Forms.Padding(0, 3, 3, 3)
		Me.picSplashIcon2.Name = "picSplashIcon2"
		Me.picSplashIcon2.Size = New System.Drawing.Size(16, 16)
		Me.picSplashIcon2.TabIndex = 10
		Me.picSplashIcon2.TabStop = False
		'
		'picSplashIcon3
		'
		Me.picSplashIcon3.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.picSplashIcon3.Location = New System.Drawing.Point(731, 382)
		Me.picSplashIcon3.Margin = New System.Windows.Forms.Padding(0, 3, 3, 3)
		Me.picSplashIcon3.Name = "picSplashIcon3"
		Me.picSplashIcon3.Size = New System.Drawing.Size(16, 16)
		Me.picSplashIcon3.TabIndex = 9
		Me.picSplashIcon3.TabStop = False
		'
		'picSplashIcon4
		'
		Me.picSplashIcon4.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.picSplashIcon4.Location = New System.Drawing.Point(731, 404)
		Me.picSplashIcon4.Margin = New System.Windows.Forms.Padding(0, 3, 3, 3)
		Me.picSplashIcon4.Name = "picSplashIcon4"
		Me.picSplashIcon4.Size = New System.Drawing.Size(16, 16)
		Me.picSplashIcon4.TabIndex = 8
		Me.picSplashIcon4.TabStop = False
		'
		'lstResources
		'
		Me.lstResources.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
				  Or System.Windows.Forms.AnchorStyles.Left) _
				  Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.lstResources.Location = New System.Drawing.Point(0, 24)
		Me.lstResources.Margin = New System.Windows.Forms.Padding(0)
		Me.lstResources.Name = "lstResources"
		Me.lstResources.Size = New System.Drawing.Size(788, 500)
		Me.lstResources.TabIndex = 9
		Me.lstResources.UseCompatibleStateImageBehavior = False
		'
		'picHeaderBar
		'
		Me.picHeaderBar.Location = New System.Drawing.Point(2, 2)
		Me.picHeaderBar.Margin = New System.Windows.Forms.Padding(1)
		Me.picHeaderBar.Name = "picHeaderBar"
		Me.picHeaderBar.Size = New System.Drawing.Size(93, 24)
		Me.picHeaderBar.TabIndex = 11
		Me.picHeaderBar.TabStop = False
		'
		'lstHeaderBar
		'
		Me.lstHeaderBar.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
				  Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.lstHeaderBar.Location = New System.Drawing.Point(0, 0)
		Me.lstHeaderBar.Margin = New System.Windows.Forms.Padding(0)
		Me.lstHeaderBar.Name = "lstHeaderBar"
		Me.lstHeaderBar.Size = New System.Drawing.Size(788, 25)
		Me.lstHeaderBar.TabIndex = 10
		Me.lstHeaderBar.UseCompatibleStateImageBehavior = False
		'
		'ListView1
		'
		Me.ListView1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
				  Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.ListView1.Location = New System.Drawing.Point(0, 0)
		Me.ListView1.Margin = New System.Windows.Forms.Padding(0)
		Me.ListView1.Name = "ListView1"
		Me.ListView1.Size = New System.Drawing.Size(790, 25)
		Me.ListView1.TabIndex = 10
		Me.ListView1.UseCompatibleStateImageBehavior = False
		'
		'lstHeaderBarBorder
		'
		Me.lstHeaderBarBorder.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
				  Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.lstHeaderBarBorder.Location = New System.Drawing.Point(0, 0)
		Me.lstHeaderBarBorder.Margin = New System.Windows.Forms.Padding(0)
		Me.lstHeaderBarBorder.Name = "lstHeaderBarBorder"
		Me.lstHeaderBarBorder.Size = New System.Drawing.Size(790, 25)
		Me.lstHeaderBarBorder.TabIndex = 10
		Me.lstHeaderBarBorder.UseCompatibleStateImageBehavior = False
		'
		'FileToolStripMenuItem
		'
		Me.FileToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.NewToolStripMenuItem, Me.OpenToolStripMenuItem, Me.ToolStripMenuItem5, Me.CloseToolStripMenuItem, Me.ToolStripMenuItem4, Me.AssociateSSCFilesToolStripMenuItem, Me.ToolStripMenuItem1, Me.ExitToolStripMenuItem})
		Me.FileToolStripMenuItem.Name = "FileToolStripMenuItem"
		Me.FileToolStripMenuItem.Size = New System.Drawing.Size(37, 20)
		Me.FileToolStripMenuItem.Text = "File"
		'
		'NewToolStripMenuItem
		'
		Me.NewToolStripMenuItem.Name = "NewToolStripMenuItem"
		Me.NewToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.N), System.Windows.Forms.Keys)
		Me.NewToolStripMenuItem.Size = New System.Drawing.Size(227, 22)
		Me.NewToolStripMenuItem.Text = "New Configuration..."
		'
		'OpenToolStripMenuItem
		'
		Me.OpenToolStripMenuItem.Name = "OpenToolStripMenuItem"
		Me.OpenToolStripMenuItem.ShortcutKeyDisplayString = ""
		Me.OpenToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.O), System.Windows.Forms.Keys)
		Me.OpenToolStripMenuItem.Size = New System.Drawing.Size(227, 22)
		Me.OpenToolStripMenuItem.Text = "Open..."
		'
		'ToolStripMenuItem5
		'
		Me.ToolStripMenuItem5.Name = "ToolStripMenuItem5"
		Me.ToolStripMenuItem5.Size = New System.Drawing.Size(224, 6)
		'
		'CloseToolStripMenuItem
		'
		Me.CloseToolStripMenuItem.Name = "CloseToolStripMenuItem"
		Me.CloseToolStripMenuItem.ShortcutKeyDisplayString = ""
		Me.CloseToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.W), System.Windows.Forms.Keys)
		Me.CloseToolStripMenuItem.Size = New System.Drawing.Size(227, 22)
		Me.CloseToolStripMenuItem.Text = "Close"
		'
		'ToolStripMenuItem4
		'
		Me.ToolStripMenuItem4.Name = "ToolStripMenuItem4"
		Me.ToolStripMenuItem4.Size = New System.Drawing.Size(224, 6)
		'
		'AssociateSSCFilesToolStripMenuItem
		'
		Me.AssociateSSCFilesToolStripMenuItem.Name = "AssociateSSCFilesToolStripMenuItem"
		Me.AssociateSSCFilesToolStripMenuItem.Size = New System.Drawing.Size(227, 22)
		Me.AssociateSSCFilesToolStripMenuItem.Text = "Associate .SSC Files"
		'
		'ToolStripMenuItem1
		'
		Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
		Me.ToolStripMenuItem1.Size = New System.Drawing.Size(224, 6)
		'
		'ExitToolStripMenuItem
		'
		Me.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem"
		Me.ExitToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.X), System.Windows.Forms.Keys)
		Me.ExitToolStripMenuItem.Size = New System.Drawing.Size(227, 22)
		Me.ExitToolStripMenuItem.Text = "Exit"
		'
		'OptionsToolStripMenuItem
		'
		Me.OptionsToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ResetSplitterBarToolStripMenuItem, Me.ResetColumnWidthsToolStripMenuItem, Me.ToolStripMenuItem6, Me.ConfigureServerConnectionsToolStripMenuItem, Me.ToolStripMenuItem2, Me.SettingsToolStripMenuItem})
		Me.OptionsToolStripMenuItem.Name = "OptionsToolStripMenuItem"
		Me.OptionsToolStripMenuItem.Size = New System.Drawing.Size(61, 20)
		Me.OptionsToolStripMenuItem.Text = "Options"
		'
		'ResetSplitterBarToolStripMenuItem
		'
		Me.ResetSplitterBarToolStripMenuItem.Name = "ResetSplitterBarToolStripMenuItem"
		Me.ResetSplitterBarToolStripMenuItem.Size = New System.Drawing.Size(241, 22)
		Me.ResetSplitterBarToolStripMenuItem.Text = "Reset Splitter Bar"
		'
		'ResetColumnWidthsToolStripMenuItem
		'
		Me.ResetColumnWidthsToolStripMenuItem.Name = "ResetColumnWidthsToolStripMenuItem"
		Me.ResetColumnWidthsToolStripMenuItem.Size = New System.Drawing.Size(241, 22)
		Me.ResetColumnWidthsToolStripMenuItem.Text = "Reset Column Widths"
		'
		'ToolStripMenuItem6
		'
		Me.ToolStripMenuItem6.Name = "ToolStripMenuItem6"
		Me.ToolStripMenuItem6.Size = New System.Drawing.Size(238, 6)
		'
		'ConfigureServerConnectionsToolStripMenuItem
		'
		Me.ConfigureServerConnectionsToolStripMenuItem.Name = "ConfigureServerConnectionsToolStripMenuItem"
		Me.ConfigureServerConnectionsToolStripMenuItem.Size = New System.Drawing.Size(241, 22)
		Me.ConfigureServerConnectionsToolStripMenuItem.Text = "Configure Server Connections..."
		'
		'ToolStripMenuItem2
		'
		Me.ToolStripMenuItem2.Name = "ToolStripMenuItem2"
		Me.ToolStripMenuItem2.Size = New System.Drawing.Size(238, 6)
		'
		'SettingsToolStripMenuItem
		'
		Me.SettingsToolStripMenuItem.Name = "SettingsToolStripMenuItem"
		Me.SettingsToolStripMenuItem.Size = New System.Drawing.Size(241, 22)
		Me.SettingsToolStripMenuItem.Text = "Settings..."
		'
		'HelpToolStripMenuItem
		'
		Me.HelpToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.OpenHelpToolStripMenuItem, Me.ToolStripMenuItem3, Me.AboutToolStripMenuItem})
		Me.HelpToolStripMenuItem.Name = "HelpToolStripMenuItem"
		Me.HelpToolStripMenuItem.Size = New System.Drawing.Size(44, 20)
		Me.HelpToolStripMenuItem.Text = "Help"
		'
		'OpenHelpToolStripMenuItem
		'
		Me.OpenHelpToolStripMenuItem.Name = "OpenHelpToolStripMenuItem"
		Me.OpenHelpToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F1
		Me.OpenHelpToolStripMenuItem.Size = New System.Drawing.Size(127, 22)
		Me.OpenHelpToolStripMenuItem.Text = "Help..."
		'
		'ToolStripMenuItem3
		'
		Me.ToolStripMenuItem3.Name = "ToolStripMenuItem3"
		Me.ToolStripMenuItem3.Size = New System.Drawing.Size(124, 6)
		'
		'AboutToolStripMenuItem
		'
		Me.AboutToolStripMenuItem.Name = "AboutToolStripMenuItem"
		Me.AboutToolStripMenuItem.Size = New System.Drawing.Size(127, 22)
		Me.AboutToolStripMenuItem.Text = "About"
		'
		'CollapseAllToolStripMenuItem
		'
		Me.CollapseAllToolStripMenuItem.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
		Me.CollapseAllToolStripMenuItem.Margin = New System.Windows.Forms.Padding(0, 0, 6, 0)
		Me.CollapseAllToolStripMenuItem.Name = "CollapseAllToolStripMenuItem"
		Me.CollapseAllToolStripMenuItem.Size = New System.Drawing.Size(27, 20)
		Me.CollapseAllToolStripMenuItem.Text = "C"
		Me.CollapseAllToolStripMenuItem.ToolTipText = "Collapse All Groups"
		'
		'ExpandAllToolStripMenuItem
		'
		Me.ExpandAllToolStripMenuItem.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
		Me.ExpandAllToolStripMenuItem.AutoToolTip = True
		Me.ExpandAllToolStripMenuItem.Name = "ExpandAllToolStripMenuItem"
		Me.ExpandAllToolStripMenuItem.Size = New System.Drawing.Size(25, 20)
		Me.ExpandAllToolStripMenuItem.Text = "E"
		Me.ExpandAllToolStripMenuItem.ToolTipText = "Expand All Groups"
		'
		'ShowFullPathToolStripMenuItem
		'
		Me.ShowFullPathToolStripMenuItem.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
		Me.ShowFullPathToolStripMenuItem.Margin = New System.Windows.Forms.Padding(0, 0, 6, 0)
		Me.ShowFullPathToolStripMenuItem.Name = "ShowFullPathToolStripMenuItem"
		Me.ShowFullPathToolStripMenuItem.Size = New System.Drawing.Size(26, 20)
		Me.ShowFullPathToolStripMenuItem.Text = "P"
		Me.ShowFullPathToolStripMenuItem.ToolTipText = "Show/Hide Full Path Header Bar"
		'
		'mnuMenu
		'
		Me.mnuMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileToolStripMenuItem, Me.SearchToolStripMenuItem, Me.OptionsToolStripMenuItem, Me.HelpToolStripMenuItem, Me.CollapseAllToolStripMenuItem, Me.ExpandAllToolStripMenuItem, Me.ShowFullPathToolStripMenuItem})
		Me.mnuMenu.Location = New System.Drawing.Point(0, 0)
		Me.mnuMenu.Name = "mnuMenu"
		Me.mnuMenu.Size = New System.Drawing.Size(992, 24)
		Me.mnuMenu.TabIndex = 1
		Me.mnuMenu.Text = "MenuStrip1"
		'
		'SearchToolStripMenuItem
		'
		Me.SearchToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FindServerToolStripMenuItem, Me.FindResourceToolStripMenuItem, Me.ToolStripMenuItem7, Me.ShowServerStatsToolStripMenuItem})
		Me.SearchToolStripMenuItem.Name = "SearchToolStripMenuItem"
		Me.SearchToolStripMenuItem.Size = New System.Drawing.Size(54, 20)
		Me.SearchToolStripMenuItem.Text = "Search"
		'
		'FindServerToolStripMenuItem
		'
		Me.FindServerToolStripMenuItem.Name = "FindServerToolStripMenuItem"
		Me.FindServerToolStripMenuItem.Size = New System.Drawing.Size(175, 22)
		Me.FindServerToolStripMenuItem.Text = "Find Server..."
		'
		'FindResourceToolStripMenuItem
		'
		Me.FindResourceToolStripMenuItem.Name = "FindResourceToolStripMenuItem"
		Me.FindResourceToolStripMenuItem.Size = New System.Drawing.Size(175, 22)
		Me.FindResourceToolStripMenuItem.Text = "Find Resource..."
		'
		'ToolStripMenuItem7
		'
		Me.ToolStripMenuItem7.Name = "ToolStripMenuItem7"
		Me.ToolStripMenuItem7.Size = New System.Drawing.Size(172, 6)
		'
		'ShowServerStatsToolStripMenuItem
		'
		Me.ShowServerStatsToolStripMenuItem.Name = "ShowServerStatsToolStripMenuItem"
		Me.ShowServerStatsToolStripMenuItem.Size = New System.Drawing.Size(175, 22)
		Me.ShowServerStatsToolStripMenuItem.Text = "Show Server Stats..."
		'
		'frmMain
		'
		Me.AllowDrop = True
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(992, 570)
		Me.Controls.Add(Me.conSplitContainer)
		Me.Controls.Add(Me.stsStatus)
		Me.Controls.Add(Me.mnuMenu)
		Me.MainMenuStrip = Me.mnuMenu
		Me.MinimumSize = New System.Drawing.Size(800, 500)
		Me.Name = "frmMain"
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
		Me.Text = " Server System Checker"
		Me.stsStatus.ResumeLayout(False)
		Me.stsStatus.PerformLayout()
		Me.conSplitContainer.Panel1.ResumeLayout(False)
		Me.conSplitContainer.Panel2.ResumeLayout(False)
		Me.conSplitContainer.ResumeLayout(False)
		Me.conSplitLeft.Panel1.ResumeLayout(False)
		Me.conSplitLeft.Panel1.PerformLayout()
		Me.conSplitLeft.Panel2.ResumeLayout(False)
		Me.conSplitLeft.Panel2.PerformLayout()
		Me.conSplitLeft.ResumeLayout(False)
		Me.mnuProperties.ResumeLayout(False)
		Me.mnuProperties.PerformLayout()
		Me.panSplashPanel.ResumeLayout(False)
		Me.panSplashPanel.PerformLayout()
		CType(Me.picSplashIcon6, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.picSplashLogo, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.picSplashIcon7, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.picSplashIcon5, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.picSplashIcon1, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.picSplashIcon2, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.picSplashIcon3, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.picSplashIcon4, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.picHeaderBar, System.ComponentModel.ISupportInitialize).EndInit()
		Me.mnuMenu.ResumeLayout(False)
		Me.mnuMenu.PerformLayout()
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub
	Friend WithEvents stsStatus As System.Windows.Forms.StatusStrip
	Friend WithEvents conSplitContainer As System.Windows.Forms.SplitContainer
	Friend WithEvents tvwServerList As System.Windows.Forms.TreeView
	Friend WithEvents lblNoResourcesAtThisLevel As System.Windows.Forms.Label
	Friend WithEvents ToolStripStatusAdmin As System.Windows.Forms.ToolStripStatusLabel
	Friend WithEvents panSplashPanel As System.Windows.Forms.Panel
	Friend WithEvents picSplashIcon4 As System.Windows.Forms.PictureBox
	Friend WithEvents picSplashIcon1 As System.Windows.Forms.PictureBox
	Friend WithEvents picSplashIcon2 As System.Windows.Forms.PictureBox
	Friend WithEvents picSplashIcon3 As System.Windows.Forms.PictureBox
	Friend WithEvents lblSplash2 As System.Windows.Forms.Label
	Friend WithEvents lblSplash1 As System.Windows.Forms.Label
	Friend WithEvents lblSplash4 As System.Windows.Forms.Label
	Friend WithEvents lblSplash3 As System.Windows.Forms.Label
	Friend WithEvents lblSplash5 As System.Windows.Forms.Label
	Friend WithEvents lblSplash7 As System.Windows.Forms.Label
	Friend WithEvents picSplashIcon5 As System.Windows.Forms.PictureBox
	Friend WithEvents picSplashIcon7 As System.Windows.Forms.PictureBox
	Friend WithEvents lblNoGroupsAtThisLevel As System.Windows.Forms.Label
	Friend WithEvents tssInfo As System.Windows.Forms.ToolStripStatusLabel
	Friend WithEvents ToolStripStatusDiv1 As System.Windows.Forms.ToolStripStatusLabel
	Friend WithEvents lstResources As ServerSystemChecker.ctrlListView_CollapseGroups
	Friend WithEvents NetworkAvailabilityToolStripStatus As System.Windows.Forms.ToolStripStatusLabel
	Friend WithEvents lblSplashSubTitle As System.Windows.Forms.Label
	Friend WithEvents picSplashLogo As System.Windows.Forms.PictureBox
	Friend WithEvents lblSplashTitle As System.Windows.Forms.Label
	Friend WithEvents lstSplashOptions As System.Windows.Forms.ListView
	Friend WithEvents picSplashIcon6 As System.Windows.Forms.PictureBox
	Friend WithEvents lblSplash6 As System.Windows.Forms.Label
	Friend WithEvents ToolStripStatusSpacer As System.Windows.Forms.ToolStripStatusLabel
	Friend WithEvents lstHeaderBar As System.Windows.Forms.ListView
	Friend WithEvents picHeaderBar As System.Windows.Forms.PictureBox
	Friend WithEvents ListView1 As System.Windows.Forms.ListView
	Friend WithEvents lstHeaderBarBorder As System.Windows.Forms.ListView
	Friend WithEvents ToolStripStatusDiv0 As System.Windows.Forms.ToolStripStatusLabel
	Friend WithEvents ToolStripStatusReadOnly As System.Windows.Forms.ToolStripStatusLabel
	Friend WithEvents conSplitLeft As System.Windows.Forms.SplitContainer
	Friend WithEvents mnuProperties As System.Windows.Forms.MenuStrip
	Friend WithEvents ToolStripMenuDescriptionExpandCollapse As System.Windows.Forms.ToolStripMenuItem
	Friend WithEvents tvwDescription As System.Windows.Forms.TreeView
	Friend WithEvents txtDescription As System.Windows.Forms.TextBox
	Friend WithEvents ToolStripMenuEditDescription As System.Windows.Forms.ToolStripMenuItem
	Friend WithEvents FileToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Friend WithEvents NewToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Friend WithEvents OpenToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Friend WithEvents ToolStripMenuItem5 As System.Windows.Forms.ToolStripSeparator
	Friend WithEvents CloseToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Friend WithEvents ToolStripMenuItem4 As System.Windows.Forms.ToolStripSeparator
	Friend WithEvents AssociateSSCFilesToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Friend WithEvents ToolStripMenuItem1 As System.Windows.Forms.ToolStripSeparator
	Friend WithEvents ExitToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Friend WithEvents OptionsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Friend WithEvents ResetSplitterBarToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Friend WithEvents ResetColumnWidthsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Friend WithEvents ToolStripMenuItem6 As System.Windows.Forms.ToolStripSeparator
	Friend WithEvents ConfigureServerConnectionsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Friend WithEvents ToolStripMenuItem2 As System.Windows.Forms.ToolStripSeparator
	Friend WithEvents SettingsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Friend WithEvents HelpToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Friend WithEvents OpenHelpToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Friend WithEvents ToolStripMenuItem3 As System.Windows.Forms.ToolStripSeparator
	Friend WithEvents AboutToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Friend WithEvents CollapseAllToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Friend WithEvents ExpandAllToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Friend WithEvents ShowFullPathToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Friend WithEvents mnuMenu As System.Windows.Forms.MenuStrip
	Friend WithEvents SearchToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Friend WithEvents FindServerToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Friend WithEvents lblVersion As System.Windows.Forms.Label
	Friend WithEvents FindResourceToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Friend WithEvents ToolStripMenuItem7 As System.Windows.Forms.ToolStripSeparator
	Friend WithEvents ShowServerStatsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem

End Class
