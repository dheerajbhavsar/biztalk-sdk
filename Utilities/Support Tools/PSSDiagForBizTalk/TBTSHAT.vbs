'script for doing HAT thing

'Ready to run 1 
'Active 2 
'Suspended (resumable) 4 
'Dehydrated 8 
'Completed with discarded messages 16 
'Suspended (not resumable) 32 
'In breakpoint 64 


'Orchestration 1 
'Tracking 2 
'Messaging 4 
'MSMQT 8 
'Other 16 
'Isolated adapter 32 
'Routing failure report 64 

'Delivered, not consumed 1 
'Consumed 2 
'Suspended 4 
'Abandoned 8 

'No operation 1 
'Suspend 2 
'Terminate 4 
'Resume 8 


   Dim objLocator : Set objLocator = CreateObject("WbemScripting.SWbemLocator")
   Dim objServices : Set objServices = objLocator.ConnectServer(, "root/MicrosoftBizTalkServer")
   Dim strQueryString
   Dim objQueue
   Dim svcInsts
   Dim count
   Dim inst
   Dim fso
   Dim tf
   Dim OutputFile
   Dim k
   Dim objDir
   Dim temp
   Dim strDir
   Dim msg



   On Error Resume Next

   SetLocale(1033)
   OutputFile="output\BizTalk_Instances_Incomplete.htm"
   set fso=CreateObject("Scripting.FileSystemObject")
   if fso.FileExists(OutputFile) then
      fso.DeleteFile(OutputFile)
   end if

   Set tf = fso.CreateTextFile(OutputFile, True)
   

   Dim Context, FromTime, UntilTime, InstSet, Query
   
   wbemFlagReturnImmediately = 16 '0x10
   Set Context = CreateObject("WbemScripting.SWbemNamedValueSet")
   Set FromTime = CreateObject("WbemScripting.SWbemDateTime")
   Set UntilTime = CreateObject("WbemScripting.SWbemDateTime")
   
   UntilTime.Year = 2019
