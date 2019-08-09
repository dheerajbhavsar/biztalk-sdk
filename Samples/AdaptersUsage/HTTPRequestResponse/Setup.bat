@REM --------------------------------------------------------------------
@REM File: Setup.bat
@REM
@REM Summary: Calls scripts and programs to build, deploy, and start the sample.
@REM
@REM Sample: HTTP Request-Response
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

@SET SolutionName=HTTPRequestResponse.sln
@SET AssemblyKeyFile=HTTPRequestResponse.snk
@SET BindingFileName=HTTPRequestResponseBinding.xml
@SET OrchestrationName=Microsoft.Samples.BizTalk.HTTPRequestResponse.POReceiveOrchestration
@SET AssemblyName=HTTPRequestResponse

@ECHO.
@ECHO Configuring the IIS vRoot...

@CScript /NoLogo ..\..\ConfigureIIS.vbs RequestResponse \RequestResponse
@ECHO.
@ECHO Configuring the IIS vRoot for HTTP receive location...

@CScript /NoLogo ..\..\ConfigureIIS.vbs HTTPRequestResponseSample


@ECHO.
@ECHO If key file is not found, will generate a new key file...

@IF NOT EXIST %AssemblyKeyFile% sn -k %AssemblyKeyFile%

@ECHO.
@ECHO Building the sample Web project...

@msbuild RequestResponse\RequestResponse.sln /t:Build /p:Configuration=Debug /fileLogger /fileLoggerParameters:LogFile=Build.log

@ECHO.
@ECHO Building the project and deploying the sample assembly...

@msbuild %SolutionName% /t:Build /p:Configuration=Release /fileLogger /fileLoggerParameters:LogFile=Build.log

@gacutil /if bin\Release\%AssemblyName%.dll
@BTSTask AddApp -ApplicationName:HttpRequestResponseApplication
@BTSTask AddResource -ApplicationName:HttpRequestResponseApplication -Type:System.BizTalk:BizTalkAssembly -Source:bin\Release\%AssemblyName%.dll


@ECHO.
@ECHO Creating and binding the ports...

@BTSTask ImportBindings -Source:%BindingFileName% -ApplicationName:HttpRequestResponseApplication > Binding.log

@ECHO.
@ECHO Calling the WMI script to enlist and start the orchestration...

@CScript /NoLogo "..\..\Admin\WMI\Enlist Orchestration\VBScript\EnlistOrch.vbs" %OrchestrationName% %AssemblyName% Start

@ENDLOCAL

@PAUSE
