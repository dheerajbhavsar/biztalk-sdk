@REM --------------------------------------------------------------------
@REM File: Cleanup.bat
@REM
@REM Summary: Calls scripts and programs to stop and undeploy the sample.
@REM
@REM Sample: PartyResolution
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

@SET SupplierOrchestrationName=Microsoft.Samples.BizTalk.Supplier.SupplierProcess
@SET SupplierAssemblyName=Supplier
@SET ShipmentAgency1OrchestrationName=Microsoft.Samples.BizTalk.ShipmentAgency1.ShipmentAgency1Process
@SET ShipmentAgency1AssemblyName=ShipmentAgency1
@SET ShipmentAgency2OrchestrationName=Microsoft.Samples.BizTalk.ShipmentAgency2.ShipmentAgency2Process
@SET ShipmentAgency2AssemblyName=ShipmentAgency2
@SET BuyerOrchestrationName=Microsoft.Samples.BizTalk.Buyer.BuyerProcess
@SET BuyerAssemblyName=Buyer

@SET PODRSendPortName=SP_PODeliveryReceipt
@SET FTSA1SendPortName=SP_FilesToShipmentAgency1
@SET FTSA2SendPortName=SP_FilesToShipmentAgency2
@SET POASendPortName=SP_POAck
@SET SASendPortName=SP_ShipmentAgency_Ack
@SET BSendPortName=SP_SendPOToSupplier

@SET RPOFBReceivePortName=RP_ReceivePOFromBuyer
@SET RSAAReceivePortName=RP_Receive_ShipmentAgency_Ack
@SET SA1ReceivePortName=RP_ShipmentAgency1_OrderFiles
@SET SA2ReceivePortName=RP_ShipmentAgency2_OrderFiles
@SET BReceivePortName=RP_ReceivePOFromInternal

@SET Ship1Party=ShipmentAgency1
@SET Ship2Party=ShipmentAgency2
@SET BuyerParty=BuyerAgency

@REM Check that other samples that are used to clean this sample up are built
@IF NOT EXIST ..\..\Admin\ExplorerOM\UnenlistParties\bin\Debug\UnenlistParties.exe (
	@ECHO.
	@ECHO The UnenlistParties ExplorerOM sample must be built to help remove this sample.
	@GOTO :End
)

@IF NOT EXIST ..\..\Admin\ExplorerOM\DeleteParty\bin\Debug\DeleteParty.exe (
	@ECHO.
	@ECHO The DeleteParty ExplorerOM sample must be built to help remove this sample.
	@GOTO :End
)

@ECHO.
@ECHO Calling the WMI script to stop and unenlist the orchestrations...

@CScript /NoLogo "..\..\Admin\WMI\Stop Orchestration\VBScript\StopOrch.vbs" %SupplierOrchestrationName% %SupplierAssemblyName% Unenlist
@CScript /NoLogo "..\..\Admin\WMI\Stop Orchestration\VBScript\StopOrch.vbs" %ShipmentAgency1OrchestrationName% %ShipmentAgency1AssemblyName% Unenlist
@CScript /NoLogo "..\..\Admin\WMI\Stop Orchestration\VBScript\StopOrch.vbs" %ShipmentAgency2OrchestrationName% %ShipmentAgency2AssemblyName% Unenlist
@CScript /NoLogo "..\..\Admin\WMI\Stop Orchestration\VBScript\StopOrch.vbs" %BuyerOrchestrationName% %BuyerAssemblyName% Unenlist

@ECHO.
@ECHO Unenlisting the Parties...

@..\..\Admin\ExplorerOM\UnenlistParties\bin\Debug\UnenlistParties.exe %SupplierAssemblyName%

@ECHO.
@ECHO Removing the Parties...

@..\..\Admin\ExplorerOM\DeleteParty\bin\Debug\DeleteParty.exe %Ship1Party%
@..\..\Admin\ExplorerOM\DeleteParty\bin\Debug\DeleteParty.exe %Ship2Party%
@..\..\Admin\ExplorerOM\DeleteParty\bin\Debug\DeleteParty.exe %BuyerParty%

@ECHO.
@ECHO Undeploying the assemblies...

@FOR /F "usebackq tokens=*" %%i IN (`dir Buyer\bin\Release\Buyer.dll /s /b`) DO @SET FullAssemblyPath=%%i
@FOR /F "usebackq tokens=4*" %%i IN (`sn -Tp "%FullAssemblyPath%" ^| findstr /c:"Public key token is"`) DO @SET AssemblyPublicKeyToken=%%j
@BTSTask RemoveResource -Luid="Buyer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=%AssemblyPublicKeyToken%"

