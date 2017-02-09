Option Explicit On

Public Class ctrlListView_SubIcons
	Inherits System.Windows.Forms.ListView

	Private Const LVM_FIRST As Integer = 4096
	Private Const LVM_GETEXTENDEDLISTVIEWSTYLE As Integer = (LVM_FIRST + 54)
	Private Const LVS_EX_GRIDLINES As Integer = 1
	Private Const LVS_EX_SUBITEMIMAGES As Integer = 2
	Private Const LVS_EX_CHECKBOXES As Integer = 4
	Private Const LVS_EX_TRACKSELECT As Integer = 8
	Private Const LVS_EX_FULLROWSELECT As Integer = 32	' applies to report mode only
	Private Const WM_NOTIFY As Integer = 78

	' Change the style to accept images on subitems.
	Public Sub New()
		Me.OwnerDraw = True
		Me.DoubleBuffered = True
		AddHandler Me.DrawSubItem, AddressOf ctrlListView_SubIcons_DrawSubItem
		AddHandler Me.HandleCreated, AddressOf ctrlListView_SubIcons_HandleCreated
		AddHandler Me.DrawColumnHeader, AddressOf ctrlListView_SubIcons_DrawColumnHeader
	End Sub

	Private Sub ctrlListView_SubIcons_HandleCreated(ByVal sender As Object, ByVal e As EventArgs)
		' Change the style of listview to accept image on subitems
		Dim m As System.Windows.Forms.Message = New Message
		m.HWnd = Me.Handle
		m.Msg = LVM_GETEXTENDEDLISTVIEWSTYLE
		m.LParam = New IntPtr(LVS_EX_SUBITEMIMAGES Or LVS_EX_FULLROWSELECT Or LVS_EX_TRACKSELECT Or LVS_EX_CHECKBOXES Or LVS_EX_GRIDLINES)
		m.WParam = IntPtr.Zero
		Me.WndProc(m)
	End Sub

	' Handle DrawSubItem event
	Private Sub ctrlListView_SubIcons_DrawSubItem(ByVal sender As Object, ByVal e As DrawListViewSubItemEventArgs)
		Dim r As Rectangle
		Dim iImg As String = e.Item.ImageKey
		Dim cControl As ctrlListView_SubIcons = CType(sender, ctrlListView_SubIcons)

		If (cControl.View <> Windows.Forms.View.Details) Then
			e.DrawDefault = True
			Exit Sub
		End If

		If (e.SubItem.Text.Length < 5) Then
			e.DrawDefault = True
			Exit Sub
		End If

		If (e.SubItem.Text.StartsWith("-SEP-")) Then
			e.Graphics.DrawLine(SystemPens.Highlight, e.Bounds.Left + 6, e.Bounds.Y + CInt(e.Bounds.Height / 2), e.Bounds.Right - 6, e.Bounds.Y + CInt(e.Bounds.Height / 2))
			Exit Sub
		End If

		Dim bFirstColumn As Boolean = False
		If (e.SubItem.Bounds.X = e.Item.Bounds.X) Then bFirstColumn = True

		If ((e.SubItem.Text.StartsWith("ICON:") = True) Or (e.SubItem.Text.StartsWith("BOTH:") = True) Or (e.SubItem.Text.StartsWith("GREY:") = True)) Then
			If (e.Bounds.Width = 16) Then
				e.DrawDefault = False
				e.Graphics.DrawRectangle(SystemPens.Window, e.SubItem.Bounds)
				Exit Sub
			End If
			Dim cImageCtrl As ImageList = cControl.SmallImageList
			iImg = cImageCtrl.Images.Keys(CInt(e.SubItem.Text.Substring(5, 1)))
		End If

		Dim wImg As Integer = 16	' Forcing 16x16 icons here
		Dim hImg As Integer = 16	' 
		Dim xPos As Integer = (e.SubItem.Bounds.X + CInt(e.SubItem.Bounds.Width / 2) - CInt(wImg / 2))		' Middle of "e.SubItem.Bounds.Width"
		Dim yPos As Integer = (e.SubItem.Bounds.Y + CInt(e.SubItem.Bounds.Height / 2) - CInt(hImg / 2))		' Middle of "e.SubItem.Bounds.Height"

		Dim sbTextColour As SolidBrush = New SolidBrush(e.SubItem.ForeColor)
		Dim fFont As New Font(cControl.Font, cControl.Font.Style)
		If (e.Item.UseItemStyleForSubItems = True) Then fFont = New Font(e.Item.Font, e.Item.Font.Style)
		Dim fPos As Integer = CInt((e.SubItem.Bounds.Height - fFont.Height) / 2)

		Select Case e.SubItem.Text.Substring(0, 5).ToUpper
			Case "ICON:"
				e.DrawDefault = False
				r = New Rectangle(xPos, yPos, wImg, hImg)
				e.Graphics.DrawIcon(CType(My.Resources.ResourceManager.GetObject(iImg), Icon), r)

			Case "GREY:"
				e.DrawDefault = False
				r = New Rectangle(xPos, yPos, wImg, hImg)
				e.Graphics.DrawIcon(GreyScaleIcon(CType(My.Resources.ResourceManager.GetObject(iImg), Icon), True), r)

			Case "BOTH:"
				e.DrawDefault = False
				r = New Rectangle(e.SubItem.Bounds.X + 2, yPos, wImg, hImg)
				e.Graphics.DrawIcon(CType(My.Resources.ResourceManager.GetObject(iImg), Icon), r)
				r = New Rectangle(e.SubItem.Bounds.X + 5 + wImg, e.SubItem.Bounds.Y + fPos, e.SubItem.Bounds.Width - wImg - 5, e.SubItem.Bounds.Height - fPos)
				e.Graphics.DrawString(e.SubItem.Text.Substring(7), fFont, sbTextColour, r)
				'                                              ^- Substring of: "BOTH:x:stringtext"
			Case Else '															 0123456^
				e.DrawDefault = True
		End Select

		fFont.Dispose()
		sbTextColour.Dispose()
	End Sub

	' Handle DrawColumnHeader event
	Private Sub ctrlListView_SubIcons_DrawColumnHeader(ByVal sender As Object, ByVal e As DrawListViewColumnHeaderEventArgs)
		e.DrawDefault = True
		e.DrawBackground()
		e.DrawText()
	End Sub
End Class