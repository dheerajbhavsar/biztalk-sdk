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

Dim fso, virtualRoot, vDir, vDirName, objArgs, path

'Get the command line arguments entered for the script
Set objArgs = WScript.Arguments

vDirName = objArgs(0)

Set fso = CreateObject("Scripting.FileSystemObject")
Set virtualRoot = GetObject("IIS://localhost/W3svc/1/Root")
Set vDir = GetObject("IIS://localhost/W3svc/1/Root/" & vDirName)

If Not vDir Is Nothing Then
	path = virtualRoot.Path
	virtualRoot.Delete "IISWebVirtualDir", vDirName
	WScript.Sleep 1000
	fso.DeleteFolder fso.BuildPath(path, vDirName), True
End If