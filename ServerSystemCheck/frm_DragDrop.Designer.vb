<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_DragDrop
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
		Me.picMoveNode = New System.Windows.Forms.PictureBox()
		Me.lblMoveNode = New System.Windows.Forms.Label()
		Me.picEndCap_Left = New System.Windows.Forms.PictureBox()
		Me.picEndCap_Right = New System.Windows.Forms.PictureBox()
		Me.lblMoveTo = New System.Windows.Forms.Label()
		CType(Me.picMoveNode, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.picEndCap_Left, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.picEndCap_Right, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.SuspendLayout()
		'
		'picMoveNode
		'
		Me.picMoveNode.BackColor = System.Drawing.Color.Transparent
		Me.picMoveNode.Location = New System.Drawing.Point(12, 5)
		Me.picMoveNode.Name = "picMoveNode"
		Me.picMoveNode.Size = New System.Drawing.Size(16, 16)
		Me.picMoveNode.TabIndex = 8
		Me.picMoveNode.TabStop = False
		'
		'lblMoveNode
		'
		Me.lblMoveNode.AutoSize = True
		Me.lblMoveNode.BackColor = System.Drawing.Color.Transparent
		Me.lblMoveNode.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.lblMoveNode.Location = New System.Drawing.Point(80, 6)
		Me.lblMoveNode.Margin = New System.Windows.Forms.Padding(0)
		Me.lblMoveNode.Name = "lblMoveNode"
		Me.lblMoveNode.Size = New System.Drawing.Size(42, 13)
		Me.lblMoveNode.TabIndex = 7
		Me.lblMoveNode.Text = "xxxxxxx"
		'
		'picEndCap_Left
		'
		Me.picEndCap_Left.Location = New System.Drawing.Point(0, 0)
		Me.picEndCap_Left.Name = "picEndCap_Left"
		Me.picEndCap_Left.Size = New System.Drawing.Size(6, 24)
		Me.picEndCap_Left.TabIndex = 9
		Me.picEndCap_Left.TabStop = False
		'
		'picEndCap_Right
		'
		Me.picEndCap_Right.Location = New System.Drawing.Point(143, 1)
		Me.picEndCap_Right.Name = "picEndCap_Right"
		Me.picEndCap_Right.Size = New System.Drawing.Size(6, 24)
		Me.picEndCap_Right.TabIndex = 10
		Me.picEndCap_Right.TabStop = False
		'
		'lblMoveTo
		'
		Me.lblMoveTo.AutoSize = True
		Me.lblMoveTo.BackColor = System.Drawing.Color.Transparent
		Me.lblMoveTo.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.lblMoveTo.Location = New System.Drawing.Point(31, 6)
		Me.lblMoveTo.Margin = New System.Windows.Forms.Padding(0)
		Me.lblMoveTo.Name = "lblMoveTo"
		Me.lblMoveTo.Size = New System.Drawing.Size(49, 13)
		Me.lblMoveTo.TabIndex = 11
		Me.lblMoveTo.Text = "Move to"
		'
		'frm_DragDrop
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(150, 26)
		Me.ControlBox = False
		Me.Controls.Add(Me.lblMoveTo)
		Me.Controls.Add(Me.picMoveNode)
		Me.Controls.Add(Me.lblMoveNode)
		Me.Controls.Add(Me.picEndCap_Right)
		Me.Controls.Add(Me.picEndCap_Left)
		Me.Cursor = System.Windows.Forms.Cursors.Default
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
		Me.MaximizeBox = False
		Me.MinimizeBox = False
		Me.Name = "frm_DragDrop"
		Me.ShowIcon = False
		Me.ShowInTaskbar = False
		Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
		Me.Text = "frm_DragDrop"
		CType(Me.picMoveNode, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.picEndCap_Left, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.picEndCap_Right, System.ComponentModel.ISupportInitialize).EndInit()
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub
	Friend WithEvents picMoveNode As System.Windows.Forms.PictureBox
	Friend WithEvents lblMoveNode As System.Windows.Forms.Label
	Friend WithEvents picEndCap_Left As System.Windows.Forms.PictureBox
	Friend WithEvents picEndCap_Right As System.Windows.Forms.PictureBox
	Friend WithEvents lblMoveTo As System.Windows.Forms.Label
End Class
