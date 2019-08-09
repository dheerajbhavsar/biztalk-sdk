@REM --------------------------------------------------------------------
@REM File: Setup.bat
@REM
@REM Summary: Calls scripts and programs to build, deploy, and start the sample.
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

@SET SolutionName=SendMail.sln
@SET AssemblyKeyFile=SendMail.snk
@SET FileReceiveDirectory=In
@SET OrchestrationName=Microsoft.Samples.BizTalk.SendMail.ReceiveSend
@SET AssemblyName=SendMail
@SET SMTPServerName=SMTPServer
@SET FromEmailAddress=someone@example.com

@ECHO.
@ECHO Creating the receive directory...

@IF NOT EXIST %FileReceiveDirectory% MD %FileReceiveDirectory%

@ECHO.
@ECHO Configuring the SMTP Send Handler...

@CScript /NoLogo "..\..\Admin\WMI\Set Send Handler Property\VBScript\ConfigureSMTP.vbs" %SMTPServerName% %FromEmailAddress%

@ECHO.
@ECHO If key file is not found, will generate a new key file...

@IF NOT EXIST %AssemblyKeyFile% sn -k %AssemblyKeyFile%

@ECHO.
@ECHO Building the project...

@msbuild %SolutionName% /t:Build /p:Configuration=Debug /fileLogger /fileLoggerParameters:LogFile=Build.log

@ECHO.
@ECHO Deploying the sample assembly...

@btstask AddResource -Type:System.BizTalk:BizTalkAssembly  -Source:bin\Debug\%AssemblyName%.dll -Options:GacOnAdd

@ECHO.
@ECHO Calling the WMI script to enlist and start the orchestration...

@CScript /NoLogo "..\..\Admin\WMI\Enlist Orchestration\VBScript\EnlistOrch.vbs" %OrchestrationName% %AssemblyName% Start

@ECHO.
@ECHO The sample has been built and delpoyed, but if this was not run
@ECHO from 'C:\Program Files (x86)\Microsoft BizTalk Server 2016\SDK\Samples\AdaptersUsage\SendMail'
@ECHO you will need to update the Receive Location mannually using BizTalk Explorer.
@ECHO Please see BizTalk documentation for details.
@ECHO.

@ENDLOCAL

@PAUSE
