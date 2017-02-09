Option Explicit On

Public Class ctrlListView_DriveSpaceTile
	Inherits System.Windows.Forms.ListView

	Public Sub New()
		Me.OwnerDraw = True
		Me.DoubleBuffered = True
		AddHandler Me.DrawItem, AddressOf lv_DrawItem
	End Sub

	Private m_AlertPercentage As Integer = 10
	Private m_WarningPercentage As Integer = 20

	Public Property AlertPercentage() As Integer
		Get
			Return m_AlertPercentage
		End Get
		Set(ByVal value As Integer)
			m_AlertPercentage = value
		End Set
	End Property
	Public Property WarningPercentage As Integer
		Get
			Return m_WarningPercentage
		End Get
		Set(value As Integer)
			m_WarningPercentage = value
		End Set
	End Property

	Private Sub lv_DrawItem(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DrawListViewItemEventArgs)
		e.DrawBackground()

		' Calculate positions...
		Dim iSFH As Long = frmMain.sysFont.Height
		Dim iLine_T As Integer = e.Bounds.Y + 2									' Top
		Dim iLine_M As Integer = e.Bounds.Y + CInt((e.Bounds.Height / 2) - 6)	' Middle
		Dim iLine_B As Integer = e.Bounds.Y + CInt(e.Bounds.Height - iSFH - 2)	' Bottom

		' % FREE Space...
		Dim iPercent As Integer = -2
		If (IsNumeric(e.Item.SubItems(1).Text) = True) Then iPercent = CInt(e.Item.SubItems(1).Text)
		If (iPercent = -2) Then
			iLine_T = iLine_T + CInt(iSFH / 2)
			iLine_B = iLine_B - CInt(iSFH / 2)
		End If

		' Draw Icon...
		If (My.Resources.ResourceManager.GetObject(e.Item.ImageKey) Is Nothing) Then e.Item.ImageKey = "_16___Blank"
		e.Graphics.DrawIcon(CType(My.Resources.ResourceManager.GetObject(e.Item.ImageKey), Icon), New Rectangle(e.Bounds.X, e.Bounds.Y + 2, 48, 48))

		If (iPercent >= 0) Then
			' Draw String...
			e.Graphics.DrawString(e.Item.Text, Me.Font, SystemBrushes.WindowText, New Point(e.Bounds.X + 52, iLine_T))
			e.Graphics.DrawString(e.Item.SubItems(2).Text, Me.Font, SystemBrushes.GrayText, New Point(e.Bounds.X + 52, iLine_B))

			' Draw Percentage Bar Background...
			e.Graphics.DrawImage(My.Resources.fs_bg, e.Bounds.X + 54, iLine_M, My.Resources.fs_bg.Width, 14)

			' Fill Percentage Bar...
			Dim iPos As Integer = CInt(((My.Resources.fs_bg.Width) / 100) * (100 - iPercent)) - 6
			Dim sResourceColour As String = "_g"
			Select Case iPercent
				Case 0 To AlertPercentage : sResourceColour = "_r"						' RED - Error
				Case AlertPercentage + 1 To WarningPercentage : sResourceColour = "_y"	' YELLOW - Warning
				Case WarningPercentage + 1 To 100 : sResourceColour = "_g"				' GREEN - OK
			End Select

			If (e.Item.ImageKey.Contains("_Disabled") = True) Then sResourceColour = sResourceColour & "_disabled"
			e.Graphics.DrawImage(CType(My.Resources.ResourceManager.GetObject("fs_l" & sResourceColour), Image), e.Bounds.X + 54 + 1, (iLine_M + 1), 2, 12)

			If (iPercent < 100) Then
				e.Graphics.DrawImage(CType(My.Resources.ResourceManager.GetObject("fs_c" & sResourceColour), Image), e.Bounds.X + 54 + 3, (iLine_M + 1), iPos, 12)
				e.Graphics.DrawImage(CType(My.Resources.ResourceManager.GetObject("fs_r" & sResourceColour), Image), e.Bounds.X + 54 + iPos + 3, (iLine_M + 1), 2, 12)
			End If
		ElseIf (iPercent = -1) Then
			' Draw Unknown Percentage Bar Background...
			e.Graphics.DrawString(e.Item.Text, Me.Font, SystemBrushes.WindowText, New Point(e.Bounds.X + 52, iLine_M))
		Else
			e.Graphics.DrawString(e.Item.Text, Me.Font, SystemBrushes.WindowText, New Point(e.Bounds.X + 52, iLine_T))
			e.Graphics.DrawString(e.Item.SubItems(1).Text, Me.Font, SystemBrushes.WindowText, New Point(e.Bounds.X + 52, iLine_B))
		End If
	End Sub
End Class