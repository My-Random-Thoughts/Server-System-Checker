<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAddFreeSpace
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
		Me.picIcon = New System.Windows.Forms.PictureBox()
		Me.cmdCancel = New System.Windows.Forms.Button()
		Me.cmdOK = New System.Windows.Forms.Button()
		Me.Label2 = New System.Windows.Forms.Label()
		Me.lblTitle = New System.Windows.Forms.Label()
		Me.Label3 = New System.Windows.Forms.Label()
		Me.Label5 = New System.Windows.Forms.Label()
		Me.trackCritical = New System.Windows.Forms.TrackBar()
		Me.trackWarning = New System.Windows.Forms.TrackBar()
		Me.lblValue_Critical = New System.Windows.Forms.Label()
		Me.lblValue_Warning = New System.Windows.Forms.Label()
		Me.cmdReset = New System.Windows.Forms.Button()
		Me.lnkHelp = New System.Windows.Forms.LinkLabel()
		Me.Label10 = New System.Windows.Forms.Label()
		Me.picDTBackground = New System.Windows.Forms.PictureBox()
		Me.picEndcapL = New System.Windows.Forms.PictureBox()
		Me.picEndcapR = New System.Windows.Forms.PictureBox()
		Me.picSpaceR = New System.Windows.Forms.PictureBox()
		Me.picSpaceY = New System.Windows.Forms.PictureBox()
		Me.picSpaceG = New System.Windows.Forms.PictureBox()
		Me.cmoScanResult = New ServerSystemChecker.ctrlComboBox_Icons()
		Me.Label1 = New System.Windows.Forms.Label()
		CType(Me.picIcon, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.trackCritical, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.trackWarning, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.picDTBackground, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.picEndcapL, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.picEndcapR, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.picSpaceR, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.picSpaceY, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.picSpaceG, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.SuspendLayout()
		'
		'picIcon
		'
		Me.picIcon.Location = New System.Drawing.Point(12, 12)
		Me.picIcon.Name = "picIcon"
		Me.picIcon.Size = New System.Drawing.Size(48, 48)
		Me.picIcon.TabIndex = 13
		Me.picIcon.TabStop = False
		'
		'cmdCancel
		'
		Me.cmdCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
		Me.cmdCancel.Location = New System.Drawing.Point(276, 285)
		Me.cmdCancel.Name = "cmdCancel"
		Me.cmdCancel.Size = New System.Drawing.Size(75, 25)
		Me.cmdCancel.TabIndex = 4
		Me.cmdCancel.Text = "Cancel"
		Me.cmdCancel.UseVisualStyleBackColor = True
		'
		'cmdOK
		'
		Me.cmdOK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.cmdOK.Location = New System.Drawing.Point(357, 285)
		Me.cmdOK.Margin = New System.Windows.Forms.Padding(3, 12, 3, 3)
		Me.cmdOK.Name = "cmdOK"
		Me.cmdOK.Size = New System.Drawing.Size(75, 25)
		Me.cmdOK.TabIndex = 3
		Me.cmdOK.Text = "OK"
		Me.cmdOK.UseVisualStyleBackColor = True
		'
		'Label2
		'
		Me.Label2.AutoSize = True
		Me.Label2.Location = New System.Drawing.Point(66, 40)
		Me.Label2.Margin = New System.Windows.Forms.Padding(3)
		Me.Label2.Name = "Label2"
		Me.Label2.Size = New System.Drawing.Size(259, 13)
		Me.Label2.TabIndex = 10
		Me.Label2.Text = "Set the thresholds used for free drive space checking"
		'
		'lblTitle
		'
		Me.lblTitle.AutoSize = True
		Me.lblTitle.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.lblTitle.Location = New System.Drawing.Point(66, 18)
		Me.lblTitle.Margin = New System.Windows.Forms.Padding(3, 9, 3, 3)
		Me.lblTitle.Name = "lblTitle"
		Me.lblTitle.Size = New System.Drawing.Size(208, 16)
		Me.lblTitle.TabIndex = 9
		Me.lblTitle.Text = "Add New Free Space Thresholds"
		'
		'Label3
		'
		Me.Label3.AutoSize = True
		Me.Label3.Location = New System.Drawing.Point(12, 86)
		Me.Label3.Margin = New System.Windows.Forms.Padding(3, 12, 3, 24)
		Me.Label3.Name = "Label3"
		Me.Label3.Size = New System.Drawing.Size(44, 13)
		Me.Label3.TabIndex = 35
		Me.Label3.Text = "Critical :"
		'
		'Label5
		'
		Me.Label5.AutoSize = True
		Me.Label5.Location = New System.Drawing.Point(12, 156)
		Me.Label5.Margin = New System.Windows.Forms.Padding(3, 24, 3, 9)
		Me.Label5.Name = "Label5"
		Me.Label5.Size = New System.Drawing.Size(53, 13)
		Me.Label5.TabIndex = 39
		Me.Label5.Text = "Warning :"
		'
		'trackCritical
		'
		Me.trackCritical.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
				  Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.trackCritical.BackColor = System.Drawing.SystemColors.Control
		Me.trackCritical.Location = New System.Drawing.Point(100, 72)
		Me.trackCritical.Margin = New System.Windows.Forms.Padding(3, 9, 3, 3)
		Me.trackCritical.Maximum = 100
		Me.trackCritical.Name = "trackCritical"
		Me.trackCritical.Size = New System.Drawing.Size(332, 45)
		Me.trackCritical.TabIndex = 0
		Me.trackCritical.TickFrequency = 5
		Me.trackCritical.TickStyle = System.Windows.Forms.TickStyle.Both
		Me.trackCritical.Value = 10
		'
		'trackWarning
		'
		Me.trackWarning.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
				  Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.trackWarning.Location = New System.Drawing.Point(100, 142)
		Me.trackWarning.Maximum = 100
		Me.trackWarning.Name = "trackWarning"
		Me.trackWarning.Size = New System.Drawing.Size(332, 45)
		Me.trackWarning.TabIndex = 1
		Me.trackWarning.TickFrequency = 5
		Me.trackWarning.TickStyle = System.Windows.Forms.TickStyle.Both
		Me.trackWarning.Value = 20
		'
		'lblValue_Critical
		'
		Me.lblValue_Critical.Location = New System.Drawing.Point(71, 86)
		Me.lblValue_Critical.Margin = New System.Windows.Forms.Padding(0)
		Me.lblValue_Critical.Name = "lblValue_Critical"
		Me.lblValue_Critical.Size = New System.Drawing.Size(35, 13)
		Me.lblValue_Critical.TabIndex = 45
		Me.lblValue_Critical.Text = "10%"
		Me.lblValue_Critical.TextAlign = System.Drawing.ContentAlignment.MiddleRight
		'
		'lblValue_Warning
		'
		Me.lblValue_Warning.Location = New System.Drawing.Point(71, 156)
		Me.lblValue_Warning.Name = "lblValue_Warning"
		Me.lblValue_Warning.Size = New System.Drawing.Size(35, 13)
		Me.lblValue_Warning.TabIndex = 45
		Me.lblValue_Warning.Text = "20%"
		Me.lblValue_Warning.TextAlign = System.Drawing.ContentAlignment.MiddleRight
		'
		'cmdReset
		'
		Me.cmdReset.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.cmdReset.Location = New System.Drawing.Point(12, 285)
		Me.cmdReset.Name = "cmdReset"
		Me.cmdReset.Size = New System.Drawing.Size(125, 25)
		Me.cmdReset.TabIndex = 5
		Me.cmdReset.Text = "Reset Values"
		Me.cmdReset.UseVisualStyleBackColor = True
		'
		'lnkHelp
		'
		Me.lnkHelp.Location = New System.Drawing.Point(360, 9)
		Me.lnkHelp.Margin = New System.Windows.Forms.Padding(0)
		Me.lnkHelp.Name = "lnkHelp"
		Me.lnkHelp.Size = New System.Drawing.Size(75, 15)
		Me.lnkHelp.TabIndex = 49
		Me.lnkHelp.TabStop = True
		Me.lnkHelp.Text = "Help"
		Me.lnkHelp.TextAlign = System.Drawing.ContentAlignment.TopRight
		'
		'Label10
		'
		Me.Label10.AutoSize = True
		Me.Label10.Location = New System.Drawing.Point(12, 205)
		Me.Label10.Margin = New System.Windows.Forms.Padding(3)
		Me.Label10.Name = "Label10"
		Me.Label10.Size = New System.Drawing.Size(202, 13)
		Me.Label10.TabIndex = 64
		Me.Label10.Text = "Scan report option for non-system drives :"
		'
		'picDTBackground
		'
		Me.picDTBackground.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
				  Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.picDTBackground.Location = New System.Drawing.Point(113, 121)
		Me.picDTBackground.Name = "picDTBackground"
		Me.picDTBackground.Size = New System.Drawing.Size(306, 14)
		Me.picDTBackground.TabIndex = 68
		Me.picDTBackground.TabStop = False
		'
		'picEndcapL
		'
		Me.picEndcapL.Location = New System.Drawing.Point(15, 122)
		Me.picEndcapL.Margin = New System.Windows.Forms.Padding(1, 1, 6, 1)
		Me.picEndcapL.Name = "picEndcapL"
		Me.picEndcapL.Size = New System.Drawing.Size(12, 12)
		Me.picEndcapL.TabIndex = 69
		Me.picEndcapL.TabStop = False
		'
		'picEndcapR
		'
		Me.picEndcapR.Location = New System.Drawing.Point(77, 122)
		Me.picEndcapR.Margin = New System.Windows.Forms.Padding(6, 1, 1, 1)
		Me.picEndcapR.Name = "picEndcapR"
		Me.picEndcapR.Size = New System.Drawing.Size(12, 12)
		Me.picEndcapR.TabIndex = 70
		Me.picEndcapR.TabStop = False
		'
		'picSpaceR
		'
		Me.picSpaceR.Location = New System.Drawing.Point(33, 122)
		Me.picSpaceR.Margin = New System.Windows.Forms.Padding(1)
		Me.picSpaceR.Name = "picSpaceR"
		Me.picSpaceR.Size = New System.Drawing.Size(12, 12)
		Me.picSpaceR.TabIndex = 71
		Me.picSpaceR.TabStop = False
		'
		'picSpaceY
		'
		Me.picSpaceY.Location = New System.Drawing.Point(46, 122)
		Me.picSpaceY.Margin = New System.Windows.Forms.Padding(1)
		Me.picSpaceY.Name = "picSpaceY"
		Me.picSpaceY.Size = New System.Drawing.Size(12, 12)
		Me.picSpaceY.TabIndex = 72
		Me.picSpaceY.TabStop = False
		'
		'picSpaceG
		'
		Me.picSpaceG.Location = New System.Drawing.Point(59, 122)
		Me.picSpaceG.Margin = New System.Windows.Forms.Padding(1)
		Me.picSpaceG.Name = "picSpaceG"
		Me.picSpaceG.Size = New System.Drawing.Size(12, 12)
		Me.picSpaceG.TabIndex = 73
		Me.picSpaceG.TabStop = False
		'
		'cmoScanResult
		'
		Me.cmoScanResult.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.cmoScanResult.BackColor = System.Drawing.SystemColors.Window
		Me.cmoScanResult.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
		Me.cmoScanResult.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.cmoScanResult.FormattingEnabled = True
		Me.cmoScanResult.ItemHeight = 19
		Me.cmoScanResult.Location = New System.Drawing.Point(307, 199)
		Me.cmoScanResult.Margin = New System.Windows.Forms.Padding(3, 9, 3, 3)
		Me.cmoScanResult.Name = "cmoScanResult"
		Me.cmoScanResult.RemoveIconSpacing = False
		Me.cmoScanResult.SelectedItem = Nothing
		Me.cmoScanResult.Size = New System.Drawing.Size(125, 25)
		Me.cmoScanResult.TabIndex = 2
		'
		'Label1
		'
		Me.Label1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
				  Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.Label1.Location = New System.Drawing.Point(12, 233)
		Me.Label1.Margin = New System.Windows.Forms.Padding(3, 12, 3, 3)
		Me.Label1.Name = "Label1"
		Me.Label1.Size = New System.Drawing.Size(420, 37)
		Me.Label1.TabIndex = 50
		Me.Label1.Text = "Default values are" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "    10% citrical, 20% warning, and exclude non-system drives"
		Me.Label1.TextAlign = System.Drawing.ContentAlignment.BottomLeft
		'
		'frmAddFreeSpace
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.CancelButton = Me.cmdCancel
		Me.ClientSize = New System.Drawing.Size(444, 322)
		Me.Controls.Add(Me.picSpaceG)
		Me.Controls.Add(Me.picSpaceY)
		Me.Controls.Add(Me.picSpaceR)
		Me.Controls.Add(Me.picEndcapR)
		Me.Controls.Add(Me.picEndcapL)
		Me.Controls.Add(Me.picDTBackground)
		Me.Controls.Add(Me.cmoScanResult)
		Me.Controls.Add(Me.Label10)
		Me.Controls.Add(Me.Label1)
		Me.Controls.Add(Me.lnkHelp)
		Me.Controls.Add(Me.cmdReset)
		Me.Controls.Add(Me.lblValue_Warning)
		Me.Controls.Add(Me.lblValue_Critical)
		Me.Controls.Add(Me.trackWarning)
		Me.Controls.Add(Me.trackCritical)
		Me.Controls.Add(Me.Label5)
		Me.Controls.Add(Me.Label3)
		Me.Controls.Add(Me.picIcon)
		Me.Controls.Add(Me.cmdCancel)
		Me.Controls.Add(Me.cmdOK)
		Me.Controls.Add(Me.Label2)
		Me.Controls.Add(Me.lblTitle)
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
		Me.MaximizeBox = False
		Me.MinimizeBox = False
		Me.Name = "frmAddFreeSpace"
		Me.ShowIcon = False
		Me.ShowInTaskbar = False
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
		Me.Text = " Add New Free Space Threshold"
		CType(Me.picIcon, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.trackCritical, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.trackWarning, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.picDTBackground, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.picEndcapL, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.picEndcapR, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.picSpaceR, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.picSpaceY, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.picSpaceG, System.ComponentModel.ISupportInitialize).EndInit()
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub
	Friend WithEvents picIcon As System.Windows.Forms.PictureBox
	Friend WithEvents cmdCancel As System.Windows.Forms.Button
	Friend WithEvents cmdOK As System.Windows.Forms.Button
	Friend WithEvents Label2 As System.Windows.Forms.Label
	Friend WithEvents lblTitle As System.Windows.Forms.Label
	Friend WithEvents Label3 As System.Windows.Forms.Label
	Friend WithEvents Label5 As System.Windows.Forms.Label
	Friend WithEvents trackCritical As System.Windows.Forms.TrackBar
	Friend WithEvents trackWarning As System.Windows.Forms.TrackBar
	Friend WithEvents lblValue_Critical As System.Windows.Forms.Label
	Friend WithEvents lblValue_Warning As System.Windows.Forms.Label
	Friend WithEvents cmdReset As System.Windows.Forms.Button
	Friend WithEvents lnkHelp As System.Windows.Forms.LinkLabel
	Friend WithEvents cmoScanResult As ServerSystemChecker.ctrlComboBox_Icons
	Friend WithEvents Label10 As System.Windows.Forms.Label
	Friend WithEvents picDTBackground As System.Windows.Forms.PictureBox
	Friend WithEvents picEndcapL As System.Windows.Forms.PictureBox
	Friend WithEvents picEndcapR As System.Windows.Forms.PictureBox
	Friend WithEvents picSpaceR As System.Windows.Forms.PictureBox
	Friend WithEvents picSpaceY As System.Windows.Forms.PictureBox
	Friend WithEvents picSpaceG As System.Windows.Forms.PictureBox
	Friend WithEvents Label1 As System.Windows.Forms.Label
End Class
