<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPropertiesServer
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
		Me.lblInfo_01 = New System.Windows.Forms.Label()
		Me.lblServerName = New System.Windows.Forms.Label()
		Me.cmdClose = New System.Windows.Forms.Button()
		Me.lblCacheData = New System.Windows.Forms.Label()
		Me.lstInfo = New System.Windows.Forms.ListView()
		Me.cmdRefreshData = New System.Windows.Forms.Button()
		Me.cmdConnect = New System.Windows.Forms.Button()
		Me.lnkHelp = New System.Windows.Forms.LinkLabel()
		Me.lblToolTip = New System.Windows.Forms.Label()
		Me.cmdExcludeDrives = New System.Windows.Forms.Button()
		CType(Me.picIcon, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.SuspendLayout()
		'
		'picIcon
		'
		Me.picIcon.Location = New System.Drawing.Point(12, 12)
		Me.picIcon.Name = "picIcon"
		Me.picIcon.Size = New System.Drawing.Size(48, 48)
		Me.picIcon.TabIndex = 45
		Me.picIcon.TabStop = False
		'
		'lblInfo_01
		'
		Me.lblInfo_01.AutoSize = True
		Me.lblInfo_01.Location = New System.Drawing.Point(66, 40)
		Me.lblInfo_01.Margin = New System.Windows.Forms.Padding(3)
		Me.lblInfo_01.Name = "lblInfo_01"
		Me.lblInfo_01.Size = New System.Drawing.Size(53, 13)
		Me.lblInfo_01.TabIndex = 44
		Me.lblInfo_01.Text = "lblInfo_01"
		'
		'lblServerName
		'
		Me.lblServerName.AutoSize = True
		Me.lblServerName.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.lblServerName.Location = New System.Drawing.Point(66, 18)
		Me.lblServerName.Margin = New System.Windows.Forms.Padding(3, 9, 3, 3)
		Me.lblServerName.Name = "lblServerName"
		Me.lblServerName.Size = New System.Drawing.Size(99, 16)
		Me.lblServerName.TabIndex = 43
		Me.lblServerName.Text = "lblServerName"
		'
		'cmdClose
		'
		Me.cmdClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.cmdClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
		Me.cmdClose.Location = New System.Drawing.Point(407, 335)
		Me.cmdClose.Margin = New System.Windows.Forms.Padding(12, 12, 3, 3)
		Me.cmdClose.Name = "cmdClose"
		Me.cmdClose.Size = New System.Drawing.Size(75, 25)
		Me.cmdClose.TabIndex = 3
		Me.cmdClose.Text = "Close"
		Me.cmdClose.UseVisualStyleBackColor = True
		'
		'lblCacheData
		'
		Me.lblCacheData.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.lblCacheData.AutoSize = True
		Me.lblCacheData.Location = New System.Drawing.Point(146, 341)
		Me.lblCacheData.Margin = New System.Windows.Forms.Padding(6, 3, 3, 3)
		Me.lblCacheData.Name = "lblCacheData"
		Me.lblCacheData.Size = New System.Drawing.Size(97, 13)
		Me.lblCacheData.TabIndex = 50
		Me.lblCacheData.Text = "Using cached data"
		'
		'lstInfo
		'
		Me.lstInfo.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
				  Or System.Windows.Forms.AnchorStyles.Left) _
				  Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.lstInfo.Location = New System.Drawing.Point(12, 72)
		Me.lstInfo.Margin = New System.Windows.Forms.Padding(3, 9, 3, 3)
		Me.lstInfo.Name = "lstInfo"
		Me.lstInfo.Size = New System.Drawing.Size(470, 248)
		Me.lstInfo.TabIndex = 0
		Me.lstInfo.UseCompatibleStateImageBehavior = False
		'
		'cmdRefreshData
		'
		Me.cmdRefreshData.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.cmdRefreshData.Location = New System.Drawing.Point(12, 335)
		Me.cmdRefreshData.Margin = New System.Windows.Forms.Padding(3, 12, 3, 3)
		Me.cmdRefreshData.Name = "cmdRefreshData"
		Me.cmdRefreshData.Size = New System.Drawing.Size(125, 25)
		Me.cmdRefreshData.TabIndex = 1
		Me.cmdRefreshData.Text = "Refresh Data"
		Me.cmdRefreshData.UseVisualStyleBackColor = True
		'
		'cmdConnect
		'
		Me.cmdConnect.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.cmdConnect.Location = New System.Drawing.Point(407, 43)
		Me.cmdConnect.Margin = New System.Windows.Forms.Padding(3, 3, 3, 0)
		Me.cmdConnect.Name = "cmdConnect"
		Me.cmdConnect.Size = New System.Drawing.Size(75, 25)
		Me.cmdConnect.TabIndex = 4
		Me.cmdConnect.Text = "Connect..."
		Me.cmdConnect.UseVisualStyleBackColor = True
		'
		'lnkHelp
		'
		Me.lnkHelp.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.lnkHelp.Location = New System.Drawing.Point(410, 9)
		Me.lnkHelp.Margin = New System.Windows.Forms.Padding(0)
		Me.lnkHelp.Name = "lnkHelp"
		Me.lnkHelp.Size = New System.Drawing.Size(75, 15)
		Me.lnkHelp.TabIndex = 65
		Me.lnkHelp.TabStop = True
		Me.lnkHelp.Text = "Help"
		Me.lnkHelp.TextAlign = System.Drawing.ContentAlignment.TopRight
		'
		'lblToolTip
		'
		Me.lblToolTip.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
				  Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.lblToolTip.BackColor = System.Drawing.SystemColors.Info
		Me.lblToolTip.Location = New System.Drawing.Point(14, 295)
		Me.lblToolTip.Margin = New System.Windows.Forms.Padding(3)
		Me.lblToolTip.Name = "lblToolTip"
		Me.lblToolTip.Padding = New System.Windows.Forms.Padding(3)
		Me.lblToolTip.Size = New System.Drawing.Size(466, 23)
		Me.lblToolTip.TabIndex = 66
		Me.lblToolTip.Text = "lblToolTip"
		Me.lblToolTip.TextAlign = System.Drawing.ContentAlignment.BottomLeft
		'
		'cmdExcludeDrives
		'
		Me.cmdExcludeDrives.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.cmdExcludeDrives.Location = New System.Drawing.Point(267, 335)
		Me.cmdExcludeDrives.Name = "cmdExcludeDrives"
		Me.cmdExcludeDrives.Size = New System.Drawing.Size(125, 25)
		Me.cmdExcludeDrives.TabIndex = 2
		Me.cmdExcludeDrives.Text = "Exclude Drives..."
		Me.cmdExcludeDrives.UseVisualStyleBackColor = True
		'
		'frmPropertiesServer
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.CancelButton = Me.cmdClose
		Me.ClientSize = New System.Drawing.Size(494, 372)
		Me.Controls.Add(Me.cmdExcludeDrives)
		Me.Controls.Add(Me.lblToolTip)
		Me.Controls.Add(Me.lnkHelp)
		Me.Controls.Add(Me.cmdConnect)
		Me.Controls.Add(Me.lstInfo)
		Me.Controls.Add(Me.lblCacheData)
		Me.Controls.Add(Me.cmdRefreshData)
		Me.Controls.Add(Me.picIcon)
		Me.Controls.Add(Me.lblInfo_01)
		Me.Controls.Add(Me.lblServerName)
		Me.Controls.Add(Me.cmdClose)
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
		Me.MaximizeBox = False
		Me.MinimizeBox = False
		Me.Name = "frmPropertiesServer"
		Me.ShowIcon = False
		Me.ShowInTaskbar = False
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
		Me.Text = " Server Properties"
		CType(Me.picIcon, System.ComponentModel.ISupportInitialize).EndInit()
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub
	Friend WithEvents picIcon As System.Windows.Forms.PictureBox
	Friend WithEvents lblInfo_01 As System.Windows.Forms.Label
	Friend WithEvents lblServerName As System.Windows.Forms.Label
	Friend WithEvents cmdClose As System.Windows.Forms.Button
	Friend WithEvents lblCacheData As System.Windows.Forms.Label
	Friend WithEvents lstInfo As System.Windows.Forms.ListView
	Friend WithEvents cmdRefreshData As System.Windows.Forms.Button
	Friend WithEvents cmdConnect As System.Windows.Forms.Button
	Friend WithEvents lnkHelp As System.Windows.Forms.LinkLabel
	Friend WithEvents lblToolTip As System.Windows.Forms.Label
	Friend WithEvents cmdExcludeDrives As System.Windows.Forms.Button
End Class
