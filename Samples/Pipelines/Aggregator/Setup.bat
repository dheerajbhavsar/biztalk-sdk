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

@SET ApplicationName="Aggregator Sample"
@SET SolutionName=Aggregator.sln
@SET SampleAssemblyKeyFile=Aggregator.snk
@SET BindingFileName=AggregatorBinding.xml
@SET ReceivePortName=Aggregator_ReceivePort
@SET SendPortName=Aggregator_SendPort_FILE
@SET ReceiveLocationName=Aggregator_ReceiveLocation_FILE
@SET FileReceiveDirectory=In
@SET FileSendDirectory=Out
@SET FileReceiveLocation=\%FileReceiveDirectory%\*.txt
@SET FileSendAddress=\%FileSendDirectory%\Batch_%%MessageID%%.xml



@ECHO.
@ECHO Creating the send and receive directories...

@IF NOT EXIST %FileReceiveDirectory% MD %FileReceiveDirectory%
@IF NOT EXIST %FileSendDirectory% MD %FileSendDirectory%

@ECHO.
@ECHO If key files are not found, will generate new key files...

@IF NOT EXIST %SampleAssemblyKeyFile% sn -k %SampleAssemblyKeyFile%

@ECHO.
@ECHO Building the project and gac'ing the sample assemblies...

@msbuild %SolutionName% /t:Build /p:Configuration=Release /fileLogger /fileLoggerParameters:LogFile=Build.log

@gacutil -uf Aggregator.PipelinesAndSchemas
@gacutil -uf Aggregator.Aggregate
@gacutil -if .\PipelinesAndSchemas\bin\Release\Aggregator.PipelinesAndSchemas.dll
@gacutil -if .\Aggregate\bin\Release\Aggregator.Aggregate.dll


@ECHO.
@ECHO Creating a BizTalk application for the sample...
@BTSTask.exe AddApp -ApplicationName:%ApplicationName% -Description:"Aggregator sample"

@ECHO.
@ECHO Deploying sample assemblies to the application...

@btstask AddResource -ApplicationName:%ApplicationName% -Type:System.BizTalk:BizTalkAssembly -Source:".\PipelinesAndSchemas\bin\Release\Aggregator.PipelinesAndSchemas.dll" -Overwrite -Destination:"C:\AggregatorSample\Aggregator.PipelinesAndSchemas.dll"
@btstask AddResource -ApplicationName:%ApplicationName% -Type:System.BizTalk:BizTalkAssembly -Source:".\Aggregate\bin\Release\Aggregator.Aggregate.dll" -Overwrite -Destination:"C:\AggregatorSample\Aggregator.Aggregate.dll"


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

@CScript /NoLogo "..\..\Admin\WMI\Enlist Orchestration\VBScript\EnlistOrch.vbs" Aggregator.Aggregate.AggregatorOrchestration Aggregator.Aggregate Start
@CScript /NoLogo "..\..\Admin\WMI\Enlist Orchestration\VBScript\EnlistOrch.vbs" Aggregator.Aggregate.SuspendMessage Aggregator.Aggregate Start

@ENDLOCAL

@PAUSE
