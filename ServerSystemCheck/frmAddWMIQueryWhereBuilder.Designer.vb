<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAddWMIQueryWhereBuilder
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
		Me.txtWhereFreeText = New System.Windows.Forms.TextBox()
		Me.cmdCancel = New System.Windows.Forms.Button()
		Me.cmdOK = New System.Windows.Forms.Button()
		Me.Label1 = New System.Windows.Forms.Label()
		Me.Label2 = New System.Windows.Forms.Label()
		Me.Label3 = New System.Windows.Forms.Label()
		Me.lblNote = New System.Windows.Forms.Label()
		Me.cmdCalculator = New System.Windows.Forms.Button()
		Me.cmoWhereOperator = New ServerSystemChecker.ctrlComboBox_Icons()
		Me.cmoWhereProperties = New ServerSystemChecker.ctrlComboBox_Icons()
		Me.SuspendLayout()
		'
		'txtWhereFreeText
		'
		Me.txtWhereFreeText.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
				  Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.txtWhereFreeText.Location = New System.Drawing.Point(100, 80)
		Me.txtWhereFreeText.Margin = New System.Windows.Forms.Padding(3, 6, 3, 6)
		Me.txtWhereFreeText.Name = "txtWhereFreeText"
		Me.txtWhereFreeText.Size = New System.Drawing.Size(204, 20)
		Me.txtWhereFreeText.TabIndex = 2
		Me.txtWhereFreeText.TabStop = False
		'
		'cmdCancel
		'
		Me.cmdCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
		Me.cmdCancel.Location = New System.Drawing.Point(176, 164)
		Me.cmdCancel.Margin = New System.Windows.Forms.Padding(6, 12, 3, 3)
		Me.cmdCancel.Name = "cmdCancel"
		Me.cmdCancel.Size = New System.Drawing.Size(75, 25)
		Me.cmdCancel.TabIndex = 5
		Me.cmdCancel.Text = "Cancel"
		Me.cmdCancel.UseVisualStyleBackColor = True
		'
		'cmdOK
		'
		Me.cmdOK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.cmdOK.Location = New System.Drawing.Point(257, 164)
		Me.cmdOK.Margin = New System.Windows.Forms.Padding(3, 12, 3, 3)
		Me.cmdOK.Name = "cmdOK"
		Me.cmdOK.Size = New System.Drawing.Size(75, 25)
		Me.cmdOK.TabIndex = 4
		Me.cmdOK.Text = "OK"
		Me.cmdOK.UseVisualStyleBackColor = True
		'
		'Label1
		'
		Me.Label1.AutoSize = True
		Me.Label1.Location = New System.Drawing.Point(12, 17)
		Me.Label1.Name = "Label1"
		Me.Label1.Size = New System.Drawing.Size(52, 13)
		Me.Label1.TabIndex = 101
		Me.Label1.Text = "Property :"
		'
		'Label2
		'
		Me.Label2.AutoSize = True
		Me.Label2.Location = New System.Drawing.Point(12, 51)
		Me.Label2.Name = "Label2"
		Me.Label2.Size = New System.Drawing.Size(54, 13)
		Me.Label2.TabIndex = 102
		Me.Label2.Text = "Operator :"
		'
		'Label3
		'
		Me.Label3.AutoSize = True
		Me.Label3.Location = New System.Drawing.Point(12, 85)
		Me.Label3.Name = "Label3"
		Me.Label3.Size = New System.Drawing.Size(40, 13)
		Me.Label3.TabIndex = 103
		Me.Label3.Text = "Value :"
		'
		'lblNote
		'
		Me.lblNote.Location = New System.Drawing.Point(12, 109)
		Me.lblNote.Margin = New System.Windows.Forms.Padding(3)
		Me.lblNote.Name = "lblNote"
		Me.lblNote.Size = New System.Drawing.Size(320, 40)
		Me.lblNote.TabIndex = 104
		Me.lblNote.Text = "Text values are not case sensitive." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Files, disks and other size values are retur" & _
		  "ned in bytes."
		Me.lblNote.TextAlign = System.Drawing.ContentAlignment.BottomLeft
		'
		'cmdCalculator
		'
		Me.cmdCalculator.Location = New System.Drawing.Point(307, 78)
		Me.cmdCalculator.Margin = New System.Windows.Forms.Padding(0, 3, 3, 3)
		Me.cmdCalculator.Name = "cmdCalculator"
		Me.cmdCalculator.Size = New System.Drawing.Size(25, 25)
		Me.cmdCalculator.TabIndex = 105
		Me.cmdCalculator.UseVisualStyleBackColor = True
		'
		'cmoWhereOperator
		'
		Me.cmoWhereOperator.BackColor = System.Drawing.SystemColors.Window
		Me.cmoWhereOperator.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
		Me.cmoWhereOperator.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.cmoWhereOperator.FormattingEnabled = True
		Me.cmoWhereOperator.ItemHeight = 19
		Me.cmoWhereOperator.Location = New System.Drawing.Point(100, 46)
		Me.cmoWhereOperator.Name = "cmoWhereOperator"
		Me.cmoWhereOperator.RemoveIconSpacing = False
		Me.cmoWhereOperator.SelectedItem = Nothing
		Me.cmoWhereOperator.Size = New System.Drawing.Size(204, 25)
		Me.cmoWhereOperator.TabIndex = 1
		'
		'cmoWhereProperties
		'
		Me.cmoWhereProperties.BackColor = System.Drawing.SystemColors.Window
		Me.cmoWhereProperties.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
		Me.cmoWhereProperties.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.cmoWhereProperties.FormattingEnabled = True
		Me.cmoWhereProperties.ItemHeight = 19
		Me.cmoWhereProperties.Location = New System.Drawing.Point(100, 12)
		Me.cmoWhereProperties.Margin = New System.Windows.Forms.Padding(3, 3, 3, 6)
		Me.cmoWhereProperties.Name = "cmoWhereProperties"
		Me.cmoWhereProperties.RemoveIconSpacing = False
		Me.cmoWhereProperties.SelectedItem = Nothing
		Me.cmoWhereProperties.Size = New System.Drawing.Size(204, 25)
		Me.cmoWhereProperties.TabIndex = 0
		'
		'frmAddWMIQueryWhereBuilder
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.CancelButton = Me.cmdCancel
		Me.ClientSize = New System.Drawing.Size(344, 201)
		Me.Controls.Add(Me.cmdCalculator)
		Me.Controls.Add(Me.lblNote)
		Me.Controls.Add(Me.cmoWhereOperator)
		Me.Controls.Add(Me.cmoWhereProperties)
		Me.Controls.Add(Me.Label3)
		Me.Controls.Add(Me.Label2)
		Me.Controls.Add(Me.Label1)
		Me.Controls.Add(Me.cmdCancel)
		Me.Controls.Add(Me.cmdOK)
		Me.Controls.Add(Me.txtWhereFreeText)
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
		Me.MaximizeBox = False
		Me.MinimizeBox = False
		Me.Name = "frmAddWMIQueryWhereBuilder"
		Me.ShowIcon = False
		Me.ShowInTaskbar = False
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
		Me.Text = " Add WMI Query"
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub
	Friend WithEvents txtWhereFreeText As System.Windows.Forms.TextBox
	Friend WithEvents cmdCancel As System.Windows.Forms.Button
	Friend WithEvents cmdOK As System.Windows.Forms.Button
	Friend WithEvents Label1 As System.Windows.Forms.Label
	Friend WithEvents Label2 As System.Windows.Forms.Label
	Friend WithEvents Label3 As System.Windows.Forms.Label
	Friend WithEvents cmoWhereProperties As ServerSystemChecker.ctrlComboBox_Icons
	Friend WithEvents cmoWhereOperator As ServerSystemChecker.ctrlComboBox_Icons
	Friend WithEvents lblNote As System.Windows.Forms.Label
	Friend WithEvents cmdCalculator As System.Windows.Forms.Button
End Class
