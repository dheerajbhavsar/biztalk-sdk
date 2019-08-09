@REM
@REM File: Build.bat
@REM
@REM Summary: This script builds this sample out.
@REM
@REM
@REM --------------------------------------------------------------------
@REM This file is part of the Microsoft BizTalk Server SDK
@REM
@REM Copyright (c) Microsoft Corporation. All rights reserved.
@REM
@REM This source code is intended only as a supplement to Microsoft
@REM BizTalk Server release and/or on-line documentation. See these
@REM other materials for detailed information regarding Microsoft code samples.
@REM
@REM THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY
@REM KIND, WHETHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
@REM IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
@REM PARTICULAR PURPOSE.
@REM --------------------------------------------------------------------
echo off

@SETLOCAL

CALL "%VS140COMNTOOLS%vsvars32.bat"

SET SolutionName=.\HelloApplicationDeployment\HelloApplicationDeployment.sln
SET AssemblyKeyFile=.\HelloApplicationDeployment\HelloApplicationDeployment.snk

ECHO.
ECHO If key file is not found, will generate a new key file...
IF NOT EXIST %AssemblyKeyFile% sn -k %AssemblyKeyFile%

ECHO.
ECHO Building the project and deploying the sample assembly...
@msbuild %SolutionName% /t:Build /p:Configuration=Release /fileLogger /fileLoggerParameters:LogFile=Build.log
@btstask AddResource -Type:System.BizTalk:BizTalkAssembly  -Source:dlls\Schemas.dll -Options:GacOnAdd
@btstask AddResource -Type:System.BizTalk:BizTalkAssembly  -Source:dlls\Maps.dll -Options:GacOnAdd
@btstask AddResource -Type:System.BizTalk:BizTalkAssembly  -Source:dlls\Orchestrations.dll -Options:GacOnAdd
