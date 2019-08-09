@REM --------------------------------------------------------------------
@REM File: Cleanup.bat
@REM
@REM Summary: Calls scripts and programs to build, deploy, and start the sample.
@REM
@REM Sample: OperationsOM
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

set BAMDefFile=HelloOrchestration.xls
set TPEFile=HelloOrchestration.btt


@ECHO.
@ECHO Un-Deploying TPE and BAM Activity Definition file...

..\..\..\..\..\Tracking\bttdeploy.exe /remove %TPEFile%
..\..\..\..\..\Tracking\bm.exe remove-all -DefinitionFile:%BAMDefFile%
@ECHO.


cd ..\..\..\Orchestrations\HelloWorld
@ECHO renaming the output directory
rename Out2 Out

@ECHO. 
@ECHO Un-Installing the HelloWorld Orchestration scenario...
call cleanup.bat

@PAUSE