'FromTime1


   
   Context.Add "From", FromTime.Value
   Context.Add "Until", UntilTime.Value
   Context.Add "IterationDelayMS", 10

   'set svcInsts = objServices.ExecQuery("Select * from MSBTS_ServiceInstance where ServiceStatus = 4 OR ServiceStatus = 32 OR ServiceStatus = 8 OR ServiceStatus = 2")
   set svcInsts = objServices.ExecQuery("Select * from MSBTS_ServiceInstance","WQL", wbemFlagReturnImmediately, Context)
   count = svcInsts.count

   tf.write "<font style=" & Chr(34) & "color:blue" & Chr(34) & "><b>"
   'tf.write "Total Number of Incomplete Instances Found: " & count & "<br>"
   tf.write "These Incomplete Instances (up to 200) started after PSSDiagForBizTalk had been started."  & "<br>"
   tf.write "</b></font><br>"

	tf.write "<table cellspacing=" & Chr(34) & "0" & Chr(34) & " cellpadding=" & Chr(34) & "5" & Chr(34) & " rules=" & Chr(34) & "all" & Chr(34) & " border=" & Chr(34) & "1" & Chr(34) & " id style=" & Chr(34) & "border-collapse:collapse;" & Chr(34) & "><tr style=" & Chr(34) & "color:Black;background-color:Coral;" & Chr(34) & "><td style=" & Chr(34) & "background-color:LightBlue;" & Chr(34) & ">Service Name</td><td style=" & Chr(34) & "background-color:LightBlue;" & Chr(34) & ">Service Class</td><td style=" & Chr(34) & "background-color:LightBlue;" & Chr(34) & ">Assembly Name</td><td style=" & Chr(34) & "background-color:LightBlue;" & Chr(34) & ">Instance ID</td><td style=" & Chr(34) & "background-color:LightBlue;" & Chr(34) & ">Activation Time</td><td style=" & Chr(34) & "background-color:LightBlue;" & Chr(34) & ">Service Status</td><td style=" & Chr(34) & "background-color:LightBlue;" & Chr(34) & ">Host Name</td><td style=" & Chr(34) & "background-color:LightBlue;" & Chr(34) & ">Suspend Time</td><td style=" & Chr(34) & "background-color:LightBlue;" & Chr(34) & ">Error Category</td><td style=" & Chr(34) & "background-color:LightBlue;" & Chr(34) & ">Error Description</td><td style=" & Chr(34) & "background-color:LightBlue;" & Chr(34) & ">Pending Operation</td><td style=" & Chr(34) & "background-color:LightBlue;" & Chr(34) & ">Messages</td></tr>"

   If ( count > 0 ) Then


      Dim i : i= count
      For each inst in svcInsts
	i=i-1

	if (i<201) then

	msg=""
	'if it is messaging, we get the configuration
	if (inst.ServiceClass=4 or inst.ServiceClass=64) then
		GetServiceConfig inst.ServiceName
		msg=msg+"<tr valign=top><td nowrap style=" & Chr(34) & "background-color:LightCyan;" & Chr(34) & ">" & "<a href=" & Chr(34) & "config/" & inst.ServiceName & ".txt" & Chr(34) & ">" & inst.ServiceName & "</a>"  & "</td>"
	elseif (inst.ServiceClass=1) then
		GetXLANGServiceConfig inst
		msg=msg+ "<tr valign=top><td nowrap style=" & Chr(34) & "background-color:LightCyan;" & Chr(34) & ">" & "<a href=" & Chr(34) & "config/" & inst.AssemblyName & "." & inst.ServiceName & ".odx" & Chr(34) & ">" & inst.ServiceName & "</a>"  & "</td>"
	else		
		msg=msg+"<tr valign=top><td nowrap style=" & Chr(34) & "background-color:LightCyan;" & Chr(34) & ">" & inst.ServiceName  & "</td>"
	end if


	if (inst.ServiceClass=1) then
		msg=msg+"<td nowrap style=" & Chr(34) & "background-color:LightCyan;" & Chr(34) & ">" & "Orchestration"  & "</td>"
	elseif (inst.ServiceClass=2) then
		msg=msg+"<td nowrap style=" & Chr(34) & "background-color:LightCyan;" & Chr(34) & ">" & "Tracking"  & "</td>"  
	elseif (inst.ServiceClass=4) then
		msg=msg+"<td nowrap style=" & Chr(34) & "background-color:LightCyan;" & Chr(34) & ">" & "Messaging"  & "</td>" 
	elseif (inst.ServiceClass=8) then
		msg=msg+"<td nowrap style=" & Chr(34) & "background-color:LightCyan;" & Chr(34) & ">" & "MSMQT"  & "</td>" 
	elseif (inst.ServiceClass=16) then
		msg=msg+"<td nowrap style=" & Chr(34) & "background-color:LightCyan;" & Chr(34) & ">" & "Other"  & "</td>"  
	elseif (inst.ServiceClass=32) then
		msg=msg+"<td nowrap style=" & Chr(34) & "background-color:LightCyan;" & Chr(34) & ">" & "Isolated adapter"  & "</td>" 
	elseif (inst.ServiceClass=64) then
		msg=msg+"<td nowrap style=" & Chr(34) & "background-color:LightCyan;" & Chr(34) & ">" & "Routing failure report"  & "</td>"  
	end if

	msg=msg+"<td nowrap style=" & Chr(34) & "background-color:LightCyan;" & Chr(34) & ">" & inst.AssemblyName  & "</td>"     
	msg=msg+"<td nowrap style=" & Chr(34) & "background-color:LightCyan;" & Chr(34) & ">" & inst.InstanceID  & "</td>" 
	msg=msg+"<td nowrap style=" & Chr(34) & "background-color:LightCyan;" & Chr(34) & ">" & inst.ActivationTime  & "</td>" 
	
	if (inst.ServiceStatus=1) then
		msg=msg+"<td nowrap style=" & Chr(34) & "background-color:LightCyan;" & Chr(34) & ">" & "Readt to Run"  & "</td>"
	elseif (inst.ServiceStatus=2) then
		msg=msg+"<td nowrap style=" & Chr(34) & "background-color:LightCyan;" & Chr(34) & ">" & "Active"  & "</td>"   
	elseif (inst.ServiceStatus=4) then
		msg=msg+"<td nowrap style=" & Chr(34) & "background-color:LightCyan;" & Chr(34) & ">" & "Suspended(resumable)"  & "</td>" 
	elseif (inst.ServiceStatus=8) then
		msg=msg+"<td nowrap style=" & Chr(34) & "background-color:LightCyan;" & Chr(34) & ">" & "Dehydrated"  & "</td>"
	elseif (inst.ServiceStatus=16) then
		msg=msg+"<td nowrap style=" & Chr(34) & "background-color:LightCyan;" & Chr(34) & ">" & "Completed with discarded messages"  & "</td>"  
	elseif (inst.ServiceStatus=32) then
		msg=msg+"<td nowrap style=" & Chr(34) & "background-color:LightCyan;" & Chr(34) & ">" & "Suspended(non-resumable)"  & "</td>"  
	elseif (inst.ServiceStatus=64) then
		msg=msg+"<td nowrap style=" & Chr(34) & "background-color:LightCyan;" & Chr(34) & ">" & "In breakpoint"  & "</td>"  
	end if
		
		
	msg=msg+"<td nowrap style=" & Chr(34) & "background-color:LightCyan;" & Chr(34) & ">" & inst.HostName  & "</td>"  
	msg=msg+"<td nowrap style=" & Chr(34) & "background-color:LightCyan;" & Chr(34) & ">" & inst.SuspendTime  & "</td>" 
	msg=msg+"<td style=" & Chr(34) & "background-color:LightCyan;" & Chr(34) & ">" & inst.ErrorCategory  & "</td>"  
	msg=msg+"<td style=" & Chr(34) & "background-color:LightCyan;" & Chr(34) & ">" & inst.ErrorDescription  & "</td>"   

	if (inst.PendingOperation=1) then
		msg=msg+"<td nowrap style=" & Chr(34) & "background-color:LightCyan;" & Chr(34) & ">" & "No operation"  & "</td>" 
	elseif (inst.PendingOperation=2) then
		msg=msg+"<td nowrap style=" & Chr(34) & "background-color:LightCyan;" & Chr(34) & ">" & "Suspend"  & "</td>"  
	elseif (inst.PendingOperation=4) then
		msg=msg+"<td nowrap style=" & Chr(34) & "background-color:LightCyan;" & Chr(34) & ">" & "terminate"  & "</td>"   
	elseif (inst.PendingOperation=8) then
		msg=msg+"<td nowrap style=" & Chr(34) & "background-color:LightCyan;" & Chr(34) & ">" & "Resume"  & "</td>"  
	end if

	tf.write msg

	GetMessageInstances inst.InstanceID

	
	tf.write "</tr>" 



	end if
      Next
 
   End IF

   tf.write "</table>"
   tf.Close
   Set objTextStream = Nothing
   Set objFSO = Nothing

   Set objLocator = Nothing
   Set objServices = Nothing
   Set objQueue = Nothing
   
   
   Dim sFileName, fil, CN, sCNString, rs, strSQL, fso1
   
   set fso1 = CreateObject("Scripting.FileSystemObject")   
   sFileName = ("output\BizTalk_Instances_Completed.htm") 
      if fso1.FileExists(sFileName) then
         fso1.DeleteFile(sFileName)
      end if
   Set Fil = fso.CreateTextFile(sFileName) 
   
      Fil.write "<font style=" & Chr(34) & "color:blue" & Chr(34) & "><b>"
      Fil.write "These Instances (up to 1000) completed after PSSDiagForBizTalk had been started."  & "<br>"
      Fil.write "</b></font><br>"
   
      Fil.write "<table cellspacing=" & Chr(34) & "0" & Chr(34) & " cellpadding=" & Chr(34) & "5" & Chr(34) & " rules=" & Chr(34) & "all" & Chr(34) & " border=" & Chr(34) & "1" & Chr(34) & " id style=" & Chr(34) & "border-collapse:collapse;" & Chr(34) & "><tr style=" & Chr(34) & "color:Black;background-color:Coral;" & Chr(34) & "><td style=" & Chr(34) & "background-color:LightBlue;" & Chr(34) & ">Service/Name</td><td style=" & Chr(34) & "background-color:LightBlue;" & Chr(34) & ">Service/Type</td><td style=" & Chr(34) & "background-color:LightBlue;" & Chr(34) & ">ServiceInstance/State</td><td style=" & Chr(34) & "background-color:LightBlue;" & Chr(34) & ">StartTime</td><td style=" & Chr(34) & "background-color:LightBlue;" & Chr(34) & ">EndTime</td><td style=" & Chr(34) & "background-color:LightBlue;" & Chr(34) & ">ServiceInstance/Duration</td><td style=" & Chr(34) & "background-color:LightBlue;" & Chr(34) & ">ServiceInstance/ExitCode</td><td style=" & Chr(34) & "background-color:LightBlue;" & Chr(34) & ">ServiceInstance/ErrorInfo</td><td style=" & Chr(34) & "background-color:LightBlue;" & Chr(34) & ">ServiceInstance/Host</td><td style=" & Chr(34) & "background-color:LightBlue;" & Chr(34) & ">ServiceInstance/AssemblyName</td><td style=" & Chr(34) & "background-color:LightBlue;" & Chr(34) & ">ServiceInstance/InstanceID</td><td style=" & Chr(34) & "background-color:LightBlue;" & Chr(34) & ">ServiceInstance/ActivityID</td><td style=" & Chr(34) & "background-color:LightBlue;" & Chr(34) & ">Service/ServiceGUID</td><td style=" & Chr(34) & "background-color:LightBlue;" & Chr(34) & ">Service/ServiceClassGUID</td></tr>"
   
   
   
   Set CN = CreateObject("ADODB.Connection") 
   sCNString = "Provider=sqlncli11;Data Source=TrackingDBServerName;" & _
   "Initial Catalog=TrackingDBName;Integrated Security=SSPI;"
   
   CN.ConnectionString = sCNString 
   Err.Clear
   CN.Open 
     If Err.number <> 0 Then
          Fil.write "Failed to connect to BizTalkDTADb" & vbCrLf
	  Fil.write "    Error number: " & Err.Number & vbCrLf &_
        "    Error description: '" & Err.Description & vbCrLf
     End If

     ' Reset error handling
     On Error Goto 0
   
   Set rs = CreateObject("ADODB.recordset")
   strSQL = "SELECT TOP 1000 " & _
   	"[Service/Name], [Service/Type]," & _
   	"[ServiceInstance/State]," & _
   	"dateadd(minute, UtcOffsetMin, [ServiceInstance/StartTime])  as StartTime, " & _
   	"dateadd(minute, UtcOffsetMin, [ServiceInstance/EndTime])  as EndTime, " & _
   	"[ServiceInstance/Duration]," & _
   	"[ServiceInstance/ExitCode]," & _
   	"[ServiceInstance/ErrorInfo]," & _
   	"[ServiceInstance/Host], " & _
   	"[Service/AssemblyName], " & _
   	"[ServiceInstance/InstanceID], " & _
   	"[ServiceInstance/ActivityID], " & _
   	"[Service/ServiceGUID]," & _
   	"[Service/ServiceClassGUID]" & _
   "FROM dbo.dtav_ServiceFacts sf WITH (READPAST)" & _
   "WHERE [ServiceInstance/EndTime]>'UtcDateTime'" & _ 
   "ORDER BY sf.[ServiceInstance/EndTime] desc"
   
   
   
   rs.open strSQL, CN, 3,3
   
   if NOT rs.EOF then
   	rs.MoveFirst
   end if
   WHILE NOT rs.EOF
        Fil.write "<tr valign=top>"
        Fil.write "<td nowrap style=" & Chr(34) & "background-color:LightCyan;" & Chr(34) & ">" & rs("Service/Name")  & "</td>" 
        Fil.write "<td nowrap style=" & Chr(34) & "background-color:LightCyan;" & Chr(34) & ">" & rs("Service/Type")  & "</td>"
        Fil.write "<td nowrap style=" & Chr(34) & "background-color:LightCyan;" & Chr(34) & ">" & rs("ServiceInstance/State")  & "</td>"
        Fil.write "<td nowrap style=" & Chr(34) & "background-color:LightCyan;" & Chr(34) & ">" & rs("StartTime")  & "</td>"
        Fil.write "<td nowrap style=" & Chr(34) & "background-color:LightCyan;" & Chr(34) & ">" & rs("EndTime")  & "</td>"
        Fil.write "<td nowrap style=" & Chr(34) & "background-color:LightCyan;" & Chr(34) & ">" & rs("ServiceInstance/Duration")  & "</td>"
        Fil.write "<td nowrap style=" & Chr(34) & "background-color:LightCyan;" & Chr(34) & ">" & rs("ServiceInstance/ExitCode")  & "</td>"
        Fil.write "<td nowrap style=" & Chr(34) & "background-color:LightCyan;" & Chr(34) & ">" & rs("ServiceInstance/ErrorInfo")  & "</td>"
        Fil.write "<td nowrap style=" & Chr(34) & "background-color:LightCyan;" & Chr(34) & ">" & rs("ServiceInstance/Host")  & "</td>"
        Fil.write "<td nowrap style=" & Chr(34) & "background-color:LightCyan;" & Chr(34) & ">" & rs("Service/AssemblyName")  & "</td>"
        Fil.write "<td nowrap style=" & Chr(34) & "background-color:LightCyan;" & Chr(34) & ">" & rs("ServiceInstance/InstanceID")  & "</td>"
        Fil.write "<td nowrap style=" & Chr(34) & "background-color:LightCyan;" & Chr(34) & ">" & rs("ServiceInstance/ActivityID")  & "</td>"
        Fil.write "<td nowrap style=" & Chr(34) & "background-color:LightCyan;" & Chr(34) & ">" & rs("Service/ServiceGUID")  & "</td>"
        Fil.write "<td nowrap style=" & Chr(34) & "background-color:LightCyan;" & Chr(34) & ">" & rs("Service/ServiceClassGUID")  & "</td>"
        Fil.write "</tr>"
   rs.MoveNext
   WEND
   
   
   Fil.write "</table>"
   Fil.Close
   if not rs is Nothing then
       rs.Close
       Set rs = Nothing
   end if

   CN.Close
   Set CN = Nothing



