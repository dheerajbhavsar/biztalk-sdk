@REM --------------------------------------------------------------------
@REM File: Setup.bat
@REM
@REM Summary: Calls scripts and programs to build, deploy, and start the sample.
@REM
@REM Sample: Envelope Processing
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

@SET SolutionName=CustomPartyResolution.sln
@SET ComponentProjectName=CustomPartyResolutionPipelineComponent\CustomPartyResolutionPipelineComponent.csproj
@SET BizTalkProjectName=CustomPartyResolutionPipeline\CustomPartyResolutionPipeline.btproj
@SET PartyResolverProjectName=PartyLookup\PartyLookup.csproj
@SET AssemblyKeyFile=CustomPartyResolution.snk
@SET BindingFileName=CustomPartyResolutionBinding.xml
@SET SendPortName=CustomPartyResolutionSendPort
@SET ReceivePortName=CustomPartyResolutionReceivePort
@SET ReceiveLocationName=CustomPartyResolutionRecieveLocation
@SET FileReceiveDirectory=In
@SET FileReceiveLocation=\%FileReceiveDirectory%\*.xml
@SET FileSendDirectory=Out
@SET FileSendAddress=\%FileSendDirectory%\%%MessageID%%.xml

@ECHO.
@ECHO Creating the send and receive directory...

@IF NOT EXIST %FileReceiveDirectory% MD %FileReceiveDirectory%
@IF NOT EXIST %FileSendDirectory% MD %FileSendDirectory%

@ECHO.
@ECHO If key file is not found, will generate a new key file...

@IF NOT EXIST %AssemblyKeyFile% sn -k %AssemblyKeyFile%

@ECHO.
@ECHO Building the project and deploying the sample assembly...

@msbuild %PartyResolverProjectName% /t:Build /p:Configuration=Debug /fileLogger /fileLoggerParameters:LogFile=Build.log

@ECHO.
@ECHO Building the pipeline component and copy it to the pipeline directory...

@msbuild %ComponentProjectName% /t:Build /p:Configuration=Debug /fileLogger /fileLoggerParameters:LogFile=Build.log

@COPY CustomPartyResolutionPipelineComponent\bin\Debug\CustomPartyResolutionPipelineComponent.dll  "..\..\..\..\Pipeline Components\"

@ECHO Placing the assembly in the GAC
@gacutil /i CustomPartyResolutionPipelineComponent\bin\Debug\CustomPartyResolutionPipelineComponent.dll
@ECHO.
@ECHO Building the project and deploying the sample assembly...

@msbuild %SolutionName% /t:Build /p:Configuration=Release /fileLogger /fileLoggerParameters:LogFile=Build.log

@gacutil /if CustomPartyResolutionPipeline\bin\Release\CustomPartyResolutionPipeline.dll
@BTSTask AddApp -ApplicationName:PartyResolutionApplication
@BTSTask AddResource -ApplicationName:PartyResolutionApplication -Type:System.BizTalk:BizTalkAssembly -Source:CustomPartyResolutionPipeline\bin\Release\CustomPartyResolutionPipeline.dll

@ECHO.
@ECHO Creating and binding the ports...

@BTSTask ImportBindings -Source:%BindingFileName% -ApplicationName:PartyResolutionApplication > Binding.log

@ECHO.
@ECHO Calling the WMI script to start the send port...

@CScript /NoLogo "..\..\Admin\WMI\Start Send Port\VBScript\StartSendPort.vbs" "%SendPortName%" "%FileSendAddress%"

@ECHO.
@ECHO Calling the WMI script to enable the receive location...

@CScript /NoLogo "..\..\Admin\WMI\Enable Receive Location\VBScript\EnableRecLoc.vbs" "%ReceivePortName%" "%ReceiveLocationName%" "%FileReceiveLocation%"

@ATTRIB -r S*.XML 
gacutil /if PartyLookup\bin\Debug\Microsoft.Samples.BizTalk.PartyLookup.dll
@ENDLOCAL

@PAUSE
