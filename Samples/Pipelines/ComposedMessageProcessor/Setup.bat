@REM --------------------------------------------------------------------
@REM File: Setup.bat
@REM
@REM Summary: Calls scripts and programs to build, deploy, and start the sample.
@REM
@REM Sample: Custom Pipeline Component
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

@SET ApplicationName="CMP Sample"
@SET SolutionName=ComposedMessageProcessor.sln
@SET SampleAssemblyKeyFile=ComposedMessageProcessor.snk
@SET BindingFileName=ComposedMessageProcessorBinding.xml
@SET SendPortName=CMP_SendPort_FILE
@SET ReceivePortName=CMP_ReceivePort
@SET ReceiveLocationName=CMP_ReceiveLocation_FILE
@SET FileReceiveDirectory=In
@SET FileReceiveLocation=\%FileReceiveDirectory%\*.txt
@SET FileSendDirectory=Out
@SET FileSendAddress=\%FileSendDirectory%\%%MessageID%%.txt



@ECHO.
@ECHO Creating the send and receive directories...

@IF NOT EXIST %FileReceiveDirectory% MD %FileReceiveDirectory%
@IF NOT EXIST %FileSendDirectory% MD %FileSendDirectory%

@ECHO.
@ECHO If key files are not found, will generate new key files...

@IF NOT EXIST %SampleAssemblyKeyFile% sn -k %SampleAssemblyKeyFile%

@ECHO.
@ECHO Building the project and gac'ing the sample assemblies...

@gacutil -uf CMP.PipelinesAndSchemas
@gacutil -uf CMP.Orchestration

@msbuild PipelinesAndSchemas\PipelinesAndSchemas.btproj /t:Build /p:Configuration=Release /fileLogger /fileLoggerParameters:LogFile=Build.log

@gacutil -if .\PipelinesAndSchemas\bin\Release\CMP.PipelinesAndSchemas.dll

@msbuild Orchestration\Orchestration.btproj /t:Build /p:Configuration=Release /fileLogger /fileLoggerParameters:LogFile=Build.log

@gacutil -if .\Orchestration\bin\Release\CMP.Orchestration.dll


@ECHO.
@ECHO Creating a BizTalk application for the sample...
@BTSTask.exe AddApp -ApplicationName:%ApplicationName% -Description:"Composed Message Processor sample"

@ECHO.
@ECHO Deploying sample assemblies to the application...

@btstask AddResource -ApplicationName:%ApplicationName% -Type:System.BizTalk:BizTalkAssembly -Source:".\PipelinesAndSchemas\bin\Release\CMP.PipelinesAndSchemas.dll" -Overwrite -Destination:"C:\CMPSample\PipelinesAndSchemas.dll"
@btstask AddResource -ApplicationName:%ApplicationName% -Type:System.BizTalk:BizTalkAssembly -Source:".\Orchestration\bin\Release\CMP.Orchestration.dll" -Overwrite -Destination:"C:\CMPSample\CMP.Orchestration.dll"


@ECHO.
@ECHO Importing binding information...

@btstask ImportBindings -Source:%BindingFileName% -ApplicationName:%ApplicationName%

@ECHO.
@ECHO Calling the WMI script to start the send port...

@CScript /NoLogo "..\..\Admin\WMI\Start Send Port\VBScript\StartSendPort.vbs" %SendPortName% %FileSendAddress%

@ECHO.
@ECHO Calling the WMI script to enable the receive location...

@CScript /NoLogo "..\..\Admin\WMI\Enable Receive Location\VBScript\EnableRecLoc.vbs" %ReceivePortName% %ReceiveLocationName% %FileReceiveLocation%

@ECHO.
@ECHO Calling the WMI script to enlist and start the orchestrations...

@CScript /NoLogo "..\..\Admin\WMI\Enlist Orchestration\VBScript\EnlistOrch.vbs" CMP.Orchestration.SuspendMessage CMP.Orchestration Start
@CScript /NoLogo "..\..\Admin\WMI\Enlist Orchestration\VBScript\EnlistOrch.vbs" CMP.Orchestration.CMProcessor CMP.Orchestration Start

@ENDLOCAL

@PAUSE
