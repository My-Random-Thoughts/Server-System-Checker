<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmScan
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
		Me.proProgress = New System.Windows.Forms.ProgressBar()
		Me.cmdClose = New System.Windows.Forms.Button()
		Me.picIcon = New System.Windows.Forms.PictureBox()
		Me.lblSubTitle = New System.Windows.Forms.Label()
		Me.lblTitle = New System.Windows.Forms.Label()
		Me.lblStatus = New System.Windows.Forms.Label()
		Me.cmdExport = New System.Windows.Forms.Button()
		Me.cmdLegend = New System.Windows.Forms.Button()
		Me.lnkHelp = New System.Windows.Forms.LinkLabel()
		Me.lnkNote = New System.Windows.Forms.LinkLabel()
		Me.lstStats = New ServerSystemChecker.ctrlListView_SubIcons()
		Me.lstResults = New ServerSystemChecker.ctrlListView_SubIcons()
		CType(Me.picIcon, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.SuspendLayout()
		'
		'proProgress
		'
		Me.proProgress.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
				  Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.proProgress.Location = New System.Drawing.Point(99, 522)
		Me.proProgress.Margin = New System.Windows.Forms.Padding(9, 3, 9, 3)
		Me.proProgress.Name = "proProgress"
		Me.proProgress.Size = New System.Drawing.Size(596, 13)
		Me.proProgress.Style = System.Windows.Forms.ProgressBarStyle.Continuous
		Me.proProgress.TabIndex = 1
		'
		'cmdClose
		'
		Me.cmdClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.cmdClose.Location = New System.Drawing.Point(707, 510)
		Me.cmdClose.Margin = New System.Windows.Forms.Padding(3, 12, 3, 3)
		Me.cmdClose.Name = "cmdClose"
		Me.cmdClose.Size = New System.Drawing.Size(75, 25)
		Me.cmdClose.TabIndex = 3
		Me.cmdClose.Text = "Close"
		Me.cmdClose.UseVisualStyleBackColor = True
		'
		'picIcon
		'
		Me.picIcon.Location = New System.Drawing.Point(12, 12)
		Me.picIcon.Name = "picIcon"
		Me.picIcon.Size = New System.Drawing.Size(48, 48)
		Me.picIcon.TabIndex = 28
		Me.picIcon.TabStop = False
		'
		'lblSubTitle
		'
		Me.lblSubTitle.AutoSize = True
		Me.lblSubTitle.Location = New System.Drawing.Point(66, 40)
		Me.lblSubTitle.Margin = New System.Windows.Forms.Padding(3)
		Me.lblSubTitle.Name = "lblSubTitle"
		Me.lblSubTitle.Size = New System.Drawing.Size(201, 13)
		Me.lblSubTitle.TabIndex = 27
		Me.lblSubTitle.Text = "Please wait for the process to complete..."
		'
		'lblTitle
		'
		Me.lblTitle.AutoSize = True
		Me.lblTitle.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.lblTitle.Location = New System.Drawing.Point(66, 18)
		Me.lblTitle.Margin = New System.Windows.Forms.Padding(3, 9, 3, 3)
		Me.lblTitle.Name = "lblTitle"
		Me.lblTitle.Size = New System.Drawing.Size(165, 16)
		Me.lblTitle.TabIndex = 26
		Me.lblTitle.Text = "Server Scanning Progress"
		'
		'lblStatus
		'
		Me.lblStatus.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.lblStatus.AutoSize = True
		Me.lblStatus.Location = New System.Drawing.Point(96, 505)
		Me.lblStatus.Margin = New System.Windows.Forms.Padding(3, 0, 3, 1)
		Me.lblStatus.Name = "lblStatus"
		Me.lblStatus.Size = New System.Drawing.Size(40, 13)
		Me.lblStatus.TabIndex = 29
		Me.lblStatus.Text = "Status:"
		'
		'cmdExport
		'
		Me.cmdExport.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.cmdExport.Location = New System.Drawing.Point(576, 510)
		Me.cmdExport.Name = "cmdExport"
		Me.cmdExport.Size = New System.Drawing.Size(125, 25)
		Me.cmdExport.TabIndex = 30
		Me.cmdExport.Text = "Export Results..."
		Me.cmdExport.UseVisualStyleBackColor = True
		'
		'cmdLegend
		'
		Me.cmdLegend.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.cmdLegend.Location = New System.Drawing.Point(12, 510)
		Me.cmdLegend.Name = "cmdLegend"
		Me.cmdLegend.Size = New System.Drawing.Size(75, 25)
		Me.cmdLegend.TabIndex = 31
		Me.cmdLegend.Text = "Legend..."
		Me.cmdLegend.UseVisualStyleBackColor = True
		'
		'lnkHelp
		'
		Me.lnkHelp.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.lnkHelp.Location = New System.Drawing.Point(710, 9)
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
		Me.lnkNote.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.lnkNote.LinkColor = System.Drawing.Color.Blue
		Me.lnkNote.Location = New System.Drawing.Point(710, 30)
		Me.lnkNote.Margin = New System.Windows.Forms.Padding(0, 6, 0, 0)
		Me.lnkNote.Name = "lnkNote"
		Me.lnkNote.Size = New System.Drawing.Size(75, 15)
		Me.lnkNote.TabIndex = 46
		Me.lnkNote.TabStop = True
		Me.lnkNote.Text = "Admin Note"
		Me.lnkNote.TextAlign = System.Drawing.ContentAlignment.TopRight
		'
		'lstStats
		'
		Me.lstStats.BackColor = System.Drawing.SystemColors.Control
		Me.lstStats.Location = New System.Drawing.Point(12, 473)
		Me.lstStats.Margin = New System.Windows.Forms.Padding(3, 1, 3, 3)
		Me.lstStats.Name = "lstStats"
		Me.lstStats.OwnerDraw = True
		Me.lstStats.Size = New System.Drawing.Size(770, 22)
		Me.lstStats.TabIndex = 50
		Me.lstStats.TabStop = False
		Me.lstStats.UseCompatibleStateImageBehavior = False
		'
		'lstResults
		'
		Me.lstResults.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
				  Or System.Windows.Forms.AnchorStyles.Left) _
				  Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.lstResults.Location = New System.Drawing.Point(12, 72)
		Me.lstResults.Margin = New System.Windows.Forms.Padding(3, 9, 3, 3)
		Me.lstResults.Name = "lstResults"
		Me.lstResults.OwnerDraw = True
		Me.lstResults.Size = New System.Drawing.Size(770, 397)
		Me.lstResults.TabIndex = 49
		Me.lstResults.UseCompatibleStateImageBehavior = False
		'
		'frmScan
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(794, 547)
		Me.ControlBox = False
		Me.Controls.Add(Me.lstStats)
		Me.Controls.Add(Me.cmdExport)
		Me.Controls.Add(Me.lstResults)
		Me.Controls.Add(Me.lnkNote)
		Me.Controls.Add(Me.lnkHelp)
		Me.Controls.Add(Me.picIcon)
		Me.Controls.Add(Me.lblSubTitle)
		Me.Controls.Add(Me.lblTitle)
		Me.Controls.Add(Me.lblStatus)
		Me.Controls.Add(Me.cmdClose)
		Me.Controls.Add(Me.proProgress)
		Me.Controls.Add(Me.cmdLegend)
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
		Me.MaximizeBox = False
		Me.MinimizeBox = False
		Me.Name = "frmScan"
		Me.ShowIcon = False
		Me.ShowInTaskbar = False
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
		Me.Text = " Server Scanning And Results..."
		CType(Me.picIcon, System.ComponentModel.ISupportInitialize).EndInit()
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub
	Friend WithEvents proProgress As System.Windows.Forms.ProgressBar
	Friend WithEvents cmdClose As System.Windows.Forms.Button
	Friend WithEvents picIcon As System.Windows.Forms.PictureBox
	Friend WithEvents lblSubTitle As System.Windows.Forms.Label
	Friend WithEvents lblTitle As System.Windows.Forms.Label
	Friend WithEvents lblStatus As System.Windows.Forms.Label
	Friend WithEvents cmdExport As System.Windows.Forms.Button
	Friend WithEvents cmdLegend As System.Windows.Forms.Button
	Friend WithEvents lnkHelp As System.Windows.Forms.LinkLabel
	Friend WithEvents lnkNote As System.Windows.Forms.LinkLabel
	Friend WithEvents lstResults As ServerSystemChecker.ctrlListView_SubIcons
	Friend WithEvents lstStats As ServerSystemChecker.ctrlListView_SubIcons
End Class
