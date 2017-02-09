<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAddWMIQueryWhereBuilderCalc
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
		Me.txtInput = New System.Windows.Forms.TextBox()
		Me.cmdOK = New System.Windows.Forms.Button()
		Me.cmoUnits = New System.Windows.Forms.ComboBox()
		Me.Label1 = New System.Windows.Forms.Label()
		Me.Label2 = New System.Windows.Forms.Label()
		Me.lblOutput = New System.Windows.Forms.Label()
		Me.cmdCancel = New System.Windows.Forms.Button()
		Me.SuspendLayout()
		'
		'txtInput
		'
		Me.txtInput.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
				  Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.txtInput.Location = New System.Drawing.Point(100, 12)
		Me.txtInput.Name = "txtInput"
		Me.txtInput.Size = New System.Drawing.Size(151, 20)
		Me.txtInput.TabIndex = 0
		Me.txtInput.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
		'
		'cmdOK
		'
		Me.cmdOK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.cmdOK.Location = New System.Drawing.Point(257, 84)
		Me.cmdOK.Margin = New System.Windows.Forms.Padding(3, 12, 3, 3)
		Me.cmdOK.Name = "cmdOK"
		Me.cmdOK.Size = New System.Drawing.Size(75, 25)
		Me.cmdOK.TabIndex = 2
		Me.cmdOK.Text = "OK"
		Me.cmdOK.UseVisualStyleBackColor = True
		'
		'cmoUnits
		'
		Me.cmoUnits.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.cmoUnits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.cmoUnits.FormattingEnabled = True
		Me.cmoUnits.Location = New System.Drawing.Point(257, 12)
		Me.cmoUnits.Name = "cmoUnits"
		Me.cmoUnits.Size = New System.Drawing.Size(75, 21)
		Me.cmoUnits.TabIndex = 1
		'
		'Label1
		'
		Me.Label1.AutoSize = True
		Me.Label1.Location = New System.Drawing.Point(12, 15)
		Me.Label1.Name = "Label1"
		Me.Label1.Size = New System.Drawing.Size(67, 13)
		Me.Label1.TabIndex = 4
		Me.Label1.Text = "Input Value :"
		'
		'Label2
		'
		Me.Label2.AutoSize = True
		Me.Label2.Location = New System.Drawing.Point(12, 47)
		Me.Label2.Name = "Label2"
		Me.Label2.Size = New System.Drawing.Size(43, 13)
		Me.Label2.TabIndex = 5
		Me.Label2.Text = "Result :"
		'
		'lblOutput
		'
		Me.lblOutput.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
				  Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.lblOutput.Location = New System.Drawing.Point(100, 47)
		Me.lblOutput.Margin = New System.Windows.Forms.Padding(3, 12, 0, 3)
		Me.lblOutput.Name = "lblOutput"
		Me.lblOutput.Size = New System.Drawing.Size(151, 17)
		Me.lblOutput.TabIndex = 6
		Me.lblOutput.Text = "lblOutput (bold)"
		Me.lblOutput.TextAlign = System.Drawing.ContentAlignment.TopRight
		'
		'cmdCancel
		'
		Me.cmdCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
		Me.cmdCancel.Location = New System.Drawing.Point(176, 84)
		Me.cmdCancel.Margin = New System.Windows.Forms.Padding(6, 12, 3, 3)
		Me.cmdCancel.Name = "cmdCancel"
		Me.cmdCancel.Size = New System.Drawing.Size(75, 25)
		Me.cmdCancel.TabIndex = 3
		Me.cmdCancel.Text = "Cancel"
		Me.cmdCancel.UseVisualStyleBackColor = True
		'
		'frmAddWMIQueryWhereBuilderCalc
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.CancelButton = Me.cmdCancel
		Me.ClientSize = New System.Drawing.Size(344, 121)
		Me.Controls.Add(Me.lblOutput)
		Me.Controls.Add(Me.cmdCancel)
		Me.Controls.Add(Me.Label2)
		Me.Controls.Add(Me.Label1)
		Me.Controls.Add(Me.cmoUnits)
		Me.Controls.Add(Me.cmdOK)
		Me.Controls.Add(Me.txtInput)
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
		Me.MaximizeBox = False
		Me.MinimizeBox = False
		Me.Name = "frmAddWMIQueryWhereBuilderCalc"
		Me.ShowInTaskbar = False
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
		Me.Text = " Size Converter"
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub
	Friend WithEvents txtInput As System.Windows.Forms.TextBox
	Friend WithEvents cmdOK As System.Windows.Forms.Button
	Friend WithEvents cmoUnits As System.Windows.Forms.ComboBox
	Friend WithEvents Label1 As System.Windows.Forms.Label
	Friend WithEvents Label2 As System.Windows.Forms.Label
	Friend WithEvents lblOutput As System.Windows.Forms.Label
	Friend WithEvents cmdCancel As System.Windows.Forms.Button
End Class
