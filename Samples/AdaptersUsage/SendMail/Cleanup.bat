@REM --------------------------------------------------------------------
@REM File: Cleanup.bat
@REM
@REM Summary: Calls scripts and programs to stop and undeploy the sample.
@REM
@REM Sample: Send Mail
@REM
@REM --------------------------------------------------------------------
@REM This file is part of the Microsoft BizTalk Server SDK
@REM
@REM Copyright (c) Microsoft Corporation. All rights reserved.
@REM
@REM This source code is intended only as a supplement to Microsoft BizTalk
@REM Server release and/or on-line documentation. See these other
@REM materials for detailed information regarding Microsoft code samples.
@REM
@REM THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY
@REM KIND, WHETHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
@REM IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR
@REM PURPOSE.
@REM --------------------------------------------------------------------

@SETLOCAL

@CALL "%VS140COMNTOOLS%vsvars32.bat"

@SET OrchestrationName=Microsoft.Samples.BizTalk.SendMail.ReceiveSend
@SET AssemblyName=SendMail

@FOR /F "usebackq tokens=*" %%i IN (`dir %AssemblyName%.dll /s /b`) DO @SET FullAssemblyPath=%%i
@FOR /F "usebackq tokens=4*" %%i IN (`sn -Tp "%FullAssemblyPath%" ^| findstr /c:"Public key token is"`) DO @SET AssemblyPublicKeyToken=%%j

@ECHO.
@ECHO Calling the WMI script to stop and unenlist the orchestration...

@CScript /NoLogo "..\..\Admin\WMI\Stop Orchestration\VBScript\StopOrch.vbs" %OrchestrationName% %AssemblyName% Unenlist

@ECHO.
@ECHO Undeploying the assembly...

@BTSTask RemoveResource -Luid="%AssemblyName%, Version=1.0.0.0, Culture=neutral, PublicKeyToken=%AssemblyPublicKeyToken%" > Undeploy.log

@ECHO.
@ECHO The orchestration has now been undeployed, but the send port and receive port need to
@ECHO be removed mannually using BizTalk Explorer.
@ECHO Please see the BizTalk documentation for details.
@ECHO.

@ENDLOCAL

@PAUSE
