<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAddEventlog
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
		Me.cmdCancel = New System.Windows.Forms.Button()
		Me.cmdOK = New System.Windows.Forms.Button()
		Me.Label2 = New System.Windows.Forms.Label()
		Me.lblTitle = New System.Windows.Forms.Label()
		Me.picIcon = New System.Windows.Forms.PictureBox()
		Me.radSystem = New System.Windows.Forms.RadioButton()
		Me.radApplication = New System.Windows.Forms.RadioButton()
		Me.chkInformation = New System.Windows.Forms.CheckBox()
		Me.chkWarning = New System.Windows.Forms.CheckBox()
		Me.chkError = New System.Windows.Forms.CheckBox()
		Me.chkCritical = New System.Windows.Forms.CheckBox()
		Me.Label5 = New System.Windows.Forms.Label()
		Me.Label4 = New System.Windows.Forms.Label()
		Me.txtExclude = New System.Windows.Forms.TextBox()
		Me.Label3 = New System.Windows.Forms.Label()
		Me.Label6 = New System.Windows.Forms.Label()
		Me.chkFailure = New System.Windows.Forms.CheckBox()
		Me.chkSuccess = New System.Windows.Forms.CheckBox()
		Me.radSecurity = New System.Windows.Forms.RadioButton()
		Me.lnkHelp = New System.Windows.Forms.LinkLabel()
		Me.picImage_Success = New System.Windows.Forms.PictureBox()
		Me.picImage_Failure = New System.Windows.Forms.PictureBox()
		Me.picImage_Error = New System.Windows.Forms.PictureBox()
		Me.picImage_Critical = New System.Windows.Forms.PictureBox()
		Me.picImage_Information = New System.Windows.Forms.PictureBox()
		Me.picImage_Warning = New System.Windows.Forms.PictureBox()
		Me.lnkNote = New System.Windows.Forms.LinkLabel()
		Me.tabSearchType = New System.Windows.Forms.TabControl()
		Me.TabPage1 = New System.Windows.Forms.TabPage()
		Me.TabPage2 = New System.Windows.Forms.TabPage()
		Me.Label10 = New System.Windows.Forms.Label()
		Me.Label9 = New System.Windows.Forms.Label()
		Me.txtSpecificIDs = New System.Windows.Forms.TextBox()
		Me.Label1 = New System.Windows.Forms.Label()
		Me.Label7 = New System.Windows.Forms.Label()
		Me.Label8 = New System.Windows.Forms.Label()
		CType(Me.picIcon, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.picImage_Success, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.picImage_Failure, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.picImage_Error, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.picImage_Critical, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.picImage_Information, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.picImage_Warning, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.tabSearchType.SuspendLayout()
		Me.TabPage1.SuspendLayout()
		Me.TabPage2.SuspendLayout()
		Me.SuspendLayout()
		'
		'cmdCancel
		'
		Me.cmdCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
		Me.cmdCancel.Location = New System.Drawing.Point(226, 385)
		Me.cmdCancel.Name = "cmdCancel"
		Me.cmdCancel.Size = New System.Drawing.Size(75, 25)
		Me.cmdCancel.TabIndex = 11
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
		Me.cmdOK.TabIndex = 10
		Me.cmdOK.Text = "OK"
		Me.cmdOK.UseVisualStyleBackColor = True
		'
		'Label2
		'
		Me.Label2.AutoSize = True
		Me.Label2.Location = New System.Drawing.Point(66, 40)
		Me.Label2.Margin = New System.Windows.Forms.Padding(3)
		Me.Label2.Name = "Label2"
		Me.Label2.Size = New System.Drawing.Size(163, 13)
		Me.Label2.TabIndex = 15
		Me.Label2.Text = "Scan all events since last reboot."
		'
		'lblTitle
		'
		Me.lblTitle.AutoSize = True
		Me.lblTitle.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.lblTitle.Location = New System.Drawing.Point(66, 18)
		Me.lblTitle.Margin = New System.Windows.Forms.Padding(3, 9, 3, 3)
		Me.lblTitle.Name = "lblTitle"
		Me.lblTitle.Size = New System.Drawing.Size(153, 16)
		Me.lblTitle.TabIndex = 14
		Me.lblTitle.Text = "Add New Eventlog Scan"
		'
		'picIcon
		'
		Me.picIcon.Location = New System.Drawing.Point(12, 12)
		Me.picIcon.Name = "picIcon"
		Me.picIcon.Size = New System.Drawing.Size(48, 48)
		Me.picIcon.TabIndex = 25
		Me.picIcon.TabStop = False
		'
		'radSystem
		'
		Me.radSystem.AutoSize = True
		Me.radSystem.Location = New System.Drawing.Point(100, 93)
		Me.radSystem.Margin = New System.Windows.Forms.Padding(3, 0, 3, 3)
		Me.radSystem.Name = "radSystem"
		Me.radSystem.Size = New System.Drawing.Size(59, 17)
		Me.radSystem.TabIndex = 2
		Me.radSystem.Text = "System"
		Me.radSystem.UseVisualStyleBackColor = True
		'
		'radApplication
		'
		Me.radApplication.AutoSize = True
		Me.radApplication.Checked = True
		Me.radApplication.Location = New System.Drawing.Point(100, 73)
		Me.radApplication.Margin = New System.Windows.Forms.Padding(3, 12, 3, 3)
		Me.radApplication.Name = "radApplication"
		Me.radApplication.Size = New System.Drawing.Size(77, 17)
		Me.radApplication.TabIndex = 0
		Me.radApplication.TabStop = True
		Me.radApplication.Text = "Application"
		Me.radApplication.UseVisualStyleBackColor = True
		'
		'chkInformation
		'
		Me.chkInformation.AutoSize = True
		Me.chkInformation.Location = New System.Drawing.Point(122, 72)
		Me.chkInformation.Name = "chkInformation"
		Me.chkInformation.Size = New System.Drawing.Size(78, 17)
		Me.chkInformation.TabIndex = 6
		Me.chkInformation.Text = "Information"
		Me.chkInformation.UseVisualStyleBackColor = True
		'
		'chkWarning
		'
		Me.chkWarning.AutoSize = True
		Me.chkWarning.Location = New System.Drawing.Point(122, 52)
		Me.chkWarning.Margin = New System.Windows.Forms.Padding(3, 0, 3, 3)
		Me.chkWarning.Name = "chkWarning"
		Me.chkWarning.Size = New System.Drawing.Size(66, 17)
		Me.chkWarning.TabIndex = 5
		Me.chkWarning.Text = "Warning"
		Me.chkWarning.UseVisualStyleBackColor = True
		'
		'chkError
		'
		Me.chkError.AutoSize = True
		Me.chkError.Location = New System.Drawing.Point(122, 32)
		Me.chkError.Margin = New System.Windows.Forms.Padding(3, 0, 3, 3)
		Me.chkError.Name = "chkError"
		Me.chkError.Size = New System.Drawing.Size(48, 17)
		Me.chkError.TabIndex = 4
		Me.chkError.Text = "Error"
		Me.chkError.UseVisualStyleBackColor = True
		'
		'chkCritical
		'
		Me.chkCritical.AutoSize = True
		Me.chkCritical.Location = New System.Drawing.Point(122, 12)
		Me.chkCritical.Name = "chkCritical"
		Me.chkCritical.Size = New System.Drawing.Size(57, 17)
		Me.chkCritical.TabIndex = 3
		Me.chkCritical.Text = "Critical"
		Me.chkCritical.UseVisualStyleBackColor = True
		'
		'Label5
		'
		Me.Label5.AutoSize = True
		Me.Label5.Location = New System.Drawing.Point(12, 12)
		Me.Label5.Margin = New System.Windows.Forms.Padding(3, 3, 3, 9)
		Me.Label5.Name = "Label5"
		Me.Label5.Size = New System.Drawing.Size(70, 13)
		Me.Label5.TabIndex = 29
		Me.Label5.Text = "Event Level :"
		'
		'Label4
		'
		Me.Label4.AutoSize = True
		Me.Label4.Location = New System.Drawing.Point(12, 107)
		Me.Label4.Margin = New System.Windows.Forms.Padding(3, 24, 3, 9)
		Me.Label4.Name = "Label4"
		Me.Label4.Size = New System.Drawing.Size(70, 13)
		Me.Label4.TabIndex = 28
		Me.Label4.Text = "Exclude IDs :"
		'
		'txtExclude
		'
		Me.txtExclude.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
				  Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.txtExclude.Location = New System.Drawing.Point(100, 104)
		Me.txtExclude.Margin = New System.Windows.Forms.Padding(3, 12, 3, 3)
		Me.txtExclude.Name = "txtExclude"
		Me.txtExclude.Size = New System.Drawing.Size(250, 20)
		Me.txtExclude.TabIndex = 9
		'
		'Label3
		'
		Me.Label3.AutoSize = True
		Me.Label3.Location = New System.Drawing.Point(12, 75)
		Me.Label3.Margin = New System.Windows.Forms.Padding(3, 12, 3, 9)
		Me.Label3.Name = "Label3"
		Me.Label3.Size = New System.Drawing.Size(55, 13)
		Me.Label3.TabIndex = 26
		Me.Label3.Text = "Eventlog :"
		'
		'Label6
		'
		Me.Label6.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
				  Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.Label6.Enabled = False
		Me.Label6.Location = New System.Drawing.Point(100, 128)
		Me.Label6.Margin = New System.Windows.Forms.Padding(3, 1, 3, 3)
		Me.Label6.Name = "Label6"
		Me.Label6.Size = New System.Drawing.Size(250, 41)
		Me.Label6.TabIndex = 36
		Me.Label6.Text = "Enter ID numbers and/or ranges separated by commas.  For example: 1, 3, 5-99"
		'
		'chkFailure
		'
		Me.chkFailure.AutoSize = True
		Me.chkFailure.Location = New System.Drawing.Point(246, 32)
		Me.chkFailure.Margin = New System.Windows.Forms.Padding(3, 0, 3, 3)
		Me.chkFailure.Name = "chkFailure"
		Me.chkFailure.Size = New System.Drawing.Size(84, 17)
		Me.chkFailure.TabIndex = 8
		Me.chkFailure.Text = "Audit Failure"
		Me.chkFailure.UseVisualStyleBackColor = True
		'
		'chkSuccess
		'
		Me.chkSuccess.AutoSize = True
		Me.chkSuccess.Location = New System.Drawing.Point(246, 12)
		Me.chkSuccess.Name = "chkSuccess"
		Me.chkSuccess.Size = New System.Drawing.Size(94, 17)
		Me.chkSuccess.TabIndex = 7
		Me.chkSuccess.Text = "Audit Success"
		Me.chkSuccess.UseVisualStyleBackColor = True
		'
		'radSecurity
		'
		Me.radSecurity.AutoSize = True
		Me.radSecurity.Location = New System.Drawing.Point(100, 113)
		Me.radSecurity.Margin = New System.Windows.Forms.Padding(3, 0, 3, 3)
		Me.radSecurity.Name = "radSecurity"
		Me.radSecurity.Size = New System.Drawing.Size(63, 17)
		Me.radSecurity.TabIndex = 1
		Me.radSecurity.Text = "Security"
		Me.radSecurity.UseVisualStyleBackColor = True
		'
		'lnkHelp
		'
		Me.lnkHelp.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.lnkHelp.Location = New System.Drawing.Point(310, 9)
		Me.lnkHelp.Margin = New System.Windows.Forms.Padding(0)
		Me.lnkHelp.Name = "lnkHelp"
		Me.lnkHelp.Size = New System.Drawing.Size(75, 15)
		Me.lnkHelp.TabIndex = 40
		Me.lnkHelp.TabStop = True
		Me.lnkHelp.Text = "Help"
		Me.lnkHelp.TextAlign = System.Drawing.ContentAlignment.TopRight
		'
		'picImage_Success
		'
		Me.picImage_Success.Location = New System.Drawing.Point(224, 12)
		Me.picImage_Success.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
		Me.picImage_Success.Name = "picImage_Success"
		Me.picImage_Success.Size = New System.Drawing.Size(16, 16)
		Me.picImage_Success.TabIndex = 41
		Me.picImage_Success.TabStop = False
		'
		'picImage_Failure
		'
		Me.picImage_Failure.Location = New System.Drawing.Point(224, 32)
		Me.picImage_Failure.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
		Me.picImage_Failure.Name = "picImage_Failure"
		Me.picImage_Failure.Size = New System.Drawing.Size(16, 16)
		Me.picImage_Failure.TabIndex = 42
		Me.picImage_Failure.TabStop = False
		'
		'picImage_Error
		'
		Me.picImage_Error.Location = New System.Drawing.Point(100, 32)
		Me.picImage_Error.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
		Me.picImage_Error.Name = "picImage_Error"
		Me.picImage_Error.Size = New System.Drawing.Size(16, 16)
		Me.picImage_Error.TabIndex = 44
		Me.picImage_Error.TabStop = False
		'
		'picImage_Critical
		'
		Me.picImage_Critical.Location = New System.Drawing.Point(100, 12)
		Me.picImage_Critical.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
		Me.picImage_Critical.Name = "picImage_Critical"
		Me.picImage_Critical.Size = New System.Drawing.Size(16, 16)
		Me.picImage_Critical.TabIndex = 43
		Me.picImage_Critical.TabStop = False
		'
		'picImage_Information
		'
		Me.picImage_Information.Location = New System.Drawing.Point(100, 72)
		Me.picImage_Information.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
		Me.picImage_Information.Name = "picImage_Information"
		Me.picImage_Information.Size = New System.Drawing.Size(16, 16)
		Me.picImage_Information.TabIndex = 46
		Me.picImage_Information.TabStop = False
		'
		'picImage_Warning
		'
		Me.picImage_Warning.Location = New System.Drawing.Point(100, 52)
		Me.picImage_Warning.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
		Me.picImage_Warning.Name = "picImage_Warning"
		Me.picImage_Warning.Size = New System.Drawing.Size(16, 16)
		Me.picImage_Warning.TabIndex = 45
		Me.picImage_Warning.TabStop = False
		'
		'lnkNote
		'
		Me.lnkNote.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.lnkNote.LinkColor = System.Drawing.Color.Blue
		Me.lnkNote.Location = New System.Drawing.Point(310, 30)
		Me.lnkNote.Margin = New System.Windows.Forms.Padding(0, 6, 0, 0)
		Me.lnkNote.Name = "lnkNote"
		Me.lnkNote.Size = New System.Drawing.Size(75, 15)
		Me.lnkNote.TabIndex = 47
		Me.lnkNote.TabStop = True
		Me.lnkNote.Text = "Admin Note"
		Me.lnkNote.TextAlign = System.Drawing.ContentAlignment.TopRight
		'
		'tabSearchType
		'
		Me.tabSearchType.Controls.Add(Me.TabPage1)
		Me.tabSearchType.Controls.Add(Me.TabPage2)
		Me.tabSearchType.Location = New System.Drawing.Point(12, 157)
		Me.tabSearchType.Margin = New System.Windows.Forms.Padding(3, 24, 3, 3)
		Me.tabSearchType.Name = "tabSearchType"
		Me.tabSearchType.Padding = New System.Drawing.Point(12, 6)
		Me.tabSearchType.SelectedIndex = 0
		Me.tabSearchType.Size = New System.Drawing.Size(370, 213)
		Me.tabSearchType.TabIndex = 48
		'
		'TabPage1
		'
		Me.TabPage1.BackColor = System.Drawing.SystemColors.Control
		Me.TabPage1.Controls.Add(Me.Label5)
		Me.TabPage1.Controls.Add(Me.txtExclude)
		Me.TabPage1.Controls.Add(Me.picImage_Information)
		Me.TabPage1.Controls.Add(Me.Label4)
		Me.TabPage1.Controls.Add(Me.picImage_Warning)
		Me.TabPage1.Controls.Add(Me.chkCritical)
		Me.TabPage1.Controls.Add(Me.picImage_Error)
		Me.TabPage1.Controls.Add(Me.chkError)
		Me.TabPage1.Controls.Add(Me.picImage_Critical)
		Me.TabPage1.Controls.Add(Me.chkWarning)
		Me.TabPage1.Controls.Add(Me.picImage_Failure)
		Me.TabPage1.Controls.Add(Me.chkInformation)
		Me.TabPage1.Controls.Add(Me.picImage_Success)
		Me.TabPage1.Controls.Add(Me.Label6)
		Me.TabPage1.Controls.Add(Me.chkSuccess)
		Me.TabPage1.Controls.Add(Me.chkFailure)
		Me.TabPage1.Location = New System.Drawing.Point(4, 28)
		Me.TabPage1.Name = "TabPage1"
		Me.TabPage1.Padding = New System.Windows.Forms.Padding(9)
		Me.TabPage1.Size = New System.Drawing.Size(362, 181)
		Me.TabPage1.TabIndex = 0
		Me.TabPage1.Text = "Scan All Events"
		'
		'TabPage2
		'
		Me.TabPage2.BackColor = System.Drawing.SystemColors.Control
		Me.TabPage2.Controls.Add(Me.Label10)
		Me.TabPage2.Controls.Add(Me.Label9)
		Me.TabPage2.Controls.Add(Me.txtSpecificIDs)
		Me.TabPage2.Controls.Add(Me.Label1)
		Me.TabPage2.Controls.Add(Me.Label7)
		Me.TabPage2.Location = New System.Drawing.Point(4, 28)
		Me.TabPage2.Name = "TabPage2"
		Me.TabPage2.Padding = New System.Windows.Forms.Padding(9)
		Me.TabPage2.Size = New System.Drawing.Size(362, 181)
		Me.TabPage2.TabIndex = 1
		Me.TabPage2.Text = "Scan For Specific IDs"
		'
		'Label10
		'
		Me.Label10.Location = New System.Drawing.Point(12, 51)
		Me.Label10.Name = "Label10"
		Me.Label10.Size = New System.Drawing.Size(338, 32)
		Me.Label10.TabIndex = 41
		Me.Label10.Text = "A green tick during scanning will indicate that the specific Event IDs have not b" & _
		  "een found."
		'
		'Label9
		'
		Me.Label9.Location = New System.Drawing.Point(12, 12)
		Me.Label9.Margin = New System.Windows.Forms.Padding(3)
		Me.Label9.Name = "Label9"
		Me.Label9.Size = New System.Drawing.Size(338, 36)
		Me.Label9.TabIndex = 40
		Me.Label9.Text = "Only the Event IDs listed below will be checked.  Any events matching this list w" & _
		  "ill be flagged as a fail."
		'
		'txtSpecificIDs
		'
		Me.txtSpecificIDs.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
				  Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.txtSpecificIDs.Location = New System.Drawing.Point(100, 104)
		Me.txtSpecificIDs.Margin = New System.Windows.Forms.Padding(3, 12, 3, 3)
		Me.txtSpecificIDs.Name = "txtSpecificIDs"
		Me.txtSpecificIDs.Size = New System.Drawing.Size(250, 20)
		Me.txtSpecificIDs.TabIndex = 37
		'
		'Label1
		'
		Me.Label1.AutoSize = True
		Me.Label1.Location = New System.Drawing.Point(12, 107)
		Me.Label1.Margin = New System.Windows.Forms.Padding(3, 24, 3, 9)
		Me.Label1.Name = "Label1"
		Me.Label1.Size = New System.Drawing.Size(70, 13)
		Me.Label1.TabIndex = 38
		Me.Label1.Text = "Specific IDs :"
		'
		'Label7
		'
		Me.Label7.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
				  Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.Label7.Enabled = False
		Me.Label7.Location = New System.Drawing.Point(100, 128)
		Me.Label7.Margin = New System.Windows.Forms.Padding(3, 1, 3, 3)
		Me.Label7.Name = "Label7"
		Me.Label7.Size = New System.Drawing.Size(250, 41)
		Me.Label7.TabIndex = 39
		Me.Label7.Text = "Enter ID numbers and/or ranges separated by commas.  For example: 1, 3, 5-99"
		'
		'Label8
		'
		Me.Label8.Location = New System.Drawing.Point(12, 376)
		Me.Label8.Margin = New System.Windows.Forms.Padding(3)
		Me.Label8.Name = "Label8"
		Me.Label8.Size = New System.Drawing.Size(208, 34)
		Me.Label8.TabIndex = 50
		Me.Label8.Text = "Event IDs will be consolidated and sorted automatically if required."
		Me.Label8.TextAlign = System.Drawing.ContentAlignment.BottomLeft
		'
		'frmAddEventlog
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.CancelButton = Me.cmdCancel
		Me.ClientSize = New System.Drawing.Size(394, 422)
		Me.Controls.Add(Me.Label8)
		Me.Controls.Add(Me.tabSearchType)
		Me.Controls.Add(Me.lnkNote)
		Me.Controls.Add(Me.lnkHelp)
		Me.Controls.Add(Me.radSecurity)
		Me.Controls.Add(Me.radSystem)
		Me.Controls.Add(Me.radApplication)
		Me.Controls.Add(Me.Label3)
		Me.Controls.Add(Me.picIcon)
		Me.Controls.Add(Me.Label2)
		Me.Controls.Add(Me.lblTitle)
		Me.Controls.Add(Me.cmdCancel)
		Me.Controls.Add(Me.cmdOK)
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
		Me.MaximizeBox = False
		Me.MinimizeBox = False
		Me.Name = "frmAddEventlog"
		Me.ShowIcon = False
		Me.ShowInTaskbar = False
		Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
		Me.Text = " Add New Eventlog Scan"
		CType(Me.picIcon, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.picImage_Success, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.picImage_Failure, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.picImage_Error, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.picImage_Critical, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.picImage_Information, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.picImage_Warning, System.ComponentModel.ISupportInitialize).EndInit()
		Me.tabSearchType.ResumeLayout(False)
		Me.TabPage1.ResumeLayout(False)
		Me.TabPage1.PerformLayout()
		Me.TabPage2.ResumeLayout(False)
		Me.TabPage2.PerformLayout()
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub
	Friend WithEvents cmdCancel As System.Windows.Forms.Button
	Friend WithEvents cmdOK As System.Windows.Forms.Button
	Friend WithEvents Label2 As System.Windows.Forms.Label
	Friend WithEvents lblTitle As System.Windows.Forms.Label
	Friend WithEvents picIcon As System.Windows.Forms.PictureBox
	Friend WithEvents radSystem As System.Windows.Forms.RadioButton
	Friend WithEvents radApplication As System.Windows.Forms.RadioButton
	Friend WithEvents chkInformation As System.Windows.Forms.CheckBox
	Friend WithEvents chkWarning As System.Windows.Forms.CheckBox
	Friend WithEvents chkError As System.Windows.Forms.CheckBox
	Friend WithEvents chkCritical As System.Windows.Forms.CheckBox
	Friend WithEvents Label5 As System.Windows.Forms.Label
	Friend WithEvents Label4 As System.Windows.Forms.Label
	Friend WithEvents txtExclude As System.Windows.Forms.TextBox
	Friend WithEvents Label3 As System.Windows.Forms.Label
	Friend WithEvents Label6 As System.Windows.Forms.Label
	Friend WithEvents chkFailure As System.Windows.Forms.CheckBox
	Friend WithEvents chkSuccess As System.Windows.Forms.CheckBox
	Friend WithEvents radSecurity As System.Windows.Forms.RadioButton
	Friend WithEvents lnkHelp As System.Windows.Forms.LinkLabel
	Friend WithEvents picImage_Success As System.Windows.Forms.PictureBox
	Friend WithEvents picImage_Failure As System.Windows.Forms.PictureBox
	Friend WithEvents picImage_Error As System.Windows.Forms.PictureBox
	Friend WithEvents picImage_Critical As System.Windows.Forms.PictureBox
	Friend WithEvents picImage_Information As System.Windows.Forms.PictureBox
	Friend WithEvents picImage_Warning As System.Windows.Forms.PictureBox
	Friend WithEvents lnkNote As System.Windows.Forms.LinkLabel
	Friend WithEvents tabSearchType As System.Windows.Forms.TabControl
	Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
	Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
	Friend WithEvents txtSpecificIDs As System.Windows.Forms.TextBox
	Friend WithEvents Label1 As System.Windows.Forms.Label
	Friend WithEvents Label7 As System.Windows.Forms.Label
	Friend WithEvents Label8 As System.Windows.Forms.Label
	Friend WithEvents Label9 As System.Windows.Forms.Label
	Friend WithEvents Label10 As System.Windows.Forms.Label
End Class
