@REM --------------------------------------------------------------------
@REM File: Cleanup.bat
@REM
@REM Summary: Calls scripts and programs to stop and undeploy the sample.
@REM
@REM Sample: Expense Report Submission
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

@SET OrchestrationName=Microsoft.Samples.BizTalk.BTSExpenseDemo.BTSExpenseOrchestration
@SET AssemblyName=BTSExpenseDemo
@SET SendPortName=BTSExpenseDemoSendPort
@SET ReceivePortName=BTSExpenseDemoReceivePort
@SET VirtualDirectoryName=ExpenseReportSubmission

@ECHO.
@ECHO Calling the WMI script to stop and unenlist the orchestration...

@CScript /NoLogo "..\..\Admin\WMI\Stop Orchestration\VBScript\StopOrch.vbs" %OrchestrationName% %AssemblyName% Unenlist

@ECHO.
@ECHO Calling the WMI script to unenlist the send ports...

@CScript /NoLogo "..\..\Admin\WMI\Remove Send Port\VBScript\RemoveSendPort.vbs" %SendPortName% UnenlistOnly


@ECHO.
@ECHO Undeploying the assembly...

@BTSTask RemoveApp -ApplicationName:ExpenseDemoApplication > Undeploy.log

@ECHO.
@ECHO Removing the IIS vDir...

@CScript /NoLogo ..\..\RemoveIISVDir.vbs %VirtualDirectoryName%

@ENDLOCAL

@PAUSE
