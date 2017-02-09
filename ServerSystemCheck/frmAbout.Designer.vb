<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAbout
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
		Me.lblSubTitle = New System.Windows.Forms.Label()
		Me.lblTitle = New System.Windows.Forms.Label()
		Me.Label1 = New System.Windows.Forms.Label()
		Me.Label2 = New System.Windows.Forms.Label()
		Me.Label4 = New System.Windows.Forms.Label()
		Me.lnkLabel_1 = New System.Windows.Forms.LinkLabel()
		Me.cmdCancel = New System.Windows.Forms.Button()
		Me.lnkLabel_2 = New System.Windows.Forms.LinkLabel()
		Me.lnkLabel_3 = New System.Windows.Forms.LinkLabel()
		Me.picBackground = New System.Windows.Forms.PictureBox()
		Me.lblVersion = New System.Windows.Forms.Label()
		Me.Label5 = New System.Windows.Forms.Label()
		Me.lnkEmail = New System.Windows.Forms.LinkLabel()
		CType(Me.picIcon, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.picBackground, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.SuspendLayout()
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
		Me.lblSubTitle.Size = New System.Drawing.Size(251, 13)
		Me.lblSubTitle.TabIndex = 27
		Me.lblSubTitle.Text = "System health checker for local and remove servers"
		'
		'lblTitle
		'
		Me.lblTitle.AutoSize = True
		Me.lblTitle.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.lblTitle.Location = New System.Drawing.Point(66, 18)
		Me.lblTitle.Margin = New System.Windows.Forms.Padding(3, 9, 3, 3)
		Me.lblTitle.Name = "lblTitle"
		Me.lblTitle.Size = New System.Drawing.Size(156, 16)
		Me.lblTitle.TabIndex = 26
		Me.lblTitle.Text = "application.productname"
		'
		'Label1
		'
		Me.Label1.AutoSize = True
		Me.Label1.Location = New System.Drawing.Point(12, 75)
		Me.Label1.Margin = New System.Windows.Forms.Padding(3, 12, 3, 3)
		Me.Label1.Name = "Label1"
		Me.Label1.Size = New System.Drawing.Size(65, 13)
		Me.Label1.TabIndex = 44
		Me.Label1.Text = "Created By :"
		'
		'Label2
		'
		Me.Label2.AutoSize = True
		Me.Label2.Location = New System.Drawing.Point(100, 75)
		Me.Label2.Margin = New System.Windows.Forms.Padding(3, 12, 3, 3)
		Me.Label2.Name = "Label2"
		Me.Label2.Size = New System.Drawing.Size(72, 13)
		Me.Label2.TabIndex = 45
		Me.Label2.Text = "Mike Blackett"
		'
		'Label4
		'
		Me.Label4.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.Label4.AutoSize = True
		Me.Label4.Location = New System.Drawing.Point(12, 165)
		Me.Label4.Margin = New System.Windows.Forms.Padding(3, 12, 3, 3)
		Me.Label4.Name = "Label4"
		Me.Label4.Size = New System.Drawing.Size(64, 13)
		Me.Label4.TabIndex = 49
		Me.Label4.Text = "Resources :"
		'
		'lnkLabel_1
		'
		Me.lnkLabel_1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.lnkLabel_1.AutoSize = True
		Me.lnkLabel_1.Location = New System.Drawing.Point(100, 181)
		Me.lnkLabel_1.Margin = New System.Windows.Forms.Padding(3, 0, 3, 3)
		Me.lnkLabel_1.Name = "lnkLabel_1"
		Me.lnkLabel_1.Size = New System.Drawing.Size(87, 13)
		Me.lnkLabel_1.TabIndex = 51
		Me.lnkLabel_1.TabStop = True
		Me.lnkLabel_1.Tag = "http://www.iconarchive.com"
		Me.lnkLabel_1.Text = "IconArchive.com"
		'
		'cmdCancel
		'
		Me.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
		Me.cmdCancel.Location = New System.Drawing.Point(357, 12)
		Me.cmdCancel.Name = "cmdCancel"
		Me.cmdCancel.Size = New System.Drawing.Size(125, 25)
		Me.cmdCancel.TabIndex = 54
		Me.cmdCancel.Text = "Cancel (hidden)"
		Me.cmdCancel.UseVisualStyleBackColor = True
		Me.cmdCancel.Visible = False
		'
		'lnkLabel_2
		'
		Me.lnkLabel_2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.lnkLabel_2.AutoSize = True
		Me.lnkLabel_2.Location = New System.Drawing.Point(100, 197)
		Me.lnkLabel_2.Margin = New System.Windows.Forms.Padding(3, 0, 3, 3)
		Me.lnkLabel_2.Name = "lnkLabel_2"
		Me.lnkLabel_2.Size = New System.Drawing.Size(116, 13)
		Me.lnkLabel_2.TabIndex = 55
		Me.lnkLabel_2.TabStop = True
		Me.lnkLabel_2.Tag = "http://www.microsoft.com/en-us/download/details.aspx?id=35825"
		Me.lnkLabel_2.Text = "Microsoft Image Library"
		'
		'lnkLabel_3
		'
		Me.lnkLabel_3.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.lnkLabel_3.AutoSize = True
		Me.lnkLabel_3.Location = New System.Drawing.Point(100, 165)
		Me.lnkLabel_3.Margin = New System.Windows.Forms.Padding(3, 0, 3, 3)
		Me.lnkLabel_3.Name = "lnkLabel_3"
		Me.lnkLabel_3.Size = New System.Drawing.Size(125, 13)
		Me.lnkLabel_3.TabIndex = 57
		Me.lnkLabel_3.TabStop = True
		Me.lnkLabel_3.Tag = "http://greenfishsoftware.blogspot.hu/2012/07/greenfish-icon-editor-pro.html"
		Me.lnkLabel_3.Text = "Greenfish Icon Editor Pro"
		'
		'picBackground
		'
		Me.picBackground.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.picBackground.Location = New System.Drawing.Point(303, 31)
		Me.picBackground.Name = "picBackground"
		Me.picBackground.Size = New System.Drawing.Size(192, 192)
		Me.picBackground.TabIndex = 58
		Me.picBackground.TabStop = False
		'
		'lblVersion
		'
		Me.lblVersion.AutoSize = True
		Me.lblVersion.Location = New System.Drawing.Point(100, 122)
		Me.lblVersion.Margin = New System.Windows.Forms.Padding(3, 12, 3, 3)
		Me.lblVersion.Name = "lblVersion"
		Me.lblVersion.Size = New System.Drawing.Size(62, 13)
		Me.lblVersion.TabIndex = 60
		Me.lblVersion.Text = "app.version"
		'
		'Label5
		'
		Me.Label5.AutoSize = True
		Me.Label5.Location = New System.Drawing.Point(12, 122)
		Me.Label5.Margin = New System.Windows.Forms.Padding(3, 12, 3, 3)
		Me.Label5.Name = "Label5"
		Me.Label5.Size = New System.Drawing.Size(48, 13)
		Me.Label5.TabIndex = 59
		Me.Label5.Text = "Version :"
		'
		'lnkEmail
		'
		Me.lnkEmail.AutoSize = True
		Me.lnkEmail.Location = New System.Drawing.Point(100, 94)
		Me.lnkEmail.Margin = New System.Windows.Forms.Padding(3)
		Me.lnkEmail.Name = "lnkEmail"
		Me.lnkEmail.Size = New System.Drawing.Size(172, 13)
		Me.lnkEmail.TabIndex = 61
		Me.lnkEmail.TabStop = True
		Me.lnkEmail.Text = "support@myrandomthoughts.co.uk"
		'
		'frmAbout
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.CancelButton = Me.cmdCancel
		Me.ClientSize = New System.Drawing.Size(494, 222)
		Me.Controls.Add(Me.lnkEmail)
		Me.Controls.Add(Me.Label5)
		Me.Controls.Add(Me.lnkLabel_3)
		Me.Controls.Add(Me.lnkLabel_2)
		Me.Controls.Add(Me.lblSubTitle)
		Me.Controls.Add(Me.lblTitle)
		Me.Controls.Add(Me.cmdCancel)
		Me.Controls.Add(Me.lnkLabel_1)
		Me.Controls.Add(Me.Label4)
		Me.Controls.Add(Me.Label2)
		Me.Controls.Add(Me.Label1)
		Me.Controls.Add(Me.picIcon)
		Me.Controls.Add(Me.lblVersion)
		Me.Controls.Add(Me.picBackground)
		Me.DoubleBuffered = True
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
		Me.MaximizeBox = False
		Me.MinimizeBox = False
		Me.Name = "frmAbout"
		Me.ShowIcon = False
		Me.ShowInTaskbar = False
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
		Me.Text = " About Me"
		CType(Me.picIcon, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.picBackground, System.ComponentModel.ISupportInitialize).EndInit()
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub
	Friend WithEvents picIcon As System.Windows.Forms.PictureBox
	Friend WithEvents lblSubTitle As System.Windows.Forms.Label
	Friend WithEvents lblTitle As System.Windows.Forms.Label
	Friend WithEvents Label1 As System.Windows.Forms.Label
	Friend WithEvents Label2 As System.Windows.Forms.Label
	Friend WithEvents Label4 As System.Windows.Forms.Label
	Friend WithEvents lnkLabel_1 As System.Windows.Forms.LinkLabel
	Friend WithEvents cmdCancel As System.Windows.Forms.Button
	Friend WithEvents lnkLabel_2 As System.Windows.Forms.LinkLabel
	Friend WithEvents lnkLabel_3 As System.Windows.Forms.LinkLabel
	Friend WithEvents picBackground As System.Windows.Forms.PictureBox
	Friend WithEvents lblVersion As System.Windows.Forms.Label
	Friend WithEvents Label5 As System.Windows.Forms.Label
	Friend WithEvents lnkEmail As System.Windows.Forms.LinkLabel
End Class
