Option Explicit On
Imports System.Runtime.InteropServices
Imports System.Reflection

Public Class ctrlListView_CollapseGroups
	Inherits System.Windows.Forms.ListView

	Private Const LVM_FIRST As Integer = 4096
	Private Const LVM_SETGROUPINFO As Integer = (LVM_FIRST + 147)
	Private Const WM_LBUTTONUP As Integer = 514
	Private Const LVGA_FOOTER_LEFT As Integer = 8
	Private Const LVGA_FOOTER_CENTER As Integer = 16
	Private Const LVGA_FOOTER_RIGHT As Integer = 32

	Public Enum WindowsMessages As Integer
		WM_CHANGEUISTATE = &H127
		WM_LBUTTONDBLCLK = &H203
		WM_LBUTTONDOWN = &H201
		WM_LBUTTONUP = &H202
		WM_RBUTTONDBLCLK = &H206
		WM_KEYDOWN = &H100
		WM_KEYUP = &H101
	End Enum

	Public Delegate Sub CallBackSetGroupState(ByVal lstvwgrp As ListViewGroup, ByVal state As ListViewGroupState)
	Public Delegate Sub CallBackSetGroupFooter(ByVal lstvwgrp As ListViewGroup)
	Public Shadows Event DoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs)

	Sub New()
		Me.DoubleBuffered = True
		Call SetWindowTheme(Me.Handle, "Explorer", Nothing)
	End Sub

	Private Shared Function GetGroupID(ByVal lstvwgrp As ListViewGroup) As Integer?
		Dim rtnval As Integer? = Nothing	' <<-- Nullable type
		Dim GrpTp As Type = lstvwgrp.GetType
		If (Not (GrpTp) Is Nothing) Then
			Dim pi As PropertyInfo = GrpTp.GetProperty("ID", (BindingFlags.NonPublic Or BindingFlags.Instance))
			If (Not (pi) Is Nothing) Then
				Dim tmprtnval As Object = pi.GetValue(lstvwgrp, Nothing)
				If (Not (tmprtnval) Is Nothing) Then
					rtnval = CType(tmprtnval, Integer?)
				End If
			End If
		End If
		Return rtnval
	End Function

	Private Shared Sub setGrpState(ByVal lstvwgrp As ListViewGroup, ByVal state As ListViewGroupState)
		If (Environment.OSVersion.Version.Major < 6) Then Return
		If ((lstvwgrp Is Nothing) OrElse (lstvwgrp.ListView Is Nothing)) Then Return

		If lstvwgrp.ListView.InvokeRequired Then
			lstvwgrp.ListView.Invoke(New CallBackSetGroupState(AddressOf setGrpState), lstvwgrp, state)
		Else
			Dim GrpId As Integer? = GetGroupID(lstvwgrp)	' <<-- Nullable type
			Dim gIndex As Integer = lstvwgrp.ListView.Groups.IndexOf(lstvwgrp)
			Dim group As LVGROUP = New LVGROUP
			group.CbSize = Marshal.SizeOf(group)
			group.State = state
			group.PszFooter = String.Empty
			If (m_ShowGroupCounts = True) Then
				Dim iCnt As Integer = lstvwgrp.Items.Count
				group.PszFooter = iCnt.ToString & " item" & IIf(iCnt = 1, " ", "s ").ToString
			End If
			group.UAlign = LVGA_FOOTER_RIGHT
			group.Mask = ListViewGroupMask.State Or ListViewGroupMask.Footer Or ListViewGroupMask.Align

			Try
				Dim ip As IntPtr = IntPtr.Zero
				ip = Marshal.AllocHGlobal(group.CbSize)
				Marshal.StructureToPtr(group, ip, False)

				If (GrpId.HasValue = True) Then
					group.IGroupId = GrpId.Value
					SendMessage(lstvwgrp.ListView.Handle, LVM_SETGROUPINFO, GrpId.Value, ip)
				Else
					group.IGroupId = gIndex
					SendMessage(lstvwgrp.ListView.Handle, LVM_SETGROUPINFO, gIndex, ip)
				End If
			Catch ex As Exception
				Application.DoEvents()
			End Try

			lstvwgrp.ListView.Refresh()
		End If
	End Sub

	Private Shared Sub setGrpFooter(ByVal lstvwgrp As ListViewGroup)
		If (Environment.OSVersion.Version.Major < 6) Then Return
		If ((lstvwgrp Is Nothing) OrElse (lstvwgrp.ListView Is Nothing)) Then Return

		If lstvwgrp.ListView.InvokeRequired Then
			lstvwgrp.ListView.Invoke(New CallBackSetGroupFooter(AddressOf setGrpFooter), lstvwgrp)
		Else
			Dim GrpId As Integer? = GetGroupID(lstvwgrp)
			Dim gIndex As Integer = lstvwgrp.ListView.Groups.IndexOf(lstvwgrp)
			Dim group As LVGROUP = New LVGROUP
			group.CbSize = Marshal.SizeOf(group)
			group.PszFooter = String.Empty
			If (m_ShowGroupCounts = True) Then
				Dim iCnt As Integer = lstvwgrp.Items.Count
				group.PszFooter = iCnt.ToString & " item" & IIf(iCnt = 1, " ", "s ").ToString
			End If
			group.UAlign = LVGA_FOOTER_RIGHT
			group.Mask = ListViewGroupMask.Footer Or ListViewGroupMask.Align

			Try
				Dim ip As IntPtr = IntPtr.Zero
				ip = Marshal.AllocHGlobal(group.CbSize)
				Marshal.StructureToPtr(group, ip, False)

				If (GrpId.HasValue = True) Then
					group.IGroupId = GrpId.Value
					SendMessage(lstvwgrp.ListView.Handle, LVM_SETGROUPINFO, GrpId.Value, ip)
				Else
					group.IGroupId = gIndex
					SendMessage(lstvwgrp.ListView.Handle, LVM_SETGROUPINFO, gIndex, ip)
				End If
			Catch ex As Exception
				Application.DoEvents()
			End Try
			lstvwgrp.ListView.Refresh()
		End If
	End Sub

	Public Overloads Sub SetGroupState(ByVal state As ListViewGroupState)
		For Each lvg As ListViewGroup In Me.Groups
			setGrpState(lvg, state)
		Next
	End Sub

	Public Overloads Sub SetGroupState(ByVal state As ListViewGroupState, ByVal lvg As ListViewGroup)
		setGrpState(lvg, state)
	End Sub

	Public Sub SetGroupFooter(ByVal lvg As ListViewGroup)
		setGrpFooter(lvg)
	End Sub

	Protected Overrides Sub CreateHandle()
		MyBase.CreateHandle()
		If (bUseVisualStyles = True) Then
			Call SetWindowTheme(Me.Handle, "Explorer", Nothing)
			SendMessage(MyBase.Handle, WindowsMessages.WM_CHANGEUISTATE, 65537, IntPtr.Zero)
		End If
	End Sub

	Private ItemClicked As Boolean = False
	Protected Overrides Sub WndProc(ByRef m As Message)
		Select Case m.Msg
			Case WindowsMessages.WM_LBUTTONDOWN
				' Check to see if an item was selected or not
				If (MyBase.Groups.Count = 0) Then Return
				Dim htInfo As ListViewHitTestInfo = MyBase.HitTest(New Point((m.LParam.ToInt32() And &HFFFF), (m.LParam.ToInt32() >> 16)))
				ItemClicked = CBool(IIf(htInfo.Item Is Nothing, False, True))
				If (Not ItemClicked) Then Return

			Case WindowsMessages.WM_LBUTTONUP
				' If a non-item is selected, don't select all the items in all groups.
				If (MyBase.Groups.Count = 0) Then Return
				If (Not ItemClicked) Then
					MyBase.DefWndProc(m)
					MyBase.BeginUpdate()
					For Each Group As ListViewGroup In MyBase.Groups
						For Each Item As ListViewItem In Group.Items
							Item.Selected = False
						Next
					Next
					MyBase.EndUpdate()
					Return
				Else
					ItemClicked = False
				End If

			Case WindowsMessages.WM_LBUTTONDBLCLK
				' LEFT Button DOUBLE CLICK
				If (Me.SelectedItems.Count = 0) Then RaiseEvent DoubleClick(Me, New System.Windows.Forms.MouseEventArgs(Windows.Forms.MouseButtons.Left, 2, -1, -1, -1))

			Case WindowsMessages.WM_RBUTTONDBLCLK
				' RIGHT Button DOUBLE CLICK
				RaiseEvent DoubleClick(Me, New System.Windows.Forms.MouseEventArgs(Windows.Forms.MouseButtons.Right, 2, -1, -1, -1))
		End Select
		MyBase.WndProc(m)
	End Sub
