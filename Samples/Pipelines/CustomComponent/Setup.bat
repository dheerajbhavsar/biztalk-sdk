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

@SET SolutionName=PipelineComponentSample\PipelineComponentSample.sln
@SET MessageAssemblyKeyFile=FixMsg\FixMsg.snk
@SET SampleAssemblyKeyFile=PipelineComponentSample\PipelineComponentSample.snk
@SET BindingFileName=PipelineComponentSample\PipelineComponentSampleBinding.xml
@SET SendPortName=PCSendPort
@SET ReceivePortName=PCReceivePort
@SET ReceiveLocationName=PCReceiveLocation
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

@IF NOT EXIST %MessageAssemblyKeyFile% sn -k %MessageAssemblyKeyFile%
@IF NOT EXIST %SampleAssemblyKeyFile% sn -k %SampleAssemblyKeyFile%

@ECHO.
@ECHO Building the pipeline component and copy it to the pipeline directory...

@msbuild FixMsg\FixMsg.sln /t:Build /p:Configuration=Debug /fileLogger /fileLoggerParameters:LogFile=Build.log
@COPY FixMsg\bin\Debug\FixMsg.dll "..\..\..\..\Pipeline Components\"
@gacutil -uf FixMsg
@gacutil -if "..\..\..\..\Pipeline Components\FixMsg.dll"

@ECHO.
@ECHO Building the project and deploying the sample assembly...

@msbuild %SolutionName% /t:Build /p:Configuration=Release /fileLogger /fileLoggerParameters:LogFile=Build.log

@gacutil /if PipelineComponentSample\bin\Release\PipelineComponentSample.dll
@BTSTask AddApp -ApplicationName:CustomComponentApplication
@BTSTask AddResource -ApplicationName:CustomComponentApplication -Type:System.BizTalk:BizTalkAssembly -Source:PipelineComponentSample\bin\Release\PipelineComponentSample.dll

@ECHO.
@ECHO Creating and binding the ports...

@BTSTask ImportBindings -Source:%BindingFileName% -ApplicationName:CustomComponentApplication > Binding.log

@ECHO.
@ECHO Calling the WMI script to start the send port...

@CScript /NoLogo "..\..\Admin\WMI\Start Send Port\VBScript\StartSendPort.vbs" %SendPortName% %FileSendAddress%

@ECHO.
@ECHO Calling the WMI script to enable the receive location...

@CScript /NoLogo "..\..\Admin\WMI\Enable Receive Location\VBScript\EnableRecLoc.vbs" %ReceivePortName% %ReceiveLocationName% %FileReceiveLocation%

@ENDLOCAL

@PAUSE
