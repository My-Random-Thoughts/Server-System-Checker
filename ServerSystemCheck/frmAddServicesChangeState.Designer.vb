<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAddServicesChangeState
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
		Me.Label2 = New System.Windows.Forms.Label()
		Me.Label1 = New System.Windows.Forms.Label()
		Me.picIcon = New System.Windows.Forms.PictureBox()
		Me.lblTitle = New System.Windows.Forms.Label()
		Me.lnkHelp = New System.Windows.Forms.LinkLabel()
		Me.cmoMissing = New ServerSystemChecker.ctrlComboBox_Icons()
		Me.cmoChecking = New ServerSystemChecker.ctrlComboBox_Icons()
		CType(Me.picIcon, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.SuspendLayout()
		'
		'cmdOK
		'
		Me.cmdOK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.cmdOK.Location = New System.Drawing.Point(282, 143)
		Me.cmdOK.Margin = New System.Windows.Forms.Padding(3, 12, 3, 3)
		Me.cmdOK.Name = "cmdOK"
		Me.cmdOK.Size = New System.Drawing.Size(75, 25)
		Me.cmdOK.TabIndex = 2
		Me.cmdOK.Text = "OK"
		Me.cmdOK.UseVisualStyleBackColor = True
		'
		'cmdCancel
		'
		Me.cmdCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
		Me.cmdCancel.Location = New System.Drawing.Point(202, 143)
		Me.cmdCancel.Margin = New System.Windows.Forms.Padding(3, 12, 3, 3)
		Me.cmdCancel.Name = "cmdCancel"
		Me.cmdCancel.Size = New System.Drawing.Size(75, 25)
		Me.cmdCancel.TabIndex = 3
		Me.cmdCancel.Text = "Cancel"
		Me.cmdCancel.UseVisualStyleBackColor = True
		'
		'Label2
		'
		Me.Label2.AutoSize = True
		Me.Label2.Location = New System.Drawing.Point(12, 109)
		Me.Label2.Name = "Label2"
		Me.Label2.Size = New System.Drawing.Size(141, 13)
		Me.Label2.TabIndex = 50
		Me.Label2.Text = "Scan Result When Missing :"
		'
		'Label1
		'
		Me.Label1.AutoSize = True
		Me.Label1.Location = New System.Drawing.Point(12, 78)
		Me.Label1.Name = "Label1"
		Me.Label1.Size = New System.Drawing.Size(111, 13)
		Me.Label1.TabIndex = 49
		Me.Label1.Text = "Service Check State :"
		'
		'picIcon
		'
		Me.picIcon.Location = New System.Drawing.Point(12, 12)
		Me.picIcon.Name = "picIcon"
		Me.picIcon.Size = New System.Drawing.Size(48, 48)
		Me.picIcon.TabIndex = 53
		Me.picIcon.TabStop = False
		'
		'lblTitle
		'
		Me.lblTitle.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.lblTitle.Location = New System.Drawing.Point(66, 18)
		Me.lblTitle.Margin = New System.Windows.Forms.Padding(3, 9, 36, 3)
		Me.lblTitle.Name = "lblTitle"
		Me.lblTitle.Size = New System.Drawing.Size(258, 42)
		Me.lblTitle.TabIndex = 51
		Me.lblTitle.Text = "[service name]"
		'
		'lnkHelp
		'
		Me.lnkHelp.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.lnkHelp.Location = New System.Drawing.Point(285, 9)
		Me.lnkHelp.Margin = New System.Windows.Forms.Padding(0)
		Me.lnkHelp.Name = "lnkHelp"
		Me.lnkHelp.Size = New System.Drawing.Size(75, 15)
		Me.lnkHelp.TabIndex = 54
		Me.lnkHelp.TabStop = True
		Me.lnkHelp.Text = "Help"
		Me.lnkHelp.TextAlign = System.Drawing.ContentAlignment.TopRight
		'
		'cmoMissing
		'
		Me.cmoMissing.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.cmoMissing.BackColor = System.Drawing.SystemColors.Window
		Me.cmoMissing.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
		Me.cmoMissing.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.cmoMissing.FormattingEnabled = True
		Me.cmoMissing.ItemHeight = 19
		Me.cmoMissing.Location = New System.Drawing.Point(182, 103)
		Me.cmoMissing.Name = "cmoMissing"
		Me.cmoMissing.RemoveIconSpacing = False
		Me.cmoMissing.SelectedItem = Nothing
		Me.cmoMissing.Size = New System.Drawing.Size(175, 25)
		Me.cmoMissing.TabIndex = 1
		'
		'cmoChecking
		'
		Me.cmoChecking.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.cmoChecking.BackColor = System.Drawing.SystemColors.Window
		Me.cmoChecking.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
		Me.cmoChecking.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.cmoChecking.FormattingEnabled = True
		Me.cmoChecking.ItemHeight = 19
		Me.cmoChecking.Location = New System.Drawing.Point(182, 72)
		Me.cmoChecking.Name = "cmoChecking"
		Me.cmoChecking.RemoveIconSpacing = False
		Me.cmoChecking.SelectedItem = Nothing
		Me.cmoChecking.Size = New System.Drawing.Size(175, 25)
		Me.cmoChecking.TabIndex = 0
		'
		'frmAddServicesChangeState
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.CancelButton = Me.cmdCancel
		Me.ClientSize = New System.Drawing.Size(369, 180)
		Me.ControlBox = False
		Me.Controls.Add(Me.lblTitle)
		Me.Controls.Add(Me.lnkHelp)
		Me.Controls.Add(Me.picIcon)
		Me.Controls.Add(Me.Label2)
		Me.Controls.Add(Me.Label1)
		Me.Controls.Add(Me.cmoMissing)
		Me.Controls.Add(Me.cmoChecking)
		Me.Controls.Add(Me.cmdCancel)
		Me.Controls.Add(Me.cmdOK)
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
		Me.MaximizeBox = False
		Me.MinimizeBox = False
		Me.Name = "frmAddServicesChangeState"
		Me.ShowIcon = False
		Me.ShowInTaskbar = False
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
		Me.Text = " Change Service Checking State"
		CType(Me.picIcon, System.ComponentModel.ISupportInitialize).EndInit()
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub
	Friend WithEvents cmdOK As System.Windows.Forms.Button
	Friend WithEvents cmdCancel As System.Windows.Forms.Button
	Friend WithEvents Label2 As System.Windows.Forms.Label
	Friend WithEvents Label1 As System.Windows.Forms.Label
	Friend WithEvents cmoMissing As ServerSystemChecker.ctrlComboBox_Icons
	Friend WithEvents cmoChecking As ServerSystemChecker.ctrlComboBox_Icons
	Friend WithEvents picIcon As System.Windows.Forms.PictureBox
	Friend WithEvents lblTitle As System.Windows.Forms.Label
	Friend WithEvents lnkHelp As System.Windows.Forms.LinkLabel
End Class
