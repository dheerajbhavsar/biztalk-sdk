@REM --------------------------------------------------------------------
@REM File: Setup.bat
@REM
@REM Summary: Calls scripts and programs to build and deploy the sample.
@REM
@REM Sample: MQSSendPipelineComponent
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

@SET PipelineSolutionName="SetMQSeriesHeaderPropertyPipeline\SetMQSeriesHeaderPropertyPipeline.sln"
@SET PipelineComponentSolutionName="SetMQSeriesHeaderPropertyComponent\SetMQSeriesHeaderPropertyComponent.sln"
@SET AssemblyKeyFile=SetMQSeriesHeaderPropertyPipeline.snk
@SET PipelineComponent="SetMQSeriesHeaderPropertyComponent\bin\Release\SetMQSeriesHeaderPropertyComponent.dll"

@ECHO.
@ECHO If key file is not found, will generate a new key file...
@IF NOT EXIST %AssemblyKeyFile% sn -k %AssemblyKeyFile%

@ECHO.
@ECHO Building the pipeline component project and deploying the sample assembly...

@msbuild %PipelineComponentSolutionName% /t:Build /p:Configuration=Release /fileLogger /fileLoggerParameters:LogFile=Build.log

@IF EXIST %PipelineComponent% copy %PipelineComponent% "..\..\..\..\..\Pipeline Components\SetMQSeriesHeaderPropertyComponent.dll"

@msbuild %PipelineSolutionName% /t:Build /p:Configuration=Debug /fileLogger /fileLoggerParameters:LogFile=Build2.log

@btstask AddResource -Type:System.BizTalk:BizTalkAssembly  -Source:SetMQSeriesHeaderPropertyPipeline\bin\Debug\SetMQSeriesHeaderPropertyPipeline.dll -Options:GacOnAdd

@ENDLOCAL

@PAUSE
