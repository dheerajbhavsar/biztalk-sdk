@REM --------------------------------------------------------------------
@REM File: Cleanup.bat
@REM
@REM Summary: Calls scripts and programs to stop and undeploy the sample.
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

@SET OrchestrationName=Microsoft.Samples.BizTalk.LoansProcessor.My_Sample_Service
@SET AssemblyName=LoansProcessor
@SET SendPortName=LoansProcessor_1.0.0.0_Microsoft.Samples.BizTalk.LoansProcessor.My_Sample_Service_Outgoing
@SET ReceivePortName=LoansProcessor_1.0.0.0_Microsoft.Samples.BizTalk.LoansProcessor.My_Sample_Service_Incoming

@ECHO.
@ECHO Calling the WMI script to stop and unenlist the orchestration...

@CScript /NoLogo "..\..\Admin\WMI\Stop Orchestration\VBScript\StopOrch.vbs" %OrchestrationName% %AssemblyName% Unenlist

@ECHO.
@ECHO Calling the WMI script to unenlist the send port...

@CScript /NoLogo "..\..\Admin\WMI\Remove Send Port\VBScript\RemoveSendPort.vbs" %SendPortName% UnenlistOnly

@ECHO.
@ECHO Undeploying the assembly...

@BTSTask RemoveApp -ApplicationName:LoansProcessorApplication > Undeploy.log

@ECHO.
@ECHO We need to remove the .NET assembly from the GAC...
@GacUtil /u myFactRetriever
@IF EXIST CreateRules\bin\Debug\Loanprocessing.xml DEL CreateRules\bin\Debug\Loanprocessing.xml


:END
@ENDLOCAL
@PAUSE