@FOR /F "usebackq tokens=*" %%i IN (`dir ShipmentAgency1\bin\Release\ShipmentAgency1.dll /s /b`) DO @SET FullAssemblyPath=%%i
@FOR /F "usebackq tokens=4*" %%i IN (`sn -Tp "%FullAssemblyPath%" ^| findstr /c:"Public key token is"`) DO @SET AssemblyPublicKeyToken=%%j
@BTSTask RemoveResource -Luid="ShipmentAgency1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=%AssemblyPublicKeyToken%"

@FOR /F "usebackq tokens=*" %%i IN (`dir ShipmentAgency2\bin\Release\ShipmentAgency2.dll /s /b`) DO @SET FullAssemblyPath=%%i
@FOR /F "usebackq tokens=4*" %%i IN (`sn -Tp "%FullAssemblyPath%" ^| findstr /c:"Public key token is"`) DO @SET AssemblyPublicKeyToken=%%j
@BTSTask RemoveResource -Luid="ShipmentAgency2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=%AssemblyPublicKeyToken%"

@FOR /F "usebackq tokens=*" %%i IN (`dir Supplier\bin\Release\Supplier.dll /s /b`) DO @SET FullAssemblyPath=%%i
@FOR /F "usebackq tokens=4*" %%i IN (`sn -Tp "%FullAssemblyPath%" ^| findstr /c:"Public key token is"`) DO @SET AssemblyPublicKeyToken=%%j
@BTSTask RemoveResource -Luid="Supplier, Version=1.0.0.0, Culture=neutral, PublicKeyToken=%AssemblyPublicKeyToken%"

@FOR /F "usebackq tokens=*" %%i IN (`dir Pipeline\projectschema\bin\Release\ProjectSchema.dll /s /b`) DO @SET FullAssemblyPath=%%i
@FOR /F "usebackq tokens=4*" %%i IN (`sn -Tp "%FullAssemblyPath%" ^| findstr /c:"Public key token is"`) DO @SET AssemblyPublicKeyToken=%%j
@BTSTask RemoveResource -Luid="ProjectSchema, Version=1.0.0.0, Culture=neutral, PublicKeyToken=%AssemblyPublicKeyToken%"

@FOR /F "usebackq tokens=*" %%i IN (`dir Schemas\bin\Release\Schemas.dll /s /b`) DO @SET FullAssemblyPath=%%i
@FOR /F "usebackq tokens=4*" %%i IN (`sn -Tp "%FullAssemblyPath%" ^| findstr /c:"Public key token is"`) DO @SET AssemblyPublicKeyToken=%%j
@BTSTask RemoveResource -Luid="Schemas, Version=1.0.0.0, Culture=neutral, PublicKeyToken=%AssemblyPublicKeyToken%"

@ECHO.
@ECHO We need to remove the assemblies from the GAC...
@GacUtil /u CheckPartyname
@GacUtil /u QueryShipmentCatalogComponent

@ECHO.
@ECHO Calling the WMI script to remove the send ports...

@CScript /NoLogo "..\..\Admin\WMI\Remove Send Port\VBScript\RemoveSendPort.vbs" %PODRSendPortName%
@CScript /NoLogo "..\..\Admin\WMI\Remove Send Port\VBScript\RemoveSendPort.vbs" %FTSA1SendPortName%
@CScript /NoLogo "..\..\Admin\WMI\Remove Send Port\VBScript\RemoveSendPort.vbs" %FTSA2SendPortName%
@CScript /NoLogo "..\..\Admin\WMI\Remove Send Port\VBScript\RemoveSendPort.vbs" %POASendPortName%
@CScript /NoLogo "..\..\Admin\WMI\Remove Send Port\VBScript\RemoveSendPort.vbs" %SASendPortName%
@CScript /NoLogo "..\..\Admin\WMI\Remove Send Port\VBScript\RemoveSendPort.vbs" %BSendPortName%

@ECHO.
@ECHO Calling the WMI script to remove the receive ports...

@CScript /NoLogo "..\..\Admin\WMI\Remove Receive Port\VBScript\RemoveReceivePort.vbs" %RPOFBReceivePortName%
@CScript /NoLogo "..\..\Admin\WMI\Remove Receive Port\VBScript\RemoveReceivePort.vbs" %RSAAReceivePortName%
@CScript /NoLogo "..\..\Admin\WMI\Remove Receive Port\VBScript\RemoveReceivePort.vbs" %SA1ReceivePortName%
@CScript /NoLogo "..\..\Admin\WMI\Remove Receive Port\VBScript\RemoveReceivePort.vbs" %SA2ReceivePortName%
@CScript /NoLogo "..\..\Admin\WMI\Remove Receive Port\VBScript\RemoveReceivePort.vbs" %BReceivePortName%

@ENDLOCAL

:End
@PAUSE
