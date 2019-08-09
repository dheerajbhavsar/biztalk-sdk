'--------------------------------------------------------------------------
' File: ConfigureIIS.vbs
'
' Summary: This file is used by several samples to configure an IIS
'          virtual directory.
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

ConfigureIISVirtualDirectory
Sub ConfigureIISVirtualDirectory()
	'Get the command line arguments entered for the script
	Dim objArgs: Set objArgs = WScript.Arguments

	'Make sure the expected number of arguments were provided on the command line.
	'if not, print usage text and exit.
	If (objArgs.Count < 1) Then
		PrintUsage()
		WScript.Quit 0
	End If

	Dim objVirtualRoot, objVirtualDirectory
	Dim objAppPool
	Dim strDirectoryName, strDirectoryPath
	Dim strInstallFolder, strProcessorArchitecture
	Dim WshShell
	Dim WshSysEnv
	Dim Enable32BitAppOnWin64

	'Variable to store the result of IIS platform setting
	Enable32BitAppOnWin64 = 0
	
	Set WshShell = WScript.CreateObject("WScript.Shell")
	Set WshSysEnv = WshShell.Environment("SYSTEM")

	'Determine the type of platform (x86 vs AMD64)
	strProcessorArchitecture = WshSysEnv("PROCESSOR_ARCHITECTURE")

	strDirectoryName = objArgs(0)

	'If only one parameter is specified then use the HTTP receive ISAPI extension path for the VDir
	'Otherwise use the provided path for the VDir
	If (objArgs.Count = 1) Then
	
		'Get BizTalk Server installation folder path
		strInstallFolder = WshShell.RegRead("HKLM\SOFTWARE\Microsoft\BizTalk Server\3.0\InstallPath")
		'Append the HttpReceive to the path
		strDirectoryPath = strInstallFolder & "HttpReceive"
		
		'If platform is 64bit then determine the IIS platform configuration (32bit vs 64bit)
		If (strProcessorArchitecture = "AMD64") Then 

			Set objAppPool = GetObject("IIS://localhost/W3svc/AppPools")
			Enable32BitAppOnWin64 = objAppPool.Get("Enable32BitAppOnWin64")
			
			If (Enable32BitAppOnWin64 = 0) Then
				strDirectoryPath = strInstallFolder & "HttpReceive64"
			End If
		End If
	Else
		strDirectoryPath = WshShell.CurrentDirectory & objArgs(1)
	End If
	
	'Get IIS virtual root object
	Set objVirtualRoot = GetObject("IIS://localhost/w3svc/1/Root")

	'Create new virtual directory
	Set objVirtualDirectory = objVirtualRoot.Create("IIsWebVirtualDir", strDirectoryName)

	'Set properties on new virtual directory
	objVirtualDirectory.AccessRead = True
	objVirtualDirectory.AccessExecute = True
	objVirtualDirectory.AppFriendlyName = strDirectoryName
	objVirtualDirectory.AuthFlags = 5
	objVirtualDirectory.Path = strDirectoryPath
	objVirtualDirectory.KeyType = "IIsWebVirtualDir"

	'IMPORTANT SECURITY NOTE
	'This virtual directory is being created in high isolation mode
	'This will cause a COM+ App to be created for this virtual directory
	'The idenetity used by this COM+ App must have access the the BTS DB
	'It is NOT recommend that you use the IWAM_MachineName acount for this
	objVirtualDirectory.AppIsolated = 1
	objVirtualDirectory.AppCreate False

	'Save the changes to the IIS metabase
	objVirtualDirectory.SetInfo
	
	WScript.Echo "Virtual Directory " & strDirectoryName & " created with path:"
	WScript.Echo strDirectoryPath
End Sub

Sub PrintUsage()
	WScript.Echo "Usage:"
	WScript.Echo
	WScript.Echo "cscript ConfigIIS.vbs <Virtual Directory Name> <Virtual Directory Path>"
	WScript.Echo
	WScript.Echo " Where: "
	WScript.Echo "  <Virtual Directory Name> = The name of the virtual directory to create."
	WScript.Echo "       Example: 'MyBusinessVirtualDirectory'"
	WScript.Echo
	WScript.Echo "  <Virtual Directory Path> = The path to the directory to be made available relative to the current directory."
	WScript.Echo "       Optional parameter. If not specified then HTTP adapter ISAPI extension location will be used"
	WScript.Echo "       Example: 'MyVirtualDirectory'"
	WScript.Echo
End Sub
