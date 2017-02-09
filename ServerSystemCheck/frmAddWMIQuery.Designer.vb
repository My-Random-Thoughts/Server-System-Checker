<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAddWMIQuery
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
		Me.cmdCancel = New System.Windows.Forms.Button()
		Me.cmdOK = New System.Windows.Forms.Button()
		Me.Label2 = New System.Windows.Forms.Label()
		Me.lblTitle = New System.Windows.Forms.Label()
		Me.Label8 = New System.Windows.Forms.Label()
		Me.Label7 = New System.Windows.Forms.Label()
		Me.cmoClasses = New System.Windows.Forms.ComboBox()
		Me.Label1 = New System.Windows.Forms.Label()
		Me.cmdConnect = New System.Windows.Forms.Button()
		Me.cmdTestQuery = New System.Windows.Forms.Button()
		Me.Label4 = New System.Windows.Forms.Label()
		Me.chkCountOnly = New System.Windows.Forms.CheckBox()
		Me.floProperties = New System.Windows.Forms.FlowLayoutPanel()
		Me.lblQ_Select = New System.Windows.Forms.Label()
		Me.lblQ_From = New System.Windows.Forms.Label()
		Me.lblClass = New System.Windows.Forms.Label()
		Me.lblQ_Where = New System.Windows.Forms.Label()
		Me.lstSelectContainer = New System.Windows.Forms.ListView()
		Me.lblPropertiesCount = New System.Windows.Forms.Label()
		Me.cmoWhereAndOr = New System.Windows.Forms.ComboBox()
		Me.GroupBox1 = New System.Windows.Forms.GroupBox()
		Me.lblQueryResult = New System.Windows.Forms.Label()
		Me.Label5 = New System.Windows.Forms.Label()
		Me.GroupBox2 = New System.Windows.Forms.GroupBox()
		Me.lnkExpandResults = New System.Windows.Forms.LinkLabel()
		Me.Label3 = New System.Windows.Forms.Label()
		Me.cmdShowQuery = New System.Windows.Forms.Button()
		Me.chkAutoSortSelect = New System.Windows.Forms.CheckBox()
		Me.cmoQueryResult = New ServerSystemChecker.ctrlComboBox_Icons()
		Me.lstWhereList = New ServerSystemChecker.ctrlListView_SubIcons()
		Me.cmoIconComboBox = New ServerSystemChecker.ctrlComboBox_Icons()
		Me.lnkNote = New System.Windows.Forms.LinkLabel()
		CType(Me.picIcon, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.SuspendLayout()
		'
		'lnkHelp
		'
		Me.lnkHelp.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.lnkHelp.Location = New System.Drawing.Point(460, 9)
		Me.lnkHelp.Margin = New System.Windows.Forms.Padding(0)
		Me.lnkHelp.Name = "lnkHelp"
		Me.lnkHelp.Size = New System.Drawing.Size(75, 15)
		Me.lnkHelp.TabIndex = 57
		Me.lnkHelp.TabStop = True
		Me.lnkHelp.Text = "Help"
		Me.lnkHelp.TextAlign = System.Drawing.ContentAlignment.TopRight
		'
		'picIcon
		'
		Me.picIcon.Location = New System.Drawing.Point(12, 12)
		Me.picIcon.Name = "picIcon"
		Me.picIcon.Size = New System.Drawing.Size(48, 48)
		Me.picIcon.TabIndex = 56
		Me.picIcon.TabStop = False
		'
		'cmdCancel
		'
		Me.cmdCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
		Me.cmdCancel.Location = New System.Drawing.Point(376, 460)
		Me.cmdCancel.Margin = New System.Windows.Forms.Padding(3, 12, 3, 3)
		Me.cmdCancel.Name = "cmdCancel"
		Me.cmdCancel.Size = New System.Drawing.Size(75, 25)
		Me.cmdCancel.TabIndex = 10
		Me.cmdCancel.Text = "Cancel"
		Me.cmdCancel.UseVisualStyleBackColor = True
		'
		'cmdOK
		'
		Me.cmdOK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.cmdOK.Location = New System.Drawing.Point(457, 460)
		Me.cmdOK.Margin = New System.Windows.Forms.Padding(3, 12, 3, 3)
		Me.cmdOK.Name = "cmdOK"
		Me.cmdOK.Size = New System.Drawing.Size(75, 25)
		Me.cmdOK.TabIndex = 9
		Me.cmdOK.Text = "OK"
		Me.cmdOK.UseVisualStyleBackColor = True
		'
		'Label2
		'
		Me.Label2.AutoSize = True
		Me.Label2.Location = New System.Drawing.Point(66, 40)
		Me.Label2.Margin = New System.Windows.Forms.Padding(3)
		Me.Label2.Name = "Label2"
		Me.Label2.Size = New System.Drawing.Size(245, 13)
		Me.Label2.TabIndex = 53
		Me.Label2.Text = "Use a WMI query to interrogate server properties..."
		'
		'lblTitle
		'
		Me.lblTitle.AutoSize = True
		Me.lblTitle.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.lblTitle.Location = New System.Drawing.Point(66, 18)
		Me.lblTitle.Margin = New System.Windows.Forms.Padding(3, 9, 3, 3)
		Me.lblTitle.Name = "lblTitle"
		Me.lblTitle.Size = New System.Drawing.Size(132, 16)
		Me.lblTitle.TabIndex = 52
		Me.lblTitle.Text = "Add New WMI Query"
		'
		'Label8
		'
		Me.Label8.AutoSize = True
		Me.Label8.Location = New System.Drawing.Point(12, 160)
		Me.Label8.Margin = New System.Windows.Forms.Padding(3, 6, 3, 9)
		Me.Label8.Name = "Label8"
		Me.Label8.Size = New System.Drawing.Size(102, 13)
		Me.Label8.TabIndex = 59
		Me.Label8.Text = "WMI Query Builder :"
		'
		'Label7
		'
		Me.Label7.AutoSize = True
		Me.Label7.Location = New System.Drawing.Point(12, 75)
		Me.Label7.Margin = New System.Windows.Forms.Padding(3, 12, 3, 9)
		Me.Label7.Name = "Label7"
		Me.Label7.Size = New System.Drawing.Size(69, 13)
		Me.Label7.TabIndex = 62
		Me.Label7.Text = "Connect To :"
		'
		'cmoClasses
		'
		Me.cmoClasses.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
				  Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.cmoClasses.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.cmoClasses.FormattingEnabled = True
		Me.cmoClasses.ItemHeight = 13
		Me.cmoClasses.Location = New System.Drawing.Point(182, 106)
		Me.cmoClasses.Name = "cmoClasses"
		Me.cmoClasses.Size = New System.Drawing.Size(350, 21)
		Me.cmoClasses.TabIndex = 2
		'
		'Label1
		'
		Me.Label1.AutoSize = True
		Me.Label1.Location = New System.Drawing.Point(12, 109)
		Me.Label1.Margin = New System.Windows.Forms.Padding(3)
		Me.Label1.Name = "Label1"
		Me.Label1.Size = New System.Drawing.Size(49, 13)
		Me.Label1.TabIndex = 66
		Me.Label1.Text = "Classes :"
		'
		'cmdConnect
		'
		Me.cmdConnect.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.cmdConnect.Location = New System.Drawing.Point(457, 72)
		Me.cmdConnect.Margin = New System.Windows.Forms.Padding(3, 6, 3, 6)
		Me.cmdConnect.Name = "cmdConnect"
		Me.cmdConnect.Size = New System.Drawing.Size(75, 25)
		Me.cmdConnect.TabIndex = 1
		Me.cmdConnect.Text = "Connect..."
		Me.cmdConnect.UseVisualStyleBackColor = True
		'
		'cmdTestQuery
		'
		Me.cmdTestQuery.Location = New System.Drawing.Point(100, 401)
		Me.cmdTestQuery.Margin = New System.Windows.Forms.Padding(3, 12, 3, 3)
		Me.cmdTestQuery.Name = "cmdTestQuery"
		Me.cmdTestQuery.Size = New System.Drawing.Size(125, 25)
		Me.cmdTestQuery.TabIndex = 7
		Me.cmdTestQuery.Text = "Test Query"
		Me.cmdTestQuery.UseVisualStyleBackColor = True
		'
		'Label4
		'
		Me.Label4.AutoSize = True
		Me.Label4.Location = New System.Drawing.Point(12, 364)
		Me.Label4.Margin = New System.Windows.Forms.Padding(3, 12, 3, 9)
		Me.Label4.Name = "Label4"
		Me.Label4.Size = New System.Drawing.Size(48, 13)
		Me.Label4.TabIndex = 70
		Me.Label4.Text = "Results :"
		'
		'chkCountOnly
		'
		Me.chkCountOnly.AutoSize = True
		Me.chkCountOnly.Location = New System.Drawing.Point(100, 363)
		Me.chkCountOnly.Margin = New System.Windows.Forms.Padding(3, 6, 3, 3)
		Me.chkCountOnly.Name = "chkCountOnly"
		Me.chkCountOnly.Size = New System.Drawing.Size(113, 17)
		Me.chkCountOnly.TabIndex = 5
		Me.chkCountOnly.Text = "Return Count Only"
		Me.chkCountOnly.UseVisualStyleBackColor = True
		'
		'floProperties
		'
		Me.floProperties.BackColor = System.Drawing.SystemColors.Window
		Me.floProperties.Location = New System.Drawing.Point(106, 191)
		Me.floProperties.Margin = New System.Windows.Forms.Padding(0)
		Me.floProperties.Name = "floProperties"
		Me.floProperties.Size = New System.Drawing.Size(106, 53)
		Me.floProperties.TabIndex = 3
		'
		'lblQ_Select
		'
		Me.lblQ_Select.AutoSize = True
		Me.lblQ_Select.Location = New System.Drawing.Point(12, 185)
		Me.lblQ_Select.Margin = New System.Windows.Forms.Padding(3, 3, 3, 0)
		Me.lblQ_Select.Name = "lblQ_Select"
		Me.lblQ_Select.Padding = New System.Windows.Forms.Padding(18, 3, 0, 0)
		Me.lblQ_Select.Size = New System.Drawing.Size(66, 16)
		Me.lblQ_Select.TabIndex = 79
		Me.lblQ_Select.Text = "SELECT"
		'
		'lblQ_From
		'
		Me.lblQ_From.AutoSize = True
		Me.lblQ_From.Location = New System.Drawing.Point(12, 259)
		Me.lblQ_From.Margin = New System.Windows.Forms.Padding(3, 3, 3, 0)
		Me.lblQ_From.Name = "lblQ_From"
		Me.lblQ_From.Padding = New System.Windows.Forms.Padding(18, 0, 0, 0)
		Me.lblQ_From.Size = New System.Drawing.Size(56, 13)
		Me.lblQ_From.TabIndex = 80
		Me.lblQ_From.Text = "FROM"
		'
		'lblClass
		'
		Me.lblClass.Location = New System.Drawing.Point(100, 256)
		Me.lblClass.Margin = New System.Windows.Forms.Padding(3)
		Me.lblClass.Name = "lblClass"
		Me.lblClass.Size = New System.Drawing.Size(382, 19)
		Me.lblClass.TabIndex = 81
		Me.lblClass.Text = "(Select from 'Classes' above)"
		Me.lblClass.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
		'
		'lblQ_Where
		'
		Me.lblQ_Where.AutoSize = True
		Me.lblQ_Where.Location = New System.Drawing.Point(12, 281)
		Me.lblQ_Where.Margin = New System.Windows.Forms.Padding(3, 3, 3, 0)
		Me.lblQ_Where.Name = "lblQ_Where"
		Me.lblQ_Where.Padding = New System.Windows.Forms.Padding(18, 3, 0, 0)
		Me.lblQ_Where.Size = New System.Drawing.Size(66, 16)
		Me.lblQ_Where.TabIndex = 82
		Me.lblQ_Where.Text = "WHERE"
		'
		'lstSelectContainer
		'
		Me.lstSelectContainer.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
				  Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.lstSelectContainer.Location = New System.Drawing.Point(100, 185)
		Me.lstSelectContainer.Margin = New System.Windows.Forms.Padding(3, 0, 3, 3)
		Me.lstSelectContainer.Name = "lstSelectContainer"
		Me.lstSelectContainer.Size = New System.Drawing.Size(432, 65)
		Me.lstSelectContainer.TabIndex = 87
		Me.lstSelectContainer.UseCompatibleStateImageBehavior = False
		'
		'lblPropertiesCount
		'
		Me.lblPropertiesCount.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.lblPropertiesCount.Enabled = False
		Me.lblPropertiesCount.Location = New System.Drawing.Point(432, 131)
		Me.lblPropertiesCount.Margin = New System.Windows.Forms.Padding(3, 1, 3, 1)
		Me.lblPropertiesCount.Name = "lblPropertiesCount"
		Me.lblPropertiesCount.Size = New System.Drawing.Size(100, 15)
		Me.lblPropertiesCount.TabIndex = 88
		Me.lblPropertiesCount.Text = "- Properties"
		Me.lblPropertiesCount.TextAlign = System.Drawing.ContentAlignment.TopRight
		'
		'cmoWhereAndOr
		'
		Me.cmoWhereAndOr.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.cmoWhereAndOr.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.cmoWhereAndOr.FormattingEnabled = True
		Me.cmoWhereAndOr.Items.AddRange(New Object() {" ", "AND", "OR"})
		Me.cmoWhereAndOr.Location = New System.Drawing.Point(481, 355)
		Me.cmoWhereAndOr.Margin = New System.Windows.Forms.Padding(3, 0, 3, 3)
		Me.cmoWhereAndOr.Name = "cmoWhereAndOr"
		Me.cmoWhereAndOr.Size = New System.Drawing.Size(51, 21)
		Me.cmoWhereAndOr.TabIndex = 6
		Me.cmoWhereAndOr.TabStop = False
		'
		'GroupBox1
		'
		Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
				  Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.GroupBox1.Location = New System.Drawing.Point(12, 146)
		Me.GroupBox1.Name = "GroupBox1"
		Me.GroupBox1.Size = New System.Drawing.Size(520, 7)
		Me.GroupBox1.TabIndex = 91
		Me.GroupBox1.TabStop = False
		'
		'lblQueryResult
		'
		Me.lblQueryResult.BackColor = System.Drawing.SystemColors.Control
		Me.lblQueryResult.Location = New System.Drawing.Point(100, 407)
		Me.lblQueryResult.Margin = New System.Windows.Forms.Padding(3, 6, 3, 3)
		Me.lblQueryResult.Name = "lblQueryResult"
		Me.lblQueryResult.Size = New System.Drawing.Size(251, 38)
		Me.lblQueryResult.TabIndex = 92
		Me.lblQueryResult.Text = "[result goes here]"
		'
		'Label5
		'
		Me.Label5.AutoSize = True
		Me.Label5.Location = New System.Drawing.Point(12, 407)
		Me.Label5.Margin = New System.Windows.Forms.Padding(3, 6, 3, 3)
		Me.Label5.Name = "Label5"
		Me.Label5.Size = New System.Drawing.Size(74, 13)
		Me.Label5.TabIndex = 93
		Me.Label5.Text = "Query Result :"
		'
		'GroupBox2
		'
		Me.GroupBox2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
				  Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.GroupBox2.Location = New System.Drawing.Point(12, 387)
		Me.GroupBox2.Name = "GroupBox2"
		Me.GroupBox2.Size = New System.Drawing.Size(520, 7)
		Me.GroupBox2.TabIndex = 94
		Me.GroupBox2.TabStop = False
		'
		'lnkExpandResults
		'
		Me.lnkExpandResults.BackColor = System.Drawing.SystemColors.Control
		Me.lnkExpandResults.Location = New System.Drawing.Point(286, 407)
		Me.lnkExpandResults.Margin = New System.Windows.Forms.Padding(0)
		Me.lnkExpandResults.Name = "lnkExpandResults"
		Me.lnkExpandResults.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.lnkExpandResults.Size = New System.Drawing.Size(65, 19)
		Me.lnkExpandResults.TabIndex = 96
		Me.lnkExpandResults.TabStop = True
		Me.lnkExpandResults.Text = "...expand"
		Me.lnkExpandResults.TextAlign = System.Drawing.ContentAlignment.TopRight
		'
		'Label3
		'
		Me.Label3.AutoSize = True
		Me.Label3.Location = New System.Drawing.Point(97, 109)
		Me.Label3.Name = "Label3"
		Me.Label3.Size = New System.Drawing.Size(64, 13)
		Me.Label3.TabIndex = 98
		Me.Label3.Text = "root \ cimv2"
		'
		'cmdShowQuery
		'
		Me.cmdShowQuery.Location = New System.Drawing.Point(100, 460)
		Me.cmdShowQuery.Margin = New System.Windows.Forms.Padding(3, 12, 3, 3)
		Me.cmdShowQuery.Name = "cmdShowQuery"
		Me.cmdShowQuery.Size = New System.Drawing.Size(125, 25)
		Me.cmdShowQuery.TabIndex = 11
		Me.cmdShowQuery.Text = "Show Query"
		Me.cmdShowQuery.UseVisualStyleBackColor = True
		'
		'chkAutoSortSelect
		'
		Me.chkAutoSortSelect.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.chkAutoSortSelect.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
		Me.chkAutoSortSelect.Location = New System.Drawing.Point(332, 167)
		Me.chkAutoSortSelect.Margin = New System.Windows.Forms.Padding(3, 3, 3, 0)
		Me.chkAutoSortSelect.Name = "chkAutoSortSelect"
		Me.chkAutoSortSelect.Size = New System.Drawing.Size(200, 17)
		Me.chkAutoSortSelect.TabIndex = 99
		Me.chkAutoSortSelect.Text = "Active Sort Properties"
		Me.chkAutoSortSelect.TextAlign = System.Drawing.ContentAlignment.MiddleRight
		Me.chkAutoSortSelect.UseVisualStyleBackColor = True
		'
		'cmoQueryResult
		'
		Me.cmoQueryResult.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.cmoQueryResult.BackColor = System.Drawing.SystemColors.Window
		Me.cmoQueryResult.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
		Me.cmoQueryResult.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.cmoQueryResult.FormattingEnabled = True
		Me.cmoQueryResult.ItemHeight = 19
		Me.cmoQueryResult.Location = New System.Drawing.Point(357, 401)
		Me.cmoQueryResult.Margin = New System.Windows.Forms.Padding(3, 6, 3, 3)
		Me.cmoQueryResult.Name = "cmoQueryResult"
		Me.cmoQueryResult.RemoveIconSpacing = False
		Me.cmoQueryResult.SelectedItem = Nothing
		Me.cmoQueryResult.Size = New System.Drawing.Size(175, 25)
		Me.cmoQueryResult.TabIndex = 8
		'
		'lstWhereList
		'
		Me.lstWhereList.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
				  Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.lstWhereList.Location = New System.Drawing.Point(100, 281)
		Me.lstWhereList.Name = "lstWhereList"
		Me.lstWhereList.OwnerDraw = True
		Me.lstWhereList.Size = New System.Drawing.Size(432, 73)
		Me.lstWhereList.TabIndex = 4
		Me.lstWhereList.UseCompatibleStateImageBehavior = False
		'
		'cmoIconComboBox
		'
		Me.cmoIconComboBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
				  Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.cmoIconComboBox.BackColor = System.Drawing.SystemColors.Window
		Me.cmoIconComboBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
		Me.cmoIconComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.cmoIconComboBox.FormattingEnabled = True
		Me.cmoIconComboBox.ItemHeight = 19
		Me.cmoIconComboBox.Location = New System.Drawing.Point(100, 72)
		Me.cmoIconComboBox.Name = "cmoIconComboBox"
		Me.cmoIconComboBox.RemoveIconSpacing = False
		Me.cmoIconComboBox.SelectedItem = Nothing
		Me.cmoIconComboBox.Size = New System.Drawing.Size(350, 25)
		Me.cmoIconComboBox.TabIndex = 0
		'
		'lnkNote
		'
		Me.lnkNote.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.lnkNote.Location = New System.Drawing.Point(460, 30)
		Me.lnkNote.Margin = New System.Windows.Forms.Padding(0, 6, 0, 0)
		Me.lnkNote.Name = "lnkNote"
		Me.lnkNote.Size = New System.Drawing.Size(75, 15)
		Me.lnkNote.TabIndex = 100
		Me.lnkNote.TabStop = True
		Me.lnkNote.Text = "Examples"
		Me.lnkNote.TextAlign = System.Drawing.ContentAlignment.TopRight
		'
		'frmAddWMIQuery
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(544, 497)
		Me.Controls.Add(Me.lnkNote)
		Me.Controls.Add(Me.lblPropertiesCount)
		Me.Controls.Add(Me.chkAutoSortSelect)
		Me.Controls.Add(Me.cmdShowQuery)
		Me.Controls.Add(Me.Label3)
		Me.Controls.Add(Me.lnkExpandResults)
		Me.Controls.Add(Me.cmoQueryResult)
		Me.Controls.Add(Me.GroupBox2)
		Me.Controls.Add(Me.Label5)
		Me.Controls.Add(Me.GroupBox1)
		Me.Controls.Add(Me.lstWhereList)
		Me.Controls.Add(Me.cmoWhereAndOr)
		Me.Controls.Add(Me.lblQ_Where)
		Me.Controls.Add(Me.lblClass)
		Me.Controls.Add(Me.lblQ_From)
		Me.Controls.Add(Me.lblQ_Select)
		Me.Controls.Add(Me.floProperties)
		Me.Controls.Add(Me.chkCountOnly)
		Me.Controls.Add(Me.Label4)
		Me.Controls.Add(Me.cmdTestQuery)
		Me.Controls.Add(Me.cmdConnect)
		Me.Controls.Add(Me.Label1)
		Me.Controls.Add(Me.cmoIconComboBox)
		Me.Controls.Add(Me.cmoClasses)
		Me.Controls.Add(Me.Label7)
		Me.Controls.Add(Me.Label8)
		Me.Controls.Add(Me.lnkHelp)
		Me.Controls.Add(Me.picIcon)
		Me.Controls.Add(Me.cmdCancel)
		Me.Controls.Add(Me.cmdOK)
		Me.Controls.Add(Me.Label2)
		Me.Controls.Add(Me.lblTitle)
		Me.Controls.Add(Me.lstSelectContainer)
		Me.Controls.Add(Me.lblQueryResult)
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
		Me.MaximizeBox = False
		Me.MinimizeBox = False
		Me.Name = "frmAddWMIQuery"
		Me.ShowIcon = False
		Me.ShowInTaskbar = False
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
		Me.Text = " Add WMI Query"
		CType(Me.picIcon, System.ComponentModel.ISupportInitialize).EndInit()
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub
	Friend WithEvents lnkHelp As System.Windows.Forms.LinkLabel
	Friend WithEvents picIcon As System.Windows.Forms.PictureBox
	Friend WithEvents cmdCancel As System.Windows.Forms.Button
	Friend WithEvents cmdOK As System.Windows.Forms.Button
	Friend WithEvents Label2 As System.Windows.Forms.Label
	Friend WithEvents lblTitle As System.Windows.Forms.Label
	Friend WithEvents Label8 As System.Windows.Forms.Label
	Friend WithEvents Label7 As System.Windows.Forms.Label
	Friend WithEvents cmoClasses As System.Windows.Forms.ComboBox
	Friend WithEvents cmoIconComboBox As ServerSystemChecker.ctrlComboBox_Icons
	Friend WithEvents Label1 As System.Windows.Forms.Label
	Friend WithEvents cmdConnect As System.Windows.Forms.Button
	Friend WithEvents cmdTestQuery As System.Windows.Forms.Button
	Friend WithEvents Label4 As System.Windows.Forms.Label
	Friend WithEvents chkCountOnly As System.Windows.Forms.CheckBox
	Friend WithEvents floProperties As System.Windows.Forms.FlowLayoutPanel
	Friend WithEvents lblQ_Select As System.Windows.Forms.Label
	Friend WithEvents lblQ_From As System.Windows.Forms.Label
	Friend WithEvents lblClass As System.Windows.Forms.Label
	Friend WithEvents lblQ_Where As System.Windows.Forms.Label
	Friend WithEvents lstSelectContainer As System.Windows.Forms.ListView
	Friend WithEvents lblPropertiesCount As System.Windows.Forms.Label
	Friend WithEvents cmoWhereAndOr As System.Windows.Forms.ComboBox
	Friend WithEvents lstWhereList As ServerSystemChecker.ctrlListView_SubIcons
	Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
	Friend WithEvents lblQueryResult As System.Windows.Forms.Label
	Friend WithEvents Label5 As System.Windows.Forms.Label
	Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
	Friend WithEvents cmoQueryResult As ServerSystemChecker.ctrlComboBox_Icons
	Friend WithEvents lnkExpandResults As System.Windows.Forms.LinkLabel
	Friend WithEvents Label3 As System.Windows.Forms.Label
	Friend WithEvents cmdShowQuery As System.Windows.Forms.Button
	Friend WithEvents chkAutoSortSelect As System.Windows.Forms.CheckBox
	Friend WithEvents lnkNote As System.Windows.Forms.LinkLabel
End Class
