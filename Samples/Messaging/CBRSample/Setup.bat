@REM --------------------------------------------------------------------
@REM File: Setup.bat
@REM
@REM Summary: Calls scripts and programs to build, deploy, and start the sample.
@REM
@REM Sample: Content Based Routing
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

@SET SolutionName=CBRSample.sln
@SET AssemblyKeyFile=CBR.snk
@SET BindingFileName=CBRSampleBinding.xml
@SET SendPortNameUS=CBRUSSendPort
@SET SendPortNameCAN=CBRCANSendPort
@SET ReceivePortName=CBRReceivePort
@SET ReceiveLocationName=CBRReceiveLocation
@SET FileReceiveDirectory=In
@SET FileReceiveLocation=\%FileReceiveDirectory%\*.xml
@SET FileSendDirectoryUS=US
@SET FileSendAddressUS=\%FileSendDirectoryUS%\%%MessageID%%.xml
@SET FileSendDirectoryCAN=CAN
@SET FileSendAddressCAN=\%FileSendDirectoryCAN%\%%MessageID%%.xml

@ECHO.
@ECHO Creating the send and receive directory...

@IF NOT EXIST %FileReceiveDirectory% MD %FileReceiveDirectory%
@IF NOT EXIST %FileSendDirectoryUS% MD %FileSendDirectoryUS%
@IF NOT EXIST %FileSendDirectoryCAN% MD %FileSendDirectoryCAN%

@ECHO.
@ECHO Generating a new key file...

@IF NOT EXIST %AssemblyKeyFile% sn -k %AssemblyKeyFile%

@ECHO.
@ECHO Building the project and deploying the sample assembly...

@msbuild %SolutionName% /t:Build /p:Configuration=Debug /fileLogger /fileLoggerParameters:LogFile=Build.log
@gacutil /if bin\Debug\CBRSample.dll
@BTSTask AddApp -ApplicationName:CBRApplication
@BTSTask AddResource -ApplicationName:CBRApplication -Type:System.BizTalk:BizTalkAssembly -Source:bin\Debug\CBRSample.dll

@ECHO.
@ECHO Creating and binding the ports...

@BTSTask ImportBindings -Source:%BindingFileName% -ApplicationName:CBRApplication > Binding.log

@ECHO.
@ECHO Calling the WMI script to start the send ports...

@CScript /NoLogo "..\..\Admin\WMI\Start Send Port\VBScript\StartSendPort.vbs" %SendPortNameUS% %FileSendAddressUS%
@CScript /NoLogo "..\..\Admin\WMI\Start Send Port\VBScript\StartSendPort.vbs" %SendPortNameCAN% %FileSendAddressCAN%

@ECHO.
@ECHO Calling the WMI script to enable the receive location...

@CScript /NoLogo "..\..\Admin\WMI\Enable Receive Location\VBScript\EnableRecLoc.vbs" %ReceivePortName% %ReceiveLocationName% %FileReceiveLocation%

@ENDLOCAL

@PAUSE
