<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmOptions
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
		Me.cmdOK = New System.Windows.Forms.Button()
		Me.cmdCancel = New System.Windows.Forms.Button()
		Me.cmdResetOptions = New System.Windows.Forms.Button()
		Me.chkPingServers = New System.Windows.Forms.CheckBox()
		Me.lblTitle2 = New System.Windows.Forms.Label()
		Me.lblTitle3 = New System.Windows.Forms.Label()
		Me.Label1 = New System.Windows.Forms.Label()
		Me.chkShowFullpathHeader = New System.Windows.Forms.CheckBox()
		Me.chkShowGroupCounts = New System.Windows.Forms.CheckBox()
		Me.Label4 = New System.Windows.Forms.Label()
		Me.lnkHelp = New System.Windows.Forms.LinkLabel()
		Me.chkUseColouredGroups = New System.Windows.Forms.CheckBox()
		Me.lblTitle1 = New System.Windows.Forms.Label()
		Me.Label2 = New System.Windows.Forms.Label()
		Me.floIconSet = New System.Windows.Forms.FlowLayoutPanel()
		Me.Label3 = New System.Windows.Forms.Label()
		Me.PictureBox1 = New System.Windows.Forms.PictureBox()
		Me.cmoServerNames = New ServerSystemChecker.ctrlComboBox_Icons()
		Me.cmoGroupIcons = New ServerSystemChecker.ctrlComboBox_Icons()
		Me.cmoView = New ServerSystemChecker.ctrlComboBox_Icons()
		Me.cmoGroup = New ServerSystemChecker.ctrlComboBox_Icons()
		CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.SuspendLayout()
		'
		'cmdOK
		'
		Me.cmdOK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.cmdOK.Location = New System.Drawing.Point(457, 260)
		Me.cmdOK.Margin = New System.Windows.Forms.Padding(3, 12, 3, 3)
		Me.cmdOK.Name = "cmdOK"
		Me.cmdOK.Size = New System.Drawing.Size(75, 25)
		Me.cmdOK.TabIndex = 9
		Me.cmdOK.Text = "OK"
		Me.cmdOK.UseVisualStyleBackColor = True
		'
		'cmdCancel
		'
		Me.cmdCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
		Me.cmdCancel.Location = New System.Drawing.Point(376, 260)
		Me.cmdCancel.Margin = New System.Windows.Forms.Padding(12, 12, 3, 3)
		Me.cmdCancel.Name = "cmdCancel"
		Me.cmdCancel.Size = New System.Drawing.Size(75, 25)
		Me.cmdCancel.TabIndex = 10
		Me.cmdCancel.Text = "Cancel"
		Me.cmdCancel.UseVisualStyleBackColor = True
		'
		'cmdResetOptions
		'
		Me.cmdResetOptions.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.cmdResetOptions.Location = New System.Drawing.Point(12, 260)
		Me.cmdResetOptions.Margin = New System.Windows.Forms.Padding(3, 12, 12, 3)
		Me.cmdResetOptions.Name = "cmdResetOptions"
		Me.cmdResetOptions.Size = New System.Drawing.Size(125, 25)
		Me.cmdResetOptions.TabIndex = 11
		Me.cmdResetOptions.Text = "Reset Options"
		Me.cmdResetOptions.UseVisualStyleBackColor = True
		'
		'chkPingServers
		'
		Me.chkPingServers.AutoSize = True
		Me.chkPingServers.Checked = True
		Me.chkPingServers.CheckState = System.Windows.Forms.CheckState.Checked
		Me.chkPingServers.Location = New System.Drawing.Point(36, 214)
		Me.chkPingServers.Name = "chkPingServers"
		Me.chkPingServers.Size = New System.Drawing.Size(168, 17)
		Me.chkPingServers.TabIndex = 8
		Me.chkPingServers.Text = "Ping Servers Before Scanning"
		Me.chkPingServers.UseVisualStyleBackColor = False
		'
		'lblTitle2
		'
		Me.lblTitle2.AutoSize = True
		Me.lblTitle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.25!)
		Me.lblTitle2.Location = New System.Drawing.Point(281, 12)
		Me.lblTitle2.Margin = New System.Windows.Forms.Padding(3, 3, 3, 9)
		Me.lblTitle2.Name = "lblTitle2"
		Me.lblTitle2.Size = New System.Drawing.Size(90, 16)
		Me.lblTitle2.TabIndex = 86
		Me.lblTitle2.Text = "Resource List"
		'
		'lblTitle3
		'
		Me.lblTitle3.AutoSize = True
		Me.lblTitle3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.25!)
		Me.lblTitle3.Location = New System.Drawing.Point(12, 186)
		Me.lblTitle3.Margin = New System.Windows.Forms.Padding(3, 24, 3, 9)
		Me.lblTitle3.Name = "lblTitle3"
		Me.lblTitle3.Size = New System.Drawing.Size(88, 16)
		Me.lblTitle3.TabIndex = 91
		Me.lblTitle3.Text = "Scan Options"
		'
		'Label1
		'
		Me.Label1.AutoSize = True
		Me.Label1.Location = New System.Drawing.Point(305, 100)
		Me.Label1.Margin = New System.Windows.Forms.Padding(3, 18, 3, 18)
		Me.Label1.Name = "Label1"
		Me.Label1.Size = New System.Drawing.Size(62, 13)
		Me.Label1.TabIndex = 84
		Me.Label1.Text = "View Style :"
		'
		'chkShowFullpathHeader
		'
		Me.chkShowFullpathHeader.AutoSize = True
		Me.chkShowFullpathHeader.Checked = True
		Me.chkShowFullpathHeader.CheckState = System.Windows.Forms.CheckState.Checked
		Me.chkShowFullpathHeader.Location = New System.Drawing.Point(308, 63)
		Me.chkShowFullpathHeader.Name = "chkShowFullpathHeader"
		Me.chkShowFullpathHeader.Size = New System.Drawing.Size(154, 17)
		Me.chkShowFullpathHeader.TabIndex = 5
		Me.chkShowFullpathHeader.Text = "Show Full Path Header Bar"
		Me.chkShowFullpathHeader.UseVisualStyleBackColor = False
		'
		'chkShowGroupCounts
		'
		Me.chkShowGroupCounts.AutoSize = True
		Me.chkShowGroupCounts.Checked = True
		Me.chkShowGroupCounts.CheckState = System.Windows.Forms.CheckState.Checked
		Me.chkShowGroupCounts.Location = New System.Drawing.Point(308, 40)
		Me.chkShowGroupCounts.Name = "chkShowGroupCounts"
		Me.chkShowGroupCounts.Size = New System.Drawing.Size(121, 17)
		Me.chkShowGroupCounts.TabIndex = 4
		Me.chkShowGroupCounts.Text = "Show Group Counts"
		Me.chkShowGroupCounts.UseVisualStyleBackColor = False
		'
		'Label4
		'
		Me.Label4.AutoSize = True
		Me.Label4.Location = New System.Drawing.Point(305, 131)
		Me.Label4.Margin = New System.Windows.Forms.Padding(3, 3, 3, 18)
		Me.Label4.Name = "Label4"
		Me.Label4.Size = New System.Drawing.Size(57, 13)
		Me.Label4.TabIndex = 87
		Me.Label4.Text = "Group By :"
		'
		'lnkHelp
		'
		Me.lnkHelp.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.lnkHelp.Location = New System.Drawing.Point(460, 9)
		Me.lnkHelp.Margin = New System.Windows.Forms.Padding(0)
		Me.lnkHelp.Name = "lnkHelp"
		Me.lnkHelp.Size = New System.Drawing.Size(75, 15)
		Me.lnkHelp.TabIndex = 12
		Me.lnkHelp.TabStop = True
		Me.lnkHelp.Text = "Help"
		Me.lnkHelp.TextAlign = System.Drawing.ContentAlignment.TopRight
		'
		'chkUseColouredGroups
		'
		Me.chkUseColouredGroups.AutoSize = True
		Me.chkUseColouredGroups.Checked = True
		Me.chkUseColouredGroups.CheckState = System.Windows.Forms.CheckState.Checked
		Me.chkUseColouredGroups.Location = New System.Drawing.Point(36, 40)
		Me.chkUseColouredGroups.Name = "chkUseColouredGroups"
		Me.chkUseColouredGroups.Size = New System.Drawing.Size(127, 17)
		Me.chkUseColouredGroups.TabIndex = 0
		Me.chkUseColouredGroups.Text = "Use Coloured Groups"
		Me.chkUseColouredGroups.UseVisualStyleBackColor = False
		'
		'lblTitle1
		'
		Me.lblTitle1.AutoSize = True
		Me.lblTitle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.25!)
		Me.lblTitle1.Location = New System.Drawing.Point(12, 12)
		Me.lblTitle1.Margin = New System.Windows.Forms.Padding(3, 3, 3, 9)
		Me.lblTitle1.Name = "lblTitle1"
		Me.lblTitle1.Size = New System.Drawing.Size(127, 16)
		Me.lblTitle1.TabIndex = 80
		Me.lblTitle1.Text = "Group / Server Tree"
		'
		'Label2
		'
		Me.Label2.AutoSize = True
		Me.Label2.Location = New System.Drawing.Point(33, 70)
		Me.Label2.Margin = New System.Windows.Forms.Padding(3, 18, 3, 18)
		Me.Label2.Name = "Label2"
		Me.Label2.Size = New System.Drawing.Size(71, 13)
		Me.Label2.TabIndex = 93
		Me.Label2.Text = "Group Icons :"
		'
		'floIconSet
		'
		Me.floIconSet.Location = New System.Drawing.Point(70, 93)
		Me.floIconSet.Margin = New System.Windows.Forms.Padding(3, 0, 3, 3)
		Me.floIconSet.Name = "floIconSet"
		Me.floIconSet.Size = New System.Drawing.Size(180, 17)
		Me.floIconSet.TabIndex = 95
		'
		'Label3
		'
		Me.Label3.AutoSize = True
		Me.Label3.Location = New System.Drawing.Point(33, 131)
		Me.Label3.Margin = New System.Windows.Forms.Padding(3, 18, 3, 18)
		Me.Label3.Name = "Label3"
		Me.Label3.Size = New System.Drawing.Size(80, 13)
		Me.Label3.TabIndex = 97
		Me.Label3.Text = "Server Names :"
		'
		'PictureBox1
		'
		Me.PictureBox1.BackColor = System.Drawing.SystemColors.ControlDark
		Me.PictureBox1.Location = New System.Drawing.Point(265, 12)
		Me.PictureBox1.Margin = New System.Windows.Forms.Padding(12, 3, 12, 3)
		Me.PictureBox1.Name = "PictureBox1"
		Me.PictureBox1.Size = New System.Drawing.Size(1, 233)
		Me.PictureBox1.TabIndex = 98
		Me.PictureBox1.TabStop = False
		'
		'cmoServerNames
		'
		Me.cmoServerNames.BackColor = System.Drawing.SystemColors.Window
		Me.cmoServerNames.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
		Me.cmoServerNames.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.cmoServerNames.FormattingEnabled = True
		Me.cmoServerNames.ItemHeight = 19
		Me.cmoServerNames.Location = New System.Drawing.Point(125, 126)
		Me.cmoServerNames.Name = "cmoServerNames"
		Me.cmoServerNames.RemoveIconSpacing = False
		Me.cmoServerNames.SelectedItem = Nothing
		Me.cmoServerNames.Size = New System.Drawing.Size(125, 25)
		Me.cmoServerNames.TabIndex = 2
		'
		'cmoGroupIcons
		'
		Me.cmoGroupIcons.BackColor = System.Drawing.SystemColors.Window
		Me.cmoGroupIcons.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
		Me.cmoGroupIcons.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.cmoGroupIcons.FormattingEnabled = True
		Me.cmoGroupIcons.ItemHeight = 19
		Me.cmoGroupIcons.Location = New System.Drawing.Point(125, 65)
		Me.cmoGroupIcons.Name = "cmoGroupIcons"
		Me.cmoGroupIcons.RemoveIconSpacing = False
		Me.cmoGroupIcons.SelectedItem = Nothing
		Me.cmoGroupIcons.Size = New System.Drawing.Size(125, 25)
		Me.cmoGroupIcons.TabIndex = 1
		'
		'cmoView
		'
		Me.cmoView.BackColor = System.Drawing.SystemColors.Window
		Me.cmoView.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
		Me.cmoView.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.cmoView.FormattingEnabled = True
		Me.cmoView.ItemHeight = 19
		Me.cmoView.Location = New System.Drawing.Point(397, 95)
		Me.cmoView.Name = "cmoView"
		Me.cmoView.RemoveIconSpacing = False
		Me.cmoView.SelectedItem = Nothing
		Me.cmoView.Size = New System.Drawing.Size(125, 25)
		Me.cmoView.TabIndex = 6
		'
		'cmoGroup
		'
		Me.cmoGroup.BackColor = System.Drawing.SystemColors.Window
		Me.cmoGroup.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
		Me.cmoGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.cmoGroup.FormattingEnabled = True
		Me.cmoGroup.ItemHeight = 19
		Me.cmoGroup.Location = New System.Drawing.Point(397, 126)
		Me.cmoGroup.Name = "cmoGroup"
		Me.cmoGroup.RemoveIconSpacing = False
		Me.cmoGroup.SelectedItem = Nothing
		Me.cmoGroup.Size = New System.Drawing.Size(125, 25)
		Me.cmoGroup.TabIndex = 7
		'
		'frmOptions
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.CancelButton = Me.cmdCancel
		Me.ClientSize = New System.Drawing.Size(544, 297)
		Me.Controls.Add(Me.PictureBox1)
		Me.Controls.Add(Me.Label3)
		Me.Controls.Add(Me.cmoServerNames)
		Me.Controls.Add(Me.floIconSet)
		Me.Controls.Add(Me.Label2)
		Me.Controls.Add(Me.cmoGroupIcons)
		Me.Controls.Add(Me.lnkHelp)
		Me.Controls.Add(Me.chkPingServers)
		Me.Controls.Add(Me.chkUseColouredGroups)
		Me.Controls.Add(Me.lblTitle2)
		Me.Controls.Add(Me.lblTitle1)
		Me.Controls.Add(Me.lblTitle3)
		Me.Controls.Add(Me.cmdResetOptions)
		Me.Controls.Add(Me.Label1)
		Me.Controls.Add(Me.cmdCancel)
		Me.Controls.Add(Me.chkShowFullpathHeader)
		Me.Controls.Add(Me.cmdOK)
		Me.Controls.Add(Me.cmoView)
		Me.Controls.Add(Me.chkShowGroupCounts)
		Me.Controls.Add(Me.cmoGroup)
		Me.Controls.Add(Me.Label4)
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
		Me.MaximizeBox = False
		Me.MinimizeBox = False
		Me.Name = "frmOptions"
		Me.ShowIcon = False
		Me.ShowInTaskbar = False
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
		Me.Text = " Configuration Options"
		CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub
	Friend WithEvents cmdOK As System.Windows.Forms.Button
	Friend WithEvents cmdCancel As System.Windows.Forms.Button
	Friend WithEvents cmdResetOptions As System.Windows.Forms.Button
	Friend WithEvents chkPingServers As System.Windows.Forms.CheckBox
	Friend WithEvents lblTitle3 As System.Windows.Forms.Label
	Friend WithEvents chkShowFullpathHeader As System.Windows.Forms.CheckBox
	Friend WithEvents chkShowGroupCounts As System.Windows.Forms.CheckBox
	Friend WithEvents cmoGroup As ServerSystemChecker.ctrlComboBox_Icons
	Friend WithEvents Label4 As System.Windows.Forms.Label
	Friend WithEvents lblTitle2 As System.Windows.Forms.Label
	Friend WithEvents cmoView As ServerSystemChecker.ctrlComboBox_Icons
	Friend WithEvents lnkHelp As System.Windows.Forms.LinkLabel
	Friend WithEvents Label1 As System.Windows.Forms.Label
	Friend WithEvents chkUseColouredGroups As System.Windows.Forms.CheckBox
	Friend WithEvents lblTitle1 As System.Windows.Forms.Label
	Friend WithEvents cmoGroupIcons As ServerSystemChecker.ctrlComboBox_Icons
	Friend WithEvents Label2 As System.Windows.Forms.Label
	Friend WithEvents floIconSet As System.Windows.Forms.FlowLayoutPanel
	Friend WithEvents cmoServerNames As ServerSystemChecker.ctrlComboBox_Icons
	Friend WithEvents Label3 As System.Windows.Forms.Label
	Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
End Class
