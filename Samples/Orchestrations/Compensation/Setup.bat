@REM --------------------------------------------------------------------
@REM File: Setup.bat
@REM
@REM Summary: Calls scripts and programs to build, deploy, and start the sample.
@REM
@REM Sample: BizTalk Orchestration Compensation
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

@SET SolutionName=CompensationSample.sln
@SET AssemblyKeyFile=CompensationSample.snk
@SET BindingFileName=CompensationSampleBinding.xml
@SET SendPortName=ToCustomerSendPort
@SET FileSendDirectory=Out
@SET FileSendAddress=\%FileSendDirectory%\%%MessageID%%.xml
@SET OrchestrationName=Microsoft.Samples.BizTalk.Compensation.UpdateContact
@SET AssemblyName=CompensationOrchestration

@ECHO.
@ECHO Creating the send directory...

@IF NOT EXIST %FileSendDirectory% MD %FileSendDirectory%

@ECHO.
@ECHO If key file is not found, will generate a new key file...

@IF NOT EXIST %AssemblyKeyFile% sn -k %AssemblyKeyFile%

@ECHO.
@ECHO Building the project...

@msbuild %SolutionName% /t:Build /p:Configuration=Debug /fileLogger /fileLoggerParameters:LogFile=Build.log

@ECHO.
@ECHO Deploying the sample assembly...

@btstask AddResource -Type:System.BizTalk:BizTalkAssembly  -Source:bin\Release\%AssemblyName%.dll -Options:GacOnAdd

@ECHO.
@ECHO Build complete. Stand by to launch the BizTalk Web Service Wizard...
@ECHO.
@PAUSE
@CLS
@ECHO Instructions for completing the wizard:
@ECHO.
@ECHO * "Create Web Service" page: Accept defaults.
@ECHO.
@ECHO * "BizTalk Assembly" page: 
@ECHO    Use "%CD%\bin\release\CompensationOrchestration.dll"
@ECHO.
@ECHO * "Orchestration and Ports" page: Accept defaults.
@ECHO.
@ECHO * "Web Service Properties" page: Set the namespace to 
@ECHO   "http://Microsoft.BizTalk.Samples.Compensation/". 
@ECHO   Accept all other defaults.
@ECHO.
@ECHO * "Web Service Project" page: 
@ECHO   Set the project location to "CompensationOrchestrationWebServiceProxy". 
@ECHO   Check both boxes at the bottom of the page.
@ECHO.
@ECHO * "Web Service Project Summary" page: Click the "Create >" button.
@ECHO.
@ECHO * "Completing the BizTalk Web Services Publishing Wizard" page: 
@ECHO   Click the "Finish" button.
@ECHO.
@BTSWebSvcWiz
@ECHO.

@ECHO Creating and binding the ports...


@BTSTask ImportBindings -Source:%BindingFileName% > Binding.log

@ECHO.
@ECHO Calling the WMI script to start the send port...

@CScript /NoLogo "..\..\Admin\WMI\Start Send Port\VBScript\StartSendPort.vbs" %SendPortName% %FileSendAddress%

@ECHO.
@ECHO Creating backend database...
@osql -E -i CreateSQLDataStore.sql
@ECHO.

@ECHO.
@ECHO Adding assemblies to the GAC...
@gacutil -uf updatecrm
@gacutil -uf updatemailinglist
@gacutil -if bin\release\updatecrm.dll
@gacutil -if bin\release\updatemailinglist.dll

@ECHO.
@ECHO Calling the WMI script to enlist and start the orchestration...

@CScript /NoLogo "..\..\Admin\WMI\Enlist Orchestration\VBScript\EnlistOrch.vbs" %OrchestrationName% %AssemblyName% Start
@ECHO.
@PAUSE
@CLS

@ECHO.
@ECHO Before running the sample on Windows Server 2003, create an IIS application 
@ECHO pool and set its identity to a BizTalk application user account. Update 
@ECHO the orchestration web service virtual directory to run within this 
@ECHO application pool.
@ECHO.
@ECHO Give the "BizTalk Application Users" login db_owner permission to the
@ECHO "BTSCompensationSampleMailingList" and "Northwind" databases.

@ENDLOCAL

@PAUSE
