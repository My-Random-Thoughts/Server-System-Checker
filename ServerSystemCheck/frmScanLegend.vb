Option Explicit On

Public Class frmScanLegend
	Private Sub frmScan_Legend_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
		For Each c As Control In Controls
			c.Font = frmMain.sysFont
		Next

		Me.Top = frmScan.Bottom - Me.Height - 8
		Me.Left = frmScan.Left + 8

		picLegend_1.Image = My.Resources._16___Scan___Scanning.ToBitmap
		picLegend_2.Image = My.Resources._16___Scan___Pass.ToBitmap
		picLegend_3.Image = My.Resources._16___Eventlog___Warning.ToBitmap
		picLegend_4.Image = My.Resources._16___Scan___Failed.ToBitmap
		picLegend_5.Image = My.Resources._16___Scan___Skipped.ToBitmap
		picLegend_6.Image = My.Resources._16___Scan___Unknown.ToBitmap
		picLegend_7.Image = My.Resources._16___Scan___Exception.ToBitmap

		lblLegend_1.Text = "Currently scanning this object"
		lblLegend_2.Text = "The scan was successful"
		lblLegend_3.Text = "The scan has returned warnings"
		lblLegend_4.Text = "The scan has returned errors"
		lblLegend_5.Text = "Skipped, no scan required"
		lblLegend_6.Text = "Cannot contact the server"
		lblLegend_7.Text = "Error occurred during scanning"
	End Sub
End Class