@REM --------------------------------------------------------------------
@REM File: Setup.bat
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
@ECHO This sample uses the HelloWorld Orchestration scenario.
@PAUSE

@ECHO. 
@ECHO Installing the HelloWorld Orchestration scenario...
cd ..\..\..\Orchestrations\HelloWorld
call Setup.bat
cd ..\..\Admin\OperationsOM\OperationsSamples

@ECHO replace the old public key token with the new one...
@PAUSE
cd ..\..\..\..\Utilities\ReplacePublicKeyToken
Call ReplacePKT.bat ..\..\Samples\Orchestrations\HelloWorld\HelloWorld.snk 5f921ce24f897941 ..\..\Samples\Admin\OperationsOM\OperationsSamples\HelloOrchestration.btt
cd ..\..\Samples\Admin\OperationsOM\OperationsSamples


@ECHO.
@ECHO Deploying BAM Activity Definition and TPE file for the Hello Orchestration Scneario...

..\..\..\..\..\Tracking\bm.exe deploy-all -DefinitionFile:%BAMDefFile%
..\..\..\..\..\Tracking\bttdeploy.exe %TPEFile%

@ECHO. 

@ECHO renaming the output directory to create a failure send port...
cd ..\..\..\Orchestrations\HelloWorld
rename Out Out2

@ECHO drop a file to the receive location
copy SamplePOInput.xml In\
cd ..\..\Admin\OperationsOM\OperationsSamples



@ENDLOCAL

@PAUSE
