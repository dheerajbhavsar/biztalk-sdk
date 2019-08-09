@REM --------------------------------------------------------------------
@REM File: Setup.bat
@REM
@REM Summary: Calls scripts and programs to build, deploy, and start the sample.
@REM
@REM Sample: Method Call
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

@SET SolutionName=MethodCallSample\MethodCallSample.sln
@SET MathLibAssemblyKeyFile=MathLibrary\MathLibrary.snk
@SET SampleAssemblyKeyFile=MethodCallSample\MethodCallSample.snk
@SET BindingFileName=MethodCallSample\MethodCallSampleBinding.xml
@SET SendPortName=MethodCallSendPort
@SET ReceivePortName=MethodCallReceivePort
@SET ReceiveLocationName=MethodCallReceiveLocation
@SET FileReceiveDirectory=In
@SET FileReceiveLocation=\%FileReceiveDirectory%\*.xml
@SET FileSendDirectory=Out
@SET FileSendAddress=\%FileSendDirectory%\%%MessageID%%.xml
@SET OrchestrationName=Microsoft.Samples.BizTalk.MethodCallSample.MethodCallService
@SET AssemblyName=MethodCallSample

@ECHO.
@ECHO Creating the send and receive directories...

@IF NOT EXIST %FileReceiveDirectory% MD %FileReceiveDirectory%
@IF NOT EXIST %FileSendDirectory% MD %FileSendDirectory%

@ECHO.
@ECHO If key files are not found, will generate a new key files...

@IF NOT EXIST %MathLibAssemblyKeyFile% sn -k %MathLibAssemblyKeyFile%
@IF NOT EXIST %SampleAssemblyKeyFile% sn -k %SampleAssemblyKeyFile%

@ECHO.
@ECHO Building the project...

@msbuild %SolutionName% /t:Build /p:Configuration=Debug /fileLogger /fileLoggerParameters:LogFile=Build.log

@ECHO.
@ECHO We need to GAC the .NET assembly that will be called
@GacUtil /i MathLibrary\bin\Debug\MathLibrary.dll /f

@ECHO.
@ECHO Deploying the sample assembly...
@btstask AddResource -Type:System.BizTalk:BizTalkAssembly  -Source:MethodCallSample\bin\Debug\MethodCallSample.dll -Options:GacOnAdd

@ECHO.
@ECHO Creating and binding the ports...

@BTSTask ImportBindings -Source:%BindingFileName% > Binding.log

@ECHO.
@ECHO Calling the WMI script to start the send port...

@CScript /NoLogo "..\..\Admin\WMI\Start Send Port\VBScript\StartSendPort.vbs" %SendPortName% %FileSendAddress%

@ECHO.
@ECHO Calling the WMI script to enable the receive location...

@CScript /NoLogo "..\..\Admin\WMI\Enable Receive Location\VBScript\EnableRecLoc.vbs" %ReceivePortName% %ReceiveLocationName% %FileReceiveLocation%

@ECHO.
@ECHO Calling the WMI script to enlist and start the orchestration...

@CScript /NoLogo "..\..\Admin\WMI\Enlist Orchestration\VBScript\EnlistOrch.vbs" %OrchestrationName% %AssemblyName% Start

@ENDLOCAL

@PAUSE
