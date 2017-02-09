Public Class Interval
	Private intLower As Integer	' LEFT
	Private intUpper As Integer	' RIGHT

	Public Sub New(iLower As Integer, iUpper As Integer)
		If (iLower > iUpper) Then iUpper = iLower
		intLower = iLower
		intUpper = iUpper
	End Sub

	Public Function LessThan(iInput As Interval) As Boolean
		If (intUpper < iInput.intLower) Then Return True
		Return False
	End Function

	Public Function MoreThan(iInput As Interval) As Boolean
		If (intLower > iInput.intUpper) Then Return True
		Return False
	End Function

	Public Function Contains(iInput As Interval, iCheck As Interval) As Boolean
		If ((iCheck.intLower >= iInput.intLower) AndAlso (iCheck.intUpper <= iInput.intUpper)) Then Return True
		Return False
	End Function

	Public Function LeftMost(iInput As Interval) As Integer
		Return Math.Min(intLower, iInput.intLower)
	End Function

	Public Function RightMost(iInput As Interval) As Integer
		Return Math.Max(intUpper, iInput.intUpper)
	End Function

	Public ReadOnly Property getLower As Integer
		Get
			Return intLower
		End Get
	End Property

	Public ReadOnly Property getUpper As Integer
		Get
			Return intUpper
		End Get
	End Property

	Public Function ReturnInterval() As String
		If (intLower = intUpper) Then Return intLower.ToString Else Return intLower.ToString & "-" & intUpper.ToString
	End Function
End Class

Public Class IntervalStore
	Private iL_Intervals As List(Of Interval)

	Public Sub New()
		iL_Intervals = New List(Of Interval)
	End Sub

	Public Sub addInterval(intLower As Integer, intUpper As Integer)
		Dim iAddInt As Interval = New Interval(intLower, intUpper)
		Dim iOffSet As Interval = New Interval(intLower - 1, intUpper + 1)

		If (iL_Intervals Is Nothing) Then
			iL_Intervals.Add(iAddInt)
			Return
		End If

		Dim iL_Before As List(Of Interval) = iL_Intervals.FindAll(Function(i) iOffSet.MoreThan(i))
		Dim iL_After As List(Of Interval) = iL_Intervals.FindAll(Function(i) iOffSet.LessThan(i))

		Dim iNew As Interval
		If ((iL_Before.Count = iL_Intervals.Count) Or (iL_After.Count = iL_Intervals.Count)) Then
			iNew = iAddInt
		Else
			iNew = New Interval(iAddInt.LeftMost(iL_Intervals(iL_Before.Count)), iAddInt.RightMost(iL_Intervals((iL_Intervals.Count - iL_After.Count) - 1)))
		End If

		iL_Intervals = iL_Before
		iL_Intervals.Add(iNew)
		iL_Intervals.AddRange(iL_After)
	End Sub

	Public Sub delInterval(intLower As Integer, intUpper As Integer)
		Dim iDelInt As Interval = New Interval(intLower, intUpper)

		Dim iL_Before As List(Of Interval) = iL_Intervals.FindAll(Function(i) iDelInt.MoreThan(i))
		Dim iL_After As List(Of Interval) = iL_Intervals.FindAll(Function(i) iDelInt.LessThan(i))
		Dim iL_Range As Interval = iL_Intervals.Find(Function(i) iDelInt.Contains(i, iDelInt))

		If (iL_Range IsNot Nothing) Then
			iL_Intervals = iL_Before
			If (intLower <> iL_Range.getLower) Then iL_Intervals.Add(New Interval(iL_Range.getLower, intLower - 1))
			If (intUpper <> iL_Range.getUpper) Then iL_Intervals.Add(New Interval(intUpper + 1, iL_Range.getUpper))
			iL_Intervals.AddRange(iL_After)
		End If
	End Sub

	Public Function ReturnIntervals() As String
		Dim sReturn As String = vbNullString
		For Each iInterval As Interval In iL_Intervals
			sReturn = sReturn & (iInterval.ReturnInterval) & ","
		Next
		If (sReturn IsNot Nothing) Then Return sReturn.Trim(","c).Replace(",", ", ") Else Return Nothing
	End Function
End Class
