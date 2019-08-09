@REM --------------------------------------------------------------------
@REM File: Setup.bat
@REM
@REM Summary: Calls scripts and programs to build, deploy, and start the sample.
@REM
@REM Sample: SampleApplication1
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
@IF ERRORLEVEL 1 (
	@ECHO.
	@ECHO Unable to setup environment variables for Visual Studio
	@GOTO END
)

@SET SolutionName=BusinessRulesHelloWorld1.sln
@SET AssemblyName=BusinessRulesHelloWorld1

@ECHO.
@ECHO Building the solution ...
@IF NOT EXIST MySampleLibrary\MySampleLibrary.snk sn -k MySampleLibrary\MySampleLibrary.snk

@msbuild %SolutionName% /t:Build /p:Configuration=Debug /fileLogger /fileLoggerParameters:LogFile=Build.log

@ECHO GACing the sample library

@GacUtil /i MySampleLibrary\bin\Debug\MySampleLibrary.dll /f

:END
@ENDLOCAL
@PAUSE
