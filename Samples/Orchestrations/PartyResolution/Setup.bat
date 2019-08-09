@REM --------------------------------------------------------------------
@REM File: Setup.bat
@REM
@REM Summary: Calls scripts and programs to build, deploy, and start the sample.
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

@SET SolutionName=PartyResolution.sln
@SET AssemblyKeyFile=PartyResolution.snk

@SET SupplierBindingFileName=SupplierBinding.xml
@SET ShipmentAgency1BindingFileName=ShipingAgency1Binding.xml
@SET ShipmentAgency2BindingFileName=ShipingAgency2Binding.xml
@SET BuyerBindingFileName=BuyerBinding.xml

@SET SupplierOrchestrationName=Microsoft.Samples.BizTalk.Supplier.SupplierProcess
@SET SupplierAssemblyName=Supplier
@SET ShipmentAgency1OrchestrationName=Microsoft.Samples.BizTalk.ShipmentAgency1.ShipmentAgency1Process
@SET ShipmentAgency1AssemblyName=ShipmentAgency1
@SET ShipmentAgency2OrchestrationName=Microsoft.Samples.BizTalk.ShipmentAgency2.ShipmentAgency2Process
@SET ShipmentAgency2AssemblyName=ShipmentAgency2
@SET BuyerOrchestrationName=Microsoft.Samples.BizTalk.Buyer.BuyerProcess
@SET BuyerAssemblyName=Buyer

@SET PODRSendPortName=SP_PODeliveryReceipt
@SET PODRFileSendAddress=\FileDrop\PODeliveryReceipt\PODeliveryReceipt.xml
@SET FTSA1SendPortName=SP_FilesToShipmentAgency1
@SET FTSA1FileSendAddress=\FileDrop\ShipmentAgency1\ShipmentAgency1.txt
@SET FTSA2SendPortName=SP_FilesToShipmentAgency2
@SET FTSA2FileSendAddress=\FileDrop\ShipmentAgency2\ShipmentAgency2.txt
@SET POASendPortName=SP_POAck
@SET POAFileSendAddress=\FileDrop\PurchaseOrderAck\PurchaseOrderAck_%%MessageID%%.xml
@SET SASendPortName=SP_ShipmentAgency_Ack
@SET SAFileSendAddress=\FileDrop\ShipmentAck\ShipmentAgencyAck.xml
@SET BSendPortName=SP_SendPOToSupplier
@SET BFileSendAddress=\FileDrop\PurchaseOrdertoSupplier\PurchaseOrder.xml

@SET RPOFBReceivePortName=RP_ReceivePOFromBuyer
@SET RPOFBReceiveLocationName=RL_ReceivePOFromBuyer
@SET RPOFBFileReceiveLocation=\FileDrop\PurchaseOrdertoSupplier\*.xml
@SET RSAAReceivePortName=RP_Receive_ShipmentAgency_Ack
@SET RSAAReceiveLocationName=RL_Receive_ShipmentAgency_Ack
@SET RSAAFileReceiveLocation=\FileDrop\ShipmentAck\*.xml
@SET SA1ReceivePortName=RP_ShipmentAgency1_OrderFiles
@SET SA1ReceiveLocationName=RL_ShipmentAgency1_OrderFiles
@SET SA1FileReceiveLocation=\FileDrop\ShipmentAgency1\*.xml
@SET SA2ReceivePortName=RP_ShipmentAgency2_OrderFiles
@SET SA2ReceiveLocationName=RL_ShipmentAgency2_OrderFiles
@SET SA2FileReceiveLocation=\FileDrop\ShipmentAgency2\*.xml
@SET BReceivePortName=RP_ReceivePOFromInternal
@SET BReceiveLocationName=RL_ReceivePOFromInternal
@SET BFileReceiveLocation=\FileDrop\PurchaseOrder\*.xml

@ECHO.
@ECHO If key file is not found, will generate a new key file...

@IF NOT EXIST %AssemblyKeyFile% sn -k %AssemblyKeyFile%

@ECHO.
@ECHO Building the Solution...

@msbuild %SolutionName% /t:Build /p:Configuration=Debug /fileLogger /fileLoggerParameters:LogFile=Build.log

@ECHO.
@ECHO GAC the Assemblies...

@Gacutil /i CheckPartyName\bin\Debug\CheckPartyname.dll /f
@Gacutil /i QueryShipmentCatalogComponent\bin\Debug\QueryShipmentCatalogComponent.dll /f

@ECHO.
@ECHO Deploy Assemblies...

