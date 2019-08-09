oldMgmtDBServer = "Not Specified"
oldMgmtDBName = "Not Specified"
newMgmtDBServer = "Not Specified"
newMgmtDBName = "Not Specified"
oldMasterMsgboxDBServer = "SubscriptionDBServerName"
oldMasterMsgboxDBName = "SubscriptionDBName"
newMasterMsgboxDBServer = "SubscriptionDBServerName"
newMasterMsgboxDBName = "SubscriptionDBName"
oldRuleEngineDBServer = "RuleEngineDBServerName"
oldRuleEngineDBName = "RuleEngineDBName"
newRuleEngineDBServer = "RuleEngineDBServerName"
newRuleEngineDBName = "RuleEngineDBName"
oldTrackingDBServer = "TrackingDBServerName"
oldTrackingDBName = "TrackingDBName"
newTrackingDBServer = "TrackingDBServerName"
newTrackingDBName = "TrackingDBName"
TrackingDatabaseMachineName = "TrackingDatabaseMachineName"
TrackingDatabaseName = "TrackingDatabaseName"
oldAnalysisDBServer = "TrackAnalysisServerName"
oldAnalysisDBName = "TrackAnalysisDBName"
newAnalysisDBServer = "TrackAnalysisServerName"
newAnalysisDBName = "TrackAnalysisDBName"
oldHWSAdminDBServer = ""
oldHWSAdminDBName = ""
newHWSAdminDBServer = ""
newHWSAdminDBName = ""


'''''''''''''
Dim configObj, loaded, WshShell
Set configObj = CreateObject("MSXML2.DOMDocument")


Set WshShell = WScript.CreateObject("WScript.Shell")

configObj.async = false
loaded = configObj.load(WScript.Arguments(0))

if not loaded then
	wscript.echo "Error: Failed to load the document: " & configObj.parserError.reason
'return the error string
end if

dim node : set node = configObj.selectSingleNode("/UpdateConfiguration/ManagementDB")
if not (node Is Nothing) then
	oldMgmtDBServer = node.getAttribute("oldDBServer")
	oldMgmtDBName = node.getAttribute("oldDBName")
	newMgmtDBServer = node.getAttribute("newDBServer")
	newMgmtDBName = node.getAttribute("newDBName")
else
	newMgmtDBServer = WshShell.RegRead("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\BizTalk Server\3.0\Administration\MgmtDBServer")
	newMgmtDBName = WshShell.RegRead("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\BizTalk Server\3.0\Administration\MgmtDBName")
end if

set node = configObj.selectSingleNode("/UpdateConfiguration/OtherDatabases/Database[@Name='HWS Administration DB']")
if not (node Is Nothing) then
	oldHWSAdminDBServer = node.getAttribute("oldDBServer")
	oldHWSAdminDBName = node.getAttribute("oldDBName")
	newHWSAdminDBServer = node.getAttribute("newDBServer")
	newHWSAdminDBName = node.getAttribute("newDBName")
else
	On Error Resume Next
	newHWSAdminDBServer = WshShell.RegRead("HKLM\SOFTWARE\Microsoft\BizTalk Server\3.0\Hws\WebService\AdminDBServer")
	if (err = 0) then
		newHWSAdminDBName = WshShell.RegRead("HKLM\SOFTWARE\Microsoft\BizTalk Server\3.0\Hws\WebService\AdminDBName")
	else	
		newHWSAdminDBServer = WshShell.RegRead("HKLM\SOFTWARE\Microsoft\BizTalk Server\3.0\Hws\WMI\AdminDBServer")
		if (err = 0) then
			newHWSAdminDBName = WshShell.RegRead("HKLM\SOFTWARE\Microsoft\BizTalk Server\3.0\Hws\WMI\AdminDBName")
		else
			err.Clear
			'okay, lets try 64 bit registry keys
			newHWSAdminDBServer = WshShell.RegRead("HKLM\SOFTWARE\Wow6432Node\Microsoft\BizTalk Server\3.0\Hws\WebService\AdminDBServer")
			if (err = 0) then
				newHWSAdminDBName = WshShell.RegRead("HKLM\SOFTWARE\Wow6432Node\Microsoft\BizTalk Server\3.0\Hws\WebService\AdminDBName")
			else	
				err.Clear
				newHWSAdminDBServer = WshShell.RegRead("HKLM\SOFTWARE\Wow6432Node\Microsoft\BizTalk Server\3.0\Hws\WMI\AdminDBServer")
				if (err = 0) then
					newHWSAdminDBName = WshShell.RegRead("HKLM\SOFTWARE\Wow6432Node\Microsoft\BizTalk Server\3.0\Hws\WMI\AdminDBName")
				end if
			end if
		end if
	end if

	On Error Goto 0
