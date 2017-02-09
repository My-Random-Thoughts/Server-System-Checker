<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmFindServer
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
		Me.cmoFind = New System.Windows.Forms.ComboBox()
		Me.cmdCancel = New System.Windows.Forms.Button()
		Me.cmdFind = New System.Windows.Forms.Button()
		Me.lblTitle = New System.Windows.Forms.Label()
		Me.lblResultsCount_TR = New System.Windows.Forms.Label()
		Me.lblResultsCount_BL = New System.Windows.Forms.Label()
		Me.picPlaceHolder = New System.Windows.Forms.PictureBox()
		Me.picIcon = New System.Windows.Forms.PictureBox()
		CType(Me.picPlaceHolder, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.picIcon, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.SuspendLayout()
		'
		'cmoFind
		'
		Me.cmoFind.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
				  Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.cmoFind.FormattingEnabled = True
		Me.cmoFind.Location = New System.Drawing.Point(69, 39)
		Me.cmoFind.Margin = New System.Windows.Forms.Padding(3, 6, 3, 3)
		Me.cmoFind.Name = "cmoFind"
		Me.cmoFind.Size = New System.Drawing.Size(313, 21)
		Me.cmoFind.TabIndex = 0
		'
		'cmdCancel
		'
		Me.cmdCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
		Me.cmdCancel.Location = New System.Drawing.Point(226, 74)
		Me.cmdCancel.Margin = New System.Windows.Forms.Padding(3, 12, 3, 3)
		Me.cmdCancel.Name = "cmdCancel"
		Me.cmdCancel.Size = New System.Drawing.Size(75, 25)
		Me.cmdCancel.TabIndex = 1
		Me.cmdCancel.Text = "Cancel"
		Me.cmdCancel.UseVisualStyleBackColor = True
		'
		'cmdFind
		'
		Me.cmdFind.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.cmdFind.DialogResult = System.Windows.Forms.DialogResult.Cancel
		Me.cmdFind.Location = New System.Drawing.Point(307, 74)
		Me.cmdFind.Margin = New System.Windows.Forms.Padding(3, 12, 3, 3)
		Me.cmdFind.Name = "cmdFind"
		Me.cmdFind.Size = New System.Drawing.Size(75, 25)
		Me.cmdFind.TabIndex = 2
		Me.cmdFind.Text = "Search"
		Me.cmdFind.UseVisualStyleBackColor = True
		'
		'lblTitle
		'
		Me.lblTitle.AutoSize = True
		Me.lblTitle.Location = New System.Drawing.Point(66, 12)
		Me.lblTitle.Margin = New System.Windows.Forms.Padding(3)
		Me.lblTitle.Name = "lblTitle"
		Me.lblTitle.Size = New System.Drawing.Size(169, 13)
		Me.lblTitle.TabIndex = 10
		Me.lblTitle.Text = "Enter all or part of a server name..."
		'
		'lblResultsCount_TR
		'
		Me.lblResultsCount_TR.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.lblResultsCount_TR.Location = New System.Drawing.Point(307, 12)
		Me.lblResultsCount_TR.Margin = New System.Windows.Forms.Padding(3)
		Me.lblResultsCount_TR.Name = "lblResultsCount_TR"
		Me.lblResultsCount_TR.Size = New System.Drawing.Size(75, 16)
		Me.lblResultsCount_TR.TabIndex = 11
		Me.lblResultsCount_TR.Text = "0 results"
		Me.lblResultsCount_TR.TextAlign = System.Drawing.ContentAlignment.TopRight
		'
		'lblResultsCount_BL
		'
		Me.lblResultsCount_BL.AutoSize = True
		Me.lblResultsCount_BL.Location = New System.Drawing.Point(12, 86)
		Me.lblResultsCount_BL.Margin = New System.Windows.Forms.Padding(3)
		Me.lblResultsCount_BL.Name = "lblResultsCount_BL"
		Me.lblResultsCount_BL.Size = New System.Drawing.Size(46, 13)
		Me.lblResultsCount_BL.TabIndex = 12
		Me.lblResultsCount_BL.Text = "0 results"
		Me.lblResultsCount_BL.TextAlign = System.Drawing.ContentAlignment.TopRight
		'
		'picPlaceHolder
		'
		Me.picPlaceHolder.Location = New System.Drawing.Point(12, 74)
		Me.picPlaceHolder.Name = "picPlaceHolder"
		Me.picPlaceHolder.Size = New System.Drawing.Size(125, 25)
		Me.picPlaceHolder.TabIndex = 13
		Me.picPlaceHolder.TabStop = False
		'
		'picIcon
		'
		Me.picIcon.Location = New System.Drawing.Point(12, 12)
		Me.picIcon.Name = "picIcon"
		Me.picIcon.Size = New System.Drawing.Size(48, 48)
		Me.picIcon.TabIndex = 14
		Me.picIcon.TabStop = False
		'
		'frmFindServer
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.CancelButton = Me.cmdCancel
		Me.ClientSize = New System.Drawing.Size(394, 111)
		Me.Controls.Add(Me.lblResultsCount_BL)
		Me.Controls.Add(Me.lblResultsCount_TR)
		Me.Controls.Add(Me.lblTitle)
		Me.Controls.Add(Me.cmdFind)
		Me.Controls.Add(Me.cmdCancel)
		Me.Controls.Add(Me.cmoFind)
		Me.Controls.Add(Me.picPlaceHolder)
		Me.Controls.Add(Me.picIcon)
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
		Me.Name = "frmFindServer"
		Me.ShowIcon = False
		Me.ShowInTaskbar = False
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
		Me.Text = " Find Server..."
		CType(Me.picPlaceHolder, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.picIcon, System.ComponentModel.ISupportInitialize).EndInit()
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub
	Friend WithEvents cmoFind As System.Windows.Forms.ComboBox
	Friend WithEvents cmdCancel As System.Windows.Forms.Button
	Friend WithEvents cmdFind As System.Windows.Forms.Button
	Friend WithEvents lblTitle As System.Windows.Forms.Label
	Friend WithEvents lblResultsCount_TR As System.Windows.Forms.Label
	Friend WithEvents lblResultsCount_BL As System.Windows.Forms.Label
	Friend WithEvents picPlaceHolder As System.Windows.Forms.PictureBox
	Friend WithEvents picIcon As System.Windows.Forms.PictureBox
End Class
