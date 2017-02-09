Option Explicit On
Imports System.Xml
Imports System.Text
Imports System.Security.AccessControl

Module modXML
	Public Enum eDirectionalSearch
		ToParents
		ToChildren
		None
	End Enum

	Public xmlDoc As XmlDocument
	Public m_prePopulating As Boolean
	Public m_multiAdding As Boolean			' Used when adding lots of items to the XML, so that we only save it once, not every time
	Public m_DefaultSettings() As String = {"rg|type", "rv|list", "cg|true", "un|false", "ig|1", "gc|true", "pg|true"}
	'
	'										 rg: Resources Grouping       (TYPE, parent, none)
	'										 rv: Resources View           (LIST, tile)
	'										 cg: Use Coloured Groups      (TRUE, false)
	'										 un: Uppercase Server Names   (true, FALSE)
	'										 ig: Icon Group               (1,2,...) (default:1)
	'										 gc: Show Group Counts        (TRUE, false)
	'										 pg: Ping Servers Before Scan (TRUE, false)

	Public Sub xmlCreateNewConfig()
		Dim xEl As XElement
		xEl = <root guid="root">
				  <settings rg="type" rv="list" cg="true" un="false" ig="1" gc="true" pg="true"/>
			  </root>
		xEl.Save(sConfigFile)
	End Sub

	Public Function xmlLoadConfig() As Boolean
		Dim xList As XmlNodeList
		Dim gNode As XmlElement
		Dim sNode As XmlElement
		Dim xmlChecker As XmlDocument
		Dim sMSG As String

		Try
			xmlChecker = New XmlDocument()
			xmlChecker.Load(sConfigFile)

		Catch ex As Exception
			sMSG = vbNullString
			sMSG = sMSG & "There is an problem with the selected configuration file." & vbCrLf
			sMSG = sMSG & "The following is part of the error message..." & vbCrLf & vbCrLf & ex.Message
			MessageBox.Show(frmMain, sMSG, APN, MessageBoxButtons.OK, MessageBoxIcon.Error)
			xmlChecker = Nothing
			Return False
		End Try

		If (bCommandLine = False) Then
			frmMain.bLoadingXML = True
			xmlDoc = New XmlDocument
			xmlDoc = xmlChecker

			bReadOnlyMode = False
			If (My.Computer.FileSystem.FileExists(sConfigFile) = False) Then Return False
			If (xml_SaveXmlDocument(sConfigFile) = False) Then
				sMSG = ""
				sMSG = sMSG & "There is a problem writing to the selected file.  Check that the" & vbCrLf
				sMSG = sMSG & "file is not read-only and that you have the correct permissions." & vbCrLf & vbCrLf
				sMSG = sMSG & "You will not be able to make any changes to this configuration."
				If (bCommandLine = False) Then MessageBox.Show(frmMain, sMSG, APN, MessageBoxButtons.OK, MessageBoxIcon.Warning)
				bReadOnlyMode = True
			End If

			frmMain.ToolStripStatusReadOnly.Visible = bReadOnlyMode
			frmMain.ToolStripStatusDiv0.Visible = bReadOnlyMode

			With frmMain
				Call .clearTreeAndResourceList()
				Call .readSettings()
				.tvwServerList.BeginUpdate()
			End With

			' Read Groups...
			xList = xmlDoc.SelectNodes("descendant::group")
			If (xList Is Nothing) Then Return True
			For Each gNode In xList
				Dim pParent As String = gNode.ParentNode.Attributes.ItemOf("guid").Value
				If (pParent = "root") Then pParent = Nothing
				Call frmMain.onClick_addGroup_(gNode.Attributes.ItemOf("name").Value, gNode.Attributes.ItemOf("guid").Value, CInt(gNode.Attributes.ItemOf("colr").Value), pParent)
			Next

			' Read Servers...
			xList = xmlDoc.SelectNodes("descendant::server")
			If (xList Is Nothing) Then Return True
			For Each sNode In xList
				Call frmMain.onClick_addServer_(sNode.Attributes.ItemOf("name").Value, sNode.Attributes.ItemOf("guid").Value, sNode.ParentNode.Attributes.ItemOf("guid").Value)
			Next

			frmMain.bLoadingXML = False
			With frmMain.tvwServerList
				.EndUpdate()
				If (.Nodes.Count > 0) Then
					.Nodes(0).EnsureVisible()
					.SelectedNode = .Nodes(0)
					.Focus()
				End If
			End With
			Application.DoEvents()
		Else
			xmlDoc = New XmlDocument
			xmlDoc = xmlChecker
		End If
		Return True
	End Function

	Public Function xml_setNewGUID(ByVal sOldGUID As String, sNewGUID As String) As Boolean
		Try
			Dim gNode As XmlNode = xmlDoc.SelectSingleNode("descendant::*[@guid='" & sOldGUID & "']")
			gNode.Attributes.ItemOf("guid").InnerText = sNewGUID
			Call xml_SaveXmlDocument(sConfigFile)
			gNode = Nothing
			Return True

		Catch
			Return False
		End Try

	End Function

	Public Function xml_setAddGroup(ByVal sGroupName As String, ByVal sGUID As String, ByVal sColour As Integer, ByVal sParentGUID As String) As Boolean
		Try
			Dim nParent As XmlNode
			Dim gNode As XmlElement = xmlDoc.CreateElement("group")
			Dim rNode As XmlElement = xmlDoc.CreateElement("resources")

			gNode.SetAttribute("name", sGroupName)
			gNode.SetAttribute("guid", sGUID)
			gNode.SetAttribute("colr", sColour.ToString)
			gNode.SetAttribute("desc", vbNullString)
			gNode.AppendChild(rNode)

			If (sParentGUID Is Nothing) Then
				nParent = xmlDoc.SelectSingleNode("root")
			Else
				nParent = xmlDoc.SelectSingleNode("descendant::group[@guid='" & sParentGUID & "']")
			End If

			nParent.AppendChild(gNode)
			Call xml_SaveXmlDocument(sConfigFile)

			rNode = Nothing
			gNode = Nothing
			nParent = Nothing
			Return True

		Catch ex As Exception
			Return False
		End Try
	End Function

	Public Function xml_setRemoveGroup(ByVal sGroupGUID As String) As Boolean
		Return xml_setRemoveResource(sGroupGUID)
	End Function

	Public Function xml_setRenameGroup(ByVal sGroupGUID As String, ByVal sNewName As String, ByVal iNewColr As Integer, ByVal sDescription As String) As Boolean
		Try
			Dim gNode As XmlNode = xmlDoc.SelectSingleNode("descendant::group[@guid='" & sGroupGUID & "']")
			gNode.Attributes.ItemOf("name").InnerText = sNewName
			gNode.Attributes.ItemOf("colr").InnerText = iNewColr.ToString

			If (gNode.Attributes.ItemOf("desc") Is Nothing) Then
				Dim aNode As XmlAttribute = xmlDoc.CreateAttribute("desc")
				gNode.Attributes.Append(aNode)
			End If

			If (sDescription IsNot Nothing) Then gNode.Attributes.ItemOf("desc").InnerText = cleanXMLString(sDescription)
			Call xml_SaveXmlDocument(sConfigFile)
			gNode = Nothing
			Return True

		Catch
			Return False
		End Try
	End Function

	Public Function xml_setRenameServer(ByVal sServerGUID As String, ByVal sNewName As String) As Boolean
		Try
			Dim sNode As XmlNode = xmlDoc.SelectSingleNode("descendant::server[@guid='" & sServerGUID & "']")
			sNode.Attributes.ItemOf("name").InnerText = sNewName
			Call xml_SaveXmlDocument(sConfigFile)
			sNode = Nothing
			Return True

		Catch ex As Exception
			Return False
		End Try
	End Function

	Public Function xml_getGroupDescription(ByVal sGroupGUID As String) As String
		Dim nNode As XmlNode = xmlDoc.SelectSingleNode("descendant::group[@guid='" & sGroupGUID & "']")
		If (nNode Is Nothing) Then Return vbNullString
		Dim sReturn As String = vbNullString
		Try
			sReturn = nNode.Attributes.ItemOf("desc").Value
			If (sReturn.Length > iLength_Description) Then sReturn = sReturn.Substring(0, iLength_Description)
		Catch
		End Try
		nNode = Nothing
		Return sReturn
	End Function

	Public Function cleanXMLString(ByVal sDesc As String) As String
		' From http://forums.asp.net/post/3470197.aspx
		If ((sDesc Is Nothing) OrElse (sDesc = String.Empty)) Then Return vbNullString
		Dim sReturn As New StringBuilder()
		Dim sCurr As Integer

		For i As Integer = 0 To sDesc.Length - 1
			sCurr = AscW(sDesc(i))
			If ((sCurr = &H9) OrElse (sCurr = &HA) OrElse (sCurr = &HD)) OrElse _
				((sCurr >= &H20) AndAlso (sCurr <= &HD7FF)) OrElse _
				((sCurr >= &HE000) AndAlso (sCurr <= &HFFFD)) OrElse _
				((sCurr >= &H10000) AndAlso (sCurr <= &H10FFFF)) Then sReturn.Append(ChrW(sCurr))
		Next
		Return sReturn.ToString()
	End Function

	Public Function xml_setAddResource(ByVal sGroupGUID As String, ByVal sType As String, ByVal sName As String, ByVal sChecking As String, ByVal sGUID As String) As Boolean
		If (sGroupGUID.StartsWith("DYN-")) Then Return False
		Try
			Dim rNode As XmlElement = xmlDoc.CreateElement("resource")
			rNode.SetAttribute("type", sType)
			rNode.SetAttribute("name", sName)
			rNode.SetAttribute("checking", sChecking)
			rNode.SetAttribute("guid", sGUID)

			Dim nParent As XmlNode = xmlDoc.SelectSingleNode("descendant::group[@guid='" & sGroupGUID & "']")
			Call xml_validateXML_ChildNodes(nParent, {"resources"})
			nParent = xmlDoc.SelectSingleNode("descendant::group[@guid='" & sGroupGUID & "']/resources")
			nParent.AppendChild(rNode)

			' Saves are being done in frmMain.onClick_addResource_Services, frmMain.onClick_addResource_Hotfix, frmMain.onClick_addResource_Registry
			If (m_multiAdding = False) Then Call xml_SaveXmlDocument(sConfigFile)

			nParent = Nothing
			rNode = Nothing
			Return True

		Catch
			Return False
		End Try
	End Function

	Public Function xml_setResourceValue(ByVal sGUID As String, ByVal sNewName As String, ByVal sNewChecking As String) As Boolean
		Try
			Dim gNode As XmlNode = xmlDoc.SelectSingleNode("descendant::*[@guid='" & sGUID & "']")
			If (gNode Is Nothing) Then Return False
			gNode.Attributes.ItemOf("name").Value = sNewName
			gNode.Attributes.ItemOf("checking").Value = sNewChecking
			Call xml_SaveXmlDocument(sConfigFile)
			gNode = Nothing
			Return True

		Catch
			Return False
		End Try
	End Function

	Public Function xml_getGroupIconColour(ByVal sGroupGUID As String) As Integer
		Dim nNode As XmlNode = xmlDoc.SelectSingleNode("descendant::group[@guid='" & sGroupGUID & "']")
		If (nNode Is Nothing) Then Return 9 ' Default 'Yellow' Colour
		If (CInt(nNode.Attributes.ItemOf("colr").Value) = 99) Then Return 9
		Return CInt(nNode.Attributes.ItemOf("colr").Value)
	End Function

	Public Function xml_setRemoveResource(ByVal sGUID As String) As Boolean
		' This function is also used by xml_setRemoveGroup and xml_setRemoveServer, so we are using 'descendant::*' below...
		Try
			Dim rNode As XmlNode = xmlDoc.SelectSingleNode("descendant::*[@guid='" & sGUID & "']")
			If (rNode Is Nothing) Then Return False
			Dim nParent As XmlNode = rNode.ParentNode
			nParent.RemoveChild(rNode)
			Call xml_SaveXmlDocument(sConfigFile)
			rNode = Nothing
			Return True

		Catch
			Return False
		End Try
	End Function

	Public Function xml_setAddServer(ByVal sServerName As String, ByVal sGUID As String, ByVal sParentGUID As String) As Boolean
		If (sGUID.StartsWith("DYN-")) Then Return False
		Try
			Dim sNode As XmlElement = xmlDoc.CreateElement("server")
			sNode.SetAttribute("name", sServerName.ToUpper)
			sNode.SetAttribute("guid", sGUID)
			Application.DoEvents()

			Dim nParent As XmlNode = xmlDoc.SelectSingleNode("descendant::group[@guid='" & sParentGUID & "']")
			nParent.AppendChild(sNode)
			Application.DoEvents()

			' Do NOT save here, we are doing it in frmMain.onClick_addServer_
			'Call xml_SaveXmlDocument(sConfigFile)

			nParent = Nothing
			sNode = Nothing
			Return True

		Catch
			Return False
		End Try
	End Function

	Public Function xml_setNewParent(ByVal sGUID As String, ByVal sCurrentParentGUID As String, ByVal sNewParentGUID As String) As Boolean
		If (sCurrentParentGUID = sNewParentGUID) Then Return True
		Try
			Dim xNode As XmlNode = xmlDoc.SelectSingleNode("descendant::*[@guid='" & sGUID & "']")
			Dim xNewP As XmlNode

			If (sNewParentGUID IsNot Nothing) Then
				xNewP = xmlDoc.SelectSingleNode("descendant::*[@guid='" & sNewParentGUID & "']")
			Else
				xNewP = xmlDoc.SelectSingleNode("root")
			End If

			If (sCurrentParentGUID IsNot Nothing) Then
				xmlDoc.SelectSingleNode("descendant::*[@guid='" & sCurrentParentGUID & "']").RemoveChild(xNode)
			Else
				xmlDoc.SelectSingleNode("root").RemoveChild(xNode)
			End If

			xNewP.AppendChild(xNode)
			Call xml_SaveXmlDocument(sConfigFile)

			xNewP = Nothing
			xNode = Nothing
			Return True

		Catch ex As Exception
			Return False
		End Try
	End Function

	Public Function xml_MoveCopyResource(ByVal bAreWeMoving As Boolean, ByVal sSourceGUID As String, ByVal sDestinationGUID As String, ByVal sResourceGUID As String) As Boolean
		Try
			Dim sourceNode As XmlNode = xmlDoc.SelectSingleNode("descendant::group[@guid='" & sSourceGUID & "']/resources")
			Dim destNode As XmlNode = xmlDoc.SelectSingleNode("descendant::group[@guid='" & sDestinationGUID & "']/resources")
			Dim resourceNode As XmlNode = xmlDoc.SelectSingleNode("descendant::resource[@guid='" & sResourceGUID & "']")
			Dim copyNode As XmlNode
			If ((resourceNode Is Nothing) Or (destNode Is Nothing) Or (sourceNode Is Nothing)) Then Return False

			If bAreWeMoving = True Then
				' MOVE Node...
				resourceNode.ParentNode.RemoveChild(resourceNode)
				destNode.AppendChild(resourceNode)
			Else
				' COPY Node...
				copyNode = resourceNode.Clone
				copyNode.Attributes.ItemOf("guid").Value = Guid.NewGuid.ToString
				destNode.AppendChild(copyNode)
			End If

			Call xml_SaveXmlDocument(sConfigFile)

			copyNode = Nothing
			destNode = Nothing
			sourceNode = Nothing
			resourceNode = Nothing
			Return True

		Catch ex As Exception
			MessageBox.Show(frmMain, "There was an issue with the " & IIf(bAreWeMoving, "move", "copy").ToString & " operation." & vbCrLf & _
			 "The following was returned as part of the error message..." & vbCrLf & vbCrLf & _
			 ex.Message, APN, MessageBoxButtons.OK, MessageBoxIcon.Error)
			Return False
		End Try
	End Function

	Public Function xml_setRemoveServer(ByVal sGUID As String) As Boolean
		Return xml_setRemoveResource(sGUID)
	End Function

	' #############################################################################################

	Public Function xml_getGUIDFromGroup(ByVal sGroupName As String) As String
		Dim xNode As XmlNode
		If (InStr(sGroupName, "\") > 0) Then
			' SUB-Level Group
			Dim sSplit() As String = Split(sGroupName, "\")
			xNode = xmlDoc.SelectSingleNode("descendant::group[@name='" & sSplit(0) & "']")
			If (xNode Is Nothing) Then Return Nothing

			For i As Integer = 1 To sSplit.Count - 1
				xNode = xNode.SelectSingleNode("descendant::group[@name='" & sSplit(i) & "']")
				If (xNode Is Nothing) Then Return Nothing
			Next
		Else
			' TOP-Level Group
			xNode = xmlDoc.SelectSingleNode("descendant::group[@name='" & sGroupName & "']")
			If (xNode Is Nothing) Then Return Nothing
		End If
		Return xNode.Attributes.ItemOf("guid").Value
	End Function

	Public Function xml_getGroupList_FromGroup(ByVal sGroupGUID As String, ByVal eDirection As eDirectionalSearch) As List(Of XmlNode)
		Dim nNode As XmlNode = xmlDoc.SelectSingleNode("descendant::group[@guid='" & sGroupGUID & "']")
		If (nNode Is Nothing) Then Return Nothing
		Dim xNodeList As XmlNodeList

		If (eDirection = eDirectionalSearch.ToParents) Then
			xNodeList = nNode.SelectNodes("ancestor-or-self::group")
		Else
			xNodeList = nNode.SelectNodes("descendant-or-self::group")
		End If

		Dim xNode As List(Of XmlNode) = xNodeList.Cast(Of XmlNode).ToList
		Dim xQuery As IEnumerable(Of XmlNode) = From Q As XmlNode In xNode Select Q

		xNodeList = Nothing
		nNode = Nothing
		xNode = Nothing
		Return xQuery.ToList
	End Function
	Public Function xml_getServerList_FromGroup(ByVal sGroupGUID As String) As List(Of XmlNode)
		Dim nNode As XmlNode = xmlDoc.SelectSingleNode("descendant::group[@guid='" & sGroupGUID & "']")
		If (nNode Is Nothing) Then Return Nothing

		Dim xNodeList As XmlNodeList = nNode.SelectNodes("descendant::server")
		Dim xNode As List(Of XmlNode) = xNodeList.Cast(Of XmlNode).ToList
		Dim xQuerySort As IEnumerable(Of XmlNode) = From Q As XmlNode In xNode Order By Q.Attributes.ItemOf("name").Value Select Q

		xNodeList = Nothing
		nNode = Nothing
		xNode = Nothing
		Return xQuerySort.ToList
	End Function
	Public Function xml_getGroupList_FromServer(ByVal sServerGUID As String) As List(Of XmlNode)
		Dim nNode As XmlNode = xmlDoc.SelectSingleNode("descendant::server[@guid='" & sServerGUID & "']")
		If (nNode Is Nothing) Then Return Nothing

		Dim xNodeList As XmlNodeList = nNode.SelectNodes("ancestor::group")
		Dim xNode As List(Of XmlNode) = xNodeList.Cast(Of XmlNode).ToList
		Dim xQuery As IEnumerable(Of XmlNode) = From Q As XmlNode In xNode Select Q

		xNodeList = Nothing
		nNode = Nothing
		xNode = Nothing
		Return xQuery.ToList
	End Function
	Public Function xml_getGroupList_FromResource(ByVal sResourceGUID As String) As List(Of XmlNode)
		Dim nNode As XmlNode = xmlDoc.SelectSingleNode("descendant::resource[@guid='" & sResourceGUID & "']")
		If (nNode Is Nothing) Then Return Nothing

		Dim xNodeList As XmlNodeList = nNode.SelectNodes("ancestor::group")
		Dim xNode As List(Of XmlNode) = xNodeList.Cast(Of XmlNode).ToList
		Dim xQuery As IEnumerable(Of XmlNode) = From Q As XmlNode In xNode Select Q

		xNodeList = Nothing
		nNode = Nothing
		xNode = Nothing
		Return xQuery.ToList
	End Function
	Public Function xml_getSpecificItem(ByVal sItemGUID As String, Optional ByVal sReturnAttribute As String = vbNullString) As String
		Dim nNode As XmlNode = xmlDoc.SelectSingleNode("descendant::*[@guid='" & sItemGUID & "']")
		If (nNode Is Nothing) Then Return Nothing
		If (sReturnAttribute = vbNullString) Then
			Return nNode.Attributes.ItemOf("name").Value & "|" & _
			  nNode.Attributes.ItemOf("type").Value & "|" & _
			  nNode.Attributes.ItemOf("guid").Value & "|" & _
			  nNode.Attributes.ItemOf("checking").Value
		Else
			Return nNode.Attributes.ItemOf(sReturnAttribute).Value
		End If
	End Function

	Public Function xml_getGroupPathList(ByVal sGroupGUID As String, ByVal eDirection As eDirectionalSearch) As List(Of XmlNode)
		Dim sPath As String = vbNullString
		Dim nList As XmlNodeList
		Dim xNodePath As String = Nothing

		' This block gets the currently selected node and gets all GROUP nodes above (ancestor) or below (descendant) it...
		sPath = "descendant::group[@guid='" & sGroupGUID & "']"
		Select Case eDirection
			Case eDirectionalSearch.ToParents : xNodePath = "ancestor-or-self::group"	   ' Above
			Case eDirectionalSearch.ToChildren : xNodePath = "descendant-or-self::group"   ' Below
			Case eDirectionalSearch.None : xNodePath = "self::*"						   ' None
		End Select

		Dim newDoc As XmlNode = xmlDoc.SelectSingleNode(sPath)
		If (newDoc Is Nothing) Then Return Nothing

		nList = newDoc.SelectNodes(xNodePath)
		If (nList Is Nothing) Then Return Nothing

		Dim xNode As List(Of XmlNode) = nList.Cast(Of XmlNode).ToList
		Dim xQuery As IEnumerable(Of XmlNode) = From Q As XmlNode In xNode Select Q

		xNode = Nothing
		newDoc = Nothing
		xNodePath = Nothing
		nList = Nothing
		Return xQuery.ToList
	End Function

	Public Function xml_getResourceList(ByVal sGroupGUID As String, ByVal eDirection As eDirectionalSearch, ByVal bReturnAsFunction As Boolean) As List(Of XmlElement)
		Dim nList As List(Of XmlNode) = xml_getGroupPathList(sGroupGUID, eDirection)
		If (nList Is Nothing) Then Return Nothing

		Dim nResults As New List(Of XmlElement)
		Dim sLevel As String

		For Each xNode As XmlNode In nList
			nResults.AddRange(xNode.SelectNodes("resources/resource").Cast(Of XmlElement).ToList)
			If (bReturnAsFunction = False) Then
				sLevel = xNode.Attributes.ItemOf("name").Value
				If (xNode.Attributes.ItemOf("guid").Value = sGroupGUID) Then sLevel = "This Level"
				Call addResourceList(xNode.SelectNodes("resources/resource").Cast(Of XmlElement).ToList, sLevel)
			End If
		Next

		nList = Nothing
		Return nResults
	End Function

	Public Function xml_checkResourceExists(ByVal sGroupGUID As String, ByVal sType As String, ByVal sName As String, Optional ByVal resGUID As String = vbNullString) As Boolean
		If (m_prePopulating = True) Then Return False
		If (sGroupGUID.StartsWith("DYN-")) Then Return False

		Dim bFound As Boolean = False
		Dim sCheckValue As String = vbNullString
		Dim xList As List(Of XmlElement) = xml_getResourceList(sGroupGUID, eDirectionalSearch.ToParents, True)
		xList.AddRange(xml_getResourceList(sGroupGUID, eDirectionalSearch.ToChildren, True))

		For Each xE As XmlElement In xList
			If (xE.Attributes.ItemOf("type").Value.ToUpper = sType.ToUpper) Then
				If ((xE.Attributes.ItemOf("name").Value.ToUpper = sName.ToUpper) Or (xE.Attributes.ItemOf("checking").Value.Contains("FROM " & sName & "|") = True)) Then
					If (lResourceConflicts Is Nothing) Then lResourceConflicts = New List(Of String)
					If (xE.Attributes.ItemOf("guid").Value <> resGUID) Then
						Dim sItem As String = sGroupGUID & "|" & xE.Attributes.ItemOf("guid").Value
						If (lResourceConflicts.Contains(sItem) = False) Then lResourceConflicts.Add(sItem)
						bFound = True
					End If
				End If
			End If
		Next
		Return bFound
	End Function

	' #############################################################################################

	Private Sub xml_validateXML_Attributes(ByVal nInput As XmlNode, ByVal sAttributes() As String)
		' This will check that a given XmlNode has the required attributes
		' If not, they will be added with a blank value
		If (nInput Is Nothing) Then Exit Sub
		Dim bChanged As Boolean = False
		Dim bFound As Boolean

		For Each sAttr As String In sAttributes
			bFound = False
			For Each xAtt As XmlAttribute In nInput.Attributes
				If (xAtt.Name = sAttr.Split("|"c)(0)) Then bFound = True
			Next
			If (bFound = False) Then
				Dim xNewAtt As XmlAttribute = xmlDoc.CreateAttribute(sAttr.Split("|"c)(0))
				nInput.Attributes.Append(xNewAtt).Value = sAttr.Split("|"c)(1)
				bChanged = True
			End If
		Next

		If (bChanged = True) Then Call xml_SaveXmlDocument(sConfigFile)
	End Sub

	Private Sub xml_validateXML_ChildNodes(ByVal nInput As XmlNode, ByVal sChildNodes() As String)
		' This will check that a given XmlNode as the required child nodes
		' If not, they will be added
		If (nInput Is Nothing) Then Exit Sub
		Dim bChanged As Boolean = False
		Dim bFound As Boolean

		For Each sChild As String In sChildNodes
			bFound = False
			For Each xChild As XmlNode In nInput.ChildNodes
				If (xChild.Name = sChild) Then bFound = True
			Next
			If (bFound = False) Then
				Dim xNewNode As XmlNode = xmlDoc.CreateElement(sChild)
				nInput.AppendChild(xNewNode)
				bChanged = True
			End If
		Next

		If (bChanged = True) Then Call xml_SaveXmlDocument(sConfigFile)
	End Sub

	' #############################################################################################

	Public Function xml_saveSettings() As Boolean
		If (xmlDoc Is Nothing) Then Return False
		If (frmMain.bLoadingXML = True) Then Return True

		Try
			Call xml_validateXML_ChildNodes(xmlDoc.SelectSingleNode("root"), {"settings"})
			Dim xNode As XmlNode = xmlDoc.SelectSingleNode("root/settings")
			Call xml_validateXML_Attributes(xNode, m_DefaultSettings)

			xNode.Attributes.ItemOf("rg").Value = m_GroupView.ToLower
			xNode.Attributes.ItemOf("rv").Value = m_ResourceView.ToLower
			xNode.Attributes.ItemOf("cg").Value = m_UseColouredGroups.ToString.ToLower
			xNode.Attributes.ItemOf("un").Value = m_UppercaseServerNames.ToString.ToLower
			xNode.Attributes.ItemOf("ig").Value = m_IconGroup.ToString
			xNode.Attributes.ItemOf("gc").Value = m_ShowGroupCounts.ToString.ToLower
			xNode.Attributes.ItemOf("pg").Value = m_PingServersBeforeScan.ToString.ToLower

			Call xml_SaveXmlDocument(sConfigFile)
		Catch ex As Exception
			MessageBox.Show(frmMain, "There was an error saving the settings." & vbCrLf &
									 "The following was returned as part of the error message..." & vbCrLf & vbCrLf & _
									 ex.Message, APN, MessageBoxButtons.OK, MessageBoxIcon.Error)
			Return False
		End Try
		Return True
	End Function

	Public Function xml_readSettings() As List(Of String)
		Dim nNode As XmlNode
		Dim sInput As String
		Dim sSettings As New List(Of String)

		If (xmlDoc IsNot Nothing) Then
			nNode = xmlDoc.SelectSingleNode("root/settings")
			If (nNode IsNot Nothing) Then
				xml_validateXML_Attributes(nNode, m_DefaultSettings)
				Try
					For i As Integer = 0 To m_DefaultSettings.Count - 1
						sSettings.Add(Split(m_DefaultSettings(i), "|")(1))
					Next

					' Resource Grouping
					sInput = nNode.Attributes.ItemOf("rg").InnerText.ToLower
					If ((sInput = "none") Or (sInput = "type") Or (sInput = "parent")) Then sSettings(0) = sInput

					' Resource View
					sInput = nNode.Attributes.ItemOf("rv").InnerText.ToLower
					If ((sInput = "list") Or (sInput = "tile")) Then sSettings(1) = sInput

					' Coloured Groups
					sInput = nNode.Attributes.ItemOf("cg").InnerText.ToLower
					If ((sInput = "true") Or (sInput = "false")) Then sSettings(2) = sInput

					' Uppercase Names
					sInput = nNode.Attributes.ItemOf("un").InnerText.ToLower
					If ((sInput = "true") Or (sInput = "false")) Then sSettings(3) = sInput

					' Group Icon Set
					sInput = nNode.Attributes.ItemOf("ig").InnerText.ToLower
					If ((IsNumeric(sInput) = True) AndAlso ((CInt(sInput) > 0) AndAlso (CInt(sInput) < (iGroupIconSetCount + 1)))) Then sSettings(4) = sInput

					' Group Counts
					sInput = nNode.Attributes.ItemOf("gc").InnerText.ToLower
					If ((sInput = "true") Or (sInput = "false")) Then sSettings(5) = sInput

					' Ping Servers
					sInput = nNode.Attributes.ItemOf("pg").InnerText.ToLower
					If ((sInput = "true") Or (sInput = "false")) Then sSettings(6) = sInput


				Catch ex As Exception
					MessageBox.Show(frmMain, "There was an error reading the settings." & vbCrLf & _
					 "The following was returned as part of the error message..." & vbCrLf & vbCrLf & _
					 ex.Message, APN, MessageBoxButtons.OK, MessageBoxIcon.Error)
				End Try
			Else
				Call xml_saveSettings()
			End If
		End If
		Return sSettings
	End Function

	Public Function xml_LoadServerData(ByVal sServerGUID As String) As String()
		Try
			If (sServerGUID.StartsWith("DYN-") = True) Then sServerGUID = sServerGUID.Replace("DYN-", "")
			Dim iNode As XmlNode = xmlDoc.SelectSingleNode("descendant::server[@guid='" & sServerGUID & "']/info")
			If (iNode Is Nothing) Then Return Nothing
			Dim iCount As Integer = iNode.Attributes.Count
			Dim sData(iCount) As String

			For Each xAtt As XmlAttribute In iNode.Attributes
				If ((xAtt.Name.Length = 2) AndAlso (xAtt.Name.StartsWith("i"))) Then
					sData(CInt(xAtt.Name.Substring(1, 1))) = xAtt.Value
				End If
			Next

			Return sData
		Catch ex As Exception
			Return Nothing
		End Try
	End Function

	Public Function xml_getServerName(ByVal sServerGUID As String) As String
		Try
			Dim iNode As XmlNode = xmlDoc.SelectSingleNode("descendant::server[@guid='" & sServerGUID & "']")
			If (iNode Is Nothing) Then Return Nothing
			Return iNode.Attributes.ItemOf("name").InnerText
		Catch ex As Exception
			Return Nothing
		End Try
	End Function

	Public Function xml_LoadConnections(ByVal sGUID As String) As List(Of String)
		Dim sResults As New List(Of String)

		If (sGUID IsNot Nothing) Then
			Dim nNode As XmlNode = xmlDoc.SelectSingleNode("root/connections/connection[@guid='" & sGUID & "']")
			If (nNode Is Nothing) Then Return Nothing
			sResults.Add(nNode.Attributes.ItemOf("name").Value & "|" & _
			 nNode.Attributes.ItemOf("command").Value & "|" & _
			 nNode.Attributes.ItemOf("guid").Value)
		Else
			Dim nNode As XmlNode = xmlDoc.SelectSingleNode("root/connections")
			If ((nNode IsNot Nothing) AndAlso (nNode.ChildNodes.Count > 0)) Then
				Try
					For Each cNode As XmlNode In nNode.ChildNodes
						sResults.Add(cNode.Attributes.ItemOf("name").Value & "|" & _
						 cNode.Attributes.ItemOf("command").Value & "|" & _
						 cNode.Attributes.ItemOf("guid").Value)
					Next
				Catch ex As Exception
					Return Nothing
				End Try
			End If
		End If
		sResults.Sort()
		Return sResults
	End Function

	Public Function xml_AddConnections(ByVal sList As List(Of String)) As Boolean
		' Clear all current connections and use the attached list
		Dim nNode As XmlNode

		' First, REMOVE old entries and create NEW child node...
		Try
			nNode = xmlDoc.SelectSingleNode("root/connections")
			If (nNode IsNot Nothing) Then nNode.RemoveAll()
			Call xml_validateXML_ChildNodes(xmlDoc.SelectSingleNode("root"), {"connections"})
		Catch ex As Exception
			Return False
		End Try

		' Next, INSERT new entries...
		nNode = xmlDoc.SelectSingleNode("root/connections")
		If (sList.Count = 0) Then Return True
		Try
			For Each itm As String In sList
				Dim sItem() As String = Split(itm, "|")
				Dim sNode As XmlElement = xmlDoc.CreateElement("connection")
				sNode.SetAttribute("name", sItem(0))
				sNode.SetAttribute("guid", sItem(1))
				sNode.SetAttribute("command", sItem(2))
				nNode.AppendChild(sNode)
			Next
			Call xml_SaveXmlDocument(sConfigFile)
		Catch ex As Exception
			Return False
		End Try
		Return True
	End Function

	Public Function xml_GetExcludedDrives(ByVal sServerGUID As String) As String
		Try
			Dim iNode As XmlNode = xmlDoc.SelectSingleNode("descendant::server[@guid='" & sServerGUID & "']/exclude")
			If (iNode Is Nothing) Then Return Nothing
			Dim sRetun As String = iNode.Attributes.ItemOf("drives").Value
			Return sRetun

		Catch ex As Exception
			Return Nothing
		End Try
	End Function

	Public Function xml_SetExcludedDrives(ByVal sServerGUID As String, ByVal sDrives As String) As Boolean
		Try
			Dim iNode As XmlNode = xmlDoc.SelectSingleNode("descendant::server[@guid='" & sServerGUID & "']/exclude")
			If (iNode Is Nothing) Then
				Dim eNode As XmlElement = xmlDoc.CreateElement("exclude")
				eNode.SetAttribute("drives", sDrives)
				iNode = xmlDoc.SelectSingleNode("descendant::server[@guid='" & sServerGUID & "']")
				iNode.AppendChild(eNode)
			Else
				iNode.Attributes.ItemOf("drives").Value = sDrives
			End If
			Call xml_SaveXmlDocument(sConfigFile)
			Return True
		Catch ex As Exception
			Return False
		End Try
	End Function

	Public Function xml_SaveXmlDocument(ByVal SourceFile As String) As Boolean
		If (bReadOnlyMode = True) Then Return False
		Try
			xmlDoc.Save(SourceFile)
			Application.DoEvents()
			Return True
		Catch
			Return False
		End Try
	End Function

	'Public Function xml_SaveServerData(ByVal sServerGUID As String, ByVal sData() As String) As Boolean
	'    Now located in "frmPropertiesServerGetAll" as it is required to be a SHARED function
	'End Function
End Module