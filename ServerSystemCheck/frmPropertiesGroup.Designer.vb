<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPropertiesGroup
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
		Me.tabProperties = New System.Windows.Forms.TabControl()
		Me.TabPage1 = New System.Windows.Forms.TabPage()
		Me.lblDescriptionCount = New System.Windows.Forms.Label()
		Me.cmdEnableColours = New System.Windows.Forms.Button()
		Me.lblInfoGroupColours = New System.Windows.Forms.Label()
		Me.txtDescription = New System.Windows.Forms.TextBox()
		Me.Label4 = New System.Windows.Forms.Label()
		Me.Label1 = New System.Windows.Forms.Label()
		Me.txtGroupName = New System.Windows.Forms.TextBox()
		Me.Label3 = New System.Windows.Forms.Label()
		Me.cmoGroupColour = New ServerSystemChecker.ctrlComboBox_Icons()
		Me.TabPage2 = New System.Windows.Forms.TabPage()
		Me.lstResourceStats = New ServerSystemChecker.ctrlListView_SubIcons()
		Me.TabPage3 = New System.Windows.Forms.TabPage()
		Me.lblStats = New System.Windows.Forms.Label()
		Me.lblStatsLabels = New System.Windows.Forms.Label()
		Me.lblServerDetailsDescription = New System.Windows.Forms.Label()
		Me.cmdGetServerDetails = New System.Windows.Forms.Button()
		Me.cmdServerStats = New System.Windows.Forms.Button()
		CType(Me.picIcon, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.tabProperties.SuspendLayout()
		Me.TabPage1.SuspendLayout()
		Me.TabPage2.SuspendLayout()
		Me.TabPage3.SuspendLayout()
		Me.SuspendLayout()
		'
		'lnkHelp
		'
		Me.lnkHelp.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.lnkHelp.Location = New System.Drawing.Point(310, 9)
		Me.lnkHelp.Margin = New System.Windows.Forms.Padding(0)
		Me.lnkHelp.Name = "lnkHelp"
		Me.lnkHelp.Size = New System.Drawing.Size(75, 15)
		Me.lnkHelp.TabIndex = 54
		Me.lnkHelp.TabStop = True
		Me.lnkHelp.Text = "Help"
		Me.lnkHelp.TextAlign = System.Drawing.ContentAlignment.TopRight
		'
		'picIcon
		'
		Me.picIcon.Location = New System.Drawing.Point(12, 12)
		Me.picIcon.Name = "picIcon"
		Me.picIcon.Size = New System.Drawing.Size(48, 48)
		Me.picIcon.TabIndex = 52
		Me.picIcon.TabStop = False
		'
		'Label2
		'
		Me.Label2.AutoSize = True
		Me.Label2.Location = New System.Drawing.Point(66, 40)
		Me.Label2.Margin = New System.Windows.Forms.Padding(3)
		Me.Label2.Name = "Label2"
		Me.Label2.Size = New System.Drawing.Size(195, 13)
		Me.Label2.TabIndex = 51
		Me.Label2.Text = "Rename group, or change icon colour..."
		'
		'lblTitle
		'
		Me.lblTitle.AutoSize = True
		Me.lblTitle.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.lblTitle.Location = New System.Drawing.Point(66, 18)
		Me.lblTitle.Margin = New System.Windows.Forms.Padding(3, 9, 3, 3)
		Me.lblTitle.Name = "lblTitle"
		Me.lblTitle.Size = New System.Drawing.Size(110, 16)
		Me.lblTitle.TabIndex = 50
		Me.lblTitle.Text = "Group Properties"
		'
		'cmdCancel
		'
		Me.cmdCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
		Me.cmdCancel.Location = New System.Drawing.Point(226, 385)
		Me.cmdCancel.Margin = New System.Windows.Forms.Padding(3, 12, 3, 3)
		Me.cmdCancel.Name = "cmdCancel"
		Me.cmdCancel.Size = New System.Drawing.Size(75, 25)
		Me.cmdCancel.TabIndex = 48
		Me.cmdCancel.Text = "Cancel"
		Me.cmdCancel.UseVisualStyleBackColor = True
		'
		'cmdOK
		'
		Me.cmdOK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.cmdOK.Location = New System.Drawing.Point(307, 385)
		Me.cmdOK.Margin = New System.Windows.Forms.Padding(3, 12, 3, 3)
		Me.cmdOK.Name = "cmdOK"
		Me.cmdOK.Size = New System.Drawing.Size(75, 25)
		Me.cmdOK.TabIndex = 0
		Me.cmdOK.Text = "OK"
		Me.cmdOK.UseVisualStyleBackColor = True
		'
		'tabProperties
		'
		Me.tabProperties.Controls.Add(Me.TabPage1)
		Me.tabProperties.Controls.Add(Me.TabPage2)
		Me.tabProperties.Controls.Add(Me.TabPage3)
		Me.tabProperties.Location = New System.Drawing.Point(12, 72)
		Me.tabProperties.Name = "tabProperties"
		Me.tabProperties.Padding = New System.Drawing.Point(12, 6)
		Me.tabProperties.SelectedIndex = 0
		Me.tabProperties.Size = New System.Drawing.Size(370, 298)
		Me.tabProperties.TabIndex = 0
		'
		'TabPage1
		'
		Me.TabPage1.BackColor = System.Drawing.SystemColors.Control
		Me.TabPage1.Controls.Add(Me.lblDescriptionCount)
		Me.TabPage1.Controls.Add(Me.cmdEnableColours)
		Me.TabPage1.Controls.Add(Me.lblInfoGroupColours)
		Me.TabPage1.Controls.Add(Me.txtDescription)
		Me.TabPage1.Controls.Add(Me.Label4)
		Me.TabPage1.Controls.Add(Me.Label1)
		Me.TabPage1.Controls.Add(Me.txtGroupName)
		Me.TabPage1.Controls.Add(Me.Label3)
		Me.TabPage1.Controls.Add(Me.cmoGroupColour)
		Me.TabPage1.Location = New System.Drawing.Point(4, 28)
		Me.TabPage1.Name = "TabPage1"
		Me.TabPage1.Padding = New System.Windows.Forms.Padding(6, 0, 6, 6)
		Me.TabPage1.Size = New System.Drawing.Size(362, 266)
		Me.TabPage1.TabIndex = 0
		Me.TabPage1.Text = "Properties"
		'
		'lblDescriptionCount
		'
		Me.lblDescriptionCount.Enabled = False
		Me.lblDescriptionCount.Location = New System.Drawing.Point(100, 178)
		Me.lblDescriptionCount.Margin = New System.Windows.Forms.Padding(3, 0, 3, 3)
		Me.lblDescriptionCount.Name = "lblDescriptionCount"
		Me.lblDescriptionCount.Size = New System.Drawing.Size(253, 15)
		Me.lblDescriptionCount.TabIndex = 64
		Me.lblDescriptionCount.Text = "(250 characters remaining)"
		Me.lblDescriptionCount.TextAlign = System.Drawing.ContentAlignment.TopRight
		'
		'cmdEnableColours
		'
		Me.cmdEnableColours.Location = New System.Drawing.Point(228, 232)
		Me.cmdEnableColours.Name = "cmdEnableColours"
		Me.cmdEnableColours.Size = New System.Drawing.Size(125, 25)
		Me.cmdEnableColours.TabIndex = 3
		Me.cmdEnableColours.Text = "Enable Colours"
		Me.cmdEnableColours.UseVisualStyleBackColor = True
		'
		'lblInfoGroupColours
		'
		Me.lblInfoGroupColours.AutoSize = True
		Me.lblInfoGroupColours.Location = New System.Drawing.Point(9, 238)
		Me.lblInfoGroupColours.Margin = New System.Windows.Forms.Padding(3)
		Me.lblInfoGroupColours.Name = "lblInfoGroupColours"
		Me.lblInfoGroupColours.Size = New System.Drawing.Size(182, 13)
		Me.lblInfoGroupColours.TabIndex = 62
		Me.lblInfoGroupColours.Text = "Group colours are currently disabled :"
		Me.lblInfoGroupColours.TextAlign = System.Drawing.ContentAlignment.BottomLeft
		'
		'txtDescription
		'
		Me.txtDescription.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
				  Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.txtDescription.Location = New System.Drawing.Point(100, 75)
		Me.txtDescription.MaxLength = 250
		Me.txtDescription.Multiline = True
		Me.txtDescription.Name = "txtDescription"
		Me.txtDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
		Me.txtDescription.Size = New System.Drawing.Size(253, 100)
		Me.txtDescription.TabIndex = 2
		'
		'Label4
		'
		Me.Label4.AutoSize = True
		Me.Label4.Location = New System.Drawing.Point(9, 78)
		Me.Label4.Margin = New System.Windows.Forms.Padding(3, 12, 3, 3)
		Me.Label4.Name = "Label4"
		Me.Label4.Size = New System.Drawing.Size(66, 13)
		Me.Label4.TabIndex = 61
		Me.Label4.Text = "Description :"
		'
		'Label1
		'
		Me.Label1.AutoSize = True
		Me.Label1.Location = New System.Drawing.Point(9, 47)
		Me.Label1.Margin = New System.Windows.Forms.Padding(3, 12, 3, 3)
		Me.Label1.Name = "Label1"
		Me.Label1.Size = New System.Drawing.Size(43, 13)
		Me.Label1.TabIndex = 59
		Me.Label1.Text = "Colour :"
		'
		'txtGroupName
		'
		Me.txtGroupName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
				  Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.txtGroupName.Location = New System.Drawing.Point(100, 12)
		Me.txtGroupName.Margin = New System.Windows.Forms.Padding(3, 12, 3, 3)
		Me.txtGroupName.MaxLength = 32
		Me.txtGroupName.Name = "txtGroupName"
		Me.txtGroupName.Size = New System.Drawing.Size(253, 20)
		Me.txtGroupName.TabIndex = 0
		'
		'Label3
		'
		Me.Label3.AutoSize = True
		Me.Label3.Location = New System.Drawing.Point(9, 15)
		Me.Label3.Margin = New System.Windows.Forms.Padding(3, 12, 3, 3)
		Me.Label3.Name = "Label3"
		Me.Label3.Size = New System.Drawing.Size(73, 13)
		Me.Label3.TabIndex = 58
		Me.Label3.Text = "Group Name :"
		'
		'cmoGroupColour
		'
		Me.cmoGroupColour.BackColor = System.Drawing.SystemColors.Window
		Me.cmoGroupColour.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
		Me.cmoGroupColour.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.cmoGroupColour.FormattingEnabled = True
		Me.cmoGroupColour.ItemHeight = 19
		Me.cmoGroupColour.Location = New System.Drawing.Point(100, 41)
		Me.cmoGroupColour.Margin = New System.Windows.Forms.Padding(3, 6, 3, 6)
		Me.cmoGroupColour.Name = "cmoGroupColour"
		Me.cmoGroupColour.RemoveIconSpacing = False
		Me.cmoGroupColour.SelectedItem = Nothing
		Me.cmoGroupColour.Size = New System.Drawing.Size(175, 25)
		Me.cmoGroupColour.TabIndex = 1
		'
		'TabPage2
		'
		Me.TabPage2.BackColor = System.Drawing.SystemColors.Control
		Me.TabPage2.Controls.Add(Me.lstResourceStats)
		Me.TabPage2.Location = New System.Drawing.Point(4, 28)
		Me.TabPage2.Name = "TabPage2"
		Me.TabPage2.Padding = New System.Windows.Forms.Padding(6, 0, 6, 6)
		Me.TabPage2.Size = New System.Drawing.Size(362, 266)
		Me.TabPage2.TabIndex = 1
		Me.TabPage2.Text = "Resources"
		'
		'lstResourceStats
		'
		Me.lstResourceStats.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
				  Or System.Windows.Forms.AnchorStyles.Left) _
				  Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.lstResourceStats.Location = New System.Drawing.Point(9, 12)
		Me.lstResourceStats.Margin = New System.Windows.Forms.Padding(3, 12, 3, 3)
		Me.lstResourceStats.Name = "lstResourceStats"
		Me.lstResourceStats.OwnerDraw = True
		Me.lstResourceStats.Size = New System.Drawing.Size(344, 245)
		Me.lstResourceStats.TabIndex = 46
		Me.lstResourceStats.UseCompatibleStateImageBehavior = False
		'
		'TabPage3
		'
		Me.TabPage3.BackColor = System.Drawing.SystemColors.Control
		Me.TabPage3.Controls.Add(Me.lblStats)
		Me.TabPage3.Controls.Add(Me.lblStatsLabels)
		Me.TabPage3.Controls.Add(Me.lblServerDetailsDescription)
		Me.TabPage3.Controls.Add(Me.cmdGetServerDetails)
		Me.TabPage3.Location = New System.Drawing.Point(4, 28)
		Me.TabPage3.Name = "TabPage3"
		Me.TabPage3.Padding = New System.Windows.Forms.Padding(6, 0, 6, 6)
		Me.TabPage3.Size = New System.Drawing.Size(362, 266)
		Me.TabPage3.TabIndex = 2
		Me.TabPage3.Text = "Server Details"
		'
		'lblStats
		'
		Me.lblStats.Location = New System.Drawing.Point(155, 217)
		Me.lblStats.Margin = New System.Windows.Forms.Padding(0, 3, 3, 3)
		Me.lblStats.Name = "lblStats"
		Me.lblStats.Size = New System.Drawing.Size(30, 40)
		Me.lblStats.TabIndex = 53
		Me.lblStats.Text = "000" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "000"
		Me.lblStats.TextAlign = System.Drawing.ContentAlignment.BottomRight
		'
		'lblStatsLabels
		'
		Me.lblStatsLabels.Location = New System.Drawing.Point(9, 217)
		Me.lblStatsLabels.Margin = New System.Windows.Forms.Padding(3, 3, 0, 3)
		Me.lblStatsLabels.Name = "lblStatsLabels"
		Me.lblStatsLabels.Size = New System.Drawing.Size(150, 40)
		Me.lblStatsLabels.TabIndex = 52
		Me.lblStatsLabels.Text = "Servers (Total) :" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Servers With Properties :"
		Me.lblStatsLabels.TextAlign = System.Drawing.ContentAlignment.BottomRight
		'
		'lblServerDetailsDescription
		'
		Me.lblServerDetailsDescription.Location = New System.Drawing.Point(9, 15)
		Me.lblServerDetailsDescription.Margin = New System.Windows.Forms.Padding(3, 12, 3, 3)
		Me.lblServerDetailsDescription.Name = "lblServerDetailsDescription"
		Me.lblServerDetailsDescription.Size = New System.Drawing.Size(344, 196)
		Me.lblServerDetailsDescription.TabIndex = 0
		Me.lblServerDetailsDescription.Text = "[svc].buildServerDetailsDescription"
		'
		'cmdGetServerDetails
		'
		Me.cmdGetServerDetails.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.cmdGetServerDetails.Location = New System.Drawing.Point(228, 232)
		Me.cmdGetServerDetails.Margin = New System.Windows.Forms.Padding(24, 3, 3, 3)
		Me.cmdGetServerDetails.Name = "cmdGetServerDetails"
		Me.cmdGetServerDetails.Size = New System.Drawing.Size(125, 25)
		Me.cmdGetServerDetails.TabIndex = 2
		Me.cmdGetServerDetails.Text = "Get Server Details"
		Me.cmdGetServerDetails.UseVisualStyleBackColor = True
		'
		'cmdServerStats
		'
		Me.cmdServerStats.Location = New System.Drawing.Point(12, 385)
		Me.cmdServerStats.Name = "cmdServerStats"
		Me.cmdServerStats.Size = New System.Drawing.Size(125, 25)
		Me.cmdServerStats.TabIndex = 55
		Me.cmdServerStats.Text = "Server Stats"
		Me.cmdServerStats.UseVisualStyleBackColor = True
		'
		'frmPropertiesGroup
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.CancelButton = Me.cmdCancel
		Me.ClientSize = New System.Drawing.Size(394, 422)
		Me.Controls.Add(Me.cmdServerStats)
		Me.Controls.Add(Me.tabProperties)
		Me.Controls.Add(Me.lnkHelp)
		Me.Controls.Add(Me.picIcon)
		Me.Controls.Add(Me.Label2)
		Me.Controls.Add(Me.lblTitle)
		Me.Controls.Add(Me.cmdCancel)
		Me.Controls.Add(Me.cmdOK)
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
		Me.MaximizeBox = False
		Me.MinimizeBox = False
		Me.Name = "frmPropertiesGroup"
		Me.ShowIcon = False
		Me.ShowInTaskbar = False
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
		Me.Text = " Group Properties"
		CType(Me.picIcon, System.ComponentModel.ISupportInitialize).EndInit()
		Me.tabProperties.ResumeLayout(False)
		Me.TabPage1.ResumeLayout(False)
		Me.TabPage1.PerformLayout()
		Me.TabPage2.ResumeLayout(False)
		Me.TabPage3.ResumeLayout(False)
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub
	Friend WithEvents lnkHelp As System.Windows.Forms.LinkLabel
	Friend WithEvents picIcon As System.Windows.Forms.PictureBox
	Friend WithEvents Label2 As System.Windows.Forms.Label
	Friend WithEvents lblTitle As System.Windows.Forms.Label
	Friend WithEvents cmdCancel As System.Windows.Forms.Button
	Friend WithEvents cmdOK As System.Windows.Forms.Button
	Friend WithEvents tabProperties As System.Windows.Forms.TabControl
	Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
	Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
	Friend WithEvents Label1 As System.Windows.Forms.Label
	Friend WithEvents txtGroupName As System.Windows.Forms.TextBox
	Friend WithEvents Label3 As System.Windows.Forms.Label
	Friend WithEvents cmoGroupColour As ServerSystemChecker.ctrlComboBox_Icons
	Friend WithEvents txtDescription As System.Windows.Forms.TextBox
	Friend WithEvents Label4 As System.Windows.Forms.Label
	Friend WithEvents TabPage3 As System.Windows.Forms.TabPage
	Friend WithEvents cmdGetServerDetails As System.Windows.Forms.Button
	Friend WithEvents lblServerDetailsDescription As System.Windows.Forms.Label
	Friend WithEvents lblStatsLabels As System.Windows.Forms.Label
	Friend WithEvents lblInfoGroupColours As System.Windows.Forms.Label
	Friend WithEvents cmdEnableColours As System.Windows.Forms.Button
	Friend WithEvents lblStats As System.Windows.Forms.Label
	Friend WithEvents lblDescriptionCount As System.Windows.Forms.Label
	Friend WithEvents lstResourceStats As ServerSystemChecker.ctrlListView_SubIcons
	Friend WithEvents cmdServerStats As System.Windows.Forms.Button
End Class
