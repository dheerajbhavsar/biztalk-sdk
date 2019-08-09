'--------------------------------------------------------------------------
'
' WMI script to remove a send port.
'
'--------------------------------------------------------------------------
' This file is part of the Microsoft BizTalk Server 2016 SDK
'
' Copyright (c) Microsoft Corporation. All rights reserved.
'
' This source code is intended only as a supplement to Microsoft BizTalk
' Server 2016 release and/or on-line documentation. See these other
' materials for detailed information regarding Microsoft code samples.
'
' THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY
' KIND, WHETHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
' IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR
' PURPOSE.
'--------------------------------------------------------------------------

Option Explicit

RemoveSendPort
Sub RemoveSendPort()
	'Get the command line arguments entered for the script
	Dim objArgs: Set objArgs = WScript.Arguments

	'error handling is done by explicity checking the err object rather than using
	'the VB ON ERROR construct, so set to resume next on error.
	On Error Resume Next
	
	'Make sure the expected number of arguments were provided on the command line.
	'if not, print usage text and exit.
	If ( (objArgs.Count < 1) or (objArgs.Count > 2 ) ) Then
		PrintUsage()
		WScript.Quit 0
	End If

	Dim objInstSet, objInst, strQuery, UnenlistOnly
	Dim strSendPortName
	
	strSendPortName = objArgs(0)
	UnenlistOnly = False
	If (objArgs.Count = 2) Then
		If ("UnenlistOnly" = objArgs(1)) Then
			UnenlistOnly = True
		Else
			wscript.echo "Wrong optional flag."
			PrintUsage()
			wscript.quit 0
		End If
	End If
			
	'set up a WMI query to acquire a list of send ports with the given Name key value.
	'This should be a list of zero or one Send Ports.
	strQuery = "SELECT * FROM MSBTS_SendPort WHERE  Name =""" & strSendPortName & """"
	Set objInstSet = GetObject("Winmgmts:!root\MicrosoftBizTalkServer").ExecQuery(strQuery)
	
	'Check for error condition before continuing.
	If Err <> 0	Then
		PrintWMIErrorThenExit Err.Description, Err.Number
	End If

	'If Send Port found, Start it, otherwise print error and end.
	If objInstSet.Count > 0 then
		For Each objInst in objInstSet
			'The send port must be unelisted first.
			objInst.UnEnlist()
			If Err <> 0	Then
				PrintWMIErrorThenExit Err.Description, Err.Number
			End If
			WScript.Echo "The Send Port was successfully unenlisted."

			If Not UnEnlistOnly then
				'Now remove the send port
				objInst.Delete_()
				If Err <> 0	Then
					PrintWMIErrorThenExit Err.Description, Err.Number
				End If
				WScript.Echo "The Send Port was successfully removed."
			End If
		Next
	Else
		WScript.Echo "No Send Port was found matching that Name."
	End If
			
End Sub 

'This subroutine deals with all errors using the WbemScripting object.  Error descriptions
'are returned to the user by printing to the console.
Sub	PrintWMIErrorThenExit(strErrDesc, nErrNum)
	On Error Resume	Next
	Dim	objWMIError	: Set objWMIError =	CreateObject("WbemScripting.SwbemLastError")

	If ( TypeName(objWMIError) = "Empty" ) Then
		WScript.Echo strErrDesc & " (HRESULT: "	& Hex(nErrNum) & ")."
	Else
		WScript.Echo objWMIError.Description & "(HRESULT: "	& Hex(nErrNum) & ")."
		Set objWMIError	= Nothing
	End	If
	
	'bail out
	WScript.Quit 0
End Sub 

Sub PrintUsage()
	WScript.Echo "Usage:" + Chr(10) + Chr(10) + _
				 "cscript RemoveSendPort.vbs <Send Port Name> [UnenlistOnly]" + _
				 Chr(10) + Chr(10) + "Where: " + Chr(10) + _
				 "  <Send Port Name> = The name of the send port to remove." + _
				 Chr(10) + "       Example: 'MyBusinessSendPort'" + Chr(10) + Chr(10) + _
				 "  [UnenlistOnly]   = An optional parameter to indicate that the send port should be unenlisted but not removed." + _
				 Chr(10) + "       Example: 'UnenlistOnly'" + Chr(10) + Chr(10)
End Sub
