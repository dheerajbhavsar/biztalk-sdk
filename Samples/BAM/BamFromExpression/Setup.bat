@REM --------------------------------------------------------------------
@REM File: Setup.bat
@REM
@REM Summary:Calls scripts and programs to build, deploy, and start the sample.
@REM
@REM Sample: BAM from expression sample
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

@SET SolutionName=BamFromExpression.sln
@SET ActivityFile=BamDefinition.xls
@SET FileReceiveDirectory=Input
@SET FileSendDirectory=Output
@SET AssemblyKeyFile=key.snk
@SET SendPortName=BamFromExpression_SendPort
@SET FileSendAddress=\%FileSendDirectory%\%%MessageID%%.xml
@SET ReceivePortName=BamFromExpression_ReceivePort
@SET ReceiveLocationName=BamFromExpression_ReceiveLocation
@SET FileReceiveLocation=\%FileReceiveDirectory%\*.xml
@SET OrchestrationName=BamFromExpression.Orchestration1
@SET AssemblyName=Microsoft.Samples.BizTalk.BamFromExpression
@SET BindingFileName=BamFromExpressionBinding.xml
@SET BmExe=..\..\..\..\Tracking\bm.exe
@SET BAMCfgXml=..\..\..\..\Tracking\BAMConfiguration.xml
@SET BTSTask=..\..\..\..\BTSTask.exe

@ECHO Creating the send and receive directories...

@IF NOT EXIST %FileReceiveDirectory% MD %FileReceiveDirectory%
@IF NOT EXIST %FileSendDirectory% MD %FileSendDirectory%

@ECHO.
@ECHO If key file is not found, will generate a new key file...

@IF NOT EXIST %AssemblyKeyFile% sn -k %AssemblyKeyFile%

@ECHO.
@ECHO Building the project and deploying the sample assembly...

@msbuild %SolutionName% /t:Build /p:Configuration=Debug /fileLogger /fileLoggerParameters:LogFile=Build.log
@btstask AddResource -Type:System.BizTalk:BizTalkAssembly  -Source:bin\Debug\%AssemblyName%.dll -Options:GacOnAdd

@ECHO.
@ECHO Creating and binding the ports...

@%BTSTask% ImportBindings -Source:%BindingFileName% > Binding.log

@ECHO.
@ECHO Calling the WMI script to start the send port...

@CScript /NoLogo "..\..\Admin\WMI\Start Send Port\VBScript\StartSendPort.vbs" %SendPortName% %FileSendAddress%

@ECHO.
@ECHO Calling the WMI script to enable the receive location...

@CScript /NoLogo "..\..\Admin\WMI\Enable Receive Location\VBScript\EnableRecLoc.vbs" %ReceivePortName% %ReceiveLocationName% %FileReceiveLocation%

@ECHO.
@ECHO Calling the WMI script to enlist and start the orchestration...

@CScript /NoLogo "..\..\Admin\WMI\Enlist Orchestration\VBScript\EnlistOrch.vbs" %OrchestrationName% %AssemblyName% Start

@ECHO Deploying BAM Definition ....
%bmExe% deploy-all -DefinitionFile:%ActivityFile%
@ECHO.

@ENDLOCAL
@PAUSE
