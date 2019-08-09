@REM --------------------------------------------------------------------
@REM File: Setup.bat
@REM
@REM Summary: Custom HTTP adapter using the BizTalk Server Adapter
@REM Framework.
@REM
@REM Sample: HTTP Adapter
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


@SET SolutionName=HttpAdapter.sln

@SET VirtualDirectoryName=HttpReceive
@SET VirtualDirectoryFolder=HttpReceiveVDir

@ECHO.
@ECHO If key file is not found, will generate a new key file...
@IF NOT EXIST ..\BaseAdapter\v1.0.2\BaseAdapter.snk sn -k ..\BaseAdapter\v1.0.2\BaseAdapter.snk


@ECHO.
@ECHO Building the Solution...

@msbuild %SolutionName% /t:Build /p:Configuration=Debug /fileLogger /fileLoggerParameters:LogFile=Build.log

@ECHO Creating a folder for virtual directory and copying binary files...

@IF NOT EXIST %VirtualDirectoryFolder% MD %VirtualDirectoryFolder%
@IF NOT EXIST %VirtualDirectoryFolder%\bin MD %VirtualDirectoryFolder%\bin
@COPY /Y Run-time\HttpReceive\bin\Debug\* %VirtualDirectoryFolder%\bin

@ECHO Copying ASP.NET configuration files...

@COPY /Y Run-time\Global.asax %VirtualDirectoryFolder%
@COPY /Y Run-time\Web.config %VirtualDirectoryFolder%


@ECHO.
@ECHO Configuring the IIS vRoot...

@CScript /NoLogo ..\..\ConfigureIIS.vbs %VirtualDirectoryName% \%VirtualDirectoryFolder%

@ENDLOCAL

@PAUSE
