Option Explicit On

Public Class frm_DragDrop
	' +-----------------------------+      +--------------------+      +---+
	' | > Move to [group_name_here] |      | * Create New Group |      | X |
	' +-----------------------------+      +--------------------+      +---+

	Private Sub frm_DragDrop_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
		lblMoveTo.Font = frmMain.sysFont
		lblMoveNode.Font = frmMain.sysFontBold

		Me.Height = 24
		Me.BackgroundImage = My.Resources.tt_bg_x1
		Me.TransparencyKey = Me.BackColor
		Me.BackgroundImageLayout = ImageLayout.Stretch

		picEndCap_Left.Height = Me.Height
		picEndCap_Left.Location = New Point(0, 0)
		picEndCap_Left.Image = My.Resources.tt_l_x1

		picEndCap_Right.Height = Me.Height
		picEndCap_Right.Location = New Point(Me.Width - 6, 0)
		picEndCap_Right.Image = My.Resources.tt_r_x1
		picEndCap_Right.Anchor = AnchorStyles.Right

		picMoveNode.Location = New Point(4, 4)
		lblMoveTo.Location = New Point(picMoveNode.Right + 1, 5)
	End Sub

	Private Sub frm_DragDrop_Activated(sender As Object, e As System.EventArgs) Handles Me.Activated
		Me.TopMost = True
	End Sub

	Public Sub changeSize()
		If (lblMoveNode.Text = "!-ERROR-!") Then
			lblMoveNode.Visible = False
			picMoveNode.Image = My.Resources._16___Scan___Failed.ToBitmap
			Me.Width = 24	' (4 + 16 + 4) (Gap - Icon - Gap)
		Else
			If (lblMoveTo.Visible = False) Then
				picMoveNode.Image = CType(My.Resources.ResourceManager.GetObject("_16___Group___" & m_IconGroup & "____New"), Icon).ToBitmap
				lblMoveNode.Location = New Point(picMoveNode.Right + 1, 5)
			Else
				picMoveNode.Image = My.Resources._16___Move.ToBitmap
				lblMoveNode.Location = New Point(lblMoveTo.Right - 3, 5)
			End If
			lblMoveNode.Visible = True
			Me.Width = (lblMoveNode.Right + 3)
		End If
	End Sub
End Class