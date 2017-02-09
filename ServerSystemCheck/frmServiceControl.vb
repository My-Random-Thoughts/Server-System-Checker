Imports System.ServiceProcess
Imports System.Management

Public Class frmServiceControl

	Public bCheckStarting As Boolean
	Public sService As ServiceController
	Public sServerName As String
	Public bResult As Boolean = False
	Private bLeaveNow As Boolean
	Private WithEvents tTimer As New Timer

	Private Sub frmServiceControl_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
		For Each c As Control In Controls
			c.Font = frmMain.sysFont
		Next
		lblTitle.Font = frmMain.sysFontTitle

		picIcon.Image = My.Resources._48___Services.ToBitmap
		lblTitle.Text = "..."
		lblStatus.Text = sService.DisplayName

		proProgress.Value = 0
		proProgress.Maximum = 100
		proProgress.Style = ProgressBarStyle.Continuous

		bLeaveNow = False
		tTimer.Enabled = False
		tTimer.Interval = 100		' 1000 = 1 second

		Try
			If (bCheckStarting = False) Then
				' Check for dependent services...
				If sService.DependentServices.Count > 0 Then
					Dim sMSG As String = "The following services depend on this service..." & vbCrLf & vbCrLf
					For Each scD As ServiceController In sService.DependentServices
						sMSG = sMSG & "    " & scD.DisplayName & vbCrLf
					Next
					sMSG = sMSG & vbCrLf & "Do you want to continue.?"
					Dim iResult As DialogResult = MessageBox.Show(Me, sMSG, " " & sService.DisplayName, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
					If iResult = Windows.Forms.DialogResult.No Then bLeaveNow = True
				End If

			Else
				' Check if service is disabled...
				Using oResult As ManagementObjectCollection = wmiConnect("\\" & sServerName & "\root\cimv2", "SELECT * FROM Win32_Service", 10)
					For Each mo As ManagementObject In oResult
						If mo("DisplayName").ToString = sService.DisplayName Then
							If mo("StartMode").ToString = "Disabled" Then
								MessageBox.Show(Me, "This service is currently disabled." & vbCrLf & "It can't be started from here", " " & sService.DisplayName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
								bLeaveNow = True
								Exit For
							End If
						End If
					Next
				End Using
			End If

			If (bLeaveNow = False) Then
				Me.Visible = True
				Application.DoEvents()
				tTimer.Enabled = True
				bResult = False

				Dim sCurrentStatus As ServiceControllerStatus
				Dim sPreviousStatus As ServiceControllerStatus

				If (bCheckStarting = True) Then lblTitle.Text = "Starting..." Else lblTitle.Text = "Stopping..."
				lblTitle.Refresh()

				Do
					Application.DoEvents()
				Loop Until (proProgress.Value = 10)

				sService.Refresh()
				If (bCheckStarting = True) Then
					sPreviousStatus = ServiceControllerStatus.Running
					sService.Start()
				Else
					sPreviousStatus = ServiceControllerStatus.Stopped
					sService.Stop()
				End If

				Do
					sService.Refresh()
					sCurrentStatus = sService.Status

					If (sCurrentStatus <> sPreviousStatus) Then
						If (bCheckStarting = True) Then
							If (((sPreviousStatus = ServiceControllerStatus.Running) OrElse (sPreviousStatus = ServiceControllerStatus.StartPending)) AndAlso _
							  ((sCurrentStatus = ServiceControllerStatus.Stopped) OrElse (sCurrentStatus = ServiceControllerStatus.StopPending))) Then
								tTimer.Enabled = False
								Me.Visible = False
								Dim sMsg As String = "The '" & sService.DisplayName & "'service on '" & sService.MachineName.ToUpper
								sMsg = sMsg & "' started and then stopped. Some services stop automatically if they are not in use by other services or programs."
								MessageBox.Show(Me, sMsg, APN, MessageBoxButtons.OK, MessageBoxIcon.Information)
								bResult = True
								Exit Do
							End If
						End If
					End If

					Select Case sCurrentStatus
						Case ServiceControllerStatus.Running : If (bCheckStarting = True) Then bResult = True : lblTitle.Text = "Started"
						Case ServiceControllerStatus.Stopped : If (bCheckStarting = False) Then bResult = True : lblTitle.Text = "Stopped"
						Case ServiceControllerStatus.StartPending : lblTitle.Text = "Starting..."
						Case ServiceControllerStatus.StopPending : lblTitle.Text = "Stopping..."
						Case Else : lblTitle.Text = "Unknown State"
					End Select
					lblTitle.Refresh()
					sPreviousStatus = sService.Status

					If (bResult = True) Then
						tTimer.Enabled = False
						Me.Visible = False
						MessageBox.Show(Me, sService.DisplayName & " is now " & sCurrentStatus.ToString, APN, MessageBoxButtons.OK, MessageBoxIcon.Information)
						Exit Do
					End If

					Application.DoEvents()
				Loop Until (proProgress.Value >= proProgress.Maximum)
			End If

			If (bResult = False) Then
				MessageBox.Show(Me, "The service took too long to " & IIf(bCheckStarting, "start", "stop").ToString, APN, MessageBoxButtons.OK, MessageBoxIcon.Error)
			End If

		Catch ex As Exception
			tTimer.Enabled = False
			Me.Visible = False
			Dim sInner As String = vbNullString
			If ex.InnerException IsNot Nothing Then sInner = vbCrLf & ex.InnerException.Message
			MessageBox.Show(Me, ex.Message & sInner, APN, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
		End Try

		Call closeForm()
	End Sub

	Private Sub timTimer_Tick(sender As System.Object, e As System.EventArgs) Handles tTimer.Tick

		' |-----------x-----x-----|		100%
		' 0           1     2     3
		'
		' 0  Start
		' 1  Half way Point (100ms)		 0 -  50	 5
		' 2  Three Quarters (500ms)		51 -  75	12
		' 3  End Point      (1000ms)	76 - 100	24
		' ---------------------------------------- ----
		' Command doesn't run until 0.5 seconds     41
		'
		If (proProgress.Value < proProgress.Maximum) Then proProgress.Value = proProgress.Value + 1

		Select Case proProgress.Value
			Case 0 To 50 : tTimer.Interval = 80
			Case 51 To 75 : tTimer.Interval = 500
			Case 76 To 100 : tTimer.Interval = 1000
		End Select

		proProgress.Refresh()
		Application.DoEvents()
	End Sub

	Private Sub closeForm()
		tTimer.Enabled = False
		If (sService IsNot Nothing) Then sService.Dispose()
		Me.Close()
		Me.Dispose()
	End Sub
End Class