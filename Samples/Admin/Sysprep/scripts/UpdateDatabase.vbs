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
newBAMDBServer = ""
newBAMDBName = "" 
oldHWSAdminDBServer = ""
oldHWSAdminDBName = ""
newHWSAdminDBServer = ""
newHWSAdminDBName = ""
newSsoServer = ""


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

''Get the BAM DB Server and DB Name
set node = nothing
set node = configObj.selectSingleNode("/UpdateConfiguration/BAM/DeploymentUnit[@Name='PrimaryImportDatabase']/Property[@Name='ServerName']")
if not (node Is Nothing) then
	newBAMDBServer = "'" & node.Text & "'"
end if

set node = nothing
set node = configObj.selectSingleNode("/UpdateConfiguration/BAM/DeploymentUnit[@Name='PrimaryImportDatabase']/Property[@Name='DatabaseName']")
if not (node Is Nothing) then
	newBAMDBName = "'" & node.Text & "'"
end if

''get the SSO Server name
set node = nothing
set node = configObj.selectSingleNode("/UpdateConfiguration/OtherDatabases/Database[@Name='SSO']")
if not (node Is Nothing) then
	newSsoServer = "'" & node.getAttribute("newDBServer") & "'"
end if

'''''''''''''


wscript.echo "Info: Attempting to connect to server """ & newMgmtDBServer & """, database """ & newMgmtDBName & """..."
dim cnString : cnString = "Driver={SQL Server}; Trusted_Connection=yes; Server=" & newMgmtDBServer & "; Initial Catalog=" & newMgmtDBName 
dim MgmtDbConn : set MgmtDbConn = GetConnection(cnString)
dim MgmtDbCmd  : set MgmtDbCmd  = CreateObject("ADODB.Command")
set MgmtDbCmd.ActiveConnection = MgmtDbConn

wscript.echo "Info: Updating the management database"

wscript.echo "Info: Updating the adm_Group table with new information"
MgmtDbCmd.CommandText = "Update adm_Group SET TrackAnalysisServerName = " & newAnalysisDBServer & ", TrackAnalysisDBName = " & newAnalysisDBName & ", TrackingDBServerName = " & newTrackingDBServer & ", TrackingDBName = " & newTrackingDBName & ", SubscriptionDBServerName = " & newMasterMsgboxDBServer & ", SubscriptionDBName = " & newMasterMsgboxDBName & ", RuleEngineDBServerName = " & newRuleEngineDBServer & ", RuleEngineDBName = " & newRuleEngineDBName & ", BamDBServerName = " & newBamDBServer & ", BamDBName = " & newBamDBName & ", SSOServerName = " & newSsoServer
MgmtDbCmd.Execute

wscript.echo "Info: Updating the list of messageboxes in the management database"
for each node in configObj.selectNodes("/UpdateConfiguration/MessageBoxDB")
	oldDBName = node.getAttribute("oldDBName")
	oldDBServer = node.getAttribute("oldDBServer")
	newDBName = node.getAttribute("newDBName")
	newDBServer = node.getAttribute("newDBServer")

	' Perform the Update here

	MgmtDbCmd.CommandText = "Update adm_MessageBox set DBName = '" & newDBName & "', DBServerName = '" & newDBServer & "' WHERE DBName = '" & oldDBName & "' AND DBServerName = '" & oldDBServer & "'"
	MgmtDbCmd.Execute

	MgmtDbCmd.CommandText = "Update TDDS_Sources set SourceName = '" & newDBServer & "_" & newDBName & "', ConnectionString = 'Pooling=false;Current Language=us_english;Integrated Security=SSPI;Data Source=" & newDBServer & ";Initial Catalog=" & newDBName & "' WHERE SourceName = '" & oldDBServer & "_" & oldDBName & "'"
	MgmtDbCmd.Execute

	'Update the adm_server
	MgmtDbCmd.CommandText = "Update adm_Server set Name = '" & newMgmtDBServer & "'"
	MgmtDbCmd.Execute
	
	' If the tracking db is being updated, lets update the copy tracked messages job appropriately
	if (oldTrackingDBServer <> "TrackingDBServerName") then
	
		wscript.echo "Info: Attempting to connect to server """ & newDBServer & """, database """ & newDBName & """..."
		cnString = "Driver={SQL Server}; Trusted_Connection=yes; Server=" & newDBServer & "; Initial Catalog=" & newDBName 
		dim MsgboxDbConn : set MsgboxDbConn = GetConnection(cnString)
		dim MsgboxDbCmd  : set MsgboxDbCmd  = CreateObject("ADODB.Command")
		set MsgboxDbCmd.ActiveConnection = MsgboxDbConn 

		wscript.echo "Info: Updating the CopyTrackedMessages job in the messagebox"
		
		MsgboxDbCmd.CommandText = "exec bts_UpdateCopyTrackedMessagesJob @trackingDBServer = " & newTrackingDBServer & ", @trackingDBName = " & newTrackingDBName
		MsgboxDbCmd.Execute
	end if
	
	' If the master msgbox db is being updated, lets update the operations operate on instances on master job appropriately
	if (oldMasterMsgboxDBServer <> "SubscriptionDBServerName") then
	
		wscript.echo "Info: Attempting to connect to server """ & newDBServer & """, database """ & newDBName & """..."
		cnString = "Driver={SQL Server}; Trusted_Connection=yes; Server=" & newDBServer & "; Initial Catalog=" & newDBName 
		dim MsgboxDbOpsConn : set MsgboxDbOpsConn = GetConnection(cnString)
		dim MsgboxDbOpsCmd  : set MsgboxDbOpsCmd  = CreateObject("ADODB.Command")
		set MsgboxDbOpsCmd.ActiveConnection = MsgboxDbOpsConn 

		wscript.echo "Info: Updating the OperationsOperateOnInstancesOnMaster job in the messagebox"
		
		MsgboxDbOpsCmd.CommandText = "exec ops_UpdateOperationsOperateOnInstancesOnMasterJob @nvcMasterMsgBoxDBServer = " & newMasterMsgboxDBServer & ", @nvcMasterMsgBoxDBName = " & newMasterMsgboxDBName
		MsgboxDbOpsCmd.Execute
	end if 	

next

wscript.echo "Info: Updating the list TDDS destinations with the new tracking database"
set node = configObj.selectSingleNode("/UpdateConfiguration/TrackingDB")
if not (node Is Nothing) then
	oldDBName = node.getAttribute("oldDBName")
	oldDBServer = node.getAttribute("oldDBServer")
	newDBName = node.getAttribute("newDBName")
	newDBServer = node.getAttribute("newDBServer")
	
	MgmtDbCmd.CommandText = "Update TDDS_Destinations set ConnectionString = 'Pooling=false;Current Language=us_english;Integrated Security=SSPI;server=" & newDBServer & ";database=" & newDBName & "' WHERE ConnectionString = 'Pooling=false;Current Language=us_english;Integrated Security=SSPI;server=" & oldDBServer & ";database=" & oldDBName & "'"

	MgmtDbCmd.Execute
end if

'Updating the Other databases
wscript.echo "Info: Updating the adm_OtherBackupDatabases table"
for each node in configObj.selectNodes("/UpdateConfiguration/OtherDatabases/Database")
oldDBName = node.getAttribute("oldDBName")
	name = node.getAttribute("Name")
	newDBName = node.getAttribute("newDBName")
	newDBServer = node.getAttribute("newDBServer")

	' Perform the Update here.
	MgmtDbCmd.CommandText = "Update adm_OtherBackupDatabases set DatabaseName = '" & newDBName & "', ServerName = '" & newDBServer & "' WHERE DefaultDatabaseName = '" & Name & "'"
	MgmtDbCmd.Execute

	' We also need to update the adm_OtherDatabases table as this is used to manage hotfix installments
	MgmtDbCmd.CommandText = "Update adm_OtherDatabases set DatabaseName = '" & newDBName & "', ServerName = '" & newDBServer & "' WHERE DefaultDatabaseName = '" & Name & "'"
	MgmtDbCmd.Execute
next

'rules engine is now in this table also so lets update it here, too.
if (oldRuleEngineDBServer <> "RuleEngineDBServerName" ) then
	MgmtDbCmd.CommandText = "Update adm_OtherBackupDatabases set DatabaseName = " & newRuleEngineDBName & ", ServerName = " & newRuleEngineDBServer & "WHERE DefaultDatabaseName = 'RuleEngine DB'"
	MgmtDbCmd.Execute

	' We also need to update the adm_OtherDatabases table as this is used to manage hotfix installments
	MgmtDbCmd.CommandText = "Update adm_OtherDatabases set DatabaseName = " & newRuleEngineDBName & ", ServerName = " & newRuleEngineDBServer & "WHERE DefaultDatabaseName = 'RuleEngine DB'"
	MgmtDbCmd.Execute
end if

wscript.echo "Info: Done updating the management database tables"

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
    conn.Provider = "SQLOLEDB"
    conn.Open cnString
    Set GetConnection = conn
End Function