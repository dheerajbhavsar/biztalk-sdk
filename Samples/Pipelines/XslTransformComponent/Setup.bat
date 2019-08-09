@REM --------------------------------------------------------------------
@REM File: Setup.bat
@REM
@REM Summary: Calls scripts and programs to build, deploy, and start the sample.
@REM
@REM Sample: XSLTransformComponent
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

@SET SolutionName=XslTransformComponent.sln
@SET ComponentProjectName=XslTransform\XslTransform.csproj
@SET BizTalkProjectName=SendHtmlMessage\SendHtmlMessage.btproj
@SET AssemblyKeyFile=SendHtmlMessage\SendHtmlMessage.snk
@SET BindingFileName=XslTransformComponentBinding.XML
@SET BizTalkHostName=BizTalkServerApplication
@SET ReceivePortName=XSLTComponentRP
@SET SendPortName=XSLTComponentSP
@SET ReceiveLocationName=XSLTComponentRL
@SET FileReceiveDirectory=In
@SET FileReceiveLocation=\%FileReceiveDirectory%\*.xml
@SET FileSendDirectory=Out
@SET FileSendAddress=\%FileSendDirectory%\%%MessageID%%.html
@SET AssemblyName=XslTransform
@SET SMTPServerName=SMTPServer
@SET FromEmailAddress=fromsomeone@example.com


@ECHO.
@ECHO Creating the send and receive directories...

@IF NOT EXIST %FileReceiveDirectory% MD %FileReceiveDirectory%
@IF NOT EXIST %FileSendDirectory% MD %FileSendDirectory%

@ECHO.
@ECHO Configuring the SMTP Send Handler...

@CScript /NoLogo "..\..\Admin\WMI\Set Send Handler Property\VBScript\ConfigureSMTP.vbs" %SMTPServerName% %FromEmailAddress%

@ECHO.
@ECHO Generating a new key file...

@IF EXIST %AssemblyKeyFile% DEL /F /Q %AssemblyKeyFile%
@sn -k %AssemblyKeyFile%

@ECHO.
@ECHO Building the pipeline component and copy it to the pipeline directory...

@msbuild %ComponentProjectName% /t:Build /p:Configuration=Debug /fileLogger /fileLoggerParameters:LogFile=Build.log

@IF NOT EXIST "..\..\..\..\Pipeline Components\XslTransform.dll" COPY XslTransform\bin\Debug\XslTransform.dll "..\..\..\..\Pipeline Components\"
@IF NOT EXIST "..\..\..\..\Pipeline Components\transform.xsl" COPY SendHtmlMessage\transform.xsl "..\..\..\..\Pipeline Components\"

@ECHO.
@ECHO Building the project and deploying the sample assembly...

@msbuild %SolutionName% /t:Build /p:Configuration=Release /fileLogger /fileLoggerParameters:LogFile=Build.log

@gacutil /if SendHtmlMessage\bin\Release\SendHtmlMessage.dll
@BTSTask AddApp -ApplicationName:XslTransformApplication
@BTSTask AddResource -ApplicationName:XslTransformApplication -Type:System.BizTalk:BizTalkAssembly -Source:SendHtmlMessage\bin\Release\SendHtmlMessage.dll

@ECHO.
@ECHO Creating and binding the ports...

@BTSTask ImportBindings -Source:%BindingFileName% -ApplicationName:XslTransformApplication > Binding.log

@ECHO.
@ECHO Calling the WMI script to start the send port...

@CScript /NoLogo "..\..\Admin\WMI\Start Send Port\VBScript\StartSendPort.vbs" %SendPortName% %FileSendAddress%

@ECHO.
@ECHO Calling the WMI script to enable the receive location...

@CScript /NoLogo "..\..\Admin\WMI\Enable Receive Location\VBScript\EnableRecLoc.vbs" %ReceivePortName% %ReceiveLocationName% %FileReceiveLocation%

@PAUSE