'function
Sub GetMessageInstances(ServiceInstanceID)
   Dim MsgInsts, Query
   Dim msg
   
   tf.write "<td nowrap style=" & Chr(34) & "background-color:LightCyan;" & Chr(34) & ">"

   Query = "SELECT * FROM MSBTS_MessageInstance where ServiceInstanceID = """ & ServiceInstanceID & """"

   Set MsgInsts = GetObject("Winmgmts:!root\MicrosoftBizTalkServer").ExecQuery(Query)
   If ( MsgInsts.count > 0 ) Then

      Dim i : i= 1
      For each inst in MsgInsts
	msg=""
	'tf.write "<b>Message: " & i  & "</b><br>"
	msg=msg+"<b>Message: " & i  & "</b><br>"
	'tf.write "******Message Instance ID: " & inst.MessageInstanceID & "<br>"
	msg=msg+"******Message Instance ID: " & inst.MessageInstanceID & "<br>"
	'tf.write "******Message Type: " & inst.MessageType & "<br>"
	msg=msg+"******Message Type: " & inst.MessageType & "<br>"
	'tf.write "******Inbound Adapter: " & inst.InboundAdapterName  & "<br>"
	msg=msg+"******Inbound Adapter: " & inst.InboundAdapterName  & "<br>"
	'tf.write "******Inbound Url: " & inst.InboundUrl  & "<br>"
	msg=msg+"******Inbound Url: " & inst.InboundUrl  & "<br>"
	'tf.write "******Outbound Adapter: " & inst.OutboundAdapterName  & "<br>"
	msg=msg+"******Outbound Adapter: " & inst.OutboundAdapterName  & "<br>"
	'tf.write "******Outbound Url: " & inst.OutboundUrl  & "<br>"
	msg=msg+"******Outbound Url: " & inst.OutboundUrl  & "<br>"
	'tf.write "******Creation Time: " & inst.CreationTime  & "<br>"
	msg=msg+"******Creation Time: " & inst.CreationTime  & "<br>"
	'tf.write "******SendPort Name: " & inst.SendPortName  & "<br>"
	msg=msg+"******SendPort Name: " & inst.SendPortName  & "<br>"

	tf.write msg
	
'Messages



	i=i+1
      Next

   End IF
   tf.write "</td>"

End Sub


'function
Sub GetServiceConfig(ServiceName)
   Dim MsgInsts, Query, query1, primaryrl
   Dim OutputFile, tf, svcs, svc, svcs1, svc1

   if (ServiceName="") then
	Exit Sub
   End if
   
   OutputFile="output\config\" & ServiceName & ".txt"
   set fso=CreateObject("Scripting.FileSystemObject")
   if fso.FileExists(OutputFile) then
      Exit Sub
   end if



   Query = "SELECT * FROM MSBTS_ReceivePort where Name = """ & ServiceName & """"

   Set svcs = GetObject("Winmgmts:!root\MicrosoftBizTalkServer").ExecQuery(Query)
   If ( svcs.count > 0 ) Then
   	Set tf = fso.CreateTextFile(OutputFile, True)

	For each svc in svcs
	tf.write "Service: " & ServiceName  & vbcrlf &vbcrlf
	tf.write "******IsTwoWay: " & svc.IsTwoWay & vbcrlf
	tf.write "******InboundTransforms: " & svc.InboundTransforms & vbcrlf
	tf.write "******OutboundTransforms: " & svc.OutboundTransforms  & vbcrlf
	tf.write "******Tracking: " & svc.Tracking & vbcrlf
	tf.write "******PrimaryReceiveLocation: " & svc.PrimaryReceiveLocation  & vbcrlf
	
		'Now list the primary RL
   		Query1 = "SELECT * FROM MSBTS_ReceiveLocation where Name = """ & svc.PrimaryReceiveLocation & """"

   		Set svcs1 = GetObject("Winmgmts:!root\MicrosoftBizTalkServer").ExecQuery(Query1)
   		If ( svcs1.count > 0 ) Then


			For each svc1 in svcs1
			tf.write Chr(9) & "******AdapterName: " & svc1.AdapterName & vbcrlf
			tf.write Chr(9) & "******InboundTransportURL: " & svc1.InboundTransportURL & vbcrlf
			tf.write Chr(9) & "******PipelineName: " & svc1.PipelineName  & vbcrlf
			tf.write Chr(9) & "******HostName: " & svc1.HostName & vbcrlf
			tf.write Chr(9) & "******CustomCfg: " & svc1.CustomCfg  & vbcrlf
			next	


   		End IF
	next	

	tf.close


   End IF

   Query = "SELECT * FROM MSBTS_SendPort where Name = """ & ServiceName & """"

   Set svcs = GetObject("Winmgmts:!root\MicrosoftBizTalkServer").ExecQuery(Query)
   If ( svcs.count > 0 ) Then
   	Set tf = fso.CreateTextFile(OutputFile, True)

	For each svc in svcs
	tf.write "Service: " & ServiceName  & vbcrlf & vbcrlf
	tf.write "******IsTwoWay: " & svc.IsTwoWay & vbcrlf
	tf.write "******IsDynamic: " & svc.IsDynamic & vbcrlf
	tf.write "******OutboundTransforms: " & svc.OutboundTransforms  & vbcrlf
	tf.write "******InboundTransforms: " & svc.InboundTransforms  & vbcrlf
	tf.write "******Tracking: " & svc.Tracking & vbcrlf
	tf.write "******PTOrderedDelivery: " & svc.PTOrderedDelivery  & vbcrlf
	tf.write "******PTTransportType: " & svc.PTTransportType  & vbcrlf
	tf.write "******PTAddress: " & svc.PTAddress  & vbcrlf
	tf.write "******SendPipeline: " & svc.SendPipeline  & vbcrlf
	tf.write "******PTCustomCfg: " & svc.PTCustomCfg  & vbcrlf
	tf.write "******Filter: " & svc.Filter  & vbcrlf
	next	

	tf.close


   End IF

End Sub


'function
Sub GetXLANGServiceConfig(ServiceInstance)
   Dim MsgInsts, Query, query1, primaryrl
   Dim OutputFile, tf0, tf, svcs, svc, svcs1, svc1
   
   OutputFile="output\RunningOrchestrations.txt"
   set fso=CreateObject("Scripting.FileSystemObject")
   if fso.FileExists(OutputFile) then
   	query1=1
   else
   	set tf0=fso.CreateTextFile(OutputFile, True)
   	tf0.close
   end if


   	Set tf = fso.OpenTextFile(OutputFile, 8, True)

	tf.write ServiceInstance.AssemblyName & "." & ServiceInstance.ServiceName  & vbcrlf
	tf.write ServiceInstance.AssemblyName & ", Version=" & ServiceInstance.AssemblyVersion & ", Culture=" & ServiceInstance.AssemblyCulture & ", PublicKeyToken=" & ServiceInstance.AssemblyPublicKeyToken & vbcrlf
	

	tf.close


End Sub

