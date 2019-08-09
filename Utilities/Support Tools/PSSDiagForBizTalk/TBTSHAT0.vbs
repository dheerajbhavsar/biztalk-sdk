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
   OutputFile="output\BizTalk_Instances_PreExisting.htm"
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
   

'FromTime0


   
   Context.Add "From", FromTime.Value
   Context.Add "Until", UntilTime.Value
   Context.Add "IterationDelayMS", 10

   'set svcInsts = objServices.ExecQuery("Select * from MSBTS_ServiceInstance where ServiceStatus = 4 OR ServiceStatus = 32 OR ServiceStatus = 8 OR ServiceStatus = 2")
   set svcInsts = objServices.ExecQuery("Select * from MSBTS_ServiceInstance","WQL", wbemFlagReturnImmediately, Context)
   count = svcInsts.count

   tf.write "<font style=" & Chr(34) & "color:blue" & Chr(34) & "><b>"
   tf.write "Total Number of Preexisting less-than-24-hr-old Instances: " & count & "<br>"
   tf.write "Below is the list of these instances (up to 1000)"  & "<br>"
   tf.write "</b></font><br>"

	tf.write "<table cellspacing=" & Chr(34) & "0" & Chr(34) & " cellpadding=" & Chr(34) & "5" & Chr(34) & " rules=" & Chr(34) & "all" & Chr(34) & " border=" & Chr(34) & "1" & Chr(34) & " id style=" & Chr(34) & "border-collapse:collapse;" & Chr(34) & "><tr style=" & Chr(34) & "color:Black;background-color:Coral;" & Chr(34) & "><td style=" & Chr(34) & "background-color:LightBlue;" & Chr(34) & ">Service Name</td><td style=" & Chr(34) & "background-color:LightBlue;" & Chr(34) & ">Service Class</td><td style=" & Chr(34) & "background-color:LightBlue;" & Chr(34) & ">Assembly Name</td><td style=" & Chr(34) & "background-color:LightBlue;" & Chr(34) & ">Instance ID</td><td style=" & Chr(34) & "background-color:LightBlue;" & Chr(34) & ">Activation Time</td><td style=" & Chr(34) & "background-color:LightBlue;" & Chr(34) & ">Service Status</td><td style=" & Chr(34) & "background-color:LightBlue;" & Chr(34) & ">Host Name</td><td style=" & Chr(34) & "background-color:LightBlue;" & Chr(34) & ">Suspend Time</td><td style=" & Chr(34) & "background-color:LightBlue;" & Chr(34) & ">Error Category</td><td style=" & Chr(34) & "background-color:LightBlue;" & Chr(34) & ">Error Description</td><td style=" & Chr(34) & "background-color:LightBlue;" & Chr(34) & ">Pending Operation</td><td style=" & Chr(34) & "background-color:LightBlue;" & Chr(34) & ">Messages</td></tr>"

   If ( count > 0 ) Then


      Dim i : i= count-1000
      For each inst in svcInsts
	i=i-1

	if (i<0) then

	msg="<tr valign=top><td nowrap style=" & Chr(34) & "background-color:LightCyan;" & Chr(34) & ">" & inst.ServiceName  & "</td>"


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