@REM --------------------------------------------------------------------
@REM File: Cleanup.bat
@REM
@REM Summary: Calls scripts and programs to stop and undeploy the sample.
@REM
@REM Sample: Calling an Orchestration
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

@SET OrchestrationName1=Microsoft.Samples.BizTalk.CallOrchestration.findShippingPrice
@SET OrchestrationName2=Microsoft.Samples.BizTalk.CallOrchestration.receivePO
@SET AssemblyName=CallOrchestration
@SET SendPortName=CallOrchestrationSP
@SET ReceivePortName=CallOrchestrationRP

@ECHO.
@ECHO Calling the WMI script to stop and unenlist the orchestrations...

@CScript /NoLogo "..\..\Admin\WMI\Stop Orchestration\VBScript\StopOrch.vbs" %OrchestrationName2% %AssemblyName% Unenlist
@CScript /NoLogo "..\..\Admin\WMI\Stop Orchestration\VBScript\StopOrch.vbs" %OrchestrationName1% %AssemblyName% Unenlist

@ECHO.
@ECHO Undeploying the assembly...

@FOR /F "usebackq tokens=*" %%i IN (`dir bin\Debug\%AssemblyName%.dll /s /b`) DO @SET FullAssemblyPath=%%i
@FOR /F "usebackq tokens=4*" %%i IN (`sn -Tp "%FullAssemblyPath%" ^| findstr /c:"Public key token is"`) DO @SET AssemblyPublicKeyToken=%%j
@BTSTask RemoveResource -Luid="%AssemblyName%, Version=1.0.0.0, Culture=neutral, PublicKeyToken=%AssemblyPublicKeyToken%" > Undeploy.log

@ECHO.
@ECHO Calling the WMI script to remove the send port...

@CScript /NoLogo "..\..\Admin\WMI\Remove Send Port\VBScript\RemoveSendPort.vbs" %SendPortName%

@ECHO.
@ECHO Calling the WMI script to remove the receive port...

@CScript /NoLogo "..\..\Admin\WMI\Remove Receive Port\VBScript\RemoveReceivePort.vbs" %ReceivePortName%

@ENDLOCAL

@PAUSE
