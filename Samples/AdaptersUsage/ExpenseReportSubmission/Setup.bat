@REM --------------------------------------------------------------------
@REM File: Setup.bat
@REM
@REM Summary: Calls scripts and programs to build, deploy, and start the sample.
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

@SET SolutionName=BTSExpenseDemo\BTSExpenseDemo.sln
@SET AssemblyKeyFile=BTSExpenseDemo\BTSExpenseDemo.snk
@SET BindingFileName=BTSExpenseDemo\BTSExpenseDemoBinding.xml
@SET SendPortName=BTSExpenseDemoSendPort
@SET FileSendDirectory=Out
@SET FileSendAddress=\%FileSendDirectory%\%%MessageID%%.xml
@SET VirtualDirectoryName=ExpenseReportSubmission
@SET OrchestrationName=Microsoft.Samples.BizTalk.BTSExpenseDemo.BTSExpenseOrchestration
@SET AssemblyName=BTSExpenseDemo

@ECHO.
@ECHO Creating the send directory...

@IF NOT EXIST %FileSendDirectory% MD %FileSendDirectory%

@ECHO.
@ECHO Configuring the IIS vRoot...

@CScript /NoLogo ..\..\ConfigureIIS.vbs %VirtualDirectoryName%

@ECHO.
@ECHO If key file is not found, will generate a new key file...

@IF NOT EXIST %AssemblyKeyFile% sn -k %AssemblyKeyFile%

@ECHO.
@ECHO Building the project and deploying the sample assembly...

@msbuild %SolutionName% /t:Build /p:Configuration=Release /fileLogger /fileLoggerParameters:LogFile=Build.log

@gacutil /if %AssemblyName%\bin\Release\%AssemblyName%.dll
@BTSTask AddApp -ApplicationName:ExpenseDemoApplication
@BTSTask AddResource -ApplicationName:ExpenseDemoApplication -Type:System.BizTalk:BizTalkAssembly -Source:%AssemblyName%\bin\Release\%AssemblyName%.dll

@ECHO.
@ECHO Creating and binding the ports...

@BTSTask ImportBindings -Source:%BindingFileName% -ApplicationName:ExpenseDemoApplication > Binding.log

@ECHO.
@ECHO Calling the WMI script to start the send port...

@CScript /NoLogo "..\..\Admin\WMI\Start Send Port\VBScript\StartSendPort.vbs" %SendPortName% %FileSendAddress%

@ECHO.
@ECHO Calling the WMI script to enlist and start the orchestration...

@CScript /NoLogo "..\..\Admin\WMI\Enlist Orchestration\VBScript\EnlistOrch.vbs" %OrchestrationName% %AssemblyName% Start

@ENDLOCAL

@PAUSE
