Option Explicit On

Public Class ctrlComboBox_Icons
	Inherits Windows.Forms.ComboBox

	Private m_CurrentItem As IconComboItem
	Private m_IconComboItemList As IconComboItemCollection

	''' <summary>
	''' Enumeration to indicate how the IconComboItemCollection gets changed
	''' for various operations
	''' </summary>
	''' <remarks>Only used internally by IconComboBox</remarks>
	Public Enum IconComboItemCollectionChangeType
		ItemAdded = 0
		ItemRemoved
		ItemInserted
		CollectionCleared
	End Enum

	''' <summary>
	''' The collection of IconComboItems used to render the ComboBox.
	''' </summary>
	''' <returns>The collection of IconComboItems used to render the ComboBox.</returns>
	''' <remarks>This property provides a similar interface to the .NET ComboBox,
	''' needing to call ComboBox1.Items.Add("MyStr") to add an item.</remarks>
	Public Overloads ReadOnly Property Items() As IconComboItemCollection
		Get
			Return m_IconComboItemList
		End Get
	End Property

	''' <summary>
	''' Gets the IconComboItem object for the currently selected item in the dropdown.
	''' </summary>
	''' <value></value>
	''' <returns></returns>
	''' <remarks></remarks>
	Public Overloads Property SelectedItem() As IconComboItem
		Get
			If (Me.SelectedIndex >= 0) Then
				Return m_IconComboItemList(Me.SelectedIndex)
			Else
				Return Nothing
			End If
		End Get
		Set(ByVal Value As IconComboItem)
			m_CurrentItem = Value
		End Set
	End Property

	''' <summary>
	''' Creates a new instance of the IconComboBox.
	''' </summary>
	''' <remarks></remarks>
	Public Sub New()
		MyBase.New()
		m_IconComboItemList = New IconComboItemCollection
		AddHandler m_IconComboItemList.CollectionChanged, AddressOf m_IconComboItemList_CollectionItemsChanged
		Me.DrawMode = Windows.Forms.DrawMode.OwnerDrawFixed
		Me.DropDownStyle = ComboBoxStyle.DropDownList
	End Sub

	Private m_removeIconSpacing As Boolean = False
	Public Property RemoveIconSpacing As Boolean
		Get
			Return m_removeIconSpacing
		End Get
		Set(value As Boolean)
			m_removeIconSpacing = value
		End Set
	End Property

	'ComboBox must have its style set to DropDownList
	'Private Sub ExpCombo_DropDownStyleChanged(ByVal sender As Object, ByVal e As System.EventArgs)
	'Me.DropDownStyle = ComboBoxStyle.DropDownList
	'End Sub

	'Make sure font doesn't get set to more than 12 points
	Protected Overrides Sub OnFontChanged(ByVal e As System.EventArgs)
		If (Me.Font.SizeInPoints > 12) Then
			Me.Font = New Font(Me.Font.FontFamily, 12, Me.Font.Style)
		End If
	End Sub

	Protected Overrides Sub OnSelectedIndexChanged(ByVal e As System.EventArgs)
		If (Me.SelectedIndex < 0) Then Exit Sub
		If (Me.SelectedIndex < m_IconComboItemList.Count) Then
			m_CurrentItem = m_IconComboItemList(Me.SelectedIndex)
			MyBase.OnSelectedIndexChanged(e)
		End If
	End Sub

	''' <summary>
	''' Draws an IconComboBox item into the ComboBox in the area specified by the DrawItemEventArgs.
	''' </summary>
	''' <param name="e">Event argument specifying which item to draw and where to draw it.</param>
	''' <remarks></remarks>
	Protected Overrides Sub OnDrawItem(ByVal e As System.Windows.Forms.DrawItemEventArgs)
		If (Me.Enabled = True) Then Me.BackColor = SystemColors.Window Else Me.BackColor = SystemColors.Control
		e.DrawBackground()
		e.DrawFocusRectangle()

		Dim bounds As Rectangle = e.Bounds
		If ((e.Index > -1) AndAlso (e.Index < Me.Items.Count) AndAlso (e.Index < m_IconComboItemList.Count)) Then
			Dim currentItem As IconComboItem = m_IconComboItemList(e.Index)

			If (currentItem IsNot Nothing) Then
				If (currentItem.IsDivider) Then

					' Draw Divider...
					Dim iMiddle As Integer = CInt(bounds.Height / 2) + bounds.Top
					e.Graphics.DrawLine(SystemPens.ButtonShadow, New Point(bounds.Left + 9, iMiddle - 1), New Point(bounds.Width - 9, iMiddle - 1))
					e.Graphics.DrawLine(SystemPens.ButtonFace, New Point(bounds.Left + 9, iMiddle), New Point(bounds.Width - 9, iMiddle))
				Else

					' Draw Item...
					Dim iIcon As Icon = currentItem.ItemImage
					Dim sDisplayText As String = currentItem.DisplayText	'Items(e.Index).ToString
					Dim iIndent As Integer = currentItem.IndentCount * 14
					Dim bBrush As SolidBrush = New SolidBrush(e.ForeColor)
					If (currentItem.IsEnabled = False) Then
						iIcon = GreyScaleIcon(iIcon, True)
						bBrush.Color = SystemColors.GrayText
					End If

					Dim rRectangle As Rectangle
					If (RemoveIconSpacing = False) Then
						Dim imageRectangle As New Rectangle(bounds.Left + 5 + iIndent, bounds.Top + 2, 16, 16)
						If (iIcon IsNot Nothing) Then e.Graphics.DrawIcon(iIcon, imageRectangle)
						rRectangle = New Rectangle(bounds.Left + imageRectangle.Width + 9 + iIndent, bounds.Top, bounds.Width - imageRectangle.Width - 9 - iIndent, bounds.Height)
					Else
						rRectangle = New Rectangle(bounds.Left + 9 + iIndent, bounds.Top, bounds.Width - iIndent, bounds.Height)
					End If

					Using format As New StringFormat()
						format.LineAlignment = StringAlignment.Center
						format.Alignment = StringAlignment.Near
						e.Graphics.DrawString(sDisplayText, e.Font, bBrush, rRectangle, format)
					End Using
				End If
			Else
				Application.DoEvents()
			End If
		End If

		MyBase.OnDrawItem(e)
	End Sub

	''' <summary>
	''' Add a divider line to the ComboBox.  The line will be appended to the end of the list.
	''' </summary>
	''' <returns>The index of the added divider.</returns>
	''' <remarks></remarks>
	Public Function AddDivider() As Integer
		Dim tempItem As New IconComboItem
		tempItem.DisplayText = ""
		tempItem.IsDivider = True
		Return Me.Items.Add(tempItem)
	End Function

	' We are forced to raise an event from the IconComboItemCollection to inform us when it changes -
	' e.g. the user calls Items.Clear, Items.Add, or Items.Remove.  We need to be notified of this so that
	' we can remove the corresponding item from the base ComboBox object collection.
	Private Sub m_IconComboItemList_CollectionItemsChanged(ByVal sender As Object, ByVal e As IconComboItemCollectionChangedEventArgs)
		Select Case e.ChangeType
			Case IconComboItemCollectionChangeType.ItemAdded : MyBase.Items.Add(e.ChangedItem.DisplayText)
			Case IconComboItemCollectionChangeType.ItemInserted : MyBase.Items.Insert(e.ChangedIndex, e.ChangedItem.DisplayText)
			Case IconComboItemCollectionChangeType.ItemRemoved : MyBase.Items.Remove(e.ChangedItem.DisplayText)
			Case IconComboItemCollectionChangeType.CollectionCleared : MyBase.Items.Clear()
		End Select
	End Sub

#Region "Class IconComboItem"
	''' <summary>
	''' Object representing an entry in the IconComboBox.
	''' </summary>
	''' <remarks>Implements IEquatable for searching and sorting the IconComboItemCollection.</remarks>
	Public Class IconComboItem
		Implements IEquatable(Of IconComboItem)

		Private m_Icon As Icon
		Private m_Data As String
		Private m_DisplayText As String
		Private m_isFolder As Boolean
		Private m_isDivider As Boolean
		Private m_IndentCount As Integer
		Private m_isEnabled As Boolean

		''' <summary>
		''' The image to be displayed next to the text for this combo box item.
		''' </summary>
		''' <value>The icon to be displayed</value>
		''' <returns>The icon currently being displayed</returns>
		''' <remarks></remarks>
		Public Property ItemImage() As Icon
			Get
				Return m_Icon
			End Get
			Set(ByVal value As Icon)
				m_Icon = value
			End Set
		End Property

		''' <summary>
		''' The string representing data you want associated with this IconComboItem.
		''' </summary>
		''' <value>The data to be set</value>
		''' <returns>The data currently saved</returns>
		''' <remarks></remarks>
		Public Property Data() As String
			Get
				Return m_Data
			End Get
			Set(ByVal value As String)
				m_Data = value
			End Set
		End Property

		''' <summary>
		''' What to display for this item.
		''' </summary>
		''' <value></value>
		''' <returns></returns>
		''' <remarks></remarks>
		Public Property DisplayText() As String
			Get
				Return m_DisplayText
			End Get
			Set(ByVal value As String)
				m_DisplayText = value
			End Set
		End Property

		''' <summary>
		''' Indicates whether this IconComboItem is a Divider.
		''' </summary>
		''' <value>Boolean indicating if this item is a divider or not.  This property can only be SET by the IconComboBox.</value>
		''' <returns>True if this is a divider; False if it is not.</returns>
		''' <remarks></remarks>
		Public Property IsDivider() As Boolean
			Get
				Return m_isDivider
			End Get
			' This property is Friend so that ONLY THE IconComboBox can set this property.
			Friend Set(ByVal value As Boolean)
				m_isDivider = value
			End Set
		End Property

		''' <summary>
		''' Indicates whether this IconComboItem is a folder.
		''' </summary>
		''' <value>Boolean indicating if this item is a folder or not.  This property can only be SET by the IconComboBox.</value>
		''' <returns>True if this is a folder; False if it is not.</returns>
		''' <remarks></remarks>
		Public Property IsFolder() As Boolean
			Get
				Return m_isFolder
			End Get
			' This property is Friend so that ONLY THE IconComboBox can set this property.
			Friend Set(ByVal value As Boolean)
				m_isFolder = value
			End Set
		End Property

		''' <summary>
		''' Indicates whether this IconComboItem is enabled.
		''' </summary>
		''' <value>Boolean indicating if this item is enabled or not.  This property can only be SET by the IconComboBox.</value>
		''' <returns>True if this is enabled; False if it is not.</returns>
		''' <remarks></remarks>
		Public Property IsEnabled() As Boolean
			Get
				Return m_isEnabled
			End Get
			' This property is Friend so that ONLY THE IconComboBox can set this property.
			Friend Set(ByVal value As Boolean)
				m_isEnabled = value
			End Set
		End Property

		''' <summary>
		''' Indent amount of this IconComboItem
		''' </summary>
		''' <value></value>
		''' <returns></returns>
		''' <remarks></remarks>
		Public Property IndentCount As Integer
			Get
				Return m_IndentCount
			End Get
			Set(value As Integer)
				m_IndentCount = value
			End Set
		End Property

		Public Sub New()
		End Sub

		''' <summary>
		''' Create a new IconComboItem with the specified values
		''' </summary>
		''' <param name="Text">The text to display in the combo box</param>
		''' <param name="Data">The string representing this IconComboItem's data</param>
		''' <remarks></remarks>
		Public Sub New(ByVal Text As String, Optional ByVal Data As String = vbNullString)
			m_DisplayText = Text
			m_Data = Data
			m_isEnabled = True
		End Sub

		Public Shared Operator =(ByVal item1 As IconComboItem, ByVal item2 As IconComboItem) As Boolean
			Return item1.Data = item2.Data
		End Operator

		Public Shared Operator <>(ByVal item1 As IconComboItem, ByVal item2 As IconComboItem) As Boolean
			Return item1.Data <> item2.Data
		End Operator

		''' <summary>
		''' Returns if this IconComboItem is equal to the specified one.
		''' </summary>
		''' <returns>True if this item's Data property equals the other item's Data property.</returns>
		''' <remarks></remarks>
		Public Overloads Function Equals(ByVal ComboItem As IconComboItem) As Boolean Implements System.IEquatable(Of IconComboItem).Equals
			Return m_Data = ComboItem.Data
		End Function
		Public Overrides Function Equals(obj As Object) As Boolean
			Return MyBase.Equals(obj)
		End Function
	End Class
