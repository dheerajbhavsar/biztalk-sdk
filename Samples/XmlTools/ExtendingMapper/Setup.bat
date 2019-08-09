@REM --------------------------------------------------------------------
@REM File: Setup.bat
@REM
@REM Summary: Calls scripts and programs to build, deploy, and start the sample.
@REM
@REM Sample: Extending Mapper
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

@SET SolutionName=ExtendingMapper.sln
@SET LibraryAssemblyKeyFile=MapperClassLibrary\MapperClassLibrary.snk

@SET AssemblyKeyFile=ExtendingMapper.snk
@SET BindingFileName=ExtendingMapperBinding.xml
@SET SendPortName=ExtendingMapperSP
@SET ReceivePortName=ExtendingMapperRP
@SET ReceiveLocationName=ExtendingMapperRL
@SET FileReceiveDirectory=In
@SET FileReceiveLocation=\%FileReceiveDirectory%\*.xml
@SET FileSendDirectory=Out
@SET FileSendAddress=\%FileSendDirectory%\%%MessageID%%.xml
@SET ApplicationName=ExtendingMapperApplication

@ECHO.
@ECHO Creating the send and receive directories...

@IF NOT EXIST %FileReceiveDirectory% MD %FileReceiveDirectory%
@IF NOT EXIST %FileSendDirectory% MD %FileSendDirectory%

@ECHO.
@ECHO If key files is not found, will generate a new key file...

@IF NOT EXIST %AssemblyKeyFile% sn -k %AssemblyKeyFile%

@COPY %AssemblyKeyFile% MapperClassLibrary

@ECHO.
@ECHO Building the dependant project...
@msbuild MapperClassLibrary\MapperClassLibrary.csproj /t:Build /p:Configuration=Debug /fileLogger /fileLoggerParameters:LogFile=BuildMapperClassLibrary.log

@ECHO.
@ECHO We need to GAC the .NET assembly that will be called
@GacUtil /if MapperClassLibrary\bin\debug\Microsoft.Samples.BizTalk.ExtendingMapper.MapperClassLibrary.dll

@ECHO.
@ECHO IMPORTANT:
@ECHO If you wish to use the following maps Scriptor_CallExternalAssembly.btm
@ECHO or Scriptor_InlineXsltCallingExternalAssembly.btm then please refer to the
@ECHO documentation for additional setup steps before continuing.
@PAUSE

@ECHO Building the project and deploying the sample assembly...

@msbuild %SolutionName% /t:Build /p:Configuration=Release /fileLogger /fileLoggerParameters:LogFile=Build.log
@BTSTask AddApp -ApplicationName:%ApplicationName%
@BTSTask AddResource -ApplicationName:%ApplicationName% -Type:System.BizTalk:BizTalkAssembly -Source:bin\Release\Microsoft.Samples.BizTalk.ExtendingMapper.dll -Options:GacOnAdd

@ECHO.
@ECHO Creating and binding the ports...

@BTSTask ImportBindings -Source:%BindingFileName% -ApplicationName:%ApplicationName% > Binding.log

@ECHO.
@ECHO Calling the WMI script to start the send port...

@CScript /NoLogo "..\..\Admin\WMI\Start Send Port\VBScript\StartSendPort.vbs" %SendPortName% %FileSendAddress%

@ECHO.
@ECHO Calling the WMI script to enable the receive location...

@CScript /NoLogo "..\..\Admin\WMI\Enable Receive Location\VBScript\EnableRecLoc.vbs" %ReceivePortName% %ReceiveLocationName% %FileReceiveLocation%


@ENDLOCAL

@PAUSE
