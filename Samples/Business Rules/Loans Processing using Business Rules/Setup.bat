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

@ECHO Removing In-Out folders...
@rmdir /s /q "In"
@rmdir /s /q "Out"


@CALL "%VS140COMNTOOLS%vsvars32.bat"
@IF ERRORLEVEL 1 (
	@echo Unable to setup environment variables for Visual Studio
	@goto End
)


@SET SolutionName=LoansProcessor.sln
@SET BindingFileName=LoanProcessorBinding.xml
@SET SendPortName=LoansProcessor_1.0.0.0_Microsoft.Samples.BizTalk.LoansProcessor.My_Sample_Service_Outgoing
@SET ReceivePortName=LoansProcessor_1.0.0.0_Microsoft.Samples.BizTalk.LoansProcessor.My_Sample_Service_Incoming
@SET ReceiveLocationName=LoansProcessor_1.0.0.0_Microsoft.Samples.BizTalk.LoansProcessor.My_Sample_Service_Incoming_ReceiveLocation
@SET FileReceiveDirectory=In
@SET FileReceiveLocation="\%FileReceiveDirectory%\*.xml"
@SET FileSendDirectory=Out
@SET FileSendAddress="\%FileSendDirectory%\%%MessageID%%.xml"
@SET OrchestrationName=Microsoft.Samples.BizTalk.LoansProcessor.My_Sample_Service
@SET AssemblyName=LoansProcessor
@SET AssemblyKeyFile1=myFactRetriever.snk
@SET AssemblyKeyFile2=LoansProcessor.snk

@ECHO.
@ECHO Building the business object DLL and putting the .NET proxy in the GAC ...

@IF NOT EXIST myFactRetriever\%AssemblyKeyFile1% sn -k myFactRetriever\%AssemblyKeyFile1%

@msbuild myFactRetriever\myFactRetriever.sln /t:Build /p:Configuration=Debug /fileLogger /fileLoggerParameters:LogFile=Build.log
@GacUtil -if myFactRetriever\bin\Debug\myFactRetriever.dll

@msbuild CreateRules\CreateLoanProcessingPolicy.sln /t:Build /p:Configuration=Debug /fileLogger /fileLoggerParameters:LogFile=Build.log

@ECHO.
@ECHO Copying sampleLoan.xml to CreateRules\bin\debug
@copy sampleLoan.xml CreateRules\bin\Debug\sampleLoan.xml 

@ECHO.
@ECHO Creating sample table in Northwind Database and populating with sample data 
@osql -E -i Create_CustInfo_Table.sql

@ECHO.
@ECHO Running CreateRules.exe to generate the Loans Processing Policy
@pushd CreateRules\bin\debug
@CreateRules.exe
@popd

@ECHO.
@ECHO Creating the send and receive directory...

@IF NOT EXIST %FileReceiveDirectory% MD %FileReceiveDirectory%
@IF NOT EXIST %FileSendDirectory% MD %FileSendDirectory%

@ECHO.
@ECHO Building the project and deploying the sample assembly...

@IF NOT EXIST %AssemblyKeyFile2% sn -k %AssemblyKeyFile2%

@msbuild %SolutionName% /t:Build /p:Configuration=Debug /fileLogger /fileLoggerParameters:LogFile=Build.log
@BTSTask AddApp -ApplicationName:LoansProcessorApplication
@BTSTask AddResource -ApplicationName:LoansProcessorApplication -Type:System.BizTalk:BizTalkAssembly -Source:bin\Debug\%AssemblyName%.dll -Options:GacOnAdd

@ECHO.
@ECHO Creating and binding the ports...

@BTSTask ImportBindings -Source:%BindingFileName% -ApplicationName:LoansProcessorApplication > Binding.log

@ECHO.
@ECHO Calling the WMI script to start the send port...

@CScript /NoLogo "..\..\Admin\WMI\Start Send Port\VBScript\StartSendPort.vbs" %SendPortName% %FileSendAddress%

@ECHO.
@ECHO Calling the WMI script to enable the receive location...

@CScript /NoLogo "..\..\Admin\WMI\Enable Receive Location\VBScript\EnableRecLoc.vbs" %ReceivePortName% %ReceiveLocationName% %FileReceiveLocation%

@ECHO.
@ECHO Calling the WMI script to enlist and start the orchestration...

@CScript /NoLogo "..\..\Admin\WMI\Enlist Orchestration\VBScript\EnlistOrch.vbs" %OrchestrationName% %AssemblyName% Start

@ENDLOCAL

:End
@PAUSE
