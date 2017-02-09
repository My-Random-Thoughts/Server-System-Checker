Imports Microsoft.VisualBasic.ApplicationServices

Namespace My
	Partial Friend Class MyApplication
		Public AIPN As String = vbNullString
		Private Sub MyApplication_NetworkAvailabilityChanged(sender As Object, e As Microsoft.VisualBasic.Devices.NetworkAvailableEventArgs) Handles Me.NetworkAvailabilityChanged
			Dim sMsg As String = "Network availability has been "
			sMsg = sMsg & IIf(e.IsNetworkAvailable, "restored.", "lost." & vbCrLf & sNetworkAvailability).ToString

			If (frmMain.Visible = True) Then
				frmMain.NetworkAvailabilityToolStripStatus.Visible = Not e.IsNetworkAvailable
				frmMain.tssInfo.Text = sMsg
			End If

			MessageBox.Show(sMsg, AIPN, MessageBoxButtons.OK, MessageBoxIcon.Information)
			m_NetworkAvailability = e.IsNetworkAvailable
		End Sub
		Private Sub MyApplication_Shutdown(sender As Object, e As System.EventArgs) Handles Me.Shutdown
			' Good Bye.!
		End Sub
		Private Sub MyApplication_Startup(ByVal sender As Object, ByVal e As StartupEventArgs) Handles Me.Startup
			AIPN = Application.Info.ProductName.ToString

			If (checkDotNetVersion("v3.5") = False) Then
				MessageBox.Show("Microsoft .NET framework version 3.5 is not installed," & vbCrLf &
								"Please install it before using this application." & vbCrLf & vbCrLf & _
								"http://www.microsoft.com/download/details.aspx?id=22", AIPN, MessageBoxButtons.OK, MessageBoxIcon.Error)
				End
			End If

			If (My.Computer.Network.IsAvailable = False) Then
				MessageBox.Show("There is no network availability." & vbCrLf & sNetworkAvailability, AIPN.ToString, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			End If

			If (My.Application.CommandLineArgs.Count > 0) Then
				sConfigFile = vbNullString
				sCommand_Group = vbNullString

				For Each sCommand As String In My.Application.CommandLineArgs
					If (sCommand.Contains("?") = True) Then
						frmHelp.sSelectPageByID = "help0032"
						frmHelp.ShowDialog()
						End
					End If

					If (sCommand.StartsWith("-")) Then
						If (sCommand.StartsWith("-file:")) Then sConfigFile = sCommand.Substring(6)
						If (sCommand.StartsWith("-group:")) Then sCommand_Group = sCommand.Substring(7)
						If (sCommand.StartsWith("-report:")) Then bCommand_BasicReport = CBool(IIf(sCommand.Substring(8).ToLower = "basic", True, False))
					Else
						sConfigFile = sCommand
					End If
				Next

				' Check to make sure we have a full set of commands...
				If ((sConfigFile <> vbNullString) AndAlso (sCommand_Group <> vbNullString)) Then
					bCommandLine = True
					My.Application.MainForm = frmCommandLine
				Else
					bCommandLine = False
					My.Application.MainForm = frmMain
				End If
			Else
				bCommandLine = False
				My.Application.MainForm = frmMain
			End If
		End Sub
		Private Sub MyApplication_StartupNextInstance(sender As Object, e As StartupNextInstanceEventArgs) Handles Me.StartupNextInstance
			MessageBox.Show("This application is already running," & vbCrLf & _
							"Please close that instance before starting again", AIPN, MessageBoxButtons.OK, MessageBoxIcon.Information)
		End Sub
		Private Sub MyApplication_UnhandledException(sender As Object, e As UnhandledExceptionEventArgs) Handles Me.UnhandledException
			frmUnhandledException.uErrorMessage = e
			frmUnhandledException.ShowDialog()
		End Sub
	End Class
End Namespace