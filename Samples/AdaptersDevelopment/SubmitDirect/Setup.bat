@REM --------------------------------------------------------------------
@REM File: Setup.bat
@REM
@REM Summary: Calls scripts and programs to build, deploy, and start the sample.
@REM
@REM Sample: Adapter for programmatic message submission
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

@SET SolutionName=SubmitDirect.sln
@SET ProjectName=ProcessRequest\ProcessRequest.btproj
@SET ProjectKeyFile=ProcessRequest\ProcessRequest.snk
@SET UtilitiesKeyFile=TransportProxyUtils\TransportProxyUtils.snk
@SET SubmitMessagesBindingFileName=SubmitMessagesBinding.xml
@SET SubmitRequestBindingFileName=SubmitRequestBinding.xml
@SET OrchestrationName=Microsoft.Samples.BizTalk.ProcessRequest.Multiplier
@SET AssemblyName=ProcessRequest
@SET SendPortName=SubmitMessagesSP
@SET OneWayReceivePortName=SubmitMessagesRP
@SET OneWayReceiveLocationName=SubmitMessagesRL
@SET TwoWayReceivePortName=SubmitRequestRP
@SET TwoWayReceiveLocationName=SubmitRequestRL
@SET FileSendDirectory=Out
@SET FileSendAddress=\%FileSendDirectory%\%%MessageID%%.txt

@ECHO.
@ECHO Creating the send directory...

@IF NOT EXIST %FileSendDirectory% MD %FileSendDirectory%

@ECHO.
@ECHO If key files are not found, will generate new key files...

@IF NOT EXIST %ProjectKeyFile% sn -k %ProjectKeyFile%
@IF NOT EXIST %UtilitiesKeyFile% sn -k %UtilitiesKeyFile%

@ECHO.
@ECHO Building the project and deploying the sample assembly...

@msbuild %SolutionName% /t:Build /p:Configuration=Release /fileLogger /fileLoggerParameters:LogFile=Build.log

@gacutil /if ProcessRequest\bin\Deployment\%AssemblyName%.dll
@BTSTask AddApp -ApplicationName:SubmitDirectApplication
@BTSTask AddResource -ApplicationName:SubmitDirectApplication -Type:System.BizTalk:BizTalkAssembly -Source:ProcessRequest\bin\Deployment\%AssemblyName%.dll

@ECHO.
@ECHO Registering Submit adapter...
@TransportProxyUtilsReg\bin\Debug\RegisterAdapter.exe

@ECHO.
@ECHO Creating and binding the ports...

@BTSTask ImportBindings -Source:%SubmitMessagesBindingFileName% -ApplicationName:SubmitDirectApplication > Binding.log
@BTSTask ImportBindings -Source:%SubmitRequestBindingFileName% -ApplicationName:SubmitDirectApplication > Binding.log

@ECHO.
@ECHO Calling the WMI script to start the send port...

@CScript /NoLogo "..\..\Admin\WMI\Start Send Port\VBScript\StartSendPort.vbs" %SendPortName% %FileSendAddress%

@ECHO.
@ECHO Calling the WMI script to enable the receive location...

@CScript /NoLogo "..\..\Admin\WMI\Enable Receive Location\VBScript\EnableRecLoc.vbs" %OneWayReceivePortName% %OneWayReceiveLocationName%

@CScript /NoLogo "..\..\Admin\WMI\Enable Receive Location\VBScript\EnableRecLoc.vbs" %TwoWayReceivePortName% %TwoWayReceiveLocationName%

@ECHO.
@ECHO Calling the WMI script to enlist and start the orchestration...

@CScript /NoLogo "..\..\Admin\WMI\Enlist Orchestration\VBScript\EnlistOrch.vbs" %OrchestrationName% %AssemblyName% Start

@ENDLOCAL

@PAUSE
