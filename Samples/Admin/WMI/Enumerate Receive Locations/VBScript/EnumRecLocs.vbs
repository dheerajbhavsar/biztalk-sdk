'--------------------------------------------------------------------------
'
' WMI script to enlist a specific orchestraiton.
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

EnumRecLocs
Sub EnumRecLocs()

	'error handling is done by explicity checking the err object rather than using
	'the VB ON ERROR construct, so set to resume next on error.
	on error resume next

	Dim InstSet, Inst
	set InstSet = GetObject ("winmgmts:\root\MicrosoftBizTalkServer").InstancesOf("MSBTS_ReceiveLocation")

	'Check for error condition before continuing.
	If Err <> 0	Then
		PrintWMIErrorThenExit Err.Description, Err.Number
	End If

	'Report on number of receive locations found and list each one.
	wscript.echo "A Total of " & InstSet.Count & " Receive Locations were found."
	If InstSet.Count > 0 Then
		For Each Inst In InstSet
			wscript.echo
			wscript.echo "Receive Location Name  : " & Inst.Name
			wscript.echo "  Disabled             : " & Inst.IsDisabled
			wscript.echo "  Pipeline Name        : " & Inst.PipelineName
			wscript.echo "  Receive Port Name    : " & Inst.ReceivePortName
			wscript.echo "  Inbound Transport URL: " & Inst.InboundTransportURL
			wscript.echo
		next
	End If 
			
End Sub 

'This subroutine deals with all errors using the WbemScripting object.  Error descriptions
'are returned to the user by printing to the console.
Sub	PrintWMIErrorThenExit(strErrDesc, ErrNum)
	On Error Resume	Next
	Dim	objWMIError	: Set objWMIError =	CreateObject("WbemScripting.SwbemLastError")

	If ( TypeName(objWMIError) = "Empty" ) Then
		wscript.echo strErrDesc & " (HRESULT: "	& Hex(ErrNum) & ")."
	Else
		wscript.echo objWMIError.Description & "(HRESULT: "	& Hex(ErrNum) & ")."
		Set objWMIError	= nothing
	End	If
	
	'bail out
	wscript.quit 0
End	Sub