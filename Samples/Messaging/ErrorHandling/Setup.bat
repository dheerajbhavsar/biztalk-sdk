@REM --------------------------------------------------------------------
@REM File: Setup.bat
@REM
@REM Summary: Calls scripts and programs to build, deploy, and start the sample.
@REM
@REM Sample: Custom Pipeline Component
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

@SET ApplicationName="Error Handling Sample"
@SET SolutionName=ErrorHandling.sln
@SET SampleAssemblyKeyFile=ErrorHandling.snk
@SET BindingFileName=ErrorHandlingBinding.xml

@SET FileReceiveDirectoryExpense=ExpenseReportIn
@SET FileReceiveDirectoryResubmit=ResubmittedReportIn
@SET FileSendDirectory=ExpenseReportOut

@SET FileReceiveLocationExpense=\%FileReceiveDirectoryExpense%\*.xml
@SET FileReceiveLocationResubmit=\%FileReceiveDirectoryResubmit%\*.xml
@SET FileSendAddressExpense=\%FileSendDirectory%\Expense_%%MessageID%%.xml
@SET FileSendAddressError=\%FileSendDirectory%\ErrorReport_%%datetime.tz%%.xml

@SET InfoPathFormLocation=C:\Temp\InfoPathForms
@SET ExpenseReportForm="Expense Report.xsn"
@SET ErrorReportForm="Expense Report - Resubmit.xsn"

@ECHO.
@ECHO Creating the send and receive directories...

@IF NOT EXIST %FileReceiveDirectoryExpense% MD %FileReceiveDirectoryExpense%
@IF NOT EXIST %FileReceiveDirectoryResubmit% MD %FileReceiveDirectoryResubmit%
@IF NOT EXIST %FileSendDirectory% MD %FileSendDirectory%

@ECHO.
@ECHO Copying InfoPath forms...
@IF NOT EXIST %InfoPathFormLocation% MD %InfoPathFormLocation%
@IF NOT EXIST %InfoPathFormLocation%\%ExpenseReportForm% COPY .\InfoPathForms\%ExpenseReportForm% %InfoPathFormLocation%
@IF NOT EXIST %InfoPathFormLocation%\%ErrorReportForm% COPY .\InfoPathForms\%ErrorReportForm% %InfoPathFormLocation%

@ECHO.
@ECHO If key files are not found, will generate new key files...

@IF NOT EXIST %SampleAssemblyKeyFile% sn -k %SampleAssemblyKeyFile%

@ECHO.
@ECHO Building the project and gac'ing the sample assemblies...

@msbuild %SolutionName% /t:Build /p:Configuration=Release /fileLogger /fileLoggerParameters:LogFile=Build.log

@gacutil -uf ErrorHandling.PipelinesAndSchemas
@gacutil -uf ErrorHandling.ErrorHandler
@gacutil -if .\PipelinesAndSchemas\bin\Deployment\ErrorHandling.PipelinesAndSchemas.dll
@gacutil -if .\ErrorHandler\bin\Deployment\ErrorHandling.ErrorHandler.dll


@ECHO.
@ECHO Configuring the IIS vRoot...

@CScript /NoLogo ..\..\ConfigureIIS.vbs ExpenseReports

@ECHO.
@ECHO Creating a BizTalk application for the sample...
@BTSTask.exe AddApp -ApplicationName:%ApplicationName% -Description:"Error Handling sample"

@ECHO.
@ECHO Deploying sample assemblies to the application...

@btstask AddResource -ApplicationName:%ApplicationName% -Type:System.BizTalk:BizTalkAssembly -Source:".\PipelinesAndSchemas\bin\Deployment\ErrorHandling.PipelinesAndSchemas.dll" -Overwrite -Destination:"C:\ErrorHandlingSample\ErrorHandling.PipelinesAndSchemas.dll"
@btstask AddResource -ApplicationName:%ApplicationName% -Type:System.BizTalk:BizTalkAssembly -Source:".\ErrorHandler\bin\Deployment\ErrorHandling.ErrorHandler.dll" -Overwrite -Destination:"C:\ErrorHandlingSample\ErrorHandling.ErrorHandler.dll"


@ECHO.
@ECHO Importing binding information...

@btstask ImportBindings -Source:%BindingFileName% -ApplicationName:%ApplicationName%

@ECHO.
@ECHO Calling the WMI script to start the send ports...

@CScript /NoLogo "..\..\Admin\WMI\Start Send Port\VBScript\StartSendPort.vbs" SendExpenseReport_FILE %FileSendAddressExpense%
@CScript /NoLogo "..\..\Admin\WMI\Start Send Port\VBScript\StartSendPort.vbs" SendErrorReport_FILE %FileSendAddressError%


@ECHO.
@ECHO Calling the WMI script to enable the receive locations...

@CScript /NoLogo "..\..\Admin\WMI\Enable Receive Location\VBScript\EnableRecLoc.vbs" ReceiveResubmittedMessage ReceiveResubmittedMessage_FILE %FileReceiveLocationResubmit%
@CScript /NoLogo "..\..\Admin\WMI\Enable Receive Location\VBScript\EnableRecLoc.vbs" ReceiveResubmittedMessage ReceiveResubmittedMessage_HTTP
@CScript /NoLogo "..\..\Admin\WMI\Enable Receive Location\VBScript\EnableRecLoc.vbs" ReceiveExpenseReport ReceiveExpenseReport_FILE %FileReceiveLocationExpense%
@CScript /NoLogo "..\..\Admin\WMI\Enable Receive Location\VBScript\EnableRecLoc.vbs" ReceiveExpenseReport ReceiveExpenseReport_HTTP


@ECHO.
@ECHO Calling the WMI script to enlist and start the orchestrations...

@CScript /NoLogo "..\..\Admin\WMI\Enlist Orchestration\VBScript\EnlistOrch.vbs" ErrorHandling.ErrorHandler.SuspendMessage ErrorHandling.ErrorHandler Start
@CScript /NoLogo "..\..\Admin\WMI\Enlist Orchestration\VBScript\EnlistOrch.vbs" ErrorHandling.ErrorHandler.ResubmitLogic ErrorHandling.ErrorHandler Start

@ENDLOCAL

@PAUSE
