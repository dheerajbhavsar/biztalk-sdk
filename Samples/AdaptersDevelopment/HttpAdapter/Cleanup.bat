@REM --------------------------------------------------------------------
@REM File: Cleanup.bat
@REM
@REM Summary: Calls scripts and programs to stop and undeploy the sample.
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

@SET VirtualDirectoryName=HttpReceive
@SET VirtualDirectoryFolder=HttpReceiveVDir


@ECHO.
@ECHO Removing the IIS vDir...

@CScript /NoLogo ..\..\RemoveIISVDir.vbs %VirtualDirectoryName%

@ECHO Removing the virtual directory files...

@IF EXIST %VirtualDirectoryFolder% RMDIR /S /Q %VirtualDirectoryFolder%

@ENDLOCAL

@PAUSE
