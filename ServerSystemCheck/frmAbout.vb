Public Class frmAbout
	Private iManualCrashTickCount As Integer

	Private Sub frmAbout_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
		For Each c As Control In Controls
			c.Font = frmMain.sysFont
		Next
		lblTitle.Font = frmMain.sysFontTitle
		lblTitle.Text = Application.ProductName
		Me.Text = " About" & APN

		picIcon.Image = My.Resources._48___MainIcon.ToBitmap
		lblVersion.Text = Application.ProductVersion
		lblVersion.ForeColor = SystemColors.WindowText
		picBackground.Image = My.Resources.HelpAboutBackground

		Dim tt As New ToolTip
		tt.SetToolTip(lnkLabel_1, lnkLabel_1.Tag.ToString)
		tt.SetToolTip(lnkLabel_2, lnkLabel_2.Tag.ToString)
		tt.SetToolTip(lnkLabel_3, lnkLabel_3.Tag.ToString)

		lnkEmail.LinkBehavior = LinkBehavior.HoverUnderline
		lnkLabel_1.LinkBehavior = LinkBehavior.HoverUnderline
		lnkLabel_2.LinkBehavior = LinkBehavior.HoverUnderline
		lnkLabel_3.LinkBehavior = LinkBehavior.HoverUnderline

		iManualCrashTickCount = 0
	End Sub

	Private Sub lblVersion_Click(sender As System.Object, e As System.EventArgs) Handles lblVersion.Click
		iManualCrashTickCount = iManualCrashTickCount + 1
		If (iManualCrashTickCount = 3) Then lblVersion.ForeColor = getSubItemColour("Warning")
		If (iManualCrashTickCount = 4) Then lblVersion.ForeColor = getSubItemColour("Error")
		If (iManualCrashTickCount = 5) Then Throw New System.Exception("Manual System Exception Created")
	End Sub

	Private Sub cmdCancel_Click(sender As System.Object, e As System.EventArgs) Handles cmdCancel.Click
		Close()
		Me.Dispose()
	End Sub

	Private Sub lnkLabel_1_LinkClicked(sender As System.Object, e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lnkLabel_1.LinkClicked
		Try
			Process.Start("http://www.iconarchive.com")
		Catch
		End Try
	End Sub
	Private Sub lnkLabel_2_LinkClicked(sender As System.Object, e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lnkLabel_2.LinkClicked
		Try
			Process.Start("http://www.microsoft.com/en-us/download/details.aspx?id=35825")
		Catch
		End Try
	End Sub
	Private Sub lnkLabel_3_LinkClicked(sender As System.Object, e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lnkLabel_3.LinkClicked
		Try
			Process.Start("http://greenfishsoftware.blogspot.hu/2012/07/greenfish-icon-editor-pro.html")
		Catch
		End Try
	End Sub
	Private Sub lnkEmail_LinkClicked(sender As System.Object, e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lnkEmail.LinkClicked
		Try
			Process.Start("mailto:" & lnkEmail.Text & "&Subject=Server System Checker&Body=Version: " & Application.ProductVersion & "")
		Catch
		End Try
	End Sub
End Class