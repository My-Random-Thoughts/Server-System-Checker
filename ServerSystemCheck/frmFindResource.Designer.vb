<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmFindResource
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
		Me.lblTitle = New System.Windows.Forms.Label()
		Me.txtSearchFor = New System.Windows.Forms.TextBox()
		Me.cmdSearch = New System.Windows.Forms.Button()
		Me.cmdCancel = New System.Windows.Forms.Button()
		Me.lblResults = New System.Windows.Forms.Label()
		Me.picIcon = New System.Windows.Forms.PictureBox()
		Me.lstResults = New ServerSystemChecker.ctrlListView_SubIcons()
		Me.cmoSearchType = New ServerSystemChecker.ctrlComboBox_Icons()
		CType(Me.picIcon, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.SuspendLayout()
		'
		'lblTitle
		'
		Me.lblTitle.AutoSize = True
		Me.lblTitle.Location = New System.Drawing.Point(66, 12)
		Me.lblTitle.Margin = New System.Windows.Forms.Padding(3)
		Me.lblTitle.Name = "lblTitle"
		Me.lblTitle.Size = New System.Drawing.Size(222, 13)
		Me.lblTitle.TabIndex = 12
		Me.lblTitle.Text = "Enter all or part of a resource name or value..."
		'
		'txtSearchFor
		'
		Me.txtSearchFor.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
				  Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.txtSearchFor.Location = New System.Drawing.Point(123, 35)
		Me.txtSearchFor.Margin = New System.Windows.Forms.Padding(3, 6, 3, 3)
		Me.txtSearchFor.Name = "txtSearchFor"
		Me.txtSearchFor.Size = New System.Drawing.Size(409, 20)
		Me.txtSearchFor.TabIndex = 1
		'
		'cmdSearch
		'
		Me.cmdSearch.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.cmdSearch.Location = New System.Drawing.Point(457, 74)
		Me.cmdSearch.Margin = New System.Windows.Forms.Padding(3, 12, 3, 3)
		Me.cmdSearch.Name = "cmdSearch"
		Me.cmdSearch.Size = New System.Drawing.Size(75, 25)
		Me.cmdSearch.TabIndex = 2
		Me.cmdSearch.Text = "Search"
		Me.cmdSearch.UseVisualStyleBackColor = True
		'
		'cmdCancel
		'
		Me.cmdCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.cmdCancel.Location = New System.Drawing.Point(376, 74)
		Me.cmdCancel.Margin = New System.Windows.Forms.Padding(12, 3, 3, 3)
		Me.cmdCancel.Name = "cmdCancel"
		Me.cmdCancel.Size = New System.Drawing.Size(75, 25)
		Me.cmdCancel.TabIndex = 3
		Me.cmdCancel.Text = "Cancel"
		Me.cmdCancel.UseVisualStyleBackColor = True
		'
		'lblResults
		'
		Me.lblResults.AutoSize = True
		Me.lblResults.Location = New System.Drawing.Point(12, 86)
		Me.lblResults.Margin = New System.Windows.Forms.Padding(3)
		Me.lblResults.Name = "lblResults"
		Me.lblResults.Size = New System.Drawing.Size(46, 13)
		Me.lblResults.TabIndex = 13
		Me.lblResults.Text = "0 results"
		'
		'picIcon
		'
		Me.picIcon.Location = New System.Drawing.Point(12, 12)
		Me.picIcon.Name = "picIcon"
		Me.picIcon.Size = New System.Drawing.Size(48, 48)
		Me.picIcon.TabIndex = 15
		Me.picIcon.TabStop = False
		'
		'lstResults
		'
		Me.lstResults.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
				  Or System.Windows.Forms.AnchorStyles.Left) _
				  Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.lstResults.Location = New System.Drawing.Point(12, 114)
		Me.lstResults.Margin = New System.Windows.Forms.Padding(3, 12, 3, 3)
		Me.lstResults.Name = "lstResults"
		Me.lstResults.OwnerDraw = True
		Me.lstResults.Size = New System.Drawing.Size(520, 200)
		Me.lstResults.TabIndex = 4
		Me.lstResults.UseCompatibleStateImageBehavior = False
		'
		'cmoSearchType
		'
		Me.cmoSearchType.BackColor = System.Drawing.SystemColors.Window
		Me.cmoSearchType.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
		Me.cmoSearchType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.cmoSearchType.FormattingEnabled = True
		Me.cmoSearchType.ItemHeight = 19
		Me.cmoSearchType.Location = New System.Drawing.Point(69, 35)
		Me.cmoSearchType.Name = "cmoSearchType"
		Me.cmoSearchType.RemoveIconSpacing = False
		Me.cmoSearchType.SelectedItem = Nothing
		Me.cmoSearchType.Size = New System.Drawing.Size(48, 25)
		Me.cmoSearchType.TabIndex = 0
		'
		'frmFindResource
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(544, 326)
		Me.Controls.Add(Me.picIcon)
		Me.Controls.Add(Me.lblResults)
		Me.Controls.Add(Me.cmdCancel)
		Me.Controls.Add(Me.cmdSearch)
		Me.Controls.Add(Me.lstResults)
		Me.Controls.Add(Me.cmoSearchType)
		Me.Controls.Add(Me.txtSearchFor)
		Me.Controls.Add(Me.lblTitle)
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
		Me.MaximizeBox = False
		Me.MinimizeBox = False
		Me.Name = "frmFindResource"
		Me.ShowIcon = False
		Me.ShowInTaskbar = False
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
		Me.Text = " Find Resource..."
		CType(Me.picIcon, System.ComponentModel.ISupportInitialize).EndInit()
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub
	Friend WithEvents lblTitle As System.Windows.Forms.Label
	Friend WithEvents txtSearchFor As System.Windows.Forms.TextBox
	Friend WithEvents cmoSearchType As ServerSystemChecker.ctrlComboBox_Icons
	Friend WithEvents lstResults As ServerSystemChecker.ctrlListView_SubIcons
	Friend WithEvents cmdSearch As System.Windows.Forms.Button
	Friend WithEvents cmdCancel As System.Windows.Forms.Button
	Friend WithEvents lblResults As System.Windows.Forms.Label
	Friend WithEvents picIcon As System.Windows.Forms.PictureBox
End Class
