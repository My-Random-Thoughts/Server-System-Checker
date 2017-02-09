Option Explicit On
Imports System.ServiceProcess
Imports System.Management
Imports System.Text

Public Class frmScanReport
	Private iSectionCount As Integer = 7						' Number of sections.!
	Private sSpaceX As String = vbNullString
	Public sTimeTaken As TimeSpan								' Time taken to run report, taken from frmScan.swStopWatch
	Public sServiceEntries As List(Of ServiceController)
	Public hHotfixEntries As List(Of String)
	Public eEventLogEntries As List(Of List(Of EventLogEntry))
	Public rRegistryEntries As List(Of String)
	Public fFileCheckEntries As List(Of String)
	Public wWMIQueryEntries As List(Of String)
	Public dFreeSpaceSize As List(Of ManagementObject)

	Private Sub frmReport_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
		For Each c As Control In Controls
			c.Font = frmMain.sysFont
		Next
		lblTitle.Font = frmMain.sysFontTitle
		lnkHelp.Font = frmMain.sysFontHelp

		lnkHelp.LinkBehavior = LinkBehavior.HoverUnderline
		lnkSelect_All.LinkBehavior = LinkBehavior.HoverUnderline
		lnkSelect_None.LinkBehavior = LinkBehavior.HoverUnderline
		lnkShowExamples.LinkBehavior = LinkBehavior.HoverUnderline

		picIcon.Image = My.Resources._48___Report.ToBitmap
		proProgress.Value = 0
		proProgress.Visible = False
		lblServerName.Enabled = False
		lblServerName.Text = vbNullString
		lnkShowExamples.Visible = True
		Call radSimple_CheckedChanged(sender, e)
		Call lnkSelect_None_LinkClicked(Nothing, Nothing)

		lnkSelect_All.Left = lblSelect.Left + lblSelect.Width
		lnkSelect_None.Left = lnkSelect_All.Left + lnkSelect_All.Width

		Me.Visible = True
		Application.DoEvents()
		radSimple.Focus()
	End Sub

	Private Sub cmdCancel_Click(sender As System.Object, e As System.EventArgs) Handles cmdCancel.Click
		Me.Close()
		Me.Dispose()
	End Sub

	Public Sub radSimple_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles radSimple.CheckedChanged
		Call changeReportOptions(False)
	End Sub

	Public Sub radDetailed_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles radDetailed.CheckedChanged
		Call changeReportOptions(True)
	End Sub

	Private Sub changeReportOptions(ByVal bEnable As Boolean)
		chkServices.Enabled = bEnable
		chkHotfixes.Enabled = bEnable
		chkEventlog.Enabled = bEnable
		chkRegKeys.Enabled = bEnable
		chkFiles.Enabled = bEnable
		chkWMI.Enabled = bEnable
		chkDriveSytem.Enabled = bEnable
		chkDrives_All.Enabled = bEnable
		chkDrives_Excluded.Enabled = bEnable
		If (bEnable = True) Then
			Call chkDriveSytem_CheckedChanged(Nothing, Nothing)
			Call chkDrives_All_CheckedChanged(Nothing, Nothing)
		End If
		lblSelect.Visible = bEnable
		lnkSelect_All.Visible = bEnable
		lnkSelect_None.Visible = bEnable

		picImage_S.Image = changeReportImage(bEnable, My.Resources._16___Resource___Services)
		picImage_H.Image = changeReportImage(bEnable, My.Resources._16___Resource___Hotfix)
		picImage_E.Image = changeReportImage(bEnable, My.Resources._16___Resource___Eventlog)
		picImage_R.Image = changeReportImage(bEnable, My.Resources._16___Resource___Registry)
		picImage_F.Image = changeReportImage(bEnable, My.Resources._16___Resource___File)
		picImage_W.Image = changeReportImage(bEnable, My.Resources._16___Resource___WMIQuery)
		picImage_D.Image = changeReportImage(bEnable, My.Resources._16___Resource___Drive)
	End Sub
	Private Function changeReportImage(ByVal bEnable As Boolean, ByVal ico As Icon) As Image
		Dim iReturn As Image = Nothing
		If (bEnable = False) Then iReturn = GreyScaleImage(ico.ToBitmap, True) Else iReturn = ico.ToBitmap
		Return iReturn
	End Function

	Private Sub lnkHelp_LinkClicked(sender As System.Object, e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lnkHelp.LinkClicked
		frmHelp.sSelectPageByID = "help0019"
		frmHelp.ShowDialog(Me)
	End Sub

	Public Sub cmdExport_Click(sender As System.Object, e As System.EventArgs) Handles cmdExport.Click
		Dim dtNow As String = System.DateTime.Now.ToString("yyyy-MM-dd HH-mm")
		Dim sReportFile As String = Application.StartupPath & "\Server Report (" & dtNow & ").html"
		Dim swOutputFile As New StringBuilder

		swOutputFile.Append(doReport__Header)
		With frmScan.lstResults
			proProgress.Maximum = .Items.Count + 1
			proProgress.Value = 0
			proProgress.Visible = True
			lnkShowExamples.Visible = False

			For Each lItem As ListViewItem In .Items
				lblServerName.Text = lItem.Text.Trim
				If (proProgress.Value < proProgress.Maximum) Then proProgress.Value = proProgress.Value + 1
				sSpaceX = StringRepeat(lItem.IndentCount * 7, "&nbsp;")
				Dim sOutput As New StringBuilder

				If (lItem.ImageKey.StartsWith("_16___Server") = False) Then
					'  Write Out GROUPS...
					sOutput.Append("    <tr class='nohover'>" & vbCrLf)
					sOutput.Append("        <td colspan='" & CInt(IIf(radSimple.Checked = True, iSectionCount + 1, 2)) & "' class='left'><i>")
					sOutput.Append(sSpaceX & getHTMLIcon(lItem.ImageKey, 16) & lItem.Text.Trim & "</i></td>" & vbCrLf)
					sOutput.Append("    </tr>" & vbCrLf)
					swOutputFile.Append(sOutput)
				Else
					' Write Out SERVERS
					sOutput.Append("    <tr>" & vbCrLf)
					sOutput.Append("        <td class='topl'><span>" & sSpaceX & getHTMLIcon(lItem.ImageKey, 16) & lItem.Text.Trim & "</span>")
					If (chkIncludeUptime.Checked = True) Then
						Dim sUpTime As String = Split(lItem.Name, "|")(1).ToString.Replace("Uptime: ", vbNullString)
						If (sUpTime <> "Unknown") Then sOutput.Append("<span class='topr'>" & sUpTime & "</span>")
					End If
					sOutput.Append("</td>" & vbCrLf)

					swOutputFile.Append(sOutput)
					Application.DoEvents()

					' Load the data...
					sServiceEntries = CType(lItem.SubItems(1).Tag, List(Of System.ServiceProcess.ServiceController))
					hHotfixEntries = CType(lItem.SubItems(2).Tag, List(Of String))
					eEventLogEntries = CType(lItem.SubItems(3).Tag, List(Of List(Of EventLogEntry)))
					rRegistryEntries = CType(lItem.SubItems(4).Tag, List(Of String))
					fFileCheckEntries = CType(lItem.SubItems(5).Tag, List(Of String))
					wWMIQueryEntries = CType(lItem.SubItems(6).Tag, List(Of String))
					dFreeSpaceSize = CType(lItem.SubItems(7).Tag, List(Of System.Management.ManagementObject))

					sOutput.Length = 0
					If (lItem.SubItems(1).Name = "Failed to connect to server") Then
						sOutput.Append("        <td class='left' colspan='" & iSectionCount & "'>" & getHTMLIcon("_16___Scan___Unknown", 16) & "Unable to connect to server</td>" & vbCrLf)
						sOutput.Append("    </tr>" & vbCrLf)
						swOutputFile.Append(sOutput)
					Else
						sSpaceX = StringRepeat(7, "&nbsp;")
						If (radDetailed.Checked = True) Then swOutputFile.Append("        <td class='left'>" & vbCrLf)
						swOutputFile.Append(doReport_Services(lItem))
						swOutputFile.Append(doReport_Hotfixes(lItem))
						swOutputFile.Append(doReport_Eventlog(lItem))
						swOutputFile.Append(doReport_Registry(lItem))
						swOutputFile.Append(doReport_FileScan(lItem))
						swOutputFile.Append(doReport_WMIQuery(lItem))
						swOutputFile.Append(doReport_FreeSpace(lItem))
						If (radDetailed.Checked = True) Then swOutputFile.Append(vbCrLf & "        </td>" & vbCrLf)
						swOutputFile.Append("    </tr>" & vbCrLf)
					End If
				End If
			Next

			lblServerName.Text = vbNullString
			lnkShowExamples.Visible = True
			proProgress.Visible = False
			proProgress.Value = 0
			swOutputFile.Append("</table>" & vbCrLf & "<br/>Report Generated : " & dtNow & vbCrLf)
			swOutputFile.Append("<br/>Time Taken : " & sTimeTaken.Minutes & " minutes, " & sTimeTaken.Seconds & " seconds." & vbCrLf & "</body>" & vbCrLf & "</html>")
			Application.DoEvents()

			Using swFile As New IO.StreamWriter(sReportFile, False, System.Text.Encoding.ASCII)
				swFile.Write(swOutputFile)
				Application.DoEvents()
				swFile.Close()
			End Using
			swOutputFile = Nothing
		End With

		Application.DoEvents()
		If (bCommandLine = False) Then
			Dim iResult As DialogResult = MessageBox.Show(Me, "Report complete, do you want to open it now.?", APN, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
			If (iResult = Windows.Forms.DialogResult.Yes) Then Process.Start(sReportFile)
		End If

		Close()
	End Sub

	' #############################################################################################

	Private Function doReport__Header() As String
		Dim htmlHeader As New StringBuilder
		htmlHeader.Append("<!DOCTYPE html PUBLIC '-//W3C//DTD XHTML 1.0 Transitional//EN'" & vbCrLf)
		htmlHeader.Append("'http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd'>" & vbCrLf)
		htmlHeader.Append("<html xmlns='http://www.w3.org/1999/xhtml'>" & vbCrLf)
		htmlHeader.Append("<head>" & vbCrLf)
		htmlHeader.Append("    <title>Server Status Report</title>" & vbCrLf)
		htmlHeader.Append("    <meta http-equiv='Content-Type' content='text/html; charset=utf-8'>" & vbCrLf)
		htmlHeader.Append("    <style type='text/css'>" & vbCrLf)
		htmlHeader.Append("        body{font-family:Verdana,sans-serif;font-size:11px;margin: 0 auto;padding:10px;color:#000000;background:#FFFFFF;line-height:1.182em; }" & vbCrLf)
		htmlHeader.Append("        tr:hover{background:#F0F0F0;cursor:default;}" & vbCrLf)
		htmlHeader.Append("        tr.nohover:hover{background:#FFFFFF!important;cursor:default;}" & vbCrLf)
		htmlHeader.Append("        .header{background:#F0F0F0;font-weight:bold;}" & vbCrLf)
		htmlHeader.Append("        td{white-space:nowrap;border-left:1px solid white;border-top:1px solid white;}" & vbCrLf)
		htmlHeader.Append("        .tdnone{white-space:nowrap;border:0px;}" & vbCrLf)
		htmlHeader.Append("        .tableformat{border-right:1px solid #c0c0c0;border-bottom:1px solid #c0c0c0;}" & vbCrLf)
		htmlHeader.Append("        .left{text-align:left;}" & vbCrLf)
		htmlHeader.Append("        .center{text-align:center;}" & vbCrLf)
		htmlHeader.Append("        .right{text-align:right;}" & vbCrLf)
		htmlHeader.Append("        .floatr{float:right;}" & vbCrLf)
		htmlHeader.Append("        .topl{vertical-align:top;text-align:left;}" & vbCrLf)
		htmlHeader.Append("        .topr{vertical-align:top;float:right;}" & vbCrLf)
		htmlHeader.Append("    </style>" & vbCrLf)
		htmlHeader.Append("</head>" & vbCrLf)
		htmlHeader.Append("<body>" & vbCrLf)
		htmlHeader.Append("<center><h1>Server Status Report</h1></center>" & vbCrLf)
		htmlHeader.Append("<br/><br/>" & vbCrLf)
		htmlHeader.Append("<table class='tableformat' cellspacing='0' cellpadding='6' border='1'>" & vbCrLf)
		htmlHeader.Append("    <tr class='header'>" & vbCrLf)
		htmlHeader.Append("        <td width='100%'><span>Server Name</span>")
		If (chkIncludeUptime.Checked = True) Then htmlHeader.Append("<span class='floatr'>Uptime</span>")
		htmlHeader.Append("</td>" & vbCrLf)

		If (radSimple.Checked = True) Then
			htmlHeader.Append("        <td class='center'>" & getHTMLIcon("_16___Resource___Services", 16) & "Services</td>" & vbCrLf)
			htmlHeader.Append("        <td class='center'>" & getHTMLIcon("_16___Resource___Hotfix", 16) & "Hotfixes</td>" & vbCrLf)
			htmlHeader.Append("        <td class='center'>" & getHTMLIcon("_16___Resource___Eventlog", 16) & "Eventlog</td>" & vbCrLf)
			htmlHeader.Append("        <td class='center'>" & getHTMLIcon("_16___Resource___Registry", 16) & "Registry</td>" & vbCrLf)
			htmlHeader.Append("        <td class='center'>" & getHTMLIcon("_16___Resource___File", 16) & "File Check</td>" & vbCrLf)
			htmlHeader.Append("        <td class='center'>" & getHTMLIcon("_16___Resource___WMIQuery", 16) & "WMI Query</td>" & vbCrLf)
			htmlHeader.Append("        <td class='center'>" & getHTMLIcon("_16___Resource___Drive", 16) & "Free Space</td>" & vbCrLf)
			htmlHeader.Append("    </tr>" & vbCrLf)
		Else
			htmlHeader.Append("        <td class='center'>" & getHTMLIcon("_16___Properties", 16) & "Results</td>" & vbCrLf)
			htmlHeader.Append("    </tr>" & vbCrLf)
		End If
		Return htmlHeader.ToString
	End Function

	Private Function doReport__Common(ByVal sTitle As String, ByVal sDisplayText As String, ByVal sIconImage As String) As String
		Dim sOutput As String = vbNullString
		If (radSimple.Checked = True) Then
			If ((sTitle = "Free Drive Space") OrElse (sTitle = "Eventlog Scan")) Then
				sOutput = sOutput & "        <td class='left' cellspacing='0' cellpadding='0'>" & vbCrLf
				sOutput = sOutput & "            <table cellspacing='0' cellpadding='0' border='0'><tr><td class='tdnone'>" & vbCrLf
				sOutput = sOutput & "                " & getHTMLIcon(sIconImage, 14) & "&nbsp;</td><td class='tdnone'>" & vbCrLf
				sOutput = sOutput & "                " & sDisplayText & vbCrLf & "            </td></tr></table>" & vbCrLf & "        </td>" & vbCrLf
			Else
				sOutput = sOutput & "        <td class='left'>" & getHTMLIcon(sIconImage, 16) & sDisplayText & "</td>" & vbCrLf
			End If
		Else
			sOutput = sOutput & "            <strong>" & getHTMLIcon(sIconImage, 16) & sTitle & "</strong>ISSUES<br/>" & vbCrLf & "REPLACE"
		End If
		Return sOutput
	End Function

	' #############################################################################################

	Private Function doReport_Services(lItem As ListViewItem) As String
		Dim sOutput As String = doReport__Common("Windows Services", lItem.SubItems(1).Name, lItem.SubItems(1).Text)
		If (sOutput.EndsWith("REPLACE") = False) Then Return sOutput

		' DETAILED OUTPUT
		sOutput = sOutput.Replace("ISSUES", " - <i>" & lItem.SubItems(1).Name & "</i>")
		If (chkServices.Checked = True) Then
			If ((sServiceEntries IsNot Nothing) AndAlso (sServiceEntries.Count > 0)) Then
				Dim sName As String
				Dim sStatus As String
				For Each eItm As ServiceController In sServiceEntries
					If eItm.DisplayName.Contains("|") Then
						sName = eItm.DisplayName.Split("|"c)(1)
						sStatus = eItm.DisplayName.Split("|"c)(0)
					Else
						sName = eItm.DisplayName
						sStatus = eItm.Status.ToString
					End If
					sOutput = sOutput.Replace("REPLACE", "                " & sSpaceX & sName & " (" & sStatus & ")<br/>" & vbCrLf & "REPLACE")
				Next
			End If
			sOutput = sOutput.Replace("REPLACE", vbNullString)
		Else
			sOutput = Nothing
		End If
		Return sOutput
	End Function

	Private Function doReport_Hotfixes(lItem As ListViewItem) As String
		Dim sOutput As String = doReport__Common("Hotfix Patches", lItem.SubItems(2).Name, lItem.SubItems(2).Text)
		If (sOutput.EndsWith("REPLACE") = False) Then Return sOutput

		' DETAILED OUTPUT
		sOutput = sOutput.Replace("ISSUES", " - <i>" & lItem.SubItems(2).Name & "</i>")
		If (chkHotfixes.Checked = True) Then
			If ((hHotfixEntries IsNot Nothing) AndAlso (hHotfixEntries.Count > 0)) Then
				For Each eItm As String In hHotfixEntries
					sOutput = sOutput.Replace("REPLACE", "                " & sSpaceX & (Split(eItm, "|")(0)) & " (" & (Split(eItm, "|")(1)) & ")<br/>" & vbCrLf & "REPLACE")
				Next
			End If
			sOutput = sOutput.Replace("REPLACE", vbNullString)
		Else
			sOutput = Nothing
		End If
		Return sOutput
	End Function

	Private Function doReport_Eventlog(lItem As ListViewItem) As String
		Dim sHTMLIcon As String
		Dim sOutput As String = doReport__Common("Eventlog Scan", splitEventlog(lItem.SubItems(3).Name), lItem.SubItems(3).Text)
		If (sOutput.EndsWith("REPLACE") = False) Then Return sOutput

		' DETAILED OUTPUT
		sOutput = sOutput.Replace("ISSUES", " - <i>" & splitEventlog(lItem.SubItems(3).Name.Split(CChar(vbCrLf))(0)) & "</i>")
		sOutput = sOutput.Replace("<br/></i>", "</i>")
		If (chkEventlog.Checked = True) Then
			If ((eEventLogEntries IsNot Nothing) AndAlso (eEventLogEntries.Count > 0)) Then
				For Each lItm As List(Of EventLogEntry) In eEventLogEntries
					For Each eItm As EventLogEntry In lItm
						If (chkIncludeIcons.Checked = True) Then
							Select Case eItm.EntryType
								Case 0 : sHTMLIcon = getHTMLIcon("_16___Eventlog___Critical", 10)
								Case EventLogEntryType.Error : sHTMLIcon = getHTMLIcon("_16___Eventlog___Error", 10)
								Case EventLogEntryType.Warning : sHTMLIcon = getHTMLIcon("_16___Eventlog___Warning", 10)
								Case EventLogEntryType.Information : sHTMLIcon = getHTMLIcon("_16___Eventlog___Information", 10)
								Case EventLogEntryType.SuccessAudit : sHTMLIcon = getHTMLIcon("_16___Eventlog___Success", 10)
								Case EventLogEntryType.FailureAudit : sHTMLIcon = getHTMLIcon("_16___Eventlog___Failure", 10)
								Case Else : sHTMLIcon = getHTMLIcon("NULL", 12)
							End Select
						Else
							sHTMLIcon = vbNullString
						End If
						sOutput = sOutput.Replace("REPLACE", "                " & sSpaceX & sHTMLIcon & "(" & (eItm.InstanceId And 65535).ToString & ") " & eItm.Source & "<br/>" & vbCrLf & "REPLACE")
					Next
				Next
			End If
			sOutput = sOutput.Replace("REPLACE", vbNullString)
		Else
			sOutput = Nothing
		End If
		Return sOutput
	End Function
	Private Function splitEventlog(ByVal sInput As String) As String
		Dim sReturn As String = sInput.Replace(vbTab, " ")
		sReturn = sReturn.Replace(vbCrLf, "<br/>")
		sReturn = sReturn.Replace("  ", " ")
		Return sReturn
	End Function

	Private Function doReport_Registry(lItem As ListViewItem) As String
		Dim sOutput As String = doReport__Common("Registry Scan", lItem.SubItems(4).Name, lItem.SubItems(4).Text)
		If (sOutput.EndsWith("REPLACE") = False) Then Return sOutput

		' DETAILED OUTPUT
		sOutput = sOutput.Replace("ISSUES", " - <i>" & lItem.SubItems(4).Name & "</i>")
		If (chkRegKeys.Checked = True) Then
			If ((rRegistryEntries IsNot Nothing) AndAlso (rRegistryEntries.Count > 0)) Then
				For Each eItm As String In rRegistryEntries
					Dim sData() As String = Split(eItm, ", ")
					sOutput = sOutput.Replace("REPLACE", "                " & sSpaceX & Split(sData(0), "|")(2) & "<br/>" & vbCrLf & "REPLACE")
				Next
			End If
			sOutput = sOutput.Replace("REPLACE", vbNullString)
		Else
			sOutput = Nothing
		End If
		Return sOutput
	End Function

	Private Function doReport_FileScan(lItem As ListViewItem) As String
		Dim sOutput As String = doReport__Common("File Check", lItem.SubItems(5).Name, lItem.SubItems(5).Text)
		If (sOutput.EndsWith("REPLACE") = False) Then Return sOutput

		'DETAILED OUTPUT
		sOutput = sOutput.Replace("ISSUES", " - <i>" & lItem.SubItems(5).Name & "</i>")
		If (chkFiles.Checked = True) Then
			If ((fFileCheckEntries IsNot Nothing) AndAlso (fFileCheckEntries.Count > 0)) Then
				For Each eItm As String In fFileCheckEntries
					sOutput = sOutput.Replace("REPLACE", "                " & sSpaceX & (Split(eItm, "|")(0)) & "<br/>" & vbCrLf & "REPLACE")
				Next
			End If
			sOutput = sOutput.Replace("REPLACE", vbNullString)
		Else
			sOutput = Nothing
		End If
		Return sOutput
	End Function

	Private Function doReport_WMIQuery(lItem As ListViewItem) As String
		Dim sOutput As String = doReport__Common("WMI Query", lItem.SubItems(6).Name, lItem.SubItems(6).Text)
		If (sOutput.EndsWith("REPLACE") = False) Then Return sOutput

		'DETAILED OUTPUT
		sOutput = sOutput.Replace("ISSUES", " - <i>" & lItem.SubItems(6).Name & "</i>")
		If (chkFiles.Checked = True) Then
			If ((wWMIQueryEntries IsNot Nothing) AndAlso (wWMIQueryEntries.Count > 0)) Then
				For Each eItm As String In wWMIQueryEntries
					sOutput = sOutput.Replace("REPLACE", "                " & sSpaceX & (Split(eItm, "|")(0)) & "<br/>" & vbCrLf & "REPLACE")
				Next
			End If
			sOutput = sOutput.Replace("REPLACE", vbNullString)
		Else
			sOutput = Nothing
		End If
		Return sOutput

	End Function

	Private Function doReport_FreeSpace(lItem As ListViewItem) As String
		Dim sOutput As String = doReport__Common("Free Drive Space", lItem.SubItems(7).Name.Replace(vbCrLf, "<br/>"), lItem.SubItems(7).Text)
		If (sOutput.EndsWith("REPLACE") = False) Then Return sOutput

		sOutput = sOutput.Replace("ISSUES", "")

		If (chkDriveSytem.Checked = True) Then
			Try
				Dim eDrives As String = xml_GetExcludedDrives(lItem.Name.Split("|"c)(0))
				If (eDrives Is Nothing) Then eDrives = " "

				For Each eItm As ManagementObject In dFreeSpaceSize
					Dim bAddDrive As Boolean = False
					Try
						Console.WriteLine("IGNORE ME: VolumeName: " & eItm("VolumeName").ToString) ' Cluster drive test
						If (eItm("Name").ToString = "THIS_ONE") Then bAddDrive = True
						If ((eItm("Name").ToString <> "THIS_ONE") AndAlso (chkDrives_All.Checked = True)) Then bAddDrive = True
						If ((eDrives.Contains(eItm("DeviceID").ToString & ",") = True) AndAlso (chkDrives_Excluded.Checked = False)) Then bAddDrive = False
					Catch
						bAddDrive = False
					End Try
					If (bAddDrive = True) Then sOutput = sOutput.Replace("REPLACE", doReport_FreeSpaceLine(eItm))
				Next
				sOutput = sOutput.Replace("REPLACE", vbNullString)

			Catch ex As Exception
				' Unknown drive (possible cluster resource), don't add it.
			End Try
		Else
			sOutput = Nothing
		End If
		Return sOutput
	End Function
	Private Function doReport_FreeSpaceLine(ByVal eItm As ManagementObject) As String
		Dim iPercent As Integer = CInt((100 / CLng(eItm("Size"))) * CLng(eItm("FreeSpace")))
		Return "                " & sSpaceX & eItm("DeviceID").ToString & " - " & iPercent.ToString & "% free of " & getSizeAndUnits(CLng(eItm("Size"))) & "<br/>" & vbCrLf & "REPLACE"
	End Function

	' #############################################################################################

	Private Function getHTMLIcon(ByVal sImageName As String, ByVal iSize As Integer) As String
		If (chkIncludeIcons.Checked = False) Then Return vbNullString
		Dim iImage As Image = Nothing
		Dim base64 As String = vbNullString
		Dim sImageKey As String = "_16___Blank"

		Select Case sImageName
			Case "ICON:1" : sImageKey = "_16___Scan___Scanning"
			Case "ICON:2" : sImageKey = "_16___Scan___Pass"
			Case "ICON:3" : sImageKey = "_16___Scan___Failed"
			Case "ICON:4" : sImageKey = "_16___Scan___Unknown"
			Case "ICON:5" : sImageKey = "_16___Eventlog___Warning"
			Case "ICON:6" : sImageKey = "_16___Scan___Skipped"
			Case "NULL" : sImageKey = "_16___Blank"
			Case Else : sImageKey = sImageName
		End Select

		Try
			iImage = CType(My.Resources.ResourceManager.GetObject(sImageKey), Icon).ToBitmap
		Catch
			iImage = CType(My.Resources._16___Blank, Icon).ToBitmap
		End Try

		base64 = "<img src='data:image/png;base64," & ImageToBase64String(iImage, Imaging.ImageFormat.Png) & _
		   "' style='vertical-align:middle' height='" & iSize & "px' width='" & iSize & "px'>&nbsp;"
		Return base64
	End Function

	Private Sub chkServices_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkServices.CheckedChanged
		Call enableExportButton()
	End Sub
	Private Sub chkHotfixes_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkHotfixes.CheckedChanged
		Call enableExportButton()
	End Sub
	Private Sub chkEventlog_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkEventlog.CheckedChanged
		Call enableExportButton()
	End Sub
	Private Sub chkRegKeys_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkRegKeys.CheckedChanged
		Call enableExportButton()
	End Sub
	Private Sub chkFiles_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkFiles.CheckedChanged
		Call enableExportButton()
	End Sub
	Private Sub chkWMI_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkWMI.CheckedChanged
		Call enableExportButton()
	End Sub
	Private Sub chkDriveSytem_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkDriveSytem.CheckedChanged
		Call enableExportButton()
		chkDrives_All.Enabled = chkDriveSytem.Checked
		If (chkDriveSytem.Checked = False) Then chkDrives_All.Checked = False
	End Sub
	Private Sub chkDrives_All_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkDrives_All.CheckedChanged
		chkDrives_Excluded.Enabled = chkDrives_All.Checked
		If (chkDrives_All.Checked = False) Then chkDrives_Excluded.Checked = False
	End Sub
	Private Sub enableExportButton()
		cmdExport.Enabled = (radSimple.Checked Or chkServices.Checked Or chkHotfixes.Checked Or _
		  chkEventlog.Checked Or chkRegKeys.Checked Or chkFiles.Checked Or chkDriveSytem.Checked)
	End Sub

	Private Sub lnkShowExamples_LinkClicked(sender As System.Object, e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lnkShowExamples.LinkClicked
		MessageBox.Show(Me, "Your default browser will now open" & vbCrLf & "with an example of both types of report", APN, MessageBoxButtons.OK, MessageBoxIcon.Information)
		Try
			My.Computer.FileSystem.WriteAllText(Application.StartupPath & "\Report_Example.html", My.Resources.Report_Examples, False, Encoding.ASCII)
			Application.DoEvents()
			Process.Start(Application.StartupPath & "\Report_Example.html")
		Catch ex As Exception
			MessageBox.Show(Me, "There was an error saving the examples." & vbCrLf & "The error returned was..." & vbCrLf & vbCrLf & ex.Message, APN, MessageBoxButtons.OK, MessageBoxIcon.Error)
		End Try
	End Sub

	Private Sub lnkSelect_None_LinkClicked(sender As System.Object, e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lnkSelect_None.LinkClicked
		For Each ctl As Control In Me.Controls
			If (ctl.Name.StartsWith("chk")) Then
				Dim chk As CheckBox = TryCast(ctl, CheckBox)
				If (chk.Name.StartsWith("chkInclude") = False) Then chk.Checked = False
			End If
		Next
	End Sub

	Private Sub lnkSelect_All_LinkClicked(sender As System.Object, e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lnkSelect_All.LinkClicked
		For Each ctl As Control In Me.Controls
			If (ctl.Name.StartsWith("chk")) Then
				Dim chk As CheckBox = TryCast(ctl, CheckBox)
				If (chk.Name.StartsWith("chkInclude") = False) Then chk.Checked = True
			End If
		Next
	End Sub
End Class