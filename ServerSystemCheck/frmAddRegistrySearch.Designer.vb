<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAddRegistrySearch
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
		Me.tvwRegistryTree = New System.Windows.Forms.TreeView()
		Me.cmdCancel = New System.Windows.Forms.Button()
		Me.cmdOK = New System.Windows.Forms.Button()
		Me.lblLabel = New System.Windows.Forms.Label()
		Me.lnkHelp = New System.Windows.Forms.LinkLabel()
		Me.picIcon = New System.Windows.Forms.PictureBox()
		Me.Label2 = New System.Windows.Forms.Label()
		Me.lblTitle = New System.Windows.Forms.Label()
		Me.lblToolTip = New System.Windows.Forms.Label()
		Me.chkAutoExpand = New System.Windows.Forms.CheckBox()
		Me.lnkNote = New System.Windows.Forms.LinkLabel()
		Me.cmdLocatePath = New System.Windows.Forms.Button()
		Me.cmoIconComboBox = New ServerSystemChecker.ctrlComboBox_Icons()
		Me.lblFullPath = New System.Windows.Forms.Label()
		CType(Me.picIcon, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.SuspendLayout()
		'
		'tvwRegistryTree
		'
		Me.tvwRegistryTree.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
				  Or System.Windows.Forms.AnchorStyles.Left) _
				  Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.tvwRegistryTree.Location = New System.Drawing.Point(12, 103)
		Me.tvwRegistryTree.Name = "tvwRegistryTree"
		Me.tvwRegistryTree.Size = New System.Drawing.Size(470, 201)
		Me.tvwRegistryTree.TabIndex = 0
		'
		'cmdCancel
		'
		Me.cmdCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
		Me.cmdCancel.Location = New System.Drawing.Point(326, 335)
		Me.cmdCancel.Name = "cmdCancel"
		Me.cmdCancel.Size = New System.Drawing.Size(75, 25)
		Me.cmdCancel.TabIndex = 8
		Me.cmdCancel.Text = "Cancel"
		Me.cmdCancel.UseVisualStyleBackColor = True
		'
		'cmdOK
		'
		Me.cmdOK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.cmdOK.Location = New System.Drawing.Point(407, 335)
		Me.cmdOK.Margin = New System.Windows.Forms.Padding(3, 12, 3, 3)
		Me.cmdOK.Name = "cmdOK"
		Me.cmdOK.Size = New System.Drawing.Size(75, 25)
		Me.cmdOK.TabIndex = 7
		Me.cmdOK.Text = "OK"
		Me.cmdOK.UseVisualStyleBackColor = True
		'
		'lblLabel
		'
		Me.lblLabel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.lblLabel.Location = New System.Drawing.Point(74, 72)
		Me.lblLabel.Name = "lblLabel"
		Me.lblLabel.Size = New System.Drawing.Size(152, 25)
		Me.lblLabel.TabIndex = 62
		Me.lblLabel.Text = "Load From :"
		Me.lblLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight
		'
		'lnkHelp
		'
		Me.lnkHelp.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.lnkHelp.Location = New System.Drawing.Point(410, 9)
		Me.lnkHelp.Margin = New System.Windows.Forms.Padding(0)
		Me.lnkHelp.Name = "lnkHelp"
		Me.lnkHelp.Size = New System.Drawing.Size(75, 15)
		Me.lnkHelp.TabIndex = 67
		Me.lnkHelp.TabStop = True
		Me.lnkHelp.Text = "Help"
		Me.lnkHelp.TextAlign = System.Drawing.ContentAlignment.TopRight
		'
		'picIcon
		'
		Me.picIcon.Location = New System.Drawing.Point(12, 12)
		Me.picIcon.Name = "picIcon"
		Me.picIcon.Size = New System.Drawing.Size(48, 48)
		Me.picIcon.TabIndex = 66
		Me.picIcon.TabStop = False
		'
		'Label2
		'
		Me.Label2.AutoSize = True
		Me.Label2.Location = New System.Drawing.Point(66, 40)
		Me.Label2.Margin = New System.Windows.Forms.Padding(3)
		Me.Label2.Name = "Label2"
		Me.Label2.Size = New System.Drawing.Size(264, 13)
		Me.Label2.TabIndex = 65
		Me.Label2.Text = "Locate a specific key within the local or remote registry"
		'
		'lblTitle
		'
		Me.lblTitle.AutoSize = True
		Me.lblTitle.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.lblTitle.Location = New System.Drawing.Point(66, 18)
		Me.lblTitle.Margin = New System.Windows.Forms.Padding(3, 9, 3, 3)
		Me.lblTitle.Name = "lblTitle"
		Me.lblTitle.Size = New System.Drawing.Size(133, 16)
		Me.lblTitle.TabIndex = 64
		Me.lblTitle.Text = "Browse The Registry"
		'
		'lblToolTip
		'
		Me.lblToolTip.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
				  Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.lblToolTip.BackColor = System.Drawing.SystemColors.Info
		Me.lblToolTip.Location = New System.Drawing.Point(13, 280)
		Me.lblToolTip.Margin = New System.Windows.Forms.Padding(3)
		Me.lblToolTip.Name = "lblToolTip"
		Me.lblToolTip.Padding = New System.Windows.Forms.Padding(3)
		Me.lblToolTip.Size = New System.Drawing.Size(468, 23)
		Me.lblToolTip.TabIndex = 70
		Me.lblToolTip.Text = "Access denied - Admin options may need to be enabled - See help for more details"
		Me.lblToolTip.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
		'
		'chkAutoExpand
		'
		Me.chkAutoExpand.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.chkAutoExpand.AutoSize = True
		Me.chkAutoExpand.Location = New System.Drawing.Point(12, 343)
		Me.chkAutoExpand.Name = "chkAutoExpand"
		Me.chkAutoExpand.Size = New System.Drawing.Size(164, 17)
		Me.chkAutoExpand.TabIndex = 71
		Me.chkAutoExpand.Text = "Auto-Expand Selected Folder"
		Me.chkAutoExpand.UseVisualStyleBackColor = True
		'
		'lnkNote
		'
		Me.lnkNote.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.lnkNote.LinkColor = System.Drawing.Color.Blue
		Me.lnkNote.Location = New System.Drawing.Point(410, 30)
		Me.lnkNote.Margin = New System.Windows.Forms.Padding(0, 6, 0, 0)
		Me.lnkNote.Name = "lnkNote"
		Me.lnkNote.Size = New System.Drawing.Size(75, 15)
		Me.lnkNote.TabIndex = 72
		Me.lnkNote.TabStop = True
		Me.lnkNote.Text = "Admin Note"
		Me.lnkNote.TextAlign = System.Drawing.ContentAlignment.TopRight
		'
		'cmdLocatePath
		'
		Me.cmdLocatePath.Location = New System.Drawing.Point(12, 72)
		Me.cmdLocatePath.Name = "cmdLocatePath"
		Me.cmdLocatePath.Size = New System.Drawing.Size(25, 25)
		Me.cmdLocatePath.TabIndex = 73
		Me.cmdLocatePath.UseVisualStyleBackColor = True
		'
		'cmoIconComboBox
		'
		Me.cmoIconComboBox.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.cmoIconComboBox.BackColor = System.Drawing.SystemColors.Window
		Me.cmoIconComboBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
		Me.cmoIconComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.cmoIconComboBox.FormattingEnabled = True
		Me.cmoIconComboBox.ItemHeight = 19
		Me.cmoIconComboBox.Location = New System.Drawing.Point(232, 72)
		Me.cmoIconComboBox.Name = "cmoIconComboBox"
		Me.cmoIconComboBox.RemoveIconSpacing = False
		Me.cmoIconComboBox.SelectedItem = Nothing
		Me.cmoIconComboBox.Size = New System.Drawing.Size(250, 25)
		Me.cmoIconComboBox.TabIndex = 61
		'
		'lblFullPath
		'
		Me.lblFullPath.Location = New System.Drawing.Point(12, 307)
		Me.lblFullPath.Margin = New System.Windows.Forms.Padding(3, 1, 3, 0)
		Me.lblFullPath.Name = "lblFullPath"
		Me.lblFullPath.Size = New System.Drawing.Size(470, 16)
		Me.lblFullPath.TabIndex = 75
		Me.lblFullPath.Text = "lblFullPath.set-via-code"
		'
		'frmAddRegistrySearch
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.CancelButton = Me.cmdCancel
		Me.ClientSize = New System.Drawing.Size(494, 372)
		Me.Controls.Add(Me.lblFullPath)
		Me.Controls.Add(Me.cmdLocatePath)
		Me.Controls.Add(Me.lnkNote)
		Me.Controls.Add(Me.chkAutoExpand)
		Me.Controls.Add(Me.lblToolTip)
		Me.Controls.Add(Me.lnkHelp)
		Me.Controls.Add(Me.picIcon)
		Me.Controls.Add(Me.Label2)
		Me.Controls.Add(Me.lblTitle)
		Me.Controls.Add(Me.lblLabel)
		Me.Controls.Add(Me.cmoIconComboBox)
		Me.Controls.Add(Me.cmdCancel)
		Me.Controls.Add(Me.cmdOK)
		Me.Controls.Add(Me.tvwRegistryTree)
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
		Me.MaximizeBox = False
		Me.MinimizeBox = False
		Me.Name = "frmAddRegistrySearch"
		Me.ShowIcon = False
		Me.ShowInTaskbar = False
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
		Me.Text = "frmAddRegistrySearch"
		CType(Me.picIcon, System.ComponentModel.ISupportInitialize).EndInit()
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub
	Friend WithEvents tvwRegistryTree As System.Windows.Forms.TreeView
	Friend WithEvents cmdCancel As System.Windows.Forms.Button
	Friend WithEvents cmdOK As System.Windows.Forms.Button
	Friend WithEvents lblLabel As System.Windows.Forms.Label
	Friend WithEvents cmoIconComboBox As ServerSystemChecker.ctrlComboBox_Icons
	Friend WithEvents lnkHelp As System.Windows.Forms.LinkLabel
	Friend WithEvents picIcon As System.Windows.Forms.PictureBox
	Friend WithEvents Label2 As System.Windows.Forms.Label
	Friend WithEvents lblTitle As System.Windows.Forms.Label
	Friend WithEvents lblToolTip As System.Windows.Forms.Label
	Friend WithEvents chkAutoExpand As System.Windows.Forms.CheckBox
	Friend WithEvents lnkNote As System.Windows.Forms.LinkLabel
	Friend WithEvents cmdLocatePath As System.Windows.Forms.Button
	Friend WithEvents lblFullPath As System.Windows.Forms.Label
End Class
