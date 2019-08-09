@REM --------------------------------------------------------------------
@REM File: Setup.bat
@REM
@REM Summary: Calls scripts and programs to build, deploy, and start the sample.
@REM
@REM Sample: Schema Resolver Pipeline Component
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

@SET ComponentSolutionName=SchemaResolverFlatFileDasm\SchemaResolverFlatFileDasm.sln
@SET SolutionName=SchemaResolverSample\SchemaResolverSample.sln
@SET SampleAssemblyKeyFile=SchemaResolverSample\SchemaResolver.snk
@SET BindingFileName=SchemaResolverBinding.xml
@SET SendPortName=SchemaResolverSP
@SET ReceivePortName=SchemaResolverRP
@SET ReceiveLocationName=SchemaResolverRL
@SET FileReceiveDirectory=In
@SET FileReceiveLocation=\%FileReceiveDirectory%\*.txt
@SET FileSendDirectory=Out
@SET FileSendAddress=\%FileSendDirectory%\%%MessageID%%.xml

@ECHO.
@ECHO If key files are not found, will generate new key files...
@IF NOT EXIST %SampleAssemblyKeyFile% sn -k %SampleAssemblyKeyFile%

@ECHO.
@ECHO Building the pipeline component and copy it to the pipeline directory...
@msbuild %ComponentSolutionName% /t:Build /p:Configuration=Debug /p:Platform="Any CPU" /fileLogger /fileLoggerParameters:LogFile=Build.log
@COPY SchemaResolverFlatFileDasm\bin\Debug\Microsoft.Samples.BizTalk.SchemaResolverFlatFileDasm.dll "..\..\..\..\Pipeline Components\"

@ECHO.
@ECHO Creating the send and receive directories...
@IF NOT EXIST %FileReceiveDirectory% MD %FileReceiveDirectory%
@IF NOT EXIST %FileSendDirectory% MD %FileSendDirectory%

@ECHO.
@ECHO Building the project and deploying the sample assembly...
@msbuild %SolutionName% /t:Build /p:Configuration=Debug /fileLogger /fileLoggerParameters:LogFile=Build.log

@BTSTask AddApp -ApplicationName:SchemaResolverApplication
@BTSTask AddResource -ApplicationName:SchemaResolverApplication -Type:System.BizTalk:BizTalkAssembly -Source:SchemaResolverSample\bin\Debug\Microsoft.Samples.BizTalk.SchemaResolver.dll -Options:GacOnAdd

@ECHO.
@ECHO Creating and binding the ports...
@BTSTask ImportBindings -Source:%BindingFileName% -ApplicationName:SchemaResolverApplication > Binding.log

@ECHO.
@ECHO Calling the WMI script to start the send port...
@CScript /NoLogo "..\..\Admin\WMI\Start Send Port\VBScript\StartSendPort.vbs" %SendPortName% %FileSendAddress%

@ECHO.
@ECHO Calling the WMI script to enable the receive location...
@CScript /NoLogo "..\..\Admin\WMI\Enable Receive Location\VBScript\EnableRecLoc.vbs" %ReceivePortName% %ReceiveLocationName% %FileReceiveLocation%

@ENDLOCAL

@PAUSE
