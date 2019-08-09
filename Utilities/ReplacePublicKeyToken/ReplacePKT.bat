@REM
@REM File: ReplacePKT.bat
@REM
@REM Summary: Replaces occurrences of old public key token in the 
@REM specified file with a new one.
@REM
@REM --------------------------------------------------------------------
@REM This file is part of the Microsoft BizTalk Server SDK
@REM
@REM Copyright (c) Microsoft Corporation. All rights reserved.
@REM
@REM This source code is intended only as a supplement to Microsoft
@REM BizTalk Server  release and/or on-line documentation. See these
@REM other materials for detailed information regarding Microsoft code samples.
@REM
@REM THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY
@REM KIND, WHETHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
@REM IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
@REM PARTICULAR PURPOSE.
@REM --------------------------------------------------------------------

@SETLOCAL

@ECHO.

@CALL "%VS110COMNTOOLS%vsvars32.bat"
@IF ERRORLEVEL 1 (
        @ECHO.
        @ECHO Unable to setup environment variables for Visual Studio
        @GOTO END
)

@SET SNK_FILE=%~f1
@SET OLD_PKT=%2
@SET FILE_TO_REPLACE=%~f3
@SET EXTRA_PARAM=%4

@IF NOT DEFINED SNK_FILE (
	@ECHO.
	@GOTO USAGE
)
@IF NOT DEFINED FILE_TO_REPLACE  (
	@IF DEFINED OLD_PKT (
		@ECHO.
		@GOTO USAGE
	)
)
@IF DEFINED EXTRA_PARAM (
	@ECHO.
	@GOTO USAGE
)

@REM Extract the public key token from the key file

@SET TEMP_PK=%TEMP%\__TPK
@SET TEMP_TOKEN=%TEMP%\__TPK_TOKEN
del /q %TEMP_PK%
del /q %TEMP_TOKEN%
sn -p "%SNK_FILE%" %TEMP_PK%
sn -t %TEMP_PK% >%TEMP_TOKEN%
del /q %TEMP_PK%

@REM change dir to script folder
@pushd %~f0\..

@IF DEFINED OLD_PKT (
	@REM update file with new public key token
	cscript ReplacePKT.wsf %TEMP_TOKEN% %OLD_PKT% "%FILE_TO_REPLACE%"
)

echo XXXX > %TEMP%\___TPK.txt
cscript ReplacePKT.wsf %TEMP_TOKEN% XXXX %TEMP%\___TPK.txt

@ENDLOCAL

for /f %%i in ('type %TEMP%\___TPK.txt') do set NEWTOKEN=%%i
del /q %TEMP%\___TPK.txt

@popd

@GOTO END



:USAGE
@ECHO "Usage: ReplacePKT.bat <.snk file> [<old public key token>] [<file to replace>]"
@ECHO.
@ECHO.


:END