#End Region

#Region "Class IconComboItemCollection"
	Public Class IconComboItemCollection

		Private m_List As New Generic.List(Of ctrlComboBox_Icons.IconComboItem)
		Friend Event CollectionChanged(ByVal sender As Object, ByVal e As IconComboItemCollectionChangedEventArgs)

		''' <summary>
		''' The number of items in the current collection
		''' </summary>
		''' <value></value>
		''' <returns></returns>
		''' <remarks></remarks>
		Public ReadOnly Property Count() As Integer
			Get
				Return m_List.Count
			End Get
		End Property

		''' <summary>
		''' Gets the IconComboItem at the specified index
		''' </summary>
		''' <param name="index">The index of the IconComboItem to return</param>
		''' <value></value>
		''' <returns>The IconComboItem at the specified index, or nothing if the index is out of range.</returns>
		''' <remarks></remarks>
		Default Public ReadOnly Property Item(ByVal Index As Integer) As IconComboItem
			Get
				If (Index < m_List.Count) Then
					Return m_List(Index)
				Else
					Return Nothing
				End If
			End Get
		End Property

		''' <summary>
		''' Add the specified IconComboItem to the end of the collection.
		''' </summary>
		''' <param name="ComboItem">The IconComboItem to add.  Can be Null.</param>
		''' <returns>The zero-based index where the IconComboItem was added.</returns>
		''' <remarks></remarks>
		Public Function Add(ByVal ComboItem As IconComboItem) As Integer
			m_List.Add(ComboItem)
			RaiseEvent CollectionChanged(Me, New IconComboItemCollectionChangedEventArgs(m_List.Count - 1, ctrlComboBox_Icons.IconComboItemCollectionChangeType.ItemAdded, ComboItem))
			Return m_List.Count - 1
		End Function

		''' <summary>
		''' Insert the specified IconComboItem at the specified index.
		''' </summary>
		''' <param name="Index"></param>
		''' <param name="ComboItem"></param>
		''' <returns></returns>
		''' <remarks></remarks>
		Public Function Insert(ByVal Index As Integer, ByVal ComboItem As IconComboItem) As Boolean
			m_List.Insert(Index, ComboItem)
			If (m_List(Index) = ComboItem) Then
				RaiseEvent CollectionChanged(Me, New IconComboItemCollectionChangedEventArgs(Index, ctrlComboBox_Icons.IconComboItemCollectionChangeType.ItemInserted, ComboItem))
				Return True
			End If
			Return False
		End Function

		Public Function Remove(ByVal ComboItem As IconComboItem) As Boolean
			If (m_List.Remove(ComboItem)) Then
				RaiseEvent CollectionChanged(Me, New IconComboItemCollectionChangedEventArgs(0, ctrlComboBox_Icons.IconComboItemCollectionChangeType.ItemRemoved, ComboItem))
				Return True
			End If
			Return False
		End Function

		''' <summary>
		''' Clear all the items from this collection and from the combo box.
		''' </summary>
		''' <remarks></remarks>
		Public Sub Clear()
			m_List.Clear()
			RaiseEvent CollectionChanged(Me, New IconComboItemCollectionChangedEventArgs(-1, IconComboItemCollectionChangeType.CollectionCleared, Nothing))
		End Sub

		''' <summary>
		''' Determine if the collection contains the specified IconComboItem, using the
		''' <see>IconComboItem.Equals</see> method for comparison.
		''' </summary>
		''' <param name="ComboItem"></param>
		''' <returns></returns>
		''' <remarks></remarks>
		Public Function Contains(ByVal ComboItem As IconComboItem) As Boolean
			Return m_List.Contains(ComboItem)
		End Function

		''' <summary>
		''' Searches for the specified IconComboItem and returns the zero-based index of the first
		''' occurrence within the entire IconComboItemCollection.
		''' </summary>
		''' <param name="ComboItem">The object to locate in the IconComboItemCollection.
		''' The value can be null for reference types.</param>
		''' <returns>The zero-based index of the first occurrence of item within
		''' the entire IconComboItemCollection, if found; otherwise, –1.</returns>
		''' <remarks></remarks>
		Public Function IndexOf(ByVal ComboItem As IconComboItem) As Integer
			Return m_List.IndexOf(ComboItem)
		End Function
	End Class
