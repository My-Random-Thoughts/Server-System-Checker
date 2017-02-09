<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAddHotFix
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
		Me.lstHotfixList = New System.Windows.Forms.ListView()
		Me.cmdAdd = New System.Windows.Forms.Button()
		Me.cmdDel = New System.Windows.Forms.Button()
		Me.lblLabel = New System.Windows.Forms.Label()
		Me.lnkNote = New System.Windows.Forms.LinkLabel()
		Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
		Me.cmdCloseImportList = New System.Windows.Forms.Button()
		Me.proProgress = New System.Windows.Forms.ProgressBar()
		Me.lstImportList = New ServerSystemChecker.ctrlListView_CollapseGroups()
		Me.cmoIconComboBox = New ServerSystemChecker.ctrlComboBox_Icons()
		CType(Me.picIcon, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.SplitContainer1.Panel1.SuspendLayout()
		Me.SplitContainer1.Panel2.SuspendLayout()
		Me.SplitContainer1.SuspendLayout()
		Me.SuspendLayout()
		'
		'lnkHelp
		'
		Me.lnkHelp.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.lnkHelp.Location = New System.Drawing.Point(360, 9)
		Me.lnkHelp.Margin = New System.Windows.Forms.Padding(0)
		Me.lnkHelp.Name = "lnkHelp"
		Me.lnkHelp.Size = New System.Drawing.Size(75, 15)
		Me.lnkHelp.TabIndex = 51
		Me.lnkHelp.TabStop = True
		Me.lnkHelp.Text = "Help"
		Me.lnkHelp.TextAlign = System.Drawing.ContentAlignment.TopRight
		'
		'picIcon
		'
		Me.picIcon.Location = New System.Drawing.Point(12, 12)
		Me.picIcon.Name = "picIcon"
		Me.picIcon.Size = New System.Drawing.Size(48, 48)
		Me.picIcon.TabIndex = 49
		Me.picIcon.TabStop = False
		'
		'cmdCancel
		'
		Me.cmdCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
		Me.cmdCancel.Location = New System.Drawing.Point(276, 335)
		Me.cmdCancel.Name = "cmdCancel"
		Me.cmdCancel.Size = New System.Drawing.Size(75, 25)
		Me.cmdCancel.TabIndex = 48
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
		Me.cmdOK.TabIndex = 47
		Me.cmdOK.Text = "OK"
		Me.cmdOK.UseVisualStyleBackColor = True
		'
		'Label2
		'
		Me.Label2.AutoSize = True
		Me.Label2.Location = New System.Drawing.Point(66, 40)
		Me.Label2.Margin = New System.Windows.Forms.Padding(3)
		Me.Label2.Name = "Label2"
		Me.Label2.Size = New System.Drawing.Size(186, 13)
		Me.Label2.TabIndex = 45
		Me.Label2.Text = "Add a list of hotfixes and/or patches..."
		'
		'lblTitle
		'
		Me.lblTitle.AutoSize = True
		Me.lblTitle.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.lblTitle.Location = New System.Drawing.Point(66, 18)
		Me.lblTitle.Margin = New System.Windows.Forms.Padding(3, 9, 3, 3)
		Me.lblTitle.Name = "lblTitle"
		Me.lblTitle.Size = New System.Drawing.Size(143, 16)
		Me.lblTitle.TabIndex = 44
		Me.lblTitle.Text = "Add New Hotfix / Patch"
		'
		'lstHotfixList
		'
		Me.lstHotfixList.Dock = System.Windows.Forms.DockStyle.Fill
		Me.lstHotfixList.Location = New System.Drawing.Point(0, 0)
		Me.lstHotfixList.Margin = New System.Windows.Forms.Padding(3, 9, 3, 3)
		Me.lstHotfixList.Name = "lstHotfixList"
		Me.lstHotfixList.Size = New System.Drawing.Size(200, 217)
		Me.lstHotfixList.TabIndex = 54
		Me.lstHotfixList.UseCompatibleStateImageBehavior = False
		'
		'cmdAdd
		'
		Me.cmdAdd.ImageKey = "(none)"
		Me.cmdAdd.Location = New System.Drawing.Point(12, 72)
		Me.cmdAdd.Name = "cmdAdd"
		Me.cmdAdd.Size = New System.Drawing.Size(25, 25)
		Me.cmdAdd.TabIndex = 57
		Me.cmdAdd.UseVisualStyleBackColor = True
		'
		'cmdDel
		'
		Me.cmdDel.ImageKey = "(none)"
		Me.cmdDel.Location = New System.Drawing.Point(43, 72)
		Me.cmdDel.Name = "cmdDel"
		Me.cmdDel.Size = New System.Drawing.Size(25, 25)
		Me.cmdDel.TabIndex = 58
		Me.cmdDel.UseVisualStyleBackColor = True
		'
		'lblLabel
		'
		Me.lblLabel.Location = New System.Drawing.Point(74, 72)
		Me.lblLabel.Name = "lblLabel"
		Me.lblLabel.Size = New System.Drawing.Size(102, 25)
		Me.lblLabel.TabIndex = 60
		Me.lblLabel.Text = "Import From :"
		Me.lblLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight
		'
		'lnkNote
		'
		Me.lnkNote.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.lnkNote.LinkColor = System.Drawing.Color.Blue
		Me.lnkNote.Location = New System.Drawing.Point(360, 30)
		Me.lnkNote.Margin = New System.Windows.Forms.Padding(0, 6, 0, 0)
		Me.lnkNote.Name = "lnkNote"
		Me.lnkNote.Size = New System.Drawing.Size(75, 15)
		Me.lnkNote.TabIndex = 62
		Me.lnkNote.TabStop = True
		Me.lnkNote.Text = "Admin Note"
		Me.lnkNote.TextAlign = System.Drawing.ContentAlignment.TopRight
		'
		'SplitContainer1
		'
		Me.SplitContainer1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
				  Or System.Windows.Forms.AnchorStyles.Left) _
				  Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.SplitContainer1.Location = New System.Drawing.Point(12, 103)
		Me.SplitContainer1.Name = "SplitContainer1"
		'
		'SplitContainer1.Panel1
		'
		Me.SplitContainer1.Panel1.Controls.Add(Me.lstHotfixList)
		'
		'SplitContainer1.Panel2
		'
		Me.SplitContainer1.Panel2.Controls.Add(Me.lstImportList)
		Me.SplitContainer1.Size = New System.Drawing.Size(420, 217)
		Me.SplitContainer1.SplitterDistance = 200
		Me.SplitContainer1.TabIndex = 64
		'
		'cmdCloseImportList
		'
		Me.cmdCloseImportList.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.cmdCloseImportList.Location = New System.Drawing.Point(12, 335)
		Me.cmdCloseImportList.Margin = New System.Windows.Forms.Padding(3, 3, 12, 3)
		Me.cmdCloseImportList.Name = "cmdCloseImportList"
		Me.cmdCloseImportList.Size = New System.Drawing.Size(125, 25)
		Me.cmdCloseImportList.TabIndex = 80
		Me.cmdCloseImportList.Text = "Close Import List"
		Me.cmdCloseImportList.UseVisualStyleBackColor = True
		'
		'proProgress
		'
		Me.proProgress.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
				  Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.proProgress.Location = New System.Drawing.Point(12, 347)
		Me.proProgress.Margin = New System.Windows.Forms.Padding(3, 3, 9, 3)
		Me.proProgress.Name = "proProgress"
		Me.proProgress.Size = New System.Drawing.Size(252, 13)
		Me.proProgress.TabIndex = 81
		'
		'lstImportList
		'
		Me.lstImportList.Dock = System.Windows.Forms.DockStyle.Fill
		Me.lstImportList.Location = New System.Drawing.Point(0, 0)
		Me.lstImportList.Name = "lstImportList"
		Me.lstImportList.Size = New System.Drawing.Size(216, 217)
		Me.lstImportList.TabIndex = 56
		Me.lstImportList.UseCompatibleStateImageBehavior = False
		'
		'cmoIconComboBox
		'
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
		Me.cmoIconComboBox.TabIndex = 59
		'
		'frmAddHotFix
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.CancelButton = Me.cmdCancel
		Me.ClientSize = New System.Drawing.Size(444, 372)
		Me.Controls.Add(Me.proProgress)
		Me.Controls.Add(Me.cmdCloseImportList)
		Me.Controls.Add(Me.SplitContainer1)
		Me.Controls.Add(Me.lnkNote)
		Me.Controls.Add(Me.lblLabel)
		Me.Controls.Add(Me.cmoIconComboBox)
		Me.Controls.Add(Me.cmdDel)
		Me.Controls.Add(Me.cmdAdd)
		Me.Controls.Add(Me.lnkHelp)
		Me.Controls.Add(Me.picIcon)
		Me.Controls.Add(Me.cmdCancel)
		Me.Controls.Add(Me.cmdOK)
		Me.Controls.Add(Me.Label2)
		Me.Controls.Add(Me.lblTitle)
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
		Me.MaximizeBox = False
		Me.MinimizeBox = False
		Me.Name = "frmAddHotFix"
		Me.ShowIcon = False
		Me.ShowInTaskbar = False
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
		Me.Text = " Add New Hotfix / Patch"
		CType(Me.picIcon, System.ComponentModel.ISupportInitialize).EndInit()
		Me.SplitContainer1.Panel1.ResumeLayout(False)
		Me.SplitContainer1.Panel2.ResumeLayout(False)
		Me.SplitContainer1.ResumeLayout(False)
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub
	Friend WithEvents lnkHelp As System.Windows.Forms.LinkLabel
	Friend WithEvents picIcon As System.Windows.Forms.PictureBox
	Friend WithEvents cmdCancel As System.Windows.Forms.Button
	Friend WithEvents cmdOK As System.Windows.Forms.Button
	Friend WithEvents Label2 As System.Windows.Forms.Label
	Friend WithEvents lblTitle As System.Windows.Forms.Label
	Friend WithEvents lstHotfixList As System.Windows.Forms.ListView
	Friend WithEvents cmdAdd As System.Windows.Forms.Button
	Friend WithEvents cmdDel As System.Windows.Forms.Button
	Friend WithEvents cmoIconComboBox As ServerSystemChecker.ctrlComboBox_Icons
	Friend WithEvents lblLabel As System.Windows.Forms.Label
	Friend WithEvents lnkNote As System.Windows.Forms.LinkLabel
	Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
	Friend WithEvents cmdCloseImportList As System.Windows.Forms.Button
	Friend WithEvents lstImportList As ServerSystemChecker.ctrlListView_CollapseGroups
	Friend WithEvents proProgress As System.Windows.Forms.ProgressBar
End Class
