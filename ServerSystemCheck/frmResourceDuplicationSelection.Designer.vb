<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmResourceDuplicationSelection
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
		Me.Label2 = New System.Windows.Forms.Label()
		Me.cmdCancel = New System.Windows.Forms.Button()
		Me.lnkHelp = New System.Windows.Forms.LinkLabel()
		Me.picIcon = New System.Windows.Forms.PictureBox()
		Me.lblTitle = New System.Windows.Forms.Label()
		Me.cmdTarget = New System.Windows.Forms.Button()
		Me.cmdSource = New System.Windows.Forms.Button()
		Me.picIconOverlay = New System.Windows.Forms.PictureBox()
		Me.Label1 = New System.Windows.Forms.Label()
		Me.Label3 = New System.Windows.Forms.Label()
		Me.lstResources = New ServerSystemChecker.ctrlListView_SubIcons()
		CType(Me.picIcon, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.picIconOverlay, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.SuspendLayout()
		'
		'Label2
		'
		Me.Label2.AutoSize = True
		Me.Label2.Location = New System.Drawing.Point(66, 40)
		Me.Label2.Margin = New System.Windows.Forms.Padding(3)
		Me.Label2.Name = "Label2"
		Me.Label2.Size = New System.Drawing.Size(324, 13)
		Me.Label2.TabIndex = 75
		Me.Label2.Text = "Select which resource you want to keep, the other will be deleted..."
		'
		'cmdCancel
		'
		Me.cmdCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
		Me.cmdCancel.Location = New System.Drawing.Point(507, 335)
		Me.cmdCancel.Margin = New System.Windows.Forms.Padding(12, 12, 3, 3)
		Me.cmdCancel.Name = "cmdCancel"
		Me.cmdCancel.Size = New System.Drawing.Size(75, 25)
		Me.cmdCancel.TabIndex = 74
		Me.cmdCancel.Text = "Cancel"
		Me.cmdCancel.UseVisualStyleBackColor = True
		'
		'lnkHelp
		'
		Me.lnkHelp.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.lnkHelp.Enabled = False
		Me.lnkHelp.Location = New System.Drawing.Point(509, 11)
		Me.lnkHelp.Margin = New System.Windows.Forms.Padding(0)
		Me.lnkHelp.Name = "lnkHelp"
		Me.lnkHelp.Size = New System.Drawing.Size(75, 15)
		Me.lnkHelp.TabIndex = 73
		Me.lnkHelp.TabStop = True
		Me.lnkHelp.Text = "Help"
		Me.lnkHelp.TextAlign = System.Drawing.ContentAlignment.TopRight
		'
		'picIcon
		'
		Me.picIcon.Location = New System.Drawing.Point(12, 12)
		Me.picIcon.Name = "picIcon"
		Me.picIcon.Size = New System.Drawing.Size(48, 48)
		Me.picIcon.TabIndex = 71
		Me.picIcon.TabStop = False
		'
		'lblTitle
		'
		Me.lblTitle.AutoSize = True
		Me.lblTitle.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.lblTitle.Location = New System.Drawing.Point(66, 18)
		Me.lblTitle.Margin = New System.Windows.Forms.Padding(3, 9, 3, 3)
		Me.lblTitle.Name = "lblTitle"
		Me.lblTitle.Size = New System.Drawing.Size(137, 16)
		Me.lblTitle.TabIndex = 70
		Me.lblTitle.Text = "Resource Duplication"
		'
		'cmdTarget
		'
		Me.cmdTarget.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.cmdTarget.Location = New System.Drawing.Point(125, 335)
		Me.cmdTarget.Name = "cmdTarget"
		Me.cmdTarget.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.cmdTarget.Size = New System.Drawing.Size(125, 25)
		Me.cmdTarget.TabIndex = 77
		Me.cmdTarget.Text = "Target"
		Me.cmdTarget.UseVisualStyleBackColor = True
		'
		'cmdSource
		'
		Me.cmdSource.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.cmdSource.Location = New System.Drawing.Point(125, 304)
		Me.cmdSource.Margin = New System.Windows.Forms.Padding(3, 6, 3, 3)
		Me.cmdSource.Name = "cmdSource"
		Me.cmdSource.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.cmdSource.Size = New System.Drawing.Size(125, 25)
		Me.cmdSource.TabIndex = 78
		Me.cmdSource.Text = "Source"
		Me.cmdSource.UseVisualStyleBackColor = True
		'
		'picIconOverlay
		'
		Me.picIconOverlay.BackColor = System.Drawing.Color.Transparent
		Me.picIconOverlay.Location = New System.Drawing.Point(44, 44)
		Me.picIconOverlay.Name = "picIconOverlay"
		Me.picIconOverlay.Size = New System.Drawing.Size(16, 16)
		Me.picIconOverlay.TabIndex = 81
		Me.picIconOverlay.TabStop = False
		'
		'Label1
		'
		Me.Label1.Location = New System.Drawing.Point(256, 335)
		Me.Label1.Margin = New System.Windows.Forms.Padding(3)
		Me.Label1.Name = "Label1"
		Me.Label1.Size = New System.Drawing.Size(134, 25)
		Me.Label1.TabIndex = 83
		Me.Label1.Text = "(Recommended)"
		Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
		'
		'Label3
		'
		Me.Label3.Location = New System.Drawing.Point(12, 304)
		Me.Label3.Margin = New System.Windows.Forms.Padding(3)
		Me.Label3.Name = "Label3"
		Me.Label3.Size = New System.Drawing.Size(107, 56)
		Me.Label3.TabIndex = 84
		Me.Label3.Text = "Choose To Keep :"
		Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
		'
		'lstResources
		'
		Me.lstResources.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
				  Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.lstResources.Location = New System.Drawing.Point(12, 72)
		Me.lstResources.Margin = New System.Windows.Forms.Padding(3, 9, 3, 3)
		Me.lstResources.Name = "lstResources"
		Me.lstResources.OwnerDraw = True
		Me.lstResources.Size = New System.Drawing.Size(570, 223)
		Me.lstResources.TabIndex = 82
		Me.lstResources.UseCompatibleStateImageBehavior = False
		'
		'frmResourceDuplicationSelection
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(594, 372)
		Me.Controls.Add(Me.Label3)
		Me.Controls.Add(Me.Label1)
		Me.Controls.Add(Me.lstResources)
		Me.Controls.Add(Me.picIconOverlay)
		Me.Controls.Add(Me.cmdSource)
		Me.Controls.Add(Me.cmdTarget)
		Me.Controls.Add(Me.Label2)
		Me.Controls.Add(Me.cmdCancel)
		Me.Controls.Add(Me.lnkHelp)
		Me.Controls.Add(Me.picIcon)
		Me.Controls.Add(Me.lblTitle)
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
		Me.MaximizeBox = False
		Me.MinimizeBox = False
		Me.Name = "frmResourceDuplicationSelection"
		Me.ShowIcon = False
		Me.ShowInTaskbar = False
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
		Me.Text = " Resource Duplication Selection"
		CType(Me.picIcon, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.picIconOverlay, System.ComponentModel.ISupportInitialize).EndInit()
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub
	Friend WithEvents Label2 As System.Windows.Forms.Label
	Friend WithEvents cmdCancel As System.Windows.Forms.Button
	Friend WithEvents lnkHelp As System.Windows.Forms.LinkLabel
	Friend WithEvents picIcon As System.Windows.Forms.PictureBox
	Friend WithEvents lblTitle As System.Windows.Forms.Label
	Friend WithEvents cmdTarget As System.Windows.Forms.Button
	Friend WithEvents cmdSource As System.Windows.Forms.Button
	Friend WithEvents picIconOverlay As System.Windows.Forms.PictureBox
	Friend WithEvents lstResources As ServerSystemChecker.ctrlListView_SubIcons
	Friend WithEvents Label1 As System.Windows.Forms.Label
	Friend WithEvents Label3 As System.Windows.Forms.Label
End Class
