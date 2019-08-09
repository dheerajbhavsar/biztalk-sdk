@REM
@REM File: PreProcScript.bat
@REM
@REM This script is of type preprocessingscript, and has code which will run
@REM before install and uninstall.
@REM
@REM --------------------------------------------------------------------
@REM This file is part of the Microsoft BizTalk Server SDK
@REM
@REM Copyright (c) Microsoft Corporation. All rights reserved.
@REM
@REM This source code is intended only as a supplement to Microsoft
@REM BizTalk Server release and/or on-line documentation. See these
@REM other materials for detailed information regarding Microsoft code samples.
@REM
@REM THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY
@REM KIND, WHETHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
@REM IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
@REM PARTICULAR PURPOSE.
@REM --------------------------------------------------------------------

@setlocal
REM ### For verifying BTAD_* environment variables when script is called.
set LogFile=C:\ScriptLog.txt
echo %DATE% %TIME% Public key should be set in script >> %LogFile%
set .NET_FRAMEWORK_LOCN="%ProgramFiles%"\"Microsoft Visual Studio 8\SDK\v2.0\Bin\"

REM Remove the "REM" and set the public key token below.
REM set PublicKeyToken=
if not defined PublicKeyToken ( 
	echo %DATE% %TIME% Public key should be set in script >> %LogFile%
	REM Exit with an error code of 1, which will cause the MSI to roll back
	exit /b 1
)

echo Script log %DATE% %TIME% > "%LogFile%"

REM ### Pre install part of the script called for a new application
REM if "%BTAD_ChangeRequestAction%"=="Create" (
if "%BTAD_ChangeRequestAction%"=="Update" (
  if "%BTAD_InstallMode%"=="Install" (
    if "%BTAD_HostClass%"=="BizTalkHostInstance" (
	REM ### Create the folders which will drop messages
	mkdir c:\CreateApplicationSample\Out\
	mkdir c:\CreateApplicationSample\In\
    )
  )
)

REM ### Pre uninstall part of the script called for an existing application
if "%BTAD_ChangeRequestAction%"=="Delete" (
  if "%BTAD_InstallMode%"=="Uninstall" (
    if "%BTAD_HostClass%"=="BizTalkHostInstance" (
	REM ### Remove the following line, and add your code here 
	del c:\CreateApplicationSample\Out\ /s /q
	del c:\CreateApplicationSample\In\ /s/q
	rd c:\CreateApplicationSample\Out\ /s /q
	rd c:\CreateApplicationSample\In\ /s/q

	REM #### MSI Uninstallation does not remove all components. Some of the clean up has to be
	REM #### done by hand. Please look at documentation for a full list of these. The sample
	REM #### application added files into the GAC and hence the GAC must be cleaned up here.
	%.NET_FRAMEWORK_LOCN%\gacutil /u "Schemas,version=1.0.0.0,Culture=neutral,PublicKeyToken=%PublicKeyToken%"
	%.NET_FRAMEWORK_LOCN%\gacutil /u "Maps,version=1.0.0.0,Culture=neutral,PublicKeyToken=%PublicKeyToken%"
	%.NET_FRAMEWORK_LOCN%\gacutil /u "Orchestrations,version=1.0.0.0,Culture=neutral,PublicKeyToken=%PublicKeyToken%"
    )
  )
)

REM ### Indicates a success value. If in the script, an error occured, you could 
REM ### exit with a value of 1, in which case the MSI process would fail and do a best effort 
REM ### roll back.
exit /B 0
@endlocal