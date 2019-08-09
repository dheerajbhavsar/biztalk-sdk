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

@SET AssemblyName=CustomPartyResolutionPipeline
@SET SendPortName=CustomPartyResolutionSendPort
@SET ReceivePortName=CustomPartyResolutionReceivePort

@REM Check that other samples that are used to clean this sample up are built
@IF NOT EXIST ..\..\Admin\ExplorerOM\DeleteParty\bin\Debug\DeleteParty.exe (
	@ECHO.
	@ECHO The DeleteParty ExplorerOM sample must be built to help remove this sample.
	@GOTO :End
)


@ECHO.
@ECHO Calling the WMI script to delete the parties...
@..\..\Admin\ExplorerOM\DeleteParty\bin\Debug\DeleteParty.exe "Bobs Bolts"
@..\..\Admin\ExplorerOM\DeleteParty\bin\Debug\DeleteParty.exe "Bens Hammers"

@ECHO.
@ECHO Calling the WMI script to unenlist the send port...

@CScript /NoLogo "..\..\Admin\WMI\Remove Send Port\VBScript\RemoveSendPort.vbs" %SendPortName% UnenlistOnly

@ECHO.
@ECHO Undeploying the assembly and removing sample application...

@BTSTask RemoveApp -ApplicationName:PartyResolutionApplication > Undeploy.log

@ECHO.
@ECHO Removing the pipeline component from the pipeline directory...

@DEL /F /Q "..\..\..\..\Pipeline Components\CustomPartyResolutionPipelineComponent.dll"
gacutil /u Microsoft.Samples.BizTalk.PartyLookup
:End
@ENDLOCAL
@PAUSE