#End Region

#Region "Friend Class IconComboItemCollectionChangedEventArgs"
	''' <summary>
	''' These args are used in events indicating that the IconComboItemCollection has been
	''' changed.  This class can only be used by the IconComboBox.
	''' </summary>
	''' <remarks></remarks>
	Friend Class IconComboItemCollectionChangedEventArgs
		Inherits EventArgs

		Private m_ChangeType As ctrlComboBox_Icons.IconComboItemCollectionChangeType
		Private m_ChangedItem As ctrlComboBox_Icons.IconComboItem
		Private m_ChangedIndex As Integer

		''' <summary>
		''' Indicates how the IconComboItemCollection was changed - add, delete, remove, or clear.
		''' </summary>
		''' <value></value>
		''' <returns></returns>
		''' <remarks></remarks>
		Public ReadOnly Property ChangeType() As ctrlComboBox_Icons.IconComboItemCollectionChangeType
			Get
				Return m_ChangeType
			End Get
		End Property

		''' <summary>
		''' The item that was changed resulting in this event.
		''' </summary>
		''' <value></value>
		''' <returns></returns>
		''' <remarks></remarks>
		Public ReadOnly Property ChangedItem() As ctrlComboBox_Icons.IconComboItem
			Get
				Return m_ChangedItem
			End Get
		End Property

		''' <summary>
		''' The index of the changed item in the IconComboItemCollection.
		''' </summary>
		''' <value></value>
		''' <returns></returns>
		''' <remarks></remarks>
		Public ReadOnly Property ChangedIndex() As Integer
			Get
				Return m_ChangedIndex
			End Get
		End Property

		''' <summary>
		''' Create a new instance of these event args with the specified arguments
		''' </summary>
		''' <param name="Index">Index of the changed item</param>
		''' <param name="ComboType">How the item was changed. <seealso>IconComboBox.IconComboItemCollectionChangeType</seealso></param>
		''' <param name="ComboItem">The <see>IconComboItem</see> that was changed</param>
		''' <remarks></remarks>
		Public Sub New(ByVal Index As Integer, ByVal ComboType As ctrlComboBox_Icons.IconComboItemCollectionChangeType, ByVal ComboItem As ctrlComboBox_Icons.IconComboItem)
			m_ChangedIndex = Index
			m_ChangeType = ComboType
			m_ChangedItem = ComboItem
		End Sub
	End Class
#End Region
End Class