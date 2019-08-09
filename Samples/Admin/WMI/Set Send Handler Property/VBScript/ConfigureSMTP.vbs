'--------------------------------------------------------------------------
'
' WMI script to configure the SMTP Send Handler.
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

ConfigureSMTPSendHandler
Sub ConfigureSMTPSendHandler()
	'error handling is done by explicity checking the err object rather than using
	'the VB ON ERROR construct, so set to resume next on error.
	On Error Resume Next

	'Get the command line arguments entered for the script
	Dim objArgs: Set objArgs = WScript.Arguments

	'Make sure the expected number of arguments were provided on the command line.
	'if not, print usage text and exit.
	If (objArgs.Count <> 2) Then
		PrintUsage()
		WScript.Quit 0
	End If

	Dim objInstSet, objInst, strQuery, strSMTPHostName, strFromAddress, strConfigXML, strAdapterName
	
	strAdapterName = "SMTP"
	strSMTPHostName = objArgs(0)
	strFromAddress = objArgs(1)
	strConfigXML = "<CustomProps><SMTPHost vt=""8"">" & strSMTPHostName & "</SMTPHost><SMTPAuthenticate vt=""19"">2</SMTPAuthenticate><From vt=""8"">" & strFromAddress & "</From></CustomProps>"

	'set up a WMI query to acquire a list of send handlers with the given Name key value.
	'This should be a list of zero or one send handlers.
	strQuery = "SELECT * FROM MSBTS_SendHandler WHERE AdapterName =""" & strAdapterName & """"
	Set objInstSet = GetObject("Winmgmts:!root\MicrosoftBizTalkServer").ExecQuery(strQuery)
	
	'Check for error condition before continuing.
	If Err <> 0	Then
		PrintWMIErrorThenExit Err.Description, Err.Number
	End If

	'If send handler found, set configuration information, otherwise print error and end.
	If objInstSet.Count > 0 then
		For Each objInst in objInstSet
			'Set config data for send handler
			objInst.CustomCfg = strConfigXML
			If Err <> 0	Then
				PrintWMIErrorThenExit Err.Description, Err.Number
			End If
			
			'Commit the change to the database
			objInst.Put_(1)
			If Err <> 0	Then
				PrintWMIErrorThenExit Err.Description, Err.Number
			End If
			WScript.Echo "The Send Handler was successfully configured."
		Next
	Else
		WScript.Echo "No Send Handler was found matching that AdapterName."
	End If
End Sub

'This subroutine deals with all errors using the WbemScripting object.  Error descriptions
'are returned to the user by printing to the console.
Sub	PrintWMIErrorThenExit(strErrDesc, ErrNum)
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
				 "cscript ConfigureSMTP.vbs <SMTP Server Name> <From EMail Address>" + _
				 Chr(10) + Chr(10) + "Where: " + Chr(10) + _
				 "  <SMTP Server Name>   = The name of the SMTP server that will be used to send mail." + _
				 Chr(10) + "       Example: 'MyBusinessSMTPServer'" + Chr(10) + Chr(10) + _
				 "  <From EMail Address> = The email address that the sent mail will be marked as being from." + _
				 Chr(10) + "       Example: 'someone@example.com'" + Chr(10) + Chr(10)
End Sub
