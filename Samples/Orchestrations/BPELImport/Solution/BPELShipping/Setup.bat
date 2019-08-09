@REM --------------------------------------------------------------------
@REM File: Setup.bat
@REM
@REM Summary: Calls scripts and programs to build, deploy, and start the sample.
@REM
@REM Sample: Prebuilt BPELShipping sample
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

@SET SolutionName=BPELShipping.sln
@SET AssemblyKeyFile=..\BPELShipping.snk
@SET OrchestrationName=_OrderShippingProcess_Module_.OrderShippingProcess
@SET AssemblyName=BPELShipping
@SET BindingFileName=..\bindings\BPELBindings.xml




@ECHO.
@ECHO If key file is not found, will generate a new key file...

@IF NOT EXIST %AssemblyKeyFile% sn -k %AssemblyKeyFile%

@ECHO.
@ECHO Building the project
@msbuild %SolutionName% /t:Build /p:Configuration=Debug /fileLogger /fileLoggerParameters:LogFile=Build.log

@ECHO.
@ECHO GAC the assembly
@gacutil /if BPELShipping\bin\Debug\%AssemblyName%.dll

@ECHO.
@ECHO deploying the sample assembly...
@btstask AddResource -Type:System.BizTalk:BizTalkAssembly  -Source:BPELShipping\bin\Debug\BPELShipping.dll -Options:GacOnAdd

@ECHO.
@ECHO Creating and binding the ports...
@btstask ImportBindings -Source:%BindingFileName% > Binding.log


pushd ..
@SET SendPortName=BPELSendOrder
@SET FileSendDirectory=Ports\SendOrder
@SET FileSendAddress=\%FileSendDirectory%\%%MessageID%%.xml
@ECHO.
@ECHO Creating the send and receive directories...
@IF NOT EXIST %FileSendDirectory% MD %FileSendDirectory%
@ECHO.
@ECHO Calling the WMI script to start the send port...
@CScript /NoLogo "..\..\..\Admin\WMI\Start Send Port\VBScript\StartSendPort.vbs" %SendPortName% %FileSendAddress%
popd

pushd ..
@SET SendPortName=BPELPlaceShipRequest
@SET FileSendDirectory=Ports\PlaceShipOrder
@SET FileSendAddress=\%FileSendDirectory%\%%MessageID%%.xml
@ECHO.
@ECHO Creating the send and receive directories...
@IF NOT EXIST %FileSendDirectory% MD %FileSendDirectory%
@ECHO.
@ECHO Calling the WMI script to start the send port...
@CScript /NoLogo "..\..\..\Admin\WMI\Start Send Port\VBScript\StartSendPort.vbs" %SendPortName% %FileSendAddress%
popd

pushd ..
@SET SendPortName=BPELInvoiceResponse
@SET FileSendDirectory=Ports\InvoiceResponse
@SET FileSendAddress=\%FileSendDirectory%\%%MessageID%%.xml
@ECHO.
@ECHO Creating the send and receive directories...
@IF NOT EXIST %FileSendDirectory% MD %FileSendDirectory%
@ECHO.
@ECHO Calling the WMI script to start the send port...
@CScript /NoLogo "..\..\..\Admin\WMI\Start Send Port\VBScript\StartSendPort.vbs" %SendPortName% %FileSendAddress%
popd

pushd ..
@SET SendPortName=BPELConfirmOrder
@SET FileSendDirectory=Ports\FinalConfirmation
@SET FileSendAddress=\%FileSendDirectory%\%%MessageID%%.xml
@ECHO.
@ECHO Creating the send and receive directories...
@IF NOT EXIST %FileSendDirectory% MD %FileSendDirectory%
@ECHO.
@ECHO Calling the WMI script to start the send port...
@CScript /NoLogo "..\..\..\Admin\WMI\Start Send Port\VBScript\StartSendPort.vbs" %SendPortName% %FileSendAddress%
popd


pushd ..
@SET ReceivePortName=BPELShippingInbound
@SET ReceiveLocationName=BPELShippingInboundLocation
@SET FileReceiveDirectory=Ports\ReceiveShippingInfo
@SET FileReceiveLocation=\%FileReceiveDirectory%\*.xml
@ECHO.
@ECHO Creating the receive directories...
@IF NOT EXIST %FileReceiveDirectory% MD %FileReceiveDirectory%
@ECHO.
@ECHO Calling the WMI script to enable the receive location...
@CScript /NoLogo "..\..\..\Admin\WMI\Enable Receive Location\VBScript\EnableRecLoc.vbs" %ReceivePortName% %ReceiveLocationName% %FileReceiveLocation%
popd

pushd ..
@SET ReceivePortName=BPELReceiveShipConfirmation
@SET ReceiveLocationName=BPELReceiveShipConfirmationLocation
@SET FileReceiveDirectory=Ports\ReceiveShipConfirmation
@SET FileReceiveLocation=\%FileReceiveDirectory%\*.xml
@ECHO.
@ECHO Creating the receive directories...
@IF NOT EXIST %FileReceiveDirectory% MD %FileReceiveDirectory%
@ECHO.
@ECHO Calling the WMI script to enable the receive location...
@CScript /NoLogo "..\..\..\Admin\WMI\Enable Receive Location\VBScript\EnableRecLoc.vbs" %ReceivePortName% %ReceiveLocationName% %FileReceiveLocation%
popd

pushd ..
@SET ReceivePortName=BPELReceiveOrder
@SET ReceiveLocationName=BPELReceiveOrderLocation
@SET FileReceiveDirectory=Ports\ReceiveOrder
@SET FileReceiveLocation=\%FileReceiveDirectory%\*.xml
@ECHO.
@ECHO Creating the receive directories...
@IF NOT EXIST %FileReceiveDirectory% MD %FileReceiveDirectory%
@ECHO.
@ECHO Calling the WMI script to enable the receive location...
@CScript /NoLogo "..\..\..\Admin\WMI\Enable Receive Location\VBScript\EnableRecLoc.vbs" %ReceivePortName% %ReceiveLocationName% %FileReceiveLocation%
popd


pushd ..
@SET ReceivePortName=BPELInvoiceRequest
@SET ReceiveLocationName=BPELInvoiceRequestLocation
@SET FileReceiveDirectory=Ports\ReceiveInvoice
@SET FileReceiveLocation=\%FileReceiveDirectory%\*.xml
@ECHO.
@ECHO Creating the receive directories...
@IF NOT EXIST %FileReceiveDirectory% MD %FileReceiveDirectory%
@ECHO.
@ECHO Calling the WMI script to enable the receive location...
@CScript /NoLogo "..\..\..\Admin\WMI\Enable Receive Location\VBScript\EnableRecLoc.vbs" %ReceivePortName% %ReceiveLocationName% %FileReceiveLocation%
popd

@ECHO.
@ECHO Calling the WMI script to enlist and start the orchestration...

@CScript /NoLogo "..\..\..\..\Admin\WMI\Enlist Orchestration\VBScript\EnlistOrch.vbs" %OrchestrationName% %AssemblyName% Start
@ENDLOCAL

@PAUSE
