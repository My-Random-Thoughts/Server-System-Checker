<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAddWMIQueryTestResults
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
		Me.cmdClose = New System.Windows.Forms.Button()
		Me.txtResult = New System.Windows.Forms.TextBox()
		Me.lblNote = New System.Windows.Forms.Label()
		Me.ListView1 = New System.Windows.Forms.ListView()
		Me.SuspendLayout()
		'
		'cmdClose
		'
		Me.cmdClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.cmdClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
		Me.cmdClose.Location = New System.Drawing.Point(357, 235)
		Me.cmdClose.Margin = New System.Windows.Forms.Padding(3, 12, 3, 3)
		Me.cmdClose.Name = "cmdClose"
		Me.cmdClose.Size = New System.Drawing.Size(75, 25)
		Me.cmdClose.TabIndex = 0
		Me.cmdClose.Text = "Close"
		Me.cmdClose.UseVisualStyleBackColor = True
		'
		'txtResult
		'
		Me.txtResult.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
				  Or System.Windows.Forms.AnchorStyles.Left) _
				  Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.txtResult.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.txtResult.Location = New System.Drawing.Point(14, 14)
		Me.txtResult.Multiline = True
		Me.txtResult.Name = "txtResult"
		Me.txtResult.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
		Me.txtResult.Size = New System.Drawing.Size(416, 204)
		Me.txtResult.TabIndex = 0
		'
		'lblNote
		'
		Me.lblNote.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.lblNote.AutoSize = True
		Me.lblNote.Location = New System.Drawing.Point(12, 247)
		Me.lblNote.Margin = New System.Windows.Forms.Padding(3, 6, 3, 3)
		Me.lblNote.Name = "lblNote"
		Me.lblNote.Size = New System.Drawing.Size(290, 13)
		Me.lblNote.TabIndex = 2
		Me.lblNote.Text = "This result will not pass a boolean test, try adding more filters"
		'
		'ListView1
		'
		Me.ListView1.Location = New System.Drawing.Point(12, 12)
		Me.ListView1.Name = "ListView1"
		Me.ListView1.Size = New System.Drawing.Size(420, 208)
		Me.ListView1.TabIndex = 3
		Me.ListView1.UseCompatibleStateImageBehavior = False
		'
		'frmAddWMIQueryTestResults
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.CancelButton = Me.cmdClose
		Me.ClientSize = New System.Drawing.Size(444, 272)
		Me.Controls.Add(Me.lblNote)
		Me.Controls.Add(Me.txtResult)
		Me.Controls.Add(Me.cmdClose)
		Me.Controls.Add(Me.ListView1)
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
		Me.MaximizeBox = False
		Me.MinimizeBox = False
		Me.Name = "frmAddWMIQueryTestResults"
		Me.ShowIcon = False
		Me.ShowInTaskbar = False
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
		Me.Text = " Add WMI Query - Test Results"
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub
	Friend WithEvents cmdClose As System.Windows.Forms.Button
	Friend WithEvents txtResult As System.Windows.Forms.TextBox
	Friend WithEvents lblNote As System.Windows.Forms.Label
	Friend WithEvents ListView1 As System.Windows.Forms.ListView
End Class