End Class

<StructLayout(LayoutKind.Sequential, CharSet:=CharSet.Unicode)> Public Structure LVGROUP
	Public CbSize As Integer
	Public Mask As ListViewGroupMask
	<MarshalAs(UnmanagedType.LPWStr)> Public PszHeader As String
	Public CchHeader As Integer
	<MarshalAs(UnmanagedType.LPWStr)> Public PszFooter As String
	Public CchFooter As Integer
	Public IGroupId As Integer
	Public StateMask As Integer
	Public State As ListViewGroupState
	Public UAlign As UInteger
	Public PszSubtitle As IntPtr
	Public CchSubtitle As UInteger
	<MarshalAs(UnmanagedType.LPWStr)> Public PszTask As String
	Public CchTask As UInteger
	<MarshalAs(UnmanagedType.LPWStr)> Public PszDescriptionTop As String
	Public CchDescriptionTop As UInteger
	<MarshalAs(UnmanagedType.LPWStr)> Public PszDescriptionBottom As String
	Public CchDescriptionBottom As UInteger
	Public ITitleImage As Integer
	Public IExtendedImage As Integer
	Public IFirstItem As Integer
	Public CItems As IntPtr
	Public PszSubsetTitle As IntPtr
	Public CchSubsetTitle As IntPtr
End Structure

Public Enum ListViewGroupMask
	None = 0
	Header = 1
	Footer = 2
	State = 4
	Align = 8
	GroupId = 16
	SubTitle = 256
	Task = 512
	DescriptionTop = 1024
	DescriptionBottom = 2048
	TitleImage = 4096
	ExtendedImage = 8192
	Items = 16384
	Subset = 32768
	SubsetItems = 65536
End Enum

Public Enum ListViewGroupState
	Normal = 0
	Collapsed = 1
	Hidden = 2
	NoHeader = 4
	Collapsible = 8
	Focused = 16
	Selected = 32
	SubSeted = 64
	SubSetLinkFocused = 128
End Enum
