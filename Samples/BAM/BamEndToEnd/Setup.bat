@REM --------------------------------------------------------------------
@REM File: Setup.bat
@REM
@REM Summary:Calls scripts and programs to build, deploy, and start the sample.
@REM
@REM Sample: BAM end to end sample
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

@SET SolutionName=BamEndToEnd.sln
@SET ActivityFile=BamEndToEnd.xls
@SET FileReceiveDirectory=Input
@SET FileSendDirectory=Output
@SET AssemblyKeyFile=key.snk
@SET SendPortName=BamEndToEnd_SendPort
@SET FileSendAddress=\%FileSendDirectory%\%%MessageID%%.xml
@SET ReceivePortName=BamEndToEnd_ReceivePort
@SET ReceiveLocationName=BamEndToEnd_ReceiveLocation
@SET FileReceiveLocation=\%FileReceiveDirectory%\*.xml
@SET OrchestrationName1=BamEndToEnd.Services.Orchestration1
@SET OrchestrationName2=BamEndToEnd.Services.Orchestration2
@SET OrchestrationName3=BamEndToEnd.Services.Orchestration3
@SET AssemblyName=Microsoft.Samples.BizTalk.BamEndToEnd.Services
@SET BindingFileName=BamEndToEndBinding.xml
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
@ECHO Building the project

@msbuild %SolutionName% /t:Build /p:Configuration=Debug /fileLogger /fileLoggerParameters:LogFile=Build.log

@xcopy "Services\bin\Development\Microsoft.Samples.BizTalk.BamEndToEnd.Components.dll" "..\..\..\..\Pipeline Components" /y

@ECHO.
@ECHO GACing the sample assembly

@gacutil /if Services\bin\Development\%AssemblyName%.dll

@ECHO.
@ECHO Deploying the sample assembly...

@btstask AddResource -Type:System.BizTalk:BizTalkAssembly  -Source:Services\bin\Development\%AssemblyName%.dll -Options:GacOnAdd

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
@ECHO Calling the WMI script to enlist and start the orchestrations...

@CScript /NoLogo "..\..\Admin\WMI\Enlist Orchestration\VBScript\EnlistOrch.vbs" %OrchestrationName3% %AssemblyName% Start
@CScript /NoLogo "..\..\Admin\WMI\Enlist Orchestration\VBScript\EnlistOrch.vbs" %OrchestrationName2% %AssemblyName% Start
@CScript /NoLogo "..\..\Admin\WMI\Enlist Orchestration\VBScript\EnlistOrch.vbs" %OrchestrationName1% %AssemblyName% Start

@ECHO.
@ECHO Deploying BAM Definition ....
%bmExe% deploy-all -DefinitionFile:%ActivityFile%

@ENDLOCAL
@PAUSE
