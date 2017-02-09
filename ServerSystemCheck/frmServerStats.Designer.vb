<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmServerStats
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
		Me.cmdOK = New System.Windows.Forms.Button()
		Me.lblTotals = New System.Windows.Forms.Label()
		Me.Label1 = New System.Windows.Forms.Label()
		Me.tvwStats = New System.Windows.Forms.TreeView()
		Me.cmoListBy = New ServerSystemChecker.ctrlComboBox_Icons()
		CType(Me.picIcon, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.SuspendLayout()
		'
		'lnkHelp
		'
		Me.lnkHelp.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.lnkHelp.Location = New System.Drawing.Point(310, 9)
		Me.lnkHelp.Margin = New System.Windows.Forms.Padding(0)
		Me.lnkHelp.Name = "lnkHelp"
		Me.lnkHelp.Size = New System.Drawing.Size(75, 15)
		Me.lnkHelp.TabIndex = 44
		Me.lnkHelp.TabStop = True
		Me.lnkHelp.Text = "Help"
		Me.lnkHelp.TextAlign = System.Drawing.ContentAlignment.TopRight
		'
		'picIcon
		'
		Me.picIcon.Location = New System.Drawing.Point(12, 12)
		Me.picIcon.Name = "picIcon"
		Me.picIcon.Size = New System.Drawing.Size(48, 48)
		Me.picIcon.TabIndex = 43
		Me.picIcon.TabStop = False
		'
		'Label2
		'
		Me.Label2.AutoSize = True
		Me.Label2.Location = New System.Drawing.Point(66, 40)
		Me.Label2.Margin = New System.Windows.Forms.Padding(3)
		Me.Label2.Name = "Label2"
		Me.Label2.Size = New System.Drawing.Size(175, 13)
		Me.Label2.TabIndex = 42
		Me.Label2.Text = "Display Server Information Statistics"
		'
		'lblTitle
		'
		Me.lblTitle.AutoSize = True
		Me.lblTitle.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.lblTitle.Location = New System.Drawing.Point(66, 18)
		Me.lblTitle.Margin = New System.Windows.Forms.Padding(3, 9, 3, 3)
		Me.lblTitle.Name = "lblTitle"
		Me.lblTitle.Size = New System.Drawing.Size(104, 16)
		Me.lblTitle.TabIndex = 41
		Me.lblTitle.Text = "Server Statistics"
		'
		'cmdOK
		'
		Me.cmdOK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.cmdOK.Location = New System.Drawing.Point(307, 435)
		Me.cmdOK.Margin = New System.Windows.Forms.Padding(3, 12, 3, 3)
		Me.cmdOK.Name = "cmdOK"
		Me.cmdOK.Size = New System.Drawing.Size(75, 25)
		Me.cmdOK.TabIndex = 45
		Me.cmdOK.Text = "Close"
		Me.cmdOK.UseVisualStyleBackColor = True
		'
		'lblTotals
		'
		Me.lblTotals.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.lblTotals.AutoSize = True
		Me.lblTotals.Location = New System.Drawing.Point(12, 447)
		Me.lblTotals.Name = "lblTotals"
		Me.lblTotals.Size = New System.Drawing.Size(136, 13)
		Me.lblTotals.TabIndex = 47
		Me.lblTotals.Text = "0 servers, out of a total of 0"
		'
		'Label1
		'
		Me.Label1.AutoSize = True
		Me.Label1.Location = New System.Drawing.Point(12, 78)
		Me.Label1.Margin = New System.Windows.Forms.Padding(3, 12, 3, 9)
		Me.Label1.Name = "Label1"
		Me.Label1.Size = New System.Drawing.Size(41, 13)
		Me.Label1.TabIndex = 49
		Me.Label1.Text = "List By:"
		'
		'tvwStats
		'
		Me.tvwStats.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
				  Or System.Windows.Forms.AnchorStyles.Left) _
				  Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.tvwStats.Location = New System.Drawing.Point(12, 106)
		Me.tvwStats.Margin = New System.Windows.Forms.Padding(3, 6, 3, 3)
		Me.tvwStats.Name = "tvwStats"
		Me.tvwStats.Size = New System.Drawing.Size(370, 314)
		Me.tvwStats.TabIndex = 52
		'
		'cmoListBy
		'
		Me.cmoListBy.BackColor = System.Drawing.SystemColors.Window
		Me.cmoListBy.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
		Me.cmoListBy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.cmoListBy.FormattingEnabled = True
		Me.cmoListBy.ItemHeight = 19
		Me.cmoListBy.Location = New System.Drawing.Point(100, 72)
		Me.cmoListBy.Name = "cmoListBy"
		Me.cmoListBy.RemoveIconSpacing = False
		Me.cmoListBy.SelectedItem = Nothing
		Me.cmoListBy.Size = New System.Drawing.Size(282, 25)
		Me.cmoListBy.TabIndex = 54
		'
		'frmServerStats
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(394, 472)
		Me.Controls.Add(Me.cmoListBy)
		Me.Controls.Add(Me.Label1)
		Me.Controls.Add(Me.lblTotals)
		Me.Controls.Add(Me.cmdOK)
		Me.Controls.Add(Me.lnkHelp)
		Me.Controls.Add(Me.picIcon)
		Me.Controls.Add(Me.Label2)
		Me.Controls.Add(Me.lblTitle)
		Me.Controls.Add(Me.tvwStats)
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
		Me.MaximizeBox = False
		Me.MinimizeBox = False
		Me.Name = "frmServerStats"
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
		Me.Text = " Server Statistics"
		CType(Me.picIcon, System.ComponentModel.ISupportInitialize).EndInit()
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub
	Friend WithEvents lnkHelp As System.Windows.Forms.LinkLabel
	Friend WithEvents picIcon As System.Windows.Forms.PictureBox
	Friend WithEvents Label2 As System.Windows.Forms.Label
	Friend WithEvents lblTitle As System.Windows.Forms.Label
	Friend WithEvents cmdOK As System.Windows.Forms.Button
	Friend WithEvents lblTotals As System.Windows.Forms.Label
	Friend WithEvents Label1 As System.Windows.Forms.Label
	Friend WithEvents tvwStats As System.Windows.Forms.TreeView
	Friend WithEvents cmoListBy As ServerSystemChecker.ctrlComboBox_Icons
End Class
