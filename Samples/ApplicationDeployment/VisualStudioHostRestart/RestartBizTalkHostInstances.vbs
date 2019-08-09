' RestartBizTalkHostInstances.vbs
' Restarts BizTalk In-Process Host Instances on local machine.
'
' Microsoft BizTalk Server
' Copyright (C) Microsoft Corporation. All rights reserved.
'
' THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND,
' WHETHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED
' WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.
' THE ENTIRE RISK OF USE OR RESULTS IN CONNECTION WITH THE USE OF THIS CODE
' AND INFORMATION REMAINS WITH THE USER.

' Executing the script with wscript.exe will pop up windows during interactive
' execution. Use cscript.exe for execution within a command shell.

' Returns string representation of Host Instance HostType property value.
Function HostTypeString(hostType)
    Select Case hostType
        Case 1: HostTypeString = "In-Process"
        Case 2: HostTypeString = "Isolated"
        Case Else: HostTypeString = "???"
    End Select
End Function

' Returns string representation of Host Instance ServiceState property value.
Function ServiceStateString(serviceState)
    Select Case serviceState
        Case 1: ServiceStateString = "Stopped"
        Case 4: ServiceStateString = "Started"
        Case 8: ServiceStateString = "Unknown"
        Case Else: ServiceStateString = "???"
    End Select
End Function

' Lists Host Instances on local machine.
Sub ListHostInstances
    Wscript.Echo "------------------------------------------------------------------------"
    Wscript.Echo "Host Instances:"
    Wscript.Echo
    Set objWMIService = GetObject("winmgmts://./root/MicrosoftBizTalkServer")
'   query all BizTalk host instances
    Set colHostInstances = objWMIService.ExecQuery("Select * from MSBTS_HostInstance")
'   Show all the details of each host instance
    For Each objHostInstance in colHostInstances
	    Wscript.Echo "  Host Instance """ & objHostInstance.Name & """"
	    Wscript.Echo "    HostName=""" & objHostInstance.HostName & """"
	    Wscript.Echo "    HostType=" & HostTypeString(objHostInstance.HostType)
	    Wscript.Echo "    IsDisabled=" & objHostInstance.IsDisabled
	    Wscript.Echo "    ServiceState=" & ServiceStateString(objHostInstance.ServiceState)
        Wscript.Echo
    Next
    Wscript.Echo "------------------------------------------------------------------------"
    Wscript.Echo
End Sub

' Stops In-Process Host Instances on local machine.
Sub HostInstanceStop
'   specify the name space for Windows Management Instrumentation (WMI)
    Set objWMIService = GetObject("winmgmts://./root/MicrosoftBizTalkServer")
'   query BizTalk host instances that are of type In-Process (within BizTalk Server installation) and enabled
    Set colHostInstances = objWMIService.ExecQuery("Select * from MSBTS_HostInstance Where HostType=1 And IsDisabled=False")
'   if any host instance is found stop them
    If (colHostInstances.Count > 0) Then
        Wscript.Echo "Stopping In-Process Host Instances:"
        For Each objHostInstance in colHostInstances
            Wscript.Echo "  Stopping """ & objHostInstance.Name & """..."
	        objHostInstance.Stop
        Next
    Else
        Wscript.Echo "No In-Process Host Instances to stop."
    End If
End Sub

' Starts In-Process Host Instances on local machine.
Sub HostInstanceStart
'   specify the name space for Windows Management Instrumentation (WMI)
    Set objWMIService = GetObject("winmgmts://./root/MicrosoftBizTalkServer")
'   query BizTalk host instances that are of type In-Process (within BizTalk Server installation) and enabled
    Set colHostInstances = objWMIService.ExecQuery("Select * from MSBTS_HostInstance Where HostType=1 And IsDisabled=False")
'   If any host instance is found start them
    If (colHostInstances.Count > 0) Then
        Wscript.Echo "Starting In-Process Host Instances:"
        For Each objHostInstance in colHostInstances
            Wscript.Echo "  Starting """ & objHostInstance.Name & """..."
	        objHostInstance.Start
        Next
    Else
        Wscript.Echo "No In-Process Host Instances to start."
    End If
End Sub

' Main entry point.
Sub Main
    Wscript.Echo "Restarting BizTalk In-Process Host Instances on local machine..."
'   Call ListHostInstances
    Call HostInstanceStop
    Call HostInstanceStart
'   Call ListHostInstances
End Sub

Call Main
