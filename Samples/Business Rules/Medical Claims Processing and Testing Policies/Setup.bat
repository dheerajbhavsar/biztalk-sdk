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
@SET SolutionName=RulesForMedicalClaims.sln
@SET AssemblyName=RulesForMedicalClaims
@SET AssemblyKeyFile1=Claims.snk
@SET AssemblyKeyFile2=FactRetrieverForClaimsProcessing.snk

@ECHO.
@ECHO Building business object DLL ...

@cd Claims
@IF NOT EXIST %AssemblyKeyFile1% sn -k %AssemblyKeyFile1%
@cd ..

@msbuild Claims\Claims.sln /t:Build /p:Configuration=Debug /fileLogger /fileLoggerParameters:LogFile=Build.log

@cd FactRetrieverForClaimsProcessing
@IF NOT EXIST %AssemblyKeyFile2% sn -k %AssemblyKeyFile2%
@cd ..

@msbuild FactRetrieverForClaimsProcessing\FactRetrieverForClaimsProcessing.sln /t:Build /p:Configuration=Debug /fileLogger /fileLoggerParameters:LogFile=Build.log

@ECHO.
@ECHO Building the sample ...

@msbuild RulesForMedicalClaims\%SolutionName% /t:Build /p:Configuration=Debug /fileLogger /fileLoggerParameters:LogFile=Build.log

@ECHO.
@ECHO Copying necessary files ...
@copy Claims\bin\Debug\Claims.dll RulesForMedicalClaims\bin\Debug
@copy FactRetrieverForClaimsProcessing\bin\Debug\FactRetrieverForClaimsProcessing.dll RulesForMedicalClaims\bin\Debug

@ECHO.
@ECHO Creating PolicyValidity table in Northwind Database and inserting sample data 
@osql -E -i Create_PolicyValidity_Table.sql

:END
@ENDLOCAL
@PAUSE
