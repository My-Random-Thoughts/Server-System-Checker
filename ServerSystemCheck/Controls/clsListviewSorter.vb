Option Explicit On

Imports System.Runtime.InteropServices

Public Class ListViewColumnSorter
	Implements System.Collections.IComparer
	Private ColumnToSort As Integer
	Private OrderOfSort As SortOrder
	Private ObjectCompare As CaseInsensitiveComparer
	Private SortByTag As Boolean
	'
	' Taken From http://support.microsoft.com/kb/319399
	'
	Public Sub New()
		ColumnToSort = 0
		OrderOfSort = SortOrder.None
		ObjectCompare = New CaseInsensitiveComparer()
	End Sub

	Public Function Compare(ByVal x As Object, ByVal y As Object) As Integer Implements IComparer.Compare
		If ((x Is Nothing) Or (y Is Nothing)) Then Return 0
		Dim compareResult As Integer
		Dim lvX As ListViewItem = CType(x, ListViewItem)
		Dim lvY As ListViewItem = CType(y, ListViewItem)
		Dim fD As Boolean
		Dim sD As Boolean
		Dim fDate As DateTime
		Dim sDate As DateTime

		' Try to sort as dates first, then fail back to strings
		Try
			Try
				If (SortByTag = True) Then
					' DATES by TAG
					fD = DateTime.TryParse(lvX.SubItems(ColumnToSort).Tag.ToString, fDate)
					sD = DateTime.TryParse(lvY.SubItems(ColumnToSort).Tag.ToString, sDate)
				Else
					' DATES by TEXT
					fD = DateTime.TryParse(lvX.SubItems(ColumnToSort).Text, fDate)
					sD = DateTime.TryParse(lvY.SubItems(ColumnToSort).Text, sDate)
				End If
			Catch
				fD = False
				sD = False
			End Try

			If ((fD = True) And (sD = True)) Then
				' DATES
				compareResult = DateTime.Compare(fDate, sDate)
			Else
				If (SortByTag = True) Then
					' TEXT by TAG
					compareResult = ObjectCompare.Compare(lvX.SubItems(ColumnToSort).Tag.ToString, lvY.SubItems(ColumnToSort).Tag.ToString)
				Else
					' TEXT by TEXT
					compareResult = ObjectCompare.Compare(lvX.SubItems(ColumnToSort).Text, lvY.SubItems(ColumnToSort).Text)
				End If
			End If

			' SORT...
			If (OrderOfSort = SortOrder.Ascending) Then
				Return compareResult
			ElseIf (OrderOfSort = SortOrder.Descending) Then
				Return (-compareResult)
			Else
				Return 0
			End If
		Catch ex As Exception
			Return 0
		End Try
	End Function

	Public Property TagSort() As Boolean
		Set(ByVal value As Boolean)
			SortByTag = value
		End Set
		Get
			Return SortByTag
		End Get
	End Property

	Public Property SortColumn() As Integer
		Set(ByVal Value As Integer)
			ColumnToSort = Value
		End Set
		Get
			Return ColumnToSort
		End Get
	End Property

	Public Property Order() As SortOrder
		Set(ByVal Value As SortOrder)
			OrderOfSort = Value
		End Set
		Get
			Return OrderOfSort
		End Get
	End Property
End Class

Public Class ListView_GroupSorter
	Friend _listview As ListView
	' This breaks sorting for some reason.!
	''Public Shared Widening Operator CType(sorter As ListView_GroupSorter) As ListView
	''	Return sorter._listview
	''End Operator
	Public Shared Widening Operator CType(lv As ListView) As ListView_GroupSorter
		Return New ListView_GroupSorter(lv)
	End Operator

	Friend Sub New(listview As ListView)
		_listview = listview
	End Sub
	Public Sub SortGroups(ascending As Boolean, tagSort As Boolean)
		_listview.BeginUpdate()
		Dim lvgs As New List(Of ListViewGroup)()
		For Each lvg As ListViewGroup In _listview.Groups
			lvgs.Add(lvg)
		Next
		_listview.Groups.Clear()
		lvgs.Sort(New ListView_GroupHeaderSorter(ascending, tagSort))
		_listview.Groups.AddRange(lvgs.ToArray())
		_listview.EndUpdate()
	End Sub
End Class

Public Class ListView_GroupHeaderSorter
	Implements IComparer(Of ListViewGroup)
	Private _ascending As Boolean = True
	Private _tagSort As Boolean = False
	Public Sub New(ascending As Boolean, SortByTag As Boolean)
		_ascending = ascending
		_tagSort = SortByTag
	End Sub
	Public Function Compare(x As Windows.Forms.ListViewGroup, y As Windows.Forms.ListViewGroup) As Integer Implements Collections.Generic.IComparer(Of System.Windows.Forms.ListViewGroup).Compare
		If (_ascending) Then
			If _tagSort = True Then
				Return String.Compare(DirectCast(x, ListViewGroup).Name, DirectCast(y, ListViewGroup).Name)
			Else
				Return String.Compare(DirectCast(x, ListViewGroup).Header, DirectCast(y, ListViewGroup).Header)
			End If
		Else
			Return String.Compare(DirectCast(y, ListViewGroup).Header, DirectCast(x, ListViewGroup).Header)
		End If
	End Function
End Class