btstask AddResource -Type:System.BizTalk:BizTalkAssembly  -Source:Schemas\bin\Release\Schemas.dll -Options:GacOnAdd
btstask AddResource -Type:System.BizTalk:BizTalkAssembly  -Source:Pipeline\projectschema\bin\Release\ProjectSchema.dll -Options:GacOnAdd 
btstask AddResource -Type:System.BizTalk:BizTalkAssembly  -Source:Buyer\bin\Release\Buyer.dll -Options:GacOnAdd
btstask ImportBindings -Source:%BuyerBindingFileName%
btstask AddResource -Type:System.BizTalk:BizTalkAssembly  -Source:ShipmentAgency1\bin\Release\ShipmentAgency1.dll -Options:GacOnAdd
btstask ImportBindings -Source:%ShipmentAgency1BindingFileName%
btstask AddResource -Type:System.BizTalk:BizTalkAssembly  -Source:ShipmentAgency2\bin\Release\ShipmentAgency2.dll -Options:GacOnAdd
btstask ImportBindings -Source:%ShipmentAgency2BindingFileName%
btstask AddResource -Type:System.BizTalk:BizTalkAssembly  -Source:Supplier\bin\Release\Supplier.dll -Options:GacOnAdd
btstask ImportBindings -Source:%SupplierBindingFileName%

@ECHO.
@ECHO Calling the WMI script to start the send ports...

@CScript /NoLogo "..\..\Admin\WMI\Start Send Port\VBScript\StartSendPort.vbs" %PODRSendPortName% %PODRFileSendAddress%
@CScript /NoLogo "..\..\Admin\WMI\Start Send Port\VBScript\StartSendPort.vbs" %FTSA1SendPortName% %FTSA1FileSendAddress%
@CScript /NoLogo "..\..\Admin\WMI\Start Send Port\VBScript\StartSendPort.vbs" %FTSA2SendPortName% %FTSA2FileSendAddress%
@CScript /NoLogo "..\..\Admin\WMI\Start Send Port\VBScript\StartSendPort.vbs" %POASendPortName% %POAFileSendAddress%
@CScript /NoLogo "..\..\Admin\WMI\Start Send Port\VBScript\StartSendPort.vbs" %SASendPortName% %SAFileSendAddress%
@CScript /NoLogo "..\..\Admin\WMI\Start Send Port\VBScript\StartSendPort.vbs" %BSendPortName% %BFileSendAddress%

@ECHO.
@ECHO Calling the WMI script to enable the receive locations...

@CScript /NoLogo "..\..\Admin\WMI\Enable Receive Location\VBScript\EnableRecLoc.vbs" %RPOFBReceivePortName% %RPOFBReceiveLocationName% %RPOFBFileReceiveLocation%
@CScript /NoLogo "..\..\Admin\WMI\Enable Receive Location\VBScript\EnableRecLoc.vbs" %RSAAReceivePortName% %RSAAReceiveLocationName% %RSAAFileReceiveLocation%
@CScript /NoLogo "..\..\Admin\WMI\Enable Receive Location\VBScript\EnableRecLoc.vbs" %SA1ReceivePortName% %SA1ReceiveLocationName% %SA1FileReceiveLocation%
@CScript /NoLogo "..\..\Admin\WMI\Enable Receive Location\VBScript\EnableRecLoc.vbs" %SA2ReceivePortName% %SA2ReceiveLocationName% %SA2FileReceiveLocation%
@CScript /NoLogo "..\..\Admin\WMI\Enable Receive Location\VBScript\EnableRecLoc.vbs" %BReceivePortName% %BReceiveLocationName% %BFileReceiveLocation%

@ECHO.
@ECHO Calling the WMI script to enlist and start the orchestrations...

@CScript /NoLogo "..\..\Admin\WMI\Enlist Orchestration\VBScript\EnlistOrch.vbs" %SupplierOrchestrationName% %SupplierAssemblyName% Start
@CScript /NoLogo "..\..\Admin\WMI\Enlist Orchestration\VBScript\EnlistOrch.vbs" %ShipmentAgency1OrchestrationName% %ShipmentAgency1AssemblyName% Start
@CScript /NoLogo "..\..\Admin\WMI\Enlist Orchestration\VBScript\EnlistOrch.vbs" %ShipmentAgency2OrchestrationName% %ShipmentAgency2AssemblyName% Start
@CScript /NoLogo "..\..\Admin\WMI\Enlist Orchestration\VBScript\EnlistOrch.vbs" %BuyerOrchestrationName% %BuyerAssemblyName% Start

@ENDLOCAL

@PAUSE