end if

set node = configObj.selectSingleNode("/UpdateConfiguration/MessageBoxDB[@IsMaster='1']")
if not (node Is Nothing) then
	oldMasterMsgboxDBServer = "'" & node.getAttribute("oldDBServer") & "'"
	oldMasterMsgboxDBName = "'" & node.getAttribute("oldDBName") & "'"
	newMasterMsgboxDBServer = "'" & node.getAttribute("newDBServer") & "'"
	newMasterMsgboxDBName = "'" & node.getAttribute("newDBName") & "'"
end if

set node = nothing
set node = configObj.selectSingleNode("/UpdateConfiguration/TrackingDB")
if not (node Is Nothing) then
	oldTrackingDBServer = "'" & node.getAttribute("oldDBServer") & "'"
	oldTrackingDBName = "'" & node.getAttribute("oldDBName") & "'"
	newTrackingDBServer = "'" & node.getAttribute("newDBServer") & "'"
	newTrackingDBName = "'" & node.getAttribute("newDBName") & "'"

	TrackingDatabaseMachineName = newTrackingDBServer
	TrackingDatabaseName = newTrackingDBName
end if

set node = nothing
set node = configObj.selectSingleNode("/UpdateConfiguration/AnalysisDB")
if not (node Is Nothing) then
	oldAnalysisDBDBServer = "'" & node.getAttribute("oldDBServer") & "'"
	oldAnalysisDBDBName = "'" & node.getAttribute("oldDBName") & "'"
	newAnalysisDBDBServer = "'" & node.getAttribute("newDBServer") & "'"
	newAnalysisDBDBName = "'" & node.getAttribute("newDBName") & "'"
end if

set node = nothing
set node = configObj.selectSingleNode("/UpdateConfiguration/RuleEngineDB")
if not (node Is Nothing) then
	oldRuleEngineDBServer = "'" & node.getAttribute("oldDBServer") & "'"
	oldRuleEngineDBName = "'" & node.getAttribute("oldDBName") & "'"
	newRuleEngineDBServer = "'" & node.getAttribute("newDBServer") & "'"
	newRuleEngineDBName = "'" & node.getAttribute("newDBName") & "'"
end if

'''''''''''''

'Updating BAM databases
set node = configObj.selectSingleNode("/UpdateConfiguration/BAM")
if (node Is Nothing) then
	wscript.echo "Info: The BAM databases were not updated because no node was found in the xml file."
else
	wscript.echo "Info: Updating the BAM databases"
	Dim bamConfigObj, success, error
	Set bamConfigObj = CreateObject("Microsoft.BizTalk.Bam.CfgExtHelper.CfgHelper")

	success = bamConfigObj.Restore(newMgmtDBServer, newMgmtDBName, WScript.Arguments(0), error)
	if success then
		wscript.echo "Info: Done updating the BAM databases"
	else
		wscript.echo "Error: Failed to update the BAM databases: " & error
	end if
end if

' ===============================================================
'  GetConnection(cnString)
'  This function accepts a connection string and opens
'  a connection by using it. It returns the connection
'  back
' ===============================================================
Function GetConnection(cnString)
    dim conn : set conn = CreateObject("ADODB.Connection")
    conn.ConnectionTimeout = 30
    conn.Provider = "SQLNCLI11"
    conn.Open cnString
    Set GetConnection = conn
End Function