@REM --------------------------------------------------------------------
@REM File: Cleanup.bat
@REM
@REM Summary: Calls scripts and programs to stop and undeploy the sample.
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


@SET InfoPathFormLocation=C:\Temp\InfoPathForms
@SET ExpenseReportForm="Expense Report.xsn"
@SET ErrorReportForm="Expense Report - Resubmit.xsn"


@ECHO.
@ECHO Calling the WMI script to un-enlist orchestrations...

@CScript /NoLogo "..\..\Admin\WMI\Stop Orchestration\VBScript\StopOrch.vbs" ErrorHandling.ErrorHandler.SuspendMessage ErrorHandling.ErrorHandler Unenlist
@CScript /NoLogo "..\..\Admin\WMI\Stop Orchestration\VBScript\StopOrch.vbs" ErrorHandling.ErrorHandler.ResubmitLogic ErrorHandling.ErrorHandler Unenlist

@ECHO.
@ECHO Calling the WMI script to remove the send port...

@CScript /NoLogo "..\..\Admin\WMI\Remove Send Port\VBScript\RemoveSendPort.vbs" SendErrorReport_FILE UnenlistOnly
@CScript /NoLogo "..\..\Admin\WMI\Remove Send Port\VBScript\RemoveSendPort.vbs" SendExpenseReport_FILE UnenlistOnly


@ECHO.
@ECHO Remove the Error Handling Sample application...

@BTSTask RemoveApp -ApplicationName:"Error Handling Sample"

@ECHO.
@ECHO Removing the Assemblies from the gac...
@gacutil -uf ErrorHandling.PipelinesAndSchemas
@gacutil -uf ErrorHandling.ErrorHandler

@ECHO.
@ECHO Removing the IIS vDir...

@CScript /NoLogo ..\..\RemoveIISVDir.vbs ExpenseReports

@ECHO.
@ECHO Removing the InfoPath forms...
@IF EXIST %InfoPathFormLocation%\%ExpenseReportForm% DEL %InfoPathFormLocation%\%ExpenseReportForm%
@IF EXIST %InfoPathFormLocation%\%ErrorReportForm% DEL %InfoPathFormLocation%\%ErrorReportForm%
@IF EXIST %InfoPathFormLocation% RMDIR %InfoPathFormLocation%

@ENDLOCAL

@PAUSE
